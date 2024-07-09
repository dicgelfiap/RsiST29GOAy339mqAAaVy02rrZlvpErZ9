using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000787 RID: 1927
	[ComVisible(true)]
	public sealed class PropertySig : MethodBaseSig
	{
		// Token: 0x060044DB RID: 17627 RVA: 0x0016C3BC File Offset: 0x0016C3BC
		public static PropertySig CreateStatic(TypeSig retType)
		{
			return new PropertySig(false, retType);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x0016C3C8 File Offset: 0x0016C3C8
		public static PropertySig CreateStatic(TypeSig retType, TypeSig argType1)
		{
			return new PropertySig(false, retType, argType1);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0016C3D4 File Offset: 0x0016C3D4
		public static PropertySig CreateStatic(TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new PropertySig(false, retType, argType1, argType2);
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0016C3E0 File Offset: 0x0016C3E0
		public static PropertySig CreateStatic(TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new PropertySig(false, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0016C3EC File Offset: 0x0016C3EC
		public static PropertySig CreateStatic(TypeSig retType, params TypeSig[] argTypes)
		{
			return new PropertySig(false, retType, argTypes);
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0016C3F8 File Offset: 0x0016C3F8
		public static PropertySig CreateInstance(TypeSig retType)
		{
			return new PropertySig(true, retType);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0016C404 File Offset: 0x0016C404
		public static PropertySig CreateInstance(TypeSig retType, TypeSig argType1)
		{
			return new PropertySig(true, retType, argType1);
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0016C410 File Offset: 0x0016C410
		public static PropertySig CreateInstance(TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new PropertySig(true, retType, argType1, argType2);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0016C41C File Offset: 0x0016C41C
		public static PropertySig CreateInstance(TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new PropertySig(true, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x0016C428 File Offset: 0x0016C428
		public static PropertySig CreateInstance(TypeSig retType, params TypeSig[] argTypes)
		{
			return new PropertySig(true, retType, argTypes);
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x0016C434 File Offset: 0x0016C434
		public PropertySig()
		{
			this.callingConvention = CallingConvention.Property;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x0016C450 File Offset: 0x0016C450
		internal PropertySig(CallingConvention callingConvention)
		{
			this.callingConvention = callingConvention;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x0016C46C File Offset: 0x0016C46C
		public PropertySig(bool hasThis)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x0016C4A4 File Offset: 0x0016C4A4
		public PropertySig(bool hasThis, TypeSig retType)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.retType = retType;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x0016C4E4 File Offset: 0x0016C4E4
		public PropertySig(bool hasThis, TypeSig retType, TypeSig argType1)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1
			};
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x0016C52C File Offset: 0x0016C52C
		public PropertySig(bool hasThis, TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1,
				argType2
			};
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x0016C57C File Offset: 0x0016C57C
		public PropertySig(bool hasThis, TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1,
				argType2,
				argType3
			};
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x0016C5D4 File Offset: 0x0016C5D4
		public PropertySig(bool hasThis, TypeSig retType, params TypeSig[] argTypes)
		{
			this.callingConvention = (CallingConvention.Property | (hasThis ? CallingConvention.HasThis : CallingConvention.Default));
			this.retType = retType;
			this.parameters = new List<TypeSig>(argTypes);
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x0016C614 File Offset: 0x0016C614
		internal PropertySig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, IList<TypeSig> argTypes, IList<TypeSig> paramsAfterSentinel)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>(argTypes);
			this.paramsAfterSentinel = ((paramsAfterSentinel == null) ? null : new List<TypeSig>(paramsAfterSentinel));
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0016C668 File Offset: 0x0016C668
		public PropertySig Clone()
		{
			return new PropertySig(this.callingConvention, this.genParamCount, this.retType, this.parameters, this.paramsAfterSentinel);
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0016C690 File Offset: 0x0016C690
		public override string ToString()
		{
			return FullNameFactory.MethodBaseSigFullName(this, null);
		}
	}
}
