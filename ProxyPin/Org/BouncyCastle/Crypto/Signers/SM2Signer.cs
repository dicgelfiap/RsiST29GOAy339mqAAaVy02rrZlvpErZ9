using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004B0 RID: 1200
	public class SM2Signer : ISigner
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x000CDEF4 File Offset: 0x000CDEF4
		public SM2Signer() : this(StandardDsaEncoding.Instance, new SM3Digest())
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000CDF08 File Offset: 0x000CDF08
		public SM2Signer(IDigest digest) : this(StandardDsaEncoding.Instance, digest)
		{
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000CDF18 File Offset: 0x000CDF18
		public SM2Signer(IDsaEncoding encoding) : this(encoding, new SM3Digest())
		{
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000CDF28 File Offset: 0x000CDF28
		public SM2Signer(IDsaEncoding encoding, IDigest digest)
		{
			this.encoding = encoding;
			this.digest = digest;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x000CDF4C File Offset: 0x000CDF4C
		public virtual string AlgorithmName
		{
			get
			{
				return "SM2Sign";
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000CDF54 File Offset: 0x000CDF54
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			ICipherParameters cipherParameters;
			byte[] array;
			if (parameters is ParametersWithID)
			{
				cipherParameters = ((ParametersWithID)parameters).Parameters;
				array = ((ParametersWithID)parameters).GetID();
				if (array.Length >= 8192)
				{
					throw new ArgumentException("SM2 user ID must be less than 2^16 bits long");
				}
			}
			else
			{
				cipherParameters = parameters;
				array = Hex.DecodeStrict("31323334353637383132333435363738");
			}
			if (forSigning)
			{
				if (cipherParameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)cipherParameters;
					this.ecKey = (ECKeyParameters)parametersWithRandom.Parameters;
					this.ecParams = this.ecKey.Parameters;
					this.kCalculator.Init(this.ecParams.N, parametersWithRandom.Random);
				}
				else
				{
					this.ecKey = (ECKeyParameters)cipherParameters;
					this.ecParams = this.ecKey.Parameters;
					this.kCalculator.Init(this.ecParams.N, new SecureRandom());
				}
				this.pubPoint = this.CreateBasePointMultiplier().Multiply(this.ecParams.G, ((ECPrivateKeyParameters)this.ecKey).D).Normalize();
			}
			else
			{
				this.ecKey = (ECKeyParameters)cipherParameters;
				this.ecParams = this.ecKey.Parameters;
				this.pubPoint = ((ECPublicKeyParameters)this.ecKey).Q;
			}
			this.digest.Reset();
			this.z = this.GetZ(array);
			this.digest.BlockUpdate(this.z, 0, this.z.Length);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000CE0DC File Offset: 0x000CE0DC
		public virtual void Update(byte b)
		{
			this.digest.Update(b);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000CE0EC File Offset: 0x000CE0EC
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.digest.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000CE0FC File Offset: 0x000CE0FC
		public virtual bool VerifySignature(byte[] signature)
		{
			try
			{
				BigInteger[] array = this.encoding.Decode(this.ecParams.N, signature);
				return this.VerifySignature(array[0], array[1]);
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000CE154 File Offset: 0x000CE154
		public virtual void Reset()
		{
			if (this.z != null)
			{
				this.digest.Reset();
				this.digest.BlockUpdate(this.z, 0, this.z.Length);
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000CE188 File Offset: 0x000CE188
		public virtual byte[] GenerateSignature()
		{
			byte[] message = DigestUtilities.DoFinal(this.digest);
			BigInteger n = this.ecParams.N;
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger d = ((ECPrivateKeyParameters)this.ecKey).D;
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger3;
			BigInteger bigInteger5;
			for (;;)
			{
				BigInteger bigInteger2 = this.kCalculator.NextK();
				ECPoint ecpoint = ecmultiplier.Multiply(this.ecParams.G, bigInteger2).Normalize();
				bigInteger3 = bigInteger.Add(ecpoint.AffineXCoord.ToBigInteger()).Mod(n);
				if (bigInteger3.SignValue != 0 && !bigInteger3.Add(bigInteger2).Equals(n))
				{
					BigInteger bigInteger4 = d.Add(BigInteger.One).ModInverse(n);
					bigInteger5 = bigInteger2.Subtract(bigInteger3.Multiply(d)).Mod(n);
					bigInteger5 = bigInteger4.Multiply(bigInteger5).Mod(n);
					if (bigInteger5.SignValue != 0)
					{
						break;
					}
				}
			}
			byte[] result;
			try
			{
				result = this.encoding.Encode(this.ecParams.N, bigInteger3, bigInteger5);
			}
			catch (Exception ex)
			{
				throw new CryptoException("unable to encode signature: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000CE2C0 File Offset: 0x000CE2C0
		private bool VerifySignature(BigInteger r, BigInteger s)
		{
			BigInteger n = this.ecParams.N;
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.One) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			byte[] message = DigestUtilities.DoFinal(this.digest);
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger bigInteger2 = r.Add(s).Mod(n);
			if (bigInteger2.SignValue == 0)
			{
				return false;
			}
			ECPoint q = ((ECPublicKeyParameters)this.ecKey).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(this.ecParams.G, s, q, bigInteger2).Normalize();
			return !ecpoint.IsInfinity && r.Equals(bigInteger.Add(ecpoint.AffineXCoord.ToBigInteger()).Mod(n));
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000CE3A8 File Offset: 0x000CE3A8
		private byte[] GetZ(byte[] userID)
		{
			this.AddUserID(this.digest, userID);
			this.AddFieldElement(this.digest, this.ecParams.Curve.A);
			this.AddFieldElement(this.digest, this.ecParams.Curve.B);
			this.AddFieldElement(this.digest, this.ecParams.G.AffineXCoord);
			this.AddFieldElement(this.digest, this.ecParams.G.AffineYCoord);
			this.AddFieldElement(this.digest, this.pubPoint.AffineXCoord);
			this.AddFieldElement(this.digest, this.pubPoint.AffineYCoord);
			return DigestUtilities.DoFinal(this.digest);
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000CE470 File Offset: 0x000CE470
		private void AddUserID(IDigest digest, byte[] userID)
		{
			int num = userID.Length * 8;
			digest.Update((byte)(num >> 8));
			digest.Update((byte)num);
			digest.BlockUpdate(userID, 0, userID.Length);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000CE4A4 File Offset: 0x000CE4A4
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000CE4C8 File Offset: 0x000CE4C8
		protected virtual BigInteger CalculateE(BigInteger n, byte[] message)
		{
			return new BigInteger(1, message);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000CE4D4 File Offset: 0x000CE4D4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x04001769 RID: 5993
		private readonly IDsaKCalculator kCalculator = new RandomDsaKCalculator();

		// Token: 0x0400176A RID: 5994
		private readonly IDigest digest;

		// Token: 0x0400176B RID: 5995
		private readonly IDsaEncoding encoding;

		// Token: 0x0400176C RID: 5996
		private ECDomainParameters ecParams;

		// Token: 0x0400176D RID: 5997
		private ECPoint pubPoint;

		// Token: 0x0400176E RID: 5998
		private ECKeyParameters ecKey;

		// Token: 0x0400176F RID: 5999
		private byte[] z;
	}
}
