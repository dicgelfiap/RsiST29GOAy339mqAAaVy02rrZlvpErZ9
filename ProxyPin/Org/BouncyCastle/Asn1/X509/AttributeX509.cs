using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001C3 RID: 451
	public class AttributeX509 : Asn1Encodable
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00058C28 File Offset: 0x00058C28
		public static AttributeX509 GetInstance(object obj)
		{
			if (obj == null || obj is AttributeX509)
			{
				return (AttributeX509)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeX509((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00058C84 File Offset: 0x00058C84
		private AttributeX509(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00058CE8 File Offset: 0x00058CE8
		public AttributeX509(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00058D00 File Offset: 0x00058D00
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00058D08 File Offset: 0x00058D08
		public Asn1Encodable[] GetAttributeValues()
		{
			return this.attrValues.ToArray();
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00058D18 File Offset: 0x00058D18
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00058D20 File Offset: 0x00058D20
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x04000AFA RID: 2810
		private readonly DerObjectIdentifier attrType;

		// Token: 0x04000AFB RID: 2811
		private readonly Asn1Set attrValues;
	}
}
