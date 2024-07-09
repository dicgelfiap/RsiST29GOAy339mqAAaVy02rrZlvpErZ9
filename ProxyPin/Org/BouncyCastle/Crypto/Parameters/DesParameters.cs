using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000431 RID: 1073
	public class DesParameters : KeyParameter
	{
		// Token: 0x060021DE RID: 8670 RVA: 0x000C368C File Offset: 0x000C368C
		public DesParameters(byte[] key) : base(key)
		{
			if (DesParameters.IsWeakKey(key))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000C36AC File Offset: 0x000C36AC
		public DesParameters(byte[] key, int keyOff, int keyLen) : base(key, keyOff, keyLen)
		{
			if (DesParameters.IsWeakKey(key, keyOff))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000C36D0 File Offset: 0x000C36D0
		public static bool IsWeakKey(byte[] key, int offset)
		{
			if (key.Length - offset < 8)
			{
				throw new ArgumentException("key material too short.");
			}
			for (int i = 0; i < 16; i++)
			{
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					if (key[j + offset] != DesParameters.DES_weak_keys[i * 8 + j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000C3740 File Offset: 0x000C3740
		public static bool IsWeakKey(byte[] key)
		{
			return DesParameters.IsWeakKey(key, 0);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000C374C File Offset: 0x000C374C
		public static byte SetOddParity(byte b)
		{
			uint num = (uint)(b ^ 1);
			num ^= num >> 4;
			num ^= num >> 2;
			num ^= num >> 1;
			num &= 1U;
			return (byte)((uint)b ^ num);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000C377C File Offset: 0x000C377C
		public static void SetOddParity(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = DesParameters.SetOddParity(bytes[i]);
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000C37AC File Offset: 0x000C37AC
		public static void SetOddParity(byte[] bytes, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				bytes[off + i] = DesParameters.SetOddParity(bytes[off + i]);
			}
		}

		// Token: 0x040015D8 RID: 5592
		public const int DesKeyLength = 8;

		// Token: 0x040015D9 RID: 5593
		private const int N_DES_WEAK_KEYS = 16;

		// Token: 0x040015DA RID: 5594
		private static readonly byte[] DES_weak_keys = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			31,
			31,
			31,
			31,
			14,
			14,
			14,
			14,
			224,
			224,
			224,
			224,
			241,
			241,
			241,
			241,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			31,
			224,
			31,
			224,
			14,
			241,
			14,
			241,
			1,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			254,
			1,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			224,
			254,
			224,
			254,
			241,
			254,
			241,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			224,
			31,
			224,
			31,
			241,
			14,
			241,
			14,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			1,
			254,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			1,
			254,
			224,
			254,
			224,
			254,
			241,
			254,
			241
		};
	}
}
