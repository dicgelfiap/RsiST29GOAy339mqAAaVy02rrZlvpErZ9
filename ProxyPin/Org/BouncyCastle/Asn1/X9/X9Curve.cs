using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022F RID: 559
	public class X9Curve : Asn1Encodable
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x00066720 File Offset: 0x00066720
		public X9Curve(ECCurve curve) : this(curve, null)
		{
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0006672C File Offset: 0x0006672C
		public X9Curve(ECCurve curve, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			this.curve = curve;
			this.seed = Arrays.Clone(seed);
			if (ECAlgorithms.IsFpCurve(curve))
			{
				this.fieldIdentifier = X9ObjectIdentifiers.PrimeField;
				return;
			}
			if (ECAlgorithms.IsF2mCurve(curve))
			{
				this.fieldIdentifier = X9ObjectIdentifiers.CharacteristicTwoField;
				return;
			}
			throw new ArgumentException("This type of ECCurve is not implemented");
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000667A0 File Offset: 0x000667A0
		[Obsolete("Use constructor including order/cofactor")]
		public X9Curve(X9FieldID fieldID, Asn1Sequence seq) : this(fieldID, null, null, seq)
		{
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000667AC File Offset: 0x000667AC
		public X9Curve(X9FieldID fieldID, BigInteger order, BigInteger cofactor, Asn1Sequence seq)
		{
			if (fieldID == null)
			{
				throw new ArgumentNullException("fieldID");
			}
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.fieldIdentifier = fieldID.Identifier;
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField))
			{
				BigInteger value = ((DerInteger)fieldID.Parameters).Value;
				BigInteger a = new BigInteger(1, Asn1OctetString.GetInstance(seq[0]).GetOctets());
				BigInteger b = new BigInteger(1, Asn1OctetString.GetInstance(seq[1]).GetOctets());
				this.curve = new FpCurve(value, a, b, order, cofactor);
			}
			else
			{
				if (!this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
				{
					throw new ArgumentException("This type of ECCurve is not implemented");
				}
				DerSequence derSequence = (DerSequence)fieldID.Parameters;
				int intValueExact = ((DerInteger)derSequence[0]).IntValueExact;
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)derSequence[1];
				int k = 0;
				int k2 = 0;
				int intValueExact2;
				if (derObjectIdentifier.Equals(X9ObjectIdentifiers.TPBasis))
				{
					intValueExact2 = ((DerInteger)derSequence[2]).IntValueExact;
				}
				else
				{
					DerSequence derSequence2 = (DerSequence)derSequence[2];
					intValueExact2 = ((DerInteger)derSequence2[0]).IntValueExact;
					k = ((DerInteger)derSequence2[1]).IntValueExact;
					k2 = ((DerInteger)derSequence2[2]).IntValueExact;
				}
				BigInteger a2 = new BigInteger(1, Asn1OctetString.GetInstance(seq[0]).GetOctets());
				BigInteger b2 = new BigInteger(1, Asn1OctetString.GetInstance(seq[1]).GetOctets());
				this.curve = new F2mCurve(intValueExact, intValueExact2, k, k2, a2, b2, order, cofactor);
			}
			if (seq.Count == 3)
			{
				this.seed = ((DerBitString)seq[2]).GetBytes();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x000669A0 File Offset: 0x000669A0
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000669A8 File Offset: 0x000669A8
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000669B8 File Offset: 0x000669B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField) || this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
			{
				asn1EncodableVector.Add(new X9FieldElement(this.curve.A).ToAsn1Object());
				asn1EncodableVector.Add(new X9FieldElement(this.curve.B).ToAsn1Object());
			}
			if (this.seed != null)
			{
				asn1EncodableVector.Add(new DerBitString(this.seed));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000D05 RID: 3333
		private readonly ECCurve curve;

		// Token: 0x04000D06 RID: 3334
		private readonly byte[] seed;

		// Token: 0x04000D07 RID: 3335
		private readonly DerObjectIdentifier fieldIdentifier;
	}
}
