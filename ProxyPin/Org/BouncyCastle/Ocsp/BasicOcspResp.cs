using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000632 RID: 1586
	public class BasicOcspResp : X509ExtensionBase
	{
		// Token: 0x06003745 RID: 14149 RVA: 0x0012915C File Offset: 0x0012915C
		public BasicOcspResp(BasicOcspResponse resp)
		{
			this.resp = resp;
			this.data = resp.TbsResponseData;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x00129178 File Offset: 0x00129178
		public byte[] GetTbsResponseData()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.data.GetDerEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsResponseData", e);
			}
			return derEncoded;
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x001291B4 File Offset: 0x001291B4
		public int Version
		{
			get
			{
				return this.data.Version.IntValueExact + 1;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x001291C8 File Offset: 0x001291C8
		public RespID ResponderId
		{
			get
			{
				return new RespID(this.data.ResponderID);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x001291DC File Offset: 0x001291DC
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x001291F0 File Offset: 0x001291F0
		public SingleResp[] Responses
		{
			get
			{
				Asn1Sequence responses = this.data.Responses;
				SingleResp[] array = new SingleResp[responses.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
				}
				return array;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x00129244 File Offset: 0x00129244
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x00129254 File Offset: 0x00129254
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x0012925C File Offset: 0x0012925C
		public string SignatureAlgName
		{
			get
			{
				return OcspUtilities.GetAlgorithmName(this.resp.SignatureAlgorithm.Algorithm);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x00129274 File Offset: 0x00129274
		public string SignatureAlgOid
		{
			get
			{
				return this.resp.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0012928C File Offset: 0x0012928C
		[Obsolete("RespData class is no longer required as all functionality is available on this class")]
		public RespData GetResponseData()
		{
			return new RespData(this.data);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x0012929C File Offset: 0x0012929C
		public byte[] GetSignature()
		{
			return this.resp.GetSignatureOctets();
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x001292AC File Offset: 0x001292AC
		private IList GetCertList()
		{
			IList list = Platform.CreateArrayList();
			Asn1Sequence certs = this.resp.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						list.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (IOException e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
					catch (CertificateException e2)
					{
						throw new OcspException("can't re-encode certificate!", e2);
					}
				}
			}
			return list;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x00129370 File Offset: 0x00129370
		public X509Certificate[] GetCerts()
		{
			IList certList = this.GetCertList();
			X509Certificate[] array = new X509Certificate[certList.Count];
			for (int i = 0; i < certList.Count; i++)
			{
				array[i] = (X509Certificate)certList[i];
			}
			return array;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x001293BC File Offset: 0x001293BC
		public IX509Store GetCertificates(string type)
		{
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

		// Token: 0x06003754 RID: 14164 RVA: 0x00129408 File Offset: 0x00129408
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgName);
				signer.Init(false, publicKey);
				byte[] derEncoded = this.data.GetDerEncoded();
				signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x00129478 File Offset: 0x00129478
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x00129488 File Offset: 0x00129488
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BasicOcspResp basicOcspResp = obj as BasicOcspResp;
			return basicOcspResp != null && this.resp.Equals(basicOcspResp.resp);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x001294C4 File Offset: 0x001294C4
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x04001D4F RID: 7503
		private readonly BasicOcspResponse resp;

		// Token: 0x04001D50 RID: 7504
		private readonly ResponseData data;
	}
}
