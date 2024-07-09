using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049E RID: 1182
	public class Ed25519phSigner : ISigner
	{
		// Token: 0x0600245F RID: 9311 RVA: 0x000CA9EC File Offset: 0x000CA9EC
		public Ed25519phSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000CAA0C File Offset: 0x000CAA0C
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519ph";
			}
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000CAA14 File Offset: 0x000CAA14
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed25519PrivateKeyParameters)parameters;
				this.publicKey = null;
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed25519PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000CAA54 File Offset: 0x000CAA54
		public virtual void Update(byte b)
		{
			this.prehash.Update(b);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000CAA64 File Offset: 0x000CAA64
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.prehash.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000CAA74 File Offset: 0x000CAA74
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519phSigner not initialised for signature generation.");
			}
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != this.prehash.DoFinal(array, 0))
			{
				throw new InvalidOperationException("Prehash digest failed");
			}
			byte[] array2 = new byte[Ed25519PrivateKeyParameters.SignatureSize];
			this.privateKey.Sign(Ed25519.Algorithm.Ed25519ph, this.context, array, 0, Ed25519.PrehashSize, array2, 0);
			return array2;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000CAAFC File Offset: 0x000CAAFC
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519phSigner not initialised for verification");
			}
			if (Ed25519.SignatureSize != signature.Length)
			{
				return false;
			}
			byte[] encoded = this.publicKey.GetEncoded();
			return Ed25519.VerifyPrehash(signature, 0, encoded, 0, this.context, this.prehash);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000CAB60 File Offset: 0x000CAB60
		public void Reset()
		{
			this.prehash.Reset();
		}

		// Token: 0x040016F9 RID: 5881
		private readonly IDigest prehash = Ed25519.CreatePrehash();

		// Token: 0x040016FA RID: 5882
		private readonly byte[] context;

		// Token: 0x040016FB RID: 5883
		private bool forSigning;

		// Token: 0x040016FC RID: 5884
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x040016FD RID: 5885
		private Ed25519PublicKeyParameters publicKey;
	}
}
