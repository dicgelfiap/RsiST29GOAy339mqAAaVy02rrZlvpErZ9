using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A0 RID: 1184
	public class Ed448phSigner : ISigner
	{
		// Token: 0x0600246F RID: 9327 RVA: 0x000CAC68 File Offset: 0x000CAC68
		public Ed448phSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000CAC88 File Offset: 0x000CAC88
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed448ph";
			}
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000CAC90 File Offset: 0x000CAC90
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed448PrivateKeyParameters)parameters;
				this.publicKey = null;
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed448PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000CACD0 File Offset: 0x000CACD0
		public virtual void Update(byte b)
		{
			this.prehash.Update(b);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000CACE0 File Offset: 0x000CACE0
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.prehash.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000CACF0 File Offset: 0x000CACF0
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed448phSigner not initialised for signature generation.");
			}
			byte[] array = new byte[Ed448.PrehashSize];
			if (Ed448.PrehashSize != this.prehash.DoFinal(array, 0, Ed448.PrehashSize))
			{
				throw new InvalidOperationException("Prehash digest failed");
			}
			byte[] array2 = new byte[Ed448PrivateKeyParameters.SignatureSize];
			this.privateKey.Sign(Ed448.Algorithm.Ed448ph, this.context, array, 0, Ed448.PrehashSize, array2, 0);
			return array2;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000CAD7C File Offset: 0x000CAD7C
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed448phSigner not initialised for verification");
			}
			if (Ed448.SignatureSize != signature.Length)
			{
				return false;
			}
			byte[] encoded = this.publicKey.GetEncoded();
			return Ed448.VerifyPrehash(signature, 0, encoded, 0, this.context, this.prehash);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000CADE0 File Offset: 0x000CADE0
		public void Reset()
		{
			this.prehash.Reset();
		}

		// Token: 0x04001702 RID: 5890
		private readonly IXof prehash = Ed448.CreatePrehash();

		// Token: 0x04001703 RID: 5891
		private readonly byte[] context;

		// Token: 0x04001704 RID: 5892
		private bool forSigning;

		// Token: 0x04001705 RID: 5893
		private Ed448PrivateKeyParameters privateKey;

		// Token: 0x04001706 RID: 5894
		private Ed448PublicKeyParameters publicKey;
	}
}
