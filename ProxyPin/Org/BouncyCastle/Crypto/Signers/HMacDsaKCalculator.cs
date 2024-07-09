using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A6 RID: 1190
	public class HMacDsaKCalculator : IDsaKCalculator
	{
		// Token: 0x06002499 RID: 9369 RVA: 0x000CB63C File Offset: 0x000CB63C
		public HMacDsaKCalculator(IDigest digest)
		{
			this.hMac = new HMac(digest);
			this.V = new byte[this.hMac.GetMacSize()];
			this.K = new byte[this.hMac.GetMacSize()];
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000CB67C File Offset: 0x000CB67C
		public virtual bool IsDeterministic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000CB680 File Offset: 0x000CB680
		public virtual void Init(BigInteger n, SecureRandom random)
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000CB68C File Offset: 0x000CB68C
		public void Init(BigInteger n, BigInteger d, byte[] message)
		{
			this.n = n;
			Arrays.Fill(this.V, 1);
			Arrays.Fill(this.K, 0);
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			byte[] array = new byte[unsignedByteLength];
			byte[] array2 = BigIntegers.AsUnsignedByteArray(d);
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			byte[] array3 = new byte[unsignedByteLength];
			BigInteger bigInteger = this.BitsToInt(message);
			if (bigInteger.CompareTo(n) >= 0)
			{
				bigInteger = bigInteger.Subtract(n);
			}
			byte[] array4 = BigIntegers.AsUnsignedByteArray(bigInteger);
			Array.Copy(array4, 0, array3, array3.Length - array4.Length, array4.Length);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.Update(0);
			this.hMac.BlockUpdate(array, 0, array.Length);
			this.hMac.BlockUpdate(array3, 0, array3.Length);
			this.hMac.DoFinal(this.K, 0);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.DoFinal(this.V, 0);
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.Update(1);
			this.hMac.BlockUpdate(array, 0, array.Length);
			this.hMac.BlockUpdate(array3, 0, array3.Length);
			this.hMac.DoFinal(this.K, 0);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.DoFinal(this.V, 0);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000CB878 File Offset: 0x000CB878
		public virtual BigInteger NextK()
		{
			byte[] array = new byte[BigIntegers.GetUnsignedByteLength(this.n)];
			BigInteger bigInteger;
			for (;;)
			{
				int num;
				for (int i = 0; i < array.Length; i += num)
				{
					this.hMac.BlockUpdate(this.V, 0, this.V.Length);
					this.hMac.DoFinal(this.V, 0);
					num = Math.Min(array.Length - i, this.V.Length);
					Array.Copy(this.V, 0, array, i, num);
				}
				bigInteger = this.BitsToInt(array);
				if (bigInteger.SignValue > 0 && bigInteger.CompareTo(this.n) < 0)
				{
					break;
				}
				this.hMac.BlockUpdate(this.V, 0, this.V.Length);
				this.hMac.Update(0);
				this.hMac.DoFinal(this.K, 0);
				this.hMac.Init(new KeyParameter(this.K));
				this.hMac.BlockUpdate(this.V, 0, this.V.Length);
				this.hMac.DoFinal(this.V, 0);
			}
			return bigInteger;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000CB9A4 File Offset: 0x000CB9A4
		private BigInteger BitsToInt(byte[] t)
		{
			BigInteger bigInteger = new BigInteger(1, t);
			if (t.Length * 8 > this.n.BitLength)
			{
				bigInteger = bigInteger.ShiftRight(t.Length * 8 - this.n.BitLength);
			}
			return bigInteger;
		}

		// Token: 0x04001716 RID: 5910
		private readonly HMac hMac;

		// Token: 0x04001717 RID: 5911
		private readonly byte[] K;

		// Token: 0x04001718 RID: 5912
		private readonly byte[] V;

		// Token: 0x04001719 RID: 5913
		private BigInteger n;
	}
}
