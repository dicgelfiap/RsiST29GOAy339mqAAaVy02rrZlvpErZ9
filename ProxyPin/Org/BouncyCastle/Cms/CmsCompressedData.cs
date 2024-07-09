using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E0 RID: 736
	public class CmsCompressedData
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x0007405C File Offset: 0x0007405C
		public CmsCompressedData(byte[] compressedData) : this(CmsUtilities.ReadContentInfo(compressedData))
		{
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0007406C File Offset: 0x0007406C
		public CmsCompressedData(Stream compressedDataStream) : this(CmsUtilities.ReadContentInfo(compressedDataStream))
		{
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0007407C File Offset: 0x0007407C
		public CmsCompressedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0007408C File Offset: 0x0007408C
		public byte[] GetContent()
		{
			CompressedData instance = CompressedData.GetInstance(this.contentInfo.Content);
			ContentInfo encapContentInfo = instance.EncapContentInfo;
			Asn1OctetString asn1OctetString = (Asn1OctetString)encapContentInfo.Content;
			ZInputStream zinputStream = new ZInputStream(asn1OctetString.GetOctetStream());
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(zinputStream);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			finally
			{
				Platform.Dispose(zinputStream);
			}
			return result;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0007410C File Offset: 0x0007410C
		public byte[] GetContent(int limit)
		{
			CompressedData instance = CompressedData.GetInstance(this.contentInfo.Content);
			ContentInfo encapContentInfo = instance.EncapContentInfo;
			Asn1OctetString asn1OctetString = (Asn1OctetString)encapContentInfo.Content;
			ZInputStream inStream = new ZInputStream(new MemoryStream(asn1OctetString.GetOctets(), false));
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(inStream, limit);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			return result;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00074180 File Offset: 0x00074180
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00074188 File Offset: 0x00074188
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04000F2B RID: 3883
		internal ContentInfo contentInfo;
	}
}
