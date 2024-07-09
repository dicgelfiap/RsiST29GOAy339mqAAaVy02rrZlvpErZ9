using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AC RID: 1196
	public class PlainDsaEncoding : IDsaEncoding
	{
		// Token: 0x060024C9 RID: 9417 RVA: 0x000CD098 File Offset: 0x000CD098
		public virtual BigInteger[] Decode(BigInteger n, byte[] encoding)
		{
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			if (encoding.Length != unsignedByteLength * 2)
			{
				throw new ArgumentException("Encoding has incorrect length", "encoding");
			}
			return new BigInteger[]
			{
				this.DecodeValue(n, encoding, 0, unsignedByteLength),
				this.DecodeValue(n, encoding, unsignedByteLength, unsignedByteLength)
			};
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000CD0F8 File Offset: 0x000CD0F8
		public virtual byte[] Encode(BigInteger n, BigInteger r, BigInteger s)
		{
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			byte[] array = new byte[unsignedByteLength * 2];
			this.EncodeValue(n, r, array, 0, unsignedByteLength);
			this.EncodeValue(n, s, array, unsignedByteLength, unsignedByteLength);
			return array;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000CD130 File Offset: 0x000CD130
		protected virtual BigInteger CheckValue(BigInteger n, BigInteger x)
		{
			if (x.SignValue < 0 || x.CompareTo(n) >= 0)
			{
				throw new ArgumentException("Value out of range", "x");
			}
			return x;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000CD15C File Offset: 0x000CD15C
		protected virtual BigInteger DecodeValue(BigInteger n, byte[] buf, int off, int len)
		{
			return this.CheckValue(n, new BigInteger(1, buf, off, len));
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000CD170 File Offset: 0x000CD170
		protected virtual void EncodeValue(BigInteger n, BigInteger x, byte[] buf, int off, int len)
		{
			byte[] array = this.CheckValue(n, x).ToByteArrayUnsigned();
			int num = Math.Max(0, array.Length - len);
			int num2 = array.Length - num;
			int num3 = len - num2;
			Arrays.Fill(buf, off, off + num3, 0);
			Array.Copy(array, num, buf, off + num3, num2);
		}

		// Token: 0x04001752 RID: 5970
		public static readonly PlainDsaEncoding Instance = new PlainDsaEncoding();
	}
}
