using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200048A RID: 1162
	public abstract class EntropyUtilities
	{
		// Token: 0x060023D7 RID: 9175 RVA: 0x000C8968 File Offset: 0x000C8968
		public static byte[] GenerateSeed(IEntropySource entropySource, int numBytes)
		{
			byte[] array = new byte[numBytes];
			int num;
			for (int i = 0; i < numBytes; i += num)
			{
				byte[] entropy = entropySource.GetEntropy();
				num = Math.Min(array.Length, numBytes - i);
				Array.Copy(entropy, 0, array, i, num);
			}
			return array;
		}
	}
}
