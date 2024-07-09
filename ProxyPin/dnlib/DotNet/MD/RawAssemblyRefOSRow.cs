using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C0 RID: 2496
	[ComVisible(true)]
	public readonly struct RawAssemblyRefOSRow
	{
		// Token: 0x06005F98 RID: 24472 RVA: 0x001C95AC File Offset: 0x001C95AC
		public RawAssemblyRefOSRow(uint OSPlatformId, uint OSMajorVersion, uint OSMinorVersion, uint AssemblyRef)
		{
			this.OSPlatformId = OSPlatformId;
			this.OSMajorVersion = OSMajorVersion;
			this.OSMinorVersion = OSMinorVersion;
			this.AssemblyRef = AssemblyRef;
		}

		// Token: 0x17001403 RID: 5123
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.OSPlatformId;
					break;
				case 1:
					result = this.OSMajorVersion;
					break;
				case 2:
					result = this.OSMinorVersion;
					break;
				case 3:
					result = this.AssemblyRef;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EC0 RID: 11968
		public readonly uint OSPlatformId;

		// Token: 0x04002EC1 RID: 11969
		public readonly uint OSMajorVersion;

		// Token: 0x04002EC2 RID: 11970
		public readonly uint OSMinorVersion;

		// Token: 0x04002EC3 RID: 11971
		public readonly uint AssemblyRef;
	}
}
