using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065F RID: 1631
	public class PgpPublicKeyRing : PgpKeyRing
	{
		// Token: 0x060038AA RID: 14506 RVA: 0x00130D3C File Offset: 0x00130D3C
		public PgpPublicKeyRing(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x00130D4C File Offset: 0x00130D4C
		internal PgpPublicKeyRing(IList pubKeys)
		{
			this.keys = pubKeys;
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00130D5C File Offset: 0x00130D5C
		public PgpPublicKeyRing(Stream inputStream)
		{
			this.keys = Platform.CreateArrayList();
			BcpgInputStream bcpgInputStream = BcpgInputStream.Wrap(inputStream);
			PacketTag packetTag = bcpgInputStream.NextPacketTag();
			if (packetTag != PacketTag.PublicKey && packetTag != PacketTag.PublicSubkey)
			{
				string str = "public key ring doesn't start with public key tag: tag 0x";
				int num = (int)packetTag;
				throw new IOException(str + num.ToString("X"));
			}
			PublicKeyPacket publicPk = PgpPublicKeyRing.ReadPublicKeyPacket(bcpgInputStream);
			TrustPacket trustPk = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
			IList keySigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
			IList ids;
			IList idTrusts;
			IList idSigs;
			PgpKeyRing.ReadUserIDs(bcpgInputStream, out ids, out idTrusts, out idSigs);
			this.keys.Add(new PgpPublicKey(publicPk, trustPk, keySigs, ids, idTrusts, idSigs));
			while (bcpgInputStream.NextPacketTag() == PacketTag.PublicSubkey)
			{
				this.keys.Add(PgpPublicKeyRing.ReadSubkey(bcpgInputStream));
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x00130E1C File Offset: 0x00130E1C
		public virtual PgpPublicKey GetPublicKey()
		{
			return (PgpPublicKey)this.keys[0];
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x00130E30 File Offset: 0x00130E30
		public virtual PgpPublicKey GetPublicKey(long keyId)
		{
			foreach (object obj in this.keys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj;
				if (keyId == pgpPublicKey.KeyId)
				{
					return pgpPublicKey;
				}
			}
			return null;
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x00130EA4 File Offset: 0x00130EA4
		public virtual IEnumerable GetPublicKeys()
		{
			return new EnumerableProxy(this.keys);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x00130EB4 File Offset: 0x00130EB4
		public virtual byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00130ED8 File Offset: 0x00130ED8
		public virtual void Encode(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			foreach (object obj in this.keys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj;
				pgpPublicKey.Encode(outStr);
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x00130F4C File Offset: 0x00130F4C
		public static PgpPublicKeyRing InsertPublicKey(PgpPublicKeyRing pubRing, PgpPublicKey pubKey)
		{
			IList list = Platform.CreateArrayList(pubRing.keys);
			bool flag = false;
			bool flag2 = false;
			for (int num = 0; num != list.Count; num++)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)list[num];
				if (pgpPublicKey.KeyId == pubKey.KeyId)
				{
					flag = true;
					list[num] = pubKey;
				}
				if (pgpPublicKey.IsMasterKey)
				{
					flag2 = true;
				}
			}
			if (!flag)
			{
				if (pubKey.IsMasterKey)
				{
					if (flag2)
					{
						throw new ArgumentException("cannot add a master key to a ring that already has one");
					}
					list.Insert(0, pubKey);
				}
				else
				{
					list.Add(pubKey);
				}
			}
			return new PgpPublicKeyRing(list);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00130FF8 File Offset: 0x00130FF8
		public static PgpPublicKeyRing RemovePublicKey(PgpPublicKeyRing pubRing, PgpPublicKey pubKey)
		{
			IList list = Platform.CreateArrayList(pubRing.keys);
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)list[i];
				if (pgpPublicKey.KeyId == pubKey.KeyId)
				{
					flag = true;
					list.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return new PgpPublicKeyRing(list);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x00131060 File Offset: 0x00131060
		internal static PublicKeyPacket ReadPublicKeyPacket(BcpgInputStream bcpgInput)
		{
			Packet packet = bcpgInput.ReadPacket();
			if (!(packet is PublicKeyPacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			return (PublicKeyPacket)packet;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x0013109C File Offset: 0x0013109C
		internal static PgpPublicKey ReadSubkey(BcpgInputStream bcpgInput)
		{
			PublicKeyPacket publicPk = PgpPublicKeyRing.ReadPublicKeyPacket(bcpgInput);
			TrustPacket trustPk = PgpKeyRing.ReadOptionalTrustPacket(bcpgInput);
			IList sigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInput);
			return new PgpPublicKey(publicPk, trustPk, sigs);
		}

		// Token: 0x04001DE3 RID: 7651
		private readonly IList keys;
	}
}
