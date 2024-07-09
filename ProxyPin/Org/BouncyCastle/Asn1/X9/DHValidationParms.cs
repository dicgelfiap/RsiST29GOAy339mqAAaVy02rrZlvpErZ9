using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000229 RID: 553
	public class DHValidationParms : Asn1Encodable
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00065DA4 File Offset: 0x00065DA4
		public static DHValidationParms GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHValidationParms.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00065DB4 File Offset: 0x00065DB4
		public static DHValidationParms GetInstance(object obj)
		{
			if (obj == null || obj is DHValidationParms)
			{
				return (DHValidationParms)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DHValidationParms((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DHValidationParms: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00065E10 File Offset: 0x00065E10
		public DHValidationParms(DerBitString seed, DerInteger pgenCounter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			if (pgenCounter == null)
			{
				throw new ArgumentNullException("pgenCounter");
			}
			this.seed = seed;
			this.pgenCounter = pgenCounter;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00065E48 File Offset: 0x00065E48
		private DHValidationParms(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.seed = DerBitString.GetInstance(seq[0]);
			this.pgenCounter = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00065EB0 File Offset: 0x00065EB0
		public DerBitString Seed
		{
			get
			{
				return this.seed;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00065EB8 File Offset: 0x00065EB8
		public DerInteger PgenCounter
		{
			get
			{
				return this.pgenCounter;
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00065EC0 File Offset: 0x00065EC0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.seed,
				this.pgenCounter
			});
		}

		// Token: 0x04000CFA RID: 3322
		private readonly DerBitString seed;

		// Token: 0x04000CFB RID: 3323
		private readonly DerInteger pgenCounter;
	}
}
