using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000142 RID: 322
	public class ECGost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0004B434 File Offset: 0x0004B434
		public static ECGost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ECGost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0004B444 File Offset: 0x0004B444
		public static ECGost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is ECGost3410ParamSetParameters)
			{
				return (ECGost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ECGost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0004B49C File Offset: 0x0004B49C
		public ECGost3410ParamSetParameters(BigInteger a, BigInteger b, BigInteger p, BigInteger q, int x, BigInteger y)
		{
			this.a = new DerInteger(a);
			this.b = new DerInteger(b);
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.x = new DerInteger(x);
			this.y = new DerInteger(y);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0004B500 File Offset: 0x0004B500
		public ECGost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 6)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.a = DerInteger.GetInstance(seq[0]);
			this.b = DerInteger.GetInstance(seq[1]);
			this.p = DerInteger.GetInstance(seq[2]);
			this.q = DerInteger.GetInstance(seq[3]);
			this.x = DerInteger.GetInstance(seq[4]);
			this.y = DerInteger.GetInstance(seq[5]);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0004B5A0 File Offset: 0x0004B5A0
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0004B5B0 File Offset: 0x0004B5B0
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0004B5C0 File Offset: 0x0004B5C0
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0004B5D0 File Offset: 0x0004B5D0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.a,
				this.b,
				this.p,
				this.q,
				this.x,
				this.y
			});
		}

		// Token: 0x040007BB RID: 1979
		internal readonly DerInteger p;

		// Token: 0x040007BC RID: 1980
		internal readonly DerInteger q;

		// Token: 0x040007BD RID: 1981
		internal readonly DerInteger a;

		// Token: 0x040007BE RID: 1982
		internal readonly DerInteger b;

		// Token: 0x040007BF RID: 1983
		internal readonly DerInteger x;

		// Token: 0x040007C0 RID: 1984
		internal readonly DerInteger y;
	}
}
