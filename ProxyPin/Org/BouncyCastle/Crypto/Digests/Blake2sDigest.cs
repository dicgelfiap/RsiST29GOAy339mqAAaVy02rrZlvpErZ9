using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200034E RID: 846
	public class Blake2sDigest : IDigest
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00081D2C File Offset: 0x00081D2C
		public Blake2sDigest() : this(256)
		{
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00081D3C File Offset: 0x00081D3C
		public Blake2sDigest(Blake2sDigest digest)
		{
			this.digestLength = 32;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new uint[16];
			this.chainValue = null;
			this.t0 = 0U;
			this.t1 = 0U;
			this.f0 = 0U;
			base..ctor();
			this.bufferPos = digest.bufferPos;
			this.buffer = Arrays.Clone(digest.buffer);
			this.keyLength = digest.keyLength;
			this.key = Arrays.Clone(digest.key);
			this.digestLength = digest.digestLength;
			this.chainValue = Arrays.Clone(digest.chainValue);
			this.personalization = Arrays.Clone(digest.personalization);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00081E18 File Offset: 0x00081E18
		public Blake2sDigest(int digestBits)
		{
			this.digestLength = 32;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new uint[16];
			this.chainValue = null;
			this.t0 = 0U;
			this.t1 = 0U;
			this.f0 = 0U;
			base..ctor();
			if (digestBits < 8 || digestBits > 256 || digestBits % 8 != 0)
			{
				throw new ArgumentException("BLAKE2s digest bit length must be a multiple of 8 and not greater than 256");
			}
			this.buffer = new byte[64];
			this.keyLength = 0;
			this.digestLength = digestBits / 8;
			this.Init();
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00081ED4 File Offset: 0x00081ED4
		public Blake2sDigest(byte[] key)
		{
			this.digestLength = 32;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new uint[16];
			this.chainValue = null;
			this.t0 = 0U;
			this.t1 = 0U;
			this.f0 = 0U;
			base..ctor();
			this.buffer = new byte[64];
			if (key != null)
			{
				if (key.Length > 32)
				{
					throw new ArgumentException("Keys > 32 are not supported");
				}
				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);
				this.keyLength = key.Length;
				Array.Copy(key, 0, this.buffer, 0, key.Length);
				this.bufferPos = 64;
			}
			this.digestLength = 32;
			this.Init();
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00081FC0 File Offset: 0x00081FC0
		public Blake2sDigest(byte[] key, int digestBytes, byte[] salt, byte[] personalization)
		{
			this.digestLength = 32;
			this.keyLength = 0;
			this.salt = null;
			this.personalization = null;
			this.key = null;
			this.buffer = null;
			this.bufferPos = 0;
			this.internalState = new uint[16];
			this.chainValue = null;
			this.t0 = 0U;
			this.t1 = 0U;
			this.f0 = 0U;
			base..ctor();
			if (digestBytes < 1 || digestBytes > 32)
			{
				throw new ArgumentException("Invalid digest length (required: 1 - 32)");
			}
			this.digestLength = digestBytes;
			this.buffer = new byte[64];
			if (salt != null)
			{
				if (salt.Length != 8)
				{
					throw new ArgumentException("Salt length must be exactly 8 bytes");
				}
				this.salt = new byte[8];
				Array.Copy(salt, 0, this.salt, 0, salt.Length);
			}
			if (personalization != null)
			{
				if (personalization.Length != 8)
				{
					throw new ArgumentException("Personalization length must be exactly 8 bytes");
				}
				this.personalization = new byte[8];
				Array.Copy(personalization, 0, this.personalization, 0, personalization.Length);
			}
			if (key != null)
			{
				if (key.Length > 32)
				{
					throw new ArgumentException("Keys > 32 bytes are not supported");
				}
				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);
				this.keyLength = key.Length;
				Array.Copy(key, 0, this.buffer, 0, key.Length);
				this.bufferPos = 64;
			}
			this.Init();
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00082134 File Offset: 0x00082134
		private void Init()
		{
			if (this.chainValue == null)
			{
				this.chainValue = new uint[8];
				this.chainValue[0] = (Blake2sDigest.blake2s_IV[0] ^ (uint)(this.digestLength | this.keyLength << 8 | 16842752));
				this.chainValue[1] = Blake2sDigest.blake2s_IV[1];
				this.chainValue[2] = Blake2sDigest.blake2s_IV[2];
				this.chainValue[3] = Blake2sDigest.blake2s_IV[3];
				this.chainValue[4] = Blake2sDigest.blake2s_IV[4];
				this.chainValue[5] = Blake2sDigest.blake2s_IV[5];
				if (this.salt != null)
				{
					uint[] array;
					(array = this.chainValue)[4] = (array[4] ^ Pack.LE_To_UInt32(this.salt, 0));
					(array = this.chainValue)[5] = (array[5] ^ Pack.LE_To_UInt32(this.salt, 4));
				}
				this.chainValue[6] = Blake2sDigest.blake2s_IV[6];
				this.chainValue[7] = Blake2sDigest.blake2s_IV[7];
				if (this.personalization != null)
				{
					uint[] array;
					(array = this.chainValue)[6] = (array[6] ^ Pack.LE_To_UInt32(this.personalization, 0));
					(array = this.chainValue)[7] = (array[7] ^ Pack.LE_To_UInt32(this.personalization, 4));
				}
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00082268 File Offset: 0x00082268
		private void InitializeInternalState()
		{
			Array.Copy(this.chainValue, 0, this.internalState, 0, this.chainValue.Length);
			Array.Copy(Blake2sDigest.blake2s_IV, 0, this.internalState, this.chainValue.Length, 4);
			this.internalState[12] = (this.t0 ^ Blake2sDigest.blake2s_IV[4]);
			this.internalState[13] = (this.t1 ^ Blake2sDigest.blake2s_IV[5]);
			this.internalState[14] = (this.f0 ^ Blake2sDigest.blake2s_IV[6]);
			this.internalState[15] = Blake2sDigest.blake2s_IV[7];
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00082304 File Offset: 0x00082304
		public virtual void Update(byte b)
		{
			if (64 - this.bufferPos == 0)
			{
				this.t0 += 64U;
				if (this.t0 == 0U)
				{
					this.t1 += 1U;
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

		// Token: 0x0600193C RID: 6460 RVA: 0x0008239C File Offset: 0x0008239C
		public virtual void BlockUpdate(byte[] message, int offset, int len)
		{
			if (message == null || len == 0)
			{
				return;
			}
			int num = 0;
			if (this.bufferPos != 0)
			{
				num = 64 - this.bufferPos;
				if (num >= len)
				{
					Array.Copy(message, offset, this.buffer, this.bufferPos, len);
					this.bufferPos += len;
					return;
				}
				Array.Copy(message, offset, this.buffer, this.bufferPos, num);
				this.t0 += 64U;
				if (this.t0 == 0U)
				{
					this.t1 += 1U;
				}
				this.Compress(this.buffer, 0);
				this.bufferPos = 0;
				Array.Clear(this.buffer, 0, this.buffer.Length);
			}
			int num2 = offset + len - 64;
			int i;
			for (i = offset + num; i < num2; i += 64)
			{
				this.t0 += 64U;
				if (this.t0 == 0U)
				{
					this.t1 += 1U;
				}
				this.Compress(message, i);
			}
			Array.Copy(message, i, this.buffer, 0, offset + len - i);
			this.bufferPos += offset + len - i;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x000824D4 File Offset: 0x000824D4
		public virtual int DoFinal(byte[] output, int outOffset)
		{
			this.f0 = uint.MaxValue;
			this.t0 += (uint)this.bufferPos;
			if (this.t0 < 0U && (long)this.bufferPos > (long)(-(long)((ulong)this.t0)))
			{
				this.t1 += 1U;
			}
			this.Compress(this.buffer, 0);
			Array.Clear(this.buffer, 0, this.buffer.Length);
			Array.Clear(this.internalState, 0, this.internalState.Length);
			int num = 0;
			while (num < this.chainValue.Length && num * 4 < this.digestLength)
			{
				byte[] sourceArray = Pack.UInt32_To_LE(this.chainValue[num]);
				if (num * 4 < this.digestLength - 4)
				{
					Array.Copy(sourceArray, 0, output, outOffset + num * 4, 4);
				}
				else
				{
					Array.Copy(sourceArray, 0, output, outOffset + num * 4, this.digestLength - num * 4);
				}
				num++;
			}
			Array.Clear(this.chainValue, 0, this.chainValue.Length);
			this.Reset();
			return this.digestLength;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000825F0 File Offset: 0x000825F0
		public virtual void Reset()
		{
			this.bufferPos = 0;
			this.f0 = 0U;
			this.t0 = 0U;
			this.t1 = 0U;
			this.chainValue = null;
			Array.Clear(this.buffer, 0, this.buffer.Length);
			if (this.key != null)
			{
				Array.Copy(this.key, 0, this.buffer, 0, this.key.Length);
				this.bufferPos = 64;
			}
			this.Init();
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0008266C File Offset: 0x0008266C
		private void Compress(byte[] message, int messagePos)
		{
			this.InitializeInternalState();
			uint[] array = new uint[16];
			for (int i = 0; i < 16; i++)
			{
				array[i] = Pack.LE_To_UInt32(message, messagePos + i * 4);
			}
			for (int j = 0; j < 10; j++)
			{
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 0]], array[(int)Blake2sDigest.blake2s_sigma[j, 1]], 0, 4, 8, 12);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 2]], array[(int)Blake2sDigest.blake2s_sigma[j, 3]], 1, 5, 9, 13);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 4]], array[(int)Blake2sDigest.blake2s_sigma[j, 5]], 2, 6, 10, 14);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 6]], array[(int)Blake2sDigest.blake2s_sigma[j, 7]], 3, 7, 11, 15);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 8]], array[(int)Blake2sDigest.blake2s_sigma[j, 9]], 0, 5, 10, 15);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 10]], array[(int)Blake2sDigest.blake2s_sigma[j, 11]], 1, 6, 11, 12);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 12]], array[(int)Blake2sDigest.blake2s_sigma[j, 13]], 2, 7, 8, 13);
				this.G(array[(int)Blake2sDigest.blake2s_sigma[j, 14]], array[(int)Blake2sDigest.blake2s_sigma[j, 15]], 3, 4, 9, 14);
			}
			for (int k = 0; k < this.chainValue.Length; k++)
			{
				this.chainValue[k] = (this.chainValue[k] ^ this.internalState[k] ^ this.internalState[k + 8]);
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0008283C File Offset: 0x0008283C
		private void G(uint m1, uint m2, int posA, int posB, int posC, int posD)
		{
			this.internalState[posA] = this.internalState[posA] + this.internalState[posB] + m1;
			this.internalState[posD] = this.rotr32(this.internalState[posD] ^ this.internalState[posA], 16);
			this.internalState[posC] = this.internalState[posC] + this.internalState[posD];
			this.internalState[posB] = this.rotr32(this.internalState[posB] ^ this.internalState[posC], 12);
			this.internalState[posA] = this.internalState[posA] + this.internalState[posB] + m2;
			this.internalState[posD] = this.rotr32(this.internalState[posD] ^ this.internalState[posA], 8);
			this.internalState[posC] = this.internalState[posC] + this.internalState[posD];
			this.internalState[posB] = this.rotr32(this.internalState[posB] ^ this.internalState[posC], 7);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0008294C File Offset: 0x0008294C
		private uint rotr32(uint x, int rot)
		{
			return x >> rot | x << -rot;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0008295C File Offset: 0x0008295C
		public virtual string AlgorithmName
		{
			get
			{
				return "BLAKE2s";
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00082964 File Offset: 0x00082964
		public virtual int GetDigestSize()
		{
			return this.digestLength;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0008296C File Offset: 0x0008296C
		public virtual int GetByteLength()
		{
			return 64;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00082970 File Offset: 0x00082970
		public virtual void ClearKey()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
				Array.Clear(this.buffer, 0, this.buffer.Length);
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000829A8 File Offset: 0x000829A8
		public virtual void ClearSalt()
		{
			if (this.salt != null)
			{
				Array.Clear(this.salt, 0, this.salt.Length);
			}
		}

		// Token: 0x040010E9 RID: 4329
		private const int ROUNDS = 10;

		// Token: 0x040010EA RID: 4330
		private const int BLOCK_LENGTH_BYTES = 64;

		// Token: 0x040010EB RID: 4331
		private static readonly uint[] blake2s_IV = new uint[]
		{
			1779033703U,
			3144134277U,
			1013904242U,
			2773480762U,
			1359893119U,
			2600822924U,
			528734635U,
			1541459225U
		};

		// Token: 0x040010EC RID: 4332
		private static readonly byte[,] blake2s_sigma = new byte[,]
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
			}
		};

		// Token: 0x040010ED RID: 4333
		private int digestLength;

		// Token: 0x040010EE RID: 4334
		private int keyLength;

		// Token: 0x040010EF RID: 4335
		private byte[] salt;

		// Token: 0x040010F0 RID: 4336
		private byte[] personalization;

		// Token: 0x040010F1 RID: 4337
		private byte[] key;

		// Token: 0x040010F2 RID: 4338
		private byte[] buffer;

		// Token: 0x040010F3 RID: 4339
		private int bufferPos;

		// Token: 0x040010F4 RID: 4340
		private uint[] internalState;

		// Token: 0x040010F5 RID: 4341
		private uint[] chainValue;

		// Token: 0x040010F6 RID: 4342
		private uint t0;

		// Token: 0x040010F7 RID: 4343
		private uint t1;

		// Token: 0x040010F8 RID: 4344
		private uint f0;
	}
}
