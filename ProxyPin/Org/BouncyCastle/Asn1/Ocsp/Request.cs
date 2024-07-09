using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000196 RID: 406
	public class Request : Asn1Encodable
	{
		// Token: 0x06000D56 RID: 3414 RVA: 0x00053CE8 File Offset: 0x00053CE8
		public static Request GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Request.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00053CF8 File Offset: 0x00053CF8
		public static Request GetInstance(object obj)
		{
			if (obj == null || obj is Request)
			{
				return (Request)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Request((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00053D54 File Offset: 0x00053D54
		public Request(CertID reqCert, X509Extensions singleRequestExtensions)
		{
			if (reqCert == null)
			{
				throw new ArgumentNullException("reqCert");
			}
			this.reqCert = reqCert;
			this.singleRequestExtensions = singleRequestExtensions;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00053D7C File Offset: 0x00053D7C
		private Request(Asn1Sequence seq)
		{
			this.reqCert = CertID.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.singleRequestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00053DCC File Offset: 0x00053DCC
		public CertID ReqCert
		{
			get
			{
				return this.reqCert;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00053DD4 File Offset: 0x00053DD4
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.singleRequestExtensions;
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00053DDC File Offset: 0x00053DDC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.reqCert
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.singleRequestExtensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400099E RID: 2462
		private readonly CertID reqCert;

		// Token: 0x0400099F RID: 2463
		private readonly X509Extensions singleRequestExtensions;
	}
}
