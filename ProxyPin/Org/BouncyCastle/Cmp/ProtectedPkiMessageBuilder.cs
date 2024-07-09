using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CD RID: 717
	public class ProtectedPkiMessageBuilder
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x00072CD0 File Offset: 0x00072CD0
		public ProtectedPkiMessageBuilder(GeneralName sender, GeneralName recipient) : this(PkiHeader.CMP_2000, sender, recipient)
		{
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00072CE0 File Offset: 0x00072CE0
		public ProtectedPkiMessageBuilder(int pvno, GeneralName sender, GeneralName recipient)
		{
			this.hdrBuilBuilder = new PkiHeaderBuilder(pvno, sender, recipient);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00072D0C File Offset: 0x00072D0C
		public ProtectedPkiMessageBuilder SetTransactionId(byte[] tid)
		{
			this.hdrBuilBuilder.SetTransactionID(tid);
			return this;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00072D1C File Offset: 0x00072D1C
		public ProtectedPkiMessageBuilder SetFreeText(PkiFreeText freeText)
		{
			this.hdrBuilBuilder.SetFreeText(freeText);
			return this;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00072D2C File Offset: 0x00072D2C
		public ProtectedPkiMessageBuilder AddGeneralInfo(InfoTypeAndValue genInfo)
		{
			this.generalInfos.Add(genInfo);
			return this;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00072D3C File Offset: 0x00072D3C
		public ProtectedPkiMessageBuilder SetMessageTime(DerGeneralizedTime generalizedTime)
		{
			this.hdrBuilBuilder.SetMessageTime(generalizedTime);
			return this;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00072D4C File Offset: 0x00072D4C
		public ProtectedPkiMessageBuilder SetRecipKID(byte[] id)
		{
			this.hdrBuilBuilder.SetRecipKID(id);
			return this;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00072D5C File Offset: 0x00072D5C
		public ProtectedPkiMessageBuilder SetRecipNonce(byte[] nonce)
		{
			this.hdrBuilBuilder.SetRecipNonce(nonce);
			return this;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00072D6C File Offset: 0x00072D6C
		public ProtectedPkiMessageBuilder SetSenderKID(byte[] id)
		{
			this.hdrBuilBuilder.SetSenderKID(id);
			return this;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00072D7C File Offset: 0x00072D7C
		public ProtectedPkiMessageBuilder SetSenderNonce(byte[] nonce)
		{
			this.hdrBuilBuilder.SetSenderNonce(nonce);
			return this;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00072D8C File Offset: 0x00072D8C
		public ProtectedPkiMessageBuilder SetBody(PkiBody body)
		{
			this.body = body;
			return this;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00072D98 File Offset: 0x00072D98
		public ProtectedPkiMessageBuilder AddCmpCertificate(X509Certificate certificate)
		{
			this.extraCerts.Add(certificate);
			return this;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00072DA8 File Offset: 0x00072DA8
		public ProtectedPkiMessage Build(ISignatureFactory signatureFactory)
		{
			if (this.body == null)
			{
				throw new InvalidOperationException("body must be set before building");
			}
			IStreamCalculator signer = signatureFactory.CreateCalculator();
			if (!(signatureFactory.AlgorithmDetails is AlgorithmIdentifier))
			{
				throw new ArgumentException("AlgorithmDetails is not AlgorithmIdentifier");
			}
			this.FinalizeHeader((AlgorithmIdentifier)signatureFactory.AlgorithmDetails);
			PkiHeader header = this.hdrBuilBuilder.Build();
			DerBitString protection = new DerBitString(this.CalculateSignature(signer, header, this.body));
			return this.FinalizeMessage(header, protection);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00072E2C File Offset: 0x00072E2C
		public ProtectedPkiMessage Build(IMacFactory factory)
		{
			if (this.body == null)
			{
				throw new InvalidOperationException("body must be set before building");
			}
			IStreamCalculator signer = factory.CreateCalculator();
			this.FinalizeHeader((AlgorithmIdentifier)factory.AlgorithmDetails);
			PkiHeader header = this.hdrBuilBuilder.Build();
			DerBitString protection = new DerBitString(this.CalculateSignature(signer, header, this.body));
			return this.FinalizeMessage(header, protection);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00072E94 File Offset: 0x00072E94
		private void FinalizeHeader(AlgorithmIdentifier algorithmIdentifier)
		{
			this.hdrBuilBuilder.SetProtectionAlg(algorithmIdentifier);
			if (this.generalInfos.Count > 0)
			{
				InfoTypeAndValue[] array = new InfoTypeAndValue[this.generalInfos.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (InfoTypeAndValue)this.generalInfos[i];
				}
				this.hdrBuilBuilder.SetGeneralInfo(array);
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00072F0C File Offset: 0x00072F0C
		private ProtectedPkiMessage FinalizeMessage(PkiHeader header, DerBitString protection)
		{
			if (this.extraCerts.Count > 0)
			{
				CmpCertificate[] array = new CmpCertificate[this.extraCerts.Count];
				for (int i = 0; i < array.Length; i++)
				{
					byte[] encoded = ((X509Certificate)this.extraCerts[i]).GetEncoded();
					array[i] = CmpCertificate.GetInstance(Asn1Object.FromByteArray(encoded));
				}
				return new ProtectedPkiMessage(new PkiMessage(header, this.body, protection, array));
			}
			return new ProtectedPkiMessage(new PkiMessage(header, this.body, protection));
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00072FA0 File Offset: 0x00072FA0
		private byte[] CalculateSignature(IStreamCalculator signer, PkiHeader header, PkiBody body)
		{
			byte[] encoded = new DerSequence(new Asn1EncodableVector
			{
				header,
				body
			}).GetEncoded();
			signer.Stream.Write(encoded, 0, encoded.Length);
			object result = signer.GetResult();
			if (result is DefaultSignatureResult)
			{
				return ((DefaultSignatureResult)result).Collect();
			}
			if (result is IBlockResult)
			{
				return ((IBlockResult)result).Collect();
			}
			if (result is byte[])
			{
				return (byte[])result;
			}
			throw new InvalidOperationException("result is not byte[] or DefaultSignatureResult");
		}

		// Token: 0x04000EE7 RID: 3815
		private PkiHeaderBuilder hdrBuilBuilder;

		// Token: 0x04000EE8 RID: 3816
		private PkiBody body;

		// Token: 0x04000EE9 RID: 3817
		private IList generalInfos = Platform.CreateArrayList();

		// Token: 0x04000EEA RID: 3818
		private IList extraCerts = Platform.CreateArrayList();
	}
}
