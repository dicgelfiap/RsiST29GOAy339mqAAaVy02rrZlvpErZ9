using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003AA RID: 938
	public class ThreefishEngine : IBlockCipher
	{
		// Token: 0x06001DEA RID: 7658 RVA: 0x000AC6C0 File Offset: 0x000AC6C0
		static ThreefishEngine()
		{
			for (int i = 0; i < ThreefishEngine.MOD9.Length; i++)
			{
				ThreefishEngine.MOD17[i] = i % 17;
				ThreefishEngine.MOD9[i] = i % 9;
				ThreefishEngine.MOD5[i] = i % 5;
				ThreefishEngine.MOD3[i] = i % 3;
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000AC750 File Offset: 0x000AC750
		public ThreefishEngine(int blocksizeBits)
		{
			this.blocksizeBytes = blocksizeBits / 8;
			this.blocksizeWords = this.blocksizeBytes / 8;
			this.currentBlock = new ulong[this.blocksizeWords];
			this.kw = new ulong[2 * this.blocksizeWords + 1];
			if (blocksizeBits == 256)
			{
				this.cipher = new ThreefishEngine.Threefish256Cipher(this.kw, this.t);
				return;
			}
			if (blocksizeBits == 512)
			{
				this.cipher = new ThreefishEngine.Threefish512Cipher(this.kw, this.t);
				return;
			}
			if (blocksizeBits != 1024)
			{
				throw new ArgumentException("Invalid blocksize - Threefish is defined with block size of 256, 512, or 1024 bits");
			}
			this.cipher = new ThreefishEngine.Threefish1024Cipher(this.kw, this.t);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000AC82C File Offset: 0x000AC82C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			byte[] key;
			byte[] array;
			if (parameters is TweakableBlockCipherParameters)
			{
				TweakableBlockCipherParameters tweakableBlockCipherParameters = (TweakableBlockCipherParameters)parameters;
				key = tweakableBlockCipherParameters.Key.GetKey();
				array = tweakableBlockCipherParameters.Tweak;
			}
			else
			{
				if (!(parameters is KeyParameter))
				{
					throw new ArgumentException("Invalid parameter passed to Threefish init - " + Platform.GetTypeName(parameters));
				}
				key = ((KeyParameter)parameters).GetKey();
				array = null;
			}
			ulong[] array2 = null;
			ulong[] tweak = null;
			if (key != null)
			{
				if (key.Length != this.blocksizeBytes)
				{
					throw new ArgumentException("Threefish key must be same size as block (" + this.blocksizeBytes + " bytes)");
				}
				array2 = new ulong[this.blocksizeWords];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = ThreefishEngine.BytesToWord(key, i * 8);
				}
			}
			if (array != null)
			{
				if (array.Length != 16)
				{
					throw new ArgumentException("Threefish tweak must be " + 16 + " bytes");
				}
				tweak = new ulong[]
				{
					ThreefishEngine.BytesToWord(array, 0),
					ThreefishEngine.BytesToWord(array, 8)
				};
			}
			this.Init(forEncryption, array2, tweak);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000AC95C File Offset: 0x000AC95C
		internal void Init(bool forEncryption, ulong[] key, ulong[] tweak)
		{
			this.forEncryption = forEncryption;
			if (key != null)
			{
				this.SetKey(key);
			}
			if (tweak != null)
			{
				this.SetTweak(tweak);
			}
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000AC980 File Offset: 0x000AC980
		private void SetKey(ulong[] key)
		{
			if (key.Length != this.blocksizeWords)
			{
				throw new ArgumentException("Threefish key must be same size as block (" + this.blocksizeWords + " words)");
			}
			ulong num = 2004413935125273122UL;
			for (int i = 0; i < this.blocksizeWords; i++)
			{
				this.kw[i] = key[i];
				num ^= this.kw[i];
			}
			this.kw[this.blocksizeWords] = num;
			Array.Copy(this.kw, 0, this.kw, this.blocksizeWords + 1, this.blocksizeWords);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000ACA24 File Offset: 0x000ACA24
		private void SetTweak(ulong[] tweak)
		{
			if (tweak.Length != 2)
			{
				throw new ArgumentException("Tweak must be " + 2 + " words.");
			}
			this.t[0] = tweak[0];
			this.t[1] = tweak[1];
			this.t[2] = (this.t[0] ^ this.t[1]);
			this.t[3] = this.t[0];
			this.t[4] = this.t[1];
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x000ACAA8 File Offset: 0x000ACAA8
		public virtual string AlgorithmName
		{
			get
			{
				return "Threefish-" + this.blocksizeBytes * 8;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000ACAC4 File Offset: 0x000ACAC4
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000ACAC8 File Offset: 0x000ACAC8
		public virtual int GetBlockSize()
		{
			return this.blocksizeBytes;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000ACAD0 File Offset: 0x000ACAD0
		public virtual void Reset()
		{
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000ACAD4 File Offset: 0x000ACAD4
		public virtual int ProcessBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			if (outOff + this.blocksizeBytes > outBytes.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (inOff + this.blocksizeBytes > inBytes.Length)
			{
				throw new DataLengthException("Input buffer too short");
			}
			for (int i = 0; i < this.blocksizeBytes; i += 8)
			{
				this.currentBlock[i >> 3] = ThreefishEngine.BytesToWord(inBytes, inOff + i);
			}
			this.ProcessBlock(this.currentBlock, this.currentBlock);
			for (int j = 0; j < this.blocksizeBytes; j += 8)
			{
				ThreefishEngine.WordToBytes(this.currentBlock[j >> 3], outBytes, outOff + j);
			}
			return this.blocksizeBytes;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000ACB84 File Offset: 0x000ACB84
		internal int ProcessBlock(ulong[] inWords, ulong[] outWords)
		{
			if (this.kw[this.blocksizeWords] == 0UL)
			{
				throw new InvalidOperationException("Threefish engine not initialised");
			}
			if (inWords.Length != this.blocksizeWords)
			{
				throw new DataLengthException("Input buffer too short");
			}
			if (outWords.Length != this.blocksizeWords)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (this.forEncryption)
			{
				this.cipher.EncryptBlock(inWords, outWords);
			}
			else
			{
				this.cipher.DecryptBlock(inWords, outWords);
			}
			return this.blocksizeWords;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000ACC18 File Offset: 0x000ACC18
		internal static ulong BytesToWord(byte[] bytes, int off)
		{
			if (off + 8 > bytes.Length)
			{
				throw new ArgumentException();
			}
			int num = off + 1;
			ulong num2 = (ulong)bytes[off] & 255UL;
			num2 |= ((ulong)bytes[num++] & 255UL) << 8;
			num2 |= ((ulong)bytes[num++] & 255UL) << 16;
			num2 |= ((ulong)bytes[num++] & 255UL) << 24;
			num2 |= ((ulong)bytes[num++] & 255UL) << 32;
			num2 |= ((ulong)bytes[num++] & 255UL) << 40;
			num2 |= ((ulong)bytes[num++] & 255UL) << 48;
			return num2 | ((ulong)bytes[num++] & 255UL) << 56;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000ACCE4 File Offset: 0x000ACCE4
		internal static void WordToBytes(ulong word, byte[] bytes, int off)
		{
			if (off + 8 > bytes.Length)
			{
				throw new ArgumentException();
			}
			int num = off + 1;
			bytes[off] = (byte)word;
			bytes[num++] = (byte)(word >> 8);
			bytes[num++] = (byte)(word >> 16);
			bytes[num++] = (byte)(word >> 24);
			bytes[num++] = (byte)(word >> 32);
			bytes[num++] = (byte)(word >> 40);
			bytes[num++] = (byte)(word >> 48);
			bytes[num++] = (byte)(word >> 56);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000ACD64 File Offset: 0x000ACD64
		private static ulong RotlXor(ulong x, int n, ulong xor)
		{
			return (x << n | x >> 64 - n) ^ xor;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000ACD78 File Offset: 0x000ACD78
		private static ulong XorRotr(ulong x, int n, ulong xor)
		{
			ulong num = x ^ xor;
			return num >> n | num << 64 - n;
		}

		// Token: 0x040013AF RID: 5039
		public const int BLOCKSIZE_256 = 256;

		// Token: 0x040013B0 RID: 5040
		public const int BLOCKSIZE_512 = 512;

		// Token: 0x040013B1 RID: 5041
		public const int BLOCKSIZE_1024 = 1024;

		// Token: 0x040013B2 RID: 5042
		private const int TWEAK_SIZE_BYTES = 16;

		// Token: 0x040013B3 RID: 5043
		private const int TWEAK_SIZE_WORDS = 2;

		// Token: 0x040013B4 RID: 5044
		private const int ROUNDS_256 = 72;

		// Token: 0x040013B5 RID: 5045
		private const int ROUNDS_512 = 72;

		// Token: 0x040013B6 RID: 5046
		private const int ROUNDS_1024 = 80;

		// Token: 0x040013B7 RID: 5047
		private const int MAX_ROUNDS = 80;

		// Token: 0x040013B8 RID: 5048
		private const ulong C_240 = 2004413935125273122UL;

		// Token: 0x040013B9 RID: 5049
		private static readonly int[] MOD9 = new int[80];

		// Token: 0x040013BA RID: 5050
		private static readonly int[] MOD17 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x040013BB RID: 5051
		private static readonly int[] MOD5 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x040013BC RID: 5052
		private static readonly int[] MOD3 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x040013BD RID: 5053
		private readonly int blocksizeBytes;

		// Token: 0x040013BE RID: 5054
		private readonly int blocksizeWords;

		// Token: 0x040013BF RID: 5055
		private readonly ulong[] currentBlock;

		// Token: 0x040013C0 RID: 5056
		private readonly ulong[] t = new ulong[5];

		// Token: 0x040013C1 RID: 5057
		private readonly ulong[] kw;

		// Token: 0x040013C2 RID: 5058
		private readonly ThreefishEngine.ThreefishCipher cipher;

		// Token: 0x040013C3 RID: 5059
		private bool forEncryption;

		// Token: 0x02000E08 RID: 3592
		private abstract class ThreefishCipher
		{
			// Token: 0x06008C09 RID: 35849 RVA: 0x0029FB04 File Offset: 0x0029FB04
			protected ThreefishCipher(ulong[] kw, ulong[] t)
			{
				this.kw = kw;
				this.t = t;
			}

			// Token: 0x06008C0A RID: 35850
			internal abstract void EncryptBlock(ulong[] block, ulong[] outWords);

			// Token: 0x06008C0B RID: 35851
			internal abstract void DecryptBlock(ulong[] block, ulong[] outWords);

			// Token: 0x040040C5 RID: 16581
			protected readonly ulong[] t;

			// Token: 0x040040C6 RID: 16582
			protected readonly ulong[] kw;
		}

		// Token: 0x02000E09 RID: 3593
		private sealed class Threefish256Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06008C0C RID: 35852 RVA: 0x0029FB1C File Offset: 0x0029FB1C
			public Threefish256Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06008C0D RID: 35853 RVA: 0x0029FB28 File Offset: 0x0029FB28
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD5;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 9)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				num += kw[0];
				num2 += kw[1] + t[0];
				num3 += kw[2] + t[1];
				num4 += kw[3];
				for (int i = 1; i < 18; i += 2)
				{
					int num5 = mod[i];
					int num6 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 14, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 16, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 52, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 57, num3 += num2);
					num2 = ThreefishEngine.RotlXor(num2, 23, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 40, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 5, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 37, num3 += num2);
					num += kw[num5];
					num2 += kw[num5 + 1] + t[num6];
					num3 += kw[num5 + 2] + t[num6 + 1];
					num4 += kw[num5 + 3] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 25, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 33, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 46, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 12, num3 += num2);
					num2 = ThreefishEngine.RotlXor(num2, 58, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 22, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 32, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 32, num3 += num2);
					num += kw[num5 + 1];
					num2 += kw[num5 + 2] + t[num6 + 1];
					num3 += kw[num5 + 3] + t[num6 + 2];
					num4 += kw[num5 + 4] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
			}

			// Token: 0x06008C0E RID: 35854 RVA: 0x0029FD90 File Offset: 0x0029FD90
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD5;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 9)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				for (int i = 17; i >= 1; i -= 2)
				{
					int num5 = mod[i];
					int num6 = mod2[i];
					num -= kw[num5 + 1];
					num2 -= kw[num5 + 2] + t[num6 + 1];
					num3 -= kw[num5 + 3] + t[num6 + 2];
					num4 -= kw[num5 + 4] + (ulong)i + 1UL;
					num4 = ThreefishEngine.XorRotr(num4, 32, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 32, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 58, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 22, num3);
					num3 -= num4;
					num4 = ThreefishEngine.XorRotr(num4, 46, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 12, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 25, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 33, num3);
					num3 -= num4;
					num -= kw[num5];
					num2 -= kw[num5 + 1] + t[num6];
					num3 -= kw[num5 + 2] + t[num6 + 1];
					num4 -= kw[num5 + 3] + (ulong)i;
					num4 = ThreefishEngine.XorRotr(num4, 5, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 37, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 23, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 40, num3);
					num3 -= num4;
					num4 = ThreefishEngine.XorRotr(num4, 52, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 57, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 14, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 16, num3);
					num3 -= num4;
				}
				num -= kw[0];
				num2 -= kw[1] + t[0];
				num3 -= kw[2] + t[1];
				num4 -= kw[3];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
			}

			// Token: 0x040040C7 RID: 16583
			private const int ROTATION_0_0 = 14;

			// Token: 0x040040C8 RID: 16584
			private const int ROTATION_0_1 = 16;

			// Token: 0x040040C9 RID: 16585
			private const int ROTATION_1_0 = 52;

			// Token: 0x040040CA RID: 16586
			private const int ROTATION_1_1 = 57;

			// Token: 0x040040CB RID: 16587
			private const int ROTATION_2_0 = 23;

			// Token: 0x040040CC RID: 16588
			private const int ROTATION_2_1 = 40;

			// Token: 0x040040CD RID: 16589
			private const int ROTATION_3_0 = 5;

			// Token: 0x040040CE RID: 16590
			private const int ROTATION_3_1 = 37;

			// Token: 0x040040CF RID: 16591
			private const int ROTATION_4_0 = 25;

			// Token: 0x040040D0 RID: 16592
			private const int ROTATION_4_1 = 33;

			// Token: 0x040040D1 RID: 16593
			private const int ROTATION_5_0 = 46;

			// Token: 0x040040D2 RID: 16594
			private const int ROTATION_5_1 = 12;

			// Token: 0x040040D3 RID: 16595
			private const int ROTATION_6_0 = 58;

			// Token: 0x040040D4 RID: 16596
			private const int ROTATION_6_1 = 22;

			// Token: 0x040040D5 RID: 16597
			private const int ROTATION_7_0 = 32;

			// Token: 0x040040D6 RID: 16598
			private const int ROTATION_7_1 = 32;
		}

		// Token: 0x02000E0A RID: 3594
		private sealed class Threefish512Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06008C0F RID: 35855 RVA: 0x002A0008 File Offset: 0x002A0008
			internal Threefish512Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06008C10 RID: 35856 RVA: 0x002A0014 File Offset: 0x002A0014
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD9;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 17)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				num += kw[0];
				num2 += kw[1];
				num3 += kw[2];
				num4 += kw[3];
				num5 += kw[4];
				num6 += kw[5] + t[0];
				num7 += kw[6] + t[1];
				num8 += kw[7];
				for (int i = 1; i < 18; i += 2)
				{
					int num9 = mod[i];
					int num10 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 46, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 36, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 19, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 37, num7 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 33, num3 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 27, num5 += num8);
					num6 = ThreefishEngine.RotlXor(num6, 14, num7 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 42, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 17, num5 += num2);
					num4 = ThreefishEngine.RotlXor(num4, 49, num7 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 36, num += num6);
					num8 = ThreefishEngine.RotlXor(num8, 39, num3 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 44, num7 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 9, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 54, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 56, num5 += num4);
					num += kw[num9];
					num2 += kw[num9 + 1];
					num3 += kw[num9 + 2];
					num4 += kw[num9 + 3];
					num5 += kw[num9 + 4];
					num6 += kw[num9 + 5] + t[num10];
					num7 += kw[num9 + 6] + t[num10 + 1];
					num8 += kw[num9 + 7] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 39, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 30, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 34, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 24, num7 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 13, num3 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 50, num5 += num8);
					num6 = ThreefishEngine.RotlXor(num6, 10, num7 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 17, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 25, num5 += num2);
					num4 = ThreefishEngine.RotlXor(num4, 29, num7 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 39, num += num6);
					num8 = ThreefishEngine.RotlXor(num8, 43, num3 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 8, num7 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 35, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 56, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 22, num5 += num4);
					num += kw[num9 + 1];
					num2 += kw[num9 + 2];
					num3 += kw[num9 + 3];
					num4 += kw[num9 + 4];
					num5 += kw[num9 + 5];
					num6 += kw[num9 + 6] + t[num10 + 1];
					num7 += kw[num9 + 7] + t[num10 + 2];
					num8 += kw[num9 + 8] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
				outWords[4] = num5;
				outWords[5] = num6;
				outWords[6] = num7;
				outWords[7] = num8;
			}

			// Token: 0x06008C11 RID: 35857 RVA: 0x002A044C File Offset: 0x002A044C
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD9;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 17)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				for (int i = 17; i >= 1; i -= 2)
				{
					int num9 = mod[i];
					int num10 = mod2[i];
					num -= kw[num9 + 1];
					num2 -= kw[num9 + 2];
					num3 -= kw[num9 + 3];
					num4 -= kw[num9 + 4];
					num5 -= kw[num9 + 5];
					num6 -= kw[num9 + 6] + t[num10 + 1];
					num7 -= kw[num9 + 7] + t[num10 + 2];
					num8 -= kw[num9 + 8] + (ulong)i + 1UL;
					num2 = ThreefishEngine.XorRotr(num2, 8, num7);
					num7 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 35, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 56, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 22, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 25, num5);
					num5 -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 29, num7);
					num7 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 39, num);
					num -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 43, num3);
					num3 -= num8;
					num2 = ThreefishEngine.XorRotr(num2, 13, num3);
					num3 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 50, num5);
					num5 -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 10, num7);
					num7 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 17, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 39, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 30, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 34, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 24, num7);
					num7 -= num8;
					num -= kw[num9];
					num2 -= kw[num9 + 1];
					num3 -= kw[num9 + 2];
					num4 -= kw[num9 + 3];
					num5 -= kw[num9 + 4];
					num6 -= kw[num9 + 5] + t[num10];
					num7 -= kw[num9 + 6] + t[num10 + 1];
					num8 -= kw[num9 + 7] + (ulong)i;
					num2 = ThreefishEngine.XorRotr(num2, 44, num7);
					num7 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 9, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 54, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 56, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 17, num5);
					num5 -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 49, num7);
					num7 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 36, num);
					num -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 39, num3);
					num3 -= num8;
					num2 = ThreefishEngine.XorRotr(num2, 33, num3);
					num3 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 27, num5);
					num5 -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 14, num7);
					num7 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 42, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 46, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 36, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 19, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 37, num7);
					num7 -= num8;
				}
				num -= kw[0];
				num2 -= kw[1];
				num3 -= kw[2];
				num4 -= kw[3];
				num5 -= kw[4];
				num6 -= kw[5] + t[0];
				num7 -= kw[6] + t[1];
				num8 -= kw[7];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
				state[4] = num5;
				state[5] = num6;
				state[6] = num7;
				state[7] = num8;
			}

			// Token: 0x040040D7 RID: 16599
			private const int ROTATION_0_0 = 46;

			// Token: 0x040040D8 RID: 16600
			private const int ROTATION_0_1 = 36;

			// Token: 0x040040D9 RID: 16601
			private const int ROTATION_0_2 = 19;

			// Token: 0x040040DA RID: 16602
			private const int ROTATION_0_3 = 37;

			// Token: 0x040040DB RID: 16603
			private const int ROTATION_1_0 = 33;

			// Token: 0x040040DC RID: 16604
			private const int ROTATION_1_1 = 27;

			// Token: 0x040040DD RID: 16605
			private const int ROTATION_1_2 = 14;

			// Token: 0x040040DE RID: 16606
			private const int ROTATION_1_3 = 42;

			// Token: 0x040040DF RID: 16607
			private const int ROTATION_2_0 = 17;

			// Token: 0x040040E0 RID: 16608
			private const int ROTATION_2_1 = 49;

			// Token: 0x040040E1 RID: 16609
			private const int ROTATION_2_2 = 36;

			// Token: 0x040040E2 RID: 16610
			private const int ROTATION_2_3 = 39;

			// Token: 0x040040E3 RID: 16611
			private const int ROTATION_3_0 = 44;

			// Token: 0x040040E4 RID: 16612
			private const int ROTATION_3_1 = 9;

			// Token: 0x040040E5 RID: 16613
			private const int ROTATION_3_2 = 54;

			// Token: 0x040040E6 RID: 16614
			private const int ROTATION_3_3 = 56;

			// Token: 0x040040E7 RID: 16615
			private const int ROTATION_4_0 = 39;

			// Token: 0x040040E8 RID: 16616
			private const int ROTATION_4_1 = 30;

			// Token: 0x040040E9 RID: 16617
			private const int ROTATION_4_2 = 34;

			// Token: 0x040040EA RID: 16618
			private const int ROTATION_4_3 = 24;

			// Token: 0x040040EB RID: 16619
			private const int ROTATION_5_0 = 13;

			// Token: 0x040040EC RID: 16620
			private const int ROTATION_5_1 = 50;

			// Token: 0x040040ED RID: 16621
			private const int ROTATION_5_2 = 10;

			// Token: 0x040040EE RID: 16622
			private const int ROTATION_5_3 = 17;

			// Token: 0x040040EF RID: 16623
			private const int ROTATION_6_0 = 25;

			// Token: 0x040040F0 RID: 16624
			private const int ROTATION_6_1 = 29;

			// Token: 0x040040F1 RID: 16625
			private const int ROTATION_6_2 = 39;

			// Token: 0x040040F2 RID: 16626
			private const int ROTATION_6_3 = 43;

			// Token: 0x040040F3 RID: 16627
			private const int ROTATION_7_0 = 8;

			// Token: 0x040040F4 RID: 16628
			private const int ROTATION_7_1 = 35;

			// Token: 0x040040F5 RID: 16629
			private const int ROTATION_7_2 = 56;

			// Token: 0x040040F6 RID: 16630
			private const int ROTATION_7_3 = 22;
		}

		// Token: 0x02000E0B RID: 3595
		private sealed class Threefish1024Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06008C12 RID: 35858 RVA: 0x002A08A4 File Offset: 0x002A08A4
			public Threefish1024Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06008C13 RID: 35859 RVA: 0x002A08B0 File Offset: 0x002A08B0
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD17;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 33)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				ulong num9 = block[8];
				ulong num10 = block[9];
				ulong num11 = block[10];
				ulong num12 = block[11];
				ulong num13 = block[12];
				ulong num14 = block[13];
				ulong num15 = block[14];
				ulong num16 = block[15];
				num += kw[0];
				num2 += kw[1];
				num3 += kw[2];
				num4 += kw[3];
				num5 += kw[4];
				num6 += kw[5];
				num7 += kw[6];
				num8 += kw[7];
				num9 += kw[8];
				num10 += kw[9];
				num11 += kw[10];
				num12 += kw[11];
				num13 += kw[12];
				num14 += kw[13] + t[0];
				num15 += kw[14] + t[1];
				num16 += kw[15];
				for (int i = 1; i < 20; i += 2)
				{
					int num17 = mod[i];
					int num18 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 24, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 13, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 8, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 47, num7 += num8);
					num10 = ThreefishEngine.RotlXor(num10, 8, num9 += num10);
					num12 = ThreefishEngine.RotlXor(num12, 17, num11 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 22, num13 += num14);
					num16 = ThreefishEngine.RotlXor(num16, 37, num15 += num16);
					num10 = ThreefishEngine.RotlXor(num10, 38, num += num10);
					num14 = ThreefishEngine.RotlXor(num14, 19, num3 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 10, num7 += num12);
					num16 = ThreefishEngine.RotlXor(num16, 55, num5 += num16);
					num8 = ThreefishEngine.RotlXor(num8, 49, num11 += num8);
					num4 = ThreefishEngine.RotlXor(num4, 18, num13 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 23, num15 += num6);
					num2 = ThreefishEngine.RotlXor(num2, 52, num9 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 33, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 4, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 51, num5 += num4);
					num2 = ThreefishEngine.RotlXor(num2, 13, num7 += num2);
					num16 = ThreefishEngine.RotlXor(num16, 34, num13 += num16);
					num14 = ThreefishEngine.RotlXor(num14, 41, num15 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 59, num9 += num12);
					num10 = ThreefishEngine.RotlXor(num10, 17, num11 += num10);
					num16 = ThreefishEngine.RotlXor(num16, 5, num += num16);
					num12 = ThreefishEngine.RotlXor(num12, 20, num3 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 48, num7 += num14);
					num10 = ThreefishEngine.RotlXor(num10, 41, num5 += num10);
					num2 = ThreefishEngine.RotlXor(num2, 47, num15 += num2);
					num6 = ThreefishEngine.RotlXor(num6, 28, num9 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 16, num11 += num4);
					num8 = ThreefishEngine.RotlXor(num8, 25, num13 += num8);
					num += kw[num17];
					num2 += kw[num17 + 1];
					num3 += kw[num17 + 2];
					num4 += kw[num17 + 3];
					num5 += kw[num17 + 4];
					num6 += kw[num17 + 5];
					num7 += kw[num17 + 6];
					num8 += kw[num17 + 7];
					num9 += kw[num17 + 8];
					num10 += kw[num17 + 9];
					num11 += kw[num17 + 10];
					num12 += kw[num17 + 11];
					num13 += kw[num17 + 12];
					num14 += kw[num17 + 13] + t[num18];
					num15 += kw[num17 + 14] + t[num18 + 1];
					num16 += kw[num17 + 15] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 41, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 9, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 37, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 31, num7 += num8);
					num10 = ThreefishEngine.RotlXor(num10, 12, num9 += num10);
					num12 = ThreefishEngine.RotlXor(num12, 47, num11 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 44, num13 += num14);
					num16 = ThreefishEngine.RotlXor(num16, 30, num15 += num16);
					num10 = ThreefishEngine.RotlXor(num10, 16, num += num10);
					num14 = ThreefishEngine.RotlXor(num14, 34, num3 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 56, num7 += num12);
					num16 = ThreefishEngine.RotlXor(num16, 51, num5 += num16);
					num8 = ThreefishEngine.RotlXor(num8, 4, num11 += num8);
					num4 = ThreefishEngine.RotlXor(num4, 53, num13 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 42, num15 += num6);
					num2 = ThreefishEngine.RotlXor(num2, 41, num9 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 31, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 44, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 47, num5 += num4);
					num2 = ThreefishEngine.RotlXor(num2, 46, num7 += num2);
					num16 = ThreefishEngine.RotlXor(num16, 19, num13 += num16);
					num14 = ThreefishEngine.RotlXor(num14, 42, num15 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 44, num9 += num12);
					num10 = ThreefishEngine.RotlXor(num10, 25, num11 += num10);
					num16 = ThreefishEngine.RotlXor(num16, 9, num += num16);
					num12 = ThreefishEngine.RotlXor(num12, 48, num3 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 35, num7 += num14);
					num10 = ThreefishEngine.RotlXor(num10, 52, num5 += num10);
					num2 = ThreefishEngine.RotlXor(num2, 23, num15 += num2);
					num6 = ThreefishEngine.RotlXor(num6, 31, num9 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 37, num11 += num4);
					num8 = ThreefishEngine.RotlXor(num8, 20, num13 += num8);
					num += kw[num17 + 1];
					num2 += kw[num17 + 2];
					num3 += kw[num17 + 3];
					num4 += kw[num17 + 4];
					num5 += kw[num17 + 5];
					num6 += kw[num17 + 6];
					num7 += kw[num17 + 7];
					num8 += kw[num17 + 8];
					num9 += kw[num17 + 9];
					num10 += kw[num17 + 10];
					num11 += kw[num17 + 11];
					num12 += kw[num17 + 12];
					num13 += kw[num17 + 13];
					num14 += kw[num17 + 14] + t[num18 + 1];
					num15 += kw[num17 + 15] + t[num18 + 2];
					num16 += kw[num17 + 16] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
				outWords[4] = num5;
				outWords[5] = num6;
				outWords[6] = num7;
				outWords[7] = num8;
				outWords[8] = num9;
				outWords[9] = num10;
				outWords[10] = num11;
				outWords[11] = num12;
				outWords[12] = num13;
				outWords[13] = num14;
				outWords[14] = num15;
				outWords[15] = num16;
			}

			// Token: 0x06008C14 RID: 35860 RVA: 0x002A10A8 File Offset: 0x002A10A8
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD17;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 33)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				ulong num9 = block[8];
				ulong num10 = block[9];
				ulong num11 = block[10];
				ulong num12 = block[11];
				ulong num13 = block[12];
				ulong num14 = block[13];
				ulong num15 = block[14];
				ulong num16 = block[15];
				for (int i = 19; i >= 1; i -= 2)
				{
					int num17 = mod[i];
					int num18 = mod2[i];
					num -= kw[num17 + 1];
					num2 -= kw[num17 + 2];
					num3 -= kw[num17 + 3];
					num4 -= kw[num17 + 4];
					num5 -= kw[num17 + 5];
					num6 -= kw[num17 + 6];
					num7 -= kw[num17 + 7];
					num8 -= kw[num17 + 8];
					num9 -= kw[num17 + 9];
					num10 -= kw[num17 + 10];
					num11 -= kw[num17 + 11];
					num12 -= kw[num17 + 12];
					num13 -= kw[num17 + 13];
					num14 -= kw[num17 + 14] + t[num18 + 1];
					num15 -= kw[num17 + 15] + t[num18 + 2];
					num16 -= kw[num17 + 16] + (ulong)i + 1UL;
					num16 = ThreefishEngine.XorRotr(num16, 9, num);
					num -= num16;
					num12 = ThreefishEngine.XorRotr(num12, 48, num3);
					num3 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 35, num7);
					num7 -= num14;
					num10 = ThreefishEngine.XorRotr(num10, 52, num5);
					num5 -= num10;
					num2 = ThreefishEngine.XorRotr(num2, 23, num15);
					num15 -= num2;
					num6 = ThreefishEngine.XorRotr(num6, 31, num9);
					num9 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 37, num11);
					num11 -= num4;
					num8 = ThreefishEngine.XorRotr(num8, 20, num13);
					num13 -= num8;
					num8 = ThreefishEngine.XorRotr(num8, 31, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 44, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 47, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 46, num7);
					num7 -= num2;
					num16 = ThreefishEngine.XorRotr(num16, 19, num13);
					num13 -= num16;
					num14 = ThreefishEngine.XorRotr(num14, 42, num15);
					num15 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 44, num9);
					num9 -= num12;
					num10 = ThreefishEngine.XorRotr(num10, 25, num11);
					num11 -= num10;
					num10 = ThreefishEngine.XorRotr(num10, 16, num);
					num -= num10;
					num14 = ThreefishEngine.XorRotr(num14, 34, num3);
					num3 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 56, num7);
					num7 -= num12;
					num16 = ThreefishEngine.XorRotr(num16, 51, num5);
					num5 -= num16;
					num8 = ThreefishEngine.XorRotr(num8, 4, num11);
					num11 -= num8;
					num4 = ThreefishEngine.XorRotr(num4, 53, num13);
					num13 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 42, num15);
					num15 -= num6;
					num2 = ThreefishEngine.XorRotr(num2, 41, num9);
					num9 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 41, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 9, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 37, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 31, num7);
					num7 -= num8;
					num10 = ThreefishEngine.XorRotr(num10, 12, num9);
					num9 -= num10;
					num12 = ThreefishEngine.XorRotr(num12, 47, num11);
					num11 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 44, num13);
					num13 -= num14;
					num16 = ThreefishEngine.XorRotr(num16, 30, num15);
					num15 -= num16;
					num -= kw[num17];
					num2 -= kw[num17 + 1];
					num3 -= kw[num17 + 2];
					num4 -= kw[num17 + 3];
					num5 -= kw[num17 + 4];
					num6 -= kw[num17 + 5];
					num7 -= kw[num17 + 6];
					num8 -= kw[num17 + 7];
					num9 -= kw[num17 + 8];
					num10 -= kw[num17 + 9];
					num11 -= kw[num17 + 10];
					num12 -= kw[num17 + 11];
					num13 -= kw[num17 + 12];
					num14 -= kw[num17 + 13] + t[num18];
					num15 -= kw[num17 + 14] + t[num18 + 1];
					num16 -= kw[num17 + 15] + (ulong)i;
					num16 = ThreefishEngine.XorRotr(num16, 5, num);
					num -= num16;
					num12 = ThreefishEngine.XorRotr(num12, 20, num3);
					num3 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 48, num7);
					num7 -= num14;
					num10 = ThreefishEngine.XorRotr(num10, 41, num5);
					num5 -= num10;
					num2 = ThreefishEngine.XorRotr(num2, 47, num15);
					num15 -= num2;
					num6 = ThreefishEngine.XorRotr(num6, 28, num9);
					num9 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 16, num11);
					num11 -= num4;
					num8 = ThreefishEngine.XorRotr(num8, 25, num13);
					num13 -= num8;
					num8 = ThreefishEngine.XorRotr(num8, 33, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 4, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 51, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 13, num7);
					num7 -= num2;
					num16 = ThreefishEngine.XorRotr(num16, 34, num13);
					num13 -= num16;
					num14 = ThreefishEngine.XorRotr(num14, 41, num15);
					num15 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 59, num9);
					num9 -= num12;
					num10 = ThreefishEngine.XorRotr(num10, 17, num11);
					num11 -= num10;
					num10 = ThreefishEngine.XorRotr(num10, 38, num);
					num -= num10;
					num14 = ThreefishEngine.XorRotr(num14, 19, num3);
					num3 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 10, num7);
					num7 -= num12;
					num16 = ThreefishEngine.XorRotr(num16, 55, num5);
					num5 -= num16;
					num8 = ThreefishEngine.XorRotr(num8, 49, num11);
					num11 -= num8;
					num4 = ThreefishEngine.XorRotr(num4, 18, num13);
					num13 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 23, num15);
					num15 -= num6;
					num2 = ThreefishEngine.XorRotr(num2, 52, num9);
					num9 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 24, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 13, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 8, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 47, num7);
					num7 -= num8;
					num10 = ThreefishEngine.XorRotr(num10, 8, num9);
					num9 -= num10;
					num12 = ThreefishEngine.XorRotr(num12, 17, num11);
					num11 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 22, num13);
					num13 -= num14;
					num16 = ThreefishEngine.XorRotr(num16, 37, num15);
					num15 -= num16;
				}
				num -= kw[0];
				num2 -= kw[1];
				num3 -= kw[2];
				num4 -= kw[3];
				num5 -= kw[4];
				num6 -= kw[5];
				num7 -= kw[6];
				num8 -= kw[7];
				num9 -= kw[8];
				num10 -= kw[9];
				num11 -= kw[10];
				num12 -= kw[11];
				num13 -= kw[12];
				num14 -= kw[13] + t[0];
				num15 -= kw[14] + t[1];
				num16 -= kw[15];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
				state[4] = num5;
				state[5] = num6;
				state[6] = num7;
				state[7] = num8;
				state[8] = num9;
				state[9] = num10;
				state[10] = num11;
				state[11] = num12;
				state[12] = num13;
				state[13] = num14;
				state[14] = num15;
				state[15] = num16;
			}

			// Token: 0x040040F7 RID: 16631
			private const int ROTATION_0_0 = 24;

			// Token: 0x040040F8 RID: 16632
			private const int ROTATION_0_1 = 13;

			// Token: 0x040040F9 RID: 16633
			private const int ROTATION_0_2 = 8;

			// Token: 0x040040FA RID: 16634
			private const int ROTATION_0_3 = 47;

			// Token: 0x040040FB RID: 16635
			private const int ROTATION_0_4 = 8;

			// Token: 0x040040FC RID: 16636
			private const int ROTATION_0_5 = 17;

			// Token: 0x040040FD RID: 16637
			private const int ROTATION_0_6 = 22;

			// Token: 0x040040FE RID: 16638
			private const int ROTATION_0_7 = 37;

			// Token: 0x040040FF RID: 16639
			private const int ROTATION_1_0 = 38;

			// Token: 0x04004100 RID: 16640
			private const int ROTATION_1_1 = 19;

			// Token: 0x04004101 RID: 16641
			private const int ROTATION_1_2 = 10;

			// Token: 0x04004102 RID: 16642
			private const int ROTATION_1_3 = 55;

			// Token: 0x04004103 RID: 16643
			private const int ROTATION_1_4 = 49;

			// Token: 0x04004104 RID: 16644
			private const int ROTATION_1_5 = 18;

			// Token: 0x04004105 RID: 16645
			private const int ROTATION_1_6 = 23;

			// Token: 0x04004106 RID: 16646
			private const int ROTATION_1_7 = 52;

			// Token: 0x04004107 RID: 16647
			private const int ROTATION_2_0 = 33;

			// Token: 0x04004108 RID: 16648
			private const int ROTATION_2_1 = 4;

			// Token: 0x04004109 RID: 16649
			private const int ROTATION_2_2 = 51;

			// Token: 0x0400410A RID: 16650
			private const int ROTATION_2_3 = 13;

			// Token: 0x0400410B RID: 16651
			private const int ROTATION_2_4 = 34;

			// Token: 0x0400410C RID: 16652
			private const int ROTATION_2_5 = 41;

			// Token: 0x0400410D RID: 16653
			private const int ROTATION_2_6 = 59;

			// Token: 0x0400410E RID: 16654
			private const int ROTATION_2_7 = 17;

			// Token: 0x0400410F RID: 16655
			private const int ROTATION_3_0 = 5;

			// Token: 0x04004110 RID: 16656
			private const int ROTATION_3_1 = 20;

			// Token: 0x04004111 RID: 16657
			private const int ROTATION_3_2 = 48;

			// Token: 0x04004112 RID: 16658
			private const int ROTATION_3_3 = 41;

			// Token: 0x04004113 RID: 16659
			private const int ROTATION_3_4 = 47;

			// Token: 0x04004114 RID: 16660
			private const int ROTATION_3_5 = 28;

			// Token: 0x04004115 RID: 16661
			private const int ROTATION_3_6 = 16;

			// Token: 0x04004116 RID: 16662
			private const int ROTATION_3_7 = 25;

			// Token: 0x04004117 RID: 16663
			private const int ROTATION_4_0 = 41;

			// Token: 0x04004118 RID: 16664
			private const int ROTATION_4_1 = 9;

			// Token: 0x04004119 RID: 16665
			private const int ROTATION_4_2 = 37;

			// Token: 0x0400411A RID: 16666
			private const int ROTATION_4_3 = 31;

			// Token: 0x0400411B RID: 16667
			private const int ROTATION_4_4 = 12;

			// Token: 0x0400411C RID: 16668
			private const int ROTATION_4_5 = 47;

			// Token: 0x0400411D RID: 16669
			private const int ROTATION_4_6 = 44;

			// Token: 0x0400411E RID: 16670
			private const int ROTATION_4_7 = 30;

			// Token: 0x0400411F RID: 16671
			private const int ROTATION_5_0 = 16;

			// Token: 0x04004120 RID: 16672
			private const int ROTATION_5_1 = 34;

			// Token: 0x04004121 RID: 16673
			private const int ROTATION_5_2 = 56;

			// Token: 0x04004122 RID: 16674
			private const int ROTATION_5_3 = 51;

			// Token: 0x04004123 RID: 16675
			private const int ROTATION_5_4 = 4;

			// Token: 0x04004124 RID: 16676
			private const int ROTATION_5_5 = 53;

			// Token: 0x04004125 RID: 16677
			private const int ROTATION_5_6 = 42;

			// Token: 0x04004126 RID: 16678
			private const int ROTATION_5_7 = 41;

			// Token: 0x04004127 RID: 16679
			private const int ROTATION_6_0 = 31;

			// Token: 0x04004128 RID: 16680
			private const int ROTATION_6_1 = 44;

			// Token: 0x04004129 RID: 16681
			private const int ROTATION_6_2 = 47;

			// Token: 0x0400412A RID: 16682
			private const int ROTATION_6_3 = 46;

			// Token: 0x0400412B RID: 16683
			private const int ROTATION_6_4 = 19;

			// Token: 0x0400412C RID: 16684
			private const int ROTATION_6_5 = 42;

			// Token: 0x0400412D RID: 16685
			private const int ROTATION_6_6 = 44;

			// Token: 0x0400412E RID: 16686
			private const int ROTATION_6_7 = 25;

			// Token: 0x0400412F RID: 16687
			private const int ROTATION_7_0 = 9;

			// Token: 0x04004130 RID: 16688
			private const int ROTATION_7_1 = 48;

			// Token: 0x04004131 RID: 16689
			private const int ROTATION_7_2 = 35;

			// Token: 0x04004132 RID: 16690
			private const int ROTATION_7_3 = 52;

			// Token: 0x04004133 RID: 16691
			private const int ROTATION_7_4 = 23;

			// Token: 0x04004134 RID: 16692
			private const int ROTATION_7_5 = 31;

			// Token: 0x04004135 RID: 16693
			private const int ROTATION_7_6 = 37;

			// Token: 0x04004136 RID: 16694
			private const int ROTATION_7_7 = 20;
		}
	}
}
