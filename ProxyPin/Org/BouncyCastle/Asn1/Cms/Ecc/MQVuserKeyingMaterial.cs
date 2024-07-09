using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms.Ecc
{
	// Token: 0x020000F9 RID: 249
	public class MQVuserKeyingMaterial : Asn1Encodable
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x00044250 File Offset: 0x00044250
		public MQVuserKeyingMaterial(OriginatorPublicKey ephemeralPublicKey, Asn1OctetString addedukm)
		{
			this.ephemeralPublicKey = ephemeralPublicKey;
			this.addedukm = addedukm;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00044268 File Offset: 0x00044268
		private MQVuserKeyingMaterial(Asn1Sequence seq)
		{
			this.ephemeralPublicKey = OriginatorPublicKey.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.addedukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000442B8 File Offset: 0x000442B8
		public static MQVuserKeyingMaterial GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return MQVuserKeyingMaterial.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000442C8 File Offset: 0x000442C8
		public static MQVuserKeyingMaterial GetInstance(object obj)
		{
			if (obj == null || obj is MQVuserKeyingMaterial)
			{
				return (MQVuserKeyingMaterial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MQVuserKeyingMaterial((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid MQVuserKeyingMaterial: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00044320 File Offset: 0x00044320
		public OriginatorPublicKey EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00044328 File Offset: 0x00044328
		public Asn1OctetString AddedUkm
		{
			get
			{
				return this.addedukm;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00044330 File Offset: 0x00044330
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ephemeralPublicKey
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.addedukm);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040006A1 RID: 1697
		private OriginatorPublicKey ephemeralPublicKey;

		// Token: 0x040006A2 RID: 1698
		private Asn1OctetString addedukm;
	}
}
