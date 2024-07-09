using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A3 RID: 1187
	public class Gost3410DigestSigner : ISigner
	{
		// Token: 0x06002487 RID: 9351 RVA: 0x000CB138 File Offset: 0x000CB138
		public Gost3410DigestSigner(IDsa signer, IDigest digest)
		{
			this.dsaSigner = signer;
			this.digest = digest;
			this.halfSize = digest.GetDigestSize();
			this.size = this.halfSize * 2;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000CB168 File Offset: 0x000CB168
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsaSigner.AlgorithmName;
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000CB18C File Offset: 0x000CB18C
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
			this.dsaSigner.Init(forSigning, parameters);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000CB218 File Offset: 0x000CB218
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000CB228 File Offset: 0x000CB228
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000CB238 File Offset: 0x000CB238
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GOST3410DigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] result;
			try
			{
				BigInteger[] array2 = this.dsaSigner.GenerateSignature(array);
				byte[] array3 = new byte[this.size];
				byte[] array4 = array2[0].ToByteArrayUnsigned();
				byte[] array5 = array2[1].ToByteArrayUnsigned();
				array5.CopyTo(array3, this.halfSize - array5.Length);
				array4.CopyTo(array3, this.size - array4.Length);
				result = array3;
			}
			catch (Exception ex)
			{
				throw new SignatureException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000CB300 File Offset: 0x000CB300
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger r;
			BigInteger s;
			try
			{
				r = new BigInteger(1, signature, this.halfSize, this.halfSize);
				s = new BigInteger(1, signature, 0, this.halfSize);
			}
			catch (Exception exception)
			{
				throw new SignatureException("error decoding signature bytes.", exception);
			}
			return this.dsaSigner.VerifySignature(array, r, s);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000CB398 File Offset: 0x000CB398
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x0400170F RID: 5903
		private readonly IDigest digest;

		// Token: 0x04001710 RID: 5904
		private readonly IDsa dsaSigner;

		// Token: 0x04001711 RID: 5905
		private readonly int size;

		// Token: 0x04001712 RID: 5906
		private int halfSize;

		// Token: 0x04001713 RID: 5907
		private bool forSigning;
	}
}
