using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.Field;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000230 RID: 560
	public class X9ECParameters : Asn1Encodable
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00066A54 File Offset: 0x00066A54
		public static X9ECParameters GetInstance(object obj)
		{
			if (obj is X9ECParameters)
			{
				return (X9ECParameters)obj;
			}
			if (obj != null)
			{
				return new X9ECParameters(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00066A7C File Offset: 0x00066A7C
		public X9ECParameters(Asn1Sequence seq)
		{
			if (!(seq[0] is DerInteger) || !((DerInteger)seq[0]).Value.Equals(BigInteger.One))
			{
				throw new ArgumentException("bad version in X9ECParameters");
			}
			this.n = ((DerInteger)seq[4]).Value;
			if (seq.Count == 6)
			{
				this.h = ((DerInteger)seq[5]).Value;
			}
			X9Curve x9Curve = new X9Curve(X9FieldID.GetInstance(seq[1]), this.n, this.h, Asn1Sequence.GetInstance(seq[2]));
			this.curve = x9Curve.Curve;
			object obj = seq[3];
			if (obj is X9ECPoint)
			{
				this.g = (X9ECPoint)obj;
			}
			else
			{
				this.g = new X9ECPoint(this.curve, (Asn1OctetString)obj);
			}
			this.seed = x9Curve.GetSeed();
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00066B88 File Offset: 0x00066B88
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, null, null)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00066B98 File Offset: 0x00066B98
		public X9ECParameters(ECCurve curve, X9ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00066BA8 File Offset: 0x00066BA8
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00066BB8 File Offset: 0x00066BB8
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed) : this(curve, new X9ECPoint(g), n, h, seed)
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00066BCC File Offset: 0x00066BCC
		public X9ECParameters(ECCurve curve, X9ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			this.curve = curve;
			this.g = g;
			this.n = n;
			this.h = h;
			this.seed = seed;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				this.fieldID = new X9FieldID(curve.Field.Characteristic);
				return;
			}
			if (!ECAlgorithms.IsF2mCurve(curve))
			{
				throw new ArgumentException("'curve' is of an unsupported type");
			}
			IPolynomialExtensionField polynomialExtensionField = (IPolynomialExtensionField)curve.Field;
			int[] exponentsPresent = polynomialExtensionField.MinimalPolynomial.GetExponentsPresent();
			if (exponentsPresent.Length == 3)
			{
				this.fieldID = new X9FieldID(exponentsPresent[2], exponentsPresent[1]);
				return;
			}
			if (exponentsPresent.Length == 5)
			{
				this.fieldID = new X9FieldID(exponentsPresent[4], exponentsPresent[1], exponentsPresent[2], exponentsPresent[3]);
				return;
			}
			throw new ArgumentException("Only trinomial and pentomial curves are supported");
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00066CA0 File Offset: 0x00066CA0
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00066CA8 File Offset: 0x00066CA8
		public ECPoint G
		{
			get
			{
				return this.g.Point;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00066CB8 File Offset: 0x00066CB8
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00066CC0 File Offset: 0x00066CC0
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00066CC8 File Offset: 0x00066CC8
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00066CD0 File Offset: 0x00066CD0
		public X9Curve CurveEntry
		{
			get
			{
				return new X9Curve(this.curve, this.seed);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00066CE4 File Offset: 0x00066CE4
		public X9FieldID FieldIDEntry
		{
			get
			{
				return this.fieldID;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00066CEC File Offset: 0x00066CEC
		public X9ECPoint BaseEntry
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00066CF4 File Offset: 0x00066CF4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(BigInteger.One),
				this.fieldID,
				new X9Curve(this.curve, this.seed),
				this.g,
				new DerInteger(this.n)
			});
			if (this.h != null)
			{
				asn1EncodableVector.Add(new DerInteger(this.h));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000D08 RID: 3336
		private X9FieldID fieldID;

		// Token: 0x04000D09 RID: 3337
		private ECCurve curve;

		// Token: 0x04000D0A RID: 3338
		private X9ECPoint g;

		// Token: 0x04000D0B RID: 3339
		private BigInteger n;

		// Token: 0x04000D0C RID: 3340
		private BigInteger h;

		// Token: 0x04000D0D RID: 3341
		private byte[] seed;
	}
}
