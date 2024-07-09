using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200082C RID: 2092
	[ComVisible(true)]
	public sealed class Parameter : IVariable
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x00185B9C File Offset: 0x00185B9C
		public int Index
		{
			get
			{
				return this.paramIndex;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x00185BA4 File Offset: 0x00185BA4
		public int MethodSigIndex
		{
			get
			{
				return this.methodSigIndex;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x00185BAC File Offset: 0x00185BAC
		public bool IsNormalMethodParameter
		{
			get
			{
				return this.methodSigIndex >= 0;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x00185BBC File Offset: 0x00185BBC
		public bool IsHiddenThisParameter
		{
			get
			{
				return this.methodSigIndex == -2;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x00185BC8 File Offset: 0x00185BC8
		public bool IsReturnTypeParameter
		{
			get
			{
				return this.methodSigIndex == -1;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x00185BD4 File Offset: 0x00185BD4
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x00185BDC File Offset: 0x00185BDC
		public TypeSig Type
		{
			get
			{
				return this.typeSig;
			}
			set
			{
				this.typeSig = value;
				if (this.parameterList != null)
				{
					this.parameterList.TypeUpdated(this);
				}
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x00185BFC File Offset: 0x00185BFC
		public MethodDef Method
		{
			get
			{
				ParameterList parameterList = this.parameterList;
				if (parameterList == null)
				{
					return null;
				}
				return parameterList.Method;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x00185C14 File Offset: 0x00185C14
		public ParamDef ParamDef
		{
			get
			{
				ParameterList parameterList = this.parameterList;
				if (parameterList == null)
				{
					return null;
				}
				return parameterList.FindParamDef(this);
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00185C2C File Offset: 0x00185C2C
		public bool HasParamDef
		{
			get
			{
				return this.ParamDef != null;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00185C3C File Offset: 0x00185C3C
		// (set) Token: 0x06004E32 RID: 20018 RVA: 0x00185C6C File Offset: 0x00185C6C
		public string Name
		{
			get
			{
				ParamDef paramDef = this.ParamDef;
				if (paramDef != null)
				{
					return UTF8String.ToSystemStringOrEmpty(paramDef.Name);
				}
				return string.Empty;
			}
			set
			{
				ParamDef paramDef = this.ParamDef;
				if (paramDef != null)
				{
					paramDef.Name = value;
				}
			}
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00185C98 File Offset: 0x00185C98
		public Parameter(int paramIndex)
		{
			this.paramIndex = paramIndex;
			this.methodSigIndex = paramIndex;
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00185CB0 File Offset: 0x00185CB0
		public Parameter(int paramIndex, TypeSig type)
		{
			this.paramIndex = paramIndex;
			this.methodSigIndex = paramIndex;
			this.typeSig = type;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00185CD0 File Offset: 0x00185CD0
		public Parameter(int paramIndex, int methodSigIndex)
		{
			this.paramIndex = paramIndex;
			this.methodSigIndex = methodSigIndex;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00185CE8 File Offset: 0x00185CE8
		public Parameter(int paramIndex, int methodSigIndex, TypeSig type)
		{
			this.paramIndex = paramIndex;
			this.methodSigIndex = methodSigIndex;
			this.typeSig = type;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00185D08 File Offset: 0x00185D08
		internal Parameter(ParameterList parameterList, int paramIndex, int methodSigIndex)
		{
			this.parameterList = parameterList;
			this.paramIndex = paramIndex;
			this.methodSigIndex = methodSigIndex;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00185D28 File Offset: 0x00185D28
		public void CreateParamDef()
		{
			if (this.parameterList != null)
			{
				this.parameterList.CreateParamDef(this);
			}
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00185D44 File Offset: 0x00185D44
		public override string ToString()
		{
			string name = this.Name;
			if (!string.IsNullOrEmpty(name))
			{
				return name;
			}
			if (this.IsReturnTypeParameter)
			{
				return "RET_PARAM";
			}
			return string.Format("A_{0}", this.paramIndex);
		}

		// Token: 0x04002690 RID: 9872
		private readonly ParameterList parameterList;

		// Token: 0x04002691 RID: 9873
		private TypeSig typeSig;

		// Token: 0x04002692 RID: 9874
		private readonly int paramIndex;

		// Token: 0x04002693 RID: 9875
		private readonly int methodSigIndex;

		// Token: 0x04002694 RID: 9876
		public const int HIDDEN_THIS_METHOD_SIG_INDEX = -2;

		// Token: 0x04002695 RID: 9877
		public const int RETURN_TYPE_METHOD_SIG_INDEX = -1;
	}
}
