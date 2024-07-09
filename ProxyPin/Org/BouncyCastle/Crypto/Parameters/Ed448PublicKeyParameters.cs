using System;
using System.IO;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044E RID: 1102
	public sealed class Ed448PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x060022A1 RID: 8865 RVA: 0x000C53CC File Offset: 0x000C53CC
		public Ed448PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, Ed448PublicKeyParameters.KeySize);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000C53F8 File Offset: 0x000C53F8
		public Ed448PublicKeyParameters(Stream input) : base(false)
		{
			if (Ed448PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed448 public key");
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000C5434 File Offset: 0x000C5434
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed448PublicKeyParameters.KeySize);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000C544C File Offset: 0x000C544C
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x0400161C RID: 5660
		public static readonly int KeySize = Ed448.PublicKeySize;

		// Token: 0x0400161D RID: 5661
		private readonly byte[] data = new byte[Ed448PublicKeyParameters.KeySize];
	}
}
