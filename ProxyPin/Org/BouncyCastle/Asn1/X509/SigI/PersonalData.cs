using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020001E0 RID: 480
	public class PersonalData : Asn1Encodable
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x0005C4EC File Offset: 0x0005C4EC
		public static PersonalData GetInstance(object obj)
		{
			if (obj == null || obj is PersonalData)
			{
				return (PersonalData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PersonalData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0005C548 File Offset: 0x0005C548
		private PersonalData(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.nameOrPseudonym = NameOrPseudonym.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 0:
					this.nameDistinguisher = DerInteger.GetInstance(instance, false).Value;
					break;
				case 1:
					this.dateOfBirth = DerGeneralizedTime.GetInstance(instance, false);
					break;
				case 2:
					this.placeOfBirth = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
					this.gender = DerPrintableString.GetInstance(instance, false).GetString();
					break;
				case 4:
					this.postalAddress = DirectoryString.GetInstance(instance, true);
					break;
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0005C668 File Offset: 0x0005C668
		public PersonalData(NameOrPseudonym nameOrPseudonym, BigInteger nameDistinguisher, DerGeneralizedTime dateOfBirth, DirectoryString placeOfBirth, string gender, DirectoryString postalAddress)
		{
			this.nameOrPseudonym = nameOrPseudonym;
			this.dateOfBirth = dateOfBirth;
			this.gender = gender;
			this.nameDistinguisher = nameDistinguisher;
			this.postalAddress = postalAddress;
			this.placeOfBirth = placeOfBirth;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0005C6A0 File Offset: 0x0005C6A0
		public NameOrPseudonym NameOrPseudonym
		{
			get
			{
				return this.nameOrPseudonym;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0005C6A8 File Offset: 0x0005C6A8
		public BigInteger NameDistinguisher
		{
			get
			{
				return this.nameDistinguisher;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0005C6B0 File Offset: 0x0005C6B0
		public DerGeneralizedTime DateOfBirth
		{
			get
			{
				return this.dateOfBirth;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0005C6B8 File Offset: 0x0005C6B8
		public DirectoryString PlaceOfBirth
		{
			get
			{
				return this.placeOfBirth;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0005C6C0 File Offset: 0x0005C6C0
		public string Gender
		{
			get
			{
				return this.gender;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0005C6C8 File Offset: 0x0005C6C8
		public DirectoryString PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0005C6D0 File Offset: 0x0005C6D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.nameOrPseudonym
			});
			if (this.nameDistinguisher != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 0, new DerInteger(this.nameDistinguisher)));
			}
			asn1EncodableVector.AddOptionalTagged(false, 1, this.dateOfBirth);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.placeOfBirth);
			if (this.gender != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 3, new DerPrintableString(this.gender, true)));
			}
			asn1EncodableVector.AddOptionalTagged(true, 4, this.postalAddress);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B8B RID: 2955
		private readonly NameOrPseudonym nameOrPseudonym;

		// Token: 0x04000B8C RID: 2956
		private readonly BigInteger nameDistinguisher;

		// Token: 0x04000B8D RID: 2957
		private readonly DerGeneralizedTime dateOfBirth;

		// Token: 0x04000B8E RID: 2958
		private readonly DirectoryString placeOfBirth;

		// Token: 0x04000B8F RID: 2959
		private readonly string gender;

		// Token: 0x04000B90 RID: 2960
		private readonly DirectoryString postalAddress;
	}
}
