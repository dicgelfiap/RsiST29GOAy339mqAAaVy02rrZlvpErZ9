using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099C RID: 2460
	[ComVisible(true)]
	public readonly struct RawTypeRefRow
	{
		// Token: 0x06005F50 RID: 24400 RVA: 0x001C87D4 File Offset: 0x001C87D4
		public RawTypeRefRow(uint ResolutionScope, uint Name, uint Namespace)
		{
			this.ResolutionScope = ResolutionScope;
			this.Name = Name;
			this.Namespace = Namespace;
		}

		// Token: 0x170013DF RID: 5087
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.ResolutionScope;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.Namespace;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E5C RID: 11868
		public readonly uint ResolutionScope;

		// Token: 0x04002E5D RID: 11869
		public readonly uint Name;

		// Token: 0x04002E5E RID: 11870
		public readonly uint Namespace;
	}
}
