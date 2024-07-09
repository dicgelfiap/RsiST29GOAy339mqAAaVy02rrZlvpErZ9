using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000664 RID: 1636
	public class PgpSignature
	{
		// Token: 0x0600391B RID: 14619 RVA: 0x00133744 File Offset: 0x00133744
		private static SignaturePacket Cast(Packet packet)
		{
			if (!(packet is SignaturePacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			return (SignaturePacket)packet;
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x00133768 File Offset: 0x00133768
		internal PgpSignature(BcpgInputStream bcpgInput) : this(PgpSignature.Cast(bcpgInput.ReadPacket()))
		{
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x0013377C File Offset: 0x0013377C
		internal PgpSignature(SignaturePacket sigPacket) : this(sigPacket, null)
		{
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x00133788 File Offset: 0x00133788
		internal PgpSignature(SignaturePacket sigPacket, TrustPacket trustPacket)
		{
			if (sigPacket == null)
			{
				throw new ArgumentNullException("sigPacket");
			}
			this.sigPck = sigPacket;
			this.signatureType = this.sigPck.SignatureType;
			this.trustPck = trustPacket;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x001337C0 File Offset: 0x001337C0
		private void GetSig()
		{
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(this.sigPck.KeyAlgorithm, this.sigPck.HashAlgorithm));
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x001337E8 File Offset: 0x001337E8
		public int Version
		{
			get
			{
				return this.sigPck.Version;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x001337F8 File Offset: 0x001337F8
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.sigPck.KeyAlgorithm;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x00133808 File Offset: 0x00133808
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.sigPck.HashAlgorithm;
			}
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x00133818 File Offset: 0x00133818
		public bool IsCertification()
		{
			return PgpSignature.IsCertification(this.SignatureType);
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x00133828 File Offset: 0x00133828
		public void InitVerify(PgpPublicKey pubKey)
		{
			this.lastb = 0;
			if (this.sig == null)
			{
				this.GetSig();
			}
			try
			{
				this.sig.Init(false, pubKey.GetKey());
			}
			catch (InvalidKeyException exception)
			{
				throw new PgpException("invalid key.", exception);
			}
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x00133884 File Offset: 0x00133884
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.sig.Update(b);
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x001338A8 File Offset: 0x001338A8
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
				this.sig.Update(b);
			}
			this.lastb = b;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x00133900 File Offset: 0x00133900
		private void doUpdateCRLF()
		{
			this.sig.Update(13);
			this.sig.Update(10);
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x0013391C File Offset: 0x0013391C
		public void Update(params byte[] bytes)
		{
			this.Update(bytes, 0, bytes.Length);
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x0013392C File Offset: 0x0013392C
		public void Update(byte[] bytes, int off, int length)
		{
			if (this.signatureType == 1)
			{
				int num = off + length;
				for (int num2 = off; num2 != num; num2++)
				{
					this.doCanonicalUpdateByte(bytes[num2]);
				}
				return;
			}
			this.sig.BlockUpdate(bytes, off, length);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00133974 File Offset: 0x00133974
		public bool Verify()
		{
			byte[] signatureTrailer = this.GetSignatureTrailer();
			this.sig.BlockUpdate(signatureTrailer, 0, signatureTrailer.Length);
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x001339B0 File Offset: 0x001339B0
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

		// Token: 0x0600392C RID: 14636 RVA: 0x00133A00 File Offset: 0x00133A00
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

		// Token: 0x0600392D RID: 14637 RVA: 0x00133A48 File Offset: 0x00133A48
		public bool VerifyCertification(PgpUserAttributeSubpacketVector userAttributes, PgpPublicKey key)
		{
			this.UpdateWithPublicKey(key);
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
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x00133AE0 File Offset: 0x00133AE0
		public bool VerifyCertification(string id, PgpPublicKey key)
		{
			this.UpdateWithPublicKey(key);
			this.UpdateWithIdData(180, Strings.ToUtf8ByteArray(id));
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x00133B2C File Offset: 0x00133B2C
		public bool VerifyCertification(PgpPublicKey masterKey, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(masterKey);
			this.UpdateWithPublicKey(pubKey);
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x00133B70 File Offset: 0x00133B70
		public bool VerifyCertification(PgpPublicKey pubKey)
		{
			if (this.SignatureType != 32 && this.SignatureType != 40)
			{
				throw new InvalidOperationException("signature is not a key signature");
			}
			this.UpdateWithPublicKey(pubKey);
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x00133BD0 File Offset: 0x00133BD0
		public int SignatureType
		{
			get
			{
				return this.sigPck.SignatureType;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x00133BE0 File Offset: 0x00133BE0
		public long KeyId
		{
			get
			{
				return this.sigPck.KeyId;
			}
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00133BF0 File Offset: 0x00133BF0
		[Obsolete("Use 'CreationTime' property instead")]
		public DateTime GetCreationTime()
		{
			return this.CreationTime;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x00133BF8 File Offset: 0x00133BF8
		public DateTime CreationTime
		{
			get
			{
				return DateTimeUtilities.UnixMsToDateTime(this.sigPck.CreationTime);
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x00133C0C File Offset: 0x00133C0C
		public byte[] GetSignatureTrailer()
		{
			return this.sigPck.GetSignatureTrailer();
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x00133C1C File Offset: 0x00133C1C
		public bool HasSubpackets
		{
			get
			{
				return this.sigPck.GetHashedSubPackets() != null || this.sigPck.GetUnhashedSubPackets() != null;
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x00133C44 File Offset: 0x00133C44
		public PgpSignatureSubpacketVector GetHashedSubPackets()
		{
			return this.createSubpacketVector(this.sigPck.GetHashedSubPackets());
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x00133C58 File Offset: 0x00133C58
		public PgpSignatureSubpacketVector GetUnhashedSubPackets()
		{
			return this.createSubpacketVector(this.sigPck.GetUnhashedSubPackets());
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x00133C6C File Offset: 0x00133C6C
		private PgpSignatureSubpacketVector createSubpacketVector(SignatureSubpacket[] pcks)
		{
			if (pcks != null)
			{
				return new PgpSignatureSubpacketVector(pcks);
			}
			return null;
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x00133C7C File Offset: 0x00133C7C
		public byte[] GetSignature()
		{
			MPInteger[] signature = this.sigPck.GetSignature();
			if (signature != null)
			{
				if (signature.Length == 1)
				{
					return signature[0].Value.ToByteArrayUnsigned();
				}
				try
				{
					return new DerSequence(new Asn1Encodable[]
					{
						new DerInteger(signature[0].Value),
						new DerInteger(signature[1].Value)
					}).GetEncoded();
				}
				catch (IOException exception)
				{
					throw new PgpException("exception encoding DSA sig.", exception);
				}
			}
			return this.sigPck.GetSignatureBytes();
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x00133D30 File Offset: 0x00133D30
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x00133D54 File Offset: 0x00133D54
		public void Encode(Stream outStream)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStream);
			bcpgOutputStream.WritePacket(this.sigPck);
			if (this.trustPck != null)
			{
				bcpgOutputStream.WritePacket(this.trustPck);
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x00133D90 File Offset: 0x00133D90
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

		// Token: 0x0600393E RID: 14654 RVA: 0x00133DCC File Offset: 0x00133DCC
		public static bool IsCertification(int signatureType)
		{
			switch (signatureType)
			{
			case 16:
			case 17:
			case 18:
			case 19:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x04001DEC RID: 7660
		public const int BinaryDocument = 0;

		// Token: 0x04001DED RID: 7661
		public const int CanonicalTextDocument = 1;

		// Token: 0x04001DEE RID: 7662
		public const int StandAlone = 2;

		// Token: 0x04001DEF RID: 7663
		public const int DefaultCertification = 16;

		// Token: 0x04001DF0 RID: 7664
		public const int NoCertification = 17;

		// Token: 0x04001DF1 RID: 7665
		public const int CasualCertification = 18;

		// Token: 0x04001DF2 RID: 7666
		public const int PositiveCertification = 19;

		// Token: 0x04001DF3 RID: 7667
		public const int SubkeyBinding = 24;

		// Token: 0x04001DF4 RID: 7668
		public const int PrimaryKeyBinding = 25;

		// Token: 0x04001DF5 RID: 7669
		public const int DirectKey = 31;

		// Token: 0x04001DF6 RID: 7670
		public const int KeyRevocation = 32;

		// Token: 0x04001DF7 RID: 7671
		public const int SubkeyRevocation = 40;

		// Token: 0x04001DF8 RID: 7672
		public const int CertificationRevocation = 48;

		// Token: 0x04001DF9 RID: 7673
		public const int Timestamp = 64;

		// Token: 0x04001DFA RID: 7674
		private readonly SignaturePacket sigPck;

		// Token: 0x04001DFB RID: 7675
		private readonly int signatureType;

		// Token: 0x04001DFC RID: 7676
		private readonly TrustPacket trustPck;

		// Token: 0x04001DFD RID: 7677
		private ISigner sig;

		// Token: 0x04001DFE RID: 7678
		private byte lastb;
	}
}
