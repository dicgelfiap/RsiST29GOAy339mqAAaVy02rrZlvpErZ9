using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000637 RID: 1591
	public class OcspReq : X509ExtensionBase
	{
		// Token: 0x06003775 RID: 14197 RVA: 0x00129A18 File Offset: 0x00129A18
		public OcspReq(OcspRequest req)
		{
			this.req = req;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x00129A28 File Offset: 0x00129A28
		public OcspReq(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x00129A38 File Offset: 0x00129A38
		public OcspReq(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x00129A48 File Offset: 0x00129A48
		private OcspReq(Asn1InputStream aIn)
		{
			try
			{
				this.req = OcspRequest.GetInstance(aIn.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new IOException("malformed request: " + ex.Message);
			}
			catch (InvalidCastException ex2)
			{
				throw new IOException("malformed request: " + ex2.Message);
			}
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x00129ABC File Offset: 0x00129ABC
		public byte[] GetTbsRequest()
		{
			byte[] encoded;
			try
			{
				encoded = this.req.TbsRequest.GetEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsRequest", e);
			}
			return encoded;
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x00129B00 File Offset: 0x00129B00
		public int Version
		{
			get
			{
				return this.req.TbsRequest.Version.IntValueExact + 1;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x00129B1C File Offset: 0x00129B1C
		public GeneralName RequestorName
		{
			get
			{
				return GeneralName.GetInstance(this.req.TbsRequest.RequestorName);
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x00129B34 File Offset: 0x00129B34
		public Req[] GetRequestList()
		{
			Asn1Sequence requestList = this.req.TbsRequest.RequestList;
			Req[] array = new Req[requestList.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new Req(Request.GetInstance(requestList[num]));
			}
			return array;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x00129B8C File Offset: 0x00129B8C
		public X509Extensions RequestExtensions
		{
			get
			{
				return X509Extensions.GetInstance(this.req.TbsRequest.RequestExtensions);
			}
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x00129BA4 File Offset: 0x00129BA4
		protected override X509Extensions GetX509Extensions()
		{
			return this.RequestExtensions;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x00129BAC File Offset: 0x00129BAC
		public string SignatureAlgOid
		{
			get
			{
				if (!this.IsSigned)
				{
					return null;
				}
				return this.req.OptionalSignature.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x00129BE4 File Offset: 0x00129BE4
		public byte[] GetSignature()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			return this.req.OptionalSignature.GetSignatureOctets();
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x00129C04 File Offset: 0x00129C04
		private IList GetCertList()
		{
			IList list = Platform.CreateArrayList();
			Asn1Sequence certs = this.req.OptionalSignature.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						list.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (Exception e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
				}
			}
			return list;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x00129CB4 File Offset: 0x00129CB4
		public X509Certificate[] GetCerts()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			IList certList = this.GetCertList();
			X509Certificate[] array = new X509Certificate[certList.Count];
			for (int i = 0; i < certList.Count; i++)
			{
				array[i] = (X509Certificate)certList[i];
			}
			return array;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x00129D10 File Offset: 0x00129D10
		public IX509Store GetCertificates(string type)
		{
			if (!this.IsSigned)
			{
				return null;
			}
			IX509Store result;
			try
			{
				result = X509StoreFactory.Create("Certificate/" + type, new X509CollectionStoreParameters(this.GetCertList()));
			}
			catch (Exception e)
			{
				throw new OcspException("can't setup the CertStore", e);
			}
			return result;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x00129D6C File Offset: 0x00129D6C
		public bool IsSigned
		{
			get
			{
				return this.req.OptionalSignature != null;
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x00129D80 File Offset: 0x00129D80
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			if (!this.IsSigned)
			{
				throw new OcspException("attempt to Verify signature on unsigned object");
			}
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgOid);
				signer.Init(false, publicKey);
				byte[] encoded = this.req.TbsRequest.GetEncoded();
				signer.BlockUpdate(encoded, 0, encoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x00129E0C File Offset: 0x00129E0C
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x04001D57 RID: 7511
		private OcspRequest req;
	}
}
