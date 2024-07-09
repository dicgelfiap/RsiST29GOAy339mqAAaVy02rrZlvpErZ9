using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000246 RID: 582
	public interface Asn1TaggedObjectParser : IAsn1Convertible
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060012C3 RID: 4803
		int TagNo { get; }

		// Token: 0x060012C4 RID: 4804
		IAsn1Convertible GetObjectParser(int tag, bool isExplicit);
	}
}
