using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000387 RID: 903
	public class DesEdeEngine : DesEngine
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x0009F23C File Offset: 0x0009F23C
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to DESede init - " + Platform.GetTypeName(parameters));
			}
			byte[] key = ((KeyParameter)parameters).GetKey();
			if (key.Length != 24 && key.Length != 16)
			{
				throw new ArgumentException("key size must be 16 or 24 bytes.");
			}
			this.forEncryption = forEncryption;
			byte[] array = new byte[8];
			Array.Copy(key, 0, array, 0, array.Length);
			this.workingKey1 = DesEngine.GenerateWorkingKey(forEncryption, array);
			byte[] array2 = new byte[8];
			Array.Copy(key, 8, array2, 0, array2.Length);
			this.workingKey2 = DesEngine.GenerateWorkingKey(!forEncryption, array2);
			if (key.Length == 24)
			{
				byte[] array3 = new byte[8];
				Array.Copy(key, 16, array3, 0, array3.Length);
				this.workingKey3 = DesEngine.GenerateWorkingKey(forEncryption, array3);
				return;
			}
			this.workingKey3 = this.workingKey1;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0009F31C File Offset: 0x0009F31C
		public override string AlgorithmName
		{
			get
			{
				return "DESede";
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0009F324 File Offset: 0x0009F324
		public override int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0009F328 File Offset: 0x0009F328
		public override int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey1 == null)
			{
				throw new InvalidOperationException("DESede engine not initialised");
			}
			Check.DataLength(input, inOff, 8, "input buffer too short");
			Check.OutputLength(output, outOff, 8, "output buffer too short");
			byte[] array = new byte[8];
			if (this.forEncryption)
			{
				DesEngine.DesFunc(this.workingKey1, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey3, array, 0, output, outOff);
			}
			else
			{
				DesEngine.DesFunc(this.workingKey3, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey1, array, 0, output, outOff);
			}
			return 8;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0009F3E0 File Offset: 0x0009F3E0
		public override void Reset()
		{
		}

		// Token: 0x040012CE RID: 4814
		private int[] workingKey1;

		// Token: 0x040012CF RID: 4815
		private int[] workingKey2;

		// Token: 0x040012D0 RID: 4816
		private int[] workingKey3;

		// Token: 0x040012D1 RID: 4817
		private bool forEncryption;
	}
}
