using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000432 RID: 1074
	public class DesEdeParameters : DesParameters
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x000C37F8 File Offset: 0x000C37F8
		private static byte[] FixKey(byte[] key, int keyOff, int keyLen)
		{
			byte[] array = new byte[24];
			if (keyLen != 16)
			{
				if (keyLen != 24)
				{
					throw new ArgumentException("Bad length for DESede key: " + keyLen, "keyLen");
				}
				Array.Copy(key, keyOff, array, 0, 24);
			}
			else
			{
				Array.Copy(key, keyOff, array, 0, 16);
				Array.Copy(key, keyOff, array, 16, 8);
			}
			if (DesEdeParameters.IsWeakKey(array))
			{
				throw new ArgumentException("attempt to create weak DESede key");
			}
			return array;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000C3888 File Offset: 0x000C3888
		public DesEdeParameters(byte[] key) : base(DesEdeParameters.FixKey(key, 0, key.Length))
		{
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000C389C File Offset: 0x000C389C
		public DesEdeParameters(byte[] key, int keyOff, int keyLen) : base(DesEdeParameters.FixKey(key, keyOff, keyLen))
		{
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000C38AC File Offset: 0x000C38AC
		public static bool IsWeakKey(byte[] key, int offset, int length)
		{
			for (int i = offset; i < length; i += 8)
			{
				if (DesParameters.IsWeakKey(key, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000C38DC File Offset: 0x000C38DC
		public new static bool IsWeakKey(byte[] key, int offset)
		{
			return DesEdeParameters.IsWeakKey(key, offset, key.Length - offset);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000C38EC File Offset: 0x000C38EC
		public new static bool IsWeakKey(byte[] key)
		{
			return DesEdeParameters.IsWeakKey(key, 0, key.Length);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000C38F8 File Offset: 0x000C38F8
		public static bool IsRealEdeKey(byte[] key, int offset)
		{
			if (key.Length != 16)
			{
				return DesEdeParameters.IsReal3Key(key, offset);
			}
			return DesEdeParameters.IsReal2Key(key, offset);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000C3914 File Offset: 0x000C3914
		public static bool IsReal2Key(byte[] key, int offset)
		{
			bool flag = false;
			for (int num = offset; num != offset + 8; num++)
			{
				flag |= (key[num] != key[num + 8]);
			}
			return flag;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000C394C File Offset: 0x000C394C
		public static bool IsReal3Key(byte[] key, int offset)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int num = offset; num != offset + 8; num++)
			{
				flag |= (key[num] != key[num + 8]);
				flag2 |= (key[num] != key[num + 16]);
				flag3 |= (key[num + 8] != key[num + 16]);
			}
			return flag && flag2 && flag3;
		}

		// Token: 0x040015DB RID: 5595
		public const int DesEdeKeyLength = 24;
	}
}
