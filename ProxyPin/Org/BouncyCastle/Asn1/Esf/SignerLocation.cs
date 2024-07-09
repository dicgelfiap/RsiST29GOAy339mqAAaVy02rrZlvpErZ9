using System;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000161 RID: 353
	public class SignerLocation : Asn1Encodable
	{
		// Token: 0x06000C0E RID: 3086 RVA: 0x0004E728 File Offset: 0x0004E728
		public SignerLocation(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.countryName = DirectoryString.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.localityName = DirectoryString.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
				{
					bool explicitly = asn1TaggedObject.IsExplicit();
					this.postalAddress = Asn1Sequence.GetInstance(asn1TaggedObject, explicitly);
					if (this.postalAddress != null && this.postalAddress.Count > 6)
					{
						throw new ArgumentException("postal address must contain less than 6 strings");
					}
					break;
				}
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0004E814 File Offset: 0x0004E814
		private SignerLocation(DirectoryString countryName, DirectoryString localityName, Asn1Sequence postalAddress)
		{
			if (postalAddress != null && postalAddress.Count > 6)
			{
				throw new ArgumentException("postal address must contain less than 6 strings");
			}
			this.countryName = countryName;
			this.localityName = localityName;
			this.postalAddress = postalAddress;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0004E850 File Offset: 0x0004E850
		public SignerLocation(DirectoryString countryName, DirectoryString localityName, DirectoryString[] postalAddress) : this(countryName, localityName, new DerSequence(postalAddress))
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004E860 File Offset: 0x0004E860
		public SignerLocation(DerUtf8String countryName, DerUtf8String localityName, Asn1Sequence postalAddress) : this(DirectoryString.GetInstance(countryName), DirectoryString.GetInstance(localityName), postalAddress)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0004E878 File Offset: 0x0004E878
		public static SignerLocation GetInstance(object obj)
		{
			if (obj == null || obj is SignerLocation)
			{
				return (SignerLocation)obj;
			}
			return new SignerLocation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0004E8A0 File Offset: 0x0004E8A0
		public DirectoryString Country
		{
			get
			{
				return this.countryName;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0004E8A8 File Offset: 0x0004E8A8
		public DirectoryString Locality
		{
			get
			{
				return this.localityName;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0004E8B0 File Offset: 0x0004E8B0
		public DirectoryString[] GetPostal()
		{
			if (this.postalAddress == null)
			{
				return null;
			}
			DirectoryString[] array = new DirectoryString[this.postalAddress.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DirectoryString.GetInstance(this.postalAddress[num]);
			}
			return array;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0004E90C File Offset: 0x0004E90C
		[Obsolete("Use 'Country' property instead")]
		public DerUtf8String CountryName
		{
			get
			{
				if (this.countryName != null)
				{
					return new DerUtf8String(this.countryName.GetString());
				}
				return null;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0004E92C File Offset: 0x0004E92C
		[Obsolete("Use 'Locality' property instead")]
		public DerUtf8String LocalityName
		{
			get
			{
				if (this.localityName != null)
				{
					return new DerUtf8String(this.localityName.GetString());
				}
				return null;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0004E94C File Offset: 0x0004E94C
		public Asn1Sequence PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0004E954 File Offset: 0x0004E954
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.countryName);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.localityName);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.postalAddress);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000827 RID: 2087
		private DirectoryString countryName;

		// Token: 0x04000828 RID: 2088
		private DirectoryString localityName;

		// Token: 0x04000829 RID: 2089
		private Asn1Sequence postalAddress;
	}
}
