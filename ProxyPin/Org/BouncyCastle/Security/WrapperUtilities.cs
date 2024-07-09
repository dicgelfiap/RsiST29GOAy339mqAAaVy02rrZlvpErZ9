using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B9 RID: 1721
	public sealed class WrapperUtilities
	{
		// Token: 0x06003C3C RID: 15420 RVA: 0x0014D4BC File Offset: 0x0014D4BC
		private WrapperUtilities()
		{
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x0014D4C4 File Offset: 0x0014D4C4
		static WrapperUtilities()
		{
			((WrapperUtilities.WrapAlgorithm)Enums.GetArbitraryValue(typeof(WrapperUtilities.WrapAlgorithm))).ToString();
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes128Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes192Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes256Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id] = "DESEDEWRAP";
			WrapperUtilities.algorithms["TDEAWRAP"] = "DESEDEWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCmsRC2Wrap.Id] = "RC2WRAP";
			WrapperUtilities.algorithms[KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id] = "SEEDWRAP";
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0014D5F4 File Offset: 0x0014D5F4
		public static IWrapper GetWrapper(DerObjectIdentifier oid)
		{
			return WrapperUtilities.GetWrapper(oid.Id);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0014D604 File Offset: 0x0014D604
		public static IWrapper GetWrapper(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)WrapperUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			try
			{
				switch ((WrapperUtilities.WrapAlgorithm)Enums.GetEnumValue(typeof(WrapperUtilities.WrapAlgorithm), text2))
				{
				case WrapperUtilities.WrapAlgorithm.AESWRAP:
					return new AesWrapEngine();
				case WrapperUtilities.WrapAlgorithm.CAMELLIAWRAP:
					return new CamelliaWrapEngine();
				case WrapperUtilities.WrapAlgorithm.DESEDEWRAP:
					return new DesEdeWrapEngine();
				case WrapperUtilities.WrapAlgorithm.RC2WRAP:
					return new RC2WrapEngine();
				case WrapperUtilities.WrapAlgorithm.SEEDWRAP:
					return new SeedWrapEngine();
				case WrapperUtilities.WrapAlgorithm.DESEDERFC3211WRAP:
					return new Rfc3211WrapEngine(new DesEdeEngine());
				case WrapperUtilities.WrapAlgorithm.AESRFC3211WRAP:
					return new Rfc3211WrapEngine(new AesEngine());
				case WrapperUtilities.WrapAlgorithm.CAMELLIARFC3211WRAP:
					return new Rfc3211WrapEngine(new CamelliaEngine());
				}
			}
			catch (ArgumentException)
			{
			}
			IBufferedCipher cipher = CipherUtilities.GetCipher(algorithm);
			if (cipher != null)
			{
				return new WrapperUtilities.BufferedCipherWrapper(cipher);
			}
			throw new SecurityUtilityException("Wrapper " + algorithm + " not recognised.");
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x0014D730 File Offset: 0x0014D730
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)WrapperUtilities.algorithms[oid.Id];
		}

		// Token: 0x04001EA8 RID: 7848
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x02000E6D RID: 3693
		private enum WrapAlgorithm
		{
			// Token: 0x040042DD RID: 17117
			AESWRAP,
			// Token: 0x040042DE RID: 17118
			CAMELLIAWRAP,
			// Token: 0x040042DF RID: 17119
			DESEDEWRAP,
			// Token: 0x040042E0 RID: 17120
			RC2WRAP,
			// Token: 0x040042E1 RID: 17121
			SEEDWRAP,
			// Token: 0x040042E2 RID: 17122
			DESEDERFC3211WRAP,
			// Token: 0x040042E3 RID: 17123
			AESRFC3211WRAP,
			// Token: 0x040042E4 RID: 17124
			CAMELLIARFC3211WRAP
		}

		// Token: 0x02000E6E RID: 3694
		private class BufferedCipherWrapper : IWrapper
		{
			// Token: 0x06008D6E RID: 36206 RVA: 0x002A6934 File Offset: 0x002A6934
			public BufferedCipherWrapper(IBufferedCipher cipher)
			{
				this.cipher = cipher;
			}

			// Token: 0x17001DAE RID: 7598
			// (get) Token: 0x06008D6F RID: 36207 RVA: 0x002A6944 File Offset: 0x002A6944
			public string AlgorithmName
			{
				get
				{
					return this.cipher.AlgorithmName;
				}
			}

			// Token: 0x06008D70 RID: 36208 RVA: 0x002A6954 File Offset: 0x002A6954
			public void Init(bool forWrapping, ICipherParameters parameters)
			{
				this.forWrapping = forWrapping;
				this.cipher.Init(forWrapping, parameters);
			}

			// Token: 0x06008D71 RID: 36209 RVA: 0x002A696C File Offset: 0x002A696C
			public byte[] Wrap(byte[] input, int inOff, int length)
			{
				if (!this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for wrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x06008D72 RID: 36210 RVA: 0x002A6994 File Offset: 0x002A6994
			public byte[] Unwrap(byte[] input, int inOff, int length)
			{
				if (this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for unwrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x040042E5 RID: 17125
			private readonly IBufferedCipher cipher;

			// Token: 0x040042E6 RID: 17126
			private bool forWrapping;
		}
	}
}
