using System;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D4 RID: 2260
	internal static class RoslynContentIdProvider
	{
		// Token: 0x0600580C RID: 22540 RVA: 0x001B04A8 File Offset: 0x001B04A8
		public static void GetContentId(byte[] hash, out Guid guid, out uint timestamp)
		{
			if (hash.Length < 20)
			{
				throw new InvalidOperationException();
			}
			byte[] array = new byte[16];
			Array.Copy(hash, 0, array, 0, array.Length);
			array[7] = ((array[7] & 15) | 64);
			array[8] = ((array[8] & 63) | 128);
			guid = new Guid(array);
			timestamp = (uint)(int.MinValue | ((int)hash[19] << 24 | (int)hash[18] << 16 | (int)hash[17] << 8 | (int)hash[16]));
		}
	}
}
