using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DE RID: 1758
	public class HexTranslator : ITranslator
	{
		// Token: 0x06003D7E RID: 15742 RVA: 0x00150D5C File Offset: 0x00150D5C
		public int GetEncodedBlockSize()
		{
			return 2;
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x00150D60 File Offset: 0x00150D60
		public int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int i = 0;
			int num = 0;
			while (i < length)
			{
				outBytes[outOff + num] = HexTranslator.hexTable[input[inOff] >> 4 & 15];
				outBytes[outOff + num + 1] = HexTranslator.hexTable[(int)(input[inOff] & 15)];
				inOff++;
				i++;
				num += 2;
			}
			return length * 2;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x00150DB8 File Offset: 0x00150DB8
		public int GetDecodedBlockSize()
		{
			return 1;
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x00150DBC File Offset: 0x00150DBC
		public int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int num = length / 2;
			for (int i = 0; i < num; i++)
			{
				byte b = input[inOff + i * 2];
				byte b2 = input[inOff + i * 2 + 1];
				if (b < 97)
				{
					outBytes[outOff] = (byte)(b - 48 << 4);
				}
				else
				{
					outBytes[outOff] = (byte)(b - 97 + 10 << 4);
				}
				if (b2 < 97)
				{
					IntPtr intPtr;
					outBytes[(int)(intPtr = (IntPtr)outOff)] = outBytes[(int)intPtr] + (b2 - 48);
				}
				else
				{
					IntPtr intPtr;
					outBytes[(int)(intPtr = (IntPtr)outOff)] = outBytes[(int)intPtr] + (b2 - 97 + 10);
				}
				outOff++;
			}
			return num;
		}

		// Token: 0x04001EF8 RID: 7928
		private static readonly byte[] hexTable = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};
	}
}
