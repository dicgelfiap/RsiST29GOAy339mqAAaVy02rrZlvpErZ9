using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200084F RID: 2127
	[ComVisible(true)]
	public abstract class StandAloneSig : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation, IContainsGenericParameter
	{
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005052 RID: 20562 RVA: 0x0018F98C File Offset: 0x0018F98C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.StandAloneSig, this.rid);
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005053 RID: 20563 RVA: 0x0018F99C File Offset: 0x0018F99C
		// (set) Token: 0x06005054 RID: 20564 RVA: 0x0018F9A4 File Offset: 0x0018F9A4
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

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005055 RID: 20565 RVA: 0x0018F9B0 File Offset: 0x0018F9B0
		public int HasCustomAttributeTag
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x0018F9B4 File Offset: 0x0018F9B4
		// (set) Token: 0x06005057 RID: 20567 RVA: 0x0018F9BC File Offset: 0x0018F9BC
		public CallingConventionSig Signature
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x0018F9C8 File Offset: 0x0018F9C8
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

		// Token: 0x06005059 RID: 20569 RVA: 0x0018F9E4 File Offset: 0x0018F9E4
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x0018F9F8 File Offset: 0x0018F9F8
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x0018FA08 File Offset: 0x0018FA08
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x0018FA0C File Offset: 0x0018FA0C
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x0018FA1C File Offset: 0x0018FA1C
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

		// Token: 0x0600505E RID: 20574 RVA: 0x0018FA38 File Offset: 0x0018FA38
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x0018FA4C File Offset: 0x0018FA4C
		// (set) Token: 0x06005060 RID: 20576 RVA: 0x0018FA5C File Offset: 0x0018FA5C
		public MethodSig MethodSig
		{
			get
			{
				return this.signature as MethodSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x0018FA68 File Offset: 0x0018FA68
		// (set) Token: 0x06005062 RID: 20578 RVA: 0x0018FA78 File Offset: 0x0018FA78
		public LocalSig LocalSig
		{
			get
			{
				return this.signature as LocalSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x0018FA84 File Offset: 0x0018FA84
		public bool ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x04002750 RID: 10064
		protected uint rid;

		// Token: 0x04002751 RID: 10065
		protected CallingConventionSig signature;

		// Token: 0x04002752 RID: 10066
		protected CustomAttributeCollection customAttributes;

		// Token: 0x04002753 RID: 10067
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
