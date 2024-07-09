using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DC RID: 1756
	public class HexEncoder : IEncoder
	{
		// Token: 0x06003D73 RID: 15731 RVA: 0x001509A8 File Offset: 0x001509A8
		protected void InitialiseDecodingTable()
		{
			Arrays.Fill(this.decodingTable, byte.MaxValue);
			for (int i = 0; i < this.encodingTable.Length; i++)
			{
				this.decodingTable[(int)this.encodingTable[i]] = (byte)i;
			}
			this.decodingTable[65] = this.decodingTable[97];
			this.decodingTable[66] = this.decodingTable[98];
			this.decodingTable[67] = this.decodingTable[99];
			this.decodingTable[68] = this.decodingTable[100];
			this.decodingTable[69] = this.decodingTable[101];
			this.decodingTable[70] = this.decodingTable[102];
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00150A5C File Offset: 0x00150A5C
		public HexEncoder()
		{
			this.InitialiseDecodingTable();
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00150A94 File Offset: 0x00150A94
		public int Encode(byte[] data, int off, int length, Stream outStream)
		{
			for (int i = off; i < off + length; i++)
			{
				int num = (int)data[i];
				outStream.WriteByte(this.encodingTable[num >> 4]);
				outStream.WriteByte(this.encodingTable[num & 15]);
			}
			return length * 2;
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00150AE0 File Offset: 0x00150AE0
		private static bool Ignore(char c)
		{
			return c == '\n' || c == '\r' || c == '\t' || c == ' ';
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00150B04 File Offset: 0x00150B04
		public int Decode(byte[] data, int off, int length, Stream outStream)
		{
			int num = 0;
			int num2 = off + length;
			while (num2 > off && HexEncoder.Ignore((char)data[num2 - 1]))
			{
				num2--;
			}
			int i = off;
			while (i < num2)
			{
				while (i < num2 && HexEncoder.Ignore((char)data[i]))
				{
					i++;
				}
				byte b = this.decodingTable[(int)data[i++]];
				while (i < num2 && HexEncoder.Ignore((char)data[i]))
				{
					i++;
				}
				byte b2 = this.decodingTable[(int)data[i++]];
				if ((b | b2) >= 128)
				{
					throw new IOException("invalid characters encountered in Hex data");
				}
				outStream.WriteByte((byte)((int)b << 4 | (int)b2));
				num++;
			}
			return num;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x00150BBC File Offset: 0x00150BBC
		public int DecodeString(string data, Stream outStream)
		{
			int num = 0;
			int num2 = data.Length;
			while (num2 > 0 && HexEncoder.Ignore(data[num2 - 1]))
			{
				num2--;
			}
			int i = 0;
			while (i < num2)
			{
				while (i < num2 && HexEncoder.Ignore(data[i]))
				{
					i++;
				}
				byte b = this.decodingTable[(int)data[i++]];
				while (i < num2 && HexEncoder.Ignore(data[i]))
				{
					i++;
				}
				byte b2 = this.decodingTable[(int)data[i++]];
				if ((b | b2) >= 128)
				{
					throw new IOException("invalid characters encountered in Hex data");
				}
				outStream.WriteByte((byte)((int)b << 4 | (int)b2));
				num++;
			}
			return num;
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x00150C90 File Offset: 0x00150C90
		internal byte[] DecodeStrict(string str, int off, int len)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (off < 0 || len < 0 || off > str.Length - len)
			{
				throw new IndexOutOfRangeException("invalid offset and/or length specified");
			}
			if ((len & 1) != 0)
			{
				throw new ArgumentException("a hexadecimal encoding must have an even number of characters", "len");
			}
			int num = len >> 1;
			byte[] array = new byte[num];
			int num2 = off;
			for (int i = 0; i < num; i++)
			{
				byte b = this.decodingTable[(int)str[num2++]];
				byte b2 = this.decodingTable[(int)str[num2++]];
				if ((b | b2) >= 128)
				{
					throw new IOException("invalid characters encountered in Hex data");
				}
				array[i] = (byte)((int)b << 4 | (int)b2);
			}
			return array;
		}

		// Token: 0x04001EF6 RID: 7926
		protected readonly byte[] encodingTable = new byte[]
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

		// Token: 0x04001EF7 RID: 7927
		protected readonly byte[] decodingTable = new byte[128];
	}
}
