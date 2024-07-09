using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E0 RID: 1248
	internal class CombinedHash : TlsHandshakeHash, IDigest
	{
		// Token: 0x0600265E RID: 9822 RVA: 0x000D0B04 File Offset: 0x000D0B04
		internal CombinedHash()
		{
			this.mMd5 = TlsUtilities.CreateHash(1);
			this.mSha1 = TlsUtilities.CreateHash(2);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000D0B24 File Offset: 0x000D0B24
		internal CombinedHash(CombinedHash t)
		{
			this.mContext = t.mContext;
			this.mMd5 = TlsUtilities.CloneHash(1, t.mMd5);
			this.mSha1 = TlsUtilities.CloneHash(2, t.mSha1);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000D0B5C File Offset: 0x000D0B5C
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000D0B68 File Offset: 0x000D0B68
		public virtual TlsHandshakeHash NotifyPrfDetermined()
		{
			return this;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000D0B6C File Offset: 0x000D0B6C
		public virtual void TrackHashAlgorithm(byte hashAlgorithm)
		{
			throw new InvalidOperationException("CombinedHash only supports calculating the legacy PRF for handshake hash");
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000D0B78 File Offset: 0x000D0B78
		public virtual void SealHashAlgorithms()
		{
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000D0B7C File Offset: 0x000D0B7C
		public virtual TlsHandshakeHash StopTracking()
		{
			return new CombinedHash(this);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000D0B84 File Offset: 0x000D0B84
		public virtual IDigest ForkPrfHash()
		{
			return new CombinedHash(this);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000D0B8C File Offset: 0x000D0B8C
		public virtual byte[] GetFinalHash(byte hashAlgorithm)
		{
			throw new InvalidOperationException("CombinedHash doesn't support multiple hashes");
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000D0B98 File Offset: 0x000D0B98
		public virtual string AlgorithmName
		{
			get
			{
				return this.mMd5.AlgorithmName + " and " + this.mSha1.AlgorithmName;
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000D0BBC File Offset: 0x000D0BBC
		public virtual int GetByteLength()
		{
			return Math.Max(this.mMd5.GetByteLength(), this.mSha1.GetByteLength());
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000D0BDC File Offset: 0x000D0BDC
		public virtual int GetDigestSize()
		{
			return this.mMd5.GetDigestSize() + this.mSha1.GetDigestSize();
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000D0BF8 File Offset: 0x000D0BF8
		public virtual void Update(byte input)
		{
			this.mMd5.Update(input);
			this.mSha1.Update(input);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000D0C14 File Offset: 0x000D0C14
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.mMd5.BlockUpdate(input, inOff, len);
			this.mSha1.BlockUpdate(input, inOff, len);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000D0C34 File Offset: 0x000D0C34
		public virtual int DoFinal(byte[] output, int outOff)
		{
			if (this.mContext != null && TlsUtilities.IsSsl(this.mContext))
			{
				this.Ssl3Complete(this.mMd5, Ssl3Mac.IPAD, Ssl3Mac.OPAD, 48);
				this.Ssl3Complete(this.mSha1, Ssl3Mac.IPAD, Ssl3Mac.OPAD, 40);
			}
			int num = this.mMd5.DoFinal(output, outOff);
			int num2 = this.mSha1.DoFinal(output, outOff + num);
			return num + num2;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000D0CB4 File Offset: 0x000D0CB4
		public virtual void Reset()
		{
			this.mMd5.Reset();
			this.mSha1.Reset();
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000D0CCC File Offset: 0x000D0CCC
		protected virtual void Ssl3Complete(IDigest d, byte[] ipad, byte[] opad, int padLength)
		{
			byte[] masterSecret = this.mContext.SecurityParameters.masterSecret;
			d.BlockUpdate(masterSecret, 0, masterSecret.Length);
			d.BlockUpdate(ipad, 0, padLength);
			byte[] array = DigestUtilities.DoFinal(d);
			d.BlockUpdate(masterSecret, 0, masterSecret.Length);
			d.BlockUpdate(opad, 0, padLength);
			d.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x04001902 RID: 6402
		protected TlsContext mContext;

		// Token: 0x04001903 RID: 6403
		protected IDigest mMd5;

		// Token: 0x04001904 RID: 6404
		protected IDigest mSha1;
	}
}
