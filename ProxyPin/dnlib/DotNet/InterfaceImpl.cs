using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007ED RID: 2029
	[DebuggerDisplay("{Interface}")]
	[ComVisible(true)]
	public abstract class InterfaceImpl : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IContainsGenericParameter, IHasCustomDebugInformation
	{
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x00179948 File Offset: 0x00179948
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.InterfaceImpl, this.rid);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x00179958 File Offset: 0x00179958
		// (set) Token: 0x0600494D RID: 18765 RVA: 0x00179960 File Offset: 0x00179960
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

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600494E RID: 18766 RVA: 0x0017996C File Offset: 0x0017996C
		public int HasCustomAttributeTag
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x00179970 File Offset: 0x00179970
		// (set) Token: 0x06004950 RID: 18768 RVA: 0x00179978 File Offset: 0x00179978
		public ITypeDefOrRef Interface
		{
			get
			{
				return this.@interface;
			}
			set
			{
				this.@interface = value;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x00179984 File Offset: 0x00179984
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

		// Token: 0x06004952 RID: 18770 RVA: 0x001799A0 File Offset: 0x001799A0
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x001799B4 File Offset: 0x001799B4
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x001799C4 File Offset: 0x001799C4
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x001799C8 File Offset: 0x001799C8
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x001799D8 File Offset: 0x001799D8
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

		// Token: 0x06004957 RID: 18775 RVA: 0x001799F4 File Offset: 0x001799F4
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x00179A08 File Offset: 0x00179A08
		bool IContainsGenericParameter.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x0400252B RID: 9515
		protected uint rid;

		// Token: 0x0400252C RID: 9516
		protected ITypeDefOrRef @interface;

		// Token: 0x0400252D RID: 9517
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400252E RID: 9518
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
