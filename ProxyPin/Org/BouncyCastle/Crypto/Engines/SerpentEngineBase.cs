using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A4 RID: 932
	public abstract class SerpentEngineBase : IBlockCipher
	{
		// Token: 0x06001D98 RID: 7576 RVA: 0x000A877C File Offset: 0x000A877C
		public virtual void Init(bool encrypting, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to " + this.AlgorithmName + " init - " + Platform.GetTypeName(parameters));
			}
			this.encrypting = encrypting;
			this.wKey = this.MakeWorkingKey(((KeyParameter)parameters).GetKey());
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x000A87D8 File Offset: 0x000A87D8
		public virtual string AlgorithmName
		{
			get
			{
				return "Serpent";
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000A87E0 File Offset: 0x000A87E0
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000A87E4 File Offset: 0x000A87E4
		public virtual int GetBlockSize()
		{
			return SerpentEngineBase.BlockSize;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000A87EC File Offset: 0x000A87EC
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.wKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, SerpentEngineBase.BlockSize, "input buffer too short");
			Check.OutputLength(output, outOff, SerpentEngineBase.BlockSize, "output buffer too short");
			if (this.encrypting)
			{
				this.EncryptBlock(input, inOff, output, outOff);
			}
			else
			{
				this.DecryptBlock(input, inOff, output, outOff);
			}
			return SerpentEngineBase.BlockSize;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000A886C File Offset: 0x000A886C
		public virtual void Reset()
		{
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000A8870 File Offset: 0x000A8870
		protected static int RotateLeft(int x, int bits)
		{
			return x << bits | (int)((uint)x >> 32 - bits);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000A8884 File Offset: 0x000A8884
		private static int RotateRight(int x, int bits)
		{
			return (int)((uint)x >> bits | (uint)((uint)x << 32 - bits));
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000A8898 File Offset: 0x000A8898
		protected void Sb0(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = c ^ num;
			int num3 = b ^ num2;
			this.X3 = ((a & d) ^ num3);
			int num4 = a ^ (b & num);
			this.X2 = (num3 ^ (c | num4));
			int num5 = this.X3 & (num2 ^ num4);
			this.X1 = (~num2 ^ num5);
			this.X0 = (num5 ^ ~num4);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000A88F8 File Offset: 0x000A88F8
		protected void Ib0(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = d ^ (num | num2);
			int num4 = c ^ num3;
			this.X2 = (num2 ^ num4);
			int num5 = num ^ (d & num2);
			this.X1 = (num3 ^ (this.X2 & num5));
			this.X3 = ((a & num3) ^ (num4 | this.X1));
			this.X0 = (this.X3 ^ (num4 ^ num5));
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000A8960 File Offset: 0x000A8960
		protected void Sb1(int a, int b, int c, int d)
		{
			int num = b ^ ~a;
			int num2 = c ^ (a | num);
			this.X2 = (d ^ num2);
			int num3 = b ^ (d | num);
			int num4 = num ^ this.X2;
			this.X3 = (num4 ^ (num2 & num3));
			int num5 = num2 ^ num3;
			this.X1 = (this.X3 ^ num5);
			this.X0 = (num2 ^ (num4 & num5));
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000A89C4 File Offset: 0x000A89C4
		protected void Ib1(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = a ^ (b & num);
			int num3 = num ^ num2;
			this.X3 = (c ^ num3);
			int num4 = b ^ (num & num2);
			int num5 = this.X3 | num4;
			this.X1 = (num2 ^ num5);
			int num6 = ~this.X1;
			int num7 = this.X3 ^ num4;
			this.X0 = (num6 ^ num7);
			this.X2 = (num3 ^ (num6 | num7));
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000A8A34 File Offset: 0x000A8A34
		protected void Sb2(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = b ^ d;
			int num3 = c & num;
			this.X0 = (num2 ^ num3);
			int num4 = c ^ num;
			int num5 = c ^ this.X0;
			int num6 = b & num5;
			this.X3 = (num4 ^ num6);
			this.X2 = (a ^ ((d | num6) & (this.X0 | num4)));
			this.X1 = (num2 ^ this.X3 ^ (this.X2 ^ (d | num)));
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000A8AA8 File Offset: 0x000A8AA8
		protected void Ib2(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = ~num;
			int num3 = a ^ c;
			int num4 = c ^ num;
			int num5 = b & num4;
			this.X0 = (num3 ^ num5);
			int num6 = a | num2;
			int num7 = d ^ num6;
			int num8 = num3 | num7;
			this.X3 = (num ^ num8);
			int num9 = ~num4;
			int num10 = this.X0 | this.X3;
			this.X1 = (num9 ^ num10);
			this.X2 = ((d & num9) ^ (num3 ^ num10));
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000A8B24 File Offset: 0x000A8B24
		protected void Sb3(int a, int b, int c, int d)
		{
			int num = a ^ b;
			int num2 = a & c;
			int num3 = a | d;
			int num4 = c ^ d;
			int num5 = num & num3;
			int num6 = num2 | num5;
			this.X2 = (num4 ^ num6);
			int num7 = b ^ num3;
			int num8 = num6 ^ num7;
			int num9 = num4 & num8;
			this.X0 = (num ^ num9);
			int num10 = this.X2 & this.X0;
			this.X1 = (num8 ^ num10);
			this.X3 = ((b | d) ^ (num4 ^ num10));
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000A8BA4 File Offset: 0x000A8BA4
		protected void Ib3(int a, int b, int c, int d)
		{
			int num = a | b;
			int num2 = b ^ c;
			int num3 = b & num2;
			int num4 = a ^ num3;
			int num5 = c ^ num4;
			int num6 = d | num4;
			this.X0 = (num2 ^ num6);
			int num7 = num2 | num6;
			int num8 = d ^ num7;
			this.X2 = (num5 ^ num8);
			int num9 = num ^ num8;
			int num10 = this.X0 & num9;
			this.X3 = (num4 ^ num10);
			this.X1 = (this.X3 ^ (this.X0 ^ num9));
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000A8C24 File Offset: 0x000A8C24
		protected void Sb4(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = d & num;
			int num3 = c ^ num2;
			int num4 = b | num3;
			this.X3 = (num ^ num4);
			int num5 = ~b;
			int num6 = num | num5;
			this.X0 = (num3 ^ num6);
			int num7 = a & this.X0;
			int num8 = num ^ num5;
			int num9 = num4 & num8;
			this.X2 = (num7 ^ num9);
			this.X1 = (a ^ num3 ^ (num8 & this.X2));
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000A8C98 File Offset: 0x000A8C98
		protected void Ib4(int a, int b, int c, int d)
		{
			int num = c | d;
			int num2 = a & num;
			int num3 = b ^ num2;
			int num4 = a & num3;
			int num5 = c ^ num4;
			this.X1 = (d ^ num5);
			int num6 = ~a;
			int num7 = num5 & this.X1;
			this.X3 = (num3 ^ num7);
			int num8 = this.X1 | num6;
			int num9 = d ^ num8;
			this.X0 = (this.X3 ^ num9);
			this.X2 = ((num3 & num9) ^ (this.X1 ^ num6));
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000A8D18 File Offset: 0x000A8D18
		protected void Sb5(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = a ^ d;
			int num4 = c ^ num;
			int num5 = num2 | num3;
			this.X0 = (num4 ^ num5);
			int num6 = d & this.X0;
			int num7 = num2 ^ this.X0;
			this.X1 = (num6 ^ num7);
			int num8 = num | this.X0;
			int num9 = num2 | num6;
			int num10 = num3 ^ num8;
			this.X2 = (num9 ^ num10);
			this.X3 = (b ^ num6 ^ (this.X1 & num10));
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000A8DA0 File Offset: 0x000A8DA0
		protected void Ib5(int a, int b, int c, int d)
		{
			int num = ~c;
			int num2 = b & num;
			int num3 = d ^ num2;
			int num4 = a & num3;
			int num5 = b ^ num;
			this.X3 = (num4 ^ num5);
			int num6 = b | this.X3;
			int num7 = a & num6;
			this.X1 = (num3 ^ num7);
			int num8 = a | d;
			int num9 = num ^ num6;
			this.X0 = (num8 ^ num9);
			this.X2 = ((b & num8) ^ (num4 | (a ^ c)));
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000A8E14 File Offset: 0x000A8E14
		protected void Sb6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ d;
			int num3 = b ^ num2;
			int num4 = num | num2;
			int num5 = c ^ num4;
			this.X1 = (b ^ num5);
			int num6 = num2 | this.X1;
			int num7 = d ^ num6;
			int num8 = num5 & num7;
			this.X2 = (num3 ^ num8);
			int num9 = num5 ^ num7;
			this.X0 = (this.X2 ^ num9);
			this.X3 = (~num5 ^ (num3 & num9));
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000A8E8C File Offset: 0x000A8E8C
		protected void Ib6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = c ^ num2;
			int num4 = c | num;
			int num5 = d ^ num4;
			this.X1 = (num3 ^ num5);
			int num6 = num3 & num5;
			int num7 = num2 ^ num6;
			int num8 = b | num7;
			this.X3 = (num5 ^ num8);
			int num9 = b | this.X3;
			this.X0 = (num7 ^ num9);
			this.X2 = ((d & num) ^ (num3 ^ num9));
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000A8F00 File Offset: 0x000A8F00
		protected void Sb7(int a, int b, int c, int d)
		{
			int num = b ^ c;
			int num2 = c & num;
			int num3 = d ^ num2;
			int num4 = a ^ num3;
			int num5 = d | num;
			int num6 = num4 & num5;
			this.X1 = (b ^ num6);
			int num7 = num3 | this.X1;
			int num8 = a & num4;
			this.X3 = (num ^ num8);
			int num9 = num4 ^ num7;
			int num10 = this.X3 & num9;
			this.X2 = (num3 ^ num10);
			this.X0 = (~num9 ^ (this.X3 & this.X2));
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000A8F84 File Offset: 0x000A8F84
		protected void Ib7(int a, int b, int c, int d)
		{
			int num = c | (a & b);
			int num2 = d & (a | b);
			this.X3 = (num ^ num2);
			int num3 = ~d;
			int num4 = b ^ num2;
			int num5 = num4 | (this.X3 ^ num3);
			this.X1 = (a ^ num5);
			this.X0 = (c ^ num4 ^ (d | this.X1));
			this.X2 = (num ^ this.X1 ^ (this.X0 ^ (a & this.X3)));
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000A8FFC File Offset: 0x000A8FFC
		protected void LT()
		{
			int num = SerpentEngineBase.RotateLeft(this.X0, 13);
			int num2 = SerpentEngineBase.RotateLeft(this.X2, 3);
			int x = this.X1 ^ num ^ num2;
			int x2 = this.X3 ^ num2 ^ num << 3;
			this.X1 = SerpentEngineBase.RotateLeft(x, 1);
			this.X3 = SerpentEngineBase.RotateLeft(x2, 7);
			this.X0 = SerpentEngineBase.RotateLeft(num ^ this.X1 ^ this.X3, 5);
			this.X2 = SerpentEngineBase.RotateLeft(num2 ^ this.X3 ^ this.X1 << 7, 22);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000A9094 File Offset: 0x000A9094
		protected void InverseLT()
		{
			int num = SerpentEngineBase.RotateRight(this.X2, 22) ^ this.X3 ^ this.X1 << 7;
			int num2 = SerpentEngineBase.RotateRight(this.X0, 5) ^ this.X1 ^ this.X3;
			int num3 = SerpentEngineBase.RotateRight(this.X3, 7);
			int num4 = SerpentEngineBase.RotateRight(this.X1, 1);
			this.X3 = (num3 ^ num ^ num2 << 3);
			this.X1 = (num4 ^ num2 ^ num);
			this.X2 = SerpentEngineBase.RotateRight(num, 3);
			this.X0 = SerpentEngineBase.RotateRight(num2, 13);
		}

		// Token: 0x06001DB2 RID: 7602
		protected abstract int[] MakeWorkingKey(byte[] key);

		// Token: 0x06001DB3 RID: 7603
		protected abstract void EncryptBlock(byte[] input, int inOff, byte[] output, int outOff);

		// Token: 0x06001DB4 RID: 7604
		protected abstract void DecryptBlock(byte[] input, int inOff, byte[] output, int outOff);

		// Token: 0x0400138A RID: 5002
		internal const int ROUNDS = 32;

		// Token: 0x0400138B RID: 5003
		internal const int PHI = -1640531527;

		// Token: 0x0400138C RID: 5004
		protected static readonly int BlockSize = 16;

		// Token: 0x0400138D RID: 5005
		protected bool encrypting;

		// Token: 0x0400138E RID: 5006
		protected int[] wKey;

		// Token: 0x0400138F RID: 5007
		protected int X0;

		// Token: 0x04001390 RID: 5008
		protected int X1;

		// Token: 0x04001391 RID: 5009
		protected int X2;

		// Token: 0x04001392 RID: 5010
		protected int X3;
	}
}
