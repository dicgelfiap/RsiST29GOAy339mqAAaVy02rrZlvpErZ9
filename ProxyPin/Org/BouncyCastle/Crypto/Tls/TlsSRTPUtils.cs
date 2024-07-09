using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000550 RID: 1360
	public abstract class TlsSRTPUtils
	{
		// Token: 0x060029C0 RID: 10688 RVA: 0x000E00E8 File Offset: 0x000E00E8
		public static void AddUseSrtpExtension(IDictionary extensions, UseSrtpData useSRTPData)
		{
			extensions[14] = TlsSRTPUtils.CreateUseSrtpExtension(useSRTPData);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000E0100 File Offset: 0x000E0100
		public static UseSrtpData GetUseSrtpExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 14);
			if (extensionData != null)
			{
				return TlsSRTPUtils.ReadUseSrtpExtension(extensionData);
			}
			return null;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000E0128 File Offset: 0x000E0128
		public static byte[] CreateUseSrtpExtension(UseSrtpData useSrtpData)
		{
			if (useSrtpData == null)
			{
				throw new ArgumentNullException("useSrtpData");
			}
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteUint16ArrayWithUint16Length(useSrtpData.ProtectionProfiles, memoryStream);
			TlsUtilities.WriteOpaque8(useSrtpData.Mki, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000E0170 File Offset: 0x000E0170
		public static UseSrtpData ReadUseSrtpExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, true);
			int num = TlsUtilities.ReadUint16(memoryStream);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int[] protectionProfiles = TlsUtilities.ReadUint16Array(num / 2, memoryStream);
			byte[] mki = TlsUtilities.ReadOpaque8(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return new UseSrtpData(protectionProfiles, mki);
		}
	}
}
