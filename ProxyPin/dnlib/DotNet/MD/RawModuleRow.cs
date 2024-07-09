using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099B RID: 2459
	[ComVisible(true)]
	public readonly struct RawModuleRow
	{
		// Token: 0x06005F4E RID: 24398 RVA: 0x001C873C File Offset: 0x001C873C
		public RawModuleRow(ushort Generation, uint Name, uint Mvid, uint EncId, uint EncBaseId)
		{
			this.Generation = Generation;
			this.Name = Name;
			this.Mvid = Mvid;
			this.EncId = EncId;
			this.EncBaseId = EncBaseId;
		}

		// Token: 0x170013DE RID: 5086
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Generation;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.Mvid;
					break;
				case 3:
					result = this.EncId;
					break;
				case 4:
					result = this.EncBaseId;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E57 RID: 11863
		public readonly ushort Generation;

		// Token: 0x04002E58 RID: 11864
		public readonly uint Name;

		// Token: 0x04002E59 RID: 11865
		public readonly uint Mvid;

		// Token: 0x04002E5A RID: 11866
		public readonly uint EncId;

		// Token: 0x04002E5B RID: 11867
		public readonly uint EncBaseId;
	}
}
