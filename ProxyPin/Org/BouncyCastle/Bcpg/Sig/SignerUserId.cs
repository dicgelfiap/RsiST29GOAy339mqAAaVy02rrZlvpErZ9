using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000293 RID: 659
	public class SignerUserId : SignatureSubpacket
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x0006DFC8 File Offset: 0x0006DFC8
		private static byte[] UserIdToBytes(string id)
		{
			byte[] array = new byte[id.Length];
			for (int num = 0; num != id.Length; num++)
			{
				array[num] = (byte)id[num];
			}
			return array;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0006E008 File Offset: 0x0006E008
		public SignerUserId(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.SignerUserId, critical, isLongLength, data)
		{
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0006E018 File Offset: 0x0006E018
		public SignerUserId(bool critical, string userId) : base(SignatureSubpacketTag.SignerUserId, critical, false, SignerUserId.UserIdToBytes(userId))
		{
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0006E02C File Offset: 0x0006E02C
		public string GetId()
		{
			char[] array = new char[this.data.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (char)(this.data[num] & byte.MaxValue);
			}
			return new string(array);
		}
	}
}
