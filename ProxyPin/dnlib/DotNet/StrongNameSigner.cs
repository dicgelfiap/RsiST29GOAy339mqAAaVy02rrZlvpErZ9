using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace dnlib.DotNet
{
	// Token: 0x02000857 RID: 2135
	[ComVisible(true)]
	public readonly struct StrongNameSigner
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x00190370 File Offset: 0x00190370
		public StrongNameSigner(Stream stream)
		{
			this = new StrongNameSigner(stream, 0L);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x0019037C File Offset: 0x0019037C
		public StrongNameSigner(Stream stream, long baseOffset)
		{
			this.stream = stream;
			this.baseOffset = baseOffset;
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0019038C File Offset: 0x0019038C
		public byte[] WriteSignature(StrongNameKey snk, long snSigOffset)
		{
			byte[] array = this.CalculateSignature(snk, snSigOffset);
			this.stream.Position = this.baseOffset + snSigOffset;
			this.stream.Write(array, 0, array.Length);
			return array;
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x001903CC File Offset: 0x001903CC
		public byte[] CalculateSignature(StrongNameKey snk, long snSigOffset)
		{
			uint signatureSize = (uint)snk.SignatureSize;
			AssemblyHashAlgorithm hashAlg = (snk.HashAlgorithm == AssemblyHashAlgorithm.None) ? AssemblyHashAlgorithm.SHA1 : snk.HashAlgorithm;
			byte[] hash = this.StrongNameHashData(hashAlg, snSigOffset, signatureSize);
			byte[] strongNameSignature = this.GetStrongNameSignature(snk, hashAlg, hash);
			if ((long)strongNameSignature.Length != (long)((ulong)signatureSize))
			{
				throw new InvalidOperationException("Invalid strong name signature size");
			}
			return strongNameSignature;
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0019042C File Offset: 0x0019042C
		private byte[] StrongNameHashData(AssemblyHashAlgorithm hashAlg, long snSigOffset, uint snSigSize)
		{
			BinaryReader binaryReader = new BinaryReader(this.stream);
			snSigOffset += this.baseOffset;
			long num = snSigOffset + (long)((ulong)snSigSize);
			byte[] result;
			using (AssemblyHash assemblyHash = new AssemblyHash(hashAlg))
			{
				byte[] array = new byte[32768];
				this.stream.Position = this.baseOffset + 60L;
				uint length = binaryReader.ReadUInt32();
				this.stream.Position = this.baseOffset;
				assemblyHash.Hash(this.stream, length, array);
				this.stream.Position += 6L;
				int num2 = (int)binaryReader.ReadUInt16();
				this.stream.Position -= 8L;
				assemblyHash.Hash(this.stream, 24U, array);
				bool flag = binaryReader.ReadUInt16() == 267;
				this.stream.Position -= 2L;
				int num3 = flag ? 96 : 112;
				if (this.stream.Read(array, 0, num3) != num3)
				{
					throw new IOException("Could not read data");
				}
				for (int i = 0; i < 4; i++)
				{
					array[64 + i] = 0;
				}
				assemblyHash.Hash(array, 0, num3);
				if (this.stream.Read(array, 0, 128) != 128)
				{
					throw new IOException("Could not read data");
				}
				for (int j = 0; j < 8; j++)
				{
					array[32 + j] = 0;
				}
				assemblyHash.Hash(array, 0, 128);
				long position = this.stream.Position;
				assemblyHash.Hash(this.stream, (uint)(num2 * 40), array);
				for (int k = 0; k < num2; k++)
				{
					this.stream.Position = position + (long)(k * 40) + 16L;
					uint num4 = binaryReader.ReadUInt32();
					uint num5 = binaryReader.ReadUInt32();
					this.stream.Position = this.baseOffset + (long)((ulong)num5);
					while (num4 > 0U)
					{
						long position2 = this.stream.Position;
						if (snSigOffset <= position2 && position2 < num)
						{
							uint num6 = (uint)(num - position2);
							if (num6 >= num4)
							{
								break;
							}
							num4 -= num6;
							this.stream.Position += (long)((ulong)num6);
						}
						else
						{
							if (position2 >= num)
							{
								assemblyHash.Hash(this.stream, num4, array);
								break;
							}
							uint num7 = (uint)Math.Min(snSigOffset - position2, (long)((ulong)num4));
							assemblyHash.Hash(this.stream, num7, array);
							num4 -= num7;
						}
					}
				}
				result = assemblyHash.ComputeHash();
			}
			return result;
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x001906FC File Offset: 0x001906FC
		private byte[] GetStrongNameSignature(StrongNameKey snk, AssemblyHashAlgorithm hashAlg, byte[] hash)
		{
			byte[] result;
			using (RSA rsa = snk.CreateRSA())
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
				string hashAlgorithm = hashAlg.GetName() ?? AssemblyHashAlgorithm.SHA1.GetName();
				rsapkcs1SignatureFormatter.SetHashAlgorithm(hashAlgorithm);
				byte[] array = rsapkcs1SignatureFormatter.CreateSignature(hash);
				Array.Reverse(array);
				result = array;
			}
			return result;
		}

		// Token: 0x04002769 RID: 10089
		private readonly Stream stream;

		// Token: 0x0400276A RID: 10090
		private readonly long baseOffset;
	}
}
