using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200021A RID: 538
	public class V2Form : Asn1Encodable
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x00062B54 File Offset: 0x00062B54
		public static V2Form GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return V2Form.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00062B64 File Offset: 0x00062B64
		public static V2Form GetInstance(object obj)
		{
			if (obj is V2Form)
			{
				return (V2Form)obj;
			}
			if (obj != null)
			{
				return new V2Form(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00062B8C File Offset: 0x00062B8C
		public V2Form(GeneralNames issuerName) : this(issuerName, null, null)
		{
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00062B98 File Offset: 0x00062B98
		public V2Form(GeneralNames issuerName, IssuerSerial baseCertificateID) : this(issuerName, baseCertificateID, null)
		{
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00062BA4 File Offset: 0x00062BA4
		public V2Form(GeneralNames issuerName, ObjectDigestInfo objectDigestInfo) : this(issuerName, null, objectDigestInfo)
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00062BB0 File Offset: 0x00062BB0
		public V2Form(GeneralNames issuerName, IssuerSerial baseCertificateID, ObjectDigestInfo objectDigestInfo)
		{
			this.issuerName = issuerName;
			this.baseCertificateID = baseCertificateID;
			this.objectDigestInfo = objectDigestInfo;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00062BD0 File Offset: 0x00062BD0
		private V2Form(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			if (!(seq[0] is Asn1TaggedObject))
			{
				num++;
				this.issuerName = GeneralNames.GetInstance(seq[0]);
			}
			for (int num2 = num; num2 != seq.Count; num2++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num2]);
				if (instance.TagNo == 0)
				{
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00062CB0 File Offset: 0x00062CB0
		public GeneralNames IssuerName
		{
			get
			{
				return this.issuerName;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00062CB8 File Offset: 0x00062CB8
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00062CC0 File Offset: 0x00062CC0
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00062CC8 File Offset: 0x00062CC8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerName
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.baseCertificateID);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.objectDigestInfo);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C6E RID: 3182
		internal GeneralNames issuerName;

		// Token: 0x04000C6F RID: 3183
		internal IssuerSerial baseCertificateID;

		// Token: 0x04000C70 RID: 3184
		internal ObjectDigestInfo objectDigestInfo;
	}
}
