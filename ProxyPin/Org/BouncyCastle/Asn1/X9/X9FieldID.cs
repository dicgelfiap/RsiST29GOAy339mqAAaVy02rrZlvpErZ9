using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000233 RID: 563
	public class X9FieldID : Asn1Encodable
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00066F1C File Offset: 0x00066F1C
		public X9FieldID(BigInteger primeP)
		{
			this.id = X9ObjectIdentifiers.PrimeField;
			this.parameters = new DerInteger(primeP);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00066F3C File Offset: 0x00066F3C
		public X9FieldID(int m, int k1) : this(m, k1, 0, 0)
		{
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00066F48 File Offset: 0x00066F48
		public X9FieldID(int m, int k1, int k2, int k3)
		{
			this.id = X9ObjectIdentifiers.CharacteristicTwoField;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(m)
			});
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("inconsistent k values");
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.TPBasis,
					new DerInteger(k1)
				});
			}
			else
			{
				if (k2 <= k1 || k3 <= k2)
				{
					throw new ArgumentException("inconsistent k values");
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.PPBasis,
					new DerSequence(new Asn1Encodable[]
					{
						new DerInteger(k1),
						new DerInteger(k2),
						new DerInteger(k3)
					})
				});
			}
			this.parameters = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00067044 File Offset: 0x00067044
		private X9FieldID(Asn1Sequence seq)
		{
			this.id = DerObjectIdentifier.GetInstance(seq[0]);
			this.parameters = seq[1].ToAsn1Object();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00067080 File Offset: 0x00067080
		public static X9FieldID GetInstance(object obj)
		{
			if (obj is X9FieldID)
			{
				return (X9FieldID)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new X9FieldID(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x000670A8 File Offset: 0x000670A8
		public DerObjectIdentifier Identifier
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000670B0 File Offset: 0x000670B0
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000670B8 File Offset: 0x000670B8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.id,
				this.parameters
			});
		}

		// Token: 0x04000D12 RID: 3346
		private readonly DerObjectIdentifier id;

		// Token: 0x04000D13 RID: 3347
		private readonly Asn1Object parameters;
	}
}
