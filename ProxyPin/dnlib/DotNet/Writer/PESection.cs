using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D0 RID: 2256
	[ComVisible(true)]
	public sealed class PESection : ChunkList<IChunk>
	{
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x001AD67C File Offset: 0x001AD67C
		// (set) Token: 0x060057BD RID: 22461 RVA: 0x001AD684 File Offset: 0x001AD684
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x060057BE RID: 22462 RVA: 0x001AD690 File Offset: 0x001AD690
		// (set) Token: 0x060057BF RID: 22463 RVA: 0x001AD698 File Offset: 0x001AD698
		public uint Characteristics
		{
			get
			{
				return this.characteristics;
			}
			set
			{
				this.characteristics = value;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x060057C0 RID: 22464 RVA: 0x001AD6A4 File Offset: 0x001AD6A4
		public bool IsCode
		{
			get
			{
				return (this.characteristics & 32U) > 0U;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x001AD6B4 File Offset: 0x001AD6B4
		public bool IsInitializedData
		{
			get
			{
				return (this.characteristics & 64U) > 0U;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x001AD6C4 File Offset: 0x001AD6C4
		public bool IsUninitializedData
		{
			get
			{
				return (this.characteristics & 128U) > 0U;
			}
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x001AD6D8 File Offset: 0x001AD6D8
		public PESection(string name, uint characteristics)
		{
			this.name = name;
			this.characteristics = characteristics;
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x001AD6F0 File Offset: 0x001AD6F0
		public uint WriteHeaderTo(DataWriter writer, uint fileAlignment, uint sectionAlignment, uint rva)
		{
			uint virtualSize = base.GetVirtualSize();
			uint fileLength = base.GetFileLength();
			uint result = Utils.AlignUp(virtualSize, sectionAlignment);
			uint value = Utils.AlignUp(fileLength, fileAlignment);
			uint fileOffset = (uint)base.FileOffset;
			writer.WriteBytes(Encoding.UTF8.GetBytes(this.Name + "\0\0\0\0\0\0\0\0"), 0, 8);
			writer.WriteUInt32(virtualSize);
			writer.WriteUInt32(rva);
			writer.WriteUInt32(value);
			writer.WriteUInt32(fileOffset);
			writer.WriteInt32(0);
			writer.WriteInt32(0);
			writer.WriteUInt16(0);
			writer.WriteUInt16(0);
			writer.WriteUInt32(this.Characteristics);
			return result;
		}

		// Token: 0x04002A38 RID: 10808
		private string name;

		// Token: 0x04002A39 RID: 10809
		private uint characteristics;
	}
}
