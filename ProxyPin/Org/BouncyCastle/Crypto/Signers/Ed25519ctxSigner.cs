using System;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049D RID: 1181
	public class Ed25519ctxSigner : ISigner
	{
		// Token: 0x06002457 RID: 9303 RVA: 0x000CA8DC File Offset: 0x000CA8DC
		public Ed25519ctxSigner(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x000CA8FC File Offset: 0x000CA8FC
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed25519ctx";
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000CA904 File Offset: 0x000CA904
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

		// Token: 0x0600245A RID: 9306 RVA: 0x000CA944 File Offset: 0x000CA944
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000CA954 File Offset: 0x000CA954
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000CA964 File Offset: 0x000CA964
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed25519ctxSigner not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey, this.context);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000CA9A0 File Offset: 0x000CA9A0
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed25519ctxSigner not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, this.context, signature);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000CA9DC File Offset: 0x000CA9DC
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x040016F4 RID: 5876
		private readonly Ed25519ctxSigner.Buffer buffer = new Ed25519ctxSigner.Buffer();

		// Token: 0x040016F5 RID: 5877
		private readonly byte[] context;

		// Token: 0x040016F6 RID: 5878
		private bool forSigning;

		// Token: 0x040016F7 RID: 5879
		private Ed25519PrivateKeyParameters privateKey;

		// Token: 0x040016F8 RID: 5880
		private Ed25519PublicKeyParameters publicKey;

		// Token: 0x02000E15 RID: 3605
		private class Buffer : MemoryStream
		{
			// Token: 0x06008C32 RID: 35890 RVA: 0x002A1E08 File Offset: 0x002A1E08
			internal byte[] GenerateSignature(Ed25519PrivateKeyParameters privateKey, byte[] ctx)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed25519PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed25519.Algorithm.Ed25519ctx, ctx, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06008C33 RID: 35891 RVA: 0x002A1E6C File Offset: 0x002A1E6C
			internal bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] ctx, byte[] signature)
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
					bool flag = Ed25519.Verify(signature, 0, encoded, 0, ctx, buffer, 0, mLen);
					this.Reset();
					result = flag;
				}
				return result;
			}

			// Token: 0x06008C34 RID: 35892 RVA: 0x002A1EE0 File Offset: 0x002A1EE0
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
