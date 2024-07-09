using System;
using System.IO;
using System.Security.Cryptography;

namespace dnlib.DotNet
{
	// Token: 0x02000778 RID: 1912
	internal readonly struct AssemblyHash : IDisposable
	{
		// Token: 0x06004352 RID: 17234 RVA: 0x00167C5C File Offset: 0x00167C5C
		public AssemblyHash(AssemblyHashAlgorithm hashAlgo)
		{
			HashAlgorithm hashAlgorithm;
			if (hashAlgo != AssemblyHashAlgorithm.MD5)
			{
				switch (hashAlgo)
				{
				case AssemblyHashAlgorithm.SHA_256:
					hashAlgorithm = SHA256.Create();
					break;
				case AssemblyHashAlgorithm.SHA_384:
					hashAlgorithm = SHA384.Create();
					break;
				case AssemblyHashAlgorithm.SHA_512:
					hashAlgorithm = SHA512.Create();
					break;
				default:
					hashAlgorithm = SHA1.Create();
					break;
				}
			}
			else
			{
				hashAlgorithm = MD5.Create();
			}
			this.hasher = hashAlgorithm;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x00167CD0 File Offset: 0x00167CD0
		public void Dispose()
		{
			if (this.hasher != null)
			{
				((IDisposable)this.hasher).Dispose();
			}
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x00167CE8 File Offset: 0x00167CE8
		public static byte[] Hash(byte[] data, AssemblyHashAlgorithm hashAlgo)
		{
			if (data == null)
			{
				return null;
			}
			byte[] result;
			using (AssemblyHash assemblyHash = new AssemblyHash(hashAlgo))
			{
				assemblyHash.Hash(data);
				result = assemblyHash.ComputeHash();
			}
			return result;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x00167D3C File Offset: 0x00167D3C
		public void Hash(byte[] data)
		{
			this.Hash(data, 0, data.Length);
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x00167D4C File Offset: 0x00167D4C
		public void Hash(byte[] data, int offset, int length)
		{
			if (this.hasher.TransformBlock(data, offset, length, data, offset) != length)
			{
				throw new IOException("Could not calculate hash");
			}
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x00167D70 File Offset: 0x00167D70
		public void Hash(Stream stream, uint length, byte[] buffer)
		{
			while (length > 0U)
			{
				int num = (length > (uint)buffer.Length) ? buffer.Length : ((int)length);
				if (stream.Read(buffer, 0, num) != num)
				{
					throw new IOException("Could not read data");
				}
				this.Hash(buffer, 0, num);
				length -= (uint)num;
			}
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x00167DC8 File Offset: 0x00167DC8
		public byte[] ComputeHash()
		{
			this.hasher.TransformFinalBlock(Array2.Empty<byte>(), 0, 0);
			return this.hasher.Hash;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x00167DE8 File Offset: 0x00167DE8
		public static PublicKeyToken CreatePublicKeyToken(byte[] publicKeyData)
		{
			if (publicKeyData == null)
			{
				return new PublicKeyToken();
			}
			byte[] array = AssemblyHash.Hash(publicKeyData, AssemblyHashAlgorithm.SHA1);
			byte[] array2 = new byte[8];
			int num = 0;
			while (num < array2.Length && num < array.Length)
			{
				array2[num] = array[array.Length - num - 1];
				num++;
			}
			return new PublicKeyToken(array2);
		}

		// Token: 0x040023C8 RID: 9160
		private readonly HashAlgorithm hasher;
	}
}
