using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007AF RID: 1967
	[ComVisible(true)]
	public abstract class FileDef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IImplementation, IFullName, IHasCustomDebugInformation, IManagedEntryPoint
	{
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x00171400 File Offset: 0x00171400
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.File, this.rid);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x00171410 File Offset: 0x00171410
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x00171418 File Offset: 0x00171418
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

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x00171424 File Offset: 0x00171424
		public int HasCustomAttributeTag
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x00171428 File Offset: 0x00171428
		public int ImplementationTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0017142C File Offset: 0x0017142C
		// (set) Token: 0x06004726 RID: 18214 RVA: 0x00171434 File Offset: 0x00171434
		public FileAttributes Flags
		{
			get
			{
				return (FileAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x00171440 File Offset: 0x00171440
		// (set) Token: 0x06004728 RID: 18216 RVA: 0x00171448 File Offset: 0x00171448
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

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x00171454 File Offset: 0x00171454
		// (set) Token: 0x0600472A RID: 18218 RVA: 0x0017145C File Offset: 0x0017145C
		public byte[] HashValue
		{
			get
			{
				return this.hashValue;
			}
			set
			{
				this.hashValue = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x00171468 File Offset: 0x00171468
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

		// Token: 0x0600472C RID: 18220 RVA: 0x00171484 File Offset: 0x00171484
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600472D RID: 18221 RVA: 0x00171498 File Offset: 0x00171498
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x001714A8 File Offset: 0x001714A8
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600472F RID: 18223 RVA: 0x001714AC File Offset: 0x001714AC
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x001714BC File Offset: 0x001714BC
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

		// Token: 0x06004731 RID: 18225 RVA: 0x001714D8 File Offset: 0x001714D8
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x001714EC File Offset: 0x001714EC
		private void ModifyAttributes(bool set, FileAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x00171514 File Offset: 0x00171514
		// (set) Token: 0x06004734 RID: 18228 RVA: 0x00171524 File Offset: 0x00171524
		public bool ContainsMetadata
		{
			get
			{
				return (this.attributes & 1) == 0;
			}
			set
			{
				this.ModifyAttributes(!value, FileAttributes.ContainsNoMetadata);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x00171534 File Offset: 0x00171534
		// (set) Token: 0x06004736 RID: 18230 RVA: 0x00171544 File Offset: 0x00171544
		public bool ContainsNoMetadata
		{
			get
			{
				return (this.attributes & 1) != 0;
			}
			set
			{
				this.ModifyAttributes(value, FileAttributes.ContainsNoMetadata);
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x00171550 File Offset: 0x00171550
		public string FullName
		{
			get
			{
				return UTF8String.ToSystemStringOrEmpty(this.name);
			}
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00171560 File Offset: 0x00171560
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040024D5 RID: 9429
		protected uint rid;

		// Token: 0x040024D6 RID: 9430
		protected int attributes;

		// Token: 0x040024D7 RID: 9431
		protected UTF8String name;

		// Token: 0x040024D8 RID: 9432
		protected byte[] hashValue;

		// Token: 0x040024D9 RID: 9433
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040024DA RID: 9434
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
