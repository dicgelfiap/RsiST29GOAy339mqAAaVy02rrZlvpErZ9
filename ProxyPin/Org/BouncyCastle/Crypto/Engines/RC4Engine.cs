using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000397 RID: 919
	public class RC4Engine : IStreamCipher
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x000A5828 File Offset: 0x000A5828
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.workingKey = ((KeyParameter)parameters).GetKey();
				this.SetKey(this.workingKey);
				return;
			}
			throw new ArgumentException("invalid parameter passed to RC4 init - " + Platform.GetTypeName(parameters));
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000A5868 File Offset: 0x000A5868
		public virtual string AlgorithmName
		{
			get
			{
				return "RC4";
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000A5870 File Offset: 0x000A5870
		public virtual byte ReturnByte(byte input)
		{
			this.x = (this.x + 1 & 255);
			this.y = ((int)this.engineState[this.x] + this.y & 255);
			byte b = this.engineState[this.x];
			this.engineState[this.x] = this.engineState[this.y];
			this.engineState[this.y] = b;
			return input ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)];
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000A5918 File Offset: 0x000A5918
		public virtual void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, length, "input buffer too short");
			Check.OutputLength(output, outOff, length, "output buffer too short");
			for (int i = 0; i < length; i++)
			{
				this.x = (this.x + 1 & 255);
				this.y = ((int)this.engineState[this.x] + this.y & 255);
				byte b = this.engineState[this.x];
				this.engineState[this.x] = this.engineState[this.y];
				this.engineState[this.y] = b;
				output[i + outOff] = (input[i + inOff] ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)]);
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000A59F8 File Offset: 0x000A59F8
		public virtual void Reset()
		{
			this.SetKey(this.workingKey);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000A5A08 File Offset: 0x000A5A08
		private void SetKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			this.x = 0;
			this.y = 0;
			if (this.engineState == null)
			{
				this.engineState = new byte[RC4Engine.STATE_LENGTH];
			}
			for (int i = 0; i < RC4Engine.STATE_LENGTH; i++)
			{
				this.engineState[i] = (byte)i;
			}
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < RC4Engine.STATE_LENGTH; j++)
			{
				num2 = ((int)((keyBytes[num] & byte.MaxValue) + this.engineState[j]) + num2 & 255);
				byte b = this.engineState[j];
				this.engineState[j] = this.engineState[num2];
				this.engineState[num2] = b;
				num = (num + 1) % keyBytes.Length;
			}
		}

		// Token: 0x04001345 RID: 4933
		private static readonly int STATE_LENGTH = 256;

		// Token: 0x04001346 RID: 4934
		private byte[] engineState;

		// Token: 0x04001347 RID: 4935
		private int x;

		// Token: 0x04001348 RID: 4936
		private int y;

		// Token: 0x04001349 RID: 4937
		private byte[] workingKey;
	}
}
