using System;
using System.Collections;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C2 RID: 1730
	public abstract class TspAlgorithms
	{
		// Token: 0x06003C9B RID: 15515 RVA: 0x0014EAC8 File Offset: 0x0014EAC8
		static TspAlgorithms()
		{
			string[] array = new string[]
			{
				TspAlgorithms.Gost3411,
				TspAlgorithms.Gost3411_2012_256,
				TspAlgorithms.Gost3411_2012_512,
				TspAlgorithms.MD5,
				TspAlgorithms.RipeMD128,
				TspAlgorithms.RipeMD160,
				TspAlgorithms.RipeMD256,
				TspAlgorithms.Sha1,
				TspAlgorithms.Sha224,
				TspAlgorithms.Sha256,
				TspAlgorithms.Sha384,
				TspAlgorithms.Sha512,
				TspAlgorithms.SM3
			};
			TspAlgorithms.Allowed = Platform.CreateArrayList();
			foreach (string value in array)
			{
				TspAlgorithms.Allowed.Add(value);
			}
		}

		// Token: 0x04001ECC RID: 7884
		public static readonly string MD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x04001ECD RID: 7885
		public static readonly string Sha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x04001ECE RID: 7886
		public static readonly string Sha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x04001ECF RID: 7887
		public static readonly string Sha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x04001ED0 RID: 7888
		public static readonly string Sha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x04001ED1 RID: 7889
		public static readonly string Sha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x04001ED2 RID: 7890
		public static readonly string RipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x04001ED3 RID: 7891
		public static readonly string RipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x04001ED4 RID: 7892
		public static readonly string RipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x04001ED5 RID: 7893
		public static readonly string Gost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x04001ED6 RID: 7894
		public static readonly string Gost3411_2012_256 = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id;

		// Token: 0x04001ED7 RID: 7895
		public static readonly string Gost3411_2012_512 = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id;

		// Token: 0x04001ED8 RID: 7896
		public static readonly string SM3 = GMObjectIdentifiers.sm3.Id;

		// Token: 0x04001ED9 RID: 7897
		public static readonly IList Allowed;
	}
}
