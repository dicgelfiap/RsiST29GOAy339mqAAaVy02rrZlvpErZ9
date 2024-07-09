using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C8 RID: 2504
	[ComVisible(true)]
	public readonly struct RawDocumentRow
	{
		// Token: 0x06005FA9 RID: 24489 RVA: 0x001C995C File Offset: 0x001C995C
		public RawDocumentRow(uint Name, uint HashAlgorithm, uint Hash, uint Language)
		{
			this.Name = Name;
			this.HashAlgorithm = HashAlgorithm;
			this.Hash = Hash;
			this.Language = Language;
		}

		// Token: 0x1700140B RID: 5131
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Name;
					break;
				case 1:
					result = this.HashAlgorithm;
					break;
				case 2:
					result = this.Hash;
					break;
				case 3:
					result = this.Language;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EDB RID: 11995
		public readonly uint Name;

		// Token: 0x04002EDC RID: 11996
		public readonly uint HashAlgorithm;

		// Token: 0x04002EDD RID: 11997
		public readonly uint Hash;

		// Token: 0x04002EDE RID: 11998
		public readonly uint Language;
	}
}
