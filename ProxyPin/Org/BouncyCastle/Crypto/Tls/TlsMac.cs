using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053E RID: 1342
	public class TlsMac
	{
		// Token: 0x0600294A RID: 10570 RVA: 0x000DD860 File Offset: 0x000DD860
		public TlsMac(TlsContext context, IDigest digest, byte[] key, int keyOff, int keyLen)
		{
			this.context = context;
			KeyParameter keyParameter = new KeyParameter(key, keyOff, keyLen);
			this.secret = Arrays.Clone(keyParameter.GetKey());
			if (digest is LongDigest)
			{
				this.digestBlockSize = 128;
				this.digestOverhead = 16;
			}
			else
			{
				this.digestBlockSize = 64;
				this.digestOverhead = 8;
			}
			if (TlsUtilities.IsSsl(context))
			{
				this.mac = new Ssl3Mac(digest);
				if (digest.GetDigestSize() == 20)
				{
					this.digestOverhead = 4;
				}
			}
			else
			{
				this.mac = new HMac(digest);
			}
			this.mac.Init(keyParameter);
			this.macLength = this.mac.GetMacSize();
			if (context.SecurityParameters.truncatedHMac)
			{
				this.macLength = Math.Min(this.macLength, 10);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000DD948 File Offset: 0x000DD948
		public virtual byte[] MacSecret
		{
			get
			{
				return this.secret;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000DD950 File Offset: 0x000DD950
		public virtual int Size
		{
			get
			{
				return this.macLength;
			}
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000DD958 File Offset: 0x000DD958
		public virtual byte[] CalculateMac(long seqNo, byte type, byte[] message, int offset, int length)
		{
			ProtocolVersion serverVersion = this.context.ServerVersion;
			bool isSsl = serverVersion.IsSsl;
			byte[] array = new byte[isSsl ? 11 : 13];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			if (!isSsl)
			{
				TlsUtilities.WriteVersion(serverVersion, array, 9);
			}
			TlsUtilities.WriteUint16(length, array, array.Length - 2);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.BlockUpdate(message, offset, length);
			return this.Truncate(MacUtilities.DoFinal(this.mac));
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000DD9F0 File Offset: 0x000DD9F0
		public virtual byte[] CalculateMacConstantTime(long seqNo, byte type, byte[] message, int offset, int length, int fullLength, byte[] dummyData)
		{
			byte[] result = this.CalculateMac(seqNo, type, message, offset, length);
			int num = TlsUtilities.IsSsl(this.context) ? 11 : 13;
			int num2 = this.GetDigestBlockCount(num + fullLength) - this.GetDigestBlockCount(num + length);
			while (--num2 >= 0)
			{
				this.mac.BlockUpdate(dummyData, 0, this.digestBlockSize);
			}
			this.mac.Update(dummyData[0]);
			this.mac.Reset();
			return result;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000DDA7C File Offset: 0x000DDA7C
		protected virtual int GetDigestBlockCount(int inputLength)
		{
			return (inputLength + this.digestOverhead) / this.digestBlockSize;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000DDA90 File Offset: 0x000DDA90
		protected virtual byte[] Truncate(byte[] bs)
		{
			if (bs.Length <= this.macLength)
			{
				return bs;
			}
			return Arrays.CopyOf(bs, this.macLength);
		}

		// Token: 0x04001AE4 RID: 6884
		protected readonly TlsContext context;

		// Token: 0x04001AE5 RID: 6885
		protected readonly byte[] secret;

		// Token: 0x04001AE6 RID: 6886
		protected readonly IMac mac;

		// Token: 0x04001AE7 RID: 6887
		protected readonly int digestBlockSize;

		// Token: 0x04001AE8 RID: 6888
		protected readonly int digestOverhead;

		// Token: 0x04001AE9 RID: 6889
		protected readonly int macLength;
	}
}
