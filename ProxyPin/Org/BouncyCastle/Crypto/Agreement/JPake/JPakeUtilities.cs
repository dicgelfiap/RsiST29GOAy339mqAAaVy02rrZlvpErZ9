using System;
using System.Text;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x02000333 RID: 819
	public abstract class JPakeUtilities
	{
		// Token: 0x06001880 RID: 6272 RVA: 0x0007E84C File Offset: 0x0007E84C
		public static BigInteger GenerateX1(BigInteger q, SecureRandom random)
		{
			BigInteger zero = JPakeUtilities.Zero;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			return BigIntegers.CreateRandomInRange(zero, max, random);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007E878 File Offset: 0x0007E878
		public static BigInteger GenerateX2(BigInteger q, SecureRandom random)
		{
			BigInteger one = JPakeUtilities.One;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			return BigIntegers.CreateRandomInRange(one, max, random);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0007E8A4 File Offset: 0x0007E8A4
		public static BigInteger CalculateS(char[] password)
		{
			return new BigInteger(Encoding.UTF8.GetBytes(password));
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0007E8B8 File Offset: 0x0007E8B8
		public static BigInteger CalculateGx(BigInteger p, BigInteger g, BigInteger x)
		{
			return g.ModPow(x, p);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0007E8C4 File Offset: 0x0007E8C4
		public static BigInteger CalculateGA(BigInteger p, BigInteger gx1, BigInteger gx3, BigInteger gx4)
		{
			return gx1.Multiply(gx3).Multiply(gx4).Mod(p);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0007E8DC File Offset: 0x0007E8DC
		public static BigInteger CalculateX2s(BigInteger q, BigInteger x2, BigInteger s)
		{
			return x2.Multiply(s).Mod(q);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0007E8EC File Offset: 0x0007E8EC
		public static BigInteger CalculateA(BigInteger p, BigInteger q, BigInteger gA, BigInteger x2s)
		{
			return gA.ModPow(x2s, p);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0007E8F8 File Offset: 0x0007E8F8
		public static BigInteger[] CalculateZeroKnowledgeProof(BigInteger p, BigInteger q, BigInteger g, BigInteger gx, BigInteger x, string participantId, IDigest digest, SecureRandom random)
		{
			BigInteger zero = JPakeUtilities.Zero;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			BigInteger bigInteger = BigIntegers.CreateRandomInRange(zero, max, random);
			BigInteger bigInteger2 = g.ModPow(bigInteger, p);
			BigInteger val = JPakeUtilities.CalculateHashForZeroKnowledgeProof(g, bigInteger2, gx, participantId, digest);
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger.Subtract(x.Multiply(val)).Mod(q)
			};
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0007E96C File Offset: 0x0007E96C
		private static BigInteger CalculateHashForZeroKnowledgeProof(BigInteger g, BigInteger gr, BigInteger gx, string participantId, IDigest digest)
		{
			digest.Reset();
			JPakeUtilities.UpdateDigestIncludingSize(digest, g);
			JPakeUtilities.UpdateDigestIncludingSize(digest, gr);
			JPakeUtilities.UpdateDigestIncludingSize(digest, gx);
			JPakeUtilities.UpdateDigestIncludingSize(digest, participantId);
			byte[] bytes = DigestUtilities.DoFinal(digest);
			return new BigInteger(bytes);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007E9B4 File Offset: 0x0007E9B4
		public static void ValidateGx4(BigInteger gx4)
		{
			if (gx4.Equals(JPakeUtilities.One))
			{
				throw new CryptoException("g^x validation failed.  g^x should not be 1.");
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007E9D4 File Offset: 0x0007E9D4
		public static void ValidateGa(BigInteger ga)
		{
			if (ga.Equals(JPakeUtilities.One))
			{
				throw new CryptoException("ga is equal to 1.  It should not be.  The chances of this happening are on the order of 2^160 for a 160-bit q.  Try again.");
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007E9F4 File Offset: 0x0007E9F4
		public static void ValidateZeroKnowledgeProof(BigInteger p, BigInteger q, BigInteger g, BigInteger gx, BigInteger[] zeroKnowledgeProof, string participantId, IDigest digest)
		{
			BigInteger bigInteger = zeroKnowledgeProof[0];
			BigInteger e = zeroKnowledgeProof[1];
			BigInteger e2 = JPakeUtilities.CalculateHashForZeroKnowledgeProof(g, bigInteger, gx, participantId, digest);
			if (gx.CompareTo(JPakeUtilities.Zero) != 1 || gx.CompareTo(p) != -1 || gx.ModPow(q, p).CompareTo(JPakeUtilities.One) != 0 || g.ModPow(e, p).Multiply(gx.ModPow(e2, p)).Mod(p).CompareTo(bigInteger) != 0)
			{
				throw new CryptoException("Zero-knowledge proof validation failed");
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0007EA8C File Offset: 0x0007EA8C
		public static BigInteger CalculateKeyingMaterial(BigInteger p, BigInteger q, BigInteger gx4, BigInteger x2, BigInteger s, BigInteger B)
		{
			return gx4.ModPow(x2.Multiply(s).Negate().Mod(q), p).Multiply(B).ModPow(x2, p);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0007EAC8 File Offset: 0x0007EAC8
		public static void ValidateParticipantIdsDiffer(string participantId1, string participantId2)
		{
			if (participantId1.Equals(participantId2))
			{
				throw new CryptoException("Both participants are using the same participantId (" + participantId1 + "). This is not allowed. Each participant must use a unique participantId.");
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0007EAEC File Offset: 0x0007EAEC
		public static void ValidateParticipantIdsEqual(string expectedParticipantId, string actualParticipantId)
		{
			if (!expectedParticipantId.Equals(actualParticipantId))
			{
				throw new CryptoException(string.Concat(new string[]
				{
					"Received payload from incorrect partner (",
					actualParticipantId,
					"). Expected to receive payload from ",
					expectedParticipantId,
					"."
				}));
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0007EB50 File Offset: 0x0007EB50
		public static void ValidateNotNull(object obj, string description)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(description);
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0007EB60 File Offset: 0x0007EB60
		public static BigInteger CalculateMacTag(string participantId, string partnerParticipantId, BigInteger gx1, BigInteger gx2, BigInteger gx3, BigInteger gx4, BigInteger keyingMaterial, IDigest digest)
		{
			byte[] array = JPakeUtilities.CalculateMacKey(keyingMaterial, digest);
			HMac hmac = new HMac(digest);
			hmac.Init(new KeyParameter(array));
			Arrays.Fill(array, 0);
			JPakeUtilities.UpdateMac(hmac, "KC_1_U");
			JPakeUtilities.UpdateMac(hmac, participantId);
			JPakeUtilities.UpdateMac(hmac, partnerParticipantId);
			JPakeUtilities.UpdateMac(hmac, gx1);
			JPakeUtilities.UpdateMac(hmac, gx2);
			JPakeUtilities.UpdateMac(hmac, gx3);
			JPakeUtilities.UpdateMac(hmac, gx4);
			byte[] bytes = MacUtilities.DoFinal(hmac);
			return new BigInteger(bytes);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0007EBDC File Offset: 0x0007EBDC
		private static byte[] CalculateMacKey(BigInteger keyingMaterial, IDigest digest)
		{
			digest.Reset();
			JPakeUtilities.UpdateDigest(digest, keyingMaterial);
			JPakeUtilities.UpdateDigest(digest, "JPAKE_KC");
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0007EBFC File Offset: 0x0007EBFC
		public static void ValidateMacTag(string participantId, string partnerParticipantId, BigInteger gx1, BigInteger gx2, BigInteger gx3, BigInteger gx4, BigInteger keyingMaterial, IDigest digest, BigInteger partnerMacTag)
		{
			BigInteger bigInteger = JPakeUtilities.CalculateMacTag(partnerParticipantId, participantId, gx3, gx4, gx1, gx2, keyingMaterial, digest);
			if (!bigInteger.Equals(partnerMacTag))
			{
				throw new CryptoException("Partner MacTag validation failed. Therefore, the password, MAC, or digest algorithm of each participant does not match.");
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0007EC38 File Offset: 0x0007EC38
		private static void UpdateDigest(IDigest digest, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateDigest(digest, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0007EC48 File Offset: 0x0007EC48
		private static void UpdateDigest(IDigest digest, string str)
		{
			JPakeUtilities.UpdateDigest(digest, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0007EC5C File Offset: 0x0007EC5C
		private static void UpdateDigest(IDigest digest, byte[] bytes)
		{
			digest.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0007EC70 File Offset: 0x0007EC70
		private static void UpdateDigestIncludingSize(IDigest digest, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateDigestIncludingSize(digest, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0007EC80 File Offset: 0x0007EC80
		private static void UpdateDigestIncludingSize(IDigest digest, string str)
		{
			JPakeUtilities.UpdateDigestIncludingSize(digest, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0007EC94 File Offset: 0x0007EC94
		private static void UpdateDigestIncludingSize(IDigest digest, byte[] bytes)
		{
			digest.BlockUpdate(JPakeUtilities.IntToByteArray(bytes.Length), 0, 4);
			digest.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0007ECB8 File Offset: 0x0007ECB8
		private static void UpdateMac(IMac mac, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateMac(mac, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0007ECC8 File Offset: 0x0007ECC8
		private static void UpdateMac(IMac mac, string str)
		{
			JPakeUtilities.UpdateMac(mac, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0007ECDC File Offset: 0x0007ECDC
		private static void UpdateMac(IMac mac, byte[] bytes)
		{
			mac.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0007ECF0 File Offset: 0x0007ECF0
		private static byte[] IntToByteArray(int value)
		{
			return Pack.UInt32_To_BE((uint)value);
		}

		// Token: 0x0400104A RID: 4170
		public static readonly BigInteger Zero = BigInteger.Zero;

		// Token: 0x0400104B RID: 4171
		public static readonly BigInteger One = BigInteger.One;
	}
}
