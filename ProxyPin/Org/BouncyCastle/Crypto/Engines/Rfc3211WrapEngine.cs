﻿using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200039B RID: 923
	public class Rfc3211WrapEngine : IWrapper
	{
		// Token: 0x06001D3E RID: 7486 RVA: 0x000A6910 File Offset: 0x000A6910
		public Rfc3211WrapEngine(IBlockCipher engine)
		{
			this.engine = new CbcBlockCipher(engine);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000A6924 File Offset: 0x000A6924
		public virtual void Init(bool forWrapping, ICipherParameters param)
		{
			this.forWrapping = forWrapping;
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.rand = parametersWithRandom.Random;
				this.param = (parametersWithRandom.Parameters as ParametersWithIV);
			}
			else
			{
				if (forWrapping)
				{
					this.rand = new SecureRandom();
				}
				this.param = (param as ParametersWithIV);
			}
			if (this.param == null)
			{
				throw new ArgumentException("RFC3211Wrap requires an IV", "param");
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000A69A8 File Offset: 0x000A69A8
		public virtual string AlgorithmName
		{
			get
			{
				return this.engine.GetUnderlyingCipher().AlgorithmName + "/RFC3211Wrap";
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000A69C4 File Offset: 0x000A69C4
		public virtual byte[] Wrap(byte[] inBytes, int inOff, int inLen)
		{
			if (!this.forWrapping)
			{
				throw new InvalidOperationException("not set for wrapping");
			}
			if (inLen > 255 || inLen < 0)
			{
				throw new ArgumentException("input must be from 0 to 255 bytes", "inLen");
			}
			this.engine.Init(true, this.param);
			int blockSize = this.engine.GetBlockSize();
			byte[] array;
			if (inLen + 4 < blockSize * 2)
			{
				array = new byte[blockSize * 2];
			}
			else
			{
				array = new byte[((inLen + 4) % blockSize == 0) ? (inLen + 4) : (((inLen + 4) / blockSize + 1) * blockSize)];
			}
			array[0] = (byte)inLen;
			Array.Copy(inBytes, inOff, array, 4, inLen);
			this.rand.NextBytes(array, inLen + 4, array.Length - inLen - 4);
			array[1] = ~array[4];
			array[2] = ~array[5];
			array[3] = ~array[6];
			for (int i = 0; i < array.Length; i += blockSize)
			{
				this.engine.ProcessBlock(array, i, array, i);
			}
			for (int j = 0; j < array.Length; j += blockSize)
			{
				this.engine.ProcessBlock(array, j, array, j);
			}
			return array;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000A6AE8 File Offset: 0x000A6AE8
		public virtual byte[] Unwrap(byte[] inBytes, int inOff, int inLen)
		{
			if (this.forWrapping)
			{
				throw new InvalidOperationException("not set for unwrapping");
			}
			int blockSize = this.engine.GetBlockSize();
			if (inLen < 2 * blockSize)
			{
				throw new InvalidCipherTextException("input too short");
			}
			byte[] array = new byte[inLen];
			byte[] array2 = new byte[blockSize];
			Array.Copy(inBytes, inOff, array, 0, inLen);
			Array.Copy(inBytes, inOff, array2, 0, array2.Length);
			this.engine.Init(false, new ParametersWithIV(this.param.Parameters, array2));
			for (int i = blockSize; i < array.Length; i += blockSize)
			{
				this.engine.ProcessBlock(array, i, array, i);
			}
			Array.Copy(array, array.Length - array2.Length, array2, 0, array2.Length);
			this.engine.Init(false, new ParametersWithIV(this.param.Parameters, array2));
			this.engine.ProcessBlock(array, 0, array, 0);
			this.engine.Init(false, this.param);
			for (int j = 0; j < array.Length; j += blockSize)
			{
				this.engine.ProcessBlock(array, j, array, j);
			}
			bool flag = (int)array[0] > array.Length - 4;
			byte[] array3;
			if (flag)
			{
				array3 = new byte[array.Length - 4];
			}
			else
			{
				array3 = new byte[(int)array[0]];
			}
			Array.Copy(array, 4, array3, 0, array3.Length);
			int num = 0;
			for (int num2 = 0; num2 != 3; num2++)
			{
				byte b = ~array[1 + num2];
				num |= (int)(b ^ array[4 + num2]);
			}
			Array.Clear(array, 0, array.Length);
			if (num != 0 || flag)
			{
				throw new InvalidCipherTextException("wrapped key corrupted");
			}
			return array3;
		}

		// Token: 0x0400135E RID: 4958
		private CbcBlockCipher engine;

		// Token: 0x0400135F RID: 4959
		private ParametersWithIV param;

		// Token: 0x04001360 RID: 4960
		private bool forWrapping;

		// Token: 0x04001361 RID: 4961
		private SecureRandom rand;
	}
}
