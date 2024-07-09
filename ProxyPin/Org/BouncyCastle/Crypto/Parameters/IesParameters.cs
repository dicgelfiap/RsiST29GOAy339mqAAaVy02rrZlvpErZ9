using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045B RID: 1115
	public class IesParameters : ICipherParameters
	{
		// Token: 0x060022E9 RID: 8937 RVA: 0x000C5D34 File Offset: 0x000C5D34
		public IesParameters(byte[] derivation, byte[] encoding, int macKeySize)
		{
			this.derivation = derivation;
			this.encoding = encoding;
			this.macKeySize = macKeySize;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000C5D54 File Offset: 0x000C5D54
		public byte[] GetDerivationV()
		{
			return this.derivation;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000C5D5C File Offset: 0x000C5D5C
		public byte[] GetEncodingV()
		{
			return this.encoding;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000C5D64 File Offset: 0x000C5D64
		public int MacKeySize
		{
			get
			{
				return this.macKeySize;
			}
		}

		// Token: 0x04001637 RID: 5687
		private byte[] derivation;

		// Token: 0x04001638 RID: 5688
		private byte[] encoding;

		// Token: 0x04001639 RID: 5689
		private int macKeySize;
	}
}
