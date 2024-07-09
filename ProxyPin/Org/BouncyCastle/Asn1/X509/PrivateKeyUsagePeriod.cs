using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000209 RID: 521
	public class PrivateKeyUsagePeriod : Asn1Encodable
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0006110C File Offset: 0x0006110C
		public static PrivateKeyUsagePeriod GetInstance(object obj)
		{
			if (obj is PrivateKeyUsagePeriod)
			{
				return (PrivateKeyUsagePeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PrivateKeyUsagePeriod((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return PrivateKeyUsagePeriod.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0006117C File Offset: 0x0006117C
		private PrivateKeyUsagePeriod(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				if (asn1TaggedObject.TagNo == 0)
				{
					this._notBefore = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
				else if (asn1TaggedObject.TagNo == 1)
				{
					this._notAfter = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00061210 File Offset: 0x00061210
		public DerGeneralizedTime NotBefore
		{
			get
			{
				return this._notBefore;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00061218 File Offset: 0x00061218
		public DerGeneralizedTime NotAfter
		{
			get
			{
				return this._notAfter;
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00061220 File Offset: 0x00061220
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this._notBefore);
			asn1EncodableVector.AddOptionalTagged(false, 1, this._notAfter);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C2A RID: 3114
		private DerGeneralizedTime _notBefore;

		// Token: 0x04000C2B RID: 3115
		private DerGeneralizedTime _notAfter;
	}
}
