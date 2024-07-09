using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200071A RID: 1818
	public class X509CertificateParser
	{
		// Token: 0x06003FAC RID: 16300 RVA: 0x0015D12C File Offset: 0x0015D12C
		private X509Certificate ReadDerCertificate(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Certificates;
				return this.GetCertificate();
			}
			return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0015D1B4 File Offset: 0x0015D1B4
		private X509Certificate GetCertificate()
		{
			if (this.sData != null)
			{
				while (this.sDataObjectCount < this.sData.Count)
				{
					object obj = this.sData[this.sDataObjectCount++];
					if (obj is Asn1Sequence)
					{
						return this.CreateX509Certificate(X509CertificateStructure.GetInstance(obj));
					}
				}
			}
			return null;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0015D220 File Offset: 0x0015D220
		private X509Certificate ReadPemCertificate(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CertificateParser.PemCertParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0015D254 File Offset: 0x0015D254
		protected virtual X509Certificate CreateX509Certificate(X509CertificateStructure c)
		{
			return new X509Certificate(c);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0015D25C File Offset: 0x0015D25C
		public X509Certificate ReadCertificate(byte[] input)
		{
			return this.ReadCertificate(new MemoryStream(input, false));
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0015D26C File Offset: 0x0015D26C
		public ICollection ReadCertificates(byte[] input)
		{
			return this.ReadCertificates(new MemoryStream(input, false));
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0015D27C File Offset: 0x0015D27C
		public X509Certificate ReadCertificate(Stream inStream)
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
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			X509Certificate result;
			try
			{
				if (this.sData != null)
				{
					if (this.sDataObjectCount != this.sData.Count)
					{
						result = this.GetCertificate();
					}
					else
					{
						this.sData = null;
						this.sDataObjectCount = 0;
						result = null;
					}
				}
				else
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
						if (num != 48)
						{
							result = this.ReadPemCertificate(pushbackStream);
						}
						else
						{
							result = this.ReadDerCertificate(new Asn1InputStream(pushbackStream));
						}
					}
				}
			}
			catch (Exception exception)
			{
				throw new CertificateException("Failed to read certificate", exception);
			}
			return result;
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0015D3A8 File Offset: 0x0015D3A8
		public ICollection ReadCertificates(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509Certificate value;
			while ((value = this.ReadCertificate(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040020A9 RID: 8361
		private static readonly PemParser PemCertParser = new PemParser("CERTIFICATE");

		// Token: 0x040020AA RID: 8362
		private Asn1Set sData;

		// Token: 0x040020AB RID: 8363
		private int sDataObjectCount;

		// Token: 0x040020AC RID: 8364
		private Stream currentStream;
	}
}
