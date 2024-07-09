using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F7 RID: 503
	public class DsaParameter : Asn1Encodable
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0005ECC4 File Offset: 0x0005ECC4
		public static DsaParameter GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DsaParameter.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0005ECD4 File Offset: 0x0005ECD4
		public static DsaParameter GetInstance(object obj)
		{
			if (obj == null || obj is DsaParameter)
			{
				return (DsaParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DsaParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DsaParameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0005ED2C File Offset: 0x0005ED2C
		public DsaParameter(BigInteger p, BigInteger q, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.g = new DerInteger(g);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005ED58 File Offset: 0x0005ED58
		private DsaParameter(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.q = DerInteger.GetInstance(seq[1]);
			this.g = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0005EDD4 File Offset: 0x0005EDD4
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0005EDE4 File Offset: 0x0005EDE4
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0005EDF4 File Offset: 0x0005EDF4
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0005EE04 File Offset: 0x0005EE04
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.q,
				this.g
			});
		}

		// Token: 0x04000BD9 RID: 3033
		internal readonly DerInteger p;

		// Token: 0x04000BDA RID: 3034
		internal readonly DerInteger q;

		// Token: 0x04000BDB RID: 3035
		internal readonly DerInteger g;
	}
}
