using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066C RID: 1644
	public class PgpV3SignatureGenerator
	{
		// Token: 0x060039A7 RID: 14759 RVA: 0x001357C8 File Offset: 0x001357C8
		public PgpV3SignatureGenerator(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.dig = DigestUtilities.GetDigest(PgpUtilities.GetDigestName(hashAlgorithm));
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(keyAlgorithm, hashAlgorithm));
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x00135804 File Offset: 0x00135804
		public void InitSign(int sigType, PgpPrivateKey key)
		{
			this.InitSign(sigType, key, null);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x00135810 File Offset: 0x00135810
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

		// Token: 0x060039AA RID: 14762 RVA: 0x00135888 File Offset: 0x00135888
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.doUpdateByte(b);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x001358A8 File Offset: 0x001358A8
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

		// Token: 0x060039AC RID: 14764 RVA: 0x001358FC File Offset: 0x001358FC
		private void doUpdateCRLF()
		{
			this.doUpdateByte(13);
			this.doUpdateByte(10);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x00135910 File Offset: 0x00135910
		private void doUpdateByte(byte b)
		{
			this.sig.Update(b);
			this.dig.Update(b);
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x0013592C File Offset: 0x0013592C
		public void Update(byte[] b)
		{
			if (this.signatureType == 1)
			{
				for (int num = 0; num != b.Length; num++)
				{
					this.doCanonicalUpdateByte(b[num]);
				}
				return;
			}
			this.sig.BlockUpdate(b, 0, b.Length);
			this.dig.BlockUpdate(b, 0, b.Length);
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x00135984 File Offset: 0x00135984
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

		// Token: 0x060039B0 RID: 14768 RVA: 0x001359DC File Offset: 0x001359DC
		public PgpOnePassSignature GenerateOnePassVersion(bool isNested)
		{
			return new PgpOnePassSignature(new OnePassSignaturePacket(this.signatureType, this.hashAlgorithm, this.keyAlgorithm, this.privKey.KeyId, isNested));
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x00135A08 File Offset: 0x00135A08
		public PgpSignature Generate()
		{
			long num = DateTimeUtilities.CurrentUnixMs() / 1000L;
			byte[] array = new byte[]
			{
				(byte)this.signatureType,
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			};
			this.sig.BlockUpdate(array, 0, array.Length);
			this.dig.BlockUpdate(array, 0, array.Length);
			byte[] encoding = this.sig.GenerateSignature();
			byte[] array2 = DigestUtilities.DoFinal(this.dig);
			byte[] fingerprint = new byte[]
			{
				array2[0],
				array2[1]
			};
			MPInteger[] signature = (this.keyAlgorithm == PublicKeyAlgorithmTag.RsaSign || this.keyAlgorithm == PublicKeyAlgorithmTag.RsaGeneral) ? PgpUtilities.RsaSigToMpi(encoding) : PgpUtilities.DsaSigToMpi(encoding);
			return new PgpSignature(new SignaturePacket(3, this.signatureType, this.privKey.KeyId, this.keyAlgorithm, this.hashAlgorithm, num * 1000L, fingerprint, signature));
		}

		// Token: 0x04001E0F RID: 7695
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x04001E10 RID: 7696
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04001E11 RID: 7697
		private PgpPrivateKey privKey;

		// Token: 0x04001E12 RID: 7698
		private ISigner sig;

		// Token: 0x04001E13 RID: 7699
		private IDigest dig;

		// Token: 0x04001E14 RID: 7700
		private int signatureType;

		// Token: 0x04001E15 RID: 7701
		private byte lastb;
	}
}
