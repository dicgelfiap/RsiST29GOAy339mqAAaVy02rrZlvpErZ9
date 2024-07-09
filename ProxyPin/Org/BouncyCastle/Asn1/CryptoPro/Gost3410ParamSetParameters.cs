using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000145 RID: 325
	public class Gost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x0004B8D8 File Offset: 0x0004B8D8
		public static Gost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0004B8E8 File Offset: 0x0004B8E8
		public static Gost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410ParamSetParameters)
			{
				return (Gost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0004B940 File Offset: 0x0004B940
		public Gost3410ParamSetParameters(int keySize, BigInteger p, BigInteger q, BigInteger a)
		{
			this.keySize = keySize;
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.a = new DerInteger(a);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0004B974 File Offset: 0x0004B974
		private Gost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.keySize = DerInteger.GetInstance(seq[0]).IntValueExact;
			this.p = DerInteger.GetInstance(seq[1]);
			this.q = DerInteger.GetInstance(seq[2]);
			this.a = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0004B9F4 File Offset: 0x0004B9F4
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0004B9FC File Offset: 0x0004B9FC
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0004BA0C File Offset: 0x0004BA0C
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0004BA1C File Offset: 0x0004BA1C
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0004BA2C File Offset: 0x0004BA2C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.keySize),
				this.p,
				this.q,
				this.a
			});
		}

		// Token: 0x040007C8 RID: 1992
		private readonly int keySize;

		// Token: 0x040007C9 RID: 1993
		private readonly DerInteger p;

		// Token: 0x040007CA RID: 1994
		private readonly DerInteger q;

		// Token: 0x040007CB RID: 1995
		private readonly DerInteger a;
	}
}
