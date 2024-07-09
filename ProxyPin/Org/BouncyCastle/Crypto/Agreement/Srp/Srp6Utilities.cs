using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200033D RID: 829
	public class Srp6Utilities
	{
		// Token: 0x060018CC RID: 6348 RVA: 0x0007F8AC File Offset: 0x0007F8AC
		public static BigInteger CalculateK(IDigest digest, BigInteger N, BigInteger g)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, N, g);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0007F8B8 File Offset: 0x0007F8B8
		public static BigInteger CalculateU(IDigest digest, BigInteger N, BigInteger A, BigInteger B)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, A, B);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0007F8C4 File Offset: 0x0007F8C4
		public static BigInteger CalculateX(IDigest digest, BigInteger N, byte[] salt, byte[] identity, byte[] password)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(identity, 0, identity.Length);
			digest.Update(58);
			digest.BlockUpdate(password, 0, password.Length);
			digest.DoFinal(array, 0);
			digest.BlockUpdate(salt, 0, salt.Length);
			digest.BlockUpdate(array, 0, array.Length);
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0007F930 File Offset: 0x0007F930
		public static BigInteger GeneratePrivateValue(IDigest digest, BigInteger N, BigInteger g, SecureRandom random)
		{
			int num = Math.Min(256, N.BitLength / 2);
			BigInteger min = BigInteger.One.ShiftLeft(num - 1);
			BigInteger max = N.Subtract(BigInteger.One);
			return BigIntegers.CreateRandomInRange(min, max, random);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0007F978 File Offset: 0x0007F978
		public static BigInteger ValidatePublicValue(BigInteger N, BigInteger val)
		{
			val = val.Mod(N);
			if (val.Equals(BigInteger.Zero))
			{
				throw new CryptoException("Invalid public value: 0");
			}
			return val;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0007F9A0 File Offset: 0x0007F9A0
		public static BigInteger CalculateM1(IDigest digest, BigInteger N, BigInteger A, BigInteger B, BigInteger S)
		{
			return Srp6Utilities.HashPaddedTriplet(digest, N, A, B, S);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0007F9C0 File Offset: 0x0007F9C0
		public static BigInteger CalculateM2(IDigest digest, BigInteger N, BigInteger A, BigInteger M1, BigInteger S)
		{
			return Srp6Utilities.HashPaddedTriplet(digest, N, A, M1, S);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0007F9E0 File Offset: 0x0007F9E0
		public static BigInteger CalculateKey(IDigest digest, BigInteger N, BigInteger S)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(S, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0007FA2C File Offset: 0x0007FA2C
		private static BigInteger HashPaddedTriplet(IDigest digest, BigInteger N, BigInteger n1, BigInteger n2, BigInteger n3)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(n1, length);
			byte[] padded2 = Srp6Utilities.GetPadded(n2, length);
			byte[] padded3 = Srp6Utilities.GetPadded(n3, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			digest.BlockUpdate(padded2, 0, padded2.Length);
			digest.BlockUpdate(padded3, 0, padded3.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0007FAA4 File Offset: 0x0007FAA4
		private static BigInteger HashPaddedPair(IDigest digest, BigInteger N, BigInteger n1, BigInteger n2)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(n1, length);
			byte[] padded2 = Srp6Utilities.GetPadded(n2, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			digest.BlockUpdate(padded2, 0, padded2.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0007FB04 File Offset: 0x0007FB04
		private static byte[] GetPadded(BigInteger n, int length)
		{
			byte[] array = BigIntegers.AsUnsignedByteArray(n);
			if (array.Length < length)
			{
				byte[] array2 = new byte[length];
				Array.Copy(array, 0, array2, length - array.Length, array.Length);
				array = array2;
			}
			return array;
		}
	}
}
