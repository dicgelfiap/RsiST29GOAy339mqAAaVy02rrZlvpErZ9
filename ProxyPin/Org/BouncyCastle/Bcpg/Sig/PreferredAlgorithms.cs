using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200028A RID: 650
	public class PreferredAlgorithms : SignatureSubpacket
	{
		// Token: 0x06001479 RID: 5241 RVA: 0x0006DBC8 File Offset: 0x0006DBC8
		private static byte[] IntToByteArray(int[] v)
		{
			byte[] array = new byte[v.Length];
			for (int num = 0; num != v.Length; num++)
			{
				array[num] = (byte)v[num];
			}
			return array;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0006DBFC File Offset: 0x0006DBFC
		public PreferredAlgorithms(SignatureSubpacketTag type, bool critical, bool isLongLength, byte[] data) : base(type, critical, isLongLength, data)
		{
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0006DC0C File Offset: 0x0006DC0C
		public PreferredAlgorithms(SignatureSubpacketTag type, bool critical, int[] preferences) : base(type, critical, false, PreferredAlgorithms.IntToByteArray(preferences))
		{
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0006DC20 File Offset: 0x0006DC20
		public int[] GetPreferences()
		{
			int[] array = new int[this.data.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (int)(this.data[num] & byte.MaxValue);
			}
			return array;
		}
	}
}
