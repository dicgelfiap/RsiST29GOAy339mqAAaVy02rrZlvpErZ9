using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044A RID: 1098
	public sealed class Ed25519PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x000C4F04 File Offset: 0x000C4F04
		public Ed25519PrivateKeyParameters(SecureRandom random) : base(true)
		{
			Ed25519.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000C4F2C File Offset: 0x000C4F2C
		public Ed25519PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, Ed25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000C4F58 File Offset: 0x000C4F58
		public Ed25519PrivateKeyParameters(Stream input) : base(true)
		{
			if (Ed25519PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed25519 private key");
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000C4F94 File Offset: 0x000C4F94
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000C4FAC File Offset: 0x000C4FAC
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000C4FBC File Offset: 0x000C4FBC
		public Ed25519PublicKeyParameters GeneratePublicKey()
		{
			Ed25519PublicKeyParameters result;
			lock (this.data)
			{
				if (this.cachedPublicKey == null)
				{
					byte[] array = new byte[Ed25519.PublicKeySize];
					Ed25519.GeneratePublicKey(this.data, 0, array, 0);
					this.cachedPublicKey = new Ed25519PublicKeyParameters(array, 0);
				}
				result = this.cachedPublicKey;
			}
			return result;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000C502C File Offset: 0x000C502C
		[Obsolete("Use overload that doesn't take a public key")]
		public void Sign(Ed25519.Algorithm algorithm, Ed25519PublicKeyParameters publicKey, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			this.Sign(algorithm, ctx, msg, msgOff, msgLen, sig, sigOff);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000C5050 File Offset: 0x000C5050
		public void Sign(Ed25519.Algorithm algorithm, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			Ed25519PublicKeyParameters ed25519PublicKeyParameters = this.GeneratePublicKey();
			byte[] array = new byte[Ed25519.PublicKeySize];
			ed25519PublicKeyParameters.Encode(array, 0);
			switch (algorithm)
			{
			case Ed25519.Algorithm.Ed25519:
				if (ctx != null)
				{
					throw new ArgumentException("ctx");
				}
				Ed25519.Sign(this.data, 0, array, 0, msg, msgOff, msgLen, sig, sigOff);
				return;
			case Ed25519.Algorithm.Ed25519ctx:
				Ed25519.Sign(this.data, 0, array, 0, ctx, msg, msgOff, msgLen, sig, sigOff);
				return;
			case Ed25519.Algorithm.Ed25519ph:
				if (Ed25519.PrehashSize != msgLen)
				{
					throw new ArgumentException("msgLen");
				}
				Ed25519.SignPrehash(this.data, 0, array, 0, ctx, msg, msgOff, sig, sigOff);
				return;
			default:
				throw new ArgumentException("algorithm");
			}
		}

		// Token: 0x04001612 RID: 5650
		public static readonly int KeySize = Ed25519.SecretKeySize;

		// Token: 0x04001613 RID: 5651
		public static readonly int SignatureSize = Ed25519.SignatureSize;

		// Token: 0x04001614 RID: 5652
		private readonly byte[] data = new byte[Ed25519PrivateKeyParameters.KeySize];

		// Token: 0x04001615 RID: 5653
		private Ed25519PublicKeyParameters cachedPublicKey;
	}
}
