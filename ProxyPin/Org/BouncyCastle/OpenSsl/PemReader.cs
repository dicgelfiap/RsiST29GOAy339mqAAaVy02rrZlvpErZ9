using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.IO.Pem;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x02000677 RID: 1655
	public class PemReader : PemReader
	{
		// Token: 0x060039D6 RID: 14806 RVA: 0x0013662C File Offset: 0x0013662C
		public PemReader(TextReader reader) : this(reader, null)
		{
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x00136638 File Offset: 0x00136638
		public PemReader(TextReader reader, IPasswordFinder pFinder) : base(reader)
		{
			this.pFinder = pFinder;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x00136648 File Offset: 0x00136648
		public object ReadObject()
		{
			if (BouncyCastle.Crypto263585.<PrivateImplementationDetails>.$$method0x60034f8-1 == null)
			{
				BouncyCastle.Crypto263585.<PrivateImplementationDetails>.$$method0x60034f8-1 = new Hashtable(22, 0.5f)
				{
					{
						"PUBLIC KEY",
						0
					},
					{
						"RSA PUBLIC KEY",
						1
					},
					{
						"CERTIFICATE REQUEST",
						2
					},
					{
						"NEW CERTIFICATE REQUEST",
						3
					},
					{
						"CERTIFICATE",
						4
					},
					{
						"X509 CERTIFICATE",
						5
					},
					{
						"PKCS7",
						6
					},
					{
						"CMS",
						7
					},
					{
						"X509 CRL",
						8
					},
					{
						"ATTRIBUTE CERTIFICATE",
						9
					}
				};
			}
			PemObject pemObject = base.ReadPemObject();
			if (pemObject == null)
			{
				return null;
			}
			if (Platform.EndsWith(pemObject.Type, "PRIVATE KEY"))
			{
				return this.ReadPrivateKey(pemObject);
			}
			object obj;
			if ((obj = pemObject.Type) != null && (obj = BouncyCastle.Crypto263585.<PrivateImplementationDetails>.$$method0x60034f8-1[obj]) != null)
			{
				switch ((int)obj)
				{
				case 0:
					return this.ReadPublicKey(pemObject);
				case 1:
					return this.ReadRsaPublicKey(pemObject);
				case 2:
				case 3:
					return this.ReadCertificateRequest(pemObject);
				case 4:
				case 5:
					return this.ReadCertificate(pemObject);
				case 6:
				case 7:
					return this.ReadPkcs7(pemObject);
				case 8:
					return this.ReadCrl(pemObject);
				case 9:
					return this.ReadAttributeCertificate(pemObject);
				}
			}
			throw new IOException("unrecognised object: " + pemObject.Type);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x001367F8 File Offset: 0x001367F8
		private AsymmetricKeyParameter ReadRsaPublicKey(PemObject pemObject)
		{
			RsaPublicKeyStructure instance = RsaPublicKeyStructure.GetInstance(Asn1Object.FromByteArray(pemObject.Content));
			return new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x0013682C File Offset: 0x0013682C
		private AsymmetricKeyParameter ReadPublicKey(PemObject pemObject)
		{
			return PublicKeyFactory.CreateKey(pemObject.Content);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x0013683C File Offset: 0x0013683C
		private X509Certificate ReadCertificate(PemObject pemObject)
		{
			X509Certificate result;
			try
			{
				result = new X509CertificateParser().ReadCertificate(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x00136888 File Offset: 0x00136888
		private X509Crl ReadCrl(PemObject pemObject)
		{
			X509Crl result;
			try
			{
				result = new X509CrlParser().ReadCrl(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x001368D4 File Offset: 0x001368D4
		private Pkcs10CertificationRequest ReadCertificateRequest(PemObject pemObject)
		{
			Pkcs10CertificationRequest result;
			try
			{
				result = new Pkcs10CertificationRequest(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0013691C File Offset: 0x0013691C
		private IX509AttributeCertificate ReadAttributeCertificate(PemObject pemObject)
		{
			return new X509V2AttributeCertificate(pemObject.Content);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0013692C File Offset: 0x0013692C
		private Org.BouncyCastle.Asn1.Cms.ContentInfo ReadPkcs7(PemObject pemObject)
		{
			Org.BouncyCastle.Asn1.Cms.ContentInfo instance;
			try
			{
				instance = Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(Asn1Object.FromByteArray(pemObject.Content));
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing PKCS7 object: " + ex.ToString());
			}
			return instance;
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x00136978 File Offset: 0x00136978
		private object ReadPrivateKey(PemObject pemObject)
		{
			string text = pemObject.Type.Substring(0, pemObject.Type.Length - "PRIVATE KEY".Length).Trim();
			byte[] array = pemObject.Content;
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in pemObject.Headers)
			{
				PemHeader pemHeader = (PemHeader)obj;
				dictionary[pemHeader.Name] = pemHeader.Value;
			}
			string a = (string)dictionary["Proc-Type"];
			if (a == "4,ENCRYPTED")
			{
				if (this.pFinder == null)
				{
					throw new PasswordException("No password finder specified, but a password is required");
				}
				char[] password = this.pFinder.GetPassword();
				if (password == null)
				{
					throw new PasswordException("Password is null, but a password is required");
				}
				string text2 = (string)dictionary["DEK-Info"];
				string[] array2 = text2.Split(new char[]
				{
					','
				});
				string dekAlgName = array2[0].Trim();
				byte[] iv = Hex.Decode(array2[1].Trim());
				array = PemUtilities.Crypt(false, array, password, dekAlgName, iv);
			}
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(array);
				string text3;
				if ((text3 = text) != null)
				{
					text3 = string.IsInterned(text3);
					AsymmetricKeyParameter asymmetricKeyParameter;
					AsymmetricKeyParameter publicParameter;
					if (text3 != "RSA")
					{
						if (text3 != "DSA")
						{
							if (text3 != "EC")
							{
								if (text3 != "ENCRYPTED")
								{
									if (text3 != "")
									{
										goto IL_395;
									}
									return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(instance));
								}
								else
								{
									char[] password2 = this.pFinder.GetPassword();
									if (password2 == null)
									{
										throw new PasswordException("Password is null, but a password is required");
									}
									return PrivateKeyFactory.DecryptKey(password2, EncryptedPrivateKeyInfo.GetInstance(instance));
								}
							}
							else
							{
								ECPrivateKeyStructure instance2 = ECPrivateKeyStructure.GetInstance(instance);
								AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, instance2.GetParameters());
								PrivateKeyInfo keyInfo = new PrivateKeyInfo(algorithmIdentifier, instance2.ToAsn1Object());
								asymmetricKeyParameter = PrivateKeyFactory.CreateKey(keyInfo);
								DerBitString publicKey = instance2.GetPublicKey();
								if (publicKey != null)
								{
									SubjectPublicKeyInfo keyInfo2 = new SubjectPublicKeyInfo(algorithmIdentifier, publicKey.GetBytes());
									publicParameter = PublicKeyFactory.CreateKey(keyInfo2);
								}
								else
								{
									publicParameter = ECKeyPairGenerator.GetCorrespondingPublicKey((ECPrivateKeyParameters)asymmetricKeyParameter);
								}
							}
						}
						else
						{
							if (instance.Count != 6)
							{
								throw new PemException("malformed sequence in DSA private key");
							}
							DerInteger derInteger = (DerInteger)instance[1];
							DerInteger derInteger2 = (DerInteger)instance[2];
							DerInteger derInteger3 = (DerInteger)instance[3];
							DerInteger derInteger4 = (DerInteger)instance[4];
							DerInteger derInteger5 = (DerInteger)instance[5];
							DsaParameters parameters = new DsaParameters(derInteger.Value, derInteger2.Value, derInteger3.Value);
							asymmetricKeyParameter = new DsaPrivateKeyParameters(derInteger5.Value, parameters);
							publicParameter = new DsaPublicKeyParameters(derInteger4.Value, parameters);
						}
					}
					else
					{
						if (instance.Count != 9)
						{
							throw new PemException("malformed sequence in RSA private key");
						}
						RsaPrivateKeyStructure instance3 = RsaPrivateKeyStructure.GetInstance(instance);
						publicParameter = new RsaKeyParameters(false, instance3.Modulus, instance3.PublicExponent);
						asymmetricKeyParameter = new RsaPrivateCrtKeyParameters(instance3.Modulus, instance3.PublicExponent, instance3.PrivateExponent, instance3.Prime1, instance3.Prime2, instance3.Exponent1, instance3.Exponent2, instance3.Coefficient);
					}
					return new AsymmetricCipherKeyPair(publicParameter, asymmetricKeyParameter);
				}
				IL_395:
				throw new ArgumentException("Unknown key type: " + text, "type");
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new PemException("problem creating " + text + " private key: " + ex2.ToString());
			}
			object result;
			return result;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x00136DB4 File Offset: 0x00136DB4
		private static X9ECParameters GetCurveParameters(string name)
		{
			X9ECParameters byName = CustomNamedCurves.GetByName(name);
			if (byName == null)
			{
				byName = ECNamedCurveTable.GetByName(name);
			}
			if (byName == null)
			{
				throw new Exception("unknown curve name: " + name);
			}
			return byName;
		}

		// Token: 0x04001E1F RID: 7711
		private readonly IPasswordFinder pFinder;
	}
}
