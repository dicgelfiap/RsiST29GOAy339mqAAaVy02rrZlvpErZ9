using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000496 RID: 1174
	public class DsaDigestSigner : ISigner
	{
		// Token: 0x06002426 RID: 9254 RVA: 0x000C9944 File Offset: 0x000C9944
		public DsaDigestSigner(IDsa dsa, IDigest digest)
		{
			this.dsa = dsa;
			this.digest = digest;
			this.encoding = StandardDsaEncoding.Instance;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000C9968 File Offset: 0x000C9968
		public DsaDigestSigner(IDsaExt dsa, IDigest digest, IDsaEncoding encoding)
		{
			this.dsa = dsa;
			this.digest = digest;
			this.encoding = encoding;
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000C9988 File Offset: 0x000C9988
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsa.AlgorithmName;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000C99AC File Offset: 0x000C99AC
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing Requires Private Key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification Requires Public Key.");
			}
			this.Reset();
			this.dsa.Init(forSigning, parameters);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000C9A38 File Offset: 0x000C9A38
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000C9A48 File Offset: 0x000C9A48
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000C9A58 File Offset: 0x000C9A58
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger[] array2 = this.dsa.GenerateSignature(array);
			byte[] result;
			try
			{
				result = this.encoding.Encode(this.GetOrder(), array2[0], array2[1]);
			}
			catch (Exception)
			{
				throw new InvalidOperationException("unable to encode signature");
			}
			return result;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000C9AEC File Offset: 0x000C9AEC
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				BigInteger[] array2 = this.encoding.Decode(this.GetOrder(), signature);
				result = this.dsa.VerifySignature(array, array2[0], array2[1]);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000C9B7C File Offset: 0x000C9B7C
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000C9B8C File Offset: 0x000C9B8C
		protected virtual BigInteger GetOrder()
		{
			if (!(this.dsa is IDsaExt))
			{
				return null;
			}
			return ((IDsaExt)this.dsa).Order;
		}

		// Token: 0x040016E3 RID: 5859
		private readonly IDsa dsa;

		// Token: 0x040016E4 RID: 5860
		private readonly IDigest digest;

		// Token: 0x040016E5 RID: 5861
		private readonly IDsaEncoding encoding;

		// Token: 0x040016E6 RID: 5862
		private bool forSigning;
	}
}
