using System;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200017C RID: 380
	public class ProcurationSyntax : Asn1Encodable
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0005163C File Offset: 0x0005163C
		public static ProcurationSyntax GetInstance(object obj)
		{
			if (obj == null || obj is ProcurationSyntax)
			{
				return (ProcurationSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProcurationSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00051698 File Offset: 0x00051698
		private ProcurationSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 1:
					this.country = DerPrintableString.GetInstance(instance, true).GetString();
					break;
				case 2:
					this.typeOfSubstitution = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
				{
					Asn1Object @object = instance.GetObject();
					if (@object is Asn1TaggedObject)
					{
						this.thirdPerson = GeneralName.GetInstance(@object);
					}
					else
					{
						this.certRef = IssuerSerial.GetInstance(@object);
					}
					break;
				}
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000517A0 File Offset: 0x000517A0
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, IssuerSerial certRef)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = null;
			this.certRef = certRef;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000517C4 File Offset: 0x000517C4
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, GeneralName thirdPerson)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = thirdPerson;
			this.certRef = null;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000517E8 File Offset: 0x000517E8
		public virtual string Country
		{
			get
			{
				return this.country;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000517F0 File Offset: 0x000517F0
		public virtual DirectoryString TypeOfSubstitution
		{
			get
			{
				return this.typeOfSubstitution;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000517F8 File Offset: 0x000517F8
		public virtual GeneralName ThirdPerson
		{
			get
			{
				return this.thirdPerson;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00051800 File Offset: 0x00051800
		public virtual IssuerSerial CertRef
		{
			get
			{
				return this.certRef;
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00051808 File Offset: 0x00051808
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.country != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, new DerPrintableString(this.country, true)));
			}
			asn1EncodableVector.AddOptionalTagged(true, 2, this.typeOfSubstitution);
			if (this.thirdPerson != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 3, this.thirdPerson));
			}
			else
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 3, this.certRef));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040008C7 RID: 2247
		private readonly string country;

		// Token: 0x040008C8 RID: 2248
		private readonly DirectoryString typeOfSubstitution;

		// Token: 0x040008C9 RID: 2249
		private readonly GeneralName thirdPerson;

		// Token: 0x040008CA RID: 2250
		private readonly IssuerSerial certRef;
	}
}
