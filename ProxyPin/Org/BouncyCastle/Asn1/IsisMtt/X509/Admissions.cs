using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000177 RID: 375
	public class Admissions : Asn1Encodable
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x00050BAC File Offset: 0x00050BAC
		public static Admissions GetInstance(object obj)
		{
			if (obj == null || obj is Admissions)
			{
				return (Admissions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Admissions((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00050C08 File Offset: 0x00050C08
		private Admissions(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				switch (((Asn1TaggedObject)asn1Encodable).TagNo)
				{
				case 0:
					this.admissionAuthority = GeneralName.GetInstance((Asn1TaggedObject)asn1Encodable, true);
					break;
				case 1:
					this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
					break;
				default:
					throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
				}
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			if (asn1Encodable is Asn1TaggedObject)
			{
				int tagNo = ((Asn1TaggedObject)asn1Encodable).TagNo;
				if (tagNo != 1)
				{
					throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionInfos = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(enumerator.Current));
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00050D88 File Offset: 0x00050D88
		public Admissions(GeneralName admissionAuthority, NamingAuthority namingAuthority, ProfessionInfo[] professionInfos)
		{
			this.admissionAuthority = admissionAuthority;
			this.namingAuthority = namingAuthority;
			this.professionInfos = new DerSequence(professionInfos);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00050DAC File Offset: 0x00050DAC
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00050DB4 File Offset: 0x00050DB4
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00050DBC File Offset: 0x00050DBC
		public ProfessionInfo[] GetProfessionInfos()
		{
			ProfessionInfo[] array = new ProfessionInfo[this.professionInfos.Count];
			int num = 0;
			foreach (object obj in this.professionInfos)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				array[num++] = ProfessionInfo.GetInstance(obj2);
			}
			return array;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00050E3C File Offset: 0x00050E3C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.admissionAuthority);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.namingAuthority);
			asn1EncodableVector.Add(this.professionInfos);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040008BA RID: 2234
		private readonly GeneralName admissionAuthority;

		// Token: 0x040008BB RID: 2235
		private readonly NamingAuthority namingAuthority;

		// Token: 0x040008BC RID: 2236
		private readonly Asn1Sequence professionInfos;
	}
}
