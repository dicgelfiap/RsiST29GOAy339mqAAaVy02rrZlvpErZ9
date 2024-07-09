using System;
using System.IO;
using System.Security.Cryptography;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089D RID: 2205
	internal static class Hasher
	{
		// Token: 0x06005468 RID: 21608 RVA: 0x0019BAA4 File Offset: 0x0019BAA4
		private static HashAlgorithm CreateHasher(ChecksumAlgorithm checksumAlgorithm)
		{
			HashAlgorithm result;
			switch (checksumAlgorithm)
			{
			case ChecksumAlgorithm.SHA1:
				result = SHA1.Create();
				break;
			case ChecksumAlgorithm.SHA256:
				result = SHA256.Create();
				break;
			case ChecksumAlgorithm.SHA384:
				result = SHA384.Create();
				break;
			case ChecksumAlgorithm.SHA512:
				result = SHA512.Create();
				break;
			default:
				throw new ArgumentOutOfRangeException("checksumAlgorithm");
			}
			return result;
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0019BB08 File Offset: 0x0019BB08
		public static string GetChecksumName(ChecksumAlgorithm checksumAlgorithm)
		{
			string result;
			switch (checksumAlgorithm)
			{
			case ChecksumAlgorithm.SHA1:
				result = "SHA1";
				break;
			case ChecksumAlgorithm.SHA256:
				result = "SHA256";
				break;
			case ChecksumAlgorithm.SHA384:
				result = "SHA384";
				break;
			case ChecksumAlgorithm.SHA512:
				result = "SHA512";
				break;
			default:
				throw new ArgumentOutOfRangeException("checksumAlgorithm");
			}
			return result;
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0019BB6C File Offset: 0x0019BB6C
		public static bool TryGetChecksumAlgorithm(string checksumName, out ChecksumAlgorithm pdbChecksumAlgorithm, out int checksumSize)
		{
			if (checksumName != null)
			{
				if (checksumName == "SHA1")
				{
					pdbChecksumAlgorithm = ChecksumAlgorithm.SHA1;
					checksumSize = 20;
					return true;
				}
				if (checksumName == "SHA256")
				{
					pdbChecksumAlgorithm = ChecksumAlgorithm.SHA256;
					checksumSize = 32;
					return true;
				}
				if (checksumName == "SHA384")
				{
					pdbChecksumAlgorithm = ChecksumAlgorithm.SHA384;
					checksumSize = 48;
					return true;
				}
				if (checksumName == "SHA512")
				{
					pdbChecksumAlgorithm = ChecksumAlgorithm.SHA512;
					checksumSize = 64;
					return true;
				}
			}
			pdbChecksumAlgorithm = ChecksumAlgorithm.SHA1;
			checksumSize = -1;
			return false;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0019BBF4 File Offset: 0x0019BBF4
		public static byte[] Hash(ChecksumAlgorithm checksumAlgorithm, Stream stream, long length)
		{
			byte[] array = new byte[(int)Math.Min(8192L, length)];
			byte[] hash;
			using (HashAlgorithm hashAlgorithm = Hasher.CreateHasher(checksumAlgorithm))
			{
				while (length > 0L)
				{
					int count = (int)Math.Min(length, (long)array.Length);
					int num = stream.Read(array, 0, count);
					if (num == 0)
					{
						throw new InvalidOperationException("Couldn't read all bytes");
					}
					hashAlgorithm.TransformBlock(array, 0, num, array, 0);
					length -= (long)num;
				}
				hashAlgorithm.TransformFinalBlock(Array2.Empty<byte>(), 0, 0);
				hash = hashAlgorithm.Hash;
			}
			return hash;
		}
	}
}
