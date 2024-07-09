using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065D RID: 1629
	public class PgpPublicKey
	{
		// Token: 0x06003873 RID: 14451 RVA: 0x0012F1D4 File Offset: 0x0012F1D4
		public static byte[] CalculateFingerprint(PublicKeyPacket publicPk)
		{
			IBcpgKey key = publicPk.Key;
			IDigest digest;
			if (publicPk.Version <= 3)
			{
				RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)key;
				try
				{
					digest = DigestUtilities.GetDigest("MD5");
					PgpPublicKey.UpdateDigest(digest, rsaPublicBcpgKey.Modulus);
					PgpPublicKey.UpdateDigest(digest, rsaPublicBcpgKey.PublicExponent);
					goto IL_BD;
				}
				catch (Exception ex)
				{
					throw new PgpException("can't encode key components: " + ex.Message, ex);
				}
			}
			try
			{
				byte[] encodedContents = publicPk.GetEncodedContents();
				digest = DigestUtilities.GetDigest("SHA1");
				digest.Update(153);
				digest.Update((byte)(encodedContents.Length >> 8));
				digest.Update((byte)encodedContents.Length);
				digest.BlockUpdate(encodedContents, 0, encodedContents.Length);
			}
			catch (Exception ex2)
			{
				throw new PgpException("can't encode key components: " + ex2.Message, ex2);
			}
			IL_BD:
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x0012F2C0 File Offset: 0x0012F2C0
		private static void UpdateDigest(IDigest d, BigInteger b)
		{
			byte[] array = b.ToByteArrayUnsigned();
			d.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x0012F2E4 File Offset: 0x0012F2E4
		private void Init()
		{
			IBcpgKey key = this.publicPk.Key;
			this.fingerprint = PgpPublicKey.CalculateFingerprint(this.publicPk);
			if (this.publicPk.Version <= 3)
			{
				RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)key;
				this.keyId = rsaPublicBcpgKey.Modulus.LongValue;
				this.keyStrength = rsaPublicBcpgKey.Modulus.BitLength;
				return;
			}
			this.keyId = (long)((ulong)this.fingerprint[this.fingerprint.Length - 8] << 56 | (ulong)this.fingerprint[this.fingerprint.Length - 7] << 48 | (ulong)this.fingerprint[this.fingerprint.Length - 6] << 40 | (ulong)this.fingerprint[this.fingerprint.Length - 5] << 32 | (ulong)this.fingerprint[this.fingerprint.Length - 4] << 24 | (ulong)this.fingerprint[this.fingerprint.Length - 3] << 16 | (ulong)this.fingerprint[this.fingerprint.Length - 2] << 8 | (ulong)this.fingerprint[this.fingerprint.Length - 1]);
			if (key is RsaPublicBcpgKey)
			{
				this.keyStrength = ((RsaPublicBcpgKey)key).Modulus.BitLength;
				return;
			}
			if (key is DsaPublicBcpgKey)
			{
				this.keyStrength = ((DsaPublicBcpgKey)key).P.BitLength;
				return;
			}
			if (key is ElGamalPublicBcpgKey)
			{
				this.keyStrength = ((ElGamalPublicBcpgKey)key).P.BitLength;
				return;
			}
			if (key is ECPublicBcpgKey)
			{
				this.keyStrength = ECKeyPairGenerator.FindECCurveByOid(((ECPublicBcpgKey)key).CurveOid).Curve.FieldSize;
			}
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x0012F490 File Offset: 0x0012F490
		public PgpPublicKey(PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, DateTime time)
		{
			if (pubKey.IsPrivate)
			{
				throw new ArgumentException("Expected a public key", "pubKey");
			}
			IBcpgKey key;
			if (pubKey is RsaKeyParameters)
			{
				RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)pubKey;
				key = new RsaPublicBcpgKey(rsaKeyParameters.Modulus, rsaKeyParameters.Exponent);
			}
			else if (pubKey is DsaPublicKeyParameters)
			{
				DsaPublicKeyParameters dsaPublicKeyParameters = (DsaPublicKeyParameters)pubKey;
				DsaParameters parameters = dsaPublicKeyParameters.Parameters;
				key = new DsaPublicBcpgKey(parameters.P, parameters.Q, parameters.G, dsaPublicKeyParameters.Y);
			}
			else if (pubKey is ECPublicKeyParameters)
			{
				ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
				if (algorithm == PublicKeyAlgorithmTag.EC)
				{
					key = new ECDHPublicBcpgKey(ecpublicKeyParameters.PublicKeyParamSet, ecpublicKeyParameters.Q, HashAlgorithmTag.Sha256, SymmetricKeyAlgorithmTag.Aes128);
				}
				else
				{
					if (algorithm != PublicKeyAlgorithmTag.ECDsa)
					{
						throw new PgpException("unknown EC algorithm");
					}
					key = new ECDsaPublicBcpgKey(ecpublicKeyParameters.PublicKeyParamSet, ecpublicKeyParameters.Q);
				}
			}
			else
			{
				if (!(pubKey is ElGamalPublicKeyParameters))
				{
					throw new PgpException("unknown key class");
				}
				ElGamalPublicKeyParameters elGamalPublicKeyParameters = (ElGamalPublicKeyParameters)pubKey;
				ElGamalParameters parameters2 = elGamalPublicKeyParameters.Parameters;
				key = new ElGamalPublicBcpgKey(parameters2.P, parameters2.G, elGamalPublicKeyParameters.Y);
			}
			this.publicPk = new PublicKeyPacket(algorithm, time, key);
			this.ids = Platform.CreateArrayList();
			this.idSigs = Platform.CreateArrayList();
			try
			{
				this.Init();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception calculating keyId", exception);
			}
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x0012F648 File Offset: 0x0012F648
		public PgpPublicKey(PublicKeyPacket publicPk) : this(publicPk, Platform.CreateArrayList(), Platform.CreateArrayList())
		{
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x0012F65C File Offset: 0x0012F65C
		internal PgpPublicKey(PublicKeyPacket publicPk, TrustPacket trustPk, IList sigs)
		{
			this.publicPk = publicPk;
			this.trustPk = trustPk;
			this.subSigs = sigs;
			this.Init();
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0012F6BC File Offset: 0x0012F6BC
		internal PgpPublicKey(PgpPublicKey key, TrustPacket trust, IList subSigs)
		{
			this.publicPk = key.publicPk;
			this.trustPk = trust;
			this.subSigs = subSigs;
			this.fingerprint = key.fingerprint;
			this.keyId = key.keyId;
			this.keyStrength = key.keyStrength;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x0012F740 File Offset: 0x0012F740
		internal PgpPublicKey(PgpPublicKey pubKey)
		{
			this.publicPk = pubKey.publicPk;
			this.keySigs = Platform.CreateArrayList(pubKey.keySigs);
			this.ids = Platform.CreateArrayList(pubKey.ids);
			this.idTrusts = Platform.CreateArrayList(pubKey.idTrusts);
			this.idSigs = Platform.CreateArrayList(pubKey.idSigs.Count);
			for (int num = 0; num != pubKey.idSigs.Count; num++)
			{
				this.idSigs.Add(Platform.CreateArrayList((IList)pubKey.idSigs[num]));
			}
			if (pubKey.subSigs != null)
			{
				this.subSigs = Platform.CreateArrayList(pubKey.subSigs.Count);
				for (int num2 = 0; num2 != pubKey.subSigs.Count; num2++)
				{
					this.subSigs.Add(pubKey.subSigs[num2]);
				}
			}
			this.fingerprint = pubKey.fingerprint;
			this.keyId = pubKey.keyId;
			this.keyStrength = pubKey.keyStrength;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x0012F88C File Offset: 0x0012F88C
		internal PgpPublicKey(PublicKeyPacket publicPk, TrustPacket trustPk, IList keySigs, IList ids, IList idTrusts, IList idSigs)
		{
			this.publicPk = publicPk;
			this.trustPk = trustPk;
			this.keySigs = keySigs;
			this.ids = ids;
			this.idTrusts = idTrusts;
			this.idSigs = idSigs;
			this.Init();
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x0012F904 File Offset: 0x0012F904
		internal PgpPublicKey(PublicKeyPacket publicPk, IList ids, IList idSigs)
		{
			this.publicPk = publicPk;
			this.ids = ids;
			this.idSigs = idSigs;
			this.Init();
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x0012F964 File Offset: 0x0012F964
		public int Version
		{
			get
			{
				return this.publicPk.Version;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x0012F974 File Offset: 0x0012F974
		public DateTime CreationTime
		{
			get
			{
				return this.publicPk.GetTime();
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x0012F984 File Offset: 0x0012F984
		[Obsolete("Use 'GetValidSeconds' instead")]
		public int ValidDays
		{
			get
			{
				if (this.publicPk.Version <= 3)
				{
					return this.publicPk.ValidDays;
				}
				long validSeconds = this.GetValidSeconds();
				if (validSeconds <= 0L)
				{
					return 0;
				}
				int val = (int)(validSeconds / 86400L);
				return Math.Max(1, val);
			}
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x0012F9D4 File Offset: 0x0012F9D4
		public byte[] GetTrustData()
		{
			if (this.trustPk == null)
			{
				return null;
			}
			return Arrays.Clone(this.trustPk.GetLevelAndTrustAmount());
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x0012F9F4 File Offset: 0x0012F9F4
		public long GetValidSeconds()
		{
			if (this.publicPk.Version <= 3)
			{
				return (long)this.publicPk.ValidDays * 86400L;
			}
			if (this.IsMasterKey)
			{
				for (int num = 0; num != PgpPublicKey.MasterKeyCertificationTypes.Length; num++)
				{
					long expirationTimeFromSig = this.GetExpirationTimeFromSig(true, PgpPublicKey.MasterKeyCertificationTypes[num]);
					if (expirationTimeFromSig >= 0L)
					{
						return expirationTimeFromSig;
					}
				}
			}
			else
			{
				long expirationTimeFromSig2 = this.GetExpirationTimeFromSig(false, 24);
				if (expirationTimeFromSig2 >= 0L)
				{
					return expirationTimeFromSig2;
				}
			}
			return 0L;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x0012FA80 File Offset: 0x0012FA80
		private long GetExpirationTimeFromSig(bool selfSigned, int signatureType)
		{
			long num = -1L;
			long num2 = -1L;
			foreach (object obj in this.GetSignaturesOfType(signatureType))
			{
				PgpSignature pgpSignature = (PgpSignature)obj;
				if (!selfSigned || pgpSignature.KeyId == this.KeyId)
				{
					PgpSignatureSubpacketVector hashedSubPackets = pgpSignature.GetHashedSubPackets();
					if (hashedSubPackets != null)
					{
						long keyExpirationTime = hashedSubPackets.GetKeyExpirationTime();
						if (pgpSignature.KeyId == this.KeyId)
						{
							if (pgpSignature.CreationTime.Ticks > num2)
							{
								num2 = pgpSignature.CreationTime.Ticks;
								num = keyExpirationTime;
							}
						}
						else if (keyExpirationTime == 0L || keyExpirationTime > num)
						{
							num = keyExpirationTime;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x0012FB6C File Offset: 0x0012FB6C
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x0012FB74 File Offset: 0x0012FB74
		public byte[] GetFingerprint()
		{
			return (byte[])this.fingerprint.Clone();
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x0012FB88 File Offset: 0x0012FB88
		public bool IsEncryptionKey
		{
			get
			{
				PublicKeyAlgorithmTag algorithm = this.publicPk.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					break;
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.EC:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
						return true;
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x0012FBDC File Offset: 0x0012FBDC
		public bool IsMasterKey
		{
			get
			{
				return this.subSigs == null;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x0012FBE8 File Offset: 0x0012FBE8
		public PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.publicPk.Algorithm;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x0012FBF8 File Offset: 0x0012FBF8
		public int BitStrength
		{
			get
			{
				return this.keyStrength;
			}
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x0012FC00 File Offset: 0x0012FC00
		public AsymmetricKeyParameter GetKey()
		{
			AsymmetricKeyParameter result;
			try
			{
				PublicKeyAlgorithmTag algorithm = this.publicPk.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
				case PublicKeyAlgorithmTag.RsaSign:
				{
					RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)this.publicPk.Key;
					result = new RsaKeyParameters(false, rsaPublicBcpgKey.Modulus, rsaPublicBcpgKey.PublicExponent);
					break;
				}
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
					{
						ElGamalPublicBcpgKey elGamalPublicBcpgKey = (ElGamalPublicBcpgKey)this.publicPk.Key;
						result = new ElGamalPublicKeyParameters(elGamalPublicBcpgKey.Y, new ElGamalParameters(elGamalPublicBcpgKey.P, elGamalPublicBcpgKey.G));
						break;
					}
					case PublicKeyAlgorithmTag.Dsa:
					{
						DsaPublicBcpgKey dsaPublicBcpgKey = (DsaPublicBcpgKey)this.publicPk.Key;
						result = new DsaPublicKeyParameters(dsaPublicBcpgKey.Y, new DsaParameters(dsaPublicBcpgKey.P, dsaPublicBcpgKey.Q, dsaPublicBcpgKey.G));
						break;
					}
					case PublicKeyAlgorithmTag.EC:
						result = this.GetECKey("ECDH");
						break;
					case PublicKeyAlgorithmTag.ECDsa:
						result = this.GetECKey("ECDSA");
						break;
					default:
						throw new PgpException("unknown public key algorithm encountered");
					}
					break;
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception constructing public key", exception);
			}
			return result;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0012FD60 File Offset: 0x0012FD60
		private ECPublicKeyParameters GetECKey(string algorithm)
		{
			ECPublicBcpgKey ecpublicBcpgKey = (ECPublicBcpgKey)this.publicPk.Key;
			X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(ecpublicBcpgKey.CurveOid);
			ECPoint q = x9ECParameters.Curve.DecodePoint(BigIntegers.AsUnsignedByteArray(ecpublicBcpgKey.EncodedPoint));
			return new ECPublicKeyParameters(algorithm, q, ecpublicBcpgKey.CurveOid);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x0012FDB4 File Offset: 0x0012FDB4
		public IEnumerable GetUserIds()
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.ids)
			{
				if (obj is string)
				{
					list.Add(obj);
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x0012FE2C File Offset: 0x0012FE2C
		public IEnumerable GetUserAttributes()
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.ids)
			{
				if (obj is PgpUserAttributeSubpacketVector)
				{
					list.Add(obj);
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0012FEA4 File Offset: 0x0012FEA4
		public IEnumerable GetSignaturesForId(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			for (int num = 0; num != this.ids.Count; num++)
			{
				if (id.Equals(this.ids[num]))
				{
					return new EnumerableProxy((IList)this.idSigs[num]);
				}
			}
			return null;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0012FF10 File Offset: 0x0012FF10
		public IEnumerable GetSignaturesForUserAttribute(PgpUserAttributeSubpacketVector userAttributes)
		{
			for (int num = 0; num != this.ids.Count; num++)
			{
				if (userAttributes.Equals(this.ids[num]))
				{
					return new EnumerableProxy((IList)this.idSigs[num]);
				}
			}
			return null;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0012FF6C File Offset: 0x0012FF6C
		public IEnumerable GetSignaturesOfType(int signatureType)
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.GetSignatures())
			{
				PgpSignature pgpSignature = (PgpSignature)obj;
				if (pgpSignature.SignatureType == signatureType)
				{
					list.Add(pgpSignature);
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x0012FFE8 File Offset: 0x0012FFE8
		public IEnumerable GetSignatures()
		{
			IList list = this.subSigs;
			if (list == null)
			{
				list = Platform.CreateArrayList(this.keySigs);
				foreach (object obj in this.idSigs)
				{
					ICollection range = (ICollection)obj;
					CollectionUtilities.AddRange(list, range);
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0013006C File Offset: 0x0013006C
		public IEnumerable GetKeySignatures()
		{
			IList list = this.subSigs;
			if (list == null)
			{
				list = Platform.CreateArrayList(this.keySigs);
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x0013009C File Offset: 0x0013009C
		public PublicKeyPacket PublicKeyPacket
		{
			get
			{
				return this.publicPk;
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x001300A4 File Offset: 0x001300A4
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x001300C8 File Offset: 0x001300C8
		public void Encode(Stream outStr)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStr);
			bcpgOutputStream.WritePacket(this.publicPk);
			if (this.trustPk != null)
			{
				bcpgOutputStream.WritePacket(this.trustPk);
			}
			if (this.subSigs == null)
			{
				foreach (object obj in this.keySigs)
				{
					PgpSignature pgpSignature = (PgpSignature)obj;
					pgpSignature.Encode(bcpgOutputStream);
				}
				for (int num = 0; num != this.ids.Count; num++)
				{
					if (this.ids[num] is string)
					{
						string id = (string)this.ids[num];
						bcpgOutputStream.WritePacket(new UserIdPacket(id));
					}
					else
					{
						PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = (PgpUserAttributeSubpacketVector)this.ids[num];
						bcpgOutputStream.WritePacket(new UserAttributePacket(pgpUserAttributeSubpacketVector.ToSubpacketArray()));
					}
					if (this.idTrusts[num] != null)
					{
						bcpgOutputStream.WritePacket((ContainedPacket)this.idTrusts[num]);
					}
					foreach (object obj2 in ((IList)this.idSigs[num]))
					{
						PgpSignature pgpSignature2 = (PgpSignature)obj2;
						pgpSignature2.Encode(bcpgOutputStream);
					}
				}
				return;
			}
			foreach (object obj3 in this.subSigs)
			{
				PgpSignature pgpSignature3 = (PgpSignature)obj3;
				pgpSignature3.Encode(bcpgOutputStream);
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x001302C0 File Offset: 0x001302C0
		public bool IsRevoked()
		{
			int num = 0;
			bool flag = false;
			if (this.IsMasterKey)
			{
				while (!flag)
				{
					if (num >= this.keySigs.Count)
					{
						break;
					}
					if (((PgpSignature)this.keySigs[num++]).SignatureType == 32)
					{
						flag = true;
					}
				}
			}
			else
			{
				while (!flag && num < this.subSigs.Count)
				{
					if (((PgpSignature)this.subSigs[num++]).SignatureType == 40)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x0013035C File Offset: 0x0013035C
		public static PgpPublicKey AddCertification(PgpPublicKey key, string id, PgpSignature certification)
		{
			return PgpPublicKey.AddCert(key, id, certification);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x00130368 File Offset: 0x00130368
		public static PgpPublicKey AddCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes, PgpSignature certification)
		{
			return PgpPublicKey.AddCert(key, userAttributes, certification);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x00130374 File Offset: 0x00130374
		private static PgpPublicKey AddCert(PgpPublicKey key, object id, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			IList list = null;
			for (int num = 0; num != pgpPublicKey.ids.Count; num++)
			{
				if (id.Equals(pgpPublicKey.ids[num]))
				{
					list = (IList)pgpPublicKey.idSigs[num];
				}
			}
			if (list != null)
			{
				list.Add(certification);
			}
			else
			{
				list = Platform.CreateArrayList();
				list.Add(certification);
				pgpPublicKey.ids.Add(id);
				pgpPublicKey.idTrusts.Add(null);
				pgpPublicKey.idSigs.Add(list);
			}
			return pgpPublicKey;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0013041C File Offset: 0x0013041C
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes)
		{
			return PgpPublicKey.RemoveCert(key, userAttributes);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00130428 File Offset: 0x00130428
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, string id)
		{
			return PgpPublicKey.RemoveCert(key, id);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00130434 File Offset: 0x00130434
		private static PgpPublicKey RemoveCert(PgpPublicKey key, object id)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			bool flag = false;
			for (int i = 0; i < pgpPublicKey.ids.Count; i++)
			{
				if (id.Equals(pgpPublicKey.ids[i]))
				{
					flag = true;
					pgpPublicKey.ids.RemoveAt(i);
					pgpPublicKey.idTrusts.RemoveAt(i);
					pgpPublicKey.idSigs.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return pgpPublicKey;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x001304B0 File Offset: 0x001304B0
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, string id, PgpSignature certification)
		{
			return PgpPublicKey.RemoveCert(key, id, certification);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x001304BC File Offset: 0x001304BC
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpUserAttributeSubpacketVector userAttributes, PgpSignature certification)
		{
			return PgpPublicKey.RemoveCert(key, userAttributes, certification);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x001304C8 File Offset: 0x001304C8
		private static PgpPublicKey RemoveCert(PgpPublicKey key, object id, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			bool flag = false;
			for (int i = 0; i < pgpPublicKey.ids.Count; i++)
			{
				if (id.Equals(pgpPublicKey.ids[i]))
				{
					IList list = (IList)pgpPublicKey.idSigs[i];
					flag = list.Contains(certification);
					if (flag)
					{
						list.Remove(certification);
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return pgpPublicKey;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x00130544 File Offset: 0x00130544
		public static PgpPublicKey AddCertification(PgpPublicKey key, PgpSignature certification)
		{
			if (key.IsMasterKey)
			{
				if (certification.SignatureType == 40)
				{
					throw new ArgumentException("signature type incorrect for master key revocation.");
				}
			}
			else if (certification.SignatureType == 32)
			{
				throw new ArgumentException("signature type incorrect for sub-key revocation.");
			}
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			if (pgpPublicKey.subSigs != null)
			{
				pgpPublicKey.subSigs.Add(certification);
			}
			else
			{
				pgpPublicKey.keySigs.Add(certification);
			}
			return pgpPublicKey;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x001305C4 File Offset: 0x001305C4
		public static PgpPublicKey RemoveCertification(PgpPublicKey key, PgpSignature certification)
		{
			PgpPublicKey pgpPublicKey = new PgpPublicKey(key);
			IList list = (pgpPublicKey.subSigs != null) ? pgpPublicKey.subSigs : pgpPublicKey.keySigs;
			int num = list.IndexOf(certification);
			bool flag = num >= 0;
			if (flag)
			{
				list.RemoveAt(num);
			}
			else
			{
				foreach (object obj in key.GetUserIds())
				{
					string id = (string)obj;
					foreach (object obj2 in key.GetSignaturesForId(id))
					{
						if (certification == obj2)
						{
							flag = true;
							pgpPublicKey = PgpPublicKey.RemoveCertification(pgpPublicKey, id, certification);
						}
					}
				}
				if (!flag)
				{
					foreach (object obj3 in key.GetUserAttributes())
					{
						PgpUserAttributeSubpacketVector userAttributes = (PgpUserAttributeSubpacketVector)obj3;
						foreach (object obj4 in key.GetSignaturesForUserAttribute(userAttributes))
						{
							if (certification == obj4)
							{
								flag = true;
								pgpPublicKey = PgpPublicKey.RemoveCertification(pgpPublicKey, userAttributes, certification);
							}
						}
					}
				}
			}
			return pgpPublicKey;
		}

		// Token: 0x04001DD7 RID: 7639
		private static readonly int[] MasterKeyCertificationTypes = new int[]
		{
			19,
			18,
			17,
			16
		};

		// Token: 0x04001DD8 RID: 7640
		private long keyId;

		// Token: 0x04001DD9 RID: 7641
		private byte[] fingerprint;

		// Token: 0x04001DDA RID: 7642
		private int keyStrength;

		// Token: 0x04001DDB RID: 7643
		internal PublicKeyPacket publicPk;

		// Token: 0x04001DDC RID: 7644
		internal TrustPacket trustPk;

		// Token: 0x04001DDD RID: 7645
		internal IList keySigs = Platform.CreateArrayList();

		// Token: 0x04001DDE RID: 7646
		internal IList ids = Platform.CreateArrayList();

		// Token: 0x04001DDF RID: 7647
		internal IList idTrusts = Platform.CreateArrayList();

		// Token: 0x04001DE0 RID: 7648
		internal IList idSigs = Platform.CreateArrayList();

		// Token: 0x04001DE1 RID: 7649
		internal IList subSigs;
	}
}
