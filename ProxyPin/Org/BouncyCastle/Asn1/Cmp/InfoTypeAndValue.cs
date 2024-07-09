using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DC RID: 220
	public class InfoTypeAndValue : Asn1Encodable
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00041B94 File Offset: 0x00041B94
		private InfoTypeAndValue(Asn1Sequence seq)
		{
			this.infoType = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.infoValue = seq[1];
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00041BD8 File Offset: 0x00041BD8
		public static InfoTypeAndValue GetInstance(object obj)
		{
			if (obj is InfoTypeAndValue)
			{
				return (InfoTypeAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new InfoTypeAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00041C2C File Offset: 0x00041C2C
		public InfoTypeAndValue(DerObjectIdentifier infoType)
		{
			this.infoType = infoType;
			this.infoValue = null;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00041C44 File Offset: 0x00041C44
		public InfoTypeAndValue(DerObjectIdentifier infoType, Asn1Encodable optionalValue)
		{
			this.infoType = infoType;
			this.infoValue = optionalValue;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00041C5C File Offset: 0x00041C5C
		public virtual DerObjectIdentifier InfoType
		{
			get
			{
				return this.infoType;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00041C64 File Offset: 0x00041C64
		public virtual Asn1Encodable InfoValue
		{
			get
			{
				return this.infoValue;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00041C6C File Offset: 0x00041C6C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.infoType
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.infoValue
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400060F RID: 1551
		private readonly DerObjectIdentifier infoType;

		// Token: 0x04000610 RID: 1552
		private readonly Asn1Encodable infoValue;
	}
}
