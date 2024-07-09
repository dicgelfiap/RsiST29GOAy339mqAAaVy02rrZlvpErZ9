using System;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.W32Resources;

namespace dnlib.PE
{
	// Token: 0x02000756 RID: 1878
	[ComVisible(true)]
	public static class PEExtensions
	{
		// Token: 0x060041C1 RID: 16833 RVA: 0x0016373C File Offset: 0x0016373C
		public static ResourceData FindWin32ResourceData(this IPEImage self, ResourceName type, ResourceName name, ResourceName langId)
		{
			Win32Resources win32Resources = self.Win32Resources;
			if (win32Resources == null)
			{
				return null;
			}
			return win32Resources.Find(type, name, langId);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x00163758 File Offset: 0x00163758
		internal static uint CalculatePECheckSum(this Stream stream, long length, long checkSumOffset)
		{
			if ((length & 1L) != 0L)
			{
				PEExtensions.ThrowInvalidOperationException("Invalid PE length");
			}
			byte[] buffer = new byte[(int)Math.Min(length, 8192L)];
			uint num = 0U;
			num = PEExtensions.CalculatePECheckSum(stream, checkSumOffset, num, buffer);
			stream.Position += 4L;
			num = PEExtensions.CalculatePECheckSum(stream, length - checkSumOffset - 4L, num, buffer);
			ulong num2 = (ulong)num + (ulong)length;
			return (uint)num2 + (uint)(num2 >> 32);
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x001637C8 File Offset: 0x001637C8
		private static uint CalculatePECheckSum(Stream stream, long length, uint checkSum, byte[] buffer)
		{
			int num3;
			for (long num = 0L; num < length; num += (long)num3)
			{
				int num2 = (int)Math.Min(length - num, (long)buffer.Length);
				num3 = stream.Read(buffer, 0, num2);
				if (num3 != num2)
				{
					PEExtensions.ThrowInvalidOperationException("Couldn't read all bytes");
				}
				int i = 0;
				while (i < num3)
				{
					checkSum += (uint)((int)buffer[i++] | (int)buffer[i++] << 8);
					checkSum = (uint)((ushort)(checkSum + (checkSum >> 16)));
				}
			}
			return checkSum;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00163840 File Offset: 0x00163840
		private static void ThrowInvalidOperationException(string message)
		{
			throw new InvalidOperationException(message);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00163848 File Offset: 0x00163848
		public static RVA AlignUp(this RVA rva, uint alignment)
		{
			return (RVA)(rva + alignment - (RVA)1U & ~(RVA)(alignment - 1U));
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x00163854 File Offset: 0x00163854
		public static RVA AlignUp(this RVA rva, int alignment)
		{
			return (RVA)((ulong)rva + (ulong)((long)alignment) - 1UL & (ulong)((long)(~(long)(alignment - 1))));
		}
	}
}
