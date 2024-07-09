using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027A RID: 634
	public class DerVisibleString : DerStringBase
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0006CC30 File Offset: 0x0006CC30
		public static DerVisibleString GetInstance(object obj)
		{
			if (obj == null || obj is DerVisibleString)
			{
				return (DerVisibleString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerVisibleString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerVisibleString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006CCA8 File Offset: 0x0006CCA8
		public static DerVisibleString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerVisibleString.GetInstance(obj.GetObject());
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006CCB8 File Offset: 0x0006CCB8
		public DerVisibleString(byte[] str) : this(Strings.FromAsciiByteArray(str))
		{
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0006CCC8 File Offset: 0x0006CCC8
		public DerVisibleString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0006CCE8 File Offset: 0x0006CCE8
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0006CCF0 File Offset: 0x0006CCF0
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0006CD00 File Offset: 0x0006CD00
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(26, this.GetOctets());
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0006CD10 File Offset: 0x0006CD10
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerVisibleString derVisibleString = asn1Object as DerVisibleString;
			return derVisibleString != null && this.str.Equals(derVisibleString.str);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006CD44 File Offset: 0x0006CD44
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x04000DC3 RID: 3523
		private readonly string str;
	}
}
