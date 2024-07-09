using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006D8 RID: 1752
	public class Base64Encoder : IEncoder
	{
		// Token: 0x06003D57 RID: 15703 RVA: 0x0014FF08 File Offset: 0x0014FF08
		protected void InitialiseDecodingTable()
		{
			Arrays.Fill(this.decodingTable, byte.MaxValue);
			for (int i = 0; i < this.encodingTable.Length; i++)
			{
				this.decodingTable[(int)this.encodingTable[i]] = (byte)i;
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x0014FF50 File Offset: 0x0014FF50
		public Base64Encoder()
		{
			this.InitialiseDecodingTable();
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x0014FF90 File Offset: 0x0014FF90
		public int Encode(byte[] data, int off, int length, Stream outStream)
		{
			int num = length % 3;
			int num2 = length - num;
			for (int i = off; i < off + num2; i += 3)
			{
				int num3 = (int)(data[i] & byte.MaxValue);
				int num4 = (int)(data[i + 1] & byte.MaxValue);
				int num5 = (int)(data[i + 2] & byte.MaxValue);
				outStream.WriteByte(this.encodingTable[(int)((uint)num3 >> 2 & 63U)]);
				outStream.WriteByte(this.encodingTable[(num3 << 4 | (int)((uint)num4 >> 4)) & 63]);
				outStream.WriteByte(this.encodingTable[(num4 << 2 | (int)((uint)num5 >> 6)) & 63]);
				outStream.WriteByte(this.encodingTable[num5 & 63]);
			}
			switch (num)
			{
			case 1:
			{
				int num6 = (int)(data[off + num2] & byte.MaxValue);
				int num7 = num6 >> 2 & 63;
				int num8 = num6 << 4 & 63;
				outStream.WriteByte(this.encodingTable[num7]);
				outStream.WriteByte(this.encodingTable[num8]);
				outStream.WriteByte(this.padding);
				outStream.WriteByte(this.padding);
				break;
			}
			case 2:
			{
				int num6 = (int)(data[off + num2] & byte.MaxValue);
				int num9 = (int)(data[off + num2 + 1] & byte.MaxValue);
				int num7 = num6 >> 2 & 63;
				int num8 = (num6 << 4 | num9 >> 4) & 63;
				int num10 = num9 << 2 & 63;
				outStream.WriteByte(this.encodingTable[num7]);
				outStream.WriteByte(this.encodingTable[num8]);
				outStream.WriteByte(this.encodingTable[num10]);
				outStream.WriteByte(this.padding);
				break;
			}
			}
			return num2 / 3 * 4 + ((num == 0) ? 0 : 4);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x00150140 File Offset: 0x00150140
		private bool ignore(char c)
		{
			return c == '\n' || c == '\r' || c == '\t' || c == ' ';
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00150164 File Offset: 0x00150164
		public int Decode(byte[] data, int off, int length, Stream outStream)
		{
			int num = 0;
			int num2 = off + length;
			while (num2 > off && this.ignore((char)data[num2 - 1]))
			{
				num2--;
			}
			int num3 = num2 - 4;
			for (int i = this.nextI(data, off, num3); i < num3; i = this.nextI(data, i, num3))
			{
				byte b = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b2 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b3 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b4 = this.decodingTable[(int)data[i++]];
				if ((b | b2 | b3 | b4) >= 128)
				{
					throw new IOException("invalid characters encountered in base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				outStream.WriteByte((byte)((int)b2 << 4 | b3 >> 2));
				outStream.WriteByte((byte)((int)b3 << 6 | (int)b4));
				num += 3;
			}
			return num + this.decodeLastBlock(outStream, (char)data[num2 - 4], (char)data[num2 - 3], (char)data[num2 - 2], (char)data[num2 - 1]);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x00150294 File Offset: 0x00150294
		private int nextI(byte[] data, int i, int finish)
		{
			while (i < finish && this.ignore((char)data[i]))
			{
				i++;
			}
			return i;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x001502B4 File Offset: 0x001502B4
		public int DecodeString(string data, Stream outStream)
		{
			int num = 0;
			int num2 = data.Length;
			while (num2 > 0 && this.ignore(data[num2 - 1]))
			{
				num2--;
			}
			int i = 0;
			int num3 = num2 - 4;
			for (i = this.nextI(data, i, num3); i < num3; i = this.nextI(data, i, num3))
			{
				byte b = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b2 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b3 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b4 = this.decodingTable[(int)data[i++]];
				if ((b | b2 | b3 | b4) >= 128)
				{
					throw new IOException("invalid characters encountered in base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				outStream.WriteByte((byte)((int)b2 << 4 | b3 >> 2));
				outStream.WriteByte((byte)((int)b3 << 6 | (int)b4));
				num += 3;
			}
			return num + this.decodeLastBlock(outStream, data[num2 - 4], data[num2 - 3], data[num2 - 2], data[num2 - 1]);
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x00150404 File Offset: 0x00150404
		private int decodeLastBlock(Stream outStream, char c1, char c2, char c3, char c4)
		{
			if (c3 == (char)this.padding)
			{
				if (c4 != (char)this.padding)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				byte b = this.decodingTable[(int)c1];
				byte b2 = this.decodingTable[(int)c2];
				if ((b | b2) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				return 1;
			}
			else if (c4 == (char)this.padding)
			{
				byte b3 = this.decodingTable[(int)c1];
				byte b4 = this.decodingTable[(int)c2];
				byte b5 = this.decodingTable[(int)c3];
				if ((b3 | b4 | b5) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b3 << 2 | b4 >> 4));
				outStream.WriteByte((byte)((int)b4 << 4 | b5 >> 2));
				return 2;
			}
			else
			{
				byte b6 = this.decodingTable[(int)c1];
				byte b7 = this.decodingTable[(int)c2];
				byte b8 = this.decodingTable[(int)c3];
				byte b9 = this.decodingTable[(int)c4];
				if ((b6 | b7 | b8 | b9) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b6 << 2 | b7 >> 4));
				outStream.WriteByte((byte)((int)b7 << 4 | b8 >> 2));
				outStream.WriteByte((byte)((int)b8 << 6 | (int)b9));
				return 3;
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x00150554 File Offset: 0x00150554
		private int nextI(string data, int i, int finish)
		{
			while (i < finish && this.ignore(data[i]))
			{
				i++;
			}
			return i;
		}

		// Token: 0x04001EEC RID: 7916
		protected readonly byte[] encodingTable = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
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
			43,
			47
		};

		// Token: 0x04001EED RID: 7917
		protected byte padding = 61;

		// Token: 0x04001EEE RID: 7918
		protected readonly byte[] decodingTable = new byte[128];
	}
}
