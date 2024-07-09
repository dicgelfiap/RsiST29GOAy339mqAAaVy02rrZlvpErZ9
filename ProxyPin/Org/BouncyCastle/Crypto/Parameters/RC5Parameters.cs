using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046E RID: 1134
	public class RC5Parameters : KeyParameter
	{
		// Token: 0x0600233E RID: 9022 RVA: 0x000C6568 File Offset: 0x000C6568
		public RC5Parameters(byte[] key, int rounds) : base(key)
		{
			if (key.Length > 255)
			{
				throw new ArgumentException("RC5 key length can be no greater than 255");
			}
			this.rounds = rounds;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000C6590 File Offset: 0x000C6590
		public int Rounds
		{
			get
			{
				return this.rounds;
			}
		}

		// Token: 0x04001665 RID: 5733
		private readonly int rounds;
	}
}
