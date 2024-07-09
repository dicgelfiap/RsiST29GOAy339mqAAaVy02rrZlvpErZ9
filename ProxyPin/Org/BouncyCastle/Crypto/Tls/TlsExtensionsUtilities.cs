using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053B RID: 1339
	public abstract class TlsExtensionsUtilities
	{
		// Token: 0x06002915 RID: 10517 RVA: 0x000DD2A4 File Offset: 0x000DD2A4
		public static IDictionary EnsureExtensionsInitialised(IDictionary extensions)
		{
			if (extensions != null)
			{
				return extensions;
			}
			return Platform.CreateHashtable();
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000DD2B4 File Offset: 0x000DD2B4
		public static void AddClientCertificateTypeExtensionClient(IDictionary extensions, byte[] certificateTypes)
		{
			extensions[19] = TlsExtensionsUtilities.CreateCertificateTypeExtensionClient(certificateTypes);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000DD2CC File Offset: 0x000DD2CC
		public static void AddClientCertificateTypeExtensionServer(IDictionary extensions, byte certificateType)
		{
			extensions[19] = TlsExtensionsUtilities.CreateCertificateTypeExtensionServer(certificateType);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000DD2E4 File Offset: 0x000DD2E4
		public static void AddEncryptThenMacExtension(IDictionary extensions)
		{
			extensions[22] = TlsExtensionsUtilities.CreateEncryptThenMacExtension();
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000DD2F8 File Offset: 0x000DD2F8
		public static void AddExtendedMasterSecretExtension(IDictionary extensions)
		{
			extensions[23] = TlsExtensionsUtilities.CreateExtendedMasterSecretExtension();
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000DD30C File Offset: 0x000DD30C
		public static void AddHeartbeatExtension(IDictionary extensions, HeartbeatExtension heartbeatExtension)
		{
			extensions[15] = TlsExtensionsUtilities.CreateHeartbeatExtension(heartbeatExtension);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000DD324 File Offset: 0x000DD324
		public static void AddMaxFragmentLengthExtension(IDictionary extensions, byte maxFragmentLength)
		{
			extensions[1] = TlsExtensionsUtilities.CreateMaxFragmentLengthExtension(maxFragmentLength);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000DD338 File Offset: 0x000DD338
		public static void AddPaddingExtension(IDictionary extensions, int dataLength)
		{
			extensions[21] = TlsExtensionsUtilities.CreatePaddingExtension(dataLength);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000DD350 File Offset: 0x000DD350
		public static void AddServerCertificateTypeExtensionClient(IDictionary extensions, byte[] certificateTypes)
		{
			extensions[20] = TlsExtensionsUtilities.CreateCertificateTypeExtensionClient(certificateTypes);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000DD368 File Offset: 0x000DD368
		public static void AddServerCertificateTypeExtensionServer(IDictionary extensions, byte certificateType)
		{
			extensions[20] = TlsExtensionsUtilities.CreateCertificateTypeExtensionServer(certificateType);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000DD380 File Offset: 0x000DD380
		public static void AddServerNameExtension(IDictionary extensions, ServerNameList serverNameList)
		{
			extensions[0] = TlsExtensionsUtilities.CreateServerNameExtension(serverNameList);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000DD394 File Offset: 0x000DD394
		public static void AddStatusRequestExtension(IDictionary extensions, CertificateStatusRequest statusRequest)
		{
			extensions[5] = TlsExtensionsUtilities.CreateStatusRequestExtension(statusRequest);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000DD3A8 File Offset: 0x000DD3A8
		public static void AddTruncatedHMacExtension(IDictionary extensions)
		{
			extensions[4] = TlsExtensionsUtilities.CreateTruncatedHMacExtension();
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000DD3BC File Offset: 0x000DD3BC
		public static byte[] GetClientCertificateTypeExtensionClient(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 19);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadCertificateTypeExtensionClient(extensionData);
			}
			return null;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000DD3E4 File Offset: 0x000DD3E4
		public static short GetClientCertificateTypeExtensionServer(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 19);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadCertificateTypeExtensionServer(extensionData);
			}
			return -1;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000DD40C File Offset: 0x000DD40C
		public static HeartbeatExtension GetHeartbeatExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 15);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadHeartbeatExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000DD434 File Offset: 0x000DD434
		public static short GetMaxFragmentLengthExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 1);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadMaxFragmentLengthExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000DD45C File Offset: 0x000DD45C
		public static int GetPaddingExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 21);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadPaddingExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000DD484 File Offset: 0x000DD484
		public static byte[] GetServerCertificateTypeExtensionClient(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 20);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadCertificateTypeExtensionClient(extensionData);
			}
			return null;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000DD4AC File Offset: 0x000DD4AC
		public static short GetServerCertificateTypeExtensionServer(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 20);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadCertificateTypeExtensionServer(extensionData);
			}
			return -1;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000DD4D4 File Offset: 0x000DD4D4
		public static ServerNameList GetServerNameExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 0);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadServerNameExtension(extensionData);
			}
			return null;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000DD4FC File Offset: 0x000DD4FC
		public static CertificateStatusRequest GetStatusRequestExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 5);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadStatusRequestExtension(extensionData);
			}
			return null;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000DD524 File Offset: 0x000DD524
		public static bool HasEncryptThenMacExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 22);
			return extensionData != null && TlsExtensionsUtilities.ReadEncryptThenMacExtension(extensionData);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000DD54C File Offset: 0x000DD54C
		public static bool HasExtendedMasterSecretExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 23);
			return extensionData != null && TlsExtensionsUtilities.ReadExtendedMasterSecretExtension(extensionData);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000DD574 File Offset: 0x000DD574
		public static bool HasTruncatedHMacExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 4);
			return extensionData != null && TlsExtensionsUtilities.ReadTruncatedHMacExtension(extensionData);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000DD59C File Offset: 0x000DD59C
		public static byte[] CreateCertificateTypeExtensionClient(byte[] certificateTypes)
		{
			if (certificateTypes == null || certificateTypes.Length < 1 || certificateTypes.Length > 255)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(certificateTypes);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000DD5C8 File Offset: 0x000DD5C8
		public static byte[] CreateCertificateTypeExtensionServer(byte certificateType)
		{
			return TlsUtilities.EncodeUint8(certificateType);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000DD5D0 File Offset: 0x000DD5D0
		public static byte[] CreateEmptyExtensionData()
		{
			return TlsUtilities.EmptyBytes;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000DD5D8 File Offset: 0x000DD5D8
		public static byte[] CreateEncryptThenMacExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000DD5E0 File Offset: 0x000DD5E0
		public static byte[] CreateExtendedMasterSecretExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000DD5E8 File Offset: 0x000DD5E8
		public static byte[] CreateHeartbeatExtension(HeartbeatExtension heartbeatExtension)
		{
			if (heartbeatExtension == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			heartbeatExtension.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000DD61C File Offset: 0x000DD61C
		public static byte[] CreateMaxFragmentLengthExtension(byte maxFragmentLength)
		{
			return TlsUtilities.EncodeUint8(maxFragmentLength);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000DD624 File Offset: 0x000DD624
		public static byte[] CreatePaddingExtension(int dataLength)
		{
			TlsUtilities.CheckUint16(dataLength);
			return new byte[dataLength];
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000DD634 File Offset: 0x000DD634
		public static byte[] CreateServerNameExtension(ServerNameList serverNameList)
		{
			if (serverNameList == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			serverNameList.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000DD668 File Offset: 0x000DD668
		public static byte[] CreateStatusRequestExtension(CertificateStatusRequest statusRequest)
		{
			if (statusRequest == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			statusRequest.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000DD69C File Offset: 0x000DD69C
		public static byte[] CreateTruncatedHMacExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000DD6A4 File Offset: 0x000DD6A4
		private static bool ReadEmptyExtensionData(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			if (extensionData.Length != 0)
			{
				throw new TlsFatalAlert(47);
			}
			return true;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000DD6C8 File Offset: 0x000DD6C8
		public static byte[] ReadCertificateTypeExtensionClient(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			return array;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000DD6F4 File Offset: 0x000DD6F4
		public static byte ReadCertificateTypeExtensionServer(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000DD6FC File Offset: 0x000DD6FC
		public static bool ReadEncryptThenMacExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000DD704 File Offset: 0x000DD704
		public static bool ReadExtendedMasterSecretExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000DD70C File Offset: 0x000DD70C
		public static HeartbeatExtension ReadHeartbeatExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			HeartbeatExtension result = HeartbeatExtension.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000DD744 File Offset: 0x000DD744
		public static byte ReadMaxFragmentLengthExtension(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000DD74C File Offset: 0x000DD74C
		public static int ReadPaddingExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			for (int i = 0; i < extensionData.Length; i++)
			{
				if (extensionData[i] != 0)
				{
					throw new TlsFatalAlert(47);
				}
			}
			return extensionData.Length;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000DD794 File Offset: 0x000DD794
		public static ServerNameList ReadServerNameExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			ServerNameList result = ServerNameList.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000DD7CC File Offset: 0x000DD7CC
		public static CertificateStatusRequest ReadStatusRequestExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			CertificateStatusRequest result = CertificateStatusRequest.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000DD804 File Offset: 0x000DD804
		public static bool ReadTruncatedHMacExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}
	}
}
