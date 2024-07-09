using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.Threading;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200082B RID: 2091
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ParameterList_CollectionDebugView))]
	[ComVisible(true)]
	public sealed class ParameterList : IList<Parameter>, ICollection<Parameter>, IEnumerable<Parameter>, IEnumerable
	{
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x0018545C File Offset: 0x0018545C
		public MethodDef Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x00185464 File Offset: 0x00185464
		public int Count
		{
			get
			{
				this.theLock.EnterReadLock();
				int count;
				try
				{
					count = this.parameters.Count;
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x001854AC File Offset: 0x001854AC
		public int MethodSigIndexBase
		{
			get
			{
				this.theLock.EnterReadLock();
				int result;
				try
				{
					result = ((this.methodSigIndexBase == 1) ? 1 : 0);
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x17000F90 RID: 3984
		public Parameter this[int index]
		{
			get
			{
				this.theLock.EnterReadLock();
				Parameter result;
				try
				{
					result = this.parameters[index];
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return result;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x0018554C File Offset: 0x0018554C
		public Parameter ReturnParameter
		{
			get
			{
				this.theLock.EnterReadLock();
				Parameter result;
				try
				{
					result = this.returnParameter;
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00185590 File Offset: 0x00185590
		public ParameterList(MethodDef method, TypeDef declaringType)
		{
			this.method = method;
			this.parameters = new List<Parameter>();
			this.methodSigIndexBase = -1;
			this.hiddenThisParameter = new Parameter(this, 0, -2);
			this.returnParameter = new Parameter(this, -1, -1);
			this.UpdateThisParameterType(declaringType);
			this.UpdateParameterTypes();
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x001855F8 File Offset: 0x001855F8
		internal void UpdateThisParameterType(TypeDef methodDeclaringType)
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (methodDeclaringType == null)
				{
					this.hiddenThisParameter.Type = null;
				}
				else if (methodDeclaringType.IsValueType)
				{
					this.hiddenThisParameter.Type = new ByRefSig(new ValueTypeSig(methodDeclaringType));
				}
				else
				{
					this.hiddenThisParameter.Type = new ClassSig(methodDeclaringType);
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00185680 File Offset: 0x00185680
		public void UpdateParameterTypes()
		{
			this.theLock.EnterWriteLock();
			try
			{
				MethodSig methodSig = this.method.MethodSig;
				if (methodSig == null)
				{
					this.methodSigIndexBase = -1;
					this.parameters.Clear();
				}
				else
				{
					if (this.UpdateThisParameter_NoLock(methodSig))
					{
						this.parameters.Clear();
					}
					this.returnParameter.Type = methodSig.RetType;
					this.ResizeParameters_NoLock(methodSig.Params.Count + this.methodSigIndexBase);
					if (this.methodSigIndexBase > 0)
					{
						this.parameters[0] = this.hiddenThisParameter;
					}
					for (int i = 0; i < methodSig.Params.Count; i++)
					{
						this.parameters[i + this.methodSigIndexBase].Type = methodSig.Params[i];
					}
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00185780 File Offset: 0x00185780
		private bool UpdateThisParameter_NoLock(MethodSig methodSig)
		{
			int num;
			if (methodSig == null)
			{
				num = -1;
			}
			else
			{
				num = (methodSig.ImplicitThis ? 1 : 0);
			}
			if (this.methodSigIndexBase == num)
			{
				return false;
			}
			this.methodSigIndexBase = num;
			return true;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x001857C8 File Offset: 0x001857C8
		private void ResizeParameters_NoLock(int length)
		{
			if (this.parameters.Count == length)
			{
				return;
			}
			if (this.parameters.Count < length)
			{
				for (int i = this.parameters.Count; i < length; i++)
				{
					this.parameters.Add(new Parameter(this, i, i - this.methodSigIndexBase));
				}
				return;
			}
			while (this.parameters.Count > length)
			{
				this.parameters.RemoveAt(this.parameters.Count - 1);
			}
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00185858 File Offset: 0x00185858
		internal ParamDef FindParamDef(Parameter param)
		{
			this.theLock.EnterReadLock();
			ParamDef result;
			try
			{
				result = this.FindParamDef_NoLock(param);
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0018589C File Offset: 0x0018589C
		private ParamDef FindParamDef_NoLock(Parameter param)
		{
			int num;
			if (param.IsReturnTypeParameter)
			{
				num = 0;
			}
			else
			{
				if (!param.IsNormalMethodParameter)
				{
					return this.hiddenThisParamDef;
				}
				num = param.MethodSigIndex + 1;
			}
			IList<ParamDef> paramDefs = this.method.ParamDefs;
			int count = paramDefs.Count;
			for (int i = 0; i < count; i++)
			{
				ParamDef paramDef = paramDefs[i];
				if (paramDef != null && (int)paramDef.Sequence == num)
				{
					return paramDef;
				}
			}
			return null;
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00185924 File Offset: 0x00185924
		internal void TypeUpdated(Parameter param)
		{
			MethodSig methodSig = this.method.MethodSig;
			if (methodSig == null)
			{
				return;
			}
			int methodSigIndex = param.MethodSigIndex;
			if (methodSigIndex == -1)
			{
				methodSig.RetType = param.Type;
				return;
			}
			if (methodSigIndex >= 0)
			{
				methodSig.Params[methodSigIndex] = param.Type;
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0018597C File Offset: 0x0018597C
		internal void CreateParamDef(Parameter param)
		{
			this.theLock.EnterWriteLock();
			try
			{
				ParamDef paramDef = this.FindParamDef_NoLock(param);
				if (paramDef == null)
				{
					if (param.IsHiddenThisParameter)
					{
						this.hiddenThisParamDef = this.UpdateRowId_NoLock(new ParamDefUser(UTF8String.Empty, ushort.MaxValue, (ParamAttributes)0));
					}
					else
					{
						int num = param.IsReturnTypeParameter ? 0 : (param.MethodSigIndex + 1);
						paramDef = this.UpdateRowId_NoLock(new ParamDefUser(UTF8String.Empty, (ushort)num, (ParamAttributes)0));
						this.method.ParamDefs.Add(paramDef);
					}
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00185A34 File Offset: 0x00185A34
		private ParamDef UpdateRowId_NoLock(ParamDef pd)
		{
			TypeDef declaringType = this.method.DeclaringType;
			if (declaringType == null)
			{
				return pd;
			}
			ModuleDef module = declaringType.Module;
			if (module == null)
			{
				return pd;
			}
			return module.UpdateRowId<ParamDef>(pd);
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00185A70 File Offset: 0x00185A70
		public int IndexOf(Parameter item)
		{
			this.theLock.EnterReadLock();
			int result;
			try
			{
				result = this.parameters.IndexOf(item);
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00185AB8 File Offset: 0x00185AB8
		void IList<Parameter>.Insert(int index, Parameter item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00185AC0 File Offset: 0x00185AC0
		void IList<Parameter>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x00185AC8 File Offset: 0x00185AC8
		void ICollection<Parameter>.Add(Parameter item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00185AD0 File Offset: 0x00185AD0
		void ICollection<Parameter>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00185AD8 File Offset: 0x00185AD8
		bool ICollection<Parameter>.Contains(Parameter item)
		{
			this.theLock.EnterReadLock();
			bool result;
			try
			{
				result = this.parameters.Contains(item);
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00185B20 File Offset: 0x00185B20
		void ICollection<Parameter>.CopyTo(Parameter[] array, int arrayIndex)
		{
			this.theLock.EnterReadLock();
			try
			{
				this.parameters.CopyTo(array, arrayIndex);
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x00185B68 File Offset: 0x00185B68
		bool ICollection<Parameter>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00185B6C File Offset: 0x00185B6C
		bool ICollection<Parameter>.Remove(Parameter item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x00185B74 File Offset: 0x00185B74
		public ParameterList.Enumerator GetEnumerator()
		{
			return new ParameterList.Enumerator(this);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00185B7C File Offset: 0x00185B7C
		IEnumerator<Parameter> IEnumerable<Parameter>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00185B8C File Offset: 0x00185B8C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002689 RID: 9865
		private readonly MethodDef method;

		// Token: 0x0400268A RID: 9866
		private readonly List<Parameter> parameters;

		// Token: 0x0400268B RID: 9867
		private readonly Parameter hiddenThisParameter;

		// Token: 0x0400268C RID: 9868
		private ParamDef hiddenThisParamDef;

		// Token: 0x0400268D RID: 9869
		private readonly Parameter returnParameter;

		// Token: 0x0400268E RID: 9870
		private int methodSigIndexBase;

		// Token: 0x0400268F RID: 9871
		private readonly Lock theLock = Lock.Create();

		// Token: 0x02000FF4 RID: 4084
		public struct Enumerator : IEnumerator<Parameter>, IDisposable, IEnumerator
		{
			// Token: 0x06008E94 RID: 36500 RVA: 0x002AA26C File Offset: 0x002AA26C
			internal Enumerator(ParameterList list)
			{
				this.list = list;
				this.current = null;
				list.theLock.EnterReadLock();
				try
				{
					this.listEnumerator = list.parameters.GetEnumerator();
				}
				finally
				{
					list.theLock.ExitReadLock();
				}
			}

			// Token: 0x17001DE2 RID: 7650
			// (get) Token: 0x06008E95 RID: 36501 RVA: 0x002AA2C4 File Offset: 0x002AA2C4
			public Parameter Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17001DE3 RID: 7651
			// (get) Token: 0x06008E96 RID: 36502 RVA: 0x002AA2CC File Offset: 0x002AA2CC
			Parameter IEnumerator<Parameter>.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17001DE4 RID: 7652
			// (get) Token: 0x06008E97 RID: 36503 RVA: 0x002AA2D4 File Offset: 0x002AA2D4
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06008E98 RID: 36504 RVA: 0x002AA2DC File Offset: 0x002AA2DC
			public bool MoveNext()
			{
				this.list.theLock.EnterWriteLock();
				bool result;
				try
				{
					bool flag = this.listEnumerator.MoveNext();
					this.current = this.listEnumerator.Current;
					result = flag;
				}
				finally
				{
					this.list.theLock.ExitWriteLock();
				}
				return result;
			}

			// Token: 0x06008E99 RID: 36505 RVA: 0x002AA340 File Offset: 0x002AA340
			public void Dispose()
			{
				this.listEnumerator.Dispose();
			}

			// Token: 0x06008E9A RID: 36506 RVA: 0x002AA350 File Offset: 0x002AA350
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040043FC RID: 17404
			private readonly ParameterList list;

			// Token: 0x040043FD RID: 17405
			private List<Parameter>.Enumerator listEnumerator;

			// Token: 0x040043FE RID: 17406
			private Parameter current;
		}
	}
}
