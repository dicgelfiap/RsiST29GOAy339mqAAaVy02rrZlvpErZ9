using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200013C RID: 316
	public class ProofOfPossession : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0004A560 File Offset: 0x0004A560
		private ProofOfPossession(Asn1TaggedObject tagged)
		{
			this.tagNo = tagged.TagNo;
			switch (this.tagNo)
			{
			case 0:
				this.obj = DerNull.Instance;
				return;
			case 1:
				this.obj = PopoSigningKey.GetInstance(tagged, false);
				return;
			case 2:
			case 3:
				this.obj = PopoPrivKey.GetInstance(tagged, false);
				return;
			default:
				throw new ArgumentException("unknown tag: " + this.tagNo, "tagged");
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0004A5EC File Offset: 0x0004A5EC
		public static ProofOfPossession GetInstance(object obj)
		{
			if (obj is ProofOfPossession)
			{
				return (ProofOfPossession)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new ProofOfPossession((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004A640 File Offset: 0x0004A640
		public ProofOfPossession()
		{
			this.tagNo = 0;
			this.obj = DerNull.Instance;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004A65C File Offset: 0x0004A65C
		public ProofOfPossession(PopoSigningKey Poposk)
		{
			this.tagNo = 1;
			this.obj = Poposk;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0004A674 File Offset: 0x0004A674
		public ProofOfPossession(int type, PopoPrivKey privkey)
		{
			this.tagNo = type;
			this.obj = privkey;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0004A68C File Offset: 0x0004A68C
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0004A694 File Offset: 0x0004A694
		public virtual Asn1Encodable Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004A69C File Offset: 0x0004A69C
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.obj);
		}

		// Token: 0x04000791 RID: 1937
		public const int TYPE_RA_VERIFIED = 0;

		// Token: 0x04000792 RID: 1938
		public const int TYPE_SIGNING_KEY = 1;

		// Token: 0x04000793 RID: 1939
		public const int TYPE_KEY_ENCIPHERMENT = 2;

		// Token: 0x04000794 RID: 1940
		public const int TYPE_KEY_AGREEMENT = 3;

		// Token: 0x04000795 RID: 1941
		private readonly int tagNo;

		// Token: 0x04000796 RID: 1942
		private readonly Asn1Encodable obj;
	}
}
