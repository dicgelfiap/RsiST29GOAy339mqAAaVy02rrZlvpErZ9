using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000186 RID: 390
	public class DerIA5String : DerStringBase
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x000526DC File Offset: 0x000526DC
		public static DerIA5String GetInstance(object obj)
		{
			if (obj == null || obj is DerIA5String)
			{
				return (DerIA5String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0005270C File Offset: 0x0005270C
		public static DerIA5String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerIA5String)
			{
				return DerIA5String.GetInstance(@object);
			}
			return new DerIA5String(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0005274C File Offset: 0x0005274C
		public DerIA5String(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0005275C File Offset: 0x0005275C
		public DerIA5String(string str) : this(str, false)
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00052768 File Offset: 0x00052768
		public DerIA5String(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerIA5String.IsIA5String(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000527B8 File Offset: 0x000527B8
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000527C0 File Offset: 0x000527C0
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000527D0 File Offset: 0x000527D0
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(22, this.GetOctets());
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000527E0 File Offset: 0x000527E0
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000527F0 File Offset: 0x000527F0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerIA5String derIA5String = asn1Object as DerIA5String;
			return derIA5String != null && this.str.Equals(derIA5String.str);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00052824 File Offset: 0x00052824
		public static bool IsIA5String(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000932 RID: 2354
		private readonly string str;
	}
}
