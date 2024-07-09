using System;
using System.Collections;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F0 RID: 1264
	internal class DeferredHash : TlsHandshakeHash, IDigest
	{
		// Token: 0x060026C7 RID: 9927 RVA: 0x000D1C08 File Offset: 0x000D1C08
		internal DeferredHash()
		{
			this.mBuf = new DigestInputBuffer();
			this.mHashes = Platform.CreateHashtable();
			this.mPrfHashAlgorithm = -1;
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000D1C30 File Offset: 0x000D1C30
		private DeferredHash(byte prfHashAlgorithm, IDigest prfHash)
		{
			this.mBuf = null;
			this.mHashes = Platform.CreateHashtable();
			this.mPrfHashAlgorithm = (int)prfHashAlgorithm;
			this.mHashes[prfHashAlgorithm] = prfHash;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000D1C64 File Offset: 0x000D1C64
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000D1C70 File Offset: 0x000D1C70
		public virtual TlsHandshakeHash NotifyPrfDetermined()
		{
			int prfAlgorithm = this.mContext.SecurityParameters.PrfAlgorithm;
			if (prfAlgorithm == 0)
			{
				CombinedHash combinedHash = new CombinedHash();
				combinedHash.Init(this.mContext);
				this.mBuf.UpdateDigest(combinedHash);
				return combinedHash.NotifyPrfDetermined();
			}
			this.mPrfHashAlgorithm = (int)TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm);
			this.CheckTrackingHash((byte)this.mPrfHashAlgorithm);
			return this;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000D1CD8 File Offset: 0x000D1CD8
		public virtual void TrackHashAlgorithm(byte hashAlgorithm)
		{
			if (this.mBuf == null)
			{
				throw new InvalidOperationException("Too late to track more hash algorithms");
			}
			this.CheckTrackingHash(hashAlgorithm);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000D1CF8 File Offset: 0x000D1CF8
		public virtual void SealHashAlgorithms()
		{
			this.CheckStopBuffering();
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000D1D00 File Offset: 0x000D1D00
		public virtual TlsHandshakeHash StopTracking()
		{
			byte b = (byte)this.mPrfHashAlgorithm;
			IDigest digest = TlsUtilities.CloneHash(b, (IDigest)this.mHashes[b]);
			if (this.mBuf != null)
			{
				this.mBuf.UpdateDigest(digest);
			}
			DeferredHash deferredHash = new DeferredHash(b, digest);
			deferredHash.Init(this.mContext);
			return deferredHash;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000D1D64 File Offset: 0x000D1D64
		public virtual IDigest ForkPrfHash()
		{
			this.CheckStopBuffering();
			byte b = (byte)this.mPrfHashAlgorithm;
			if (this.mBuf != null)
			{
				IDigest digest = TlsUtilities.CreateHash(b);
				this.mBuf.UpdateDigest(digest);
				return digest;
			}
			return TlsUtilities.CloneHash(b, (IDigest)this.mHashes[b]);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000D1DC0 File Offset: 0x000D1DC0
		public virtual byte[] GetFinalHash(byte hashAlgorithm)
		{
			IDigest digest = (IDigest)this.mHashes[hashAlgorithm];
			if (digest == null)
			{
				throw new InvalidOperationException("HashAlgorithm." + HashAlgorithm.GetText(hashAlgorithm) + " is not being tracked");
			}
			digest = TlsUtilities.CloneHash(hashAlgorithm, digest);
			if (this.mBuf != null)
			{
				this.mBuf.UpdateDigest(digest);
			}
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x000D1E30 File Offset: 0x000D1E30
		public virtual string AlgorithmName
		{
			get
			{
				throw new InvalidOperationException("Use Fork() to get a definite IDigest");
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000D1E3C File Offset: 0x000D1E3C
		public virtual int GetByteLength()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000D1E48 File Offset: 0x000D1E48
		public virtual int GetDigestSize()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000D1E54 File Offset: 0x000D1E54
		public virtual void Update(byte input)
		{
			if (this.mBuf != null)
			{
				this.mBuf.WriteByte(input);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				IDigest digest = (IDigest)obj;
				digest.Update(input);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000D1ED4 File Offset: 0x000D1ED4
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (this.mBuf != null)
			{
				this.mBuf.Write(input, inOff, len);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				IDigest digest = (IDigest)obj;
				digest.BlockUpdate(input, inOff, len);
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000D1F58 File Offset: 0x000D1F58
		public virtual int DoFinal(byte[] output, int outOff)
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000D1F64 File Offset: 0x000D1F64
		public virtual void Reset()
		{
			if (this.mBuf != null)
			{
				this.mBuf.SetLength(0L);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				IDigest digest = (IDigest)obj;
				digest.Reset();
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000D1FE4 File Offset: 0x000D1FE4
		protected virtual void CheckStopBuffering()
		{
			if (this.mBuf != null && this.mHashes.Count <= 4)
			{
				foreach (object obj in this.mHashes.Values)
				{
					IDigest d = (IDigest)obj;
					this.mBuf.UpdateDigest(d);
				}
				this.mBuf = null;
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000D2074 File Offset: 0x000D2074
		protected virtual void CheckTrackingHash(byte hashAlgorithm)
		{
			if (!this.mHashes.Contains(hashAlgorithm))
			{
				IDigest value = TlsUtilities.CreateHash(hashAlgorithm);
				this.mHashes[hashAlgorithm] = value;
			}
		}

		// Token: 0x04001921 RID: 6433
		protected const int BUFFERING_HASH_LIMIT = 4;

		// Token: 0x04001922 RID: 6434
		protected TlsContext mContext;

		// Token: 0x04001923 RID: 6435
		private DigestInputBuffer mBuf;

		// Token: 0x04001924 RID: 6436
		private IDictionary mHashes;

		// Token: 0x04001925 RID: 6437
		private int mPrfHashAlgorithm;
	}
}
