using System;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089B RID: 2203
	[ComVisible(true)]
	public static class Extensions
	{
		// Token: 0x0600545B RID: 21595 RVA: 0x0019B7FC File Offset: 0x0019B7FC
		public static void WriteZeroes(this DataWriter writer, int count)
		{
			while (count >= 8)
			{
				writer.WriteUInt64(0UL);
				count -= 8;
			}
			for (int i = 0; i < count; i++)
			{
				writer.WriteByte(0);
			}
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0019B83C File Offset: 0x0019B83C
		public static void VerifyWriteTo(this IChunk chunk, DataWriter writer)
		{
			long position = writer.Position;
			chunk.WriteTo(writer);
			if (writer.Position - position != (long)((ulong)chunk.GetFileLength()))
			{
				Extensions.VerifyWriteToThrow(chunk);
			}
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0019B878 File Offset: 0x0019B878
		private static void VerifyWriteToThrow(IChunk chunk)
		{
			throw new IOException("Did not write all bytes: " + chunk.GetType().FullName);
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0019B894 File Offset: 0x0019B894
		internal static void WriteDataDirectory(this DataWriter writer, IChunk chunk)
		{
			if (chunk == null || chunk.GetVirtualSize() == 0U)
			{
				writer.WriteUInt64(0UL);
				return;
			}
			writer.WriteUInt32((uint)chunk.RVA);
			writer.WriteUInt32(chunk.GetVirtualSize());
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0019B8D8 File Offset: 0x0019B8D8
		internal static void WriteDebugDirectory(this DataWriter writer, DebugDirectory chunk)
		{
			if (chunk == null || chunk.GetVirtualSize() == 0U)
			{
				writer.WriteUInt64(0UL);
				return;
			}
			writer.WriteUInt32((uint)chunk.RVA);
			writer.WriteUInt32((uint)(chunk.Count * 28));
		}
	}
}
