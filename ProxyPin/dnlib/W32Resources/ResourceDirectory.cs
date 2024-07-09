using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.W32Resources
{
	// Token: 0x02000731 RID: 1841
	[ComVisible(true)]
	public abstract class ResourceDirectory : ResourceDirectoryEntry
	{
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x00161500 File Offset: 0x00161500
		// (set) Token: 0x0600409F RID: 16543 RVA: 0x00161508 File Offset: 0x00161508
		public uint Characteristics
		{
			get
			{
				return this.characteristics;
			}
			set
			{
				this.characteristics = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x00161514 File Offset: 0x00161514
		// (set) Token: 0x060040A1 RID: 16545 RVA: 0x0016151C File Offset: 0x0016151C
		public uint TimeDateStamp
		{
			get
			{
				return this.timeDateStamp;
			}
			set
			{
				this.timeDateStamp = value;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x00161528 File Offset: 0x00161528
		// (set) Token: 0x060040A3 RID: 16547 RVA: 0x00161530 File Offset: 0x00161530
		public ushort MajorVersion
		{
			get
			{
				return this.majorVersion;
			}
			set
			{
				this.majorVersion = value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x0016153C File Offset: 0x0016153C
		// (set) Token: 0x060040A5 RID: 16549 RVA: 0x00161544 File Offset: 0x00161544
		public ushort MinorVersion
		{
			get
			{
				return this.minorVersion;
			}
			set
			{
				this.minorVersion = value;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x00161550 File Offset: 0x00161550
		public IList<ResourceDirectory> Directories
		{
			get
			{
				return this.directories;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x00161558 File Offset: 0x00161558
		public IList<ResourceData> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x00161560 File Offset: 0x00161560
		protected ResourceDirectory(ResourceName name) : base(name)
		{
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0016156C File Offset: 0x0016156C
		public ResourceDirectory FindDirectory(ResourceName name)
		{
			foreach (ResourceDirectory resourceDirectory in this.directories)
			{
				if (resourceDirectory.Name == name)
				{
					return resourceDirectory;
				}
			}
			return null;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x001615DC File Offset: 0x001615DC
		public ResourceData FindData(ResourceName name)
		{
			foreach (ResourceData resourceData in this.data)
			{
				if (resourceData.Name == name)
				{
					return resourceData;
				}
			}
			return null;
		}

		// Token: 0x0400226E RID: 8814
		protected uint characteristics;

		// Token: 0x0400226F RID: 8815
		protected uint timeDateStamp;

		// Token: 0x04002270 RID: 8816
		protected ushort majorVersion;

		// Token: 0x04002271 RID: 8817
		protected ushort minorVersion;

		// Token: 0x04002272 RID: 8818
		private protected LazyList<ResourceDirectory> directories;

		// Token: 0x04002273 RID: 8819
		private protected LazyList<ResourceData> data;
	}
}
