using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000658 RID: 1624
	public class PgpOnePassSignature
	{
		// Token: 0x0600384E RID: 14414 RVA: 0x0012EAC4 File Offset: 0x0012EAC4
		private static OnePassSignaturePacket Cast(Packet packet)
		{
			if (!(packet is OnePassSignaturePacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			return (OnePassSignaturePacket)packet;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x0012EAE8 File Offset: 0x0012EAE8
		internal PgpOnePassSignature(BcpgInputStream bcpgInput) : this(PgpOnePassSignature.Cast(bcpgInput.ReadPacket()))
		{
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x0012EAFC File Offset: 0x0012EAFC
		internal PgpOnePassSignature(OnePassSignaturePacket sigPack)
		{
			this.sigPack = sigPack;
			this.signatureType = sigPack.SignatureType;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0012EB18 File Offset: 0x0012EB18
		public void InitVerify(PgpPublicKey pubKey)
		{
			this.lastb = 0;
			try
			{
				this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(this.sigPack.KeyAlgorithm, this.sigPack.HashAlgorithm));
			}
			catch (Exception exception)
			{
				throw new PgpException("can't set up signature object.", exception);
			}
			try
			{
				this.sig.Init(false, pubKey.GetKey());
			}
			catch (InvalidKeyException exception2)
			{
				throw new PgpException("invalid key.", exception2);
			}
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x0012EBA4 File Offset: 0x0012EBA4
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.sig.Update(b);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x0012EBC8 File Offset: 0x0012EBC8
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

		// Token: 0x06003854 RID: 14420 RVA: 0x0012EC20 File Offset: 0x0012EC20
		private void doUpdateCRLF()
		{
			this.sig.Update(13);
			this.sig.Update(10);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0012EC3C File Offset: 0x0012EC3C
		public void Update(byte[] bytes)
		{
			if (this.signatureType == 1)
			{
				for (int num = 0; num != bytes.Length; num++)
				{
					this.doCanonicalUpdateByte(bytes[num]);
				}
				return;
			}
			this.sig.BlockUpdate(bytes, 0, bytes.Length);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0012EC84 File Offset: 0x0012EC84
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

		// Token: 0x06003857 RID: 14423 RVA: 0x0012ECCC File Offset: 0x0012ECCC
		public bool Verify(PgpSignature pgpSig)
		{
			byte[] signatureTrailer = pgpSig.GetSignatureTrailer();
			this.sig.BlockUpdate(signatureTrailer, 0, signatureTrailer.Length);
			return this.sig.VerifySignature(pgpSig.GetSignature());
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x0012ED08 File Offset: 0x0012ED08
		public long KeyId
		{
			get
			{
				return this.sigPack.KeyId;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x0012ED18 File Offset: 0x0012ED18
		public int SignatureType
		{
			get
			{
				return this.sigPack.SignatureType;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x0012ED28 File Offset: 0x0012ED28
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.sigPack.HashAlgorithm;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x0012ED38 File Offset: 0x0012ED38
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.sigPack.KeyAlgorithm;
			}
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x0012ED48 File Offset: 0x0012ED48
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x0012ED6C File Offset: 0x0012ED6C
		public void Encode(Stream outStr)
		{
			BcpgOutputStream.Wrap(outStr).WritePacket(this.sigPack);
		}

		// Token: 0x04001DCE RID: 7630
		private readonly OnePassSignaturePacket sigPack;

		// Token: 0x04001DCF RID: 7631
		private readonly int signatureType;

		// Token: 0x04001DD0 RID: 7632
		private ISigner sig;

		// Token: 0x04001DD1 RID: 7633
		private byte lastb;
	}
}
