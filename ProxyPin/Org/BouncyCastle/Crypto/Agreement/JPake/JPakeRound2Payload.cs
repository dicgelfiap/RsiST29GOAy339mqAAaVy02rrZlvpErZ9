using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x02000331 RID: 817
	public class JPakeRound2Payload
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x0007E77C File Offset: 0x0007E77C
		public JPakeRound2Payload(string participantId, BigInteger a, BigInteger[] knowledgeProofForX2s)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(a, "a");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX2s, "knowledgeProofForX2s");
			this.participantId = participantId;
			this.a = a;
			this.knowledgeProofForX2s = new BigInteger[knowledgeProofForX2s.Length];
			knowledgeProofForX2s.CopyTo(this.knowledgeProofForX2s, 0);
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0007E7E0 File Offset: 0x0007E7E0
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0007E7E8 File Offset: 0x0007E7E8
		public virtual BigInteger A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0007E7F0 File Offset: 0x0007E7F0
		public virtual BigInteger[] KnowledgeProofForX2s
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX2s.Length];
				Array.Copy(this.knowledgeProofForX2s, array, this.knowledgeProofForX2s.Length);
				return array;
			}
		}

		// Token: 0x04001045 RID: 4165
		private readonly string participantId;

		// Token: 0x04001046 RID: 4166
		private readonly BigInteger a;

		// Token: 0x04001047 RID: 4167
		private readonly BigInteger[] knowledgeProofForX2s;
	}
}
