using System;
using System.Collections;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000295 RID: 661
	public class ArmoredInputStream : BaseInputStream
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x0006E0D8 File Offset: 0x0006E0D8
		static ArmoredInputStream()
		{
			for (int i = 65; i <= 90; i++)
			{
				ArmoredInputStream.decodingTable[i] = (byte)(i - 65);
			}
			for (int j = 97; j <= 122; j++)
			{
				ArmoredInputStream.decodingTable[j] = (byte)(j - 97 + 26);
			}
			for (int k = 48; k <= 57; k++)
			{
				ArmoredInputStream.decodingTable[k] = (byte)(k - 48 + 52);
			}
			ArmoredInputStream.decodingTable[43] = 62;
			ArmoredInputStream.decodingTable[47] = 63;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0006E16C File Offset: 0x0006E16C
		private int Decode(int in0, int in1, int in2, int in3, int[] result)
		{
			if (in3 < 0)
			{
				throw new EndOfStreamException("unexpected end of file in armored stream.");
			}
			int num;
			int num2;
			if (in2 == 61)
			{
				num = (int)(ArmoredInputStream.decodingTable[in0] & byte.MaxValue);
				num2 = (int)(ArmoredInputStream.decodingTable[in1] & byte.MaxValue);
				result[2] = ((num << 2 | num2 >> 4) & 255);
				return 2;
			}
			int num3;
			if (in3 == 61)
			{
				num = (int)ArmoredInputStream.decodingTable[in0];
				num2 = (int)ArmoredInputStream.decodingTable[in1];
				num3 = (int)ArmoredInputStream.decodingTable[in2];
				result[1] = ((num << 2 | num2 >> 4) & 255);
				result[2] = ((num2 << 4 | num3 >> 2) & 255);
				return 1;
			}
			num = (int)ArmoredInputStream.decodingTable[in0];
			num2 = (int)ArmoredInputStream.decodingTable[in1];
			num3 = (int)ArmoredInputStream.decodingTable[in2];
			int num4 = (int)ArmoredInputStream.decodingTable[in3];
			result[0] = ((num << 2 | num2 >> 4) & 255);
			result[1] = ((num2 << 4 | num3 >> 2) & 255);
			result[2] = ((num3 << 6 | num4) & 255);
			return 0;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0006E260 File Offset: 0x0006E260
		public ArmoredInputStream(Stream input) : this(input, true)
		{
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0006E26C File Offset: 0x0006E26C
		public ArmoredInputStream(Stream input, bool hasHeaders)
		{
			this.input = input;
			this.hasHeaders = hasHeaders;
			if (hasHeaders)
			{
				this.ParseHeaders();
			}
			this.start = false;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0006E308 File Offset: 0x0006E308
		private bool ParseHeaders()
		{
			this.header = null;
			int num = 0;
			bool flag = false;
			this.headerList = Platform.CreateArrayList();
			if (this.restart)
			{
				flag = true;
			}
			else
			{
				int num2;
				while ((num2 = this.input.ReadByte()) >= 0)
				{
					if (num2 == 45 && (num == 0 || num == 10 || num == 13))
					{
						flag = true;
						break;
					}
					num = num2;
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder("-");
				bool flag2 = false;
				bool flag3 = false;
				if (this.restart)
				{
					stringBuilder.Append('-');
				}
				int num2;
				while ((num2 = this.input.ReadByte()) >= 0)
				{
					if (num == 13 && num2 == 10)
					{
						flag3 = true;
					}
					if ((flag2 && num != 13 && num2 == 10) || (flag2 && num2 == 13))
					{
						break;
					}
					if (num2 == 13 || (num != 13 && num2 == 10))
					{
						string text = stringBuilder.ToString();
						if (text.Trim().Length < 1)
						{
							break;
						}
						this.headerList.Add(text);
						stringBuilder.Length = 0;
					}
					if (num2 != 10 && num2 != 13)
					{
						stringBuilder.Append((char)num2);
						flag2 = false;
					}
					else if (num2 == 13 || (num != 13 && num2 == 10))
					{
						flag2 = true;
					}
					num = num2;
				}
				if (flag3)
				{
					this.input.ReadByte();
				}
			}
			if (this.headerList.Count > 0)
			{
				this.header = (string)this.headerList[0];
			}
			this.clearText = "-----BEGIN PGP SIGNED MESSAGE-----".Equals(this.header);
			this.newLineFound = true;
			return flag;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0006E4D4 File Offset: 0x0006E4D4
		public bool IsClearText()
		{
			return this.clearText;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0006E4DC File Offset: 0x0006E4DC
		public bool IsEndOfStream()
		{
			return this.isEndOfStream;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0006E4E4 File Offset: 0x0006E4E4
		public string GetArmorHeaderLine()
		{
			return this.header;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0006E4EC File Offset: 0x0006E4EC
		public string[] GetArmorHeaders()
		{
			if (this.headerList.Count <= 1)
			{
				return null;
			}
			string[] array = new string[this.headerList.Count - 1];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (string)this.headerList[num + 1];
			}
			return array;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0006E550 File Offset: 0x0006E550
		private int ReadIgnoreSpace()
		{
			int num;
			do
			{
				num = this.input.ReadByte();
			}
			while (num == 32 || num == 9);
			return num;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0006E578 File Offset: 0x0006E578
		private int ReadIgnoreWhitespace()
		{
			int num;
			do
			{
				num = this.input.ReadByte();
			}
			while (num == 32 || num == 9 || num == 13 || num == 10);
			return num;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0006E5AC File Offset: 0x0006E5AC
		private int ReadByteClearText()
		{
			int num = this.input.ReadByte();
			if (num == 13 || (num == 10 && this.lastC != 13))
			{
				this.newLineFound = true;
			}
			else if (this.newLineFound && num == 45)
			{
				num = this.input.ReadByte();
				if (num == 45)
				{
					this.clearText = false;
					this.start = true;
					this.restart = true;
				}
				else
				{
					num = this.input.ReadByte();
				}
				this.newLineFound = false;
			}
			else if (num != 10 && this.lastC != 13)
			{
				this.newLineFound = false;
			}
			this.lastC = num;
			if (num < 0)
			{
				this.isEndOfStream = true;
			}
			return num;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0006E680 File Offset: 0x0006E680
		private int ReadClearText(byte[] buffer, int offset, int count)
		{
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.ReadByteClearText();
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException ex)
			{
				if (i == offset)
				{
					throw ex;
				}
			}
			return i - offset;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0006E6DC File Offset: 0x0006E6DC
		private int DoReadByte()
		{
			if (this.bufPtr > 2 || this.crcFound)
			{
				int num = this.ReadIgnoreSpace();
				if (num == 10 || num == 13)
				{
					num = this.ReadIgnoreWhitespace();
					if (num == 61)
					{
						this.bufPtr = this.Decode(this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.outBuf);
						if (this.bufPtr != 0)
						{
							throw new IOException("no crc found in armored message.");
						}
						this.crcFound = true;
						int num2 = (this.outBuf[0] & 255) << 16 | (this.outBuf[1] & 255) << 8 | (this.outBuf[2] & 255);
						if (num2 != this.crc.Value)
						{
							throw new IOException("crc check failed in armored message.");
						}
						return this.ReadByte();
					}
					else if (num == 45)
					{
						while ((num = this.input.ReadByte()) >= 0 && num != 10 && num != 13)
						{
						}
						if (!this.crcFound)
						{
							throw new IOException("crc check not found.");
						}
						this.crcFound = false;
						this.start = true;
						this.bufPtr = 3;
						if (num < 0)
						{
							this.isEndOfStream = true;
						}
						return -1;
					}
				}
				if (num < 0)
				{
					this.isEndOfStream = true;
					return -1;
				}
				this.bufPtr = this.Decode(num, this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.outBuf);
			}
			return this.outBuf[this.bufPtr++];
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0006E87C File Offset: 0x0006E87C
		public override int ReadByte()
		{
			if (this.start)
			{
				if (this.hasHeaders)
				{
					this.ParseHeaders();
				}
				this.crc.Reset();
				this.start = false;
			}
			if (this.clearText)
			{
				return this.ReadByteClearText();
			}
			int num = this.DoReadByte();
			this.crc.Update(num);
			return num;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0006E8E4 File Offset: 0x0006E8E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.start && count > 0)
			{
				if (this.hasHeaders)
				{
					this.ParseHeaders();
				}
				this.start = false;
			}
			if (this.clearText)
			{
				return this.ReadClearText(buffer, offset, count);
			}
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.DoReadByte();
					this.crc.Update(num2);
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException ex)
			{
				if (i == offset)
				{
					throw ex;
				}
			}
			return i - offset;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0006E98C File Offset: 0x0006E98C
		public override void Close()
		{
			Platform.Dispose(this.input);
			base.Close();
		}

		// Token: 0x04000DEA RID: 3562
		private static readonly byte[] decodingTable = new byte[128];

		// Token: 0x04000DEB RID: 3563
		private Stream input;

		// Token: 0x04000DEC RID: 3564
		private bool start = true;

		// Token: 0x04000DED RID: 3565
		private int[] outBuf = new int[3];

		// Token: 0x04000DEE RID: 3566
		private int bufPtr = 3;

		// Token: 0x04000DEF RID: 3567
		private Crc24 crc = new Crc24();

		// Token: 0x04000DF0 RID: 3568
		private bool crcFound = false;

		// Token: 0x04000DF1 RID: 3569
		private bool hasHeaders = true;

		// Token: 0x04000DF2 RID: 3570
		private string header = null;

		// Token: 0x04000DF3 RID: 3571
		private bool newLineFound = false;

		// Token: 0x04000DF4 RID: 3572
		private bool clearText = false;

		// Token: 0x04000DF5 RID: 3573
		private bool restart = false;

		// Token: 0x04000DF6 RID: 3574
		private IList headerList = Platform.CreateArrayList();

		// Token: 0x04000DF7 RID: 3575
		private int lastC = 0;

		// Token: 0x04000DF8 RID: 3576
		private bool isEndOfStream;
	}
}
