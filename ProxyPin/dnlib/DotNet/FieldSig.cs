using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000784 RID: 1924
	[ComVisible(true)]
	public sealed class FieldSig : CallingConventionSig
	{
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x0016BF9C File Offset: 0x0016BF9C
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x0016BFA4 File Offset: 0x0016BFA4
		public TypeSig Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x0016BFB0 File Offset: 0x0016BFB0
		public FieldSig()
		{
			this.callingConvention = CallingConvention.Field;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0016BFC0 File Offset: 0x0016BFC0
		public FieldSig(TypeSig type)
		{
			this.callingConvention = CallingConvention.Field;
			this.type = type;
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x0016BFD8 File Offset: 0x0016BFD8
		internal FieldSig(CallingConvention callingConvention, TypeSig type)
		{
			this.callingConvention = callingConvention;
			this.type = type;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x0016BFF0 File Offset: 0x0016BFF0
		public FieldSig Clone()
		{
			return new FieldSig(this.callingConvention, this.type);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x0016C004 File Offset: 0x0016C004
		public override string ToString()
		{
			return FullNameFactory.FullName(this.type, false, null, null, null, null);
		}

		// Token: 0x04002418 RID: 9240
		private TypeSig type;
	}
}
