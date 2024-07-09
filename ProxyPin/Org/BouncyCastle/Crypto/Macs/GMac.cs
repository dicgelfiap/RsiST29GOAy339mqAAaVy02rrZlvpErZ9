using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E8 RID: 1000
	public class GMac : IMac
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x000B832C File Offset: 0x000B832C
		public GMac(GcmBlockCipher cipher) : this(cipher, 128)
		{
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000B833C File Offset: 0x000B833C
		public GMac(GcmBlockCipher cipher, int macSizeBits)
		{
			this.cipher = cipher;
			this.macSizeBits = macSizeBits;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000B8354 File Offset: 0x000B8354
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				KeyParameter key = (KeyParameter)parametersWithIV.Parameters;
				this.cipher.Init(true, new AeadParameters(key, this.macSizeBits, iv));
				return;
			}
			throw new ArgumentException("GMAC requires ParametersWithIV");
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000B83B0 File Offset: 0x000B83B0
		public string AlgorithmName
		{
			get
			{
				return this.cipher.GetUnderlyingCipher().AlgorithmName + "-GMAC";
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000B83CC File Offset: 0x000B83CC
		public int GetMacSize()
		{
			return this.macSizeBits / 8;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000B83D8 File Offset: 0x000B83D8
		public void Update(byte input)
		{
			this.cipher.ProcessAadByte(input);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000B83E8 File Offset: 0x000B83E8
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.cipher.ProcessAadBytes(input, inOff, len);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000B83F8 File Offset: 0x000B83F8
		public int DoFinal(byte[] output, int outOff)
		{
			int result;
			try
			{
				result = this.cipher.DoFinal(output, outOff);
			}
			catch (InvalidCipherTextException ex)
			{
				throw new InvalidOperationException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000B8438 File Offset: 0x000B8438
		public void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x040014BB RID: 5307
		private readonly GcmBlockCipher cipher;

		// Token: 0x040014BC RID: 5308
		private readonly int macSizeBits;
	}
}
