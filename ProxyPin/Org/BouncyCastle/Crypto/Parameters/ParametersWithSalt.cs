using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046B RID: 1131
	public class ParametersWithSalt : ICipherParameters
	{
		// Token: 0x06002332 RID: 9010 RVA: 0x000C646C File Offset: 0x000C646C
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt) : this(parameters, salt, 0, salt.Length)
		{
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000C647C File Offset: 0x000C647C
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt, int saltOff, int saltLen)
		{
			this.salt = new byte[saltLen];
			this.parameters = parameters;
			Array.Copy(salt, saltOff, this.salt, 0, saltLen);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000C64A8 File Offset: 0x000C64A8
		public byte[] GetSalt()
		{
			return this.salt;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x000C64B0 File Offset: 0x000C64B0
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001660 RID: 5728
		private byte[] salt;

		// Token: 0x04001661 RID: 5729
		private ICipherParameters parameters;
	}
}
