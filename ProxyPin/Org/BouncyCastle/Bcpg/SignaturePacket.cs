using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002BB RID: 699
	public class SignaturePacket : ContainedPacket
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x00071678 File Offset: 0x00071678
		internal SignaturePacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			if (this.version == 3 || this.version == 2)
			{
				bcpgIn.ReadByte();
				this.signatureType = bcpgIn.ReadByte();
				this.creationTime = ((long)bcpgIn.ReadByte() << 24 | (long)bcpgIn.ReadByte() << 16 | (long)bcpgIn.ReadByte() << 8 | (long)((ulong)bcpgIn.ReadByte())) * 1000L;
				this.keyId |= (long)bcpgIn.ReadByte() << 56;
				this.keyId |= (long)bcpgIn.ReadByte() << 48;
				this.keyId |= (long)bcpgIn.ReadByte() << 40;
				this.keyId |= (long)bcpgIn.ReadByte() << 32;
				this.keyId |= (long)bcpgIn.ReadByte() << 24;
				this.keyId |= (long)bcpgIn.ReadByte() << 16;
				this.keyId |= (long)bcpgIn.ReadByte() << 8;
				this.keyId |= (long)((ulong)bcpgIn.ReadByte());
				this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
				this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
			}
			else
			{
				if (this.version != 4)
				{
					throw new Exception("unsupported version: " + this.version);
				}
				this.signatureType = bcpgIn.ReadByte();
				this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
				this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
				int num = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
				byte[] buffer = new byte[num];
				bcpgIn.ReadFully(buffer);
				SignatureSubpacketsParser signatureSubpacketsParser = new SignatureSubpacketsParser(new MemoryStream(buffer, false));
				IList list = Platform.CreateArrayList();
				SignatureSubpacket value;
				while ((value = signatureSubpacketsParser.ReadPacket()) != null)
				{
					list.Add(value);
				}
				this.hashedData = new SignatureSubpacket[list.Count];
				for (int num2 = 0; num2 != this.hashedData.Length; num2++)
				{
					SignatureSubpacket signatureSubpacket = (SignatureSubpacket)list[num2];
					if (signatureSubpacket is IssuerKeyId)
					{
						this.keyId = ((IssuerKeyId)signatureSubpacket).KeyId;
					}
					else if (signatureSubpacket is SignatureCreationTime)
					{
						this.creationTime = DateTimeUtilities.DateTimeToUnixMs(((SignatureCreationTime)signatureSubpacket).GetTime());
					}
					this.hashedData[num2] = signatureSubpacket;
				}
				int num3 = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
				byte[] buffer2 = new byte[num3];
				bcpgIn.ReadFully(buffer2);
				signatureSubpacketsParser = new SignatureSubpacketsParser(new MemoryStream(buffer2, false));
				list.Clear();
				while ((value = signatureSubpacketsParser.ReadPacket()) != null)
				{
					list.Add(value);
				}
				this.unhashedData = new SignatureSubpacket[list.Count];
				for (int num4 = 0; num4 != this.unhashedData.Length; num4++)
				{
					SignatureSubpacket signatureSubpacket2 = (SignatureSubpacket)list[num4];
					if (signatureSubpacket2 is IssuerKeyId)
					{
						this.keyId = ((IssuerKeyId)signatureSubpacket2).KeyId;
					}
					this.unhashedData[num4] = signatureSubpacket2;
				}
			}
			this.fingerprint = new byte[2];
			bcpgIn.ReadFully(this.fingerprint);
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.keyAlgorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaSign:
			{
				MPInteger mpinteger = new MPInteger(bcpgIn);
				this.signature = new MPInteger[]
				{
					mpinteger
				};
				return;
			}
			case PublicKeyAlgorithmTag.RsaEncrypt:
				break;
			default:
				switch (publicKeyAlgorithmTag)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
				{
					MPInteger mpinteger2 = new MPInteger(bcpgIn);
					MPInteger mpinteger3 = new MPInteger(bcpgIn);
					MPInteger mpinteger4 = new MPInteger(bcpgIn);
					this.signature = new MPInteger[]
					{
						mpinteger2,
						mpinteger3,
						mpinteger4
					};
					return;
				}
				case PublicKeyAlgorithmTag.Dsa:
				{
					MPInteger mpinteger5 = new MPInteger(bcpgIn);
					MPInteger mpinteger6 = new MPInteger(bcpgIn);
					this.signature = new MPInteger[]
					{
						mpinteger5,
						mpinteger6
					};
					return;
				}
				case PublicKeyAlgorithmTag.ECDsa:
				{
					MPInteger mpinteger7 = new MPInteger(bcpgIn);
					MPInteger mpinteger8 = new MPInteger(bcpgIn);
					this.signature = new MPInteger[]
					{
						mpinteger7,
						mpinteger8
					};
					return;
				}
				}
				break;
			}
			if (this.keyAlgorithm >= PublicKeyAlgorithmTag.Experimental_1 && this.keyAlgorithm <= PublicKeyAlgorithmTag.Experimental_11)
			{
				this.signature = null;
				MemoryStream memoryStream = new MemoryStream();
				int num5;
				while ((num5 = bcpgIn.ReadByte()) >= 0)
				{
					memoryStream.WriteByte((byte)num5);
				}
				this.signatureEncoding = memoryStream.ToArray();
				return;
			}
			throw new IOException("unknown signature key algorithm: " + this.keyAlgorithm);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00071B44 File Offset: 0x00071B44
		public SignaturePacket(int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, SignatureSubpacket[] hashedData, SignatureSubpacket[] unhashedData, byte[] fingerprint, MPInteger[] signature) : this(4, signatureType, keyId, keyAlgorithm, hashAlgorithm, hashedData, unhashedData, fingerprint, signature)
		{
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00071B6C File Offset: 0x00071B6C
		public SignaturePacket(int version, int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, long creationTime, byte[] fingerprint, MPInteger[] signature) : this(version, signatureType, keyId, keyAlgorithm, hashAlgorithm, null, null, fingerprint, signature)
		{
			this.creationTime = creationTime;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00071B98 File Offset: 0x00071B98
		public SignaturePacket(int version, int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, SignatureSubpacket[] hashedData, SignatureSubpacket[] unhashedData, byte[] fingerprint, MPInteger[] signature)
		{
			this.version = version;
			this.signatureType = signatureType;
			this.keyId = keyId;
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.hashedData = hashedData;
			this.unhashedData = unhashedData;
			this.fingerprint = fingerprint;
			this.signature = signature;
			if (hashedData != null)
			{
				this.setCreationTime();
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00071C04 File Offset: 0x00071C04
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00071C0C File Offset: 0x00071C0C
		public int SignatureType
		{
			get
			{
				return this.signatureType;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00071C14 File Offset: 0x00071C14
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00071C1C File Offset: 0x00071C1C
		public byte[] GetSignatureTrailer()
		{
			byte[] array;
			if (this.version == 3)
			{
				array = new byte[5];
				long num = this.creationTime / 1000L;
				array[0] = (byte)this.signatureType;
				array[1] = (byte)(num >> 24);
				array[2] = (byte)(num >> 16);
				array[3] = (byte)(num >> 8);
				array[4] = (byte)num;
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.WriteByte((byte)this.Version);
				memoryStream.WriteByte((byte)this.SignatureType);
				memoryStream.WriteByte((byte)this.KeyAlgorithm);
				memoryStream.WriteByte((byte)this.HashAlgorithm);
				MemoryStream memoryStream2 = new MemoryStream();
				SignatureSubpacket[] hashedSubPackets = this.GetHashedSubPackets();
				for (int num2 = 0; num2 != hashedSubPackets.Length; num2++)
				{
					hashedSubPackets[num2].Encode(memoryStream2);
				}
				byte[] array2 = memoryStream2.ToArray();
				memoryStream.WriteByte((byte)(array2.Length >> 8));
				memoryStream.WriteByte((byte)array2.Length);
				memoryStream.Write(array2, 0, array2.Length);
				byte[] array3 = memoryStream.ToArray();
				memoryStream.WriteByte((byte)this.Version);
				memoryStream.WriteByte(byte.MaxValue);
				memoryStream.WriteByte((byte)(array3.Length >> 24));
				memoryStream.WriteByte((byte)(array3.Length >> 16));
				memoryStream.WriteByte((byte)(array3.Length >> 8));
				memoryStream.WriteByte((byte)array3.Length);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00071D74 File Offset: 0x00071D74
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.keyAlgorithm;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00071D7C File Offset: 0x00071D7C
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00071D84 File Offset: 0x00071D84
		public MPInteger[] GetSignature()
		{
			return this.signature;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00071D8C File Offset: 0x00071D8C
		public byte[] GetSignatureBytes()
		{
			if (this.signatureEncoding != null)
			{
				return (byte[])this.signatureEncoding.Clone();
			}
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			foreach (MPInteger bcpgObject in this.signature)
			{
				try
				{
					bcpgOutputStream.WriteObject(bcpgObject);
				}
				catch (IOException arg)
				{
					throw new Exception("internal error: " + arg);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00071E1C File Offset: 0x00071E1C
		public SignatureSubpacket[] GetHashedSubPackets()
		{
			return this.hashedData;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00071E24 File Offset: 0x00071E24
		public SignatureSubpacket[] GetUnhashedSubPackets()
		{
			return this.unhashedData;
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00071E2C File Offset: 0x00071E2C
		public long CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00071E34 File Offset: 0x00071E34
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			if (this.version == 3 || this.version == 2)
			{
				bcpgOutputStream.Write(new byte[]
				{
					5,
					(byte)this.signatureType
				});
				bcpgOutputStream.WriteInt((int)(this.creationTime / 1000L));
				bcpgOutputStream.WriteLong(this.keyId);
				bcpgOutputStream.Write(new byte[]
				{
					(byte)this.keyAlgorithm,
					(byte)this.hashAlgorithm
				});
			}
			else
			{
				if (this.version != 4)
				{
					throw new IOException("unknown version: " + this.version);
				}
				bcpgOutputStream.Write(new byte[]
				{
					(byte)this.signatureType,
					(byte)this.keyAlgorithm,
					(byte)this.hashAlgorithm
				});
				SignaturePacket.EncodeLengthAndData(bcpgOutputStream, SignaturePacket.GetEncodedSubpackets(this.hashedData));
				SignaturePacket.EncodeLengthAndData(bcpgOutputStream, SignaturePacket.GetEncodedSubpackets(this.unhashedData));
			}
			bcpgOutputStream.Write(this.fingerprint);
			if (this.signature != null)
			{
				bcpgOutputStream.WriteObjects(this.signature);
			}
			else
			{
				bcpgOutputStream.Write(this.signatureEncoding);
			}
			bcpgOut.WritePacket(PacketTag.Signature, memoryStream.ToArray(), true);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00071F98 File Offset: 0x00071F98
		private static void EncodeLengthAndData(BcpgOutputStream pOut, byte[] data)
		{
			pOut.WriteShort((short)data.Length);
			pOut.Write(data);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00071FAC File Offset: 0x00071FAC
		private static byte[] GetEncodedSubpackets(SignatureSubpacket[] ps)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (SignatureSubpacket signatureSubpacket in ps)
			{
				signatureSubpacket.Encode(memoryStream);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00071FEC File Offset: 0x00071FEC
		private void setCreationTime()
		{
			foreach (SignatureSubpacket signatureSubpacket in this.hashedData)
			{
				if (signatureSubpacket is SignatureCreationTime)
				{
					this.creationTime = DateTimeUtilities.DateTimeToUnixMs(((SignatureCreationTime)signatureSubpacket).GetTime());
					return;
				}
			}
		}

		// Token: 0x04000E9D RID: 3741
		private int version;

		// Token: 0x04000E9E RID: 3742
		private int signatureType;

		// Token: 0x04000E9F RID: 3743
		private long creationTime;

		// Token: 0x04000EA0 RID: 3744
		private long keyId;

		// Token: 0x04000EA1 RID: 3745
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x04000EA2 RID: 3746
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04000EA3 RID: 3747
		private MPInteger[] signature;

		// Token: 0x04000EA4 RID: 3748
		private byte[] fingerprint;

		// Token: 0x04000EA5 RID: 3749
		private SignatureSubpacket[] hashedData;

		// Token: 0x04000EA6 RID: 3750
		private SignatureSubpacket[] unhashedData;

		// Token: 0x04000EA7 RID: 3751
		private byte[] signatureEncoding;
	}
}
