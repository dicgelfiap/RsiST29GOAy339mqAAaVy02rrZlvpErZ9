using System;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C6 RID: 454
	public class SmimeCapabilityVector
	{
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00058EE8 File Offset: 0x00058EE8
		public void AddCapability(DerObjectIdentifier capability)
		{
			this.capabilities.Add(new DerSequence(capability));
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00058EFC File Offset: 0x00058EFC
		public void AddCapability(DerObjectIdentifier capability, int value)
		{
			this.capabilities.Add(new DerSequence(new Asn1Encodable[]
			{
				capability,
				new DerInteger(value)
			}));
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00058F3C File Offset: 0x00058F3C
		public void AddCapability(DerObjectIdentifier capability, Asn1Encodable parameters)
		{
			this.capabilities.Add(new DerSequence(new Asn1Encodable[]
			{
				capability,
				parameters
			}));
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00058F78 File Offset: 0x00058F78
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			return this.capabilities;
		}

		// Token: 0x04000B04 RID: 2820
		private readonly Asn1EncodableVector capabilities = new Asn1EncodableVector();
	}
}
