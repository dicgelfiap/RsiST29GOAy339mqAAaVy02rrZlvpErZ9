using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace PeNet.Utilities
{
	// Token: 0x02000B96 RID: 2966
	[ComVisible(true)]
	public static class Hashes
	{
		// Token: 0x0600778E RID: 30606 RVA: 0x0023AB54 File Offset: 0x0023AB54
		public static string Sha256(string file)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array;
			using (StreamReader streamReader = new StreamReader(file))
			{
				array = new SHA256Managed().ComputeHash(streamReader.BaseStream);
			}
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600778F RID: 30607 RVA: 0x0023ABD8 File Offset: 0x0023ABD8
		public static string Sha256(byte[] buff)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in new SHA256Managed().ComputeHash(buff))
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x0023AC2C File Offset: 0x0023AC2C
		public static string Sha1(string file)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array;
			using (StreamReader streamReader = new StreamReader(file))
			{
				array = new SHA1Managed().ComputeHash(streamReader.BaseStream);
			}
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x0023ACB0 File Offset: 0x0023ACB0
		public static string Sha1(byte[] buff)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in new SHA1Managed().ComputeHash(buff))
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x0023AD04 File Offset: 0x0023AD04
		public static string MD5(string file)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array;
			using (StreamReader streamReader = new StreamReader(file))
			{
				array = new MD5CryptoServiceProvider().ComputeHash(streamReader.BaseStream);
			}
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x0023AD88 File Offset: 0x0023AD88
		public static string MD5(byte[] buff)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in new MD5CryptoServiceProvider().ComputeHash(buff))
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}
	}
}
