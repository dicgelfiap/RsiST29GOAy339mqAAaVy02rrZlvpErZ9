using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x02000337 RID: 823
	public class DHKdfParameters : IDerivationParameters
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x0007EED0 File Offset: 0x0007EED0
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z) : this(algorithm, keySize, z, null)
		{
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0007EEDC File Offset: 0x0007EEDC
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z, byte[] extraInfo)
		{
			this.algorithm = algorithm;
			this.keySize = keySize;
			this.z = z;
			this.extraInfo = extraInfo;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0007EF04 File Offset: 0x0007EF04
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0007EF0C File Offset: 0x0007EF0C
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0007EF14 File Offset: 0x0007EF14
		public byte[] GetZ()
		{
			return this.z;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0007EF1C File Offset: 0x0007EF1C
		public byte[] GetExtraInfo()
		{
			return this.extraInfo;
		}

		// Token: 0x04001050 RID: 4176
		private readonly DerObjectIdentifier algorithm;

		// Token: 0x04001051 RID: 4177
		private readonly int keySize;

		// Token: 0x04001052 RID: 4178
		private readonly byte[] z;

		// Token: 0x04001053 RID: 4179
		private readonly byte[] extraInfo;
	}
}
