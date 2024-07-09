using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000525 RID: 1317
	public class Ssl3Mac : IMac
	{
		// Token: 0x0600280A RID: 10250 RVA: 0x000D73F4 File Offset: 0x000D73F4
		public Ssl3Mac(IDigest digest)
		{
			this.digest = digest;
			if (digest.GetDigestSize() == 20)
			{
				this.padLength = 40;
				return;
			}
			this.padLength = 48;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000D7424 File Offset: 0x000D7424
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "/SSL3MAC";
			}
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000D743C File Offset: 0x000D743C
		public virtual void Init(ICipherParameters parameters)
		{
			this.secret = Arrays.Clone(((KeyParameter)parameters).GetKey());
			this.Reset();
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000D745C File Offset: 0x000D745C
		public virtual int GetMacSize()
		{
			return this.digest.GetDigestSize();
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000D746C File Offset: 0x000D746C
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000D747C File Offset: 0x000D747C
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.digest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000D748C File Offset: 0x000D748C
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			this.digest.BlockUpdate(this.secret, 0, this.secret.Length);
			this.digest.BlockUpdate(Ssl3Mac.OPAD, 0, this.padLength);
			this.digest.BlockUpdate(array, 0, array.Length);
			int result = this.digest.DoFinal(output, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000D7514 File Offset: 0x000D7514
		public virtual void Reset()
		{
			this.digest.Reset();
			this.digest.BlockUpdate(this.secret, 0, this.secret.Length);
			this.digest.BlockUpdate(Ssl3Mac.IPAD, 0, this.padLength);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000D7564 File Offset: 0x000D7564
		private static byte[] GenPad(byte b, int count)
		{
			byte[] array = new byte[count];
			Arrays.Fill(array, b);
			return array;
		}

		// Token: 0x04001A63 RID: 6755
		private const byte IPAD_BYTE = 54;

		// Token: 0x04001A64 RID: 6756
		private const byte OPAD_BYTE = 92;

		// Token: 0x04001A65 RID: 6757
		internal static readonly byte[] IPAD = Ssl3Mac.GenPad(54, 48);

		// Token: 0x04001A66 RID: 6758
		internal static readonly byte[] OPAD = Ssl3Mac.GenPad(92, 48);

		// Token: 0x04001A67 RID: 6759
		private readonly IDigest digest;

		// Token: 0x04001A68 RID: 6760
		private readonly int padLength;

		// Token: 0x04001A69 RID: 6761
		private byte[] secret;
	}
}
