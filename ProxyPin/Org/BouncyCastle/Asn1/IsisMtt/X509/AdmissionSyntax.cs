using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000178 RID: 376
	public class AdmissionSyntax : Asn1Encodable
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x00050E84 File Offset: 0x00050E84
		public static AdmissionSyntax GetInstance(object obj)
		{
			if (obj == null || obj is AdmissionSyntax)
			{
				return (AdmissionSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AdmissionSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00050EE0 File Offset: 0x00050EE0
		private AdmissionSyntax(Asn1Sequence seq)
		{
			switch (seq.Count)
			{
			case 1:
				this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[0]);
				return;
			case 2:
				this.admissionAuthority = GeneralName.GetInstance(seq[0]);
				this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[1]);
				return;
			default:
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00050F68 File Offset: 0x00050F68
		public AdmissionSyntax(GeneralName admissionAuthority, Asn1Sequence contentsOfAdmissions)
		{
			this.admissionAuthority = admissionAuthority;
			this.contentsOfAdmissions = contentsOfAdmissions;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00050F80 File Offset: 0x00050F80
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.admissionAuthority
			});
			asn1EncodableVector.Add(this.contentsOfAdmissions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00050FC4 File Offset: 0x00050FC4
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00050FCC File Offset: 0x00050FCC
		public virtual Admissions[] GetContentsOfAdmissions()
		{
			Admissions[] array = new Admissions[this.contentsOfAdmissions.Count];
			for (int i = 0; i < this.contentsOfAdmissions.Count; i++)
			{
				array[i] = Admissions.GetInstance(this.contentsOfAdmissions[i]);
			}
			return array;
		}

		// Token: 0x040008BD RID: 2237
		private readonly GeneralName admissionAuthority;

		// Token: 0x040008BE RID: 2238
		private readonly Asn1Sequence contentsOfAdmissions;
	}
}
