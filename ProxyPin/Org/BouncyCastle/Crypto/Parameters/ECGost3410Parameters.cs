using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000444 RID: 1092
	public class ECGost3410Parameters : ECNamedDomainParameters
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000C498C File Offset: 0x000C498C
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this._publicKeyParamSet;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000C4994 File Offset: 0x000C4994
		public DerObjectIdentifier DigestParamSet
		{
			get
			{
				return this._digestParamSet;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000C499C File Offset: 0x000C499C
		public DerObjectIdentifier EncryptionParamSet
		{
			get
			{
				return this._encryptionParamSet;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000C49A4 File Offset: 0x000C49A4
		public ECGost3410Parameters(ECNamedDomainParameters dp, DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet) : base(dp.Name, dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed())
		{
			this._publicKeyParamSet = publicKeyParamSet;
			this._digestParamSet = digestParamSet;
			this._encryptionParamSet = encryptionParamSet;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000C49F8 File Offset: 0x000C49F8
		public ECGost3410Parameters(ECDomainParameters dp, DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet) : base(publicKeyParamSet, dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed())
		{
			this._publicKeyParamSet = publicKeyParamSet;
			this._digestParamSet = digestParamSet;
			this._encryptionParamSet = encryptionParamSet;
		}

		// Token: 0x04001607 RID: 5639
		private readonly DerObjectIdentifier _publicKeyParamSet;

		// Token: 0x04001608 RID: 5640
		private readonly DerObjectIdentifier _digestParamSet;

		// Token: 0x04001609 RID: 5641
		private readonly DerObjectIdentifier _encryptionParamSet;
	}
}
