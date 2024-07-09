using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200021B RID: 539
	public class V2TbsCertListGenerator
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00062D30 File Offset: 0x00062D30
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00062D3C File Offset: 0x00062D3C
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00062D48 File Offset: 0x00062D48
		public void SetThisUpdate(DerUtcTime thisUpdate)
		{
			this.thisUpdate = new Time(thisUpdate);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00062D58 File Offset: 0x00062D58
		public void SetNextUpdate(DerUtcTime nextUpdate)
		{
			this.nextUpdate = ((nextUpdate != null) ? new Time(nextUpdate) : null);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00062D74 File Offset: 0x00062D74
		public void SetThisUpdate(Time thisUpdate)
		{
			this.thisUpdate = thisUpdate;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00062D80 File Offset: 0x00062D80
		public void SetNextUpdate(Time nextUpdate)
		{
			this.nextUpdate = nextUpdate;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00062D8C File Offset: 0x00062D8C
		public void AddCrlEntry(Asn1Sequence crlEntry)
		{
			if (this.crlEntries == null)
			{
				this.crlEntries = Platform.CreateArrayList();
			}
			this.crlEntries.Add(crlEntry);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00062DB4 File Offset: 0x00062DB4
		public void AddCrlEntry(DerInteger userCertificate, DerUtcTime revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, new Time(revocationDate), reason);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00062DC4 File Offset: 0x00062DC4
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, revocationDate, reason, null);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00062DD0 File Offset: 0x00062DD0
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason, DerGeneralizedTime invalidityDate)
		{
			IList list = Platform.CreateArrayList();
			IList list2 = Platform.CreateArrayList();
			if (reason != 0)
			{
				CrlReason crlReason = new CrlReason(reason);
				try
				{
					list.Add(X509Extensions.ReasonCode);
					list2.Add(new X509Extension(false, new DerOctetString(crlReason.GetEncoded())));
				}
				catch (IOException arg)
				{
					throw new ArgumentException("error encoding reason: " + arg);
				}
			}
			if (invalidityDate != null)
			{
				try
				{
					list.Add(X509Extensions.InvalidityDate);
					list2.Add(new X509Extension(false, new DerOctetString(invalidityDate.GetEncoded())));
				}
				catch (IOException arg2)
				{
					throw new ArgumentException("error encoding invalidityDate: " + arg2);
				}
			}
			if (list.Count != 0)
			{
				this.AddCrlEntry(userCertificate, revocationDate, new X509Extensions(list, list2));
				return;
			}
			this.AddCrlEntry(userCertificate, revocationDate, null);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00062EB8 File Offset: 0x00062EB8
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, X509Extensions extensions)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				userCertificate,
				revocationDate
			});
			if (extensions != null)
			{
				asn1EncodableVector.Add(extensions);
			}
			this.AddCrlEntry(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00062F00 File Offset: 0x00062F00
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00062F0C File Offset: 0x00062F0C
		public TbsCertificateList GenerateTbsCertList()
		{
			if (this.signature == null || this.issuer == null || this.thisUpdate == null)
			{
				throw new InvalidOperationException("Not all mandatory fields set in V2 TbsCertList generator.");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.signature,
				this.issuer,
				this.thisUpdate
			});
			if (this.nextUpdate != null)
			{
				asn1EncodableVector.Add(this.nextUpdate);
			}
			if (this.crlEntries != null)
			{
				Asn1Sequence[] array = new Asn1Sequence[this.crlEntries.Count];
				for (int i = 0; i < this.crlEntries.Count; i++)
				{
					array[i] = (Asn1Sequence)this.crlEntries[i];
				}
				asn1EncodableVector.Add(new DerSequence(array));
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(0, this.extensions));
			}
			return new TbsCertificateList(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04000C71 RID: 3185
		private DerInteger version = new DerInteger(1);

		// Token: 0x04000C72 RID: 3186
		private AlgorithmIdentifier signature;

		// Token: 0x04000C73 RID: 3187
		private X509Name issuer;

		// Token: 0x04000C74 RID: 3188
		private Time thisUpdate;

		// Token: 0x04000C75 RID: 3189
		private Time nextUpdate;

		// Token: 0x04000C76 RID: 3190
		private X509Extensions extensions;

		// Token: 0x04000C77 RID: 3191
		private IList crlEntries;
	}
}
