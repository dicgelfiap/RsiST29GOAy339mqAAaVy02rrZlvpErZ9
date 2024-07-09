using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C3 RID: 2499
	[ComVisible(true)]
	public readonly struct RawManifestResourceRow
	{
		// Token: 0x06005F9E RID: 24478 RVA: 0x001C972C File Offset: 0x001C972C
		public RawManifestResourceRow(uint Offset, uint Flags, uint Name, uint Implementation)
		{
			this.Offset = Offset;
			this.Flags = Flags;
			this.Name = Name;
			this.Implementation = Implementation;
		}

		// Token: 0x17001406 RID: 5126
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Offset;
					break;
				case 1:
					result = this.Flags;
					break;
				case 2:
					result = this.Name;
					break;
				case 3:
					result = this.Implementation;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002ECC RID: 11980
		public readonly uint Offset;

		// Token: 0x04002ECD RID: 11981
		public readonly uint Flags;

		// Token: 0x04002ECE RID: 11982
		public readonly uint Name;

		// Token: 0x04002ECF RID: 11983
		public readonly uint Implementation;
	}
}
