using System;
using System.Threading;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BE RID: 1214
	internal abstract class AbstractTlsContext : TlsContext
	{
		// Token: 0x0600256E RID: 9582 RVA: 0x000CECA4 File Offset: 0x000CECA4
		private static long NextCounterValue()
		{
			return Interlocked.Increment(ref AbstractTlsContext.counter);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000CECB0 File Offset: 0x000CECB0
		internal AbstractTlsContext(SecureRandom secureRandom, SecurityParameters securityParameters)
		{
			IDigest digest = TlsUtilities.CreateHash(4);
			byte[] array = new byte[digest.GetDigestSize()];
			secureRandom.NextBytes(array);
			this.mNonceRandom = new DigestRandomGenerator(digest);
			this.mNonceRandom.AddSeedMaterial(AbstractTlsContext.NextCounterValue());
			this.mNonceRandom.AddSeedMaterial(Times.NanoTime());
			this.mNonceRandom.AddSeedMaterial(array);
			this.mSecureRandom = secureRandom;
			this.mSecurityParameters = securityParameters;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x000CED44 File Offset: 0x000CED44
		public virtual IRandomGenerator NonceRandomGenerator
		{
			get
			{
				return this.mNonceRandom;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x000CED4C File Offset: 0x000CED4C
		public virtual SecureRandom SecureRandom
		{
			get
			{
				return this.mSecureRandom;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000CED54 File Offset: 0x000CED54
		public virtual SecurityParameters SecurityParameters
		{
			get
			{
				return this.mSecurityParameters;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002573 RID: 9587
		public abstract bool IsServer { get; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000CED5C File Offset: 0x000CED5C
		public virtual ProtocolVersion ClientVersion
		{
			get
			{
				return this.mClientVersion;
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000CED64 File Offset: 0x000CED64
		internal virtual void SetClientVersion(ProtocolVersion clientVersion)
		{
			this.mClientVersion = clientVersion;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000CED70 File Offset: 0x000CED70
		public virtual ProtocolVersion ServerVersion
		{
			get
			{
				return this.mServerVersion;
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000CED78 File Offset: 0x000CED78
		internal virtual void SetServerVersion(ProtocolVersion serverVersion)
		{
			this.mServerVersion = serverVersion;
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000CED84 File Offset: 0x000CED84
		public virtual TlsSession ResumableSession
		{
			get
			{
				return this.mSession;
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000CED8C File Offset: 0x000CED8C
		internal virtual void SetResumableSession(TlsSession session)
		{
			this.mSession = session;
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000CED98 File Offset: 0x000CED98
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x000CEDA0 File Offset: 0x000CEDA0
		public virtual object UserObject
		{
			get
			{
				return this.mUserObject;
			}
			set
			{
				this.mUserObject = value;
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000CEDAC File Offset: 0x000CEDAC
		public virtual byte[] ExportKeyingMaterial(string asciiLabel, byte[] context_value, int length)
		{
			if (context_value != null && !TlsUtilities.IsValidUint16(context_value.Length))
			{
				throw new ArgumentException("must have length less than 2^16 (or be null)", "context_value");
			}
			SecurityParameters securityParameters = this.SecurityParameters;
			if (!securityParameters.IsExtendedMasterSecret)
			{
				throw new InvalidOperationException("cannot export keying material without extended_master_secret");
			}
			byte[] clientRandom = securityParameters.ClientRandom;
			byte[] serverRandom = securityParameters.ServerRandom;
			int num = clientRandom.Length + serverRandom.Length;
			if (context_value != null)
			{
				num += 2 + context_value.Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			Array.Copy(clientRandom, 0, array, num2, clientRandom.Length);
			num2 += clientRandom.Length;
			Array.Copy(serverRandom, 0, array, num2, serverRandom.Length);
			num2 += serverRandom.Length;
			if (context_value != null)
			{
				TlsUtilities.WriteUint16(context_value.Length, array, num2);
				num2 += 2;
				Array.Copy(context_value, 0, array, num2, context_value.Length);
				num2 += context_value.Length;
			}
			if (num2 != num)
			{
				throw new InvalidOperationException("error in calculation of seed for export");
			}
			return TlsUtilities.PRF(this, securityParameters.MasterSecret, asciiLabel, array, length);
		}

		// Token: 0x04001789 RID: 6025
		private static long counter = Times.NanoTime();

		// Token: 0x0400178A RID: 6026
		private readonly IRandomGenerator mNonceRandom;

		// Token: 0x0400178B RID: 6027
		private readonly SecureRandom mSecureRandom;

		// Token: 0x0400178C RID: 6028
		private readonly SecurityParameters mSecurityParameters;

		// Token: 0x0400178D RID: 6029
		private ProtocolVersion mClientVersion = null;

		// Token: 0x0400178E RID: 6030
		private ProtocolVersion mServerVersion = null;

		// Token: 0x0400178F RID: 6031
		private TlsSession mSession = null;

		// Token: 0x04001790 RID: 6032
		private object mUserObject = null;
	}
}
