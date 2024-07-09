using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200013E RID: 318
	public class DerInteger : Asn1Object
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x0004A7A4 File Offset: 0x0004A7A4
		internal static bool AllowUnsafe()
		{
			string environmentVariable = Platform.GetEnvironmentVariable("Org.BouncyCastle.Asn1.AllowUnsafeInteger");
			return environmentVariable != null && Platform.EqualsIgnoreCase("true", environmentVariable);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0004A7D4 File Offset: 0x0004A7D4
		public static DerInteger GetInstance(object obj)
		{
			if (obj == null || obj is DerInteger)
			{
				return (DerInteger)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004A804 File Offset: 0x0004A804
		public static DerInteger GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerInteger)
			{
				return DerInteger.GetInstance(@object);
			}
			return new DerInteger(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004A858 File Offset: 0x0004A858
		public DerInteger(int value)
		{
			this.bytes = BigInteger.ValueOf((long)value).ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004A87C File Offset: 0x0004A87C
		public DerInteger(long value)
		{
			this.bytes = BigInteger.ValueOf(value).ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0004A89C File Offset: 0x0004A89C
		public DerInteger(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.bytes = value.ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0004A8C8 File Offset: 0x0004A8C8
		public DerInteger(byte[] bytes) : this(bytes, true)
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0004A8D4 File Offset: 0x0004A8D4
		internal DerInteger(byte[] bytes, bool clone)
		{
			if (DerInteger.IsMalformed(bytes))
			{
				throw new ArgumentException("malformed integer", "bytes");
			}
			this.bytes = (clone ? Arrays.Clone(bytes) : bytes);
			this.start = DerInteger.SignBytesToSkip(bytes);
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0004A92C File Offset: 0x0004A92C
		public BigInteger PositiveValue
		{
			get
			{
				return new BigInteger(1, this.bytes);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0004A93C File Offset: 0x0004A93C
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0004A94C File Offset: 0x0004A94C
		public bool HasValue(BigInteger x)
		{
			return x != null && DerInteger.IntValue(this.bytes, this.start, -1) == x.IntValue && this.Value.Equals(x);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0004A980 File Offset: 0x0004A980
		public int IntPositiveValueExact
		{
			get
			{
				int num = this.bytes.Length - this.start;
				if (num > 4 || (num == 4 && (this.bytes[this.start] & 128) != 0))
				{
					throw new ArithmeticException("ASN.1 Integer out of positive int range");
				}
				return DerInteger.IntValue(this.bytes, this.start, 255);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0004A9E8 File Offset: 0x0004A9E8
		public int IntValueExact
		{
			get
			{
				int num = this.bytes.Length - this.start;
				if (num > 4)
				{
					throw new ArithmeticException("ASN.1 Integer out of int range");
				}
				return DerInteger.IntValue(this.bytes, this.start, -1);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0004AA30 File Offset: 0x0004AA30
		public long LongValueExact
		{
			get
			{
				int num = this.bytes.Length - this.start;
				if (num > 8)
				{
					throw new ArithmeticException("ASN.1 Integer out of long range");
				}
				return DerInteger.LongValue(this.bytes, this.start, -1);
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0004AA78 File Offset: 0x0004AA78
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(2, this.bytes);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0004AA88 File Offset: 0x0004AA88
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0004AA98 File Offset: 0x0004AA98
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerInteger derInteger = asn1Object as DerInteger;
			return derInteger != null && Arrays.AreEqual(this.bytes, derInteger.bytes);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0004AACC File Offset: 0x0004AACC
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0004AADC File Offset: 0x0004AADC
		internal static int IntValue(byte[] bytes, int start, int signExt)
		{
			int num = bytes.Length;
			int num2 = Math.Max(start, num - 4);
			int num3 = (int)((sbyte)bytes[num2]) & signExt;
			while (++num2 < num)
			{
				num3 = (num3 << 8 | (int)bytes[num2]);
			}
			return num3;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0004AB18 File Offset: 0x0004AB18
		internal static long LongValue(byte[] bytes, int start, int signExt)
		{
			int num = bytes.Length;
			int num2 = Math.Max(start, num - 8);
			long num3 = (long)((int)((sbyte)bytes[num2]) & signExt);
			while (++num2 < num)
			{
				num3 = (num3 << 8 | (long)((ulong)bytes[num2]));
			}
			return num3;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0004AB58 File Offset: 0x0004AB58
		internal static bool IsMalformed(byte[] bytes)
		{
			switch (bytes.Length)
			{
			case 0:
				return true;
			case 1:
				return false;
			default:
				return (int)((sbyte)bytes[0]) == (sbyte)bytes[1] >> 7 && !DerInteger.AllowUnsafe();
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0004ABA0 File Offset: 0x0004ABA0
		internal static int SignBytesToSkip(byte[] bytes)
		{
			int num = 0;
			int num2 = bytes.Length - 1;
			while (num < num2 && (int)((sbyte)bytes[num]) == (sbyte)bytes[num + 1] >> 7)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000799 RID: 1945
		public const string AllowUnsafeProperty = "Org.BouncyCastle.Asn1.AllowUnsafeInteger";

		// Token: 0x0400079A RID: 1946
		internal const int SignExtSigned = -1;

		// Token: 0x0400079B RID: 1947
		internal const int SignExtUnsigned = 255;

		// Token: 0x0400079C RID: 1948
		private readonly byte[] bytes;

		// Token: 0x0400079D RID: 1949
		private readonly int start;
	}
}
