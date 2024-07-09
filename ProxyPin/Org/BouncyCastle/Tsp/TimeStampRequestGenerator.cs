using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BC RID: 1724
	public class TimeStampRequestGenerator
	{
		// Token: 0x06003C58 RID: 15448 RVA: 0x0014DADC File Offset: 0x0014DADC
		public void SetReqPolicy(string reqPolicy)
		{
			this.reqPolicy = new DerObjectIdentifier(reqPolicy);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x0014DAEC File Offset: 0x0014DAEC
		public void SetCertReq(bool certReq)
		{
			this.certReq = DerBoolean.GetInstance(certReq);
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x0014DAFC File Offset: 0x0014DAFC
		[Obsolete("Use method taking DerObjectIdentifier")]
		public void AddExtension(string oid, bool critical, Asn1Encodable value)
		{
			this.AddExtension(oid, critical, value.GetEncoded());
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x0014DB0C File Offset: 0x0014DB0C
		[Obsolete("Use method taking DerObjectIdentifier")]
		public void AddExtension(string oid, bool critical, byte[] value)
		{
			DerObjectIdentifier derObjectIdentifier = new DerObjectIdentifier(oid);
			this.extensions[derObjectIdentifier] = new X509Extension(critical, new DerOctetString(value));
			this.extOrdering.Add(derObjectIdentifier);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x0014DB4C File Offset: 0x0014DB4C
		public virtual void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extValue)
		{
			this.AddExtension(oid, critical, extValue.GetEncoded());
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x0014DB5C File Offset: 0x0014DB5C
		public virtual void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extValue)
		{
			this.extensions.Add(oid, new X509Extension(critical, new DerOctetString(extValue)));
			this.extOrdering.Add(oid);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x0014DB84 File Offset: 0x0014DB84
		public TimeStampRequest Generate(string digestAlgorithm, byte[] digest)
		{
			return this.Generate(digestAlgorithm, digest, null);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x0014DB90 File Offset: 0x0014DB90
		public TimeStampRequest Generate(string digestAlgorithmOid, byte[] digest, BigInteger nonce)
		{
			if (digestAlgorithmOid == null)
			{
				throw new ArgumentException("No digest algorithm specified");
			}
			DerObjectIdentifier algorithm = new DerObjectIdentifier(digestAlgorithmOid);
			AlgorithmIdentifier hashAlgorithm = new AlgorithmIdentifier(algorithm, DerNull.Instance);
			MessageImprint messageImprint = new MessageImprint(hashAlgorithm, digest);
			X509Extensions x509Extensions = null;
			if (this.extOrdering.Count != 0)
			{
				x509Extensions = new X509Extensions(this.extOrdering, this.extensions);
			}
			DerInteger nonce2 = (nonce == null) ? null : new DerInteger(nonce);
			return new TimeStampRequest(new TimeStampReq(messageImprint, this.reqPolicy, nonce2, this.certReq, x509Extensions));
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x0014DC20 File Offset: 0x0014DC20
		public virtual TimeStampRequest Generate(DerObjectIdentifier digestAlgorithm, byte[] digest)
		{
			return this.Generate(digestAlgorithm.Id, digest);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x0014DC30 File Offset: 0x0014DC30
		public virtual TimeStampRequest Generate(DerObjectIdentifier digestAlgorithm, byte[] digest, BigInteger nonce)
		{
			return this.Generate(digestAlgorithm.Id, digest, nonce);
		}

		// Token: 0x04001EAC RID: 7852
		private DerObjectIdentifier reqPolicy;

		// Token: 0x04001EAD RID: 7853
		private DerBoolean certReq;

		// Token: 0x04001EAE RID: 7854
		private IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x04001EAF RID: 7855
		private IList extOrdering = Platform.CreateArrayList();
	}
}
