using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000633 RID: 1587
	public class BasicOcspRespGenerator
	{
		// Token: 0x06003758 RID: 14168 RVA: 0x001294D4 File Offset: 0x001294D4
		public BasicOcspRespGenerator(RespID responderID)
		{
			this.responderID = responderID;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x001294F0 File Offset: 0x001294F0
		public BasicOcspRespGenerator(AsymmetricKeyParameter publicKey)
		{
			this.responderID = new RespID(publicKey);
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x00129510 File Offset: 0x00129510
		public void AddResponse(CertificateID certID, CertificateStatus certStatus)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, null));
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x0012952C File Offset: 0x0012952C
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, singleExtensions));
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x00129548 File Offset: 0x00129548
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, nextUpdate, singleExtensions));
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x00129568 File Offset: 0x00129568
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, thisUpdate, nextUpdate, singleExtensions));
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x00129584 File Offset: 0x00129584
		public void SetResponseExtensions(X509Extensions responseExtensions)
		{
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x00129590 File Offset: 0x00129590
		private BasicOcspResp GenerateResponse(ISignatureFactory signatureCalculator, X509Certificate[] chain, DateTime producedAt)
		{
			AlgorithmIdentifier algorithmIdentifier = (AlgorithmIdentifier)signatureCalculator.AlgorithmDetails;
			DerObjectIdentifier algorithm = algorithmIdentifier.Algorithm;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.list)
			{
				BasicOcspRespGenerator.ResponseObject responseObject = (BasicOcspRespGenerator.ResponseObject)obj;
				try
				{
					asn1EncodableVector.Add(responseObject.ToResponse());
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			ResponseData responseData = new ResponseData(this.responderID.ToAsn1Object(), new DerGeneralizedTime(producedAt), new DerSequence(asn1EncodableVector), this.responseExtensions);
			DerBitString signature = null;
			try
			{
				IStreamCalculator streamCalculator = signatureCalculator.CreateCalculator();
				byte[] derEncoded = responseData.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
				Platform.Dispose(streamCalculator.Stream);
				signature = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing TBSRequest: " + ex, ex);
			}
			AlgorithmIdentifier sigAlgID = OcspUtilities.GetSigAlgID(algorithm);
			DerSequence certs = null;
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
				certs = new DerSequence(asn1EncodableVector2);
			}
			return new BasicOcspResp(new BasicOcspResponse(responseData, sigAlgID, signature, certs));
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x00129778 File Offset: 0x00129778
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime thisUpdate)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, thisUpdate, null);
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x00129788 File Offset: 0x00129788
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime producedAt, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			return this.GenerateResponse(new Asn1SignatureFactory(signingAlgorithm, privateKey, random), chain, producedAt);
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x001297B0 File Offset: 0x001297B0
		public BasicOcspResp Generate(ISignatureFactory signatureCalculatorFactory, X509Certificate[] chain, DateTime producedAt)
		{
			if (signatureCalculatorFactory == null)
			{
				throw new ArgumentException("no signature calculator specified");
			}
			return this.GenerateResponse(signatureCalculatorFactory, chain, producedAt);
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x001297CC File Offset: 0x001297CC
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x04001D51 RID: 7505
		private readonly IList list = Platform.CreateArrayList();

		// Token: 0x04001D52 RID: 7506
		private X509Extensions responseExtensions;

		// Token: 0x04001D53 RID: 7507
		private RespID responderID;

		// Token: 0x02000E5B RID: 3675
		private class ResponseObject
		{
			// Token: 0x06008D42 RID: 36162 RVA: 0x002A5F38 File Offset: 0x002A5F38
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), null, extensions)
			{
			}

			// Token: 0x06008D43 RID: 36163 RVA: 0x002A5F4C File Offset: 0x002A5F4C
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), new DerGeneralizedTime(nextUpdate), extensions)
			{
			}

			// Token: 0x06008D44 RID: 36164 RVA: 0x002A5F68 File Offset: 0x002A5F68
			private ResponseObject(CertificateID certId, CertificateStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions extensions)
			{
				this.certId = certId;
				if (certStatus == null)
				{
					this.certStatus = new CertStatus();
				}
				else if (certStatus is UnknownStatus)
				{
					this.certStatus = new CertStatus(2, DerNull.Instance);
				}
				else
				{
					RevokedStatus revokedStatus = (RevokedStatus)certStatus;
					CrlReason revocationReason = revokedStatus.HasRevocationReason ? new CrlReason(revokedStatus.RevocationReason) : null;
					this.certStatus = new CertStatus(new RevokedInfo(new DerGeneralizedTime(revokedStatus.RevocationTime), revocationReason));
				}
				this.thisUpdate = thisUpdate;
				this.nextUpdate = nextUpdate;
				this.extensions = extensions;
			}

			// Token: 0x06008D45 RID: 36165 RVA: 0x002A6014 File Offset: 0x002A6014
			public SingleResponse ToResponse()
			{
				return new SingleResponse(this.certId.ToAsn1Object(), this.certStatus, this.thisUpdate, this.nextUpdate, this.extensions);
			}

			// Token: 0x0400422D RID: 16941
			internal CertificateID certId;

			// Token: 0x0400422E RID: 16942
			internal CertStatus certStatus;

			// Token: 0x0400422F RID: 16943
			internal DerGeneralizedTime thisUpdate;

			// Token: 0x04004230 RID: 16944
			internal DerGeneralizedTime nextUpdate;

			// Token: 0x04004231 RID: 16945
			internal X509Extensions extensions;
		}
	}
}
