using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F3 RID: 1267
	public abstract class DtlsProtocol
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x000D2160 File Offset: 0x000D2160
		protected DtlsProtocol(SecureRandom secureRandom)
		{
			if (secureRandom == null)
			{
				throw new ArgumentNullException("secureRandom");
			}
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000D2180 File Offset: 0x000D2180
		protected virtual void ProcessFinished(byte[] body, byte[] expected_verify_data)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			byte[] b = TlsUtilities.ReadFully(expected_verify_data.Length, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			if (!Arrays.ConstantTimeAreEqual(expected_verify_data, b))
			{
				throw new TlsFatalAlert(40);
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000D21C0 File Offset: 0x000D21C0
		internal static void ApplyMaxFragmentLengthExtension(DtlsRecordLayer recordLayer, short maxFragmentLength)
		{
			if (maxFragmentLength >= 0)
			{
				if (!MaxFragmentLength.IsValid((byte)maxFragmentLength))
				{
					throw new TlsFatalAlert(80);
				}
				int plaintextLimit = 1 << (int)(8 + maxFragmentLength);
				recordLayer.SetPlaintextLimit(plaintextLimit);
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000D21FC File Offset: 0x000D21FC
		protected static short EvaluateMaxFragmentLengthExtension(bool resumedSession, IDictionary clientExtensions, IDictionary serverExtensions, byte alertDescription)
		{
			short maxFragmentLengthExtension = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(serverExtensions);
			if (maxFragmentLengthExtension >= 0 && (!MaxFragmentLength.IsValid((byte)maxFragmentLengthExtension) || (!resumedSession && maxFragmentLengthExtension != TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions))))
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return maxFragmentLengthExtension;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000D2244 File Offset: 0x000D2244
		protected static byte[] GenerateCertificate(Certificate certificate)
		{
			MemoryStream memoryStream = new MemoryStream();
			certificate.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000D2268 File Offset: 0x000D2268
		protected static byte[] GenerateSupplementalData(IList supplementalData)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsProtocol.WriteSupplementalData(memoryStream, supplementalData);
			return memoryStream.ToArray();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000D228C File Offset: 0x000D228C
		protected static void ValidateSelectedCipherSuite(int selectedCipherSuite, byte alertDescription)
		{
			switch (TlsUtilities.GetEncryptionAlgorithm(selectedCipherSuite))
			{
			case 1:
			case 2:
				throw new TlsFatalAlert(alertDescription);
			default:
				return;
			}
		}

		// Token: 0x04001928 RID: 6440
		protected readonly SecureRandom mSecureRandom;
	}
}
