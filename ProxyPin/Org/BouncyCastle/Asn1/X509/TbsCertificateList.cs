using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000215 RID: 533
	public class TbsCertificateList : Asn1Encodable
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x000621D8 File Offset: 0x000621D8
		public static TbsCertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000621E8 File Offset: 0x000621E8
		public static TbsCertificateList GetInstance(object obj)
		{
			TbsCertificateList tbsCertificateList = obj as TbsCertificateList;
			if (obj == null || tbsCertificateList != null)
			{
				return tbsCertificateList;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsCertificateList((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00062240 File Offset: 0x00062240
		internal TbsCertificateList(Asn1Sequence seq)
		{
			if (seq.Count < 3 || seq.Count > 7)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			this.seq = seq;
			if (seq[num] is DerInteger)
			{
				this.version = DerInteger.GetInstance(seq[num++]);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.signature = AlgorithmIdentifier.GetInstance(seq[num++]);
			this.issuer = X509Name.GetInstance(seq[num++]);
			this.thisUpdate = Time.GetInstance(seq[num++]);
			if (num < seq.Count && (seq[num] is DerUtcTime || seq[num] is DerGeneralizedTime || seq[num] is Time))
			{
				this.nextUpdate = Time.GetInstance(seq[num++]);
			}
			if (num < seq.Count && !(seq[num] is Asn1TaggedObject))
			{
				this.revokedCertificates = Asn1Sequence.GetInstance(seq[num++]);
			}
			if (num < seq.Count && seq[num] is Asn1TaggedObject)
			{
				this.crlExtensions = X509Extensions.GetInstance(seq[num]);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x000623C4 File Offset: 0x000623C4
		public int Version
		{
			get
			{
				return this.version.IntValueExact + 1;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000623D4 File Offset: 0x000623D4
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x000623DC File Offset: 0x000623DC
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x000623E4 File Offset: 0x000623E4
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x000623EC File Offset: 0x000623EC
		public Time ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x000623F4 File Offset: 0x000623F4
		public Time NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000623FC File Offset: 0x000623FC
		public CrlEntry[] GetRevokedCertificates()
		{
			if (this.revokedCertificates == null)
			{
				return new CrlEntry[0];
			}
			CrlEntry[] array = new CrlEntry[this.revokedCertificates.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new CrlEntry(Asn1Sequence.GetInstance(this.revokedCertificates[i]));
			}
			return array;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00062460 File Offset: 0x00062460
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			if (this.revokedCertificates == null)
			{
				return EmptyEnumerable.Instance;
			}
			return new TbsCertificateList.RevokedCertificatesEnumeration(this.revokedCertificates);
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00062480 File Offset: 0x00062480
		public X509Extensions Extensions
		{
			get
			{
				return this.crlExtensions;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00062488 File Offset: 0x00062488
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000C51 RID: 3153
		internal Asn1Sequence seq;

		// Token: 0x04000C52 RID: 3154
		internal DerInteger version;

		// Token: 0x04000C53 RID: 3155
		internal AlgorithmIdentifier signature;

		// Token: 0x04000C54 RID: 3156
		internal X509Name issuer;

		// Token: 0x04000C55 RID: 3157
		internal Time thisUpdate;

		// Token: 0x04000C56 RID: 3158
		internal Time nextUpdate;

		// Token: 0x04000C57 RID: 3159
		internal Asn1Sequence revokedCertificates;

		// Token: 0x04000C58 RID: 3160
		internal X509Extensions crlExtensions;

		// Token: 0x02000DBB RID: 3515
		private class RevokedCertificatesEnumeration : IEnumerable
		{
			// Token: 0x06008B11 RID: 35601 RVA: 0x0029C8C8 File Offset: 0x0029C8C8
			internal RevokedCertificatesEnumeration(IEnumerable en)
			{
				this.en = en;
			}

			// Token: 0x06008B12 RID: 35602 RVA: 0x0029C8D8 File Offset: 0x0029C8D8
			public IEnumerator GetEnumerator()
			{
				return new TbsCertificateList.RevokedCertificatesEnumeration.RevokedCertificatesEnumerator(this.en.GetEnumerator());
			}

			// Token: 0x04004044 RID: 16452
			private readonly IEnumerable en;

			// Token: 0x0200120A RID: 4618
			private class RevokedCertificatesEnumerator : IEnumerator
			{
				// Token: 0x06009693 RID: 38547 RVA: 0x002CC67C File Offset: 0x002CC67C
				internal RevokedCertificatesEnumerator(IEnumerator e)
				{
					this.e = e;
				}

				// Token: 0x06009694 RID: 38548 RVA: 0x002CC68C File Offset: 0x002CC68C
				public bool MoveNext()
				{
					return this.e.MoveNext();
				}

				// Token: 0x06009695 RID: 38549 RVA: 0x002CC69C File Offset: 0x002CC69C
				public void Reset()
				{
					this.e.Reset();
				}

				// Token: 0x17001F51 RID: 8017
				// (get) Token: 0x06009696 RID: 38550 RVA: 0x002CC6AC File Offset: 0x002CC6AC
				public object Current
				{
					get
					{
						return new CrlEntry(Asn1Sequence.GetInstance(this.e.Current));
					}
				}

				// Token: 0x04004EFE RID: 20222
				private readonly IEnumerator e;
			}
		}
	}
}
