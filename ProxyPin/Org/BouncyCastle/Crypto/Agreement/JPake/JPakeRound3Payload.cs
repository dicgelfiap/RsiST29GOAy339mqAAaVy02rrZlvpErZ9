using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x02000332 RID: 818
	public class JPakeRound3Payload
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0007E824 File Offset: 0x0007E824
		public JPakeRound3Payload(string participantId, BigInteger magTag)
		{
			this.participantId = participantId;
			this.macTag = magTag;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0007E83C File Offset: 0x0007E83C
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0007E844 File Offset: 0x0007E844
		public virtual BigInteger MacTag
		{
			get
			{
				return this.macTag;
			}
		}

		// Token: 0x04001048 RID: 4168
		private readonly string participantId;

		// Token: 0x04001049 RID: 4169
		private readonly BigInteger macTag;
	}
}
