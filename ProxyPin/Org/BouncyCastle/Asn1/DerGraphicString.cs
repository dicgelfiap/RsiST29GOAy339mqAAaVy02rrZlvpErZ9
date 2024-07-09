using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026D RID: 621
	public class DerGraphicString : DerStringBase
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0006BD78 File Offset: 0x0006BD78
		public static DerGraphicString GetInstance(object obj)
		{
			if (obj == null || obj is DerGraphicString)
			{
				return (DerGraphicString)obj;
			}
			if (obj is byte[])
			{
				try
				{
					return (DerGraphicString)Asn1Object.FromByteArray((byte[])obj);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("encoding error in GetInstance: " + ex.ToString(), "obj");
				}
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0006BE08 File Offset: 0x0006BE08
		public static DerGraphicString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGraphicString)
			{
				return DerGraphicString.GetInstance(@object);
			}
			return new DerGraphicString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0006BE48 File Offset: 0x0006BE48
		public DerGraphicString(byte[] encoding)
		{
			this.mString = Arrays.Clone(encoding);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0006BE5C File Offset: 0x0006BE5C
		public override string GetString()
		{
			return Strings.FromByteArray(this.mString);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0006BE6C File Offset: 0x0006BE6C
		public byte[] GetOctets()
		{
			return Arrays.Clone(this.mString);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0006BE7C File Offset: 0x0006BE7C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(25, this.mString);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0006BE8C File Offset: 0x0006BE8C
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.mString);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0006BE9C File Offset: 0x0006BE9C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGraphicString derGraphicString = asn1Object as DerGraphicString;
			return derGraphicString != null && Arrays.AreEqual(this.mString, derGraphicString.mString);
		}

		// Token: 0x04000DB5 RID: 3509
		private readonly byte[] mString;
	}
}
