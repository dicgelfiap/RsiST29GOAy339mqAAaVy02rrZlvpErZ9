using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000657 RID: 1623
	public class PgpObjectFactory
	{
		// Token: 0x06003848 RID: 14408 RVA: 0x0012E7E8 File Offset: 0x0012E7E8
		public PgpObjectFactory(Stream inputStream)
		{
			this.bcpgIn = BcpgInputStream.Wrap(inputStream);
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0012E7FC File Offset: 0x0012E7FC
		public PgpObjectFactory(byte[] bytes) : this(new MemoryStream(bytes, false))
		{
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0012E80C File Offset: 0x0012E80C
		public PgpObject NextPgpObject()
		{
			PacketTag packetTag = this.bcpgIn.NextPacketTag();
			if (packetTag == (PacketTag)(-1))
			{
				return null;
			}
			PacketTag packetTag2 = packetTag;
			switch (packetTag2)
			{
			case PacketTag.PublicKeyEncryptedSession:
			case PacketTag.SymmetricKeyEncryptedSessionKey:
				return new PgpEncryptedDataList(this.bcpgIn);
			case PacketTag.Signature:
			{
				IList list = Platform.CreateArrayList();
				while (this.bcpgIn.NextPacketTag() == PacketTag.Signature)
				{
					try
					{
						list.Add(new PgpSignature(this.bcpgIn));
					}
					catch (PgpException arg)
					{
						throw new IOException("can't create signature object: " + arg);
					}
				}
				PgpSignature[] array = new PgpSignature[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = (PgpSignature)list[i];
				}
				return new PgpSignatureList(array);
			}
			case PacketTag.OnePassSignature:
			{
				IList list2 = Platform.CreateArrayList();
				while (this.bcpgIn.NextPacketTag() == PacketTag.OnePassSignature)
				{
					try
					{
						list2.Add(new PgpOnePassSignature(this.bcpgIn));
					}
					catch (PgpException arg2)
					{
						throw new IOException("can't create one pass signature object: " + arg2);
					}
				}
				PgpOnePassSignature[] array2 = new PgpOnePassSignature[list2.Count];
				for (int j = 0; j < list2.Count; j++)
				{
					array2[j] = (PgpOnePassSignature)list2[j];
				}
				return new PgpOnePassSignatureList(array2);
			}
			case PacketTag.SecretKey:
				try
				{
					return new PgpSecretKeyRing(this.bcpgIn);
				}
				catch (PgpException arg3)
				{
					throw new IOException("can't create secret key object: " + arg3);
				}
				break;
			case PacketTag.PublicKey:
				break;
			case PacketTag.SecretSubkey:
			case PacketTag.SymmetricKeyEncrypted:
				goto IL_1E8;
			case PacketTag.CompressedData:
				return new PgpCompressedData(this.bcpgIn);
			case PacketTag.Marker:
				return new PgpMarker(this.bcpgIn);
			case PacketTag.LiteralData:
				return new PgpLiteralData(this.bcpgIn);
			default:
				switch (packetTag2)
				{
				case PacketTag.Experimental1:
				case PacketTag.Experimental2:
				case PacketTag.Experimental3:
				case PacketTag.Experimental4:
					return new PgpExperimental(this.bcpgIn);
				default:
					goto IL_1E8;
				}
				break;
			}
			return new PgpPublicKeyRing(this.bcpgIn);
			IL_1E8:
			throw new IOException("unknown object in stream " + this.bcpgIn.NextPacketTag());
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0012EA4C File Offset: 0x0012EA4C
		[Obsolete("Use NextPgpObject() instead")]
		public object NextObject()
		{
			return this.NextPgpObject();
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x0012EA54 File Offset: 0x0012EA54
		public IList AllPgpObjects()
		{
			IList list = Platform.CreateArrayList();
			PgpObject value;
			while ((value = this.NextPgpObject()) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0012EA84 File Offset: 0x0012EA84
		public IList FilterPgpObjects(Type type)
		{
			IList list = Platform.CreateArrayList();
			PgpObject pgpObject;
			while ((pgpObject = this.NextPgpObject()) != null)
			{
				if (type.IsAssignableFrom(pgpObject.GetType()))
				{
					list.Add(pgpObject);
				}
			}
			return list;
		}

		// Token: 0x04001DCD RID: 7629
		private readonly BcpgInputStream bcpgIn;
	}
}
