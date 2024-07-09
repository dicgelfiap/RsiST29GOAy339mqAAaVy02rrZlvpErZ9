using System;
using System.Text;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x0200056B RID: 1387
	internal class SimpleBigDecimal
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x000E537C File Offset: 0x000E537C
		public static SimpleBigDecimal GetInstance(BigInteger val, int scale)
		{
			return new SimpleBigDecimal(val.ShiftLeft(scale), scale);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000E538C File Offset: 0x000E538C
		public SimpleBigDecimal(BigInteger bigInt, int scale)
		{
			if (scale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			this.bigInt = bigInt;
			this.scale = scale;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000E53B4 File Offset: 0x000E53B4
		private SimpleBigDecimal(SimpleBigDecimal limBigDec)
		{
			this.bigInt = limBigDec.bigInt;
			this.scale = limBigDec.scale;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000E53D4 File Offset: 0x000E53D4
		private void CheckScale(SimpleBigDecimal b)
		{
			if (this.scale != b.scale)
			{
				throw new ArgumentException("Only SimpleBigDecimal of same scale allowed in arithmetic operations");
			}
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000E53F4 File Offset: 0x000E53F4
		public SimpleBigDecimal AdjustScale(int newScale)
		{
			if (newScale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			if (newScale == this.scale)
			{
				return this;
			}
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(newScale - this.scale), newScale);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000E5430 File Offset: 0x000E5430
		public SimpleBigDecimal Add(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Add(b.bigInt), this.scale);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000E5458 File Offset: 0x000E5458
		public SimpleBigDecimal Add(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Add(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000E547C File Offset: 0x000E547C
		public SimpleBigDecimal Negate()
		{
			return new SimpleBigDecimal(this.bigInt.Negate(), this.scale);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000E5494 File Offset: 0x000E5494
		public SimpleBigDecimal Subtract(SimpleBigDecimal b)
		{
			return this.Add(b.Negate());
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000E54A4 File Offset: 0x000E54A4
		public SimpleBigDecimal Subtract(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Subtract(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000E54C8 File Offset: 0x000E54C8
		public SimpleBigDecimal Multiply(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Multiply(b.bigInt), this.scale + this.scale);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000E54F4 File Offset: 0x000E54F4
		public SimpleBigDecimal Multiply(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Multiply(b), this.scale);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000E5510 File Offset: 0x000E5510
		public SimpleBigDecimal Divide(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			BigInteger bigInteger = this.bigInt.ShiftLeft(this.scale);
			return new SimpleBigDecimal(bigInteger.Divide(b.bigInt), this.scale);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000E5554 File Offset: 0x000E5554
		public SimpleBigDecimal Divide(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Divide(b), this.scale);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000E5570 File Offset: 0x000E5570
		public SimpleBigDecimal ShiftLeft(int n)
		{
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(n), this.scale);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000E558C File Offset: 0x000E558C
		public int CompareTo(SimpleBigDecimal val)
		{
			this.CheckScale(val);
			return this.bigInt.CompareTo(val.bigInt);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000E55A8 File Offset: 0x000E55A8
		public int CompareTo(BigInteger val)
		{
			return this.bigInt.CompareTo(val.ShiftLeft(this.scale));
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000E55C4 File Offset: 0x000E55C4
		public BigInteger Floor()
		{
			return this.bigInt.ShiftRight(this.scale);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000E55D8 File Offset: 0x000E55D8
		public BigInteger Round()
		{
			SimpleBigDecimal simpleBigDecimal = new SimpleBigDecimal(BigInteger.One, 1);
			return this.Add(simpleBigDecimal.AdjustScale(this.scale)).Floor();
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000E560C File Offset: 0x000E560C
		public int IntValue
		{
			get
			{
				return this.Floor().IntValue;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000E561C File Offset: 0x000E561C
		public long LongValue
		{
			get
			{
				return this.Floor().LongValue;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000E562C File Offset: 0x000E562C
		public int Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000E5634 File Offset: 0x000E5634
		public override string ToString()
		{
			if (this.scale == 0)
			{
				return this.bigInt.ToString();
			}
			BigInteger bigInteger = this.Floor();
			BigInteger bigInteger2 = this.bigInt.Subtract(bigInteger.ShiftLeft(this.scale));
			if (this.bigInt.SignValue < 0)
			{
				bigInteger2 = BigInteger.One.ShiftLeft(this.scale).Subtract(bigInteger2);
			}
			if (bigInteger.SignValue == -1 && !bigInteger2.Equals(BigInteger.Zero))
			{
				bigInteger = bigInteger.Add(BigInteger.One);
			}
			string value = bigInteger.ToString();
			char[] array = new char[this.scale];
			string text = bigInteger2.ToString(2);
			int length = text.Length;
			int num = this.scale - length;
			for (int i = 0; i < num; i++)
			{
				array[i] = '0';
			}
			for (int j = 0; j < length; j++)
			{
				array[num + j] = text[j];
			}
			string value2 = new string(array);
			StringBuilder stringBuilder = new StringBuilder(value);
			stringBuilder.Append(".");
			stringBuilder.Append(value2);
			return stringBuilder.ToString();
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000E5768 File Offset: 0x000E5768
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			SimpleBigDecimal simpleBigDecimal = obj as SimpleBigDecimal;
			return simpleBigDecimal != null && this.bigInt.Equals(simpleBigDecimal.bigInt) && this.scale == simpleBigDecimal.scale;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000E57B8 File Offset: 0x000E57B8
		public override int GetHashCode()
		{
			return this.bigInt.GetHashCode() ^ this.scale;
		}

		// Token: 0x04001B40 RID: 6976
		private readonly BigInteger bigInt;

		// Token: 0x04001B41 RID: 6977
		private readonly int scale;
	}
}
