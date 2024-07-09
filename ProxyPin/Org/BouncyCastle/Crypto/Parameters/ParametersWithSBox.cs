using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046C RID: 1132
	public class ParametersWithSBox : ICipherParameters
	{
		// Token: 0x06002336 RID: 9014 RVA: 0x000C64B8 File Offset: 0x000C64B8
		public ParametersWithSBox(ICipherParameters parameters, byte[] sBox)
		{
			this.parameters = parameters;
			this.sBox = sBox;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000C64D0 File Offset: 0x000C64D0
		public byte[] GetSBox()
		{
			return this.sBox;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000C64D8 File Offset: 0x000C64D8
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001662 RID: 5730
		private ICipherParameters parameters;

		// Token: 0x04001663 RID: 5731
		private byte[] sBox;
	}
}
