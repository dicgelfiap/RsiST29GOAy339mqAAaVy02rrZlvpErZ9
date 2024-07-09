using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CA RID: 2506
	[ComVisible(true)]
	public readonly struct RawLocalScopeRow
	{
		// Token: 0x06005FAD RID: 24493 RVA: 0x001C9A2C File Offset: 0x001C9A2C
		public RawLocalScopeRow(uint Method, uint ImportScope, uint VariableList, uint ConstantList, uint StartOffset, uint Length)
		{
			this.Method = Method;
			this.ImportScope = ImportScope;
			this.VariableList = VariableList;
			this.ConstantList = ConstantList;
			this.StartOffset = StartOffset;
			this.Length = Length;
		}

		// Token: 0x1700140D RID: 5133
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Method;
					break;
				case 1:
					result = this.ImportScope;
					break;
				case 2:
					result = this.VariableList;
					break;
				case 3:
					result = this.ConstantList;
					break;
				case 4:
					result = this.StartOffset;
					break;
				case 5:
					result = this.Length;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EE1 RID: 12001
		public readonly uint Method;

		// Token: 0x04002EE2 RID: 12002
		public readonly uint ImportScope;

		// Token: 0x04002EE3 RID: 12003
		public readonly uint VariableList;

		// Token: 0x04002EE4 RID: 12004
		public readonly uint ConstantList;

		// Token: 0x04002EE5 RID: 12005
		public readonly uint StartOffset;

		// Token: 0x04002EE6 RID: 12006
		public readonly uint Length;
	}
}
