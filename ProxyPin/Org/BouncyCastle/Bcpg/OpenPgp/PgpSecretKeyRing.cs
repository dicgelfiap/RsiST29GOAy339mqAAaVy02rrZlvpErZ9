using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000662 RID: 1634
	public class PgpSecretKeyRing : PgpKeyRing
	{
		// Token: 0x060038FB RID: 14587 RVA: 0x00132C5C File Offset: 0x00132C5C
		internal PgpSecretKeyRing(IList keys) : this(keys, Platform.CreateArrayList())
		{
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x00132C6C File Offset: 0x00132C6C
		private PgpSecretKeyRing(IList keys, IList extraPubKeys)
		{
			this.keys = keys;
			this.extraPubKeys = extraPubKeys;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x00132C84 File Offset: 0x00132C84
		public PgpSecretKeyRing(byte[] encoding) : this(new MemoryStream(encoding))
		{
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00132C94 File Offset: 0x00132C94
		public PgpSecretKeyRing(Stream inputStream)
		{
			this.keys = Platform.CreateArrayList();
			this.extraPubKeys = Platform.CreateArrayList();
			BcpgInputStream bcpgInputStream = BcpgInputStream.Wrap(inputStream);
			PacketTag packetTag = bcpgInputStream.NextPacketTag();
			if (packetTag != PacketTag.SecretKey && packetTag != PacketTag.SecretSubkey)
			{
				string str = "secret key ring doesn't start with secret key tag: tag 0x";
				int num = (int)packetTag;
				throw new IOException(str + num.ToString("X"));
			}
			SecretKeyPacket secretKeyPacket = (SecretKeyPacket)bcpgInputStream.ReadPacket();
			while (bcpgInputStream.NextPacketTag() == PacketTag.Experimental2)
			{
				bcpgInputStream.ReadPacket();
			}
			TrustPacket trustPk = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
			IList keySigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
			IList ids;
			IList idTrusts;
			IList idSigs;
			PgpKeyRing.ReadUserIDs(bcpgInputStream, out ids, out idTrusts, out idSigs);
			this.keys.Add(new PgpSecretKey(secretKeyPacket, new PgpPublicKey(secretKeyPacket.PublicKeyPacket, trustPk, keySigs, ids, idTrusts, idSigs)));
			while (bcpgInputStream.NextPacketTag() == PacketTag.SecretSubkey || bcpgInputStream.NextPacketTag() == PacketTag.PublicSubkey)
			{
				if (bcpgInputStream.NextPacketTag() == PacketTag.SecretSubkey)
				{
					SecretSubkeyPacket secretSubkeyPacket = (SecretSubkeyPacket)bcpgInputStream.ReadPacket();
					while (bcpgInputStream.NextPacketTag() == PacketTag.Experimental2)
					{
						bcpgInputStream.ReadPacket();
					}
					TrustPacket trustPk2 = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
					IList sigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
					this.keys.Add(new PgpSecretKey(secretSubkeyPacket, new PgpPublicKey(secretSubkeyPacket.PublicKeyPacket, trustPk2, sigs)));
				}
				else
				{
					PublicSubkeyPacket publicPk = (PublicSubkeyPacket)bcpgInputStream.ReadPacket();
					TrustPacket trustPk3 = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
					IList sigs2 = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
					this.extraPubKeys.Add(new PgpPublicKey(publicPk, trustPk3, sigs2));
				}
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x00132E1C File Offset: 0x00132E1C
		public PgpPublicKey GetPublicKey()
		{
			return ((PgpSecretKey)this.keys[0]).PublicKey;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00132E34 File Offset: 0x00132E34
		public PgpSecretKey GetSecretKey()
		{
			return (PgpSecretKey)this.keys[0];
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x00132E48 File Offset: 0x00132E48
		public IEnumerable GetSecretKeys()
		{
			return new EnumerableProxy(this.keys);
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00132E58 File Offset: 0x00132E58
		public PgpSecretKey GetSecretKey(long keyId)
		{
			foreach (object obj in this.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				if (keyId == pgpSecretKey.KeyId)
				{
					return pgpSecretKey;
				}
			}
			return null;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x00132ECC File Offset: 0x00132ECC
		public IEnumerable GetExtraPublicKeys()
		{
			return new EnumerableProxy(this.extraPubKeys);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x00132EDC File Offset: 0x00132EDC
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x00132F00 File Offset: 0x00132F00
		public void Encode(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			foreach (object obj in this.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				pgpSecretKey.Encode(outStr);
			}
			foreach (object obj2 in this.extraPubKeys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj2;
				pgpPublicKey.Encode(outStr);
			}
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x00132FC4 File Offset: 0x00132FC4
		public static PgpSecretKeyRing ReplacePublicKeys(PgpSecretKeyRing secretRing, PgpPublicKeyRing publicRing)
		{
			IList list = Platform.CreateArrayList(secretRing.keys.Count);
			foreach (object obj in secretRing.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				PgpPublicKey publicKey = publicRing.GetPublicKey(pgpSecretKey.KeyId);
				list.Add(PgpSecretKey.ReplacePublicKey(pgpSecretKey, publicKey));
			}
			return new PgpSecretKeyRing(list);
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x00133054 File Offset: 0x00133054
		public static PgpSecretKeyRing CopyWithNewPassword(PgpSecretKeyRing ring, char[] oldPassPhrase, char[] newPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			IList list = Platform.CreateArrayList(ring.keys.Count);
			foreach (object obj in ring.GetSecretKeys())
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				if (pgpSecretKey.IsPrivateKeyEmpty)
				{
					list.Add(pgpSecretKey);
				}
				else
				{
					list.Add(PgpSecretKey.CopyWithNewPassword(pgpSecretKey, oldPassPhrase, newPassPhrase, newEncAlgorithm, rand));
				}
			}
			return new PgpSecretKeyRing(list, ring.extraPubKeys);
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x001330F8 File Offset: 0x001330F8
		public static PgpSecretKeyRing InsertSecretKey(PgpSecretKeyRing secRing, PgpSecretKey secKey)
		{
			IList list = Platform.CreateArrayList(secRing.keys);
			bool flag = false;
			bool flag2 = false;
			for (int num = 0; num != list.Count; num++)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)list[num];
				if (pgpSecretKey.KeyId == secKey.KeyId)
				{
					flag = true;
					list[num] = secKey;
				}
				if (pgpSecretKey.IsMasterKey)
				{
					flag2 = true;
				}
			}
			if (!flag)
			{
				if (secKey.IsMasterKey)
				{
					if (flag2)
					{
						throw new ArgumentException("cannot add a master key to a ring that already has one");
					}
					list.Insert(0, secKey);
				}
				else
				{
					list.Add(secKey);
				}
			}
			return new PgpSecretKeyRing(list, secRing.extraPubKeys);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x001331A8 File Offset: 0x001331A8
		public static PgpSecretKeyRing RemoveSecretKey(PgpSecretKeyRing secRing, PgpSecretKey secKey)
		{
			IList list = Platform.CreateArrayList(secRing.keys);
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)list[i];
				if (pgpSecretKey.KeyId == secKey.KeyId)
				{
					flag = true;
					list.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return new PgpSecretKeyRing(list, secRing.extraPubKeys);
		}

		// Token: 0x04001DE8 RID: 7656
		private readonly IList keys;

		// Token: 0x04001DE9 RID: 7657
		private readonly IList extraPubKeys;
	}
}
