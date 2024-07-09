using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000296 RID: 662
	public class ArmoredOutputStream : BaseOutputStream
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0006E9A0 File Offset: 0x0006E9A0
		private static void Encode(Stream outStream, int[] data, int len)
		{
			byte[] array = new byte[4];
			int num = data[0];
			array[0] = ArmoredOutputStream.encodingTable[num >> 2 & 63];
			switch (len)
			{
			case 1:
				array[1] = ArmoredOutputStream.encodingTable[num << 4 & 63];
				array[2] = 61;
				array[3] = 61;
				break;
			case 2:
			{
				int num2 = data[1];
				array[1] = ArmoredOutputStream.encodingTable[(num << 4 | num2 >> 4) & 63];
				array[2] = ArmoredOutputStream.encodingTable[num2 << 2 & 63];
				array[3] = 61;
				break;
			}
			case 3:
			{
				int num3 = data[1];
				int num4 = data[2];
				array[1] = ArmoredOutputStream.encodingTable[(num << 4 | num3 >> 4) & 63];
				array[2] = ArmoredOutputStream.encodingTable[(num3 << 2 | num4 >> 6) & 63];
				array[3] = ArmoredOutputStream.encodingTable[num4 & 63];
				break;
			}
			}
			outStream.Write(array, 0, array.Length);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0006EA80 File Offset: 0x0006EA80
		public ArmoredOutputStream(Stream outStream)
		{
			this.outStream = outStream;
			this.headers = Platform.CreateHashtable(1);
			this.headers.Add(ArmoredOutputStream.HeaderVersion, ArmoredOutputStream.Version);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0006EAFC File Offset: 0x0006EAFC
		public ArmoredOutputStream(Stream outStream, IDictionary headers)
		{
			this.outStream = outStream;
			this.headers = Platform.CreateHashtable(headers);
			if (!this.headers.Contains(ArmoredOutputStream.HeaderVersion))
			{
				this.headers.Add(ArmoredOutputStream.HeaderVersion, ArmoredOutputStream.Version);
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0006EB8C File Offset: 0x0006EB8C
		public void SetHeader(string name, string v)
		{
			if (v == null)
			{
				this.headers.Remove(name);
				return;
			}
			this.headers[name] = v;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0006EBB0 File Offset: 0x0006EBB0
		public void ResetHeaders()
		{
			string text = (string)this.headers[ArmoredOutputStream.HeaderVersion];
			this.headers.Clear();
			if (text != null)
			{
				this.headers.Add(ArmoredOutputStream.HeaderVersion, text);
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0006EBFC File Offset: 0x0006EBFC
		public void BeginClearText(HashAlgorithmTag hashAlgorithm)
		{
			string str;
			switch (hashAlgorithm)
			{
			case HashAlgorithmTag.MD5:
				str = "MD5";
				goto IL_9A;
			case HashAlgorithmTag.Sha1:
				str = "SHA1";
				goto IL_9A;
			case HashAlgorithmTag.RipeMD160:
				str = "RIPEMD160";
				goto IL_9A;
			case HashAlgorithmTag.MD2:
				str = "MD2";
				goto IL_9A;
			case HashAlgorithmTag.Sha256:
				str = "SHA256";
				goto IL_9A;
			case HashAlgorithmTag.Sha384:
				str = "SHA384";
				goto IL_9A;
			case HashAlgorithmTag.Sha512:
				str = "SHA512";
				goto IL_9A;
			}
			throw new IOException("unknown hash algorithm tag in beginClearText: " + hashAlgorithm);
			IL_9A:
			this.DoWrite("-----BEGIN PGP SIGNED MESSAGE-----" + ArmoredOutputStream.nl);
			this.DoWrite("Hash: " + str + ArmoredOutputStream.nl + ArmoredOutputStream.nl);
			this.clearText = true;
			this.newLine = true;
			this.lastb = 0;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0006ECEC File Offset: 0x0006ECEC
		public void EndClearText()
		{
			this.clearText = false;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0006ECF8 File Offset: 0x0006ECF8
		public override void WriteByte(byte b)
		{
			if (this.clearText)
			{
				this.outStream.WriteByte(b);
				if (this.newLine)
				{
					if (b != 10 || this.lastb != 13)
					{
						this.newLine = false;
					}
					if (b == 45)
					{
						this.outStream.WriteByte(32);
						this.outStream.WriteByte(45);
					}
				}
				if (b == 13 || (b == 10 && this.lastb != 13))
				{
					this.newLine = true;
				}
				this.lastb = (int)b;
				return;
			}
			if (this.start)
			{
				bool flag = (b & 64) != 0;
				int num;
				if (flag)
				{
					num = (int)(b & 63);
				}
				else
				{
					num = (b & 63) >> 2;
				}
				switch (num)
				{
				case 2:
					this.type = "SIGNATURE";
					goto IL_117;
				case 5:
					this.type = "PRIVATE KEY BLOCK";
					goto IL_117;
				case 6:
					this.type = "PUBLIC KEY BLOCK";
					goto IL_117;
				}
				this.type = "MESSAGE";
				IL_117:
				this.DoWrite(ArmoredOutputStream.headerStart + this.type + ArmoredOutputStream.headerTail + ArmoredOutputStream.nl);
				if (this.headers.Contains(ArmoredOutputStream.HeaderVersion))
				{
					this.WriteHeaderEntry(ArmoredOutputStream.HeaderVersion, (string)this.headers[ArmoredOutputStream.HeaderVersion]);
				}
				foreach (object obj in this.headers)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					if (text != ArmoredOutputStream.HeaderVersion)
					{
						string v = (string)dictionaryEntry.Value;
						this.WriteHeaderEntry(text, v);
					}
				}
				this.DoWrite(ArmoredOutputStream.nl);
				this.start = false;
			}
			if (this.bufPtr == 3)
			{
				ArmoredOutputStream.Encode(this.outStream, this.buf, this.bufPtr);
				this.bufPtr = 0;
				if ((++this.chunkCount & 15) == 0)
				{
					this.DoWrite(ArmoredOutputStream.nl);
				}
			}
			this.crc.Update((int)b);
			this.buf[this.bufPtr++] = (int)(b & byte.MaxValue);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0006EF90 File Offset: 0x0006EF90
		public override void Close()
		{
			if (this.type == null)
			{
				return;
			}
			this.DoClose();
			this.type = null;
			this.start = true;
			base.Close();
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0006EFB8 File Offset: 0x0006EFB8
		private void DoClose()
		{
			if (this.bufPtr > 0)
			{
				ArmoredOutputStream.Encode(this.outStream, this.buf, this.bufPtr);
			}
			this.DoWrite(ArmoredOutputStream.nl + '=');
			int value = this.crc.Value;
			this.buf[0] = (value >> 16 & 255);
			this.buf[1] = (value >> 8 & 255);
			this.buf[2] = (value & 255);
			ArmoredOutputStream.Encode(this.outStream, this.buf, 3);
			this.DoWrite(ArmoredOutputStream.nl);
			this.DoWrite(ArmoredOutputStream.footerStart);
			this.DoWrite(this.type);
			this.DoWrite(ArmoredOutputStream.footerTail);
			this.DoWrite(ArmoredOutputStream.nl);
			this.outStream.Flush();
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0006F098 File Offset: 0x0006F098
		private void WriteHeaderEntry(string name, string v)
		{
			this.DoWrite(name + ": " + v + ArmoredOutputStream.nl);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006F0B4 File Offset: 0x0006F0B4
		private void DoWrite(string s)
		{
			byte[] array = Strings.ToAsciiByteArray(s);
			this.outStream.Write(array, 0, array.Length);
		}

		// Token: 0x04000DF9 RID: 3577
		public static readonly string HeaderVersion = "Version";

		// Token: 0x04000DFA RID: 3578
		private static readonly byte[] encodingTable = new byte[]
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

		// Token: 0x04000DFB RID: 3579
		private readonly Stream outStream;

		// Token: 0x04000DFC RID: 3580
		private int[] buf = new int[3];

		// Token: 0x04000DFD RID: 3581
		private int bufPtr = 0;

		// Token: 0x04000DFE RID: 3582
		private Crc24 crc = new Crc24();

		// Token: 0x04000DFF RID: 3583
		private int chunkCount = 0;

		// Token: 0x04000E00 RID: 3584
		private int lastb;

		// Token: 0x04000E01 RID: 3585
		private bool start = true;

		// Token: 0x04000E02 RID: 3586
		private bool clearText = false;

		// Token: 0x04000E03 RID: 3587
		private bool newLine = false;

		// Token: 0x04000E04 RID: 3588
		private string type;

		// Token: 0x04000E05 RID: 3589
		private static readonly string nl = Platform.NewLine;

		// Token: 0x04000E06 RID: 3590
		private static readonly string headerStart = "-----BEGIN PGP ";

		// Token: 0x04000E07 RID: 3591
		private static readonly string headerTail = "-----";

		// Token: 0x04000E08 RID: 3592
		private static readonly string footerStart = "-----END PGP ";

		// Token: 0x04000E09 RID: 3593
		private static readonly string footerTail = "-----";

		// Token: 0x04000E0A RID: 3594
		private static readonly string Version = "BCPG C# v" + AssemblyInfo.Version;

		// Token: 0x04000E0B RID: 3595
		private readonly IDictionary headers;
	}
}
