using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000435 RID: 1077
	public abstract class AsymmetricKeyParameter : ICipherParameters
	{
		// Token: 0x060021F5 RID: 8693 RVA: 0x000C3A48 File Offset: 0x000C3A48
		protected AsymmetricKeyParameter(bool privateKey)
		{
			this.privateKey = privateKey;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000C3A58 File Offset: 0x000C3A58
		public bool IsPrivate
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000C3A60 File Offset: 0x000C3A60
		public override bool Equals(object obj)
		{
			AsymmetricKeyParameter asymmetricKeyParameter = obj as AsymmetricKeyParameter;
			return asymmetricKeyParameter != null && this.Equals(asymmetricKeyParameter);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000C3A88 File Offset: 0x000C3A88
		protected bool Equals(AsymmetricKeyParameter other)
		{
			return this.privateKey == other.privateKey;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000C3A98 File Offset: 0x000C3A98
		public override int GetHashCode()
		{
			return this.privateKey.GetHashCode();
		}

		// Token: 0x040015DF RID: 5599
		private readonly bool privateKey;
	}
}
