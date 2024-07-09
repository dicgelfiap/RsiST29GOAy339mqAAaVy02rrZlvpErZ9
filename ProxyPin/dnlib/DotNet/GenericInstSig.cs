using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000872 RID: 2162
	[ComVisible(true)]
	public sealed class GenericInstSig : LeafSig
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060052C0 RID: 21184 RVA: 0x0019631C File Offset: 0x0019631C
		public override ElementType ElementType
		{
			get
			{
				return ElementType.GenericInst;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x00196320 File Offset: 0x00196320
		// (set) Token: 0x060052C2 RID: 21186 RVA: 0x00196328 File Offset: 0x00196328
		public ClassOrValueTypeSig GenericType
		{
			get
			{
				return this.genericType;
			}
			set
			{
				this.genericType = value;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x00196334 File Offset: 0x00196334
		public IList<TypeSig> GenericArguments
		{
			get
			{
				return this.genericArgs;
			}
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0019633C File Offset: 0x0019633C
		public GenericInstSig()
		{
			this.genericArgs = new List<TypeSig>();
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00196350 File Offset: 0x00196350
		public GenericInstSig(ClassOrValueTypeSig genericType)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>();
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0019636C File Offset: 0x0019636C
		public GenericInstSig(ClassOrValueTypeSig genericType, uint genArgCount)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>((int)genArgCount);
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00196388 File Offset: 0x00196388
		public GenericInstSig(ClassOrValueTypeSig genericType, int genArgCount) : this(genericType, (uint)genArgCount)
		{
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00196394 File Offset: 0x00196394
		public GenericInstSig(ClassOrValueTypeSig genericType, TypeSig genArg1)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>
			{
				genArg1
			};
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x001963B8 File Offset: 0x001963B8
		public GenericInstSig(ClassOrValueTypeSig genericType, TypeSig genArg1, TypeSig genArg2)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>
			{
				genArg1,
				genArg2
			};
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x001963F0 File Offset: 0x001963F0
		public GenericInstSig(ClassOrValueTypeSig genericType, TypeSig genArg1, TypeSig genArg2, TypeSig genArg3)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>
			{
				genArg1,
				genArg2,
				genArg3
			};
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00196430 File Offset: 0x00196430
		public GenericInstSig(ClassOrValueTypeSig genericType, params TypeSig[] genArgs)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>(genArgs);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x0019644C File Offset: 0x0019644C
		public GenericInstSig(ClassOrValueTypeSig genericType, IList<TypeSig> genArgs)
		{
			this.genericType = genericType;
			this.genericArgs = new List<TypeSig>(genArgs);
		}

		// Token: 0x040027D1 RID: 10193
		private ClassOrValueTypeSig genericType;

		// Token: 0x040027D2 RID: 10194
		private readonly IList<TypeSig> genericArgs;
	}
}
