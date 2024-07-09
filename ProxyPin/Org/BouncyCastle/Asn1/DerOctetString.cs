using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000251 RID: 593
	public class DerOctetString : Asn1OctetString
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x00069BDC File Offset: 0x00069BDC
		public DerOctetString(byte[] str) : base(str)
		{
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00069BE8 File Offset: 0x00069BE8
		public DerOctetString(IAsn1Convertible obj) : this(obj.ToAsn1Object())
		{
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00069BF8 File Offset: 0x00069BF8
		public DerOctetString(Asn1Encodable obj) : base(obj.GetEncoded("DER"))
		{
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00069C0C File Offset: 0x00069C0C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(4, this.str);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00069C1C File Offset: 0x00069C1C
		internal static void Encode(DerOutputStream derOut, byte[] bytes, int offset, int length)
		{
			derOut.WriteEncoded(4, bytes, offset, length);
		}
	}
}
