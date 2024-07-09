using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000213 RID: 531
	public class TbsCertificateStructure : Asn1Encodable
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x00061E30 File Offset: 0x00061E30
		public static TbsCertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00061E40 File Offset: 0x00061E40
		public static TbsCertificateStructure GetInstance(object obj)
		{
			if (obj is TbsCertificateStructure)
			{
				return (TbsCertificateStructure)obj;
			}
			if (obj != null)
			{
				return new TbsCertificateStructure(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00061E68 File Offset: 0x00061E68
		internal TbsCertificateStructure(Asn1Sequence seq)
		{
			int num = 0;
			this.seq = seq;
			if (seq[0] is Asn1TaggedObject)
			{
				this.version = DerInteger.GetInstance((Asn1TaggedObject)seq[0], true);
			}
			else
			{
				num = -1;
				this.version = new DerInteger(0);
			}
			bool flag = false;
			bool flag2 = false;
			if (this.version.Value.Equals(BigInteger.Zero))
			{
				flag = true;
			}
			else if (this.version.Value.Equals(BigInteger.One))
			{
				flag2 = true;
			}
			else if (!this.version.Value.Equals(BigInteger.Two))
			{
				throw new ArgumentException("version number not recognised");
			}
			this.serialNumber = DerInteger.GetInstance(seq[num + 1]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[num + 2]);
			this.issuer = X509Name.GetInstance(seq[num + 3]);
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[num + 4];
			this.startDate = Time.GetInstance(asn1Sequence[0]);
			this.endDate = Time.GetInstance(asn1Sequence[1]);
			this.subject = X509Name.GetInstance(seq[num + 5]);
			this.subjectPublicKeyInfo = SubjectPublicKeyInfo.GetInstance(seq[num + 6]);
			int i = seq.Count - (num + 6) - 1;
			if (i != 0 && flag)
			{
				throw new ArgumentException("version 1 certificate contains extra data");
			}
			while (i > 0)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num + 6 + i]);
				switch (instance.TagNo)
				{
				case 1:
					this.issuerUniqueID = DerBitString.GetInstance(instance, false);
					break;
				case 2:
					this.subjectUniqueID = DerBitString.GetInstance(instance, false);
					break;
				case 3:
					if (flag2)
					{
						throw new ArgumentException("version 2 certificate cannot contain extensions");
					}
					this.extensions = X509Extensions.GetInstance(Asn1Sequence.GetInstance(instance, true));
					break;
				default:
					throw new ArgumentException("Unknown tag encountered in structure: " + instance.TagNo);
				}
				i--;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0006209C File Offset: 0x0006209C
		public int Version
		{
			get
			{
				return this.version.IntValueExact + 1;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x000620AC File Offset: 0x000620AC
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x000620B4 File Offset: 0x000620B4
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000620BC File Offset: 0x000620BC
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x000620C4 File Offset: 0x000620C4
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000620CC File Offset: 0x000620CC
		public Time StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000620D4 File Offset: 0x000620D4
		public Time EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x000620DC File Offset: 0x000620DC
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x000620E4 File Offset: 0x000620E4
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPublicKeyInfo;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x000620EC File Offset: 0x000620EC
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x000620F4 File Offset: 0x000620F4
		public DerBitString SubjectUniqueID
		{
			get
			{
				return this.subjectUniqueID;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x000620FC File Offset: 0x000620FC
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00062104 File Offset: 0x00062104
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000C41 RID: 3137
		internal Asn1Sequence seq;

		// Token: 0x04000C42 RID: 3138
		internal DerInteger version;

		// Token: 0x04000C43 RID: 3139
		internal DerInteger serialNumber;

		// Token: 0x04000C44 RID: 3140
		internal AlgorithmIdentifier signature;

		// Token: 0x04000C45 RID: 3141
		internal X509Name issuer;

		// Token: 0x04000C46 RID: 3142
		internal Time startDate;

		// Token: 0x04000C47 RID: 3143
		internal Time endDate;

		// Token: 0x04000C48 RID: 3144
		internal X509Name subject;

		// Token: 0x04000C49 RID: 3145
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x04000C4A RID: 3146
		internal DerBitString issuerUniqueID;

		// Token: 0x04000C4B RID: 3147
		internal DerBitString subjectUniqueID;

		// Token: 0x04000C4C RID: 3148
		internal X509Extensions extensions;
	}
}
