using System;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200021F RID: 543
	public abstract class X509NameEntryConverter
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x00063490 File Offset: 0x00063490
		protected Asn1Object ConvertHexEncoded(string hexString, int offset)
		{
			return Asn1Object.FromByteArray(Hex.DecodeStrict(hexString, offset, hexString.Length - offset));
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000634A8 File Offset: 0x000634A8
		protected bool CanBePrintable(string str)
		{
			return DerPrintableString.IsPrintableString(str);
		}

		// Token: 0x0600118B RID: 4491
		public abstract Asn1Object GetConvertedValue(DerObjectIdentifier oid, string value);
	}
}
