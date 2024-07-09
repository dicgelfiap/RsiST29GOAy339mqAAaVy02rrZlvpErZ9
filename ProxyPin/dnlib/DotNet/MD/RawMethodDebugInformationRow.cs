using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C9 RID: 2505
	[ComVisible(true)]
	public readonly struct RawMethodDebugInformationRow
	{
		// Token: 0x06005FAB RID: 24491 RVA: 0x001C99DC File Offset: 0x001C99DC
		public RawMethodDebugInformationRow(uint Document, uint SequencePoints)
		{
			this.Document = Document;
			this.SequencePoints = SequencePoints;
		}

		// Token: 0x1700140C RID: 5132
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index != 0)
				{
					if (index != 1)
					{
						result = 0U;
					}
					else
					{
						result = this.SequencePoints;
					}
				}
				else
				{
					result = this.Document;
				}
				return result;
			}
		}

		// Token: 0x04002EDF RID: 11999
		public readonly uint Document;

		// Token: 0x04002EE0 RID: 12000
		public readonly uint SequencePoints;
	}
}
