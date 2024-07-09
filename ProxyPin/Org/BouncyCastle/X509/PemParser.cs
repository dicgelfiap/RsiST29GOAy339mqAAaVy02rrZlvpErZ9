using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000713 RID: 1811
	internal class PemParser
	{
		// Token: 0x06003F67 RID: 16231 RVA: 0x0015B860 File Offset: 0x0015B860
		internal PemParser(string type)
		{
			this._header1 = "-----BEGIN " + type + "-----";
			this._header2 = "-----BEGIN X509 " + type + "-----";
			this._footer1 = "-----END " + type + "-----";
			this._footer2 = "-----END X509 " + type + "-----";
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x0015B8D0 File Offset: 0x0015B8D0
		private string ReadLine(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				if ((num = inStream.ReadByte()) == 13 || num == 10 || num < 0)
				{
					if (num < 0 || stringBuilder.Length != 0)
					{
						break;
					}
				}
				else if (num != 13)
				{
					stringBuilder.Append((char)num);
				}
			}
			if (num < 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0015B938 File Offset: 0x0015B938
		internal Asn1Sequence ReadPemObject(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			while ((text = this.ReadLine(inStream)) != null)
			{
				if (Platform.StartsWith(text, this._header1) || Platform.StartsWith(text, this._header2))
				{
					IL_67:
					while ((text = this.ReadLine(inStream)) != null && !Platform.StartsWith(text, this._footer1) && !Platform.StartsWith(text, this._footer2))
					{
						stringBuilder.Append(text);
					}
					if (stringBuilder.Length == 0)
					{
						return null;
					}
					Asn1Object asn1Object = Asn1Object.FromByteArray(Base64.Decode(stringBuilder.ToString()));
					if (!(asn1Object is Asn1Sequence))
					{
						throw new IOException("malformed PEM data encountered");
					}
					return (Asn1Sequence)asn1Object;
				}
			}
			goto IL_67;
		}

		// Token: 0x04002097 RID: 8343
		private readonly string _header1;

		// Token: 0x04002098 RID: 8344
		private readonly string _header2;

		// Token: 0x04002099 RID: 8345
		private readonly string _footer1;

		// Token: 0x0400209A RID: 8346
		private readonly string _footer2;
	}
}
