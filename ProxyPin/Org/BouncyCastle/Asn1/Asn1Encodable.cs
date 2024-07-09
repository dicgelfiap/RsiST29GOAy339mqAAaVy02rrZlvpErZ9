using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000CA RID: 202
	public abstract class Asn1Encodable : IAsn1Convertible
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x000405FC File Offset: 0x000405FC
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			Asn1OutputStream asn1OutputStream = new Asn1OutputStream(memoryStream);
			asn1OutputStream.WriteObject(this);
			return memoryStream.ToArray();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00040628 File Offset: 0x00040628
		public byte[] GetEncoded(string encoding)
		{
			if (encoding.Equals("DER"))
			{
				MemoryStream memoryStream = new MemoryStream();
				DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
				derOutputStream.WriteObject(this);
				return memoryStream.ToArray();
			}
			return this.GetEncoded();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0004066C File Offset: 0x0004066C
		public byte[] GetDerEncoded()
		{
			byte[] result;
			try
			{
				result = this.GetEncoded("DER");
			}
			catch (IOException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000406A4 File Offset: 0x000406A4
		public sealed override int GetHashCode()
		{
			return this.ToAsn1Object().CallAsn1GetHashCode();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000406B4 File Offset: 0x000406B4
		public sealed override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IAsn1Convertible asn1Convertible = obj as IAsn1Convertible;
			if (asn1Convertible == null)
			{
				return false;
			}
			Asn1Object asn1Object = this.ToAsn1Object();
			Asn1Object asn1Object2 = asn1Convertible.ToAsn1Object();
			return asn1Object == asn1Object2 || (asn1Object2 != null && asn1Object.CallAsn1Equals(asn1Object2));
		}

		// Token: 0x060007E2 RID: 2018
		public abstract Asn1Object ToAsn1Object();

		// Token: 0x040005BB RID: 1467
		public const string Der = "DER";

		// Token: 0x040005BC RID: 1468
		public const string Ber = "BER";
	}
}
