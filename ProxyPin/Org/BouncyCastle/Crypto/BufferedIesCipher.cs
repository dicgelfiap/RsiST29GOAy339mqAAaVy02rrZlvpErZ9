using System;
using System.IO;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200055F RID: 1375
	public class BufferedIesCipher : BufferedCipherBase
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x000E4D20 File Offset: 0x000E4D20
		public BufferedIesCipher(IesEngine engine)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			this.engine = engine;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000E4D4C File Offset: 0x000E4D4C
		public override string AlgorithmName
		{
			get
			{
				return "IES";
			}
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000E4D54 File Offset: 0x000E4D54
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			throw Platform.CreateNotImplementedException("IES");
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000E4D68 File Offset: 0x000E4D68
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000E4D6C File Offset: 0x000E4D6C
		public override int GetOutputSize(int inputLen)
		{
			if (this.engine == null)
			{
				throw new InvalidOperationException("cipher not initialised");
			}
			int num = inputLen + (int)this.buffer.Length;
			if (!this.forEncryption)
			{
				return num - 20;
			}
			return num + 20;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000E4DB8 File Offset: 0x000E4DB8
		public override int GetUpdateOutputSize(int inputLen)
		{
			return 0;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000E4DBC File Offset: 0x000E4DBC
		public override byte[] ProcessByte(byte input)
		{
			this.buffer.WriteByte(input);
			return null;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000E4DCC File Offset: 0x000E4DCC
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inOff < 0)
			{
				throw new ArgumentException("inOff");
			}
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
			if (inOff + length > input.Length)
			{
				throw new ArgumentException("invalid offset/length specified for input array");
			}
			this.buffer.Write(input, inOff, length);
			return null;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000E4E38 File Offset: 0x000E4E38
		public override byte[] DoFinal()
		{
			byte[] array = this.buffer.ToArray();
			this.Reset();
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000E4E6C File Offset: 0x000E4E6C
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000E4E80 File Offset: 0x000E4E80
		public override void Reset()
		{
			this.buffer.SetLength(0L);
		}

		// Token: 0x04001B39 RID: 6969
		private readonly IesEngine engine;

		// Token: 0x04001B3A RID: 6970
		private bool forEncryption;

		// Token: 0x04001B3B RID: 6971
		private MemoryStream buffer = new MemoryStream();
	}
}
