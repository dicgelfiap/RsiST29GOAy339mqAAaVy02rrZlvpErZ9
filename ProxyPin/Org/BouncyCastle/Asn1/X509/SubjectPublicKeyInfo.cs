using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020F RID: 527
	public class SubjectPublicKeyInfo : Asn1Encodable
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x00061A2C File Offset: 0x00061A2C
		public static SubjectPublicKeyInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectPublicKeyInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00061A3C File Offset: 0x00061A3C
		public static SubjectPublicKeyInfo GetInstance(object obj)
		{
			if (obj is SubjectPublicKeyInfo)
			{
				return (SubjectPublicKeyInfo)obj;
			}
			if (obj != null)
			{
				return new SubjectPublicKeyInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00061A64 File Offset: 0x00061A64
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, Asn1Encodable publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00061A80 File Offset: 0x00061A80
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, byte[] publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00061A9C File Offset: 0x00061A9C
		private SubjectPublicKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.keyData = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00061B04 File Offset: 0x00061B04
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00061B0C File Offset: 0x00061B0C
		public Asn1Object ParsePublicKey()
		{
			return Asn1Object.FromByteArray(this.keyData.GetOctets());
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00061B20 File Offset: 0x00061B20
		[Obsolete("Use 'ParsePublicKey' instead")]
		public Asn1Object GetPublicKey()
		{
			return Asn1Object.FromByteArray(this.keyData.GetOctets());
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00061B34 File Offset: 0x00061B34
		public DerBitString PublicKeyData
		{
			get
			{
				return this.keyData;
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00061B3C File Offset: 0x00061B3C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				this.keyData
			});
		}

		// Token: 0x04000C3B RID: 3131
		private readonly AlgorithmIdentifier algID;

		// Token: 0x04000C3C RID: 3132
		private readonly DerBitString keyData;
	}
}
