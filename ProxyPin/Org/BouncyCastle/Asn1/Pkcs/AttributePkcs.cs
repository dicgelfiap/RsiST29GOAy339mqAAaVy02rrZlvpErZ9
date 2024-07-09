using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A1 RID: 417
	public class AttributePkcs : Asn1Encodable
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x00054CD0 File Offset: 0x00054CD0
		public static AttributePkcs GetInstance(object obj)
		{
			AttributePkcs attributePkcs = obj as AttributePkcs;
			if (obj == null || attributePkcs != null)
			{
				return attributePkcs;
			}
			Asn1Sequence asn1Sequence = obj as Asn1Sequence;
			if (asn1Sequence != null)
			{
				return new AttributePkcs(asn1Sequence);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00054D24 File Offset: 0x00054D24
		private AttributePkcs(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00054D7C File Offset: 0x00054D7C
		public AttributePkcs(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00054D94 File Offset: 0x00054D94
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00054D9C File Offset: 0x00054D9C
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00054DA4 File Offset: 0x00054DA4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x040009CA RID: 2506
		private readonly DerObjectIdentifier attrType;

		// Token: 0x040009CB RID: 2507
		private readonly Asn1Set attrValues;
	}
}
