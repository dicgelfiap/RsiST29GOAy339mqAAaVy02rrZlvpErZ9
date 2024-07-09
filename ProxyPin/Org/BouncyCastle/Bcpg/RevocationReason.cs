using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200028F RID: 655
	public class RevocationReason : SignatureSubpacket
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x0006DDBC File Offset: 0x0006DDBC
		public RevocationReason(bool isCritical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.RevocationReason, isCritical, isLongLength, data)
		{
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0006DDCC File Offset: 0x0006DDCC
		public RevocationReason(bool isCritical, RevocationReasonTag reason, string description) : base(SignatureSubpacketTag.RevocationReason, isCritical, false, RevocationReason.CreateData(reason, description))
		{
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0006DDE0 File Offset: 0x0006DDE0
		private static byte[] CreateData(RevocationReasonTag reason, string description)
		{
			byte[] array = Strings.ToUtf8ByteArray(description);
			byte[] array2 = new byte[1 + array.Length];
			array2[0] = (byte)reason;
			Array.Copy(array, 0, array2, 1, array.Length);
			return array2;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0006DE14 File Offset: 0x0006DE14
		public virtual RevocationReasonTag GetRevocationReason()
		{
			return (RevocationReasonTag)base.GetData()[0];
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0006DE20 File Offset: 0x0006DE20
		public virtual string GetRevocationDescription()
		{
			byte[] data = base.GetData();
			if (data.Length == 1)
			{
				return string.Empty;
			}
			byte[] array = new byte[data.Length - 1];
			Array.Copy(data, 1, array, 0, array.Length);
			return Strings.FromUtf8ByteArray(array);
		}
	}
}
