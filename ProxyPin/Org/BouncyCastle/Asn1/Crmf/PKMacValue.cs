using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000138 RID: 312
	public class PKMacValue : Asn1Encodable
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x0004A060 File Offset: 0x0004A060
		private PKMacValue(Asn1Sequence seq)
		{
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.macValue = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0004A08C File Offset: 0x0004A08C
		public static PKMacValue GetInstance(object obj)
		{
			if (obj is PKMacValue)
			{
				return (PKMacValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PKMacValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0004A0E0 File Offset: 0x0004A0E0
		public static PKMacValue GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PKMacValue.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0004A0F0 File Offset: 0x0004A0F0
		public PKMacValue(PbmParameter pbmParams, DerBitString macValue) : this(new AlgorithmIdentifier(CmpObjectIdentifiers.passwordBasedMac, pbmParams), macValue)
		{
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0004A104 File Offset: 0x0004A104
		public PKMacValue(AlgorithmIdentifier algID, DerBitString macValue)
		{
			this.algID = algID;
			this.macValue = macValue;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0004A11C File Offset: 0x0004A11C
		public virtual AlgorithmIdentifier AlgID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0004A124 File Offset: 0x0004A124
		public virtual DerBitString MacValue
		{
			get
			{
				return this.macValue;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0004A12C File Offset: 0x0004A12C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				this.macValue
			});
		}

		// Token: 0x04000782 RID: 1922
		private readonly AlgorithmIdentifier algID;

		// Token: 0x04000783 RID: 1923
		private readonly DerBitString macValue;
	}
}
