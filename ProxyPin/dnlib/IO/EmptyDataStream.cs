using System;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x0200076A RID: 1898
	internal sealed class EmptyDataStream : DataStream
	{
		// Token: 0x06004299 RID: 17049 RVA: 0x00165CBC File Offset: 0x00165CBC
		private EmptyDataStream()
		{
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x00165CC4 File Offset: 0x00165CC4
		public unsafe override void ReadBytes(uint offset, void* destination, int length)
		{
			for (int i = 0; i < length; i++)
			{
				*(byte*)destination = 0;
			}
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00165CEC File Offset: 0x00165CEC
		public override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				destination[destinationIndex + i] = 0;
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00165D14 File Offset: 0x00165D14
		public override byte ReadByte(uint offset)
		{
			return 0;
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x00165D18 File Offset: 0x00165D18
		public override ushort ReadUInt16(uint offset)
		{
			return 0;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x00165D1C File Offset: 0x00165D1C
		public override uint ReadUInt32(uint offset)
		{
			return 0U;
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x00165D20 File Offset: 0x00165D20
		public override ulong ReadUInt64(uint offset)
		{
			return 0UL;
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x00165D24 File Offset: 0x00165D24
		public override float ReadSingle(uint offset)
		{
			return 0f;
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x00165D2C File Offset: 0x00165D2C
		public override double ReadDouble(uint offset)
		{
			return 0.0;
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x00165D38 File Offset: 0x00165D38
		public override string ReadUtf16String(uint offset, int chars)
		{
			return string.Empty;
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x00165D40 File Offset: 0x00165D40
		public override string ReadString(uint offset, int length, Encoding encoding)
		{
			return string.Empty;
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x00165D48 File Offset: 0x00165D48
		public override bool TryGetOffsetOf(uint offset, uint endOffset, byte value, out uint valueOffset)
		{
			valueOffset = 0U;
			return false;
		}

		// Token: 0x0400238C RID: 9100
		public static readonly DataStream Instance = new EmptyDataStream();
	}
}
