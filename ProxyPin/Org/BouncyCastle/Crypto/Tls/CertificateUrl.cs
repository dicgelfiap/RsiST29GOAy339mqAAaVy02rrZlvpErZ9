using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D7 RID: 1239
	public class CertificateUrl
	{
		// Token: 0x0600263D RID: 9789 RVA: 0x000D054C File Offset: 0x000D054C
		public CertificateUrl(byte type, IList urlAndHashList)
		{
			if (!CertChainType.IsValid(type))
			{
				throw new ArgumentException("not a valid CertChainType value", "type");
			}
			if (urlAndHashList == null || urlAndHashList.Count < 1)
			{
				throw new ArgumentException("must have length > 0", "urlAndHashList");
			}
			this.mType = type;
			this.mUrlAndHashList = urlAndHashList;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000D05B0 File Offset: 0x000D05B0
		public virtual byte Type
		{
			get
			{
				return this.mType;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x000D05B8 File Offset: 0x000D05B8
		public virtual IList UrlAndHashList
		{
			get
			{
				return this.mUrlAndHashList;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000D05C0 File Offset: 0x000D05C0
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mType, output);
			CertificateUrl.ListBuffer16 listBuffer = new CertificateUrl.ListBuffer16();
			foreach (object obj in this.mUrlAndHashList)
			{
				UrlAndHash urlAndHash = (UrlAndHash)obj;
				urlAndHash.Encode(listBuffer);
			}
			listBuffer.EncodeTo(output);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000D063C File Offset: 0x000D063C
		public static CertificateUrl parse(TlsContext context, Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!CertChainType.IsValid(b))
			{
				throw new TlsFatalAlert(50);
			}
			int num = TlsUtilities.ReadUint16(input);
			if (num < 1)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] buffer = TlsUtilities.ReadFully(num, input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				UrlAndHash value = UrlAndHash.Parse(context, memoryStream);
				list.Add(value);
			}
			return new CertificateUrl(b, list);
		}

		// Token: 0x040017E7 RID: 6119
		protected readonly byte mType;

		// Token: 0x040017E8 RID: 6120
		protected readonly IList mUrlAndHashList;

		// Token: 0x02000E18 RID: 3608
		internal class ListBuffer16 : MemoryStream
		{
			// Token: 0x06008C3E RID: 35902 RVA: 0x002A2194 File Offset: 0x002A2194
			internal ListBuffer16()
			{
				TlsUtilities.WriteUint16(0, this);
			}

			// Token: 0x06008C3F RID: 35903 RVA: 0x002A21A4 File Offset: 0x002A21A4
			internal void EncodeTo(Stream output)
			{
				long num = this.Length - 2L;
				TlsUtilities.CheckUint16(num);
				this.Position = 0L;
				TlsUtilities.WriteUint16((int)num, this);
				Streams.WriteBufTo(this, output);
				Platform.Dispose(this);
			}
		}
	}
}
