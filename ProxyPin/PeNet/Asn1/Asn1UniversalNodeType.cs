using System;
using System.Runtime.InteropServices;

namespace PeNet.Asn1
{
	// Token: 0x02000B84 RID: 2948
	[ComVisible(true)]
	public enum Asn1UniversalNodeType
	{
		// Token: 0x04003994 RID: 14740
		Boolean = 1,
		// Token: 0x04003995 RID: 14741
		Integer,
		// Token: 0x04003996 RID: 14742
		BitString,
		// Token: 0x04003997 RID: 14743
		OctetString,
		// Token: 0x04003998 RID: 14744
		Null,
		// Token: 0x04003999 RID: 14745
		ObjectId,
		// Token: 0x0400399A RID: 14746
		ObjectDescriptor,
		// Token: 0x0400399B RID: 14747
		InstanceOf,
		// Token: 0x0400399C RID: 14748
		Real,
		// Token: 0x0400399D RID: 14749
		Enumerated,
		// Token: 0x0400399E RID: 14750
		EmbeddedPdv,
		// Token: 0x0400399F RID: 14751
		Utf8String,
		// Token: 0x040039A0 RID: 14752
		RelativeOid,
		// Token: 0x040039A1 RID: 14753
		Sequence = 16,
		// Token: 0x040039A2 RID: 14754
		Set,
		// Token: 0x040039A3 RID: 14755
		NumericString,
		// Token: 0x040039A4 RID: 14756
		PrintableString,
		// Token: 0x040039A5 RID: 14757
		TeletextString,
		// Token: 0x040039A6 RID: 14758
		VideotextString,
		// Token: 0x040039A7 RID: 14759
		Ia5String,
		// Token: 0x040039A8 RID: 14760
		UtcTime,
		// Token: 0x040039A9 RID: 14761
		GeneralizedTime,
		// Token: 0x040039AA RID: 14762
		GraphicString = 26,
		// Token: 0x040039AB RID: 14763
		VisibleString,
		// Token: 0x040039AC RID: 14764
		GeneralString = 26,
		// Token: 0x040039AD RID: 14765
		UniversalString = 28,
		// Token: 0x040039AE RID: 14766
		CharacterString,
		// Token: 0x040039AF RID: 14767
		BmpString
	}
}
