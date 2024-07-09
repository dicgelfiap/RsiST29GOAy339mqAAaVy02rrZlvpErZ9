using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000681 RID: 1665
	public class Pkcs12Store
	{
		// Token: 0x06003A24 RID: 14884 RVA: 0x001384E0 File Offset: 0x001384E0
		private static SubjectKeyIdentifier CreateSubjectKeyID(AsymmetricKeyParameter pubKey)
		{
			return new SubjectKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey));
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x001384F0 File Offset: 0x001384F0
		internal Pkcs12Store(DerObjectIdentifier keyAlgorithm, DerObjectIdentifier certAlgorithm, bool useDerEncoding)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.certAlgorithm = certAlgorithm;
			this.useDerEncoding = useDerEncoding;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x0013855C File Offset: 0x0013855C
		public Pkcs12Store() : this(PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc, PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc, false)
		{
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x00138570 File Offset: 0x00138570
		public Pkcs12Store(Stream input, char[] password) : this()
		{
			this.Load(input, password);
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x00138580 File Offset: 0x00138580
		protected virtual void LoadKeyBag(PrivateKeyInfo privKeyInfo, Asn1Set bagAttributes)
		{
			AsymmetricKeyParameter key = PrivateKeyFactory.CreateKey(privKeyInfo);
			IDictionary dictionary = Platform.CreateHashtable();
			AsymmetricKeyEntry value = new AsymmetricKeyEntry(key, dictionary);
			string text = null;
			Asn1OctetString asn1OctetString = null;
			if (bagAttributes != null)
			{
				foreach (object obj in bagAttributes)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
					DerObjectIdentifier instance = DerObjectIdentifier.GetInstance(asn1Sequence[0]);
					Asn1Set instance2 = Asn1Set.GetInstance(asn1Sequence[1]);
					if (instance2.Count > 0)
					{
						Asn1Encodable asn1Encodable = instance2[0];
						if (dictionary.Contains(instance.Id))
						{
							if (!dictionary[instance.Id].Equals(asn1Encodable))
							{
								throw new IOException("attempt to add existing attribute with different value");
							}
						}
						else
						{
							dictionary.Add(instance.Id, asn1Encodable);
						}
						if (instance.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
						{
							text = ((DerBmpString)asn1Encodable).GetString();
							this.keys[text] = value;
						}
						else if (instance.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
						{
							asn1OctetString = (Asn1OctetString)asn1Encodable;
						}
					}
				}
			}
			if (asn1OctetString == null)
			{
				this.unmarkedKeyEntry = value;
				return;
			}
			string text2 = Hex.ToHexString(asn1OctetString.GetOctets());
			if (text == null)
			{
				this.keys[text2] = value;
				return;
			}
			this.localIds[text] = text2;
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x00138708 File Offset: 0x00138708
		protected virtual void LoadPkcs8ShroudedKeyBag(EncryptedPrivateKeyInfo encPrivKeyInfo, Asn1Set bagAttributes, char[] password, bool wrongPkcs12Zero)
		{
			if (password != null)
			{
				PrivateKeyInfo privKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, wrongPkcs12Zero, encPrivKeyInfo);
				this.LoadKeyBag(privKeyInfo, bagAttributes);
			}
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x00138734 File Offset: 0x00138734
		public void Load(Stream input, char[] password)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Pfx instance = Pfx.GetInstance(Asn1Object.FromStream(input));
			ContentInfo authSafe = instance.AuthSafe;
			bool wrongPkcs12Zero = false;
			if (instance.MacData != null)
			{
				if (password == null)
				{
					throw new ArgumentNullException("password", "no password supplied when one expected");
				}
				MacData macData = instance.MacData;
				DigestInfo mac = macData.Mac;
				AlgorithmIdentifier algorithmID = mac.AlgorithmID;
				byte[] salt = macData.GetSalt();
				int intValue = macData.IterationCount.IntValue;
				byte[] octets = Asn1OctetString.GetInstance(authSafe.Content).GetOctets();
				byte[] a = Pkcs12Store.CalculatePbeMac(algorithmID.Algorithm, salt, intValue, password, false, octets);
				byte[] digest = mac.GetDigest();
				if (!Arrays.ConstantTimeAreEqual(a, digest))
				{
					if (password.Length > 0)
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					a = Pkcs12Store.CalculatePbeMac(algorithmID.Algorithm, salt, intValue, password, true, octets);
					if (!Arrays.ConstantTimeAreEqual(a, digest))
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					wrongPkcs12Zero = true;
				}
			}
			else if (password != null)
			{
				string environmentVariable = Platform.GetEnvironmentVariable("Org.BouncyCastle.Pkcs12.IgnoreUselessPassword");
				if (environmentVariable == null || !Platform.EqualsIgnoreCase("true", environmentVariable))
				{
					throw new IOException("password supplied for keystore that does not require one");
				}
			}
			this.keys.Clear();
			this.localIds.Clear();
			this.unmarkedKeyEntry = null;
			IList list = Platform.CreateArrayList();
			if (authSafe.ContentType.Equals(PkcsObjectIdentifiers.Data))
			{
				Asn1OctetString instance2 = Asn1OctetString.GetInstance(authSafe.Content);
				AuthenticatedSafe instance3 = AuthenticatedSafe.GetInstance(instance2.GetOctets());
				ContentInfo[] contentInfo = instance3.GetContentInfo();
				foreach (ContentInfo contentInfo2 in contentInfo)
				{
					DerObjectIdentifier contentType = contentInfo2.ContentType;
					byte[] array2 = null;
					if (contentType.Equals(PkcsObjectIdentifiers.Data))
					{
						array2 = Asn1OctetString.GetInstance(contentInfo2.Content).GetOctets();
					}
					else if (contentType.Equals(PkcsObjectIdentifiers.EncryptedData) && password != null)
					{
						EncryptedData instance4 = EncryptedData.GetInstance(contentInfo2.Content);
						array2 = Pkcs12Store.CryptPbeData(false, instance4.EncryptionAlgorithm, password, wrongPkcs12Zero, instance4.Content.GetOctets());
					}
					if (array2 != null)
					{
						Asn1Sequence instance5 = Asn1Sequence.GetInstance(array2);
						foreach (object obj in instance5)
						{
							Asn1Sequence seq = (Asn1Sequence)obj;
							SafeBag safeBag = new SafeBag(seq);
							if (safeBag.BagID.Equals(PkcsObjectIdentifiers.CertBag))
							{
								list.Add(safeBag);
							}
							else if (safeBag.BagID.Equals(PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag))
							{
								this.LoadPkcs8ShroudedKeyBag(EncryptedPrivateKeyInfo.GetInstance(safeBag.BagValue), safeBag.BagAttributes, password, wrongPkcs12Zero);
							}
							else if (safeBag.BagID.Equals(PkcsObjectIdentifiers.KeyBag))
							{
								this.LoadKeyBag(PrivateKeyInfo.GetInstance(safeBag.BagValue), safeBag.BagAttributes);
							}
						}
					}
				}
			}
			this.certs.Clear();
			this.chainCerts.Clear();
			this.keyCerts.Clear();
			foreach (object obj2 in list)
			{
				SafeBag safeBag2 = (SafeBag)obj2;
				CertBag certBag = new CertBag((Asn1Sequence)safeBag2.BagValue);
				byte[] octets2 = ((Asn1OctetString)certBag.CertValue).GetOctets();
				X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(octets2);
				IDictionary dictionary = Platform.CreateHashtable();
				Asn1OctetString asn1OctetString = null;
				string text = null;
				if (safeBag2.BagAttributes != null)
				{
					foreach (object obj3 in safeBag2.BagAttributes)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)obj3;
						DerObjectIdentifier instance6 = DerObjectIdentifier.GetInstance(asn1Sequence[0]);
						Asn1Set instance7 = Asn1Set.GetInstance(asn1Sequence[1]);
						if (instance7.Count > 0)
						{
							Asn1Encodable asn1Encodable = instance7[0];
							if (dictionary.Contains(instance6.Id))
							{
								if (!dictionary[instance6.Id].Equals(asn1Encodable))
								{
									throw new IOException("attempt to add existing attribute with different value");
								}
							}
							else
							{
								dictionary.Add(instance6.Id, asn1Encodable);
							}
							if (instance6.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
							{
								text = ((DerBmpString)asn1Encodable).GetString();
							}
							else if (instance6.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
							{
								asn1OctetString = (Asn1OctetString)asn1Encodable;
							}
						}
					}
				}
				Pkcs12Store.CertId certId = new Pkcs12Store.CertId(x509Certificate.GetPublicKey());
				X509CertificateEntry value = new X509CertificateEntry(x509Certificate, dictionary);
				this.chainCerts[certId] = value;
				if (this.unmarkedKeyEntry != null)
				{
					if (this.keyCerts.Count == 0)
					{
						string text2 = Hex.ToHexString(certId.Id);
						this.keyCerts[text2] = value;
						this.keys[text2] = this.unmarkedKeyEntry;
					}
					else
					{
						this.keys["unmarked"] = this.unmarkedKeyEntry;
					}
				}
				else
				{
					if (asn1OctetString != null)
					{
						string key = Hex.ToHexString(asn1OctetString.GetOctets());
						this.keyCerts[key] = value;
					}
					if (text != null)
					{
						this.certs[text] = value;
					}
				}
			}
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x00138D34 File Offset: 0x00138D34
		public AsymmetricKeyEntry GetKey(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return (AsymmetricKeyEntry)this.keys[alias];
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x00138D58 File Offset: 0x00138D58
		public bool IsCertificateEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.certs[alias] != null && this.keys[alias] == null;
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x00138D90 File Offset: 0x00138D90
		public bool IsKeyEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.keys[alias] != null;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x00138DB8 File Offset: 0x00138DB8
		private IDictionary GetAliasesTable()
		{
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in this.certs.Keys)
			{
				string key = (string)obj;
				dictionary[key] = "cert";
			}
			foreach (object obj2 in this.keys.Keys)
			{
				string key2 = (string)obj2;
				if (dictionary[key2] == null)
				{
					dictionary[key2] = "key";
				}
			}
			return dictionary;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x00138E98 File Offset: 0x00138E98
		public IEnumerable Aliases
		{
			get
			{
				return new EnumerableProxy(this.GetAliasesTable().Keys);
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x00138EAC File Offset: 0x00138EAC
		public bool ContainsAlias(string alias)
		{
			return this.certs[alias] != null || this.keys[alias] != null;
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x00138ED4 File Offset: 0x00138ED4
		public X509CertificateEntry GetCertificate(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry == null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				else
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[alias];
				}
			}
			return x509CertificateEntry;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x00138F50 File Offset: 0x00138F50
		public string GetCertificateAlias(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			foreach (object obj in this.certs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)dictionaryEntry.Value;
				if (x509CertificateEntry.Certificate.Equals(cert))
				{
					return (string)dictionaryEntry.Key;
				}
			}
			foreach (object obj2 in this.keyCerts)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				X509CertificateEntry x509CertificateEntry2 = (X509CertificateEntry)dictionaryEntry2.Value;
				if (x509CertificateEntry2.Certificate.Equals(cert))
				{
					return (string)dictionaryEntry2.Key;
				}
			}
			return null;
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x00139080 File Offset: 0x00139080
		public X509CertificateEntry[] GetCertificateChain(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (!this.IsKeyEntry(alias))
			{
				return null;
			}
			X509CertificateEntry x509CertificateEntry = this.GetCertificate(alias);
			if (x509CertificateEntry != null)
			{
				IList list = Platform.CreateArrayList();
				while (x509CertificateEntry != null)
				{
					X509Certificate certificate = x509CertificateEntry.Certificate;
					X509CertificateEntry x509CertificateEntry2 = null;
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.AuthorityKeyIdentifier);
					if (extensionValue != null)
					{
						AuthorityKeyIdentifier instance = AuthorityKeyIdentifier.GetInstance(extensionValue.GetOctets());
						byte[] keyIdentifier = instance.GetKeyIdentifier();
						if (keyIdentifier != null)
						{
							x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[new Pkcs12Store.CertId(keyIdentifier)];
						}
					}
					if (x509CertificateEntry2 == null)
					{
						X509Name issuerDN = certificate.IssuerDN;
						X509Name subjectDN = certificate.SubjectDN;
						if (!issuerDN.Equivalent(subjectDN))
						{
							foreach (object obj in this.chainCerts.Keys)
							{
								Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj;
								X509CertificateEntry x509CertificateEntry3 = (X509CertificateEntry)this.chainCerts[key];
								X509Certificate certificate2 = x509CertificateEntry3.Certificate;
								X509Name subjectDN2 = certificate2.SubjectDN;
								if (subjectDN2.Equivalent(issuerDN))
								{
									try
									{
										certificate.Verify(certificate2.GetPublicKey());
										x509CertificateEntry2 = x509CertificateEntry3;
										break;
									}
									catch (InvalidKeyException)
									{
									}
								}
							}
						}
					}
					list.Add(x509CertificateEntry);
					if (x509CertificateEntry2 != x509CertificateEntry)
					{
						x509CertificateEntry = x509CertificateEntry2;
					}
					else
					{
						x509CertificateEntry = null;
					}
				}
				X509CertificateEntry[] array = new X509CertificateEntry[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = (X509CertificateEntry)list[i];
				}
				return array;
			}
			return null;
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x00139250 File Offset: 0x00139250
		public void SetCertificateEntry(string alias, X509CertificateEntry certEntry)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (certEntry == null)
			{
				throw new ArgumentNullException("certEntry");
			}
			if (this.keys[alias] != null)
			{
				throw new ArgumentException("There is a key entry with the name " + alias + ".");
			}
			this.certs[alias] = certEntry;
			this.chainCerts[new Pkcs12Store.CertId(certEntry.Certificate.GetPublicKey())] = certEntry;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x001392D4 File Offset: 0x001392D4
		public void SetKeyEntry(string alias, AsymmetricKeyEntry keyEntry, X509CertificateEntry[] chain)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (keyEntry == null)
			{
				throw new ArgumentNullException("keyEntry");
			}
			if (keyEntry.Key.IsPrivate && chain == null)
			{
				throw new ArgumentException("No certificate chain for private key");
			}
			if (this.keys[alias] != null)
			{
				this.DeleteEntry(alias);
			}
			this.keys[alias] = keyEntry;
			this.certs[alias] = chain[0];
			for (int num = 0; num != chain.Length; num++)
			{
				this.chainCerts[new Pkcs12Store.CertId(chain[num].Certificate.GetPublicKey())] = chain[num];
			}
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0013939C File Offset: 0x0013939C
		public void DeleteEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[alias];
			if (asymmetricKeyEntry != null)
			{
				this.keys.Remove(alias);
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry != null)
			{
				this.certs.Remove(alias);
				this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
			}
			if (asymmetricKeyEntry != null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					this.localIds.Remove(alias);
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				if (x509CertificateEntry != null)
				{
					this.keyCerts.Remove(text);
					this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
				}
			}
			if (x509CertificateEntry == null && asymmetricKeyEntry == null)
			{
				throw new ArgumentException("no such entry as " + alias);
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x001394AC File Offset: 0x001394AC
		public bool IsEntryOfType(string alias, Type entryType)
		{
			if (entryType == typeof(X509CertificateEntry))
			{
				return this.IsCertificateEntry(alias);
			}
			return entryType == typeof(AsymmetricKeyEntry) && this.IsKeyEntry(alias) && this.GetCertificate(alias) != null;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x00139504 File Offset: 0x00139504
		[Obsolete("Use 'Count' property instead")]
		public int Size()
		{
			return this.Count;
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x0013950C File Offset: 0x0013950C
		public int Count
		{
			get
			{
				return this.GetAliasesTable().Count;
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x0013951C File Offset: 0x0013951C
		public void Save(Stream stream, char[] password, SecureRandom random)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.keys.Keys)
			{
				string text = (string)obj;
				byte[] array = new byte[20];
				random.NextBytes(array);
				AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[text];
				DerObjectIdentifier oid;
				Asn1Encodable asn1Encodable;
				if (password == null)
				{
					oid = PkcsObjectIdentifiers.KeyBag;
					asn1Encodable = PrivateKeyInfoFactory.CreatePrivateKeyInfo(asymmetricKeyEntry.Key);
				}
				else
				{
					oid = PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag;
					asn1Encodable = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(this.keyAlgorithm, password, array, 1024, asymmetricKeyEntry.Key);
				}
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
				foreach (object obj2 in asymmetricKeyEntry.BagAttributeKeys)
				{
					string text2 = (string)obj2;
					Asn1Encodable element = asymmetricKeyEntry[text2];
					if (!text2.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector2.Add(new DerSequence(new Asn1Encodable[]
						{
							new DerObjectIdentifier(text2),
							new DerSet(element)
						}));
					}
				}
				asn1EncodableVector2.Add(new DerSequence(new Asn1Encodable[]
				{
					PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
					new DerSet(new DerBmpString(text))
				}));
				if (asymmetricKeyEntry[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					X509CertificateEntry certificate = this.GetCertificate(text);
					AsymmetricKeyParameter publicKey = certificate.Certificate.GetPublicKey();
					SubjectKeyIdentifier element2 = Pkcs12Store.CreateSubjectKeyID(publicKey);
					asn1EncodableVector2.Add(new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
						new DerSet(element2)
					}));
				}
				asn1EncodableVector.Add(new SafeBag(oid, asn1Encodable.ToAsn1Object(), new DerSet(asn1EncodableVector2)));
			}
			byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
			ContentInfo contentInfo = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(derEncoded));
			byte[] array2 = new byte[20];
			random.NextBytes(array2);
			Asn1EncodableVector asn1EncodableVector3 = new Asn1EncodableVector();
			Pkcs12PbeParams pkcs12PbeParams = new Pkcs12PbeParams(array2, 1024);
			AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(this.certAlgorithm, pkcs12PbeParams.ToAsn1Object());
			ISet set = new HashSet();
			foreach (object obj3 in this.keys.Keys)
			{
				string text3 = (string)obj3;
				X509CertificateEntry certificate2 = this.GetCertificate(text3);
				CertBag certBag = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(certificate2.Certificate.GetEncoded()));
				Asn1EncodableVector asn1EncodableVector4 = new Asn1EncodableVector();
				foreach (object obj4 in certificate2.BagAttributeKeys)
				{
					string text4 = (string)obj4;
					Asn1Encodable element3 = certificate2[text4];
					if (!text4.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector4.Add(new DerSequence(new Asn1Encodable[]
						{
							new DerObjectIdentifier(text4),
							new DerSet(element3)
						}));
					}
				}
				asn1EncodableVector4.Add(new DerSequence(new Asn1Encodable[]
				{
					PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
					new DerSet(new DerBmpString(text3))
				}));
				if (certificate2[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					AsymmetricKeyParameter publicKey2 = certificate2.Certificate.GetPublicKey();
					SubjectKeyIdentifier element4 = Pkcs12Store.CreateSubjectKeyID(publicKey2);
					asn1EncodableVector4.Add(new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
						new DerSet(element4)
					}));
				}
				asn1EncodableVector3.Add(new SafeBag(PkcsObjectIdentifiers.CertBag, certBag.ToAsn1Object(), new DerSet(asn1EncodableVector4)));
				set.Add(certificate2.Certificate);
			}
			foreach (object obj5 in this.certs.Keys)
			{
				string text5 = (string)obj5;
				X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[text5];
				if (this.keys[text5] == null)
				{
					CertBag certBag2 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector5 = new Asn1EncodableVector();
					foreach (object obj6 in x509CertificateEntry.BagAttributeKeys)
					{
						string text6 = (string)obj6;
						if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							Asn1Encodable element5 = x509CertificateEntry[text6];
							if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
							{
								asn1EncodableVector5.Add(new DerSequence(new Asn1Encodable[]
								{
									new DerObjectIdentifier(text6),
									new DerSet(element5)
								}));
							}
						}
					}
					asn1EncodableVector5.Add(new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
						new DerSet(new DerBmpString(text5))
					}));
					asn1EncodableVector3.Add(new SafeBag(PkcsObjectIdentifiers.CertBag, certBag2.ToAsn1Object(), new DerSet(asn1EncodableVector5)));
					set.Add(x509CertificateEntry.Certificate);
				}
			}
			foreach (object obj7 in this.chainCerts.Keys)
			{
				Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj7;
				X509CertificateEntry x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[key];
				if (!set.Contains(x509CertificateEntry2.Certificate))
				{
					CertBag certBag3 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry2.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector6 = new Asn1EncodableVector();
					foreach (object obj8 in x509CertificateEntry2.BagAttributeKeys)
					{
						string text7 = (string)obj8;
						if (!text7.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							asn1EncodableVector6.Add(new DerSequence(new Asn1Encodable[]
							{
								new DerObjectIdentifier(text7),
								new DerSet(x509CertificateEntry2[text7])
							}));
						}
					}
					asn1EncodableVector3.Add(new SafeBag(PkcsObjectIdentifiers.CertBag, certBag3.ToAsn1Object(), new DerSet(asn1EncodableVector6)));
				}
			}
			byte[] derEncoded2 = new DerSequence(asn1EncodableVector3).GetDerEncoded();
			ContentInfo contentInfo2;
			if (password == null)
			{
				contentInfo2 = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(derEncoded2));
			}
			else
			{
				byte[] str = Pkcs12Store.CryptPbeData(true, algorithmIdentifier, password, false, derEncoded2);
				EncryptedData encryptedData = new EncryptedData(PkcsObjectIdentifiers.Data, algorithmIdentifier, new BerOctetString(str));
				contentInfo2 = new ContentInfo(PkcsObjectIdentifiers.EncryptedData, encryptedData.ToAsn1Object());
			}
			ContentInfo[] info = new ContentInfo[]
			{
				contentInfo,
				contentInfo2
			};
			byte[] encoded = new AuthenticatedSafe(info).GetEncoded(this.useDerEncoding ? "DER" : "BER");
			ContentInfo contentInfo3 = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(encoded));
			MacData macData = null;
			if (password != null)
			{
				byte[] array3 = new byte[20];
				random.NextBytes(array3);
				byte[] digest = Pkcs12Store.CalculatePbeMac(OiwObjectIdentifiers.IdSha1, array3, 1024, password, false, encoded);
				AlgorithmIdentifier algID = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
				DigestInfo digInfo = new DigestInfo(algID, digest);
				macData = new MacData(digInfo, array3, 1024);
			}
			Pfx obj9 = new Pfx(contentInfo3, macData);
			DerOutputStream derOutputStream;
			if (this.useDerEncoding)
			{
				derOutputStream = new DerOutputStream(stream);
			}
			else
			{
				derOutputStream = new BerOutputStream(stream);
			}
			derOutputStream.WriteObject(obj9);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x00139E94 File Offset: 0x00139E94
		internal static byte[] CalculatePbeMac(DerObjectIdentifier oid, byte[] salt, int itCount, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			Asn1Encodable pbeParameters = PbeUtilities.GenerateAlgorithmParameters(oid, salt, itCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(oid, password, wrongPkcs12Zero, pbeParameters);
			IMac mac = (IMac)PbeUtilities.CreateEngine(oid);
			mac.Init(parameters);
			return MacUtilities.DoFinal(mac, data);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x00139ED4 File Offset: 0x00139ED4
		private static byte[] CryptPbeData(bool forEncryption, AlgorithmIdentifier algId, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algId.Algorithm) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + algId.Algorithm);
			}
			Pkcs12PbeParams instance = Pkcs12PbeParams.GetInstance(algId.Parameters);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algId.Algorithm, password, wrongPkcs12Zero, instance);
			bufferedCipher.Init(forEncryption, parameters);
			return bufferedCipher.DoFinal(data);
		}

		// Token: 0x04001E36 RID: 7734
		public const string IgnoreUselessPasswordProperty = "Org.BouncyCastle.Pkcs12.IgnoreUselessPassword";

		// Token: 0x04001E37 RID: 7735
		private const int MinIterations = 1024;

		// Token: 0x04001E38 RID: 7736
		private const int SaltSize = 20;

		// Token: 0x04001E39 RID: 7737
		private readonly Pkcs12Store.IgnoresCaseHashtable keys = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x04001E3A RID: 7738
		private readonly IDictionary localIds = Platform.CreateHashtable();

		// Token: 0x04001E3B RID: 7739
		private readonly Pkcs12Store.IgnoresCaseHashtable certs = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x04001E3C RID: 7740
		private readonly IDictionary chainCerts = Platform.CreateHashtable();

		// Token: 0x04001E3D RID: 7741
		private readonly IDictionary keyCerts = Platform.CreateHashtable();

		// Token: 0x04001E3E RID: 7742
		private readonly DerObjectIdentifier keyAlgorithm;

		// Token: 0x04001E3F RID: 7743
		private readonly DerObjectIdentifier certAlgorithm;

		// Token: 0x04001E40 RID: 7744
		private readonly bool useDerEncoding;

		// Token: 0x04001E41 RID: 7745
		private AsymmetricKeyEntry unmarkedKeyEntry = null;

		// Token: 0x02000E67 RID: 3687
		internal class CertId
		{
			// Token: 0x06008D60 RID: 36192 RVA: 0x002A674C File Offset: 0x002A674C
			internal CertId(AsymmetricKeyParameter pubKey)
			{
				this.id = Pkcs12Store.CreateSubjectKeyID(pubKey).GetKeyIdentifier();
			}

			// Token: 0x06008D61 RID: 36193 RVA: 0x002A6768 File Offset: 0x002A6768
			internal CertId(byte[] id)
			{
				this.id = id;
			}

			// Token: 0x17001DA9 RID: 7593
			// (get) Token: 0x06008D62 RID: 36194 RVA: 0x002A6778 File Offset: 0x002A6778
			internal byte[] Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x06008D63 RID: 36195 RVA: 0x002A6780 File Offset: 0x002A6780
			public override int GetHashCode()
			{
				return Arrays.GetHashCode(this.id);
			}

			// Token: 0x06008D64 RID: 36196 RVA: 0x002A6790 File Offset: 0x002A6790
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				Pkcs12Store.CertId certId = obj as Pkcs12Store.CertId;
				return certId != null && Arrays.AreEqual(this.id, certId.id);
			}

			// Token: 0x04004255 RID: 16981
			private readonly byte[] id;
		}

		// Token: 0x02000E68 RID: 3688
		private class IgnoresCaseHashtable : IEnumerable
		{
			// Token: 0x06008D65 RID: 36197 RVA: 0x002A67CC File Offset: 0x002A67CC
			public void Clear()
			{
				this.orig.Clear();
				this.keys.Clear();
			}

			// Token: 0x06008D66 RID: 36198 RVA: 0x002A67E4 File Offset: 0x002A67E4
			public IEnumerator GetEnumerator()
			{
				return this.orig.GetEnumerator();
			}

			// Token: 0x17001DAA RID: 7594
			// (get) Token: 0x06008D67 RID: 36199 RVA: 0x002A67F4 File Offset: 0x002A67F4
			public ICollection Keys
			{
				get
				{
					return this.orig.Keys;
				}
			}

			// Token: 0x06008D68 RID: 36200 RVA: 0x002A6804 File Offset: 0x002A6804
			public object Remove(string alias)
			{
				string key = Platform.ToUpperInvariant(alias);
				string text = (string)this.keys[key];
				if (text == null)
				{
					return null;
				}
				this.keys.Remove(key);
				object result = this.orig[text];
				this.orig.Remove(text);
				return result;
			}

			// Token: 0x17001DAB RID: 7595
			public object this[string alias]
			{
				get
				{
					string key = Platform.ToUpperInvariant(alias);
					string text = (string)this.keys[key];
					if (text == null)
					{
						return null;
					}
					return this.orig[text];
				}
				set
				{
					string key = Platform.ToUpperInvariant(alias);
					string text = (string)this.keys[key];
					if (text != null)
					{
						this.orig.Remove(text);
					}
					this.keys[key] = alias;
					this.orig[alias] = value;
				}
			}

			// Token: 0x17001DAC RID: 7596
			// (get) Token: 0x06008D6B RID: 36203 RVA: 0x002A68F4 File Offset: 0x002A68F4
			public ICollection Values
			{
				get
				{
					return this.orig.Values;
				}
			}

			// Token: 0x17001DAD RID: 7597
			// (get) Token: 0x06008D6C RID: 36204 RVA: 0x002A6904 File Offset: 0x002A6904
			public int Count
			{
				get
				{
					return this.orig.Count;
				}
			}

			// Token: 0x04004256 RID: 16982
			private readonly IDictionary orig = Platform.CreateHashtable();

			// Token: 0x04004257 RID: 16983
			private readonly IDictionary keys = Platform.CreateHashtable();
		}
	}
}
