using System;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000329 RID: 809
	internal class DefaultPKMacResult : IBlockResult
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x0007D800 File Offset: 0x0007D800
		public DefaultPKMacResult(IMac mac)
		{
			this.mac = mac;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0007D810 File Offset: 0x0007D810
		public byte[] Collect()
		{
			byte[] array = new byte[this.mac.GetMacSize()];
			this.mac.DoFinal(array, 0);
			return array;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0007D844 File Offset: 0x0007D844
		public int Collect(byte[] sig, int sigOff)
		{
			byte[] array = this.Collect();
			array.CopyTo(sig, sigOff);
			return array.Length;
		}

		// Token: 0x04001013 RID: 4115
		private readonly IMac mac;
	}
}
