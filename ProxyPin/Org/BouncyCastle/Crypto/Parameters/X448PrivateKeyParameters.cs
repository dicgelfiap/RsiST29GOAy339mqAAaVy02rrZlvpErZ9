using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200047C RID: 1148
	public sealed class X448PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x000C6F10 File Offset: 0x000C6F10
		public X448PrivateKeyParameters(SecureRandom random) : base(true)
		{
			X448.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000C6F38 File Offset: 0x000C6F38
		public X448PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, X448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000C6F64 File Offset: 0x000C6F64
		public X448PrivateKeyParameters(Stream input) : base(true)
		{
			if (X448PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X448 private key");
			}
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000C6FA0 File Offset: 0x000C6FA0
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000C6FB8 File Offset: 0x000C6FB8
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000C6FC8 File Offset: 0x000C6FC8
		public X448PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[56];
			X448.GeneratePublicKey(this.data, 0, array, 0);
			return new X448PublicKeyParameters(array, 0);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000C6FF8 File Offset: 0x000C6FF8
		public void GenerateSecret(X448PublicKeyParameters publicKey, byte[] buf, int off)
		{
			byte[] array = new byte[56];
			publicKey.Encode(array, 0);
			if (!X448.CalculateAgreement(this.data, 0, array, 0, buf, off))
			{
				throw new InvalidOperationException("X448 agreement failed");
			}
		}

		// Token: 0x0400168C RID: 5772
		public static readonly int KeySize = 56;

		// Token: 0x0400168D RID: 5773
		public static readonly int SecretSize = 56;

		// Token: 0x0400168E RID: 5774
		private readonly byte[] data = new byte[X448PrivateKeyParameters.KeySize];
	}
}
