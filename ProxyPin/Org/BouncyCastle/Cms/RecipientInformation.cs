using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000302 RID: 770
	public abstract class RecipientInformation
	{
		// Token: 0x06001745 RID: 5957 RVA: 0x00079B38 File Offset: 0x00079B38
		internal RecipientInformation(AlgorithmIdentifier keyEncAlg, CmsSecureReadable secureReadable)
		{
			this.keyEncAlg = keyEncAlg;
			this.secureReadable = secureReadable;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00079B5C File Offset: 0x00079B5C
		internal string GetContentAlgorithmName()
		{
			AlgorithmIdentifier algorithm = this.secureReadable.Algorithm;
			return algorithm.Algorithm.Id;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00079B84 File Offset: 0x00079B84
		public RecipientID RecipientID
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00079B8C File Offset: 0x00079B8C
		public AlgorithmIdentifier KeyEncryptionAlgorithmID
		{
			get
			{
				return this.keyEncAlg;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00079B94 File Offset: 0x00079B94
		public string KeyEncryptionAlgOid
		{
			get
			{
				return this.keyEncAlg.Algorithm.Id;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00079BA8 File Offset: 0x00079BA8
		public Asn1Object KeyEncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.keyEncAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00079BD4 File Offset: 0x00079BD4
		internal CmsTypedStream GetContentFromSessionKey(KeyParameter sKey)
		{
			CmsReadable readable = this.secureReadable.GetReadable(sKey);
			CmsTypedStream result;
			try
			{
				result = new CmsTypedStream(readable.GetInputStream());
			}
			catch (IOException e)
			{
				throw new CmsException("error getting .", e);
			}
			return result;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00079C20 File Offset: 0x00079C20
		public byte[] GetContent(ICipherParameters key)
		{
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(this.GetContentStream(key).ContentStream);
			}
			catch (IOException arg)
			{
				throw new Exception("unable to parse internal stream: " + arg);
			}
			return result;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00079C68 File Offset: 0x00079C68
		public byte[] GetMac()
		{
			if (this.resultMac == null)
			{
				object cryptoObject = this.secureReadable.CryptoObject;
				if (cryptoObject is IMac)
				{
					this.resultMac = MacUtilities.DoFinal((IMac)cryptoObject);
				}
			}
			return Arrays.Clone(this.resultMac);
		}

		// Token: 0x0600174E RID: 5966
		public abstract CmsTypedStream GetContentStream(ICipherParameters key);

		// Token: 0x04000FA9 RID: 4009
		internal RecipientID rid = new RecipientID();

		// Token: 0x04000FAA RID: 4010
		internal AlgorithmIdentifier keyEncAlg;

		// Token: 0x04000FAB RID: 4011
		internal CmsSecureReadable secureReadable;

		// Token: 0x04000FAC RID: 4012
		private byte[] resultMac;
	}
}
