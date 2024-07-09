using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000479 RID: 1145
	public sealed class X25519PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002372 RID: 9074 RVA: 0x000C6D28 File Offset: 0x000C6D28
		public X25519PrivateKeyParameters(SecureRandom random) : base(true)
		{
			X25519.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000C6D50 File Offset: 0x000C6D50
		public X25519PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, X25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000C6D7C File Offset: 0x000C6D7C
		public X25519PrivateKeyParameters(Stream input) : base(true)
		{
			if (X25519PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X25519 private key");
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000C6DB8 File Offset: 0x000C6DB8
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000C6DD0 File Offset: 0x000C6DD0
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000C6DE0 File Offset: 0x000C6DE0
		public X25519PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[32];
			X25519.GeneratePublicKey(this.data, 0, array, 0);
			return new X25519PublicKeyParameters(array, 0);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000C6E10 File Offset: 0x000C6E10
		public void GenerateSecret(X25519PublicKeyParameters publicKey, byte[] buf, int off)
		{
			byte[] array = new byte[32];
			publicKey.Encode(array, 0);
			if (!X25519.CalculateAgreement(this.data, 0, array, 0, buf, off))
			{
				throw new InvalidOperationException("X25519 agreement failed");
			}
		}

		// Token: 0x04001687 RID: 5767
		public static readonly int KeySize = 32;

		// Token: 0x04001688 RID: 5768
		public static readonly int SecretSize = 32;

		// Token: 0x04001689 RID: 5769
		private readonly byte[] data = new byte[X25519PrivateKeyParameters.KeySize];
	}
}
