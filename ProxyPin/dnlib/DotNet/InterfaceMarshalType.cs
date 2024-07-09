using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000805 RID: 2053
	[ComVisible(true)]
	public sealed class InterfaceMarshalType : MarshalType
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x0017A718 File Offset: 0x0017A718
		// (set) Token: 0x060049E6 RID: 18918 RVA: 0x0017A720 File Offset: 0x0017A720
		public int IidParamIndex
		{
			get
			{
				return this.iidParamIndex;
			}
			set
			{
				this.iidParamIndex = value;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x0017A72C File Offset: 0x0017A72C
		public bool IsIidParamIndexValid
		{
			get
			{
				return this.iidParamIndex >= 0;
			}
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0017A73C File Offset: 0x0017A73C
		public InterfaceMarshalType(NativeType nativeType) : this(nativeType, -1)
		{
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0017A748 File Offset: 0x0017A748
		public InterfaceMarshalType(NativeType nativeType, int iidParamIndex) : base(nativeType)
		{
			if (nativeType != NativeType.IUnknown && nativeType != NativeType.IDispatch && nativeType != NativeType.IntF)
			{
				throw new ArgumentException("Invalid nativeType");
			}
			this.iidParamIndex = iidParamIndex;
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0017A77C File Offset: 0x0017A77C
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.nativeType, this.iidParamIndex);
		}

		// Token: 0x04002552 RID: 9554
		private int iidParamIndex;
	}
}
