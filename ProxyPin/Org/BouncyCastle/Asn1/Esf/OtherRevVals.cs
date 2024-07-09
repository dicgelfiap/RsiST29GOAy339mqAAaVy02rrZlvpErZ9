using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015B RID: 347
	public class OtherRevVals : Asn1Encodable
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0004D9F4 File Offset: 0x0004D9F4
		public static OtherRevVals GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevVals)
			{
				return (OtherRevVals)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevVals((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevVals' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0004DA50 File Offset: 0x0004DA50
		private OtherRevVals(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevValType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevVals = seq[1].ToAsn1Object();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0004DAD0 File Offset: 0x0004DAD0
		public OtherRevVals(DerObjectIdentifier otherRevValType, Asn1Encodable otherRevVals)
		{
			if (otherRevValType == null)
			{
				throw new ArgumentNullException("otherRevValType");
			}
			if (otherRevVals == null)
			{
				throw new ArgumentNullException("otherRevVals");
			}
			this.otherRevValType = otherRevValType;
			this.otherRevVals = otherRevVals.ToAsn1Object();
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0004DB10 File Offset: 0x0004DB10
		public DerObjectIdentifier OtherRevValType
		{
			get
			{
				return this.otherRevValType;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0004DB18 File Offset: 0x0004DB18
		public Asn1Object OtherRevValsObject
		{
			get
			{
				return this.otherRevVals;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0004DB20 File Offset: 0x0004DB20
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevValType,
				this.otherRevVals
			});
		}

		// Token: 0x0400081A RID: 2074
		private readonly DerObjectIdentifier otherRevValType;

		// Token: 0x0400081B RID: 2075
		private readonly Asn1Object otherRevVals;
	}
}
