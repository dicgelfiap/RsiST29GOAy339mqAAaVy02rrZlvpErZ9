using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026B RID: 619
	public class DerGeneralString : DerStringBase
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x0006BB20 File Offset: 0x0006BB20
		public static DerGeneralString GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralString)
			{
				return (DerGeneralString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0006BB50 File Offset: 0x0006BB50
		public static DerGeneralString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGeneralString)
			{
				return DerGeneralString.GetInstance(@object);
			}
			return new DerGeneralString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0006BB90 File Offset: 0x0006BB90
		public DerGeneralString(byte[] str) : this(Strings.FromAsciiByteArray(str))
		{
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0006BBA0 File Offset: 0x0006BBA0
		public DerGeneralString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0006BBC0 File Offset: 0x0006BBC0
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0006BBC8 File Offset: 0x0006BBC8
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0006BBD8 File Offset: 0x0006BBD8
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(27, this.GetOctets());
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0006BBE8 File Offset: 0x0006BBE8
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralString derGeneralString = asn1Object as DerGeneralString;
			return derGeneralString != null && this.str.Equals(derGeneralString.str);
		}

		// Token: 0x04000DB1 RID: 3505
		private readonly string str;
	}
}
