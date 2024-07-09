using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000327 RID: 807
	internal class PKMacFactory : IMacFactory
	{
		// Token: 0x06001842 RID: 6210 RVA: 0x0007D790 File Offset: 0x0007D790
		public PKMacFactory(byte[] key, PbmParameter parameters)
		{
			this.key = Arrays.Clone(key);
			this.parameters = parameters;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0007D7AC File Offset: 0x0007D7AC
		public virtual object AlgorithmDetails
		{
			get
			{
				return new AlgorithmIdentifier(CmpObjectIdentifiers.passwordBasedMac, this.parameters);
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0007D7C0 File Offset: 0x0007D7C0
		public virtual IStreamCalculator CreateCalculator()
		{
			IMac mac = MacUtilities.GetMac(this.parameters.Mac.Algorithm);
			mac.Init(new KeyParameter(this.key));
			return new PKMacStreamCalculator(mac);
		}

		// Token: 0x04001011 RID: 4113
		protected readonly PbmParameter parameters;

		// Token: 0x04001012 RID: 4114
		private readonly byte[] key;
	}
}
