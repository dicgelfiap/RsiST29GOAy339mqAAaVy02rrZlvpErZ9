using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x02000768 RID: 1896
	[ComVisible(true)]
	public abstract class DataStream
	{
		// Token: 0x06004288 RID: 17032
		public unsafe abstract void ReadBytes(uint offset, void* destination, int length);

		// Token: 0x06004289 RID: 17033
		public abstract void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length);

		// Token: 0x0600428A RID: 17034
		public abstract byte ReadByte(uint offset);

		// Token: 0x0600428B RID: 17035
		public abstract ushort ReadUInt16(uint offset);

		// Token: 0x0600428C RID: 17036
		public abstract uint ReadUInt32(uint offset);

		// Token: 0x0600428D RID: 17037
		public abstract ulong ReadUInt64(uint offset);

		// Token: 0x0600428E RID: 17038
		public abstract float ReadSingle(uint offset);

		// Token: 0x0600428F RID: 17039
		public abstract double ReadDouble(uint offset);

		// Token: 0x06004290 RID: 17040 RVA: 0x00165B70 File Offset: 0x00165B70
		public virtual Guid ReadGuid(uint offset)
		{
			return new Guid(this.ReadUInt32(offset), this.ReadUInt16(offset + 4U), this.ReadUInt16(offset + 6U), this.ReadByte(offset + 8U), this.ReadByte(offset + 9U), this.ReadByte(offset + 10U), this.ReadByte(offset + 11U), this.ReadByte(offset + 12U), this.ReadByte(offset + 13U), this.ReadByte(offset + 14U), this.ReadByte(offset + 15U));
		}

		// Token: 0x06004291 RID: 17041
		public abstract string ReadUtf16String(uint offset, int chars);

		// Token: 0x06004292 RID: 17042
		public abstract string ReadString(uint offset, int length, Encoding encoding);

		// Token: 0x06004293 RID: 17043
		public abstract bool TryGetOffsetOf(uint offset, uint endOffset, byte value, out uint valueOffset);
	}
}
