using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044B RID: 1099
	public sealed class Ed25519PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x000C5128 File Offset: 0x000C5128
		public Ed25519PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, Ed25519PublicKeyParameters.KeySize);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000C5154 File Offset: 0x000C5154
		public Ed25519PublicKeyParameters(Stream input) : base(false)
		{
			if (Ed25519PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed25519 public key");
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000C5190 File Offset: 0x000C5190
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed25519PublicKeyParameters.KeySize);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000C51A8 File Offset: 0x000C51A8
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x04001616 RID: 5654
		public static readonly int KeySize = Ed25519.PublicKeySize;

		// Token: 0x04001617 RID: 5655
		private readonly byte[] data = new byte[Ed25519PublicKeyParameters.KeySize];
	}
}
