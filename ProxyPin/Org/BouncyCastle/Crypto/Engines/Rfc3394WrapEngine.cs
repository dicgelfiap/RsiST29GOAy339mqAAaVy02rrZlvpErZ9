using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200037A RID: 890
	public class Rfc3394WrapEngine : IWrapper
	{
		// Token: 0x06001BBC RID: 7100 RVA: 0x00098F4C File Offset: 0x00098F4C
		public Rfc3394WrapEngine(IBlockCipher engine)
		{
			this.engine = engine;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00098F74 File Offset: 0x00098F74
		public virtual void Init(bool forWrapping, ICipherParameters parameters)
		{
			this.forWrapping = forWrapping;
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (parameters is KeyParameter)
			{
				this.param = (KeyParameter)parameters;
				return;
			}
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] array = parametersWithIV.GetIV();
				if (array.Length != 8)
				{
					throw new ArgumentException("IV length not equal to 8", "parameters");
				}
				this.iv = array;
				this.param = (KeyParameter)parametersWithIV.Parameters;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00099008 File Offset: 0x00099008
		public virtual string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName;
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00099018 File Offset: 0x00099018
		public virtual byte[] Wrap(byte[] input, int inOff, int inLen)
		{
			if (!this.forWrapping)
			{
				throw new InvalidOperationException("not set for wrapping");
			}
			int num = inLen / 8;
			if (num * 8 != inLen)
			{
				throw new DataLengthException("wrap data must be a multiple of 8 bytes");
			}
			byte[] array = new byte[inLen + this.iv.Length];
			byte[] array2 = new byte[8 + this.iv.Length];
			Array.Copy(this.iv, 0, array, 0, this.iv.Length);
			Array.Copy(input, inOff, array, this.iv.Length, inLen);
			this.engine.Init(true, this.param);
			for (int num2 = 0; num2 != 6; num2++)
			{
				for (int i = 1; i <= num; i++)
				{
					Array.Copy(array, 0, array2, 0, this.iv.Length);
					Array.Copy(array, 8 * i, array2, this.iv.Length, 8);
					this.engine.ProcessBlock(array2, 0, array2, 0);
					int num3 = num * num2 + i;
					int num4 = 1;
					while (num3 != 0)
					{
						byte b = (byte)num3;
						byte[] array3;
						IntPtr intPtr;
						(array3 = array2)[(int)(intPtr = (IntPtr)(this.iv.Length - num4))] = (array3[(int)intPtr] ^ b);
						num3 = (int)((uint)num3 >> 8);
						num4++;
					}
					Array.Copy(array2, 0, array, 0, 8);
					Array.Copy(array2, 8, array, 8 * i, 8);
				}
			}
			return array;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00099168 File Offset: 0x00099168
		public virtual byte[] Unwrap(byte[] input, int inOff, int inLen)
		{
			if (this.forWrapping)
			{
				throw new InvalidOperationException("not set for unwrapping");
			}
			int num = inLen / 8;
			if (num * 8 != inLen)
			{
				throw new InvalidCipherTextException("unwrap data must be a multiple of 8 bytes");
			}
			byte[] array = new byte[inLen - this.iv.Length];
			byte[] array2 = new byte[this.iv.Length];
			byte[] array3 = new byte[8 + this.iv.Length];
			Array.Copy(input, inOff, array2, 0, this.iv.Length);
			Array.Copy(input, inOff + this.iv.Length, array, 0, inLen - this.iv.Length);
			this.engine.Init(false, this.param);
			num--;
			for (int i = 5; i >= 0; i--)
			{
				for (int j = num; j >= 1; j--)
				{
					Array.Copy(array2, 0, array3, 0, this.iv.Length);
					Array.Copy(array, 8 * (j - 1), array3, this.iv.Length, 8);
					int num2 = num * i + j;
					int num3 = 1;
					while (num2 != 0)
					{
						byte b = (byte)num2;
						byte[] array4;
						IntPtr intPtr;
						(array4 = array3)[(int)(intPtr = (IntPtr)(this.iv.Length - num3))] = (array4[(int)intPtr] ^ b);
						num2 = (int)((uint)num2 >> 8);
						num3++;
					}
					this.engine.ProcessBlock(array3, 0, array3, 0);
					Array.Copy(array3, 0, array2, 0, 8);
					Array.Copy(array3, 8, array, 8 * (j - 1), 8);
				}
			}
			if (!Arrays.ConstantTimeAreEqual(array2, this.iv))
			{
				throw new InvalidCipherTextException("checksum failed");
			}
			return array;
		}

		// Token: 0x04001271 RID: 4721
		private readonly IBlockCipher engine;

		// Token: 0x04001272 RID: 4722
		private KeyParameter param;

		// Token: 0x04001273 RID: 4723
		private bool forWrapping;

		// Token: 0x04001274 RID: 4724
		private byte[] iv = new byte[]
		{
			166,
			166,
			166,
			166,
			166,
			166,
			166,
			166
		};
	}
}
