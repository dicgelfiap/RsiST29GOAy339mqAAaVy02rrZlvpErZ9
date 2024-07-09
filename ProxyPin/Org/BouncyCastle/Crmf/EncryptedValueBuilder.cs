using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000320 RID: 800
	public class EncryptedValueBuilder
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0007D324 File Offset: 0x0007D324
		public EncryptedValueBuilder(IKeyWrapper wrapper, ICipherBuilderWithKey encryptor) : this(wrapper, encryptor, null)
		{
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0007D330 File Offset: 0x0007D330
		public EncryptedValueBuilder(IKeyWrapper wrapper, ICipherBuilderWithKey encryptor, IEncryptedValuePadder padder)
		{
			this.wrapper = wrapper;
			this.encryptor = encryptor;
			this.padder = padder;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0007D350 File Offset: 0x0007D350
		public EncryptedValue Build(char[] revocationPassphrase)
		{
			return this.EncryptData(this.PadData(Strings.ToUtf8ByteArray(revocationPassphrase)));
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0007D364 File Offset: 0x0007D364
		public EncryptedValue Build(X509Certificate holder)
		{
			EncryptedValue result;
			try
			{
				result = this.EncryptData(this.PadData(holder.GetEncoded()));
			}
			catch (IOException ex)
			{
				throw new CrmfException("cannot encode certificate: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0007D3B4 File Offset: 0x0007D3B4
		public EncryptedValue Build(PrivateKeyInfo privateKeyInfo)
		{
			Pkcs8EncryptedPrivateKeyInfoBuilder pkcs8EncryptedPrivateKeyInfoBuilder = new Pkcs8EncryptedPrivateKeyInfoBuilder(privateKeyInfo);
			AlgorithmIdentifier privateKeyAlgorithm = privateKeyInfo.PrivateKeyAlgorithm;
			AlgorithmIdentifier symmAlg = (AlgorithmIdentifier)this.encryptor.AlgorithmDetails;
			EncryptedValue result;
			try
			{
				Pkcs8EncryptedPrivateKeyInfo pkcs8EncryptedPrivateKeyInfo = pkcs8EncryptedPrivateKeyInfoBuilder.Build(this.encryptor);
				DerBitString encSymmKey = new DerBitString(this.wrapper.Wrap(((KeyParameter)this.encryptor.Key).GetKey()).Collect());
				AlgorithmIdentifier keyAlg = (AlgorithmIdentifier)this.wrapper.AlgorithmDetails;
				Asn1OctetString valueHint = null;
				result = new EncryptedValue(privateKeyAlgorithm, symmAlg, encSymmKey, keyAlg, valueHint, new DerBitString(pkcs8EncryptedPrivateKeyInfo.GetEncryptedData()));
			}
			catch (Exception ex)
			{
				throw new CrmfException("cannot wrap key: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0007D47C File Offset: 0x0007D47C
		private EncryptedValue EncryptData(byte[] data)
		{
			MemoryOutputStream memoryOutputStream = new MemoryOutputStream();
			Stream stream = this.encryptor.BuildCipher(memoryOutputStream).Stream;
			try
			{
				stream.Write(data, 0, data.Length);
				Platform.Dispose(stream);
			}
			catch (IOException ex)
			{
				throw new CrmfException("cannot process data: " + ex.Message, ex);
			}
			AlgorithmIdentifier intendedAlg = null;
			AlgorithmIdentifier symmAlg = (AlgorithmIdentifier)this.encryptor.AlgorithmDetails;
			DerBitString encSymmKey;
			try
			{
				encSymmKey = new DerBitString(this.wrapper.Wrap(((KeyParameter)this.encryptor.Key).GetKey()).Collect());
			}
			catch (Exception ex2)
			{
				throw new CrmfException("cannot wrap key: " + ex2.Message, ex2);
			}
			AlgorithmIdentifier keyAlg = (AlgorithmIdentifier)this.wrapper.AlgorithmDetails;
			Asn1OctetString valueHint = null;
			DerBitString encValue = new DerBitString(memoryOutputStream.ToArray());
			return new EncryptedValue(intendedAlg, symmAlg, encSymmKey, keyAlg, valueHint, encValue);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0007D580 File Offset: 0x0007D580
		private byte[] PadData(byte[] data)
		{
			if (this.padder != null)
			{
				return this.padder.GetPaddedData(data);
			}
			return data;
		}

		// Token: 0x04001006 RID: 4102
		private readonly IKeyWrapper wrapper;

		// Token: 0x04001007 RID: 4103
		private readonly ICipherBuilderWithKey encryptor;

		// Token: 0x04001008 RID: 4104
		private readonly IEncryptedValuePadder padder;
	}
}
