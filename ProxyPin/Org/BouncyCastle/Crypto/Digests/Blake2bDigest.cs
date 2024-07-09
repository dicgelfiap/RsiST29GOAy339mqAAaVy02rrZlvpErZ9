using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200034D RID: 845
	public class Blake2bDigest : IDigest
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x00080FD8 File Offset: 0x00080FD8
		public Blake2bDigest() : this(512)
		{
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00080FE8 File Offset: 0x00080FE8
		public Blake2bDigest(Blake2bDigest digest)
		{
			this.digestLength = 64;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new ulong[16];
			this.chainValue = null;
			this.t0 = 0UL;
			this.t1 = 0UL;
			this.f0 = 0UL;
			base..ctor();
			this.bufferPos = digest.bufferPos;
			this.buffer = Arrays.Clone(digest.buffer);
			this.keyLength = digest.keyLength;
			this.key = Arrays.Clone(digest.key);
			this.digestLength = digest.digestLength;
			this.chainValue = Arrays.Clone(digest.chainValue);
			this.personalization = Arrays.Clone(digest.personalization);
			this.salt = Arrays.Clone(digest.salt);
			this.t0 = digest.t0;
			this.t1 = digest.t1;
			this.f0 = digest.f0;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000810FC File Offset: 0x000810FC
		public Blake2bDigest(int digestSize)
		{
			this.digestLength = 64;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new ulong[16];
			this.chainValue = null;
			this.t0 = 0UL;
			this.t1 = 0UL;
			this.f0 = 0UL;
			base..ctor();
			if (digestSize < 8 || digestSize > 512 || digestSize % 8 != 0)
			{
				throw new ArgumentException("BLAKE2b digest bit length must be a multiple of 8 and not greater than 512");
			}
			this.buffer = new byte[128];
			this.keyLength = 0;
			this.digestLength = digestSize / 8;
			this.Init();
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000811BC File Offset: 0x000811BC
		public Blake2bDigest(byte[] key)
		{
			this.digestLength = 64;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new ulong[16];
			this.chainValue = null;
			this.t0 = 0UL;
			this.t1 = 0UL;
			this.f0 = 0UL;
			base..ctor();
			this.buffer = new byte[128];
			if (key != null)
			{
				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);
				if (key.Length > 64)
				{
					throw new ArgumentException("Keys > 64 are not supported");
				}
				this.keyLength = key.Length;
				Array.Copy(key, 0, this.buffer, 0, key.Length);
				this.bufferPos = 128;
			}
			this.digestLength = 64;
			this.Init();
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000812B0 File Offset: 0x000812B0
		public Blake2bDigest(byte[] key, int digestLength, byte[] salt, byte[] personalization)
		{
			this.digestLength = 64;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new ulong[16];
			this.chainValue = null;
			this.t0 = 0UL;
			this.t1 = 0UL;
			this.f0 = 0UL;
			base..ctor();
			if (digestLength < 1 || digestLength > 64)
			{
				throw new ArgumentException("Invalid digest length (required: 1 - 64)");
			}
			this.digestLength = digestLength;
			this.buffer = new byte[128];
			if (salt != null)
			{
				if (salt.Length != 16)
				{
					throw new ArgumentException("salt length must be exactly 16 bytes");
				}
				this.salt = new byte[16];
				Array.Copy(salt, 0, this.salt, 0, salt.Length);
			}
			if (personalization != null)
			{
				if (personalization.Length != 16)
				{
					throw new ArgumentException("personalization length must be exactly 16 bytes");
				}
				this.personalization = new byte[16];
				Array.Copy(personalization, 0, this.personalization, 0, personalization.Length);
			}
			if (key != null)
			{
				if (key.Length > 64)
				{
					throw new ArgumentException("Keys > 64 are not supported");
				}
				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);
				this.keyLength = key.Length;
				Array.Copy(key, 0, this.buffer, 0, key.Length);
				this.bufferPos = 128;
			}
			this.Init();
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00081434 File Offset: 0x00081434
		private void Init()
		{
			if (this.chainValue == null)
			{
				this.chainValue = new ulong[8];
				this.chainValue[0] = (Blake2bDigest.blake2b_IV[0] ^ (ulong)((long)(this.digestLength | this.keyLength << 8 | 16842752)));
				this.chainValue[1] = Blake2bDigest.blake2b_IV[1];
				this.chainValue[2] = Blake2bDigest.blake2b_IV[2];
				this.chainValue[3] = Blake2bDigest.blake2b_IV[3];
				this.chainValue[4] = Blake2bDigest.blake2b_IV[4];
				this.chainValue[5] = Blake2bDigest.blake2b_IV[5];
				if (this.salt != null)
				{
					ulong[] array;
					(array = this.chainValue)[4] = (array[4] ^ Pack.LE_To_UInt64(this.salt, 0));
					(array = this.chainValue)[5] = (array[5] ^ Pack.LE_To_UInt64(this.salt, 8));
				}
				this.chainValue[6] = Blake2bDigest.blake2b_IV[6];
				this.chainValue[7] = Blake2bDigest.blake2b_IV[7];
				if (this.personalization != null)
				{
					ulong[] array;
					(array = this.chainValue)[6] = (array[6] ^ Pack.LE_To_UInt64(this.personalization, 0));
					(array = this.chainValue)[7] = (array[7] ^ Pack.LE_To_UInt64(this.personalization, 8));
				}
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0008156C File Offset: 0x0008156C
		private void InitializeInternalState()
		{
			Array.Copy(this.chainValue, 0, this.internalState, 0, this.chainValue.Length);
			Array.Copy(Blake2bDigest.blake2b_IV, 0, this.internalState, this.chainValue.Length, 4);
			this.internalState[12] = (this.t0 ^ Blake2bDigest.blake2b_IV[4]);
			this.internalState[13] = (this.t1 ^ Blake2bDigest.blake2b_IV[5]);
			this.internalState[14] = (this.f0 ^ Blake2bDigest.blake2b_IV[6]);
			this.internalState[15] = Blake2bDigest.blake2b_IV[7];
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00081608 File Offset: 0x00081608
		public virtual void Update(byte b)
		{
			if (128 - this.bufferPos == 0)
			{
				this.t0 += 128UL;
				if (this.t0 == 0UL)
				{
					this.t1 += 1UL;
				}
				this.Compress(this.buffer, 0);
				Array.Clear(this.buffer, 0, this.buffer.Length);
				this.buffer[0] = b;
				this.bufferPos = 1;
				return;
			}
			this.buffer[this.bufferPos] = b;
			this.bufferPos++;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000816AC File Offset: 0x000816AC
		public virtual void BlockUpdate(byte[] message, int offset, int len)
		{
			if (message == null || len == 0)
			{
				return;
			}
			int num = 0;
			if (this.bufferPos != 0)
			{
				num = 128 - this.bufferPos;
				if (num >= len)
				{
					Array.Copy(message, offset, this.buffer, this.bufferPos, len);
					this.bufferPos += len;
					return;
				}
				Array.Copy(message, offset, this.buffer, this.bufferPos, num);
				this.t0 += 128UL;
				if (this.t0 == 0UL)
				{
					this.t1 += 1UL;
				}
				this.Compress(this.buffer, 0);
				this.bufferPos = 0;
				Array.Clear(this.buffer, 0, this.buffer.Length);
			}
			int num2 = offset + len - 128;
			int i;
			for (i = offset + num; i < num2; i += 128)
			{
				this.t0 += 128UL;
				if (this.t0 == 0UL)
				{
					this.t1 += 1UL;
				}
				this.Compress(message, i);
			}
			Array.Copy(message, i, this.buffer, 0, offset + len - i);
			this.bufferPos += offset + len - i;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000817FC File Offset: 0x000817FC
		public virtual int DoFinal(byte[] output, int outOffset)
		{
			this.f0 = ulong.MaxValue;
			this.t0 += (ulong)((long)this.bufferPos);
			if (this.bufferPos > 0 && this.t0 == 0UL)
			{
				this.t1 += 1UL;
			}
			this.Compress(this.buffer, 0);
			Array.Clear(this.buffer, 0, this.buffer.Length);
			Array.Clear(this.internalState, 0, this.internalState.Length);
			int num = 0;
			while (num < this.chainValue.Length && num * 8 < this.digestLength)
			{
				byte[] sourceArray = Pack.UInt64_To_LE(this.chainValue[num]);
				if (num * 8 < this.digestLength - 8)
				{
					Array.Copy(sourceArray, 0, output, outOffset + num * 8, 8);
				}
				else
				{
					Array.Copy(sourceArray, 0, output, outOffset + num * 8, this.digestLength - num * 8);
				}
				num++;
			}
			Array.Clear(this.chainValue, 0, this.chainValue.Length);
			this.Reset();
			return this.digestLength;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00081914 File Offset: 0x00081914
		public virtual void Reset()
		{
			this.bufferPos = 0;
			this.f0 = 0UL;
			this.t0 = 0UL;
			this.t1 = 0UL;
			this.chainValue = null;
			Array.Clear(this.buffer, 0, this.buffer.Length);
			if (this.key != null)
			{
				Array.Copy(this.key, 0, this.buffer, 0, this.key.Length);
				this.bufferPos = 128;
			}
			this.Init();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00081998 File Offset: 0x00081998
		private void Compress(byte[] message, int messagePos)
		{
			this.InitializeInternalState();
			ulong[] array = new ulong[16];
			for (int i = 0; i < 16; i++)
			{
				array[i] = Pack.LE_To_UInt64(message, messagePos + i * 8);
			}
			for (int j = 0; j < 12; j++)
			{
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 0]], array[(int)Blake2bDigest.blake2b_sigma[j, 1]], 0, 4, 8, 12);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 2]], array[(int)Blake2bDigest.blake2b_sigma[j, 3]], 1, 5, 9, 13);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 4]], array[(int)Blake2bDigest.blake2b_sigma[j, 5]], 2, 6, 10, 14);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 6]], array[(int)Blake2bDigest.blake2b_sigma[j, 7]], 3, 7, 11, 15);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 8]], array[(int)Blake2bDigest.blake2b_sigma[j, 9]], 0, 5, 10, 15);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 10]], array[(int)Blake2bDigest.blake2b_sigma[j, 11]], 1, 6, 11, 12);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 12]], array[(int)Blake2bDigest.blake2b_sigma[j, 13]], 2, 7, 8, 13);
				this.G(array[(int)Blake2bDigest.blake2b_sigma[j, 14]], array[(int)Blake2bDigest.blake2b_sigma[j, 15]], 3, 4, 9, 14);
			}
			for (int k = 0; k < this.chainValue.Length; k++)
			{
				this.chainValue[k] = (this.chainValue[k] ^ this.internalState[k] ^ this.internalState[k + 8]);
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00081B68 File Offset: 0x00081B68
		private void G(ulong m1, ulong m2, int posA, int posB, int posC, int posD)
		{
			this.internalState[posA] = this.internalState[posA] + this.internalState[posB] + m1;
			this.internalState[posD] = Blake2bDigest.Rotr64(this.internalState[posD] ^ this.internalState[posA], 32);
			this.internalState[posC] = this.internalState[posC] + this.internalState[posD];
			this.internalState[posB] = Blake2bDigest.Rotr64(this.internalState[posB] ^ this.internalState[posC], 24);
			this.internalState[posA] = this.internalState[posA] + this.internalState[posB] + m2;
			this.internalState[posD] = Blake2bDigest.Rotr64(this.internalState[posD] ^ this.internalState[posA], 16);
			this.internalState[posC] = this.internalState[posC] + this.internalState[posD];
			this.internalState[posB] = Blake2bDigest.Rotr64(this.internalState[posB] ^ this.internalState[posC], 63);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00081C74 File Offset: 0x00081C74
		private static ulong Rotr64(ulong x, int rot)
		{
			return x >> rot | x << -rot;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00081C84 File Offset: 0x00081C84
		public virtual string AlgorithmName
		{
			get
			{
				return "BLAKE2b";
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00081C8C File Offset: 0x00081C8C
		public virtual int GetDigestSize()
		{
			return this.digestLength;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00081C94 File Offset: 0x00081C94
		public virtual int GetByteLength()
		{
			return 128;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00081C9C File Offset: 0x00081C9C
		public virtual void ClearKey()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
				Array.Clear(this.buffer, 0, this.buffer.Length);
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00081CD4 File Offset: 0x00081CD4
		public virtual void ClearSalt()
		{
			if (this.salt != null)
			{
				Array.Clear(this.salt, 0, this.salt.Length);
			}
		}

		// Token: 0x040010D9 RID: 4313
		private const int ROUNDS = 12;

		// Token: 0x040010DA RID: 4314
		private const int BLOCK_LENGTH_BYTES = 128;

		// Token: 0x040010DB RID: 4315
		private static readonly ulong[] blake2b_IV = new ulong[]
		{
			7640891576956012808UL,
			13503953896175478587UL,
			4354685564936845355UL,
			11912009170470909681UL,
			5840696475078001361UL,
			11170449401992604703UL,
			2270897969802886507UL,
			6620516959819538809UL
		};

		// Token: 0x040010DC RID: 4316
		private static readonly byte[,] blake2b_sigma = new byte[,]
		{
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15
			},
			{
				14,
				10,
				4,
				8,
				9,
				15,
				13,
				6,
				1,
				12,
				0,
				2,
				11,
				7,
				5,
				3
			},
			{
				11,
				8,
				12,
				0,
				5,
				2,
				15,
				13,
				10,
				14,
				3,
				6,
				7,
				1,
				9,
				4
			},
			{
				7,
				9,
				3,
				1,
				13,
				12,
				11,
				14,
				2,
				6,
				5,
				10,
				4,
				0,
				15,
				8
			},
			{
				9,
				0,
				5,
				7,
				2,
				4,
				10,
				15,
				14,
				1,
				11,
				12,
				6,
				8,
				3,
				13
			},
			{
				2,
				12,
				6,
				10,
				0,
				11,
				8,
				3,
				4,
				13,
				7,
				5,
				15,
				14,
				1,
				9
			},
			{
				12,
				5,
				1,
				15,
				14,
				13,
				4,
				10,
				0,
				7,
				6,
				3,
				9,
				2,
				8,
				11
			},
			{
				13,
				11,
				7,
				14,
				12,
				1,
				3,
				9,
				5,
				0,
				15,
				4,
				8,
				6,
				2,
				10
			},
			{
				6,
				15,
				14,
				9,
				11,
				3,
				0,
				8,
				12,
				2,
				13,
				7,
				1,
				4,
				10,
				5
			},
			{
				10,
				2,
				8,
				4,
				7,
				6,
				1,
				5,
				15,
				11,
				9,
				14,
				3,
				12,
				13,
				0
			},
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15
			},
			{
				14,
				10,
				4,
				8,
				9,
				15,
				13,
				6,
				1,
				12,
				0,
				2,
				11,
				7,
				5,
				3
			}
		};

		// Token: 0x040010DD RID: 4317
		private int digestLength;

		// Token: 0x040010DE RID: 4318
		private int keyLength;

		// Token: 0x040010DF RID: 4319
		private byte[] salt;

		// Token: 0x040010E0 RID: 4320
		private byte[] personalization;

		// Token: 0x040010E1 RID: 4321
		private byte[] key;

		// Token: 0x040010E2 RID: 4322
		private byte[] buffer;

		// Token: 0x040010E3 RID: 4323
		private int bufferPos;

		// Token: 0x040010E4 RID: 4324
		private ulong[] internalState;

		// Token: 0x040010E5 RID: 4325
		private ulong[] chainValue;

		// Token: 0x040010E6 RID: 4326
		private ulong t0;

		// Token: 0x040010E7 RID: 4327
		private ulong t1;

		// Token: 0x040010E8 RID: 4328
		private ulong f0;
	}
}
