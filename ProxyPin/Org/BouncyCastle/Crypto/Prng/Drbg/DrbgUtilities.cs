using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x02000480 RID: 1152
	internal class DrbgUtilities
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x000C79E8 File Offset: 0x000C79E8
		static DrbgUtilities()
		{
			DrbgUtilities.maxSecurityStrengths.Add("SHA-1", 128);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-224", 192);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-256", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-384", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512/224", 192);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512/256", 256);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000C7AB4 File Offset: 0x000C7AB4
		internal static int GetMaxSecurityStrength(IDigest d)
		{
			return (int)DrbgUtilities.maxSecurityStrengths[d.AlgorithmName];
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000C7ACC File Offset: 0x000C7ACC
		internal static int GetMaxSecurityStrength(IMac m)
		{
			string algorithmName = m.AlgorithmName;
			return (int)DrbgUtilities.maxSecurityStrengths[algorithmName.Substring(0, algorithmName.IndexOf("/"))];
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000C7B08 File Offset: 0x000C7B08
		internal static byte[] HashDF(IDigest digest, byte[] seedMaterial, int seedLength)
		{
			byte[] array = new byte[(seedLength + 7) / 8];
			int num = array.Length / digest.GetDigestSize();
			int num2 = 1;
			byte[] array2 = new byte[digest.GetDigestSize()];
			for (int i = 0; i <= num; i++)
			{
				digest.Update((byte)num2);
				digest.Update((byte)(seedLength >> 24));
				digest.Update((byte)(seedLength >> 16));
				digest.Update((byte)(seedLength >> 8));
				digest.Update((byte)seedLength);
				digest.BlockUpdate(seedMaterial, 0, seedMaterial.Length);
				digest.DoFinal(array2, 0);
				int length = (array.Length - i * array2.Length > array2.Length) ? array2.Length : (array.Length - i * array2.Length);
				Array.Copy(array2, 0, array, i * array2.Length, length);
				num2++;
			}
			if (seedLength % 8 != 0)
			{
				int num3 = 8 - seedLength % 8;
				uint num4 = 0U;
				for (int num5 = 0; num5 != array.Length; num5++)
				{
					uint num6 = (uint)array[num5];
					array[num5] = (byte)(num6 >> num3 | num4 << 8 - num3);
					num4 = num6;
				}
			}
			return array;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000C7C1C File Offset: 0x000C7C1C
		internal static bool IsTooLarge(byte[] bytes, int maxBytes)
		{
			return bytes != null && bytes.Length > maxBytes;
		}

		// Token: 0x0400169F RID: 5791
		private static readonly IDictionary maxSecurityStrengths = Platform.CreateHashtable();
	}
}
