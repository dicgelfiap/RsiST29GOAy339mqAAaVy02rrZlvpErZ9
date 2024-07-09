using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E9 RID: 489
	public class AuthorityKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005D214 File Offset: 0x0005D214
		public static AuthorityKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AuthorityKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005D224 File Offset: 0x0005D224
		public static AuthorityKeyIdentifier GetInstance(object obj)
		{
			if (obj is AuthorityKeyIdentifier)
			{
				return (AuthorityKeyIdentifier)obj;
			}
			if (obj is X509Extension)
			{
				return AuthorityKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			if (obj == null)
			{
				return null;
			}
			return new AuthorityKeyIdentifier(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0005D278 File Offset: 0x0005D278
		public static AuthorityKeyIdentifier FromExtensions(X509Extensions extensions)
		{
			return AuthorityKeyIdentifier.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.AuthorityKeyIdentifier));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0005D28C File Offset: 0x0005D28C
		protected internal AuthorityKeyIdentifier(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj2);
				switch (instance.TagNo)
				{
				case 0:
					this.keyidentifier = Asn1OctetString.GetInstance(instance, false);
					break;
				case 1:
					this.certissuer = GeneralNames.GetInstance(instance, false);
					break;
				case 2:
					this.certserno = DerInteger.GetInstance(instance, false);
					break;
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0005D354 File Offset: 0x0005D354
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki) : this(spki, null, null)
		{
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0005D360 File Offset: 0x0005D360
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki, GeneralNames name, BigInteger serialNumber)
		{
			IDigest digest = new Sha1Digest();
			byte[] array = new byte[digest.GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			digest.BlockUpdate(bytes, 0, bytes.Length);
			digest.DoFinal(array, 0);
			this.keyidentifier = new DerOctetString(array);
			this.certissuer = name;
			this.certserno = ((serialNumber == null) ? null : new DerInteger(serialNumber));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0005D3D4 File Offset: 0x0005D3D4
		public AuthorityKeyIdentifier(GeneralNames name, BigInteger serialNumber) : this(null, name, serialNumber)
		{
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0005D3E0 File Offset: 0x0005D3E0
		public AuthorityKeyIdentifier(byte[] keyIdentifier) : this(keyIdentifier, null, null)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0005D3EC File Offset: 0x0005D3EC
		public AuthorityKeyIdentifier(byte[] keyIdentifier, GeneralNames name, BigInteger serialNumber)
		{
			this.keyidentifier = ((keyIdentifier == null) ? null : new DerOctetString(keyIdentifier));
			this.certissuer = name;
			this.certserno = ((serialNumber == null) ? null : new DerInteger(serialNumber));
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0005D43C File Offset: 0x0005D43C
		public byte[] GetKeyIdentifier()
		{
			if (this.keyidentifier != null)
			{
				return this.keyidentifier.GetOctets();
			}
			return null;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0005D458 File Offset: 0x0005D458
		public GeneralNames AuthorityCertIssuer
		{
			get
			{
				return this.certissuer;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0005D460 File Offset: 0x0005D460
		public BigInteger AuthorityCertSerialNumber
		{
			get
			{
				if (this.certserno != null)
				{
					return this.certserno.Value;
				}
				return null;
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0005D47C File Offset: 0x0005D47C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.keyidentifier);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.certissuer);
			asn1EncodableVector.AddOptionalTagged(false, 2, this.certserno);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0005D4C4 File Offset: 0x0005D4C4
		public override string ToString()
		{
			string str = (this.keyidentifier != null) ? Hex.ToHexString(this.keyidentifier.GetOctets()) : "null";
			return "AuthorityKeyIdentifier: KeyID(" + str + ")";
		}

		// Token: 0x04000BAE RID: 2990
		private readonly Asn1OctetString keyidentifier;

		// Token: 0x04000BAF RID: 2991
		private readonly GeneralNames certissuer;

		// Token: 0x04000BB0 RID: 2992
		private readonly DerInteger certserno;
	}
}
