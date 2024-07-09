using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PeNet
{
	// Token: 0x02000B8A RID: 2954
	[ComVisible(true)]
	public class CrlUrlList
	{
		// Token: 0x060076BA RID: 30394 RVA: 0x0023821C File Offset: 0x0023821C
		public CrlUrlList(byte[] rawData)
		{
			this.Urls = new List<string>();
			if (rawData == null)
			{
				return;
			}
			this.ParseCrls(rawData);
		}

		// Token: 0x060076BB RID: 30395 RVA: 0x00238240 File Offset: 0x00238240
		public CrlUrlList(X509Certificate2 cert)
		{
			this.Urls = new List<string>();
			if (cert == null)
			{
				return;
			}
			foreach (X509Extension x509Extension in cert.Extensions)
			{
				if (x509Extension.Oid.Value == "2.5.29.31")
				{
					this.ParseCrls(x509Extension.RawData);
				}
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x002382B0 File Offset: 0x002382B0
		public List<string> Urls { get; }

		// Token: 0x060076BD RID: 30397 RVA: 0x002382B8 File Offset: 0x002382B8
		private void ParseCrls(byte[] rawData)
		{
			int num = rawData.Length;
			for (int i = 0; i < num - 5; i++)
			{
				if ((rawData[i] == 104 && rawData[i + 1] == 116 && rawData[i + 2] == 116 && rawData[i + 3] == 112 && rawData[i + 4] == 58) || (rawData[i] == 108 && rawData[i + 1] == 100 && rawData[i + 2] == 97 && rawData[i + 3] == 112 && rawData[i + 4] == 58))
				{
					List<byte> list = new List<byte>();
					for (int j = i; j < num; j++)
					{
						if ((rawData[j - 4] == 46 && rawData[j - 3] == 99 && rawData[j - 2] == 114 && rawData[j - 1] == 108) || (rawData[j] == 98 && rawData[j + 1] == 97 && rawData[j + 2] == 115 && rawData[j + 3] == 101))
						{
							i = j;
							break;
						}
						if (rawData[j] < 32 || rawData[j] > 126)
						{
							i = j;
							break;
						}
						list.Add(rawData[j]);
					}
					string text = Encoding.ASCII.GetString(list.ToArray());
					if (this.IsValidUri(text) && text.StartsWith("http://") && text.EndsWith(".crl"))
					{
						this.Urls.Add(text);
					}
					if (text.StartsWith("ldap:", StringComparison.InvariantCulture))
					{
						text = "ldap://" + text.Split(new char[]
						{
							'/'
						})[2];
						this.Urls.Add(text);
					}
				}
			}
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x00238488 File Offset: 0x00238488
		private bool IsValidUri(string uri)
		{
			Uri uri2;
			return Uri.TryCreate(uri, UriKind.Absolute, out uri2) && (uri2.Scheme == Uri.UriSchemeHttp || uri2.Scheme == Uri.UriSchemeHttps);
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x002384D0 File Offset: 0x002384D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("CRL URLs:");
			foreach (string arg in this.Urls)
			{
				stringBuilder.AppendFormat("\t{0}\n", arg);
			}
			return stringBuilder.ToString();
		}
	}
}
