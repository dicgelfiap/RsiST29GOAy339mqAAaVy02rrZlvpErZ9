using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020E RID: 526
	public class SubjectKeyIdentifier : Asn1Encodable
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x00061894 File Offset: 0x00061894
		public static SubjectKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectKeyIdentifier.GetInstance(Asn1OctetString.GetInstance(obj, explicitly));
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000618A4 File Offset: 0x000618A4
		public static SubjectKeyIdentifier GetInstance(object obj)
		{
			if (obj is SubjectKeyIdentifier)
			{
				return (SubjectKeyIdentifier)obj;
			}
			if (obj is SubjectPublicKeyInfo)
			{
				return new SubjectKeyIdentifier((SubjectPublicKeyInfo)obj);
			}
			if (obj is X509Extension)
			{
				return SubjectKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			if (obj == null)
			{
				return null;
			}
			return new SubjectKeyIdentifier(Asn1OctetString.GetInstance(obj));
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00061910 File Offset: 0x00061910
		public static SubjectKeyIdentifier FromExtensions(X509Extensions extensions)
		{
			return SubjectKeyIdentifier.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.SubjectKeyIdentifier));
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00061924 File Offset: 0x00061924
		public SubjectKeyIdentifier(byte[] keyID)
		{
			if (keyID == null)
			{
				throw new ArgumentNullException("keyID");
			}
			this.keyIdentifier = Arrays.Clone(keyID);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0006194C File Offset: 0x0006194C
		public SubjectKeyIdentifier(Asn1OctetString keyID) : this(keyID.GetOctets())
		{
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0006195C File Offset: 0x0006195C
		public SubjectKeyIdentifier(SubjectPublicKeyInfo spki)
		{
			this.keyIdentifier = SubjectKeyIdentifier.GetDigest(spki);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00061970 File Offset: 0x00061970
		public byte[] GetKeyIdentifier()
		{
			return Arrays.Clone(this.keyIdentifier);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00061980 File Offset: 0x00061980
		public override Asn1Object ToAsn1Object()
		{
			return new DerOctetString(this.GetKeyIdentifier());
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00061990 File Offset: 0x00061990
		public static SubjectKeyIdentifier CreateSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			return new SubjectKeyIdentifier(keyInfo);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00061998 File Offset: 0x00061998
		public static SubjectKeyIdentifier CreateTruncatedSha1KeyIdentifier(SubjectPublicKeyInfo keyInfo)
		{
			byte[] digest = SubjectKeyIdentifier.GetDigest(keyInfo);
			byte[] array = new byte[8];
			Array.Copy(digest, digest.Length - 8, array, 0, array.Length);
			byte[] array2;
			(array2 = array)[0] = (array2[0] & 15);
			(array2 = array)[0] = (array2[0] | 64);
			return new SubjectKeyIdentifier(array);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000619E8 File Offset: 0x000619E8
		private static byte[] GetDigest(SubjectPublicKeyInfo spki)
		{
			IDigest digest = new Sha1Digest();
			byte[] array = new byte[digest.GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			digest.BlockUpdate(bytes, 0, bytes.Length);
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x04000C3A RID: 3130
		private readonly byte[] keyIdentifier;
	}
}
