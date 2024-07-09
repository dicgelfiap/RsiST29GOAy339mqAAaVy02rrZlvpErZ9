using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000786 RID: 1926
	[ComVisible(true)]
	public sealed class MethodSig : MethodBaseSig
	{
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x0016C078 File Offset: 0x0016C078
		// (set) Token: 0x060044BA RID: 17594 RVA: 0x0016C080 File Offset: 0x0016C080
		public uint OriginalToken
		{
			get
			{
				return this.origToken;
			}
			set
			{
				this.origToken = value;
			}
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0016C08C File Offset: 0x0016C08C
		public static MethodSig CreateStatic(TypeSig retType)
		{
			return new MethodSig(CallingConvention.Default, 0U, retType);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0016C098 File Offset: 0x0016C098
		public static MethodSig CreateStatic(TypeSig retType, TypeSig argType1)
		{
			return new MethodSig(CallingConvention.Default, 0U, retType, argType1);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0016C0A4 File Offset: 0x0016C0A4
		public static MethodSig CreateStatic(TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new MethodSig(CallingConvention.Default, 0U, retType, argType1, argType2);
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0016C0B0 File Offset: 0x0016C0B0
		public static MethodSig CreateStatic(TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new MethodSig(CallingConvention.Default, 0U, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x0016C0C0 File Offset: 0x0016C0C0
		public static MethodSig CreateStatic(TypeSig retType, params TypeSig[] argTypes)
		{
			return new MethodSig(CallingConvention.Default, 0U, retType, argTypes);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x0016C0CC File Offset: 0x0016C0CC
		public static MethodSig CreateInstance(TypeSig retType)
		{
			return new MethodSig(CallingConvention.HasThis, 0U, retType);
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0016C0D8 File Offset: 0x0016C0D8
		public static MethodSig CreateInstance(TypeSig retType, TypeSig argType1)
		{
			return new MethodSig(CallingConvention.HasThis, 0U, retType, argType1);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x0016C0E4 File Offset: 0x0016C0E4
		public static MethodSig CreateInstance(TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new MethodSig(CallingConvention.HasThis, 0U, retType, argType1, argType2);
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x0016C0F4 File Offset: 0x0016C0F4
		public static MethodSig CreateInstance(TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new MethodSig(CallingConvention.HasThis, 0U, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x0016C104 File Offset: 0x0016C104
		public static MethodSig CreateInstance(TypeSig retType, params TypeSig[] argTypes)
		{
			return new MethodSig(CallingConvention.HasThis, 0U, retType, argTypes);
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x0016C110 File Offset: 0x0016C110
		public static MethodSig CreateStaticGeneric(uint genParamCount, TypeSig retType)
		{
			return new MethodSig(CallingConvention.Generic, genParamCount, retType);
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x0016C11C File Offset: 0x0016C11C
		public static MethodSig CreateStaticGeneric(uint genParamCount, TypeSig retType, TypeSig argType1)
		{
			return new MethodSig(CallingConvention.Generic, genParamCount, retType, argType1);
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x0016C128 File Offset: 0x0016C128
		public static MethodSig CreateStaticGeneric(uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new MethodSig(CallingConvention.Generic, genParamCount, retType, argType1, argType2);
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x0016C138 File Offset: 0x0016C138
		public static MethodSig CreateStaticGeneric(uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new MethodSig(CallingConvention.Generic, genParamCount, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x0016C148 File Offset: 0x0016C148
		public static MethodSig CreateStaticGeneric(uint genParamCount, TypeSig retType, params TypeSig[] argTypes)
		{
			return new MethodSig(CallingConvention.Generic, genParamCount, retType, argTypes);
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x0016C154 File Offset: 0x0016C154
		public static MethodSig CreateInstanceGeneric(uint genParamCount, TypeSig retType)
		{
			return new MethodSig(CallingConvention.Generic | CallingConvention.HasThis, genParamCount, retType);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0016C160 File Offset: 0x0016C160
		public static MethodSig CreateInstanceGeneric(uint genParamCount, TypeSig retType, TypeSig argType1)
		{
			return new MethodSig(CallingConvention.Generic | CallingConvention.HasThis, genParamCount, retType, argType1);
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x0016C16C File Offset: 0x0016C16C
		public static MethodSig CreateInstanceGeneric(uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			return new MethodSig(CallingConvention.Generic | CallingConvention.HasThis, genParamCount, retType, argType1, argType2);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0016C17C File Offset: 0x0016C17C
		public static MethodSig CreateInstanceGeneric(uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			return new MethodSig(CallingConvention.Generic | CallingConvention.HasThis, genParamCount, retType, argType1, argType2, argType3);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0016C18C File Offset: 0x0016C18C
		public static MethodSig CreateInstanceGeneric(uint genParamCount, TypeSig retType, params TypeSig[] argTypes)
		{
			return new MethodSig(CallingConvention.Generic | CallingConvention.HasThis, genParamCount, retType, argTypes);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0016C198 File Offset: 0x0016C198
		public MethodSig()
		{
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x0016C1AC File Offset: 0x0016C1AC
		public MethodSig(CallingConvention callingConvention)
		{
			this.callingConvention = callingConvention;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0016C1C8 File Offset: 0x0016C1C8
		public MethodSig(CallingConvention callingConvention, uint genParamCount)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0016C1EC File Offset: 0x0016C1EC
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>();
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0016C214 File Offset: 0x0016C214
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, TypeSig argType1)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1
			};
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x0016C244 File Offset: 0x0016C244
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1,
				argType2
			};
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x0016C28C File Offset: 0x0016C28C
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, TypeSig argType1, TypeSig argType2, TypeSig argType3)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>
			{
				argType1,
				argType2,
				argType3
			};
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x0016C2DC File Offset: 0x0016C2DC
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, params TypeSig[] argTypes)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>(argTypes);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x0016C308 File Offset: 0x0016C308
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, IList<TypeSig> argTypes)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>(argTypes);
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x0016C334 File Offset: 0x0016C334
		public MethodSig(CallingConvention callingConvention, uint genParamCount, TypeSig retType, IList<TypeSig> argTypes, IList<TypeSig> paramsAfterSentinel)
		{
			this.callingConvention = callingConvention;
			this.genParamCount = genParamCount;
			this.retType = retType;
			this.parameters = new List<TypeSig>(argTypes);
			this.paramsAfterSentinel = ((paramsAfterSentinel == null) ? null : new List<TypeSig>(paramsAfterSentinel));
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0016C388 File Offset: 0x0016C388
		public MethodSig Clone()
		{
			return new MethodSig(this.callingConvention, this.genParamCount, this.retType, this.parameters, this.paramsAfterSentinel);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0016C3B0 File Offset: 0x0016C3B0
		public override string ToString()
		{
			return FullNameFactory.MethodBaseSigFullName(this, null);
		}

		// Token: 0x0400241D RID: 9245
		private uint origToken;
	}
}
