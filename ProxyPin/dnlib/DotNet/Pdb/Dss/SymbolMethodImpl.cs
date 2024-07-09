using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Threading;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097B RID: 2427
	internal sealed class SymbolMethodImpl : SymbolMethod
	{
		// Token: 0x06005D83 RID: 23939 RVA: 0x001C10B0 File Offset: 0x001C10B0
		public SymbolMethodImpl(SymbolReaderImpl reader, ISymUnmanagedMethod method)
		{
			this.reader = reader;
			this.method = method;
			this.asyncMethod = (method as ISymUnmanagedAsyncMethod);
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x001C10D4 File Offset: 0x001C10D4
		public override int Token
		{
			get
			{
				uint result;
				this.method.GetToken(out result);
				return (int)result;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x001C10F4 File Offset: 0x001C10F4
		public override SymbolScope RootScope
		{
			get
			{
				if (this.rootScope == null)
				{
					ISymUnmanagedScope symUnmanagedScope;
					this.method.GetRootScope(out symUnmanagedScope);
					Interlocked.CompareExchange<SymbolScope>(ref this.rootScope, (symUnmanagedScope == null) ? null : new SymbolScopeImpl(symUnmanagedScope, this, null), null);
				}
				return this.rootScope;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x001C1148 File Offset: 0x001C1148
		public override IList<SymbolSequencePoint> SequencePoints
		{
			get
			{
				if (this.sequencePoints == null)
				{
					uint num;
					this.method.GetSequencePointCount(out num);
					SymbolSequencePoint[] array = new SymbolSequencePoint[num];
					int[] array2 = new int[array.Length];
					new ISymbolDocument[array.Length];
					int[] array3 = new int[array.Length];
					int[] array4 = new int[array.Length];
					int[] array5 = new int[array.Length];
					int[] array6 = new int[array.Length];
					ISymUnmanagedDocument[] array7 = new ISymUnmanagedDocument[array.Length];
					if (array.Length != 0)
					{
						uint num2;
						this.method.GetSequencePoints((uint)array.Length, out num2, array2, array7, array3, array4, array5, array6);
					}
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new SymbolSequencePoint
						{
							Offset = array2[i],
							Document = new SymbolDocumentImpl(array7[i]),
							Line = array3[i],
							Column = array4[i],
							EndLine = array5[i],
							EndColumn = array6[i]
						};
					}
					this.sequencePoints = array;
				}
				return this.sequencePoints;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x001C1268 File Offset: 0x001C1268
		public int AsyncKickoffMethod
		{
			get
			{
				if (this.asyncMethod == null || !this.asyncMethod.IsAsyncMethod())
				{
					return 0;
				}
				return (int)this.asyncMethod.GetKickoffMethod();
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x001C1294 File Offset: 0x001C1294
		public uint? AsyncCatchHandlerILOffset
		{
			get
			{
				if (this.asyncMethod == null || !this.asyncMethod.IsAsyncMethod())
				{
					return null;
				}
				if (!this.asyncMethod.HasCatchHandlerILOffset())
				{
					return null;
				}
				return new uint?(this.asyncMethod.GetCatchHandlerILOffset());
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x001C12F4 File Offset: 0x001C12F4
		public IList<SymbolAsyncStepInfo> AsyncStepInfos
		{
			get
			{
				if (this.asyncMethod == null || !this.asyncMethod.IsAsyncMethod())
				{
					return null;
				}
				if (this.asyncStepInfos == null)
				{
					uint asyncStepInfoCount = this.asyncMethod.GetAsyncStepInfoCount();
					uint[] array = new uint[asyncStepInfoCount];
					uint[] array2 = new uint[asyncStepInfoCount];
					uint[] array3 = new uint[asyncStepInfoCount];
					this.asyncMethod.GetAsyncStepInfo(asyncStepInfoCount, out asyncStepInfoCount, array, array2, array3);
					SymbolAsyncStepInfo[] array4 = new SymbolAsyncStepInfo[asyncStepInfoCount];
					for (int i = 0; i < array4.Length; i++)
					{
						array4[i] = new SymbolAsyncStepInfo(array[i], array2[i], array3[i]);
					}
					this.asyncStepInfos = array4;
				}
				return this.asyncStepInfos;
			}
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x001C13AC File Offset: 0x001C13AC
		public override void GetCustomDebugInfos(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			this.reader.GetCustomDebugInfos(this, method, body, result);
		}

		// Token: 0x04002D5E RID: 11614
		private readonly SymbolReaderImpl reader;

		// Token: 0x04002D5F RID: 11615
		private readonly ISymUnmanagedMethod method;

		// Token: 0x04002D60 RID: 11616
		private readonly ISymUnmanagedAsyncMethod asyncMethod;

		// Token: 0x04002D61 RID: 11617
		private volatile SymbolScope rootScope;

		// Token: 0x04002D62 RID: 11618
		private volatile SymbolSequencePoint[] sequencePoints;

		// Token: 0x04002D63 RID: 11619
		private volatile SymbolAsyncStepInfo[] asyncStepInfos;
	}
}
