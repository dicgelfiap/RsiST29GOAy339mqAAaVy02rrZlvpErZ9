using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x0200032D RID: 813
	public class JPakeParticipant
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0007DD14 File Offset: 0x0007DD14
		public JPakeParticipant(string participantId, char[] password) : this(participantId, password, JPakePrimeOrderGroups.NIST_3072)
		{
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0007DD24 File Offset: 0x0007DD24
		public JPakeParticipant(string participantId, char[] password, JPakePrimeOrderGroup group) : this(participantId, password, group, new Sha256Digest(), new SecureRandom())
		{
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0007DD3C File Offset: 0x0007DD3C
		public JPakeParticipant(string participantId, char[] password, JPakePrimeOrderGroup group, IDigest digest, SecureRandom random)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(password, "password");
			JPakeUtilities.ValidateNotNull(group, "p");
			JPakeUtilities.ValidateNotNull(digest, "digest");
			JPakeUtilities.ValidateNotNull(random, "random");
			if (password.Length == 0)
			{
				throw new ArgumentException("Password must not be empty.");
			}
			this.participantId = participantId;
			this.password = new char[password.Length];
			Array.Copy(password, this.password, password.Length);
			this.p = group.P;
			this.q = group.Q;
			this.g = group.G;
			this.digest = digest;
			this.random = random;
			this.state = JPakeParticipant.STATE_INITIALIZED;
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0007DE04 File Offset: 0x0007DE04
		public virtual int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0007DE0C File Offset: 0x0007DE0C
		public virtual JPakeRound1Payload CreateRound1PayloadToSend()
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_1_CREATED)
			{
				throw new InvalidOperationException("Round 1 payload already created for " + this.participantId);
			}
			this.x1 = JPakeUtilities.GenerateX1(this.q, this.random);
			this.x2 = JPakeUtilities.GenerateX2(this.q, this.random);
			this.gx1 = JPakeUtilities.CalculateGx(this.p, this.g, this.x1);
			this.gx2 = JPakeUtilities.CalculateGx(this.p, this.g, this.x2);
			BigInteger[] knowledgeProofForX = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, this.g, this.gx1, this.x1, this.participantId, this.digest, this.random);
			BigInteger[] knowledgeProofForX2 = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, this.g, this.gx2, this.x2, this.participantId, this.digest, this.random);
			this.state = JPakeParticipant.STATE_ROUND_1_CREATED;
			return new JPakeRound1Payload(this.participantId, this.gx1, this.gx2, knowledgeProofForX, knowledgeProofForX2);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0007DF3C File Offset: 0x0007DF3C
		public virtual void ValidateRound1PayloadReceived(JPakeRound1Payload round1PayloadReceived)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 1 payload for " + this.participantId);
			}
			this.partnerParticipantId = round1PayloadReceived.ParticipantId;
			this.gx3 = round1PayloadReceived.Gx1;
			this.gx4 = round1PayloadReceived.Gx2;
			BigInteger[] knowledgeProofForX = round1PayloadReceived.KnowledgeProofForX1;
			BigInteger[] knowledgeProofForX2 = round1PayloadReceived.KnowledgeProofForX2;
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round1PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateGx4(this.gx4);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, this.g, this.gx3, knowledgeProofForX, round1PayloadReceived.ParticipantId, this.digest);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, this.g, this.gx4, knowledgeProofForX2, round1PayloadReceived.ParticipantId, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_1_VALIDATED;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0007E020 File Offset: 0x0007E020
		public virtual JPakeRound2Payload CreateRound2PayloadToSend()
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_2_CREATED)
			{
				throw new InvalidOperationException("Round 2 payload already created for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Round 1 payload must be validated prior to creating round 2 payload for " + this.participantId);
			}
			BigInteger gA = JPakeUtilities.CalculateGA(this.p, this.gx1, this.gx3, this.gx4);
			BigInteger s = JPakeUtilities.CalculateS(this.password);
			BigInteger bigInteger = JPakeUtilities.CalculateX2s(this.q, this.x2, s);
			BigInteger bigInteger2 = JPakeUtilities.CalculateA(this.p, this.q, gA, bigInteger);
			BigInteger[] knowledgeProofForX2s = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, gA, bigInteger2, bigInteger, this.participantId, this.digest, this.random);
			this.state = JPakeParticipant.STATE_ROUND_2_CREATED;
			return new JPakeRound2Payload(this.participantId, bigInteger2, knowledgeProofForX2s);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0007E110 File Offset: 0x0007E110
		public virtual void ValidateRound2PayloadReceived(JPakeRound2Payload round2PayloadReceived)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_2_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 2 payload for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Round 1 payload must be validated prior to validation round 2 payload for " + this.participantId);
			}
			BigInteger ga = JPakeUtilities.CalculateGA(this.p, this.gx3, this.gx1, this.gx2);
			this.b = round2PayloadReceived.A;
			BigInteger[] knowledgeProofForX2s = round2PayloadReceived.KnowledgeProofForX2s;
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round2PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateParticipantIdsEqual(this.partnerParticipantId, round2PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateGa(ga);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, ga, this.b, knowledgeProofForX2s, round2PayloadReceived.ParticipantId, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_2_VALIDATED;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0007E1F8 File Offset: 0x0007E1F8
		public virtual BigInteger CalculateKeyingMaterial()
		{
			if (this.state >= JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Key already calculated for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_2_VALIDATED)
			{
				throw new InvalidOperationException("Round 2 payload must be validated prior to creating key for " + this.participantId);
			}
			BigInteger s = JPakeUtilities.CalculateS(this.password);
			Array.Clear(this.password, 0, this.password.Length);
			this.password = null;
			BigInteger result = JPakeUtilities.CalculateKeyingMaterial(this.p, this.q, this.gx4, this.x2, s, this.b);
			this.x1 = null;
			this.x2 = null;
			this.b = null;
			this.state = JPakeParticipant.STATE_KEY_CALCULATED;
			return result;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0007E2C4 File Offset: 0x0007E2C4
		public virtual JPakeRound3Payload CreateRound3PayloadToSend(BigInteger keyingMaterial)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_3_CREATED)
			{
				throw new InvalidOperationException("Round 3 payload already created for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Keying material must be calculated prior to creating round 3 payload for " + this.participantId);
			}
			BigInteger magTag = JPakeUtilities.CalculateMacTag(this.participantId, this.partnerParticipantId, this.gx1, this.gx2, this.gx3, this.gx4, keyingMaterial, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_3_CREATED;
			return new JPakeRound3Payload(this.participantId, magTag);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0007E36C File Offset: 0x0007E36C
		public virtual void ValidateRound3PayloadReceived(JPakeRound3Payload round3PayloadReceived, BigInteger keyingMaterial)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_3_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 3 payload for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Keying material must be calculated prior to validating round 3 payload for " + this.participantId);
			}
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round3PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateParticipantIdsEqual(this.partnerParticipantId, round3PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateMacTag(this.participantId, this.partnerParticipantId, this.gx1, this.gx2, this.gx3, this.gx4, keyingMaterial, this.digest, round3PayloadReceived.MacTag);
			this.gx1 = null;
			this.gx2 = null;
			this.gx3 = null;
			this.gx4 = null;
			this.state = JPakeParticipant.STATE_ROUND_3_VALIDATED;
		}

		// Token: 0x04001022 RID: 4130
		public static readonly int STATE_INITIALIZED = 0;

		// Token: 0x04001023 RID: 4131
		public static readonly int STATE_ROUND_1_CREATED = 10;

		// Token: 0x04001024 RID: 4132
		public static readonly int STATE_ROUND_1_VALIDATED = 20;

		// Token: 0x04001025 RID: 4133
		public static readonly int STATE_ROUND_2_CREATED = 30;

		// Token: 0x04001026 RID: 4134
		public static readonly int STATE_ROUND_2_VALIDATED = 40;

		// Token: 0x04001027 RID: 4135
		public static readonly int STATE_KEY_CALCULATED = 50;

		// Token: 0x04001028 RID: 4136
		public static readonly int STATE_ROUND_3_CREATED = 60;

		// Token: 0x04001029 RID: 4137
		public static readonly int STATE_ROUND_3_VALIDATED = 70;

		// Token: 0x0400102A RID: 4138
		private string participantId;

		// Token: 0x0400102B RID: 4139
		private char[] password;

		// Token: 0x0400102C RID: 4140
		private IDigest digest;

		// Token: 0x0400102D RID: 4141
		private readonly SecureRandom random;

		// Token: 0x0400102E RID: 4142
		private readonly BigInteger p;

		// Token: 0x0400102F RID: 4143
		private readonly BigInteger q;

		// Token: 0x04001030 RID: 4144
		private readonly BigInteger g;

		// Token: 0x04001031 RID: 4145
		private string partnerParticipantId;

		// Token: 0x04001032 RID: 4146
		private BigInteger x1;

		// Token: 0x04001033 RID: 4147
		private BigInteger x2;

		// Token: 0x04001034 RID: 4148
		private BigInteger gx1;

		// Token: 0x04001035 RID: 4149
		private BigInteger gx2;

		// Token: 0x04001036 RID: 4150
		private BigInteger gx3;

		// Token: 0x04001037 RID: 4151
		private BigInteger gx4;

		// Token: 0x04001038 RID: 4152
		private BigInteger b;

		// Token: 0x04001039 RID: 4153
		private int state;
	}
}
