using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DF RID: 223
	public class PbmParameter : Asn1Encodable
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0004202C File Offset: 0x0004202C
		private PbmParameter(Asn1Sequence seq)
		{
			this.salt = Asn1OctetString.GetInstance(seq[0]);
			this.owf = AlgorithmIdentifier.GetInstance(seq[1]);
			this.iterationCount = DerInteger.GetInstance(seq[2]);
			this.mac = AlgorithmIdentifier.GetInstance(seq[3]);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0004208C File Offset: 0x0004208C
		public static PbmParameter GetInstance(object obj)
		{
			if (obj is PbmParameter)
			{
				return (PbmParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbmParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000420E0 File Offset: 0x000420E0
		public PbmParameter(byte[] salt, AlgorithmIdentifier owf, int iterationCount, AlgorithmIdentifier mac) : this(new DerOctetString(salt), owf, new DerInteger(iterationCount), mac)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000420F8 File Offset: 0x000420F8
		public PbmParameter(Asn1OctetString salt, AlgorithmIdentifier owf, DerInteger iterationCount, AlgorithmIdentifier mac)
		{
			this.salt = salt;
			this.owf = owf;
			this.iterationCount = iterationCount;
			this.mac = mac;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00042120 File Offset: 0x00042120
		public virtual Asn1OctetString Salt
		{
			get
			{
				return this.salt;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00042128 File Offset: 0x00042128
		public virtual AlgorithmIdentifier Owf
		{
			get
			{
				return this.owf;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00042130 File Offset: 0x00042130
		public virtual DerInteger IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00042138 File Offset: 0x00042138
		public virtual AlgorithmIdentifier Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00042140 File Offset: 0x00042140
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.salt,
				this.owf,
				this.iterationCount,
				this.mac
			});
		}

		// Token: 0x04000618 RID: 1560
		private Asn1OctetString salt;

		// Token: 0x04000619 RID: 1561
		private AlgorithmIdentifier owf;

		// Token: 0x0400061A RID: 1562
		private DerInteger iterationCount;

		// Token: 0x0400061B RID: 1563
		private AlgorithmIdentifier mac;
	}
}
