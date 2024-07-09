using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007F9 RID: 2041
	[DebuggerDisplay("{Offset} {Name.String} {Implementation}")]
	[ComVisible(true)]
	public abstract class ManifestResource : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation
	{
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x00179B60 File Offset: 0x00179B60
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ManifestResource, this.rid);
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x00179B70 File Offset: 0x00179B70
		// (set) Token: 0x06004979 RID: 18809 RVA: 0x00179B78 File Offset: 0x00179B78
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

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x00179B84 File Offset: 0x00179B84
		public int HasCustomAttributeTag
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x00179B88 File Offset: 0x00179B88
		// (set) Token: 0x0600497C RID: 18812 RVA: 0x00179B90 File Offset: 0x00179B90
		public uint Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x00179B9C File Offset: 0x00179B9C
		// (set) Token: 0x0600497E RID: 18814 RVA: 0x00179BA4 File Offset: 0x00179BA4
		public ManifestResourceAttributes Flags
		{
			get
			{
				return (ManifestResourceAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x00179BB0 File Offset: 0x00179BB0
		// (set) Token: 0x06004980 RID: 18816 RVA: 0x00179BB8 File Offset: 0x00179BB8
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x00179BC4 File Offset: 0x00179BC4
		// (set) Token: 0x06004982 RID: 18818 RVA: 0x00179BCC File Offset: 0x00179BCC
		public IImplementation Implementation
		{
			get
			{
				return this.implementation;
			}
			set
			{
				this.implementation = value;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x00179BD8 File Offset: 0x00179BD8
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

		// Token: 0x06004984 RID: 18820 RVA: 0x00179BF4 File Offset: 0x00179BF4
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x00179C08 File Offset: 0x00179C08
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x00179C18 File Offset: 0x00179C18
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x00179C1C File Offset: 0x00179C1C
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x00179C2C File Offset: 0x00179C2C
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

		// Token: 0x06004989 RID: 18825 RVA: 0x00179C48 File Offset: 0x00179C48
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x00179C5C File Offset: 0x00179C5C
		private void ModifyAttributes(ManifestResourceAttributes andMask, ManifestResourceAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x00179C70 File Offset: 0x00179C70
		// (set) Token: 0x0600498C RID: 18828 RVA: 0x00179C7C File Offset: 0x00179C7C
		public ManifestResourceAttributes Visibility
		{
			get
			{
				return (ManifestResourceAttributes)(this.attributes & 7);
			}
			set
			{
				this.ModifyAttributes(~ManifestResourceAttributes.VisibilityMask, value & ManifestResourceAttributes.VisibilityMask);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x00179C8C File Offset: 0x00179C8C
		public bool IsPublic
		{
			get
			{
				return (this.attributes & 7) == 1;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x00179C9C File Offset: 0x00179C9C
		public bool IsPrivate
		{
			get
			{
				return (this.attributes & 7) == 2;
			}
		}

		// Token: 0x04002532 RID: 9522
		protected uint rid;

		// Token: 0x04002533 RID: 9523
		protected uint offset;

		// Token: 0x04002534 RID: 9524
		protected int attributes;

		// Token: 0x04002535 RID: 9525
		protected UTF8String name;

		// Token: 0x04002536 RID: 9526
		protected IImplementation implementation;

		// Token: 0x04002537 RID: 9527
		protected CustomAttributeCollection customAttributes;

		// Token: 0x04002538 RID: 9528
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
