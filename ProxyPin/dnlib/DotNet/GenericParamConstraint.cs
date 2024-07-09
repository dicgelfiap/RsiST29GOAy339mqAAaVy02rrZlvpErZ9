using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007BB RID: 1979
	[ComVisible(true)]
	public abstract class GenericParamConstraint : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation, IContainsGenericParameter
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x00176EC4 File Offset: 0x00176EC4
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.GenericParamConstraint, this.rid);
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x00176ED4 File Offset: 0x00176ED4
		// (set) Token: 0x06004837 RID: 18487 RVA: 0x00176EDC File Offset: 0x00176EDC
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00176EE8 File Offset: 0x00176EE8
		public int HasCustomAttributeTag
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x00176EEC File Offset: 0x00176EEC
		// (set) Token: 0x0600483A RID: 18490 RVA: 0x00176EF4 File Offset: 0x00176EF4
		public GenericParam Owner
		{
			get
			{
				return this.owner;
			}
			internal set
			{
				this.owner = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x00176F00 File Offset: 0x00176F00
		// (set) Token: 0x0600483C RID: 18492 RVA: 0x00176F08 File Offset: 0x00176F08
		public ITypeDefOrRef Constraint
		{
			get
			{
				return this.constraint;
			}
			set
			{
				this.constraint = value;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x00176F14 File Offset: 0x00176F14
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00176F30 File Offset: 0x00176F30
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x00176F44 File Offset: 0x00176F44
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x00176F54 File Offset: 0x00176F54
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x00176F58 File Offset: 0x00176F58
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004842 RID: 18498 RVA: 0x00176F68 File Offset: 0x00176F68
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00176F84 File Offset: 0x00176F84
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004844 RID: 18500 RVA: 0x00176F98 File Offset: 0x00176F98
		bool IContainsGenericParameter.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x04002503 RID: 9475
		protected uint rid;

		// Token: 0x04002504 RID: 9476
		protected GenericParam owner;

		// Token: 0x04002505 RID: 9477
		protected ITypeDefOrRef constraint;

		// Token: 0x04002506 RID: 9478
		protected CustomAttributeCollection customAttributes;

		// Token: 0x04002507 RID: 9479
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
