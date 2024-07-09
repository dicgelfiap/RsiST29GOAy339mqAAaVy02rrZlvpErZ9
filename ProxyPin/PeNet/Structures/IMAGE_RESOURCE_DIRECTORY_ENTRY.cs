using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB1 RID: 2993
	[ComVisible(true)]
	public class IMAGE_RESOURCE_DIRECTORY_ENTRY : AbstractStructure
	{
		// Token: 0x060078E5 RID: 30949 RVA: 0x0023DD20 File Offset: 0x0023DD20
		public IMAGE_RESOURCE_DIRECTORY_ENTRY(byte[] buff, uint offset, uint resourceDirOffset) : base(buff, offset)
		{
			try
			{
				if (this.IsIdEntry)
				{
					this.ResolvedName = FlagResolver.ResolveResourceId(this.ID);
				}
				else if (this.IsNamedEntry)
				{
					uint offset2 = resourceDirOffset + (this.Name & 2147483647U);
					IMAGE_RESOURCE_DIR_STRING_U image_RESOURCE_DIR_STRING_U = new IMAGE_RESOURCE_DIR_STRING_U(this.Buff, offset2);
					this.ResolvedName = image_RESOURCE_DIR_STRING_U.NameString;
				}
			}
			catch (Exception)
			{
				this.ResolvedName = null;
			}
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x0023DDAC File Offset: 0x0023DDAC
		// (set) Token: 0x060078E7 RID: 30951 RVA: 0x0023DDB4 File Offset: 0x0023DDB4
		public IMAGE_RESOURCE_DIRECTORY ResourceDirectory { get; internal set; }

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x060078E8 RID: 30952 RVA: 0x0023DDC0 File Offset: 0x0023DDC0
		// (set) Token: 0x060078E9 RID: 30953 RVA: 0x0023DDC8 File Offset: 0x0023DDC8
		public IMAGE_RESOURCE_DATA_ENTRY ResourceDataEntry { get; internal set; }

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x0023DDD4 File Offset: 0x0023DDD4
		// (set) Token: 0x060078EB RID: 30955 RVA: 0x0023DDE8 File Offset: 0x0023DDE8
		public uint Name
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset, value);
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x0023DDFC File Offset: 0x0023DDFC
		// (set) Token: 0x060078ED RID: 30957 RVA: 0x0023DE04 File Offset: 0x0023DE04
		public string ResolvedName { get; private set; }

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x060078EE RID: 30958 RVA: 0x0023DE10 File Offset: 0x0023DE10
		// (set) Token: 0x060078EF RID: 30959 RVA: 0x0023DE20 File Offset: 0x0023DE20
		public uint ID
		{
			get
			{
				return this.Name & 65535U;
			}
			set
			{
				this.Name = (value & 65535U);
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x060078F0 RID: 30960 RVA: 0x0023DE30 File Offset: 0x0023DE30
		// (set) Token: 0x060078F1 RID: 30961 RVA: 0x0023DE48 File Offset: 0x0023DE48
		public uint OffsetToData
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 4U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 4U, value);
			}
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x0023DE60 File Offset: 0x0023DE60
		public uint OffsetToDirectory
		{
			get
			{
				return this.OffsetToData & 2147483647U;
			}
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x060078F3 RID: 30963 RVA: 0x0023DE70 File Offset: 0x0023DE70
		public bool DataIsDirectory
		{
			get
			{
				return (this.OffsetToData & 2147483648U) == 2147483648U;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x060078F4 RID: 30964 RVA: 0x0023DE8C File Offset: 0x0023DE8C
		public bool IsNamedEntry
		{
			get
			{
				return (this.Name & 2147483648U) == 2147483648U;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x060078F5 RID: 30965 RVA: 0x0023DEA8 File Offset: 0x0023DEA8
		public bool IsIdEntry
		{
			get
			{
				return !this.IsNamedEntry;
			}
		}
	}
}
