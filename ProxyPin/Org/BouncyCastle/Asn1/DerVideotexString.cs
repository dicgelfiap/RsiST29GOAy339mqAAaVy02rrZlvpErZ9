using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000279 RID: 633
	public class DerVideotexString : DerStringBase
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x0006CAD8 File Offset: 0x0006CAD8
		public static DerVideotexString GetInstance(object obj)
		{
			if (obj == null || obj is DerVideotexString)
			{
				return (DerVideotexString)obj;
			}
			if (obj is byte[])
			{
				try
				{
					return (DerVideotexString)Asn1Object.FromByteArray((byte[])obj);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("encoding error in GetInstance: " + ex.ToString(), "obj");
				}
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0006CB68 File Offset: 0x0006CB68
		public static DerVideotexString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerVideotexString)
			{
				return DerVideotexString.GetInstance(@object);
			}
			return new DerVideotexString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006CBA8 File Offset: 0x0006CBA8
		public DerVideotexString(byte[] encoding)
		{
			this.mString = Arrays.Clone(encoding);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0006CBBC File Offset: 0x0006CBBC
		public override string GetString()
		{
			return Strings.FromByteArray(this.mString);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006CBCC File Offset: 0x0006CBCC
		public byte[] GetOctets()
		{
			return Arrays.Clone(this.mString);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006CBDC File Offset: 0x0006CBDC
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(21, this.mString);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006CBEC File Offset: 0x0006CBEC
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.mString);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0006CBFC File Offset: 0x0006CBFC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerVideotexString derVideotexString = asn1Object as DerVideotexString;
			return derVideotexString != null && Arrays.AreEqual(this.mString, derVideotexString.mString);
		}

		// Token: 0x04000DC2 RID: 3522
		private readonly byte[] mString;
	}
}
