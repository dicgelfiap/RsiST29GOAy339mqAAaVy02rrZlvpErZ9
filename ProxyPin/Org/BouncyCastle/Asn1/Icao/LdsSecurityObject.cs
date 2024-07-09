using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x02000172 RID: 370
	public class LdsSecurityObject : Asn1Encodable
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x00050438 File Offset: 0x00050438
		public static LdsSecurityObject GetInstance(object obj)
		{
			if (obj is LdsSecurityObject)
			{
				return (LdsSecurityObject)obj;
			}
			if (obj != null)
			{
				return new LdsSecurityObject(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00050460 File Offset: 0x00050460
		private LdsSecurityObject(Asn1Sequence seq)
		{
			if (seq == null || seq.Count == 0)
			{
				throw new ArgumentException("null or empty sequence passed.");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digestAlgorithmIdentifier = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			Asn1Sequence instance = Asn1Sequence.GetInstance(enumerator.Current);
			if (this.version.Value.Equals(BigInteger.One))
			{
				enumerator.MoveNext();
				this.versionInfo = LdsVersionInfo.GetInstance(enumerator.Current);
			}
			this.CheckDatagroupHashSeqSize(instance.Count);
			this.datagroupHash = new DataGroupHash[instance.Count];
			for (int i = 0; i < instance.Count; i++)
			{
				this.datagroupHash[i] = DataGroupHash.GetInstance(instance[i]);
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00050564 File Offset: 0x00050564
		public LdsSecurityObject(AlgorithmIdentifier digestAlgorithmIdentifier, DataGroupHash[] datagroupHash)
		{
			this.version = new DerInteger(0);
			this.digestAlgorithmIdentifier = digestAlgorithmIdentifier;
			this.datagroupHash = datagroupHash;
			this.CheckDatagroupHashSeqSize(datagroupHash.Length);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0005059C File Offset: 0x0005059C
		public LdsSecurityObject(AlgorithmIdentifier digestAlgorithmIdentifier, DataGroupHash[] datagroupHash, LdsVersionInfo versionInfo)
		{
			this.version = new DerInteger(1);
			this.digestAlgorithmIdentifier = digestAlgorithmIdentifier;
			this.datagroupHash = datagroupHash;
			this.versionInfo = versionInfo;
			this.CheckDatagroupHashSeqSize(datagroupHash.Length);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000505DC File Offset: 0x000505DC
		private void CheckDatagroupHashSeqSize(int size)
		{
			if (size < 2 || size > 16)
			{
				throw new ArgumentException("wrong size in DataGroupHashValues : not in (2.." + 16 + ")");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0005060C File Offset: 0x0005060C
		public BigInteger Version
		{
			get
			{
				return this.version.Value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0005061C File Offset: 0x0005061C
		public AlgorithmIdentifier DigestAlgorithmIdentifier
		{
			get
			{
				return this.digestAlgorithmIdentifier;
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00050624 File Offset: 0x00050624
		public DataGroupHash[] GetDatagroupHash()
		{
			return this.datagroupHash;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0005062C File Offset: 0x0005062C
		public LdsVersionInfo VersionInfo
		{
			get
			{
				return this.versionInfo;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00050634 File Offset: 0x00050634
		public override Asn1Object ToAsn1Object()
		{
			DerSequence derSequence = new DerSequence(this.datagroupHash);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithmIdentifier,
				derSequence
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.versionInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040008AD RID: 2221
		public const int UBDataGroups = 16;

		// Token: 0x040008AE RID: 2222
		private DerInteger version = new DerInteger(0);

		// Token: 0x040008AF RID: 2223
		private AlgorithmIdentifier digestAlgorithmIdentifier;

		// Token: 0x040008B0 RID: 2224
		private DataGroupHash[] datagroupHash;

		// Token: 0x040008B1 RID: 2225
		private LdsVersionInfo versionInfo;
	}
}
