using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000554 RID: 1364
	public class UrlAndHash
	{
		// Token: 0x06002A5F RID: 10847 RVA: 0x000E373C File Offset: 0x000E373C
		public UrlAndHash(string url, byte[] sha1Hash)
		{
			if (url == null || url.Length < 1 || url.Length >= 65536)
			{
				throw new ArgumentException("must have length from 1 to (2^16 - 1)", "url");
			}
			if (sha1Hash != null && sha1Hash.Length != 20)
			{
				throw new ArgumentException("must have length == 20, if present", "sha1Hash");
			}
			this.mUrl = url;
			this.mSha1Hash = sha1Hash;
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000E37B4 File Offset: 0x000E37B4
		public virtual string Url
		{
			get
			{
				return this.mUrl;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000E37BC File Offset: 0x000E37BC
		public virtual byte[] Sha1Hash
		{
			get
			{
				return this.mSha1Hash;
			}
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000E37C4 File Offset: 0x000E37C4
		public virtual void Encode(Stream output)
		{
			byte[] buf = Strings.ToByteArray(this.mUrl);
			TlsUtilities.WriteOpaque16(buf, output);
			if (this.mSha1Hash == null)
			{
				TlsUtilities.WriteUint8(0, output);
				return;
			}
			TlsUtilities.WriteUint8(1, output);
			output.Write(this.mSha1Hash, 0, this.mSha1Hash.Length);
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000E3818 File Offset: 0x000E3818
		public static UrlAndHash Parse(TlsContext context, Stream input)
		{
			byte[] array = TlsUtilities.ReadOpaque16(input);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(47);
			}
			string url = Strings.FromByteArray(array);
			byte[] sha1Hash = null;
			switch (TlsUtilities.ReadUint8(input))
			{
			case 0:
				if (TlsUtilities.IsTlsV12(context))
				{
					throw new TlsFatalAlert(47);
				}
				break;
			case 1:
				sha1Hash = TlsUtilities.ReadFully(20, input);
				break;
			default:
				throw new TlsFatalAlert(47);
			}
			return new UrlAndHash(url, sha1Hash);
		}

		// Token: 0x04001B29 RID: 6953
		protected readonly string mUrl;

		// Token: 0x04001B2A RID: 6954
		protected readonly byte[] mSha1Hash;
	}
}
