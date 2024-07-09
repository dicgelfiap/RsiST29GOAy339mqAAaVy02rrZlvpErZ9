using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CF RID: 719
	public class RevocationDetailsBuilder
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x0007308C File Offset: 0x0007308C
		public RevocationDetailsBuilder SetPublicKey(SubjectPublicKeyInfo publicKey)
		{
			if (publicKey != null)
			{
				this._templateBuilder.SetPublicKey(publicKey);
			}
			return this;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000730A4 File Offset: 0x000730A4
		public RevocationDetailsBuilder SetIssuer(X509Name issuer)
		{
			if (issuer != null)
			{
				this._templateBuilder.SetIssuer(issuer);
			}
			return this;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000730BC File Offset: 0x000730BC
		public RevocationDetailsBuilder SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber != null)
			{
				this._templateBuilder.SetSerialNumber(new DerInteger(serialNumber));
			}
			return this;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000730D8 File Offset: 0x000730D8
		public RevocationDetailsBuilder SetSubject(X509Name subject)
		{
			if (subject != null)
			{
				this._templateBuilder.SetSubject(subject);
			}
			return this;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000730F0 File Offset: 0x000730F0
		public RevocationDetails Build()
		{
			return new RevocationDetails(new RevDetails(this._templateBuilder.Build()));
		}

		// Token: 0x04000EEC RID: 3820
		private readonly CertTemplateBuilder _templateBuilder = new CertTemplateBuilder();
	}
}
