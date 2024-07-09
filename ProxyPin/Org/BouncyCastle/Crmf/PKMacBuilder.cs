using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Iana;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200032A RID: 810
	public class PKMacBuilder
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0007D868 File Offset: 0x0007D868
		public PKMacBuilder() : this(new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1), 1000, new AlgorithmIdentifier(IanaObjectIdentifiers.HmacSha1, DerNull.Instance), new DefaultPKMacPrimitivesProvider())
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0007D894 File Offset: 0x0007D894
		public PKMacBuilder(IPKMacPrimitivesProvider provider) : this(new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1), 1000, new AlgorithmIdentifier(IanaObjectIdentifiers.HmacSha1, DerNull.Instance), provider)
		{
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0007D8BC File Offset: 0x0007D8BC
		public PKMacBuilder(IPKMacPrimitivesProvider provider, AlgorithmIdentifier digestAlgorithmIdentifier, AlgorithmIdentifier macAlgorithmIdentifier) : this(digestAlgorithmIdentifier, 1000, macAlgorithmIdentifier, provider)
		{
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0007D8CC File Offset: 0x0007D8CC
		public PKMacBuilder(IPKMacPrimitivesProvider provider, int maxIterations)
		{
			this.saltLength = 20;
			base..ctor();
			this.provider = provider;
			this.maxIterations = maxIterations;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0007D8EC File Offset: 0x0007D8EC
		private PKMacBuilder(AlgorithmIdentifier digestAlgorithmIdentifier, int iterationCount, AlgorithmIdentifier macAlgorithmIdentifier, IPKMacPrimitivesProvider provider)
		{
			this.saltLength = 20;
			base..ctor();
			this.iterationCount = iterationCount;
			this.mac = macAlgorithmIdentifier;
			this.owf = digestAlgorithmIdentifier;
			this.provider = provider;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0007D91C File Offset: 0x0007D91C
		public PKMacBuilder SetSaltLength(int saltLength)
		{
			if (saltLength < 8)
			{
				throw new ArgumentException("salt length must be at least 8 bytes");
			}
			this.saltLength = saltLength;
			return this;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0007D938 File Offset: 0x0007D938
		public PKMacBuilder SetIterationCount(int iterationCount)
		{
			if (iterationCount < 100)
			{
				throw new ArgumentException("iteration count must be at least 100");
			}
			this.CheckIterationCountCeiling(iterationCount);
			this.iterationCount = iterationCount;
			return this;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0007D95C File Offset: 0x0007D95C
		public PKMacBuilder SetParameters(PbmParameter parameters)
		{
			this.CheckIterationCountCeiling(parameters.IterationCount.IntValueExact);
			this.parameters = parameters;
			return this;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0007D978 File Offset: 0x0007D978
		public PKMacBuilder SetSecureRandom(SecureRandom random)
		{
			this.random = random;
			return this;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0007D984 File Offset: 0x0007D984
		public IMacFactory Build(char[] password)
		{
			if (this.parameters != null)
			{
				return this.GenCalculator(this.parameters, password);
			}
			byte[] array = new byte[this.saltLength];
			if (this.random == null)
			{
				this.random = new SecureRandom();
			}
			this.random.NextBytes(array);
			return this.GenCalculator(new PbmParameter(array, this.owf, this.iterationCount, this.mac), password);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0007D9FC File Offset: 0x0007D9FC
		private void CheckIterationCountCeiling(int iterationCount)
		{
			if (this.maxIterations > 0 && iterationCount > this.maxIterations)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"iteration count exceeds limit (",
					iterationCount,
					" > ",
					this.maxIterations,
					")"
				}));
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0007DA68 File Offset: 0x0007DA68
		private IMacFactory GenCalculator(PbmParameter parameters, char[] password)
		{
			byte[] array = Strings.ToUtf8ByteArray(password);
			byte[] octets = parameters.Salt.GetOctets();
			byte[] array2 = new byte[array.Length + octets.Length];
			Array.Copy(array, 0, array2, 0, array.Length);
			Array.Copy(octets, 0, array2, array.Length, octets.Length);
			IDigest digest = this.provider.CreateDigest(parameters.Owf);
			int num = parameters.IterationCount.IntValueExact;
			digest.BlockUpdate(array2, 0, array2.Length);
			array2 = new byte[digest.GetDigestSize()];
			digest.DoFinal(array2, 0);
			while (--num > 0)
			{
				digest.BlockUpdate(array2, 0, array2.Length);
				digest.DoFinal(array2, 0);
			}
			byte[] key = array2;
			return new PKMacFactory(key, parameters);
		}

		// Token: 0x04001014 RID: 4116
		private AlgorithmIdentifier owf;

		// Token: 0x04001015 RID: 4117
		private AlgorithmIdentifier mac;

		// Token: 0x04001016 RID: 4118
		private IPKMacPrimitivesProvider provider;

		// Token: 0x04001017 RID: 4119
		private SecureRandom random;

		// Token: 0x04001018 RID: 4120
		private PbmParameter parameters;

		// Token: 0x04001019 RID: 4121
		private int iterationCount;

		// Token: 0x0400101A RID: 4122
		private int saltLength;

		// Token: 0x0400101B RID: 4123
		private int maxIterations;
	}
}
