using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000194 RID: 404
	public class DerEnumerated : Asn1Object
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x000539E8 File Offset: 0x000539E8
		public static DerEnumerated GetInstance(object obj)
		{
			if (obj == null || obj is DerEnumerated)
			{
				return (DerEnumerated)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00053A18 File Offset: 0x00053A18
		public static DerEnumerated GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerEnumerated)
			{
				return DerEnumerated.GetInstance(@object);
			}
			return DerEnumerated.FromOctetString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00053A58 File Offset: 0x00053A58
		public DerEnumerated(int val)
		{
			if (val < 0)
			{
				throw new ArgumentException("enumerated must be non-negative", "val");
			}
			this.bytes = BigInteger.ValueOf((long)val).ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00053A90 File Offset: 0x00053A90
		public DerEnumerated(long val)
		{
			if (val < 0L)
			{
				throw new ArgumentException("enumerated must be non-negative", "val");
			}
			this.bytes = BigInteger.ValueOf(val).ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00053AC8 File Offset: 0x00053AC8
		public DerEnumerated(BigInteger val)
		{
			if (val.SignValue < 0)
			{
				throw new ArgumentException("enumerated must be non-negative", "val");
			}
			this.bytes = val.ToByteArray();
			this.start = 0;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00053B00 File Offset: 0x00053B00
		public DerEnumerated(byte[] bytes)
		{
			if (DerInteger.IsMalformed(bytes))
			{
				throw new ArgumentException("malformed enumerated", "bytes");
			}
			if ((bytes[0] & 128) != 0)
			{
				throw new ArgumentException("enumerated must be non-negative", "bytes");
			}
			this.bytes = Arrays.Clone(bytes);
			this.start = DerInteger.SignBytesToSkip(bytes);
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00053B68 File Offset: 0x00053B68
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00053B78 File Offset: 0x00053B78
		public bool HasValue(BigInteger x)
		{
			return x != null && DerInteger.IntValue(this.bytes, this.start, -1) == x.IntValue && this.Value.Equals(x);
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00053BAC File Offset: 0x00053BAC
		public int IntValueExact
		{
			get
			{
				int num = this.bytes.Length - this.start;
				if (num > 4)
				{
					throw new ArithmeticException("ASN.1 Enumerated out of int range");
				}
				return DerInteger.IntValue(this.bytes, this.start, -1);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00053BF4 File Offset: 0x00053BF4
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(10, this.bytes);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00053C04 File Offset: 0x00053C04
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerEnumerated derEnumerated = asn1Object as DerEnumerated;
			return derEnumerated != null && Arrays.AreEqual(this.bytes, derEnumerated.bytes);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00053C38 File Offset: 0x00053C38
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00053C48 File Offset: 0x00053C48
		internal static DerEnumerated FromOctetString(byte[] enc)
		{
			if (enc.Length > 1)
			{
				return new DerEnumerated(enc);
			}
			if (enc.Length == 0)
			{
				throw new ArgumentException("ENUMERATED has zero length", "enc");
			}
			int num = (int)enc[0];
			if (num >= DerEnumerated.cache.Length)
			{
				return new DerEnumerated(enc);
			}
			DerEnumerated derEnumerated = DerEnumerated.cache[num];
			if (derEnumerated == null)
			{
				derEnumerated = (DerEnumerated.cache[num] = new DerEnumerated(enc));
			}
			return derEnumerated;
		}

		// Token: 0x04000995 RID: 2453
		private readonly byte[] bytes;

		// Token: 0x04000996 RID: 2454
		private readonly int start;

		// Token: 0x04000997 RID: 2455
		private static readonly DerEnumerated[] cache = new DerEnumerated[12];
	}
}
