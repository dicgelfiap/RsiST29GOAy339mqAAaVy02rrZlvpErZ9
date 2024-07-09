using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200013B RID: 315
	public class PopoSigningKeyInput : Asn1Encodable
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0004A3DC File Offset: 0x0004A3DC
		private PopoSigningKeyInput(Asn1Sequence seq)
		{
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Unknown authInfo tag: " + asn1TaggedObject.TagNo, "seq");
				}
				this.sender = GeneralName.GetInstance(asn1TaggedObject.GetObject());
			}
			else
			{
				this.publicKeyMac = PKMacValue.GetInstance(asn1Encodable);
			}
			this.publicKey = SubjectPublicKeyInfo.GetInstance(seq[1]);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0004A46C File Offset: 0x0004A46C
		public static PopoSigningKeyInput GetInstance(object obj)
		{
			if (obj is PopoSigningKeyInput)
			{
				return (PopoSigningKeyInput)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoSigningKeyInput((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0004A4C0 File Offset: 0x0004A4C0
		public PopoSigningKeyInput(GeneralName sender, SubjectPublicKeyInfo spki)
		{
			this.sender = sender;
			this.publicKey = spki;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0004A4D8 File Offset: 0x0004A4D8
		public PopoSigningKeyInput(PKMacValue pkmac, SubjectPublicKeyInfo spki)
		{
			this.publicKeyMac = pkmac;
			this.publicKey = spki;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0004A4F0 File Offset: 0x0004A4F0
		public virtual GeneralName Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0004A4F8 File Offset: 0x0004A4F8
		public virtual PKMacValue PublicKeyMac
		{
			get
			{
				return this.publicKeyMac;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0004A500 File Offset: 0x0004A500
		public virtual SubjectPublicKeyInfo PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0004A508 File Offset: 0x0004A508
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.sender != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 0, this.sender));
			}
			else
			{
				asn1EncodableVector.Add(this.publicKeyMac);
			}
			asn1EncodableVector.Add(this.publicKey);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400078E RID: 1934
		private readonly GeneralName sender;

		// Token: 0x0400078F RID: 1935
		private readonly PKMacValue publicKeyMac;

		// Token: 0x04000790 RID: 1936
		private readonly SubjectPublicKeyInfo publicKey;
	}
}
