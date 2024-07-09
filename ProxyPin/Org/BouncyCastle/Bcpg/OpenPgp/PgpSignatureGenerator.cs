using System;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000665 RID: 1637
	public class PgpSignatureGenerator
	{
		// Token: 0x0600393F RID: 14655 RVA: 0x00133E00 File Offset: 0x00133E00
		public PgpSignatureGenerator(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.dig = DigestUtilities.GetDigest(PgpUtilities.GetDigestName(hashAlgorithm));
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(keyAlgorithm, hashAlgorithm));
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x00133E60 File Offset: 0x00133E60
		public void InitSign(int sigType, PgpPrivateKey key)
		{
			this.InitSign(sigType, key, null);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00133E6C File Offset: 0x00133E6C
		public void InitSign(int sigType, PgpPrivateKey key, SecureRandom random)
		{
			this.privKey = key;
			this.signatureType = sigType;
			try
			{
				ICipherParameters parameters = key.Key;
				if (random != null)
				{
					parameters = new ParametersWithRandom(key.Key, random);
				}
				this.sig.Init(true, parameters);
			}
			catch (InvalidKeyException exception)
			{
				throw new PgpException("invalid key.", exception);
			}
			this.dig.Reset();
			this.lastb = 0;
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x00133EE4 File Offset: 0x00133EE4
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.doUpdateByte(b);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x00133F04 File Offset: 0x00133F04
		private void doCanonicalUpdateByte(byte b)
		{
			if (b == 13)
			{
				this.doUpdateCRLF();
			}
			else if (b == 10)
			{
				if (this.lastb != 13)
				{
					this.doUpdateCRLF();
				}
			}
			else
			{
				this.doUpdateByte(b);
			}
			this.lastb = b;
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x00133F58 File Offset: 0x00133F58
		private void doUpdateCRLF()
		{
			this.doUpdateByte(13);
			this.doUpdateByte(10);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x00133F6C File Offset: 0x00133F6C
		private void doUpdateByte(byte b)
		{
			this.sig.Update(b);
			this.dig.Update(b);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x00133F88 File Offset: 0x00133F88
		public void Update(params byte[] b)
		{
			this.Update(b, 0, b.Length);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00133F98 File Offset: 0x00133F98
		public void Update(byte[] b, int off, int len)
		{
			if (this.signatureType == 1)
			{
				int num = off + len;
				for (int num2 = off; num2 != num; num2++)
				{
					this.doCanonicalUpdateByte(b[num2]);
				}
				return;
			}
			this.sig.BlockUpdate(b, off, len);
			this.dig.BlockUpdate(b, off, len);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x00133FF0 File Offset: 0x00133FF0
		public void SetHashedSubpackets(PgpSignatureSubpacketVector hashedPackets)
		{
			this.hashed = ((hashedPackets == null) ? PgpSignatureGenerator.EmptySignatureSubpackets : hashedPackets.ToSubpacketArray());
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x00134010 File Offset: 0x00134010
		public void SetUnhashedSubpackets(PgpSignatureSubpacketVector unhashedPackets)
		{
			this.unhashed = ((unhashedPackets == null) ? PgpSignatureGenerator.EmptySignatureSubpackets : unhashedPackets.ToSubpacketArray());
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00134030 File Offset: 0x00134030
		public PgpOnePassSignature GenerateOnePassVersion(bool isNested)
		{
			return new PgpOnePassSignature(new OnePassSignaturePacket(this.signatureType, this.hashAlgorithm, this.keyAlgorithm, this.privKey.KeyId, isNested));
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x0013405C File Offset: 0x0013405C
		public PgpSignature Generate()
		{
			SignatureSubpacket[] array = this.hashed;
			SignatureSubpacket[] array2 = this.unhashed;
			if (!this.packetPresent(this.hashed, SignatureSubpacketTag.CreationTime))
			{
				array = this.insertSubpacket(array, new SignatureCreationTime(false, DateTime.UtcNow));
			}
			if (!this.packetPresent(this.hashed, SignatureSubpacketTag.IssuerKeyId) && !this.packetPresent(this.unhashed, SignatureSubpacketTag.IssuerKeyId))
			{
				array2 = this.insertSubpacket(array2, new IssuerKeyId(false, this.privKey.KeyId));
			}
			int num = 4;
			byte[] array4;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					array[num2].Encode(memoryStream);
				}
				byte[] array3 = memoryStream.ToArray();
				MemoryStream memoryStream2 = new MemoryStream(array3.Length + 6);
				memoryStream2.WriteByte((byte)num);
				memoryStream2.WriteByte((byte)this.signatureType);
				memoryStream2.WriteByte((byte)this.keyAlgorithm);
				memoryStream2.WriteByte((byte)this.hashAlgorithm);
				memoryStream2.WriteByte((byte)(array3.Length >> 8));
				memoryStream2.WriteByte((byte)array3.Length);
				memoryStream2.Write(array3, 0, array3.Length);
				array4 = memoryStream2.ToArray();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception encoding hashed data.", exception);
			}
			this.sig.BlockUpdate(array4, 0, array4.Length);
			this.dig.BlockUpdate(array4, 0, array4.Length);
			array4 = new byte[]
			{
				(byte)num,
				byte.MaxValue,
				(byte)(array4.Length >> 24),
				(byte)(array4.Length >> 16),
				(byte)(array4.Length >> 8),
				(byte)array4.Length
			};
			this.sig.BlockUpdate(array4, 0, array4.Length);
			this.dig.BlockUpdate(array4, 0, array4.Length);
			byte[] encoding = this.sig.GenerateSignature();
			byte[] array5 = DigestUtilities.DoFinal(this.dig);
			byte[] fingerprint = new byte[]
			{
				array5[0],
				array5[1]
			};
			MPInteger[] signature = (this.keyAlgorithm == PublicKeyAlgorithmTag.RsaSign || this.keyAlgorithm == PublicKeyAlgorithmTag.RsaGeneral) ? PgpUtilities.RsaSigToMpi(encoding) : PgpUtilities.DsaSigToMpi(encoding);
			return new PgpSignature(new SignaturePacket(this.signatureType, this.privKey.KeyId, this.keyAlgorithm, this.hashAlgorithm, array, array2, fingerprint, signature));
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x001342D0 File Offset: 0x001342D0
		public PgpSignature GenerateCertification(string id, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
			this.UpdateWithIdData(180, Strings.ToUtf8ByteArray(id));
			return this.Generate();
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x001342F0 File Offset: 0x001342F0
		public PgpSignature GenerateCertification(PgpUserAttributeSubpacketVector userAttributes, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (UserAttributeSubpacket userAttributeSubpacket in userAttributes.ToSubpacketArray())
				{
					userAttributeSubpacket.Encode(memoryStream);
				}
				this.UpdateWithIdData(209, memoryStream.ToArray());
			}
			catch (IOException exception)
			{
				throw new PgpException("cannot encode subpacket array", exception);
			}
			return this.Generate();
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x0013436C File Offset: 0x0013436C
		public PgpSignature GenerateCertification(PgpPublicKey masterKey, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(masterKey);
			this.UpdateWithPublicKey(pubKey);
			return this.Generate();
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x00134384 File Offset: 0x00134384
		public PgpSignature GenerateCertification(PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
			return this.Generate();
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x00134394 File Offset: 0x00134394
		private byte[] GetEncodedPublicKey(PgpPublicKey pubKey)
		{
			byte[] encodedContents;
			try
			{
				encodedContents = pubKey.publicPk.GetEncodedContents();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception preparing key.", exception);
			}
			return encodedContents;
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x001343D0 File Offset: 0x001343D0
		private bool packetPresent(SignatureSubpacket[] packets, SignatureSubpacketTag type)
		{
			for (int num = 0; num != packets.Length; num++)
			{
				if (packets[num].SubpacketType == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x00134408 File Offset: 0x00134408
		private SignatureSubpacket[] insertSubpacket(SignatureSubpacket[] packets, SignatureSubpacket subpacket)
		{
			SignatureSubpacket[] array = new SignatureSubpacket[packets.Length + 1];
			array[0] = subpacket;
			packets.CopyTo(array, 1);
			return array;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x00134438 File Offset: 0x00134438
		private void UpdateWithIdData(int header, byte[] idBytes)
		{
			this.Update(new byte[]
			{
				(byte)header,
				(byte)(idBytes.Length >> 24),
				(byte)(idBytes.Length >> 16),
				(byte)(idBytes.Length >> 8),
				(byte)idBytes.Length
			});
			this.Update(idBytes);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x00134488 File Offset: 0x00134488
		private void UpdateWithPublicKey(PgpPublicKey key)
		{
			byte[] encodedPublicKey = this.GetEncodedPublicKey(key);
			this.Update(new byte[]
			{
				153,
				(byte)(encodedPublicKey.Length >> 8),
				(byte)encodedPublicKey.Length
			});
			this.Update(encodedPublicKey);
		}

		// Token: 0x04001DFF RID: 7679
		private static readonly SignatureSubpacket[] EmptySignatureSubpackets = new SignatureSubpacket[0];

		// Token: 0x04001E00 RID: 7680
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x04001E01 RID: 7681
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04001E02 RID: 7682
		private PgpPrivateKey privKey;

		// Token: 0x04001E03 RID: 7683
		private ISigner sig;

		// Token: 0x04001E04 RID: 7684
		private IDigest dig;

		// Token: 0x04001E05 RID: 7685
		private int signatureType;

		// Token: 0x04001E06 RID: 7686
		private byte lastb;

		// Token: 0x04001E07 RID: 7687
		private SignatureSubpacket[] unhashed = PgpSignatureGenerator.EmptySignatureSubpackets;

		// Token: 0x04001E08 RID: 7688
		private SignatureSubpacket[] hashed = PgpSignatureGenerator.EmptySignatureSubpackets;
	}
}
