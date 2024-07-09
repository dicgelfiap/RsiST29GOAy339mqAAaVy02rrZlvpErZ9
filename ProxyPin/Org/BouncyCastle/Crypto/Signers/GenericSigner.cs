using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A2 RID: 1186
	public class GenericSigner : ISigner
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000CAF00 File Offset: 0x000CAF00
		public GenericSigner(IAsymmetricBlockCipher engine, IDigest digest)
		{
			this.engine = engine;
			this.digest = digest;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000CAF18 File Offset: 0x000CAF18
		public virtual string AlgorithmName
		{
			get
			{
				return string.Concat(new string[]
				{
					"Generic(",
					this.engine.AlgorithmName,
					"/",
					this.digest.AlgorithmName,
					")"
				});
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000CAF80 File Offset: 0x000CAF80
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
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.engine.Init(forSigning, parameters);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000CB00C File Offset: 0x000CB00C
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000CB01C File Offset: 0x000CB01C
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000CB02C File Offset: 0x000CB02C
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000CB084 File Offset: 0x000CB084
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				byte[] array2 = this.engine.ProcessBlock(signature, 0, signature.Length);
				if (array2.Length < array.Length)
				{
					byte[] array3 = new byte[array.Length];
					Array.Copy(array2, 0, array3, array3.Length - array2.Length, array2.Length);
					array2 = array3;
				}
				result = Arrays.ConstantTimeAreEqual(array2, array);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000CB128 File Offset: 0x000CB128
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x0400170C RID: 5900
		private readonly IAsymmetricBlockCipher engine;

		// Token: 0x0400170D RID: 5901
		private readonly IDigest digest;

		// Token: 0x0400170E RID: 5902
		private bool forSigning;
	}
}
