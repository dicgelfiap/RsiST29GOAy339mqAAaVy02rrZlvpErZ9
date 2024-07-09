using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000469 RID: 1129
	public class ParametersWithIV : ICipherParameters
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000C63AC File Offset: 0x000C63AC
		public ParametersWithIV(ICipherParameters parameters, byte[] iv) : this(parameters, iv, 0, iv.Length)
		{
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000C63BC File Offset: 0x000C63BC
		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			this.parameters = parameters;
			this.iv = Arrays.CopyOfRange(iv, ivOff, ivOff + ivLen);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000C63F0 File Offset: 0x000C63F0
		public byte[] GetIV()
		{
			return (byte[])this.iv.Clone();
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000C6404 File Offset: 0x000C6404
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0400165C RID: 5724
		private readonly ICipherParameters parameters;

		// Token: 0x0400165D RID: 5725
		private readonly byte[] iv;
	}
}
