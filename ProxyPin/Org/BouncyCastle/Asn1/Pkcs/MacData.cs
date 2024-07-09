using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AE RID: 430
	public class MacData : Asn1Encodable
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x00055BA4 File Offset: 0x00055BA4
		public static MacData GetInstance(object obj)
		{
			if (obj is MacData)
			{
				return (MacData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MacData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00055BF8 File Offset: 0x00055BF8
		private MacData(Asn1Sequence seq)
		{
			this.digInfo = DigestInfo.GetInstance(seq[0]);
			this.salt = ((Asn1OctetString)seq[1]).GetOctets();
			if (seq.Count == 3)
			{
				this.iterationCount = ((DerInteger)seq[2]).Value;
				return;
			}
			this.iterationCount = BigInteger.One;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00055C68 File Offset: 0x00055C68
		public MacData(DigestInfo digInfo, byte[] salt, int iterationCount)
		{
			this.digInfo = digInfo;
			this.salt = (byte[])salt.Clone();
			this.iterationCount = BigInteger.ValueOf((long)iterationCount);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00055C98 File Offset: 0x00055C98
		public DigestInfo Mac
		{
			get
			{
				return this.digInfo;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00055CA0 File Offset: 0x00055CA0
		public byte[] GetSalt()
		{
			return (byte[])this.salt.Clone();
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00055CB4 File Offset: 0x00055CB4
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00055CBC File Offset: 0x00055CBC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digInfo,
				new DerOctetString(this.salt)
			});
			if (!this.iterationCount.Equals(BigInteger.One))
			{
				asn1EncodableVector.Add(new DerInteger(this.iterationCount));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009E3 RID: 2531
		internal DigestInfo digInfo;

		// Token: 0x040009E4 RID: 2532
		internal byte[] salt;

		// Token: 0x040009E5 RID: 2533
		internal BigInteger iterationCount;
	}
}
