using System;
using System.IO;

namespace dnlib.DotNet
{
	// Token: 0x02000854 RID: 2132
	internal static class StrongNameUtils
	{
		// Token: 0x06005070 RID: 20592 RVA: 0x0018FC10 File Offset: 0x0018FC10
		public static byte[] ReadBytesReverse(this BinaryReader reader, int len)
		{
			byte[] array = reader.ReadBytes(len);
			if (array.Length != len)
			{
				throw new InvalidKeyException("Can't read more bytes");
			}
			Array.Reverse(array);
			return array;
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0018FC34 File Offset: 0x0018FC34
		public static void WriteReverse(this BinaryWriter writer, byte[] data)
		{
			byte[] array = (byte[])data.Clone();
			Array.Reverse(array);
			writer.Write(array);
		}
	}
}
