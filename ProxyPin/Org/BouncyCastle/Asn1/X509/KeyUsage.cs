using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000201 RID: 513
	public class KeyUsage : DerBitString
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x00060724 File Offset: 0x00060724
		public new static KeyUsage GetInstance(object obj)
		{
			if (obj is KeyUsage)
			{
				return (KeyUsage)obj;
			}
			if (obj is X509Extension)
			{
				return KeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			if (obj == null)
			{
				return null;
			}
			return new KeyUsage(DerBitString.GetInstance(obj));
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00060778 File Offset: 0x00060778
		public static KeyUsage FromExtensions(X509Extensions extensions)
		{
			return KeyUsage.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.KeyUsage));
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0006078C File Offset: 0x0006078C
		public KeyUsage(int usage) : base(usage)
		{
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00060798 File Offset: 0x00060798
		private KeyUsage(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000607AC File Offset: 0x000607AC
		public override string ToString()
		{
			byte[] bytes = this.GetBytes();
			if (bytes.Length == 1)
			{
				return "KeyUsage: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
			}
			return "KeyUsage: 0x" + ((int)(bytes[1] & byte.MaxValue) << 8 | (int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x04000C0E RID: 3086
		public const int DigitalSignature = 128;

		// Token: 0x04000C0F RID: 3087
		public const int NonRepudiation = 64;

		// Token: 0x04000C10 RID: 3088
		public const int KeyEncipherment = 32;

		// Token: 0x04000C11 RID: 3089
		public const int DataEncipherment = 16;

		// Token: 0x04000C12 RID: 3090
		public const int KeyAgreement = 8;

		// Token: 0x04000C13 RID: 3091
		public const int KeyCertSign = 4;

		// Token: 0x04000C14 RID: 3092
		public const int CrlSign = 2;

		// Token: 0x04000C15 RID: 3093
		public const int EncipherOnly = 1;

		// Token: 0x04000C16 RID: 3094
		public const int DecipherOnly = 32768;
	}
}
