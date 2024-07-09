using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008EE RID: 2286
	[ComVisible(true)]
	public sealed class BinaryResourceData : UserResourceData
	{
		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x001B5564 File Offset: 0x001B5564
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x001B556C File Offset: 0x001B556C
		public BinaryResourceData(UserResourceType type, byte[] data) : base(type)
		{
			this.data = data;
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x001B557C File Offset: 0x001B557C
		public override void WriteData(BinaryWriter writer, IFormatter formatter)
		{
			writer.Write(this.data);
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x001B558C File Offset: 0x001B558C
		public override string ToString()
		{
			return "Binary: Length: " + this.data.Length.ToString();
		}

		// Token: 0x04002B0D RID: 11021
		private byte[] data;
	}
}
