using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000193 RID: 403
	public class OcspResponse : Asn1Encodable
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x000538B0 File Offset: 0x000538B0
		public static OcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000538C0 File Offset: 0x000538C0
		public static OcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponse)
			{
				return (OcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0005391C File Offset: 0x0005391C
		public OcspResponse(OcspResponseStatus responseStatus, ResponseBytes responseBytes)
		{
			if (responseStatus == null)
			{
				throw new ArgumentNullException("responseStatus");
			}
			this.responseStatus = responseStatus;
			this.responseBytes = responseBytes;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00053944 File Offset: 0x00053944
		private OcspResponse(Asn1Sequence seq)
		{
			this.responseStatus = new OcspResponseStatus(DerEnumerated.GetInstance(seq[0]));
			if (seq.Count == 2)
			{
				this.responseBytes = ResponseBytes.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00053998 File Offset: 0x00053998
		public OcspResponseStatus ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000539A0 File Offset: 0x000539A0
		public ResponseBytes ResponseBytes
		{
			get
			{
				return this.responseBytes;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000539A8 File Offset: 0x000539A8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.responseStatus
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.responseBytes);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000993 RID: 2451
		private readonly OcspResponseStatus responseStatus;

		// Token: 0x04000994 RID: 2452
		private readonly ResponseBytes responseBytes;
	}
}
