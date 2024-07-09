using System;
using System.IO;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000547 RID: 1351
	public abstract class TlsRsaUtilities
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x000DE730 File Offset: 0x000DE730
		public static byte[] GenerateEncryptedPreMasterSecret(TlsContext context, RsaKeyParameters rsaServerPublicKey, Stream output)
		{
			byte[] array = new byte[48];
			context.SecureRandom.NextBytes(array);
			TlsUtilities.WriteVersion(context.ClientVersion, array, 0);
			Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(new RsaBlindedEngine());
			pkcs1Encoding.Init(true, new ParametersWithRandom(rsaServerPublicKey, context.SecureRandom));
			try
			{
				byte[] array2 = pkcs1Encoding.ProcessBlock(array, 0, array.Length);
				if (TlsUtilities.IsSsl(context))
				{
					output.Write(array2, 0, array2.Length);
				}
				else
				{
					TlsUtilities.WriteOpaque16(array2, output);
				}
			}
			catch (InvalidCipherTextException alertCause)
			{
				throw new TlsFatalAlert(80, alertCause);
			}
			return array;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000DE7CC File Offset: 0x000DE7CC
		public static byte[] SafeDecryptPreMasterSecret(TlsContext context, RsaKeyParameters rsaServerPrivateKey, byte[] encryptedPreMasterSecret)
		{
			ProtocolVersion clientVersion = context.ClientVersion;
			bool flag = false;
			byte[] array = new byte[48];
			context.SecureRandom.NextBytes(array);
			byte[] array2 = Arrays.Clone(array);
			try
			{
				Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(new RsaBlindedEngine(), array);
				pkcs1Encoding.Init(false, new ParametersWithRandom(rsaServerPrivateKey, context.SecureRandom));
				array2 = pkcs1Encoding.ProcessBlock(encryptedPreMasterSecret, 0, encryptedPreMasterSecret.Length);
			}
			catch (Exception)
			{
			}
			if (!flag || !clientVersion.IsEqualOrEarlierVersionOf(ProtocolVersion.TLSv10))
			{
				int num = (clientVersion.MajorVersion ^ (int)(array2[0] & byte.MaxValue)) | (clientVersion.MinorVersion ^ (int)(array2[1] & byte.MaxValue));
				num |= num >> 1;
				num |= num >> 2;
				num |= num >> 4;
				int num2 = ~((num & 1) - 1);
				for (int i = 0; i < 48; i++)
				{
					array2[i] = (byte)(((int)array2[i] & ~num2) | ((int)array[i] & num2));
				}
			}
			return array2;
		}
	}
}
