using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000801 RID: 2049
	[ComVisible(true)]
	public sealed class SafeArrayMarshalType : MarshalType
	{
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x0017A304 File Offset: 0x0017A304
		// (set) Token: 0x060049AF RID: 18863 RVA: 0x0017A30C File Offset: 0x0017A30C
		public VariantType VariantType
		{
			get
			{
				return this.vt;
			}
			set
			{
				this.vt = value;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x0017A318 File Offset: 0x0017A318
		// (set) Token: 0x060049B1 RID: 18865 RVA: 0x0017A320 File Offset: 0x0017A320
		public ITypeDefOrRef UserDefinedSubType
		{
			get
			{
				return this.userDefinedSubType;
			}
			set
			{
				this.userDefinedSubType = value;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x0017A32C File Offset: 0x0017A32C
		public bool IsVariantTypeValid
		{
			get
			{
				return this.vt != (VariantType)4294967295U;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0017A33C File Offset: 0x0017A33C
		public bool IsUserDefinedSubTypeValid
		{
			get
			{
				return this.userDefinedSubType != null;
			}
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0017A34C File Offset: 0x0017A34C
		public SafeArrayMarshalType() : this((VariantType)4294967295U, null)
		{
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0017A358 File Offset: 0x0017A358
		public SafeArrayMarshalType(VariantType vt) : this(vt, null)
		{
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0017A364 File Offset: 0x0017A364
		public SafeArrayMarshalType(ITypeDefOrRef userDefinedSubType) : this((VariantType)4294967295U, userDefinedSubType)
		{
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0017A370 File Offset: 0x0017A370
		public SafeArrayMarshalType(VariantType vt, ITypeDefOrRef userDefinedSubType) : base(NativeType.SafeArray)
		{
			this.vt = vt;
			this.userDefinedSubType = userDefinedSubType;
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0017A388 File Offset: 0x0017A388
		public override string ToString()
		{
			ITypeDefOrRef typeDefOrRef = this.userDefinedSubType;
			if (typeDefOrRef != null)
			{
				return string.Format("{0} ({1}, {2})", this.nativeType, this.vt, typeDefOrRef);
			}
			return string.Format("{0} ({1})", this.nativeType, this.vt);
		}

		// Token: 0x04002545 RID: 9541
		private VariantType vt;

		// Token: 0x04002546 RID: 9542
		private ITypeDefOrRef userDefinedSubType;
	}
}
