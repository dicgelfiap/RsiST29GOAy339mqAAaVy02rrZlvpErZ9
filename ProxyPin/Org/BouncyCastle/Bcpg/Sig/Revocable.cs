using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200028C RID: 652
	public class Revocable : SignatureSubpacket
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0006DCC0 File Offset: 0x0006DCC0
		private static byte[] BooleanToByteArray(bool value)
		{
			byte[] array = new byte[1];
			if (value)
			{
				array[0] = 1;
				return array;
			}
			return array;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0006DCE8 File Offset: 0x0006DCE8
		public Revocable(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.Revocable, critical, isLongLength, data)
		{
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0006DCF4 File Offset: 0x0006DCF4
		public Revocable(bool critical, bool isRevocable) : base(SignatureSubpacketTag.Revocable, critical, false, Revocable.BooleanToByteArray(isRevocable))
		{
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0006DD08 File Offset: 0x0006DD08
		public bool IsRevocable()
		{
			return this.data[0] != 0;
		}
	}
}
