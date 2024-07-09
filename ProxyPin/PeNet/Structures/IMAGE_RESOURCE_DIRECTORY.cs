using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB0 RID: 2992
	[ComVisible(true)]
	public class IMAGE_RESOURCE_DIRECTORY : AbstractStructure
	{
		// Token: 0x060078D6 RID: 30934 RVA: 0x0023DB50 File Offset: 0x0023DB50
		public IMAGE_RESOURCE_DIRECTORY(byte[] buff, uint offset, uint resourceDirOffset) : base(buff, offset)
		{
			this.DirectoryEntries = this.ParseDirectoryEntries(resourceDirOffset);
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x060078D7 RID: 30935 RVA: 0x0023DB68 File Offset: 0x0023DB68
		// (set) Token: 0x060078D8 RID: 30936 RVA: 0x0023DB7C File Offset: 0x0023DB7C
		public uint Characteristics
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

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x060078D9 RID: 30937 RVA: 0x0023DB90 File Offset: 0x0023DB90
		// (set) Token: 0x060078DA RID: 30938 RVA: 0x0023DBA8 File Offset: 0x0023DBA8
		public uint TimeDateStamp
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

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x0023DBC0 File Offset: 0x0023DBC0
		// (set) Token: 0x060078DC RID: 30940 RVA: 0x0023DBD8 File Offset: 0x0023DBD8
		public ushort MajorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 8U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 8U), value);
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x0023DBF0 File Offset: 0x0023DBF0
		// (set) Token: 0x060078DE RID: 30942 RVA: 0x0023DC08 File Offset: 0x0023DC08
		public ushort MinorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 10U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 10U), value);
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x060078DF RID: 30943 RVA: 0x0023DC20 File Offset: 0x0023DC20
		// (set) Token: 0x060078E0 RID: 30944 RVA: 0x0023DC38 File Offset: 0x0023DC38
		public ushort NumberOfNameEntries
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 12U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 12U), value);
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x060078E1 RID: 30945 RVA: 0x0023DC50 File Offset: 0x0023DC50
		// (set) Token: 0x060078E2 RID: 30946 RVA: 0x0023DC68 File Offset: 0x0023DC68
		public ushort NumberOfIdEntries
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 14U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 14U), value);
			}
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x0023DC80 File Offset: 0x0023DC80
		private IMAGE_RESOURCE_DIRECTORY_ENTRY[] ParseDirectoryEntries(uint resourceDirOffset)
		{
			if (this.SanityCheckFailed())
			{
				return null;
			}
			IMAGE_RESOURCE_DIRECTORY_ENTRY[] array = new IMAGE_RESOURCE_DIRECTORY_ENTRY[(int)(this.NumberOfIdEntries + this.NumberOfNameEntries)];
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					array[i] = new IMAGE_RESOURCE_DIRECTORY_ENTRY(this.Buff, (uint)(i * 8 + (int)this.Offset + 16), resourceDirOffset);
				}
				catch (IndexOutOfRangeException)
				{
					array[i] = null;
				}
			}
			return array;
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x0023DD04 File Offset: 0x0023DD04
		private bool SanityCheckFailed()
		{
			return this.NumberOfIdEntries + this.NumberOfNameEntries >= 1000;
		}

		// Token: 0x04003A51 RID: 14929
		public readonly IMAGE_RESOURCE_DIRECTORY_ENTRY[] DirectoryEntries;
	}
}
