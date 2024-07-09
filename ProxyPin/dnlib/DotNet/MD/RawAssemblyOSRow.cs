using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BD RID: 2493
	[ComVisible(true)]
	public readonly struct RawAssemblyOSRow
	{
		// Token: 0x06005F92 RID: 24466 RVA: 0x001C93EC File Offset: 0x001C93EC
		public RawAssemblyOSRow(uint OSPlatformId, uint OSMajorVersion, uint OSMinorVersion)
		{
			this.OSPlatformId = OSPlatformId;
			this.OSMajorVersion = OSMajorVersion;
			this.OSMinorVersion = OSMinorVersion;
		}

		// Token: 0x17001400 RID: 5120
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
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EB2 RID: 11954
		public readonly uint OSPlatformId;

		// Token: 0x04002EB3 RID: 11955
		public readonly uint OSMajorVersion;

		// Token: 0x04002EB4 RID: 11956
		public readonly uint OSMinorVersion;
	}
}
