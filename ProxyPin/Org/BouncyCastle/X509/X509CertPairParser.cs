using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200071B RID: 1819
	public class X509CertPairParser
	{
		// Token: 0x06003FB6 RID: 16310 RVA: 0x0015D3F4 File Offset: 0x0015D3F4
		private X509CertificatePair ReadDerCrossCertificatePair(Stream inStream)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(inStream);
			Asn1Sequence obj = (Asn1Sequence)asn1InputStream.ReadObject();
			CertificatePair instance = CertificatePair.GetInstance(obj);
			return new X509CertificatePair(instance);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0015D428 File Offset: 0x0015D428
		public X509CertificatePair ReadCertPair(byte[] input)
		{
			return this.ReadCertPair(new MemoryStream(input, false));
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0015D438 File Offset: 0x0015D438
		public ICollection ReadCertPairs(byte[] input)
		{
			return this.ReadCertPairs(new MemoryStream(input, false));
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0015D448 File Offset: 0x0015D448
		public X509CertificatePair ReadCertPair(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentStream == null)
			{
				this.currentStream = inStream;
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
			}
			X509CertificatePair result;
			try
			{
				PushbackStream pushbackStream = new PushbackStream(inStream);
				int num = pushbackStream.ReadByte();
				if (num < 0)
				{
					result = null;
				}
				else
				{
					pushbackStream.Unread(num);
					result = this.ReadDerCrossCertificatePair(pushbackStream);
				}
			}
			catch (Exception ex)
			{
				throw new CertificateException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0015D4FC File Offset: 0x0015D4FC
		public ICollection ReadCertPairs(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509CertificatePair value;
			while ((value = this.ReadCertPair(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040020AD RID: 8365
		private Stream currentStream;
	}
}
