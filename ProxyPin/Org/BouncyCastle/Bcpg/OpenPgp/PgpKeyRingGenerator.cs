using System;
using System.Collections;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000652 RID: 1618
	public class PgpKeyRingGenerator
	{
		// Token: 0x06003829 RID: 14377 RVA: 0x0012E088 File Offset: 0x0012E088
		[Obsolete("Use version taking an explicit 'useSha1' parameter instead")]
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, passPhrase, false, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x0012E0B0 File Offset: 0x0012E0B0
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, false, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x0012E0D8 File Offset: 0x0012E0D8
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, bool utf8PassPhrase, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, PgpUtilities.EncodePassPhrase(passPhrase, utf8PassPhrase), useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x0012E108 File Offset: 0x0012E108
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, byte[] rawPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand)
		{
			this.keys = Platform.CreateArrayList();
			base..ctor();
			this.certificationLevel = certificationLevel;
			this.masterKey = masterKey;
			this.id = id;
			this.encAlgorithm = encAlgorithm;
			this.rawPassPhrase = rawPassPhrase;
			this.useSha1 = useSha1;
			this.hashedPacketVector = hashedPackets;
			this.unhashedPacketVector = unhashedPackets;
			this.rand = rand;
			this.keys.Add(new PgpSecretKey(certificationLevel, masterKey, id, encAlgorithm, rawPassPhrase, false, useSha1, hashedPackets, unhashedPackets, rand));
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0012E190 File Offset: 0x0012E190
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, hashAlgorithm, false, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0012E1BC File Offset: 0x0012E1BC
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, bool utf8PassPhrase, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, masterKey, id, encAlgorithm, hashAlgorithm, PgpUtilities.EncodePassPhrase(passPhrase, utf8PassPhrase), useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x0012E1EC File Offset: 0x0012E1EC
		public PgpKeyRingGenerator(int certificationLevel, PgpKeyPair masterKey, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, byte[] rawPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand)
		{
			this.keys = Platform.CreateArrayList();
			base..ctor();
			this.certificationLevel = certificationLevel;
			this.masterKey = masterKey;
			this.id = id;
			this.encAlgorithm = encAlgorithm;
			this.rawPassPhrase = rawPassPhrase;
			this.useSha1 = useSha1;
			this.hashedPacketVector = hashedPackets;
			this.unhashedPacketVector = unhashedPackets;
			this.rand = rand;
			this.hashAlgorithm = hashAlgorithm;
			this.keys.Add(new PgpSecretKey(certificationLevel, masterKey, id, encAlgorithm, hashAlgorithm, rawPassPhrase, false, useSha1, hashedPackets, unhashedPackets, rand));
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x0012E280 File Offset: 0x0012E280
		public void AddSubKey(PgpKeyPair keyPair)
		{
			this.AddSubKey(keyPair, this.hashedPacketVector, this.unhashedPacketVector);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x0012E298 File Offset: 0x0012E298
		public void AddSubKey(PgpKeyPair keyPair, HashAlgorithmTag hashAlgorithm)
		{
			this.AddSubKey(keyPair, this.hashedPacketVector, this.unhashedPacketVector, hashAlgorithm);
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x0012E2B0 File Offset: 0x0012E2B0
		public void AddSubKey(PgpKeyPair keyPair, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets)
		{
			try
			{
				PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(this.masterKey.PublicKey.Algorithm, HashAlgorithmTag.Sha1);
				pgpSignatureGenerator.InitSign(24, this.masterKey.PrivateKey);
				pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
				pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
				IList list = Platform.CreateArrayList();
				list.Add(pgpSignatureGenerator.GenerateCertification(this.masterKey.PublicKey, keyPair.PublicKey));
				this.keys.Add(new PgpSecretKey(keyPair.PrivateKey, new PgpPublicKey(keyPair.PublicKey, null, list), this.encAlgorithm, this.rawPassPhrase, false, this.useSha1, this.rand, false));
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception adding subkey: ", exception);
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0012E388 File Offset: 0x0012E388
		public void AddSubKey(PgpKeyPair keyPair, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, HashAlgorithmTag hashAlgorithm)
		{
			try
			{
				PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(this.masterKey.PublicKey.Algorithm, hashAlgorithm);
				pgpSignatureGenerator.InitSign(24, this.masterKey.PrivateKey);
				pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
				pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
				IList list = Platform.CreateArrayList();
				list.Add(pgpSignatureGenerator.GenerateCertification(this.masterKey.PublicKey, keyPair.PublicKey));
				this.keys.Add(new PgpSecretKey(keyPair.PrivateKey, new PgpPublicKey(keyPair.PublicKey, null, list), this.encAlgorithm, this.rawPassPhrase, false, this.useSha1, this.rand, false));
			}
			catch (PgpException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception adding subkey: ", exception);
			}
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0012E464 File Offset: 0x0012E464
		public PgpSecretKeyRing GenerateSecretKeyRing()
		{
			return new PgpSecretKeyRing(this.keys);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x0012E474 File Offset: 0x0012E474
		public PgpPublicKeyRing GeneratePublicKeyRing()
		{
			IList list = Platform.CreateArrayList();
			IEnumerator enumerator = this.keys.GetEnumerator();
			enumerator.MoveNext();
			PgpSecretKey pgpSecretKey = (PgpSecretKey)enumerator.Current;
			list.Add(pgpSecretKey.PublicKey);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				pgpSecretKey = (PgpSecretKey)obj;
				PgpPublicKey pgpPublicKey = new PgpPublicKey(pgpSecretKey.PublicKey);
				pgpPublicKey.publicPk = new PublicSubkeyPacket(pgpPublicKey.Algorithm, pgpPublicKey.CreationTime, pgpPublicKey.publicPk.Key);
				list.Add(pgpPublicKey);
			}
			return new PgpPublicKeyRing(list);
		}

		// Token: 0x04001DB6 RID: 7606
		private IList keys;

		// Token: 0x04001DB7 RID: 7607
		private string id;

		// Token: 0x04001DB8 RID: 7608
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x04001DB9 RID: 7609
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04001DBA RID: 7610
		private int certificationLevel;

		// Token: 0x04001DBB RID: 7611
		private byte[] rawPassPhrase;

		// Token: 0x04001DBC RID: 7612
		private bool useSha1;

		// Token: 0x04001DBD RID: 7613
		private PgpKeyPair masterKey;

		// Token: 0x04001DBE RID: 7614
		private PgpSignatureSubpacketVector hashedPacketVector;

		// Token: 0x04001DBF RID: 7615
		private PgpSignatureSubpacketVector unhashedPacketVector;

		// Token: 0x04001DC0 RID: 7616
		private SecureRandom rand;
	}
}
