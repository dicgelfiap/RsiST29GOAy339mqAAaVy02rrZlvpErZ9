using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000533 RID: 1331
	public abstract class TlsDHUtilities
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x000DB648 File Offset: 0x000DB648
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.DecodeStrict(hex));
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000DB658 File Offset: 0x000DB658
		private static DHParameters FromSafeP(string hexP)
		{
			BigInteger bigInteger = TlsDHUtilities.FromHex(hexP);
			BigInteger q = bigInteger.ShiftRight(1);
			return new DHParameters(bigInteger, TlsDHUtilities.Two, q);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000DB684 File Offset: 0x000DB684
		public static void AddNegotiatedDheGroupsClientExtension(IDictionary extensions, byte[] dheGroups)
		{
			extensions[ExtensionType.negotiated_ff_dhe_groups] = TlsDHUtilities.CreateNegotiatedDheGroupsClientExtension(dheGroups);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000DB69C File Offset: 0x000DB69C
		public static void AddNegotiatedDheGroupsServerExtension(IDictionary extensions, byte dheGroup)
		{
			extensions[ExtensionType.negotiated_ff_dhe_groups] = TlsDHUtilities.CreateNegotiatedDheGroupsServerExtension(dheGroup);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000DB6B4 File Offset: 0x000DB6B4
		public static byte[] GetNegotiatedDheGroupsClientExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, ExtensionType.negotiated_ff_dhe_groups);
			if (extensionData != null)
			{
				return TlsDHUtilities.ReadNegotiatedDheGroupsClientExtension(extensionData);
			}
			return null;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000DB6E0 File Offset: 0x000DB6E0
		public static short GetNegotiatedDheGroupsServerExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, ExtensionType.negotiated_ff_dhe_groups);
			if (extensionData != null)
			{
				return (short)TlsDHUtilities.ReadNegotiatedDheGroupsServerExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000DB70C File Offset: 0x000DB70C
		public static byte[] CreateNegotiatedDheGroupsClientExtension(byte[] dheGroups)
		{
			if (dheGroups == null || dheGroups.Length < 1 || dheGroups.Length > 255)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(dheGroups);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000DB738 File Offset: 0x000DB738
		public static byte[] CreateNegotiatedDheGroupsServerExtension(byte dheGroup)
		{
			return TlsUtilities.EncodeUint8(dheGroup);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000DB740 File Offset: 0x000DB740
		public static byte[] ReadNegotiatedDheGroupsClientExtension(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			return array;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000DB76C File Offset: 0x000DB76C
		public static byte ReadNegotiatedDheGroupsServerExtension(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000DB774 File Offset: 0x000DB774
		public static DHParameters GetParametersForDHEGroup(short dheGroup)
		{
			switch (dheGroup)
			{
			case 0:
				return TlsDHUtilities.draft_ffdhe2432;
			case 1:
				return TlsDHUtilities.draft_ffdhe3072;
			case 2:
				return TlsDHUtilities.draft_ffdhe4096;
			case 3:
				return TlsDHUtilities.draft_ffdhe6144;
			case 4:
				return TlsDHUtilities.draft_ffdhe8192;
			default:
				return null;
			}
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000DB7C8 File Offset: 0x000DB7C8
		public static bool ContainsDheCipherSuites(int[] cipherSuites)
		{
			for (int i = 0; i < cipherSuites.Length; i++)
			{
				if (TlsDHUtilities.IsDheCipherSuite(cipherSuites[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000DB7FC File Offset: 0x000DB7FC
		public static bool IsDheCipherSuite(int cipherSuite)
		{
			if (cipherSuite <= 181)
			{
				if (cipherSuite <= 58)
				{
					switch (cipherSuite)
					{
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 24:
					case 27:
						break;
					case 23:
					case 25:
					case 26:
						return false;
					default:
						if (cipherSuite != 45)
						{
							switch (cipherSuite)
							{
							case 50:
							case 51:
							case 52:
							case 56:
							case 57:
							case 58:
								break;
							case 53:
							case 54:
							case 55:
								return false;
							default:
								return false;
							}
						}
						break;
					}
				}
				else if (cipherSuite <= 109)
				{
					switch (cipherSuite)
					{
					case 64:
					case 68:
					case 69:
					case 70:
						break;
					case 65:
					case 66:
					case 67:
						return false;
					default:
						switch (cipherSuite)
						{
						case 103:
						case 106:
						case 107:
						case 108:
						case 109:
							break;
						case 104:
						case 105:
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
					case 135:
					case 136:
					case 137:
					case 142:
					case 143:
					case 144:
					case 145:
						break;
					case 138:
					case 139:
					case 140:
					case 141:
						return false;
					default:
						switch (cipherSuite)
						{
						case 153:
						case 154:
						case 155:
						case 158:
						case 159:
						case 162:
						case 163:
						case 166:
						case 167:
						case 170:
						case 171:
						case 178:
						case 179:
						case 180:
						case 181:
							break;
						case 156:
						case 157:
						case 160:
						case 161:
						case 164:
						case 165:
						case 168:
						case 169:
						case 172:
						case 173:
						case 174:
						case 175:
						case 176:
						case 177:
							return false;
						default:
							return false;
						}
						break;
					}
				}
			}
			else if (cipherSuite <= 49297)
			{
				switch (cipherSuite)
				{
				case 189:
				case 190:
				case 191:
				case 195:
				case 196:
				case 197:
					break;
				case 192:
				case 193:
				case 194:
					return false;
				default:
					switch (cipherSuite)
					{
					case 49276:
					case 49277:
					case 49280:
					case 49281:
					case 49284:
					case 49285:
						break;
					case 49278:
					case 49279:
					case 49282:
					case 49283:
						return false;
					default:
						switch (cipherSuite)
						{
						case 49296:
						case 49297:
							break;
						default:
							return false;
						}
						break;
					}
					break;
				}
			}
			else if (cipherSuite <= 49323)
			{
				switch (cipherSuite)
				{
				case 49302:
				case 49303:
					break;
				default:
					switch (cipherSuite)
					{
					case 49310:
					case 49311:
					case 49314:
					case 49315:
					case 49318:
					case 49319:
					case 49322:
					case 49323:
						break;
					case 49312:
					case 49313:
					case 49316:
					case 49317:
					case 49320:
					case 49321:
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (cipherSuite != 52394 && cipherSuite != 52397)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000DBAB0 File Offset: 0x000DBAB0
		public static bool AreCompatibleParameters(DHParameters a, DHParameters b)
		{
			return a.P.Equals(b.P) && a.G.Equals(b.G) && (a.Q == null || b.Q == null || a.Q.Equals(b.Q));
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000DBB18 File Offset: 0x000DBB18
		public static byte[] CalculateDHBasicAgreement(DHPublicKeyParameters publicKey, DHPrivateKeyParameters privateKey)
		{
			DHBasicAgreement dhbasicAgreement = new DHBasicAgreement();
			dhbasicAgreement.Init(privateKey);
			BigInteger n = dhbasicAgreement.CalculateAgreement(publicKey);
			return BigIntegers.AsUnsignedByteArray(n);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000DBB44 File Offset: 0x000DBB44
		public static AsymmetricCipherKeyPair GenerateDHKeyPair(SecureRandom random, DHParameters dhParams)
		{
			DHBasicKeyPairGenerator dhbasicKeyPairGenerator = new DHBasicKeyPairGenerator();
			dhbasicKeyPairGenerator.Init(new DHKeyGenerationParameters(random, dhParams));
			return dhbasicKeyPairGenerator.GenerateKeyPair();
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000DBB70 File Offset: 0x000DBB70
		public static DHPrivateKeyParameters GenerateEphemeralClientKeyExchange(SecureRandom random, DHParameters dhParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsDHUtilities.GenerateDHKeyPair(random, dhParams);
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)asymmetricCipherKeyPair.Public;
			TlsDHUtilities.WriteDHParameter(dhpublicKeyParameters.Y, output);
			return (DHPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000DBBAC File Offset: 0x000DBBAC
		public static DHPrivateKeyParameters GenerateEphemeralServerKeyExchange(SecureRandom random, DHParameters dhParams, Stream output)
		{
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = TlsDHUtilities.GenerateDHKeyPair(random, dhParams);
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)asymmetricCipherKeyPair.Public;
			TlsDHUtilities.WriteDHParameters(dhParams, output);
			TlsDHUtilities.WriteDHParameter(dhpublicKeyParameters.Y, output);
			return (DHPrivateKeyParameters)asymmetricCipherKeyPair.Private;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000DBBF0 File Offset: 0x000DBBF0
		public static BigInteger ReadDHParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque16(input));
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000DBC00 File Offset: 0x000DBC00
		public static DHParameters ReadDHParameters(Stream input)
		{
			BigInteger p = TlsDHUtilities.ReadDHParameter(input);
			BigInteger g = TlsDHUtilities.ReadDHParameter(input);
			return new DHParameters(p, g);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000DBC28 File Offset: 0x000DBC28
		public static DHParameters ReceiveDHParameters(TlsDHVerifier dhVerifier, Stream input)
		{
			DHParameters dhparameters = TlsDHUtilities.ReadDHParameters(input);
			if (!dhVerifier.Accept(dhparameters))
			{
				throw new TlsFatalAlert(71);
			}
			return dhparameters;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000DBC58 File Offset: 0x000DBC58
		public static void WriteDHParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque16(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000DBC68 File Offset: 0x000DBC68
		public static void WriteDHParameters(DHParameters dhParameters, Stream output)
		{
			TlsDHUtilities.WriteDHParameter(dhParameters.P, output);
			TlsDHUtilities.WriteDHParameter(dhParameters.G, output);
		}

		// Token: 0x04001ACD RID: 6861
		internal static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x04001ACE RID: 6862
		private static readonly string draft_ffdhe2432_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE13098533C8B3FFFFFFFFFFFFFFFF";

		// Token: 0x04001ACF RID: 6863
		internal static readonly DHParameters draft_ffdhe2432 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe2432_p);

		// Token: 0x04001AD0 RID: 6864
		private static readonly string draft_ffdhe3072_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B66C62E37FFFFFFFFFFFFFFFF";

		// Token: 0x04001AD1 RID: 6865
		internal static readonly DHParameters draft_ffdhe3072 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe3072_p);

		// Token: 0x04001AD2 RID: 6866
		private static readonly string draft_ffdhe4096_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E655F6AFFFFFFFFFFFFFFFF";

		// Token: 0x04001AD3 RID: 6867
		internal static readonly DHParameters draft_ffdhe4096 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe4096_p);

		// Token: 0x04001AD4 RID: 6868
		private static readonly string draft_ffdhe6144_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E0DD9020BFD64B645036C7A4E677D2C38532A3A23BA4442CAF53EA63BB454329B7624C8917BDD64B1C0FD4CB38E8C334C701C3ACDAD0657FCCFEC719B1F5C3E4E46041F388147FB4CFDB477A52471F7A9A96910B855322EDB6340D8A00EF092350511E30ABEC1FFF9E3A26E7FB29F8C183023C3587E38DA0077D9B4763E4E4B94B2BBC194C6651E77CAF992EEAAC0232A281BF6B3A739C1226116820AE8DB5847A67CBEF9C9091B462D538CD72B03746AE77F5E62292C311562A846505DC82DB854338AE49F5235C95B91178CCF2DD5CACEF403EC9D1810C6272B045B3B71F9DC6B80D63FDD4A8E9ADB1E6962A69526D43161C1A41D570D7938DAD4A40E329CD0E40E65FFFFFFFFFFFFFFFF";

		// Token: 0x04001AD5 RID: 6869
		internal static readonly DHParameters draft_ffdhe6144 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe6144_p);

		// Token: 0x04001AD6 RID: 6870
		private static readonly string draft_ffdhe8192_p = "FFFFFFFFFFFFFFFFADF85458A2BB4A9AAFDC5620273D3CF1D8B9C583CE2D3695A9E13641146433FBCC939DCE249B3EF97D2FE363630C75D8F681B202AEC4617AD3DF1ED5D5FD65612433F51F5F066ED0856365553DED1AF3B557135E7F57C935984F0C70E0E68B77E2A689DAF3EFE8721DF158A136ADE73530ACCA4F483A797ABC0AB182B324FB61D108A94BB2C8E3FBB96ADAB760D7F4681D4F42A3DE394DF4AE56EDE76372BB190B07A7C8EE0A6D709E02FCE1CDF7E2ECC03404CD28342F619172FE9CE98583FF8E4F1232EEF28183C3FE3B1B4C6FAD733BB5FCBC2EC22005C58EF1837D1683B2C6F34A26C1B2EFFA886B4238611FCFDCDE355B3B6519035BBC34F4DEF99C023861B46FC9D6E6C9077AD91D2691F7F7EE598CB0FAC186D91CAEFE130985139270B4130C93BC437944F4FD4452E2D74DD364F2E21E71F54BFF5CAE82AB9C9DF69EE86D2BC522363A0DABC521979B0DEADA1DBF9A42D5C4484E0ABCD06BFA53DDEF3C1B20EE3FD59D7C25E41D2B669E1EF16E6F52C3164DF4FB7930E9E4E58857B6AC7D5F42D69F6D187763CF1D5503400487F55BA57E31CC7A7135C886EFB4318AED6A1E012D9E6832A907600A918130C46DC778F971AD0038092999A333CB8B7A1A1DB93D7140003C2A4ECEA9F98D0ACC0A8291CDCEC97DCF8EC9B55A7F88A46B4DB5A851F44182E1C68A007E5E0DD9020BFD64B645036C7A4E677D2C38532A3A23BA4442CAF53EA63BB454329B7624C8917BDD64B1C0FD4CB38E8C334C701C3ACDAD0657FCCFEC719B1F5C3E4E46041F388147FB4CFDB477A52471F7A9A96910B855322EDB6340D8A00EF092350511E30ABEC1FFF9E3A26E7FB29F8C183023C3587E38DA0077D9B4763E4E4B94B2BBC194C6651E77CAF992EEAAC0232A281BF6B3A739C1226116820AE8DB5847A67CBEF9C9091B462D538CD72B03746AE77F5E62292C311562A846505DC82DB854338AE49F5235C95B91178CCF2DD5CACEF403EC9D1810C6272B045B3B71F9DC6B80D63FDD4A8E9ADB1E6962A69526D43161C1A41D570D7938DAD4A40E329CCFF46AAA36AD004CF600C8381E425A31D951AE64FDB23FCEC9509D43687FEB69EDD1CC5E0B8CC3BDF64B10EF86B63142A3AB8829555B2F747C932665CB2C0F1CC01BD70229388839D2AF05E454504AC78B7582822846C0BA35C35F5C59160CC046FD8251541FC68C9C86B022BB7099876A460E7451A8A93109703FEE1C217E6C3826E52C51AA691E0E423CFC99E9E31650C1217B624816CDAD9A95F9D5B8019488D9C0A0A1FE3075A577E23183F81D4A3F2FA4571EFC8CE0BA8A4FE8B6855DFE72B0A66EDED2FBABFBE58A30FAFABE1C5D71A87E2F741EF8C1FE86FEA6BBFDE530677F0D97D11D49F7A8443D0822E506A9F4614E011E2A94838FF88CD68C8BB7C5C6424CFFFFFFFFFFFFFFFF";

		// Token: 0x04001AD7 RID: 6871
		internal static readonly DHParameters draft_ffdhe8192 = TlsDHUtilities.FromSafeP(TlsDHUtilities.draft_ffdhe8192_p);
	}
}
