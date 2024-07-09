using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015A RID: 346
	public class OtherRevRefs : Asn1Encodable
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0004D890 File Offset: 0x0004D890
		public static OtherRevRefs GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevRefs)
			{
				return (OtherRevRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0004D8EC File Offset: 0x0004D8EC
		private OtherRevRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevRefType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevRefs = seq[1].ToAsn1Object();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0004D96C File Offset: 0x0004D96C
		public OtherRevRefs(DerObjectIdentifier otherRevRefType, Asn1Encodable otherRevRefs)
		{
			if (otherRevRefType == null)
			{
				throw new ArgumentNullException("otherRevRefType");
			}
			if (otherRevRefs == null)
			{
				throw new ArgumentNullException("otherRevRefs");
			}
			this.otherRevRefType = otherRevRefType;
			this.otherRevRefs = otherRevRefs.ToAsn1Object();
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0004D9AC File Offset: 0x0004D9AC
		public DerObjectIdentifier OtherRevRefType
		{
			get
			{
				return this.otherRevRefType;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0004D9B4 File Offset: 0x0004D9B4
		public Asn1Object OtherRevRefsObject
		{
			get
			{
				return this.otherRevRefs;
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0004D9BC File Offset: 0x0004D9BC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevRefType,
				this.otherRevRefs
			});
		}

		// Token: 0x04000818 RID: 2072
		private readonly DerObjectIdentifier otherRevRefType;

		// Token: 0x04000819 RID: 2073
		private readonly Asn1Object otherRevRefs;
	}
}
