using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B4 RID: 2484
	[ComVisible(true)]
	public readonly struct RawMethodImplRow
	{
		// Token: 0x06005F80 RID: 24448 RVA: 0x001C908C File Offset: 0x001C908C
		public RawMethodImplRow(uint Class, uint MethodBody, uint MethodDeclaration)
		{
			this.Class = Class;
			this.MethodBody = MethodBody;
			this.MethodDeclaration = MethodDeclaration;
		}

		// Token: 0x170013F7 RID: 5111
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Class;
					break;
				case 1:
					result = this.MethodBody;
					break;
				case 2:
					result = this.MethodDeclaration;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E9A RID: 11930
		public readonly uint Class;

		// Token: 0x04002E9B RID: 11931
		public readonly uint MethodBody;

		// Token: 0x04002E9C RID: 11932
		public readonly uint MethodDeclaration;
	}
}
