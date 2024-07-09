using System;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A1 RID: 1185
	public class Ed448Signer : ISigner
	{
		// Token: 0x06002477 RID: 9335 RVA: 0x000CADF0 File Offset: 0x000CADF0
		public Ed448Signer(byte[] context)
		{
			this.context = Arrays.Clone(context);
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000CAE10 File Offset: 0x000CAE10
		public virtual string AlgorithmName
		{
			get
			{
				return "Ed448";
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000CAE18 File Offset: 0x000CAE18
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				this.privateKey = (Ed448PrivateKeyParameters)parameters;
				this.publicKey = null;
			}
			else
			{
				this.privateKey = null;
				this.publicKey = (Ed448PublicKeyParameters)parameters;
			}
			this.Reset();
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000CAE58 File Offset: 0x000CAE58
		public virtual void Update(byte b)
		{
			this.buffer.WriteByte(b);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000CAE68 File Offset: 0x000CAE68
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.buffer.Write(buf, off, len);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000CAE78 File Offset: 0x000CAE78
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning || this.privateKey == null)
			{
				throw new InvalidOperationException("Ed448Signer not initialised for signature generation.");
			}
			return this.buffer.GenerateSignature(this.privateKey, this.context);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000CAEB4 File Offset: 0x000CAEB4
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning || this.publicKey == null)
			{
				throw new InvalidOperationException("Ed448Signer not initialised for verification");
			}
			return this.buffer.VerifySignature(this.publicKey, this.context, signature);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000CAEF0 File Offset: 0x000CAEF0
		public virtual void Reset()
		{
			this.buffer.Reset();
		}

		// Token: 0x04001707 RID: 5895
		private readonly Ed448Signer.Buffer buffer = new Ed448Signer.Buffer();

		// Token: 0x04001708 RID: 5896
		private readonly byte[] context;

		// Token: 0x04001709 RID: 5897
		private bool forSigning;

		// Token: 0x0400170A RID: 5898
		private Ed448PrivateKeyParameters privateKey;

		// Token: 0x0400170B RID: 5899
		private Ed448PublicKeyParameters publicKey;

		// Token: 0x02000E17 RID: 3607
		private class Buffer : MemoryStream
		{
			// Token: 0x06008C3A RID: 35898 RVA: 0x002A2064 File Offset: 0x002A2064
			internal byte[] GenerateSignature(Ed448PrivateKeyParameters privateKey, byte[] ctx)
			{
				byte[] result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int msgLen = (int)this.Position;
					byte[] array = new byte[Ed448PrivateKeyParameters.SignatureSize];
					privateKey.Sign(Ed448.Algorithm.Ed448, ctx, buffer, 0, msgLen, array, 0);
					this.Reset();
					result = array;
				}
				return result;
			}

			// Token: 0x06008C3B RID: 35899 RVA: 0x002A20C8 File Offset: 0x002A20C8
			internal bool VerifySignature(Ed448PublicKeyParameters publicKey, byte[] ctx, byte[] signature)
			{
				if (Ed448.SignatureSize != signature.Length)
				{
					return false;
				}
				bool result;
				lock (this)
				{
					byte[] buffer = this.GetBuffer();
					int mLen = (int)this.Position;
					byte[] encoded = publicKey.GetEncoded();
					bool flag = Ed448.Verify(signature, 0, encoded, 0, ctx, buffer, 0, mLen);
					this.Reset();
					result = flag;
				}
				return result;
			}

			// Token: 0x06008C3C RID: 35900 RVA: 0x002A213C File Offset: 0x002A213C
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
