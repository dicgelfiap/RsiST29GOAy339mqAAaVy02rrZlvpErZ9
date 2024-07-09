using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044D RID: 1101
	public sealed class Ed448PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x000C51D4 File Offset: 0x000C51D4
		public Ed448PrivateKeyParameters(SecureRandom random) : base(true)
		{
			Ed448.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000C51FC File Offset: 0x000C51FC
		public Ed448PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, Ed448PrivateKeyParameters.KeySize);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000C5228 File Offset: 0x000C5228
		public Ed448PrivateKeyParameters(Stream input) : base(true)
		{
			if (Ed448PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed448 private key");
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000C5264 File Offset: 0x000C5264
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed448PrivateKeyParameters.KeySize);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000C527C File Offset: 0x000C527C
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000C528C File Offset: 0x000C528C
		public Ed448PublicKeyParameters GeneratePublicKey()
		{
			Ed448PublicKeyParameters result;
			lock (this.data)
			{
				if (this.cachedPublicKey == null)
				{
					byte[] array = new byte[Ed448.PublicKeySize];
					Ed448.GeneratePublicKey(this.data, 0, array, 0);
					this.cachedPublicKey = new Ed448PublicKeyParameters(array, 0);
				}
				result = this.cachedPublicKey;
			}
			return result;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000C52FC File Offset: 0x000C52FC
		[Obsolete("Use overload that doesn't take a public key")]
		public void Sign(Ed448.Algorithm algorithm, Ed448PublicKeyParameters publicKey, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			this.Sign(algorithm, ctx, msg, msgOff, msgLen, sig, sigOff);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000C5320 File Offset: 0x000C5320
		public void Sign(Ed448.Algorithm algorithm, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			Ed448PublicKeyParameters ed448PublicKeyParameters = this.GeneratePublicKey();
			byte[] array = new byte[Ed448.PublicKeySize];
			ed448PublicKeyParameters.Encode(array, 0);
			switch (algorithm)
			{
			case Ed448.Algorithm.Ed448:
				Ed448.Sign(this.data, 0, array, 0, ctx, msg, msgOff, msgLen, sig, sigOff);
				return;
			case Ed448.Algorithm.Ed448ph:
				if (Ed448.PrehashSize != msgLen)
				{
					throw new ArgumentException("msgLen");
				}
				Ed448.SignPrehash(this.data, 0, array, 0, ctx, msg, msgOff, sig, sigOff);
				return;
			default:
				throw new ArgumentException("algorithm");
			}
		}

		// Token: 0x04001618 RID: 5656
		public static readonly int KeySize = Ed448.SecretKeySize;

		// Token: 0x04001619 RID: 5657
		public static readonly int SignatureSize = Ed448.SignatureSize;

		// Token: 0x0400161A RID: 5658
		private readonly byte[] data = new byte[Ed448PrivateKeyParameters.KeySize];

		// Token: 0x0400161B RID: 5659
		private Ed448PublicKeyParameters cachedPublicKey;
	}
}
