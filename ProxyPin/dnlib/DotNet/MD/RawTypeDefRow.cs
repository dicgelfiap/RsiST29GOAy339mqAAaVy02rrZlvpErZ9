using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099D RID: 2461
	[ComVisible(true)]
	public readonly struct RawTypeDefRow
	{
		// Token: 0x06005F52 RID: 24402 RVA: 0x001C883C File Offset: 0x001C883C
		public RawTypeDefRow(uint Flags, uint Name, uint Namespace, uint Extends, uint FieldList, uint MethodList)
		{
			this.Flags = Flags;
			this.Name = Name;
			this.Namespace = Namespace;
			this.Extends = Extends;
			this.FieldList = FieldList;
			this.MethodList = MethodList;
		}

		// Token: 0x170013E0 RID: 5088
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
					result = this.Name;
					break;
				case 2:
					result = this.Namespace;
					break;
				case 3:
					result = this.Extends;
					break;
				case 4:
					result = this.FieldList;
					break;
				case 5:
					result = this.MethodList;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E5F RID: 11871
		public readonly uint Flags;

		// Token: 0x04002E60 RID: 11872
		public readonly uint Name;

		// Token: 0x04002E61 RID: 11873
		public readonly uint Namespace;

		// Token: 0x04002E62 RID: 11874
		public readonly uint Extends;

		// Token: 0x04002E63 RID: 11875
		public readonly uint FieldList;

		// Token: 0x04002E64 RID: 11876
		public readonly uint MethodList;
	}
}
