using System;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000521 RID: 1313
	public class SimulatedTlsSrpIdentityManager : TlsSrpIdentityManager
	{
		// Token: 0x060027F1 RID: 10225 RVA: 0x000D6FE4 File Offset: 0x000D6FE4
		public static SimulatedTlsSrpIdentityManager GetRfc5054Default(Srp6GroupParameters group, byte[] seedKey)
		{
			Srp6VerifierGenerator srp6VerifierGenerator = new Srp6VerifierGenerator();
			srp6VerifierGenerator.Init(group, TlsUtilities.CreateHash(2));
			HMac hmac = new HMac(TlsUtilities.CreateHash(2));
			hmac.Init(new KeyParameter(seedKey));
			return new SimulatedTlsSrpIdentityManager(group, srp6VerifierGenerator, hmac);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000D7028 File Offset: 0x000D7028
		public SimulatedTlsSrpIdentityManager(Srp6GroupParameters group, Srp6VerifierGenerator verifierGenerator, IMac mac)
		{
			this.mGroup = group;
			this.mVerifierGenerator = verifierGenerator;
			this.mMac = mac;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000D7048 File Offset: 0x000D7048
		public virtual TlsSrpLoginParameters GetLoginParameters(byte[] identity)
		{
			this.mMac.BlockUpdate(SimulatedTlsSrpIdentityManager.PREFIX_SALT, 0, SimulatedTlsSrpIdentityManager.PREFIX_SALT.Length);
			this.mMac.BlockUpdate(identity, 0, identity.Length);
			byte[] array = new byte[this.mMac.GetMacSize()];
			this.mMac.DoFinal(array, 0);
			this.mMac.BlockUpdate(SimulatedTlsSrpIdentityManager.PREFIX_PASSWORD, 0, SimulatedTlsSrpIdentityManager.PREFIX_PASSWORD.Length);
			this.mMac.BlockUpdate(identity, 0, identity.Length);
			byte[] array2 = new byte[this.mMac.GetMacSize()];
			this.mMac.DoFinal(array2, 0);
			BigInteger verifier = this.mVerifierGenerator.GenerateVerifier(array, identity, array2);
			return new TlsSrpLoginParameters(this.mGroup, verifier, array);
		}

		// Token: 0x04001A52 RID: 6738
		private static readonly byte[] PREFIX_PASSWORD = Strings.ToByteArray("password");

		// Token: 0x04001A53 RID: 6739
		private static readonly byte[] PREFIX_SALT = Strings.ToByteArray("salt");

		// Token: 0x04001A54 RID: 6740
		protected readonly Srp6GroupParameters mGroup;

		// Token: 0x04001A55 RID: 6741
		protected readonly Srp6VerifierGenerator mVerifierGenerator;

		// Token: 0x04001A56 RID: 6742
		protected readonly IMac mMac;
	}
}
