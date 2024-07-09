using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000134 RID: 308
	public class EncryptedValue : Asn1Encodable
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00049AA8 File Offset: 0x00049AA8
		private EncryptedValue(Asn1Sequence seq)
		{
			int num = 0;
			while (seq[num] is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[num];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.intendedAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.symmAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.encSymmKey = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 3:
					this.keyAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 4:
					this.valueHint = Asn1OctetString.GetInstance(asn1TaggedObject, false);
					break;
				}
				num++;
			}
			this.encValue = DerBitString.GetInstance(seq[num]);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00049B78 File Offset: 0x00049B78
		public static EncryptedValue GetInstance(object obj)
		{
			if (obj is EncryptedValue)
			{
				return (EncryptedValue)obj;
			}
			if (obj != null)
			{
				return new EncryptedValue(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00049BA0 File Offset: 0x00049BA0
		public EncryptedValue(AlgorithmIdentifier intendedAlg, AlgorithmIdentifier symmAlg, DerBitString encSymmKey, AlgorithmIdentifier keyAlg, Asn1OctetString valueHint, DerBitString encValue)
		{
			if (encValue == null)
			{
				throw new ArgumentNullException("encValue");
			}
			this.intendedAlg = intendedAlg;
			this.symmAlg = symmAlg;
			this.encSymmKey = encSymmKey;
			this.keyAlg = keyAlg;
			this.valueHint = valueHint;
			this.encValue = encValue;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00049BF8 File Offset: 0x00049BF8
		public virtual AlgorithmIdentifier IntendedAlg
		{
			get
			{
				return this.intendedAlg;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00049C00 File Offset: 0x00049C00
		public virtual AlgorithmIdentifier SymmAlg
		{
			get
			{
				return this.symmAlg;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00049C08 File Offset: 0x00049C08
		public virtual DerBitString EncSymmKey
		{
			get
			{
				return this.encSymmKey;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00049C10 File Offset: 0x00049C10
		public virtual AlgorithmIdentifier KeyAlg
		{
			get
			{
				return this.keyAlg;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00049C18 File Offset: 0x00049C18
		public virtual Asn1OctetString ValueHint
		{
			get
			{
				return this.valueHint;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00049C20 File Offset: 0x00049C20
		public virtual DerBitString EncValue
		{
			get
			{
				return this.encValue;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00049C28 File Offset: 0x00049C28
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.intendedAlg);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.symmAlg);
			asn1EncodableVector.AddOptionalTagged(false, 2, this.encSymmKey);
			asn1EncodableVector.AddOptionalTagged(false, 3, this.keyAlg);
			asn1EncodableVector.AddOptionalTagged(false, 4, this.valueHint);
			asn1EncodableVector.Add(this.encValue);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000774 RID: 1908
		private readonly AlgorithmIdentifier intendedAlg;

		// Token: 0x04000775 RID: 1909
		private readonly AlgorithmIdentifier symmAlg;

		// Token: 0x04000776 RID: 1910
		private readonly DerBitString encSymmKey;

		// Token: 0x04000777 RID: 1911
		private readonly AlgorithmIdentifier keyAlg;

		// Token: 0x04000778 RID: 1912
		private readonly Asn1OctetString valueHint;

		// Token: 0x04000779 RID: 1913
		private readonly DerBitString encValue;
	}
}
