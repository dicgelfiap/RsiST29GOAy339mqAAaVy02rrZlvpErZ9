using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200047D RID: 1149
	public sealed class X448PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002388 RID: 9096 RVA: 0x000C704C File Offset: 0x000C704C
		public X448PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, X448PublicKeyParameters.KeySize);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000C7078 File Offset: 0x000C7078
		public X448PublicKeyParameters(Stream input) : base(false)
		{
			if (X448PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X448 public key");
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000C70B4 File Offset: 0x000C70B4
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X448PublicKeyParameters.KeySize);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000C70CC File Offset: 0x000C70CC
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x0400168F RID: 5775
		public static readonly int KeySize = 56;

		// Token: 0x04001690 RID: 5776
		private readonly byte[] data = new byte[X448PublicKeyParameters.KeySize];
	}
}
