using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace PeNet.Utilities
{
	// Token: 0x02000B97 RID: 2967
	[ComVisible(true)]
	public static class SignatureInformation
	{
		// Token: 0x06007794 RID: 30612 RVA: 0x0023ADDC File Offset: 0x0023ADDC
		public static bool IsSigned(string filePath)
		{
			X509Certificate2 x509Certificate;
			return SignatureInformation.IsSigned(filePath, out x509Certificate);
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x0023ADF8 File Offset: 0x0023ADF8
		public static bool IsSigned(string filePath, out X509Certificate2 cert)
		{
			cert = null;
			try
			{
				PeFile peFile = new PeFile(filePath);
				if (peFile.PKCS7 == null)
				{
					return false;
				}
				cert = peFile.PKCS7;
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x0023AE4C File Offset: 0x0023AE4C
		public static bool IsValidCertChain(string filePath, bool online)
		{
			X509Certificate2 cert;
			return SignatureInformation.IsSigned(filePath, out cert) && SignatureInformation.IsValidCertChain(cert, online);
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x0023AE74 File Offset: 0x0023AE74
		public static bool IsValidCertChain(string filePath, TimeSpan urlRetrievalTimeout, bool excludeRoot = true)
		{
			X509Certificate2 cert;
			return SignatureInformation.IsSigned(filePath, out cert) && SignatureInformation.IsValidCertChain(cert, urlRetrievalTimeout, excludeRoot);
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x0023AE9C File Offset: 0x0023AE9C
		public static bool IsValidCertChain(X509Certificate2 cert, bool online)
		{
			return new X509Chain
			{
				ChainPolicy = 
				{
					RevocationFlag = X509RevocationFlag.ExcludeRoot,
					RevocationMode = (online ? X509RevocationMode.Online : X509RevocationMode.Offline),
					UrlRetrievalTimeout = new TimeSpan(0, 1, 0),
					VerificationFlags = X509VerificationFlags.NoFlag
				}
			}.Build(cert);
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x0023AEFC File Offset: 0x0023AEFC
		public static bool IsValidCertChain(X509Certificate2 cert, TimeSpan urlRetrievalTimeout, bool excludeRoot = true)
		{
			return new X509Chain
			{
				ChainPolicy = 
				{
					RevocationFlag = (excludeRoot ? X509RevocationFlag.ExcludeRoot : X509RevocationFlag.EntireChain),
					RevocationMode = X509RevocationMode.Online,
					UrlRetrievalTimeout = urlRetrievalTimeout,
					VerificationFlags = X509VerificationFlags.NoFlag
				}
			}.Build(cert);
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x0023AF54 File Offset: 0x0023AF54
		[Obsolete("use `new PeFile(filePath).IsSignatureValid`", true)]
		public static bool IsSignatureValid(string filePath)
		{
			return new PeFile(filePath).IsSignatureValid;
		}
	}
}
