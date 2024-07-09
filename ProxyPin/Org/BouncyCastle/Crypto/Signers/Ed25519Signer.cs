using System;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049F RID: 1183
	public class Ed25519Signer : ISigner
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x000CAB84 File Offset: 0x000CAB84
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519";
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000CAB8C File Offset: 0x000CAB8C
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed25519PrivateKeyParameters)parameters;
				this.publicKey = null;
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed25519PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000CABCC File Offset: 0x000CABCC
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000CABDC File Offset: 0x000CABDC
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000CABEC File Offset: 0x000CABEC
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519Signer not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000CAC20 File Offset: 0x000CAC20
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519Signer not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, signature);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000CAC58 File Offset: 0x000CAC58
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x040016FE RID: 5886
		private readonly Ed25519Signer.Buffer buffer = new Ed25519Signer.Buffer();

		// Token: 0x040016FF RID: 5887
		private bool forSigning;

		// Token: 0x04001700 RID: 5888
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x04001701 RID: 5889
		private Ed25519PublicKeyParameters publicKey;

		// Token: 0x02000E16 RID: 3606
		private class Buffer : MemoryStream
		{
			// Token: 0x06008C36 RID: 35894 RVA: 0x002A1F38 File Offset: 0x002A1F38
			internal byte[] GenerateSignature(Ed25519PrivateKeyParameters privateKey)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed25519PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed25519.Algorithm.Ed25519, null, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06008C37 RID: 35895 RVA: 0x002A1F9C File Offset: 0x002A1F9C
			internal bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] signature)
			{
				if (Ed25519.SignatureSize != signature.Length)
				{
					return false;
				}
				bool result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int mLen = (int)this.Position;
					byte[] encoded = publicKey.GetEncoded();
					bool flag = Ed25519.Verify(signature, 0, encoded, 0, buffer, 0, mLen);
					this.Reset();
					result = flag;
				}
				return result;
			}

			// Token: 0x06008C38 RID: 35896 RVA: 0x002A200C File Offset: 0x002A200C
			internal void Reset()
			{
				lock (this)
				{
					long position = this.Position;
					Array.Clear(this.GetBuffer(), 0, (int)position);
					this.Position = 0L;
				}
			}
		}
	}
}
