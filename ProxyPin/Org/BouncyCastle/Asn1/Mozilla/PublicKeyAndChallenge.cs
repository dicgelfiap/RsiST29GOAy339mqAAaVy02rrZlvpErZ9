using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Mozilla
{
	// Token: 0x02000189 RID: 393
	public class PublicKeyAndChallenge : Asn1Encodable
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x000528B0 File Offset: 0x000528B0
		public static PublicKeyAndChallenge GetInstance(object obj)
		{
			if (obj is PublicKeyAndChallenge)
			{
				return (PublicKeyAndChallenge)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PublicKeyAndChallenge((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in 'PublicKeyAndChallenge' factory : " + Platform.GetTypeName(obj) + ".");
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00052904 File Offset: 0x00052904
		public PublicKeyAndChallenge(Asn1Sequence seq)
		{
			this.pkacSeq = seq;
			this.spki = SubjectPublicKeyInfo.GetInstance(seq[0]);
			this.challenge = DerIA5String.GetInstance(seq[1]);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00052938 File Offset: 0x00052938
		public override Asn1Object ToAsn1Object()
		{
			return this.pkacSeq;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00052940 File Offset: 0x00052940
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.spki;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00052948 File Offset: 0x00052948
		public DerIA5String Challenge
		{
			get
			{
				return this.challenge;
			}
		}

		// Token: 0x04000933 RID: 2355
		private Asn1Sequence pkacSeq;

		// Token: 0x04000934 RID: 2356
		private SubjectPublicKeyInfo spki;

		// Token: 0x04000935 RID: 2357
		private DerIA5String challenge;
	}
}
