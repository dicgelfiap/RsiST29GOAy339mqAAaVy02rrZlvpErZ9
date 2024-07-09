using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045A RID: 1114
	public class HkdfParameters : IDerivationParameters
	{
		// Token: 0x060022E1 RID: 8929 RVA: 0x000C5C54 File Offset: 0x000C5C54
		private HkdfParameters(byte[] ikm, bool skip, byte[] salt, byte[] info)
		{
			if (ikm == null)
			{
				throw new ArgumentNullException("ikm");
			}
			this.ikm = Arrays.Clone(ikm);
			this.skipExpand = skip;
			if (salt == null || salt.Length == 0)
			{
				this.salt = null;
			}
			else
			{
				this.salt = Arrays.Clone(salt);
			}
			if (info == null)
			{
				this.info = new byte[0];
				return;
			}
			this.info = Arrays.Clone(info);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000C5CD8 File Offset: 0x000C5CD8
		public HkdfParameters(byte[] ikm, byte[] salt, byte[] info) : this(ikm, false, salt, info)
		{
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000C5CE4 File Offset: 0x000C5CE4
		public static HkdfParameters SkipExtractParameters(byte[] ikm, byte[] info)
		{
			return new HkdfParameters(ikm, true, null, info);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000C5CF0 File Offset: 0x000C5CF0
		public static HkdfParameters DefaultParameters(byte[] ikm)
		{
			return new HkdfParameters(ikm, false, null, null);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000C5CFC File Offset: 0x000C5CFC
		public virtual byte[] GetIkm()
		{
			return Arrays.Clone(this.ikm);
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000C5D0C File Offset: 0x000C5D0C
		public virtual bool SkipExtract
		{
			get
			{
				return this.skipExpand;
			}
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000C5D14 File Offset: 0x000C5D14
		public virtual byte[] GetSalt()
		{
			return Arrays.Clone(this.salt);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000C5D24 File Offset: 0x000C5D24
		public virtual byte[] GetInfo()
		{
			return Arrays.Clone(this.info);
		}

		// Token: 0x04001633 RID: 5683
		private readonly byte[] ikm;

		// Token: 0x04001634 RID: 5684
		private readonly bool skipExpand;

		// Token: 0x04001635 RID: 5685
		private readonly byte[] salt;

		// Token: 0x04001636 RID: 5686
		private readonly byte[] info;
	}
}
