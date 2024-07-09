using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C4 RID: 452
	public class SmimeCapabilitiesAttribute : AttributeX509
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00058D58 File Offset: 0x00058D58
		public SmimeCapabilitiesAttribute(SmimeCapabilityVector capabilities) : base(SmimeAttributes.SmimeCapabilities, new DerSet(new DerSequence(capabilities.ToAsn1EncodableVector())))
		{
		}
	}
}
