using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x02000330 RID: 816
	public class JPakeRound1Payload
	{
		// Token: 0x06001873 RID: 6259 RVA: 0x0007E654 File Offset: 0x0007E654
		public JPakeRound1Payload(string participantId, BigInteger gx1, BigInteger gx2, BigInteger[] knowledgeProofForX1, BigInteger[] knowledgeProofForX2)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(gx1, "gx1");
			JPakeUtilities.ValidateNotNull(gx2, "gx2");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX1, "knowledgeProofForX1");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX2, "knowledgeProofForX2");
			this.participantId = participantId;
			this.gx1 = gx1;
			this.gx2 = gx2;
			this.knowledgeProofForX1 = new BigInteger[knowledgeProofForX1.Length];
			Array.Copy(knowledgeProofForX1, this.knowledgeProofForX1, knowledgeProofForX1.Length);
			this.knowledgeProofForX2 = new BigInteger[knowledgeProofForX2.Length];
			Array.Copy(knowledgeProofForX2, this.knowledgeProofForX2, knowledgeProofForX2.Length);
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0007E6FC File Offset: 0x0007E6FC
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0007E704 File Offset: 0x0007E704
		public virtual BigInteger Gx1
		{
			get
			{
				return this.gx1;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0007E70C File Offset: 0x0007E70C
		public virtual BigInteger Gx2
		{
			get
			{
				return this.gx2;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0007E714 File Offset: 0x0007E714
		public virtual BigInteger[] KnowledgeProofForX1
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX1.Length];
				Array.Copy(this.knowledgeProofForX1, array, this.knowledgeProofForX1.Length);
				return array;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0007E748 File Offset: 0x0007E748
		public virtual BigInteger[] KnowledgeProofForX2
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX2.Length];
				Array.Copy(this.knowledgeProofForX2, array, this.knowledgeProofForX2.Length);
				return array;
			}
		}

		// Token: 0x04001040 RID: 4160
		private readonly string participantId;

		// Token: 0x04001041 RID: 4161
		private readonly BigInteger gx1;

		// Token: 0x04001042 RID: 4162
		private readonly BigInteger gx2;

		// Token: 0x04001043 RID: 4163
		private readonly BigInteger[] knowledgeProofForX1;

		// Token: 0x04001044 RID: 4164
		private readonly BigInteger[] knowledgeProofForX2;
	}
}
