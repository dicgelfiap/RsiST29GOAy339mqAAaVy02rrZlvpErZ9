using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000288 RID: 648
	public class KeyFlags : SignatureSubpacket
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x0006D930 File Offset: 0x0006D930
		private static byte[] IntToByteArray(int v)
		{
			byte[] array = new byte[4];
			int num = 0;
			for (int num2 = 0; num2 != 4; num2++)
			{
				array[num2] = (byte)(v >> num2 * 8);
				if (array[num2] != 0)
				{
					num = num2;
				}
			}
			byte[] array2 = new byte[num + 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0006D988 File Offset: 0x0006D988
		public KeyFlags(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.KeyFlags, critical, isLongLength, data)
		{
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0006D998 File Offset: 0x0006D998
		public KeyFlags(bool critical, int flags) : base(SignatureSubpacketTag.KeyFlags, critical, false, KeyFlags.IntToByteArray(flags))
		{
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0006D9AC File Offset: 0x0006D9AC
		public int Flags
		{
			get
			{
				int num = 0;
				for (int num2 = 0; num2 != this.data.Length; num2++)
				{
					num |= (int)(this.data[num2] & byte.MaxValue) << num2 * 8;
				}
				return num;
			}
		}

		// Token: 0x04000DD7 RID: 3543
		public const int CertifyOther = 1;

		// Token: 0x04000DD8 RID: 3544
		public const int SignData = 2;

		// Token: 0x04000DD9 RID: 3545
		public const int EncryptComms = 4;

		// Token: 0x04000DDA RID: 3546
		public const int EncryptStorage = 8;

		// Token: 0x04000DDB RID: 3547
		public const int Split = 16;

		// Token: 0x04000DDC RID: 3548
		public const int Authentication = 32;

		// Token: 0x04000DDD RID: 3549
		public const int Shared = 128;
	}
}
