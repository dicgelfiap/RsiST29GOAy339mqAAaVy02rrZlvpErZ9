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
	// Token: 0x0200071E RID: 1822
	public class X509CrlParser
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x0015E284 File Offset: 0x0015E284
		public X509CrlParser() : this(false)
		{
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x0015E290 File Offset: 0x0015E290
		public X509CrlParser(bool lazyAsn1)
		{
			this.lazyAsn1 = lazyAsn1;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x0015E2A0 File Offset: 0x0015E2A0
		private X509Crl ReadPemCrl(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CrlParser.PemCrlParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0015E2D4 File Offset: 0x0015E2D4
		private X509Crl ReadDerCrl(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sCrlData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Crls;
				return this.GetCrl();
			}
			return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0015E35C File Offset: 0x0015E35C
		private X509Crl GetCrl()
		{
			if (this.sCrlData == null || this.sCrlDataObjectCount >= this.sCrlData.Count)
			{
				return null;
			}
			return this.CreateX509Crl(CertificateList.GetInstance(this.sCrlData[this.sCrlDataObjectCount++]));
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0015E3B8 File Offset: 0x0015E3B8
		protected virtual X509Crl CreateX509Crl(CertificateList c)
		{
			return new X509Crl(c);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0015E3C0 File Offset: 0x0015E3C0
		public X509Crl ReadCrl(byte[] input)
		{
			return this.ReadCrl(new MemoryStream(input, false));
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0015E3D0 File Offset: 0x0015E3D0
		public ICollection ReadCrls(byte[] input)
		{
			return this.ReadCrls(new MemoryStream(input, false));
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0015E3E0 File Offset: 0x0015E3E0
		public X509Crl ReadCrl(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentCrlStream == null)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			else if (this.currentCrlStream != inStream)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			X509Crl result;
			try
			{
				if (this.sCrlData != null)
				{
					if (this.sCrlDataObjectCount != this.sCrlData.Count)
					{
						result = this.GetCrl();
					}
					else
					{
						this.sCrlData = null;
						this.sCrlDataObjectCount = 0;
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
							result = this.ReadPemCrl(pushbackStream);
						}
						else
						{
							Asn1InputStream dIn = this.lazyAsn1 ? new LazyAsn1InputStream(pushbackStream) : new Asn1InputStream(pushbackStream);
							result = this.ReadDerCrl(dIn);
						}
					}
				}
			}
			catch (CrlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new CrlException(ex2.ToString());
			}
			return result;
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0015E534 File Offset: 0x0015E534
		public ICollection ReadCrls(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509Crl value;
			while ((value = this.ReadCrl(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040020BA RID: 8378
		private static readonly PemParser PemCrlParser = new PemParser("CRL");

		// Token: 0x040020BB RID: 8379
		private readonly bool lazyAsn1;

		// Token: 0x040020BC RID: 8380
		private Asn1Set sCrlData;

		// Token: 0x040020BD RID: 8381
		private int sCrlDataObjectCount;

		// Token: 0x040020BE RID: 8382
		private Stream currentCrlStream;
	}
}
