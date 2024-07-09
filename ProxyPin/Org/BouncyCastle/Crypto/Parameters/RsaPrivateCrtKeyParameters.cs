using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000472 RID: 1138
	public class RsaPrivateCrtKeyParameters : RsaKeyParameters
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x000C6824 File Offset: 0x000C6824
		public RsaPrivateCrtKeyParameters(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger p, BigInteger q, BigInteger dP, BigInteger dQ, BigInteger qInv) : base(true, modulus, privateExponent)
		{
			RsaPrivateCrtKeyParameters.ValidateValue(publicExponent, "publicExponent", "exponent");
			RsaPrivateCrtKeyParameters.ValidateValue(p, "p", "P value");
			RsaPrivateCrtKeyParameters.ValidateValue(q, "q", "Q value");
			RsaPrivateCrtKeyParameters.ValidateValue(dP, "dP", "DP value");
			RsaPrivateCrtKeyParameters.ValidateValue(dQ, "dQ", "DQ value");
			RsaPrivateCrtKeyParameters.ValidateValue(qInv, "qInv", "InverseQ value");
			this.e = publicExponent;
			this.p = p;
			this.q = q;
			this.dP = dP;
			this.dQ = dQ;
			this.qInv = qInv;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000C68D4 File Offset: 0x000C68D4
		public RsaPrivateCrtKeyParameters(RsaPrivateKeyStructure rsaPrivateKey) : this(rsaPrivateKey.Modulus, rsaPrivateKey.PublicExponent, rsaPrivateKey.PrivateExponent, rsaPrivateKey.Prime1, rsaPrivateKey.Prime2, rsaPrivateKey.Exponent1, rsaPrivateKey.Exponent2, rsaPrivateKey.Coefficient)
		{
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000C691C File Offset: 0x000C691C
		public BigInteger PublicExponent
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000C6924 File Offset: 0x000C6924
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000C692C File Offset: 0x000C692C
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000C6934 File Offset: 0x000C6934
		public BigInteger DP
		{
			get
			{
				return this.dP;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x000C693C File Offset: 0x000C693C
		public BigInteger DQ
		{
			get
			{
				return this.dQ;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x000C6944 File Offset: 0x000C6944
		public BigInteger QInv
		{
			get
			{
				return this.qInv;
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000C694C File Offset: 0x000C694C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = obj as RsaPrivateCrtKeyParameters;
			return rsaPrivateCrtKeyParameters != null && (rsaPrivateCrtKeyParameters.DP.Equals(this.dP) && rsaPrivateCrtKeyParameters.DQ.Equals(this.dQ) && rsaPrivateCrtKeyParameters.Exponent.Equals(base.Exponent) && rsaPrivateCrtKeyParameters.Modulus.Equals(base.Modulus) && rsaPrivateCrtKeyParameters.P.Equals(this.p) && rsaPrivateCrtKeyParameters.Q.Equals(this.q) && rsaPrivateCrtKeyParameters.PublicExponent.Equals(this.e)) && rsaPrivateCrtKeyParameters.QInv.Equals(this.qInv);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000C6A24 File Offset: 0x000C6A24
		public override int GetHashCode()
		{
			return this.DP.GetHashCode() ^ this.DQ.GetHashCode() ^ base.Exponent.GetHashCode() ^ base.Modulus.GetHashCode() ^ this.P.GetHashCode() ^ this.Q.GetHashCode() ^ this.PublicExponent.GetHashCode() ^ this.QInv.GetHashCode();
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000C6A94 File Offset: 0x000C6A94
		private static void ValidateValue(BigInteger x, string name, string desc)
		{
			if (x == null)
			{
				throw new ArgumentNullException(name);
			}
			if (x.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA " + desc, name);
			}
		}

		// Token: 0x0400166D RID: 5741
		private readonly BigInteger e;

		// Token: 0x0400166E RID: 5742
		private readonly BigInteger p;

		// Token: 0x0400166F RID: 5743
		private readonly BigInteger q;

		// Token: 0x04001670 RID: 5744
		private readonly BigInteger dP;

		// Token: 0x04001671 RID: 5745
		private readonly BigInteger dQ;

		// Token: 0x04001672 RID: 5746
		private readonly BigInteger qInv;
	}
}
