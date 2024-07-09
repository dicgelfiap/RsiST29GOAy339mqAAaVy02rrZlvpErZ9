using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200024D RID: 589
	public class BerBitString : DerBitString
	{
		// Token: 0x060012E7 RID: 4839 RVA: 0x00069974 File Offset: 0x00069974
		public BerBitString(byte[] data, int padBits) : base(data, padBits)
		{
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00069980 File Offset: 0x00069980
		public BerBitString(byte[] data) : base(data)
		{
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0006998C File Offset: 0x0006998C
		public BerBitString(int namedBits) : base(namedBits)
		{
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00069998 File Offset: 0x00069998
		public BerBitString(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000699A4 File Offset: 0x000699A4
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteEncoded(3, (byte)this.mPadBits, this.mData);
				return;
			}
			base.Encode(derOut);
		}
	}
}
