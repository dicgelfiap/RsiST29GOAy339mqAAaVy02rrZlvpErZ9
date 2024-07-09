using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000266 RID: 614
	public class DerBmpString : DerStringBase
	{
		// Token: 0x0600137D RID: 4989 RVA: 0x0006AE40 File Offset: 0x0006AE40
		public static DerBmpString GetInstance(object obj)
		{
			if (obj == null || obj is DerBmpString)
			{
				return (DerBmpString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006AE70 File Offset: 0x0006AE70
		public static DerBmpString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerBmpString)
			{
				return DerBmpString.GetInstance(@object);
			}
			return new DerBmpString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006AEB0 File Offset: 0x0006AEB0
		[Obsolete("Will become internal")]
		public DerBmpString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int num = str.Length;
			if ((num & 1) != 0)
			{
				throw new ArgumentException("malformed BMPString encoding encountered", "str");
			}
			int num2 = num / 2;
			char[] array = new char[num2];
			for (int num3 = 0; num3 != num2; num3++)
			{
				array[num3] = (char)((int)str[2 * num3] << 8 | (int)(str[2 * num3 + 1] & byte.MaxValue));
			}
			this.str = new string(array);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0006AF34 File Offset: 0x0006AF34
		internal DerBmpString(char[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = new string(str);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0006AF5C File Offset: 0x0006AF5C
		public DerBmpString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0006AF7C File Offset: 0x0006AF7C
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0006AF84 File Offset: 0x0006AF84
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBmpString derBmpString = asn1Object as DerBmpString;
			return derBmpString != null && this.str.Equals(derBmpString.str);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0006AFB8 File Offset: 0x0006AFB8
		internal override void Encode(DerOutputStream derOut)
		{
			char[] array = this.str.ToCharArray();
			byte[] array2 = new byte[array.Length * 2];
			for (int num = 0; num != array.Length; num++)
			{
				array2[2 * num] = (byte)(array[num] >> 8);
				array2[2 * num + 1] = (byte)array[num];
			}
			derOut.WriteEncoded(30, array2);
		}

		// Token: 0x04000DA6 RID: 3494
		private readonly string str;
	}
}
