using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000278 RID: 632
	public class DerUtf8String : DerStringBase
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x0006C9D8 File Offset: 0x0006C9D8
		public static DerUtf8String GetInstance(object obj)
		{
			if (obj == null || obj is DerUtf8String)
			{
				return (DerUtf8String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006CA08 File Offset: 0x0006CA08
		public static DerUtf8String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUtf8String)
			{
				return DerUtf8String.GetInstance(@object);
			}
			return new DerUtf8String(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0006CA48 File Offset: 0x0006CA48
		public DerUtf8String(byte[] str) : this(Encoding.UTF8.GetString(str, 0, str.Length))
		{
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006CA60 File Offset: 0x0006CA60
		public DerUtf8String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006CA80 File Offset: 0x0006CA80
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0006CA88 File Offset: 0x0006CA88
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtf8String derUtf8String = asn1Object as DerUtf8String;
			return derUtf8String != null && this.str.Equals(derUtf8String.str);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0006CABC File Offset: 0x0006CABC
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(12, Encoding.UTF8.GetBytes(this.str));
		}

		// Token: 0x04000DC1 RID: 3521
		private readonly string str;
	}
}
