using System;
using System.Collections;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x02000676 RID: 1654
	public class PemReader
	{
		// Token: 0x060039D1 RID: 14801 RVA: 0x0013647C File Offset: 0x0013647C
		public PemReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x0013649C File Offset: 0x0013649C
		public TextReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x001364A4 File Offset: 0x001364A4
		public PemObject ReadPemObject()
		{
			string text = this.reader.ReadLine();
			if (text != null && Platform.StartsWith(text, "-----BEGIN "))
			{
				text = text.Substring("-----BEGIN ".Length);
				int num = text.IndexOf('-');
				if (num > 0 && Platform.EndsWith(text, "-----") && text.Length - num == 5)
				{
					string type = text.Substring(0, num);
					return this.LoadObject(type);
				}
			}
			return null;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x00136528 File Offset: 0x00136528
		private PemObject LoadObject(string type)
		{
			string text = "-----END " + type;
			IList list = Platform.CreateArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			string text2;
			while ((text2 = this.reader.ReadLine()) != null && Platform.IndexOf(text2, text) == -1)
			{
				int num = text2.IndexOf(':');
				if (num == -1)
				{
					stringBuilder.Append(text2.Trim());
				}
				else
				{
					string text3 = text2.Substring(0, num).Trim();
					if (Platform.StartsWith(text3, "X-"))
					{
						text3 = text3.Substring(2);
					}
					string val = text2.Substring(num + 1).Trim();
					list.Add(new PemHeader(text3, val));
				}
			}
			if (text2 == null)
			{
				throw new IOException(text + " not found");
			}
			if (stringBuilder.Length % 4 != 0)
			{
				throw new IOException("base64 data appears to be truncated");
			}
			return new PemObject(type, list, Base64.Decode(stringBuilder.ToString()));
		}

		// Token: 0x04001E1C RID: 7708
		private const string BeginString = "-----BEGIN ";

		// Token: 0x04001E1D RID: 7709
		private const string EndString = "-----END ";

		// Token: 0x04001E1E RID: 7710
		private readonly TextReader reader;
	}
}
