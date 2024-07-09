using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000275 RID: 629
	public class DerT61String : DerStringBase
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0006C3E4 File Offset: 0x0006C3E4
		public static DerT61String GetInstance(object obj)
		{
			if (obj == null || obj is DerT61String)
			{
				return (DerT61String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0006C414 File Offset: 0x0006C414
		public static DerT61String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerT61String)
			{
				return DerT61String.GetInstance(@object);
			}
			return new DerT61String(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0006C454 File Offset: 0x0006C454
		public DerT61String(byte[] str) : this(Strings.FromByteArray(str))
		{
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0006C464 File Offset: 0x0006C464
		public DerT61String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0006C484 File Offset: 0x0006C484
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0006C48C File Offset: 0x0006C48C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(20, this.GetOctets());
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0006C49C File Offset: 0x0006C49C
		public byte[] GetOctets()
		{
			return Strings.ToByteArray(this.str);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006C4AC File Offset: 0x0006C4AC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerT61String derT61String = asn1Object as DerT61String;
			return derT61String != null && this.str.Equals(derT61String.str);
		}

		// Token: 0x04000DBD RID: 3517
		private readonly string str;
	}
}
