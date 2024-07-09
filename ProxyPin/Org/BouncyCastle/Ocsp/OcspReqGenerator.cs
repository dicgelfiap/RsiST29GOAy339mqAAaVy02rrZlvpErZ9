using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000638 RID: 1592
	public class OcspReqGenerator
	{
		// Token: 0x06003787 RID: 14215 RVA: 0x00129E1C File Offset: 0x00129E1C
		public void AddRequest(CertificateID certId)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, null));
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x00129E34 File Offset: 0x00129E34
		public void AddRequest(CertificateID certId, X509Extensions singleRequestExtensions)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, singleRequestExtensions));
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x00129E4C File Offset: 0x00129E4C
		public void SetRequestorName(X509Name requestorName)
		{
			try
			{
				this.requestorName = new GeneralName(4, requestorName);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("cannot encode principal", innerException);
			}
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x00129E88 File Offset: 0x00129E88
		public void SetRequestorName(GeneralName requestorName)
		{
			this.requestorName = requestorName;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x00129E94 File Offset: 0x00129E94
		public void SetRequestExtensions(X509Extensions requestExtensions)
		{
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x00129EA0 File Offset: 0x00129EA0
		private OcspReq GenerateRequest(DerObjectIdentifier signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.list)
			{
				OcspReqGenerator.RequestObject requestObject = (OcspReqGenerator.RequestObject)obj;
				try
				{
					asn1EncodableVector.Add(requestObject.ToRequest());
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			TbsRequest tbsRequest = new TbsRequest(this.requestorName, new DerSequence(asn1EncodableVector), this.requestExtensions);
			ISigner signer = null;
			Signature optionalSignature = null;
			if (signingAlgorithm != null)
			{
				if (this.requestorName == null)
				{
					throw new OcspException("requestorName must be specified if request is signed.");
				}
				try
				{
					signer = SignerUtilities.GetSigner(signingAlgorithm.Id);
					if (random != null)
					{
						signer.Init(true, new ParametersWithRandom(privateKey, random));
					}
					else
					{
						signer.Init(true, privateKey);
					}
				}
				catch (Exception ex)
				{
					throw new OcspException("exception creating signature: " + ex, ex);
				}
				DerBitString signatureValue = null;
				try
				{
					byte[] encoded = tbsRequest.GetEncoded();
					signer.BlockUpdate(encoded, 0, encoded.Length);
					signatureValue = new DerBitString(signer.GenerateSignature());
				}
				catch (Exception ex2)
				{
					throw new OcspException("exception processing TBSRequest: " + ex2, ex2);
				}
				AlgorithmIdentifier signatureAlgorithm = new AlgorithmIdentifier(signingAlgorithm, DerNull.Instance);
				if (chain != null && chain.Length > 0)
				{
					Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
					try
					{
						for (int num = 0; num != chain.Length; num++)
						{
							asn1EncodableVector2.Add(X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(chain[num].GetEncoded())));
						}
					}
					catch (IOException e2)
					{
						throw new OcspException("error processing certs", e2);
					}
					catch (CertificateEncodingException e3)
					{
						throw new OcspException("error encoding certs", e3);
					}
					optionalSignature = new Signature(signatureAlgorithm, signatureValue, new DerSequence(asn1EncodableVector2));
				}
				else
				{
					optionalSignature = new Signature(signatureAlgorithm, signatureValue);
				}
			}
			return new OcspReq(new OcspRequest(tbsRequest, optionalSignature));
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x0012A0D4 File Offset: 0x0012A0D4
		public OcspReq Generate()
		{
			return this.GenerateRequest(null, null, null, null);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0012A0E0 File Offset: 0x0012A0E0
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, null);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0012A0EC File Offset: 0x0012A0EC
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			OcspReq result;
			try
			{
				DerObjectIdentifier algorithmOid = OcspUtilities.GetAlgorithmOid(signingAlgorithm);
				result = this.GenerateRequest(algorithmOid, privateKey, chain, random);
			}
			catch (ArgumentException)
			{
				throw new ArgumentException("unknown signing algorithm specified: " + signingAlgorithm);
			}
			return result;
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x0012A148 File Offset: 0x0012A148
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x04001D58 RID: 7512
		private IList list = Platform.CreateArrayList();

		// Token: 0x04001D59 RID: 7513
		private GeneralName requestorName = null;

		// Token: 0x04001D5A RID: 7514
		private X509Extensions requestExtensions = null;

		// Token: 0x02000E5C RID: 3676
		private class RequestObject
		{
			// Token: 0x06008D46 RID: 36166 RVA: 0x002A6040 File Offset: 0x002A6040
			public RequestObject(CertificateID certId, X509Extensions extensions)
			{
				this.certId = certId;
				this.extensions = extensions;
			}

			// Token: 0x06008D47 RID: 36167 RVA: 0x002A6058 File Offset: 0x002A6058
			public Request ToRequest()
			{
				return new Request(this.certId.ToAsn1Object(), this.extensions);
			}

			// Token: 0x04004232 RID: 16946
			internal CertificateID certId;

			// Token: 0x04004233 RID: 16947
			internal X509Extensions extensions;
		}
	}
}
