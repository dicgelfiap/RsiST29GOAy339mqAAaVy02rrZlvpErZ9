using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C2 RID: 2498
	[ComVisible(true)]
	public readonly struct RawExportedTypeRow
	{
		// Token: 0x06005F9C RID: 24476 RVA: 0x001C9694 File Offset: 0x001C9694
		public RawExportedTypeRow(uint Flags, uint TypeDefId, uint TypeName, uint TypeNamespace, uint Implementation)
		{
			this.Flags = Flags;
			this.TypeDefId = TypeDefId;
			this.TypeName = TypeName;
			this.TypeNamespace = TypeNamespace;
			this.Implementation = Implementation;
		}

		// Token: 0x17001405 RID: 5125
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Flags;
					break;
				case 1:
					result = this.TypeDefId;
					break;
				case 2:
					result = this.TypeName;
					break;
				case 3:
					result = this.TypeNamespace;
					break;
				case 4:
					result = this.Implementation;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EC7 RID: 11975
		public readonly uint Flags;

		// Token: 0x04002EC8 RID: 11976
		public readonly uint TypeDefId;

		// Token: 0x04002EC9 RID: 11977
		public readonly uint TypeName;

		// Token: 0x04002ECA RID: 11978
		public readonly uint TypeNamespace;

		// Token: 0x04002ECB RID: 11979
		public readonly uint Implementation;
	}
}
