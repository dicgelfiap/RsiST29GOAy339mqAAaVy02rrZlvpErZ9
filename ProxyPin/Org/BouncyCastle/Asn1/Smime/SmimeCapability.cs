using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C5 RID: 453
	public class SmimeCapability : Asn1Encodable
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00058D78 File Offset: 0x00058D78
		public SmimeCapability(Asn1Sequence seq)
		{
			this.capabilityID = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.parameters = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00058DC4 File Offset: 0x00058DC4
		public SmimeCapability(DerObjectIdentifier capabilityID, Asn1Encodable parameters)
		{
			if (capabilityID == null)
			{
				throw new ArgumentNullException("capabilityID");
			}
			this.capabilityID = capabilityID;
			if (parameters != null)
			{
				this.parameters = parameters.ToAsn1Object();
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00058DF8 File Offset: 0x00058DF8
		public static SmimeCapability GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapability)
			{
				return (SmimeCapability)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapability((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid SmimeCapability");
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00058E34 File Offset: 0x00058E34
		public DerObjectIdentifier CapabilityID
		{
			get
			{
				return this.capabilityID;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00058E3C File Offset: 0x00058E3C
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00058E44 File Offset: 0x00058E44
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.capabilityID
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.parameters
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000AFC RID: 2812
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x04000AFD RID: 2813
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x04000AFE RID: 2814
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x04000AFF RID: 2815
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x04000B00 RID: 2816
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x04000B01 RID: 2817
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x04000B02 RID: 2818
		private DerObjectIdentifier capabilityID;

		// Token: 0x04000B03 RID: 2819
		private Asn1Object parameters;
	}
}
