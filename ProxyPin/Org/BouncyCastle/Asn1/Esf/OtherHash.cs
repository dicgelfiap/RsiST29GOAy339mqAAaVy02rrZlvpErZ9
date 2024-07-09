using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000158 RID: 344
	public class OtherHash : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x0004D5DC File Offset: 0x0004D5DC
		public static OtherHash GetInstance(object obj)
		{
			if (obj == null || obj is OtherHash)
			{
				return (OtherHash)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new OtherHash((Asn1OctetString)obj);
			}
			return new OtherHash(OtherHashAlgAndValue.GetInstance(obj));
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0004D618 File Offset: 0x0004D618
		public OtherHash(byte[] sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = new DerOctetString(sha1Hash);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0004D640 File Offset: 0x0004D640
		public OtherHash(Asn1OctetString sha1Hash)
		{
			if (sha1Hash == null)
			{
				throw new ArgumentNullException("sha1Hash");
			}
			this.sha1Hash = sha1Hash;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0004D660 File Offset: 0x0004D660
		public OtherHash(OtherHashAlgAndValue otherHash)
		{
			if (otherHash == null)
			{
				throw new ArgumentNullException("otherHash");
			}
			this.otherHash = otherHash;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0004D680 File Offset: 0x0004D680
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				if (this.otherHash != null)
				{
					return this.otherHash.HashAlgorithm;
				}
				return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0004D6A4 File Offset: 0x0004D6A4
		public byte[] GetHashValue()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.GetHashValue();
			}
			return this.sha1Hash.GetOctets();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0004D6C8 File Offset: 0x0004D6C8
		public override Asn1Object ToAsn1Object()
		{
			if (this.otherHash != null)
			{
				return this.otherHash.ToAsn1Object();
			}
			return this.sha1Hash;
		}

		// Token: 0x04000814 RID: 2068
		private readonly Asn1OctetString sha1Hash;

		// Token: 0x04000815 RID: 2069
		private readonly OtherHashAlgAndValue otherHash;
	}
}
