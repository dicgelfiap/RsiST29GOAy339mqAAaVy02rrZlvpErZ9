using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004CC RID: 1228
	public class BasicTlsPskIdentity : TlsPskIdentity
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x000CF86C File Offset: 0x000CF86C
		public BasicTlsPskIdentity(byte[] identity, byte[] psk)
		{
			this.mIdentity = Arrays.Clone(identity);
			this.mPsk = Arrays.Clone(psk);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000CF88C File Offset: 0x000CF88C
		public BasicTlsPskIdentity(string identity, byte[] psk)
		{
			this.mIdentity = Strings.ToUtf8ByteArray(identity);
			this.mPsk = Arrays.Clone(psk);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000CF8AC File Offset: 0x000CF8AC
		public virtual void SkipIdentityHint()
		{
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000CF8B0 File Offset: 0x000CF8B0
		public virtual void NotifyIdentityHint(byte[] psk_identity_hint)
		{
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000CF8B4 File Offset: 0x000CF8B4
		public virtual byte[] GetPskIdentity()
		{
			return this.mIdentity;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000CF8BC File Offset: 0x000CF8BC
		public virtual byte[] GetPsk()
		{
			return Arrays.Clone(this.mPsk);
		}

		// Token: 0x040017C8 RID: 6088
		protected byte[] mIdentity;

		// Token: 0x040017C9 RID: 6089
		protected byte[] mPsk;
	}
}
