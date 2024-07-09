using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crmf;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CC RID: 716
	public class ProtectedPkiMessage
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x00072A78 File Offset: 0x00072A78
		public ProtectedPkiMessage(GeneralPkiMessage pkiMessage)
		{
			if (!pkiMessage.HasProtection)
			{
				throw new ArgumentException("pki message not protected");
			}
			this.pkiMessage = pkiMessage.ToAsn1Structure();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00072AA4 File Offset: 0x00072AA4
		public ProtectedPkiMessage(PkiMessage pkiMessage)
		{
			if (pkiMessage.Header.ProtectionAlg == null)
			{
				throw new ArgumentException("pki message not protected");
			}
			this.pkiMessage = pkiMessage;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00072AD0 File Offset: 0x00072AD0
		public PkiHeader Header
		{
			get
			{
				return this.pkiMessage.Header;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00072AE0 File Offset: 0x00072AE0
		public PkiBody Body
		{
			get
			{
				return this.pkiMessage.Body;
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00072AF0 File Offset: 0x00072AF0
		public PkiMessage ToAsn1Message()
		{
			return this.pkiMessage;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00072AF8 File Offset: 0x00072AF8
		public bool HasPasswordBasedMacProtected
		{
			get
			{
				return this.Header.ProtectionAlg.Algorithm.Equals(CmpObjectIdentifiers.passwordBasedMac);
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00072B24 File Offset: 0x00072B24
		public X509Certificate[] GetCertificates()
		{
			CmpCertificate[] extraCerts = this.pkiMessage.GetExtraCerts();
			if (extraCerts == null)
			{
				return new X509Certificate[0];
			}
			X509Certificate[] array = new X509Certificate[extraCerts.Length];
			for (int i = 0; i < extraCerts.Length; i++)
			{
				array[i] = new X509Certificate(X509CertificateStructure.GetInstance(extraCerts[i].GetEncoded()));
			}
			return array;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00072B88 File Offset: 0x00072B88
		public bool Verify(IVerifierFactory verifierFactory)
		{
			IStreamCalculator streamCalculator = verifierFactory.CreateCalculator();
			IVerifier verifier = (IVerifier)this.Process(streamCalculator);
			return verifier.IsVerified(this.pkiMessage.Protection.GetBytes());
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00072BC4 File Offset: 0x00072BC4
		private object Process(IStreamCalculator streamCalculator)
		{
			byte[] derEncoded = new DerSequence(new Asn1EncodableVector
			{
				this.pkiMessage.Header,
				this.pkiMessage.Body
			}).GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			streamCalculator.Stream.Flush();
			Platform.Dispose(streamCalculator.Stream);
			return streamCalculator.GetResult();
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00072C38 File Offset: 0x00072C38
		public bool Verify(PKMacBuilder pkMacBuilder, char[] password)
		{
			if (!CmpObjectIdentifiers.passwordBasedMac.Equals(this.pkiMessage.Header.ProtectionAlg.Algorithm))
			{
				throw new InvalidOperationException("protection algorithm is not mac based");
			}
			PbmParameter instance = PbmParameter.GetInstance(this.pkiMessage.Header.ProtectionAlg.Parameters);
			pkMacBuilder.SetParameters(instance);
			IBlockResult blockResult = (IBlockResult)this.Process(pkMacBuilder.Build(password).CreateCalculator());
			return Arrays.ConstantTimeAreEqual(blockResult.Collect(), this.pkiMessage.Protection.GetBytes());
		}

		// Token: 0x04000EE6 RID: 3814
		private readonly PkiMessage pkiMessage;
	}
}
