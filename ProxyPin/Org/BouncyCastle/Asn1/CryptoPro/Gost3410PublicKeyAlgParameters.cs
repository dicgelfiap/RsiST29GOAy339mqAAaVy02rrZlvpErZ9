using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000146 RID: 326
	public class Gost3410PublicKeyAlgParameters : Asn1Encodable
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x0004BA84 File Offset: 0x0004BA84
		public static Gost3410PublicKeyAlgParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410PublicKeyAlgParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0004BA94 File Offset: 0x0004BA94
		public static Gost3410PublicKeyAlgParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410PublicKeyAlgParameters)
			{
				return (Gost3410PublicKeyAlgParameters)obj;
			}
			return new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0004BABC File Offset: 0x0004BABC
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet) : this(publicKeyParamSet, digestParamSet, null)
		{
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0004BAC8 File Offset: 0x0004BAC8
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			if (digestParamSet == null)
			{
				throw new ArgumentNullException("digestParamSet");
			}
			this.publicKeyParamSet = publicKeyParamSet;
			this.digestParamSet = digestParamSet;
			this.encryptionParamSet = encryptionParamSet;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0004BB08 File Offset: 0x0004BB08
		[Obsolete("Use 'GetInstance' instead")]
		public Gost3410PublicKeyAlgParameters(Asn1Sequence seq)
		{
			this.publicKeyParamSet = (DerObjectIdentifier)seq[0];
			this.digestParamSet = (DerObjectIdentifier)seq[1];
			if (seq.Count > 2)
			{
				this.encryptionParamSet = (DerObjectIdentifier)seq[2];
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0004BB64 File Offset: 0x0004BB64
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0004BB6C File Offset: 0x0004BB6C
		public DerObjectIdentifier DigestParamSet
		{
			get
			{
				return this.digestParamSet;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0004BB74 File Offset: 0x0004BB74
		public DerObjectIdentifier EncryptionParamSet
		{
			get
			{
				return this.encryptionParamSet;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0004BB7C File Offset: 0x0004BB7C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.publicKeyParamSet,
				this.digestParamSet
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.encryptionParamSet
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007CC RID: 1996
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x040007CD RID: 1997
		private DerObjectIdentifier digestParamSet;

		// Token: 0x040007CE RID: 1998
		private DerObjectIdentifier encryptionParamSet;
	}
}
