using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.Field;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000536 RID: 1334
	public abstract class TlsEccUtilities
	{
		// Token: 0x060028D3 RID: 10451 RVA: 0x000DBEA4 File Offset: 0x000DBEA4
		public static void AddSupportedEllipticCurvesExtension(IDictionary extensions, int[] namedCurves)
		{
			extensions[10] = TlsEccUtilities.CreateSupportedEllipticCurvesExtension(namedCurves);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000DBEBC File Offset: 0x000DBEBC
		public static void AddSupportedPointFormatsExtension(IDictionary extensions, byte[] ecPointFormats)
		{
			extensions[11] = TlsEccUtilities.CreateSupportedPointFormatsExtension(ecPointFormats);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000DBED4 File Offset: 0x000DBED4
		public static int[] GetSupportedEllipticCurvesExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 10);
			if (extensionData != null)
			{
				return TlsEccUtilities.ReadSupportedEllipticCurvesExtension(extensionData);
			}
			return null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000DBEFC File Offset: 0x000DBEFC
		public static byte[] GetSupportedPointFormatsExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 11);
			if (extensionData != null)
			{
				return TlsEccUtilities.ReadSupportedPointFormatsExtension(extensionData);
			}
			return null;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000DBF24 File Offset: 0x000DBF24
		public static byte[] CreateSupportedEllipticCurvesExtension(int[] namedCurves)
		{
			if (namedCurves == null || namedCurves.Length < 1)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint16ArrayWithUint16Length(namedCurves);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000DBF44 File Offset: 0x000DBF44
		public static byte[] CreateSupportedPointFormatsExtension(byte[] ecPointFormats)
		{
			if (ecPointFormats == null || !Arrays.Contains(ecPointFormats, 0))
			{
				ecPointFormats = Arrays.Append(ecPointFormats, 0);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(ecPointFormats);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000DBF68 File Offset: 0x000DBF68
		public static int[] ReadSupportedEllipticCurvesExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			int num = TlsUtilities.ReadUint16(memoryStream);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int[] result = TlsUtilities.ReadUint16Array(num / 2, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000DBFC4 File Offset: 0x000DBFC4
		public static byte[] ReadSupportedPointFormatsExtension(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (!Arrays.Contains(array, 0))
			{
				throw new TlsFatalAlert(47);
			}
			return array;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000DBFF4 File Offset: 0x000DBFF4
		public static string GetNameOfNamedCurve(int namedCurve)
		{
			if (!TlsEccUtilities.IsSupportedNamedCurve(namedCurve))
			{
				return null;
			}
			return TlsEccUtilities.CurveNames[namedCurve - 1];
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000DC010 File Offset: 0x000DC010
		public static ECDomainParameters GetParametersForNamedCurve(int namedCurve)
		{
			string nameOfNamedCurve = TlsEccUtilities.GetNameOfNamedCurve(namedCurve);
			if (nameOfNamedCurve == null)
			{
				return null;
			}
			X9ECParameters byName = CustomNamedCurves.GetByName(nameOfNamedCurve);
			if (byName == null)
			{
				byName = ECNamedCurveTable.GetByName(nameOfNamedCurve);
				if (byName == null)
				{
					return null;
				}
			}
			return new ECDomainParameters(byName.Curve, byName.G, byName.N, byName.H, byName.GetSeed());
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000DC070 File Offset: 0x000DC070
		public static bool HasAnySupportedNamedCurves()
		{
			return TlsEccUtilities.CurveNames.Length > 0;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000DC07C File Offset: 0x000DC07C
		public static bool ContainsEccCipherSuites(int[] cipherSuites)
		{
			for (int i = 0; i < cipherSuites.Length; i++)
			{
				if (TlsEccUtilities.IsEccCipherSuite(cipherSuites[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000DC0B0 File Offset: 0x000DC0B0
		public static bool IsEccCipherSuite(int cipherSuite)
		{
			if (cipherSuite <= 49293)
			{
				switch (cipherSuite)
				{
				case 49153:
				case 49154:
				case 49155:
				case 49156:
				case 49157:
				case 49158:
				case 49159:
				case 49160:
				case 49161:
				case 49162:
				case 49163:
				case 49164:
				case 49165:
				case 49166:
				case 49167:
				case 49168:
				case 49169:
				case 49170:
				case 49171:
				case 49172:
				case 49173:
				case 49174:
				case 49175:
				case 49176:
				case 49177:
				case 49187:
				case 49188:
				case 49189:
				case 49190:
				case 49191:
				case 49192:
				case 49193:
				case 49194:
				case 49195:
				case 49196:
				case 49197:
				case 49198:
				case 49199:
				case 49200:
				case 49201:
				case 49202:
				case 49203:
				case 49204:
				case 49205:
				case 49206:
				case 49207:
				case 49208:
				case 49209:
				case 49210:
				case 49211:
					break;
				case 49178:
				case 49179:
				case 49180:
				case 49181:
				case 49182:
				case 49183:
				case 49184:
				case 49185:
				case 49186:
					return false;
				default:
					switch (cipherSuite)
					{
					case 49266:
					case 49267:
					case 49268:
					case 49269:
					case 49270:
					case 49271:
					case 49272:
					case 49273:
					case 49286:
					case 49287:
					case 49288:
					case 49289:
					case 49290:
					case 49291:
					case 49292:
					case 49293:
						break;
					case 49274:
					case 49275:
					case 49276:
					case 49277:
					case 49278:
					case 49279:
					case 49280:
					case 49281:
					case 49282:
					case 49283:
					case 49284:
					case 49285:
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else
			{
				switch (cipherSuite)
				{
				case 49306:
				case 49307:
					break;
				default:
					switch (cipherSuite)
					{
					case 49324:
					case 49325:
					case 49326:
					case 49327:
						break;
					default:
						switch (cipherSuite)
						{
						case 52392:
						case 52393:
						case 52396:
							break;
						case 52394:
						case 52395:
							return false;
						default:
							return false;
						}
						break;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000DC2A0 File Offset: 0x000DC2A0
		public static bool AreOnSameCurve(ECDomainParameters a, ECDomainParameters b)
		{
			return a != null && a.Equals(b);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000DC2B4 File Offset: 0x000DC2B4
		public static bool IsSupportedNamedCurve(int namedCurve)
		{
			return namedCurve > 0 && namedCurve <= TlsEccUtilities.CurveNames.Length;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000DC2CC File Offset: 0x000DC2CC
		public static bool IsCompressionPreferred(byte[] ecPointFormats, byte compressionFormat)
		{
			if (ecPointFormats == null)
			{
				return false;
			}
			foreach (byte b in ecPointFormats)
			{
				if (b == 0)
				{
					return false;
				}
				if (b == compressionFormat)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000DC30C File Offset: 0x000DC30C
		public static byte[] SerializeECFieldElement(int fieldSize, BigInteger x)
		{
			return BigIntegers.AsUnsignedByteArray((fieldSize + 7) / 8, x);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000DC31C File Offset: 0x000DC31C
		public static byte[] SerializeECPoint(byte[] ecPointFormats, ECPoint point)
		{
			ECCurve curve = point.Curve;
			bool compressed = false;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				compressed = TlsEccUtilities.IsCompressionPreferred(ecPointFormats, 1);
			}
			else if (ECAlgorithms.IsF2mCurve(curve))
			{
				compressed = TlsEccUtilities.IsCompressionPreferred(ecPointFormats, 2);
			}
			return point.GetEncoded(compressed);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000DC368 File Offset: 0x000DC368
		public static byte[] SerializeECPublicKey(byte[] ecPointFormats, ECPublicKeyParameters keyParameters)
		{
			return TlsEccUtilities.SerializeECPoint(ecPointFormats, keyParameters.Q);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000DC378 File Offset: 0x000DC378
		public static BigInteger DeserializeECFieldElement(int fieldSize, byte[] encoding)
		{
			int num = (fieldSize + 7) / 8;
			if (encoding.Length != num)
			{
				throw new TlsFatalAlert(50);
			}
			return new BigInteger(1, encoding);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000DC3A8 File Offset: 0x000DC3A8
		public static ECPoint DeserializeECPoint(byte[] ecPointFormats, ECCurve curve, byte[] encoding)
		{
			if (encoding == null || encoding.Length < 1)
			{
				throw new TlsFatalAlert(47);
			}
			byte b;
			switch (encoding[0])
			{
			case 2:
			case 3:
				if (ECAlgorithms.IsF2mCurve(curve))
				{
					b = 2;
					goto IL_81;
				}
				if (ECAlgorithms.IsFpCurve(curve))
				{
					b = 1;
					goto IL_81;
				}
				throw new TlsFatalAlert(47);
			case 4:
				b = 0;
				goto IL_81;
			}
			throw new TlsFatalAlert(47);
			IL_81:
			if (b != 0 && (ecPointFormats == null || !Arrays.Contains(ecPointFormats, b)))
			{
				throw new TlsFatalAlert(47);
			}
			return curve.DecodePoint(encoding);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000DC464 File Offset: 0x000DC464
		public static ECPublicKeyParameters DeserializeECPublicKey(byte[] ecPointFormats, ECDomainParameters curve_params, byte[] encoding)
		{
			ECPublicKeyParameters result;
			try
			{
				ECPoint q = TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve_params.Curve, encoding);
				result = new ECPublicKeyParameters(q, curve_params);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000DC4A8 File Offset: 0x000DC4A8
		public static byte[] CalculateECDHBasicAgreement(ECPublicKeyParameters publicKey, ECPrivateKeyParameters privateKey)
		{
			ECDHBasicAgreement ecdhbasicAgreement = new ECDHBasicAgreement();
			ecdhbasicAgreement.Init(privateKey);
			BigInteger n = ecdhbasicAgreement.CalculateAgreement(publicKey);
			return BigIntegers.AsUnsignedByteArray(ecdhbasicAgreement.GetFieldSize(), n);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000DC4DC File Offset: 0x000DC4DC
		public static AsymmetricCipherKeyPair GenerateECKeyPair(SecureRandom random, ECDomainParameters ecParams)
		{
			ECKeyPairGenerator eckeyPairGenerator = new ECKeyPairGenerator();
			eckeyPairGenerator.Init(new ECKeyGenerationParameters(ecParams, random));
			return eckeyPairGenerator.GenerateKeyPair();
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000DC508 File Offset: 0x000DC508
		public static ECPrivateKeyParameters GenerateEphemeralClientKeyExchange(SecureRandom random, byte[] ecPointFormats, ECDomainParameters ecParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsEccUtilities.GenerateECKeyPair(random, ecParams);
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)asymmetricCipherKeyPair.Public;
			TlsEccUtilities.WriteECPoint(ecPointFormats, ecpublicKeyParameters.Q, output);
			return (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000DC548 File Offset: 0x000DC548
		internal static ECPrivateKeyParameters GenerateEphemeralServerKeyExchange(SecureRandom random, int[] namedCurves, byte[] ecPointFormats, Stream output)
		{
			int num = -1;
			if (namedCurves == null)
			{
				num = 23;
			}
			else
			{
				foreach (int num2 in namedCurves)
				{
					if (NamedCurve.IsValid(num2) && TlsEccUtilities.IsSupportedNamedCurve(num2))
					{
						num = num2;
						break;
					}
				}
			}
			ECDomainParameters ecdomainParameters = null;
			if (num >= 0)
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(num);
			}
			else if (Arrays.Contains(namedCurves, 65281))
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(23);
			}
			else if (Arrays.Contains(namedCurves, 65282))
			{
				ecdomainParameters = TlsEccUtilities.GetParametersForNamedCurve(10);
			}
			if (ecdomainParameters == null)
			{
				throw new TlsFatalAlert(80);
			}
			if (num < 0)
			{
				TlsEccUtilities.WriteExplicitECParameters(ecPointFormats, ecdomainParameters, output);
			}
			else
			{
				TlsEccUtilities.WriteNamedECParameters(num, output);
			}
			return TlsEccUtilities.GenerateEphemeralClientKeyExchange(random, ecPointFormats, ecdomainParameters, output);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000DC618 File Offset: 0x000DC618
		public static ECPublicKeyParameters ValidateECPublicKey(ECPublicKeyParameters key)
		{
			return key;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000DC61C File Offset: 0x000DC61C
		public static int ReadECExponent(int fieldSize, Stream input)
		{
			BigInteger bigInteger = TlsEccUtilities.ReadECParameter(input);
			if (bigInteger.BitLength < 32)
			{
				int intValue = bigInteger.IntValue;
				if (intValue > 0 && intValue < fieldSize)
				{
					return intValue;
				}
			}
			throw new TlsFatalAlert(47);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000DC660 File Offset: 0x000DC660
		public static BigInteger ReadECFieldElement(int fieldSize, Stream input)
		{
			return TlsEccUtilities.DeserializeECFieldElement(fieldSize, TlsUtilities.ReadOpaque8(input));
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000DC670 File Offset: 0x000DC670
		public static BigInteger ReadECParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque8(input));
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000DC680 File Offset: 0x000DC680
		public static ECDomainParameters ReadECParameters(int[] namedCurves, byte[] ecPointFormats, Stream input)
		{
			ECDomainParameters result;
			try
			{
				switch (TlsUtilities.ReadUint8(input))
				{
				case 1:
				{
					TlsEccUtilities.CheckNamedCurve(namedCurves, 65281);
					BigInteger bigInteger = TlsEccUtilities.ReadECParameter(input);
					BigInteger a = TlsEccUtilities.ReadECFieldElement(bigInteger.BitLength, input);
					BigInteger b = TlsEccUtilities.ReadECFieldElement(bigInteger.BitLength, input);
					byte[] encoding = TlsUtilities.ReadOpaque8(input);
					BigInteger bigInteger2 = TlsEccUtilities.ReadECParameter(input);
					BigInteger bigInteger3 = TlsEccUtilities.ReadECParameter(input);
					ECCurve curve = new FpCurve(bigInteger, a, b, bigInteger2, bigInteger3);
					ECPoint g = TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve, encoding);
					result = new ECDomainParameters(curve, g, bigInteger2, bigInteger3);
					break;
				}
				case 2:
				{
					TlsEccUtilities.CheckNamedCurve(namedCurves, 65282);
					int num = TlsUtilities.ReadUint16(input);
					byte b2 = TlsUtilities.ReadUint8(input);
					if (!ECBasisType.IsValid(b2))
					{
						throw new TlsFatalAlert(47);
					}
					int num2 = TlsEccUtilities.ReadECExponent(num, input);
					int k = -1;
					int k2 = -1;
					if (b2 == 2)
					{
						k = TlsEccUtilities.ReadECExponent(num, input);
						k2 = TlsEccUtilities.ReadECExponent(num, input);
					}
					BigInteger a2 = TlsEccUtilities.ReadECFieldElement(num, input);
					BigInteger b3 = TlsEccUtilities.ReadECFieldElement(num, input);
					byte[] encoding2 = TlsUtilities.ReadOpaque8(input);
					BigInteger bigInteger4 = TlsEccUtilities.ReadECParameter(input);
					BigInteger bigInteger5 = TlsEccUtilities.ReadECParameter(input);
					ECCurve curve2 = (b2 == 2) ? new F2mCurve(num, num2, k, k2, a2, b3, bigInteger4, bigInteger5) : new F2mCurve(num, num2, a2, b3, bigInteger4, bigInteger5);
					ECPoint g2 = TlsEccUtilities.DeserializeECPoint(ecPointFormats, curve2, encoding2);
					result = new ECDomainParameters(curve2, g2, bigInteger4, bigInteger5);
					break;
				}
				case 3:
				{
					int namedCurve = TlsUtilities.ReadUint16(input);
					if (!NamedCurve.RefersToASpecificNamedCurve(namedCurve))
					{
						throw new TlsFatalAlert(47);
					}
					TlsEccUtilities.CheckNamedCurve(namedCurves, namedCurve);
					result = TlsEccUtilities.GetParametersForNamedCurve(namedCurve);
					break;
				}
				default:
					throw new TlsFatalAlert(47);
				}
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000DC864 File Offset: 0x000DC864
		private static void CheckNamedCurve(int[] namedCurves, int namedCurve)
		{
			if (namedCurves != null && !Arrays.Contains(namedCurves, namedCurve))
			{
				throw new TlsFatalAlert(47);
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000DC880 File Offset: 0x000DC880
		public static void WriteECExponent(int k, Stream output)
		{
			BigInteger x = BigInteger.ValueOf((long)k);
			TlsEccUtilities.WriteECParameter(x, output);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000DC8A0 File Offset: 0x000DC8A0
		public static void WriteECFieldElement(ECFieldElement x, Stream output)
		{
			TlsUtilities.WriteOpaque8(x.GetEncoded(), output);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000DC8B0 File Offset: 0x000DC8B0
		public static void WriteECFieldElement(int fieldSize, BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECFieldElement(fieldSize, x), output);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000DC8C0 File Offset: 0x000DC8C0
		public static void WriteECParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque8(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000DC8D0 File Offset: 0x000DC8D0
		public static void WriteExplicitECParameters(byte[] ecPointFormats, ECDomainParameters ecParameters, Stream output)
		{
			ECCurve curve = ecParameters.Curve;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				TlsUtilities.WriteUint8(1, output);
				TlsEccUtilities.WriteECParameter(curve.Field.Characteristic, output);
			}
			else
			{
				if (!ECAlgorithms.IsF2mCurve(curve))
				{
					throw new ArgumentException("'ecParameters' not a known curve type");
				}
				IPolynomialExtensionField polynomialExtensionField = (IPolynomialExtensionField)curve.Field;
				int[] exponentsPresent = polynomialExtensionField.MinimalPolynomial.GetExponentsPresent();
				TlsUtilities.WriteUint8(2, output);
				int i = exponentsPresent[exponentsPresent.Length - 1];
				TlsUtilities.CheckUint16(i);
				TlsUtilities.WriteUint16(i, output);
				if (exponentsPresent.Length == 3)
				{
					TlsUtilities.WriteUint8(1, output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[1], output);
				}
				else
				{
					if (exponentsPresent.Length != 5)
					{
						throw new ArgumentException("Only trinomial and pentomial curves are supported");
					}
					TlsUtilities.WriteUint8(2, output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[1], output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[2], output);
					TlsEccUtilities.WriteECExponent(exponentsPresent[3], output);
				}
			}
			TlsEccUtilities.WriteECFieldElement(curve.A, output);
			TlsEccUtilities.WriteECFieldElement(curve.B, output);
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECPoint(ecPointFormats, ecParameters.G), output);
			TlsEccUtilities.WriteECParameter(ecParameters.N, output);
			TlsEccUtilities.WriteECParameter(ecParameters.H, output);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000DC9F8 File Offset: 0x000DC9F8
		public static void WriteECPoint(byte[] ecPointFormats, ECPoint point, Stream output)
		{
			TlsUtilities.WriteOpaque8(TlsEccUtilities.SerializeECPoint(ecPointFormats, point), output);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000DCA08 File Offset: 0x000DCA08
		public static void WriteNamedECParameters(int namedCurve, Stream output)
		{
			if (!NamedCurve.RefersToASpecificNamedCurve(namedCurve))
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteUint8(3, output);
			TlsUtilities.CheckUint16(namedCurve);
			TlsUtilities.WriteUint16(namedCurve, output);
		}

		// Token: 0x04001AD8 RID: 6872
		private static readonly string[] CurveNames = new string[]
		{
			"sect163k1",
			"sect163r1",
			"sect163r2",
			"sect193r1",
			"sect193r2",
			"sect233k1",
			"sect233r1",
			"sect239k1",
			"sect283k1",
			"sect283r1",
			"sect409k1",
			"sect409r1",
			"sect571k1",
			"sect571r1",
			"secp160k1",
			"secp160r1",
			"secp160r2",
			"secp192k1",
			"secp192r1",
			"secp224k1",
			"secp224r1",
			"secp256k1",
			"secp256r1",
			"secp384r1",
			"secp521r1",
			"brainpoolP256r1",
			"brainpoolP384r1",
			"brainpoolP512r1"
		};
	}
}
