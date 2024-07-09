using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200047A RID: 1146
	public sealed class X25519PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x000C6E64 File Offset: 0x000C6E64
		public X25519PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, X25519PublicKeyParameters.KeySize);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000C6E90 File Offset: 0x000C6E90
		public X25519PublicKeyParameters(Stream input) : base(false)
		{
			if (X25519PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X25519 public key");
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000C6ECC File Offset: 0x000C6ECC
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X25519PublicKeyParameters.KeySize);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000C6EE4 File Offset: 0x000C6EE4
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x0400168A RID: 5770
		public static readonly int KeySize = 32;

		// Token: 0x0400168B RID: 5771
		private readonly byte[] data = new byte[X25519PublicKeyParameters.KeySize];
	}
}
