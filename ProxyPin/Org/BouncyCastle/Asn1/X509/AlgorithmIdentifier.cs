using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001AA RID: 426
	public class AlgorithmIdentifier : Asn1Encodable
	{
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0005583C File Offset: 0x0005583C
		public static AlgorithmIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AlgorithmIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0005584C File Offset: 0x0005584C
		public static AlgorithmIdentifier GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is AlgorithmIdentifier)
			{
				return (AlgorithmIdentifier)obj;
			}
			return new AlgorithmIdentifier(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00055874 File Offset: 0x00055874
		public AlgorithmIdentifier(DerObjectIdentifier algorithm)
		{
			this.algorithm = algorithm;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00055884 File Offset: 0x00055884
		[Obsolete("Use version taking a DerObjectIdentifier")]
		public AlgorithmIdentifier(string algorithm)
		{
			this.algorithm = new DerObjectIdentifier(algorithm);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00055898 File Offset: 0x00055898
		public AlgorithmIdentifier(DerObjectIdentifier algorithm, Asn1Encodable parameters)
		{
			this.algorithm = algorithm;
			this.parameters = parameters;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000558B0 File Offset: 0x000558B0
		internal AlgorithmIdentifier(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.algorithm = DerObjectIdentifier.GetInstance(seq[0]);
			this.parameters = ((seq.Count < 2) ? null : seq[1]);
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0005592C File Offset: 0x0005592C
		public virtual DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00055934 File Offset: 0x00055934
		[Obsolete("Use 'Algorithm' property instead")]
		public virtual DerObjectIdentifier ObjectID
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0005593C File Offset: 0x0005593C
		public virtual Asn1Encodable Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00055944 File Offset: 0x00055944
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.algorithm
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.parameters
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009DF RID: 2527
		private readonly DerObjectIdentifier algorithm;

		// Token: 0x040009E0 RID: 2528
		private readonly Asn1Encodable parameters;
	}
}
