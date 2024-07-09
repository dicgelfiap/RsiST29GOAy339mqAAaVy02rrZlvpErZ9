using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000683 RID: 1667
	public class Pkcs12Utilities
	{
		// Token: 0x06003A42 RID: 14914 RVA: 0x00139FA8 File Offset: 0x00139FA8
		public static byte[] ConvertToDefiniteLength(byte[] berPkcs12File)
		{
			Pfx pfx = new Pfx(Asn1Sequence.GetInstance(Asn1Object.FromByteArray(berPkcs12File)));
			return pfx.GetEncoded("DER");
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x00139FD8 File Offset: 0x00139FD8
		public static byte[] ConvertToDefiniteLength(byte[] berPkcs12File, char[] passwd)
		{
			Pfx pfx = new Pfx(Asn1Sequence.GetInstance(Asn1Object.FromByteArray(berPkcs12File)));
			ContentInfo contentInfo = pfx.AuthSafe;
			Asn1OctetString instance = Asn1OctetString.GetInstance(contentInfo.Content);
			Asn1Object asn1Object = Asn1Object.FromByteArray(instance.GetOctets());
			contentInfo = new ContentInfo(contentInfo.ContentType, new DerOctetString(asn1Object.GetEncoded("DER")));
			MacData macData = pfx.MacData;
			try
			{
				int intValue = macData.IterationCount.IntValue;
				byte[] octets = Asn1OctetString.GetInstance(contentInfo.Content).GetOctets();
				byte[] digest = Pkcs12Store.CalculatePbeMac(macData.Mac.AlgorithmID.Algorithm, macData.GetSalt(), intValue, passwd, false, octets);
				AlgorithmIdentifier algID = new AlgorithmIdentifier(macData.Mac.AlgorithmID.Algorithm, DerNull.Instance);
				DigestInfo digInfo = new DigestInfo(algID, digest);
				macData = new MacData(digInfo, macData.GetSalt(), intValue);
			}
			catch (Exception ex)
			{
				throw new IOException("error constructing MAC: " + ex.ToString());
			}
			pfx = new Pfx(contentInfo, macData);
			return pfx.GetEncoded("DER");
		}
	}
}
