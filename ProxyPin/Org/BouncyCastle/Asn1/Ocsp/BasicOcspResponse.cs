using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200018D RID: 397
	public class BasicOcspResponse : Asn1Encodable
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00053100 File Offset: 0x00053100
		public static BasicOcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicOcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00053110 File Offset: 0x00053110
		public static BasicOcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is BasicOcspResponse)
			{
				return (BasicOcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BasicOcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0005316C File Offset: 0x0005316C
		public BasicOcspResponse(ResponseData tbsResponseData, AlgorithmIdentifier signatureAlgorithm, DerBitString signature, Asn1Sequence certs)
		{
			this.tbsResponseData = tbsResponseData;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
			this.certs = certs;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00053194 File Offset: 0x00053194
		private BasicOcspResponse(Asn1Sequence seq)
		{
			this.tbsResponseData = ResponseData.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signature = (DerBitString)seq[2];
			if (seq.Count > 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[3], true);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00053208 File Offset: 0x00053208
		[Obsolete("Use TbsResponseData property instead")]
		public ResponseData GetTbsResponseData()
		{
			return this.tbsResponseData;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00053210 File Offset: 0x00053210
		public ResponseData TbsResponseData
		{
			get
			{
				return this.tbsResponseData;
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00053218 File Offset: 0x00053218
		[Obsolete("Use SignatureAlgorithm property instead")]
		public AlgorithmIdentifier GetSignatureAlgorithm()
		{
			return this.signatureAlgorithm;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00053220 File Offset: 0x00053220
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00053228 File Offset: 0x00053228
		[Obsolete("Use Signature property instead")]
		public DerBitString GetSignature()
		{
			return this.signature;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00053230 File Offset: 0x00053230
		public DerBitString Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00053238 File Offset: 0x00053238
		public byte[] GetSignatureOctets()
		{
			return this.signature.GetOctets();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00053248 File Offset: 0x00053248
		[Obsolete("Use Certs property instead")]
		public Asn1Sequence GetCerts()
		{
			return this.certs;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00053250 File Offset: 0x00053250
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00053258 File Offset: 0x00053258
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsResponseData,
				this.signatureAlgorithm,
				this.signature
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.certs);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400097B RID: 2427
		private readonly ResponseData tbsResponseData;

		// Token: 0x0400097C RID: 2428
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x0400097D RID: 2429
		private readonly DerBitString signature;

		// Token: 0x0400097E RID: 2430
		private readonly Asn1Sequence certs;
	}
}
