using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000510 RID: 1296
	public class OcspStatusRequest
	{
		// Token: 0x06002776 RID: 10102 RVA: 0x000D5B24 File Offset: 0x000D5B24
		public OcspStatusRequest(IList responderIDList, X509Extensions requestExtensions)
		{
			this.mResponderIDList = responderIDList;
			this.mRequestExtensions = requestExtensions;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000D5B3C File Offset: 0x000D5B3C
		public virtual IList ResponderIDList
		{
			get
			{
				return this.mResponderIDList;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000D5B44 File Offset: 0x000D5B44
		public virtual X509Extensions RequestExtensions
		{
			get
			{
				return this.mRequestExtensions;
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000D5B4C File Offset: 0x000D5B4C
		public virtual void Encode(Stream output)
		{
			if (this.mResponderIDList == null || this.mResponderIDList.Count < 1)
			{
				TlsUtilities.WriteUint16(0, output);
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int i = 0; i < this.mResponderIDList.Count; i++)
				{
					ResponderID responderID = (ResponderID)this.mResponderIDList[i];
					byte[] encoded = responderID.GetEncoded("DER");
					TlsUtilities.WriteOpaque16(encoded, memoryStream);
				}
				TlsUtilities.CheckUint16(memoryStream.Length);
				TlsUtilities.WriteUint16((int)memoryStream.Length, output);
				Streams.WriteBufTo(memoryStream, output);
			}
			if (this.mRequestExtensions == null)
			{
				TlsUtilities.WriteUint16(0, output);
				return;
			}
			byte[] encoded2 = this.mRequestExtensions.GetEncoded("DER");
			TlsUtilities.CheckUint16(encoded2.Length);
			TlsUtilities.WriteUint16(encoded2.Length, output);
			output.Write(encoded2, 0, encoded2.Length);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000D5C30 File Offset: 0x000D5C30
		public static OcspStatusRequest Parse(Stream input)
		{
			IList list = Platform.CreateArrayList();
			int num = TlsUtilities.ReadUint16(input);
			if (num > 0)
			{
				byte[] buffer = TlsUtilities.ReadFully(num, input);
				MemoryStream memoryStream = new MemoryStream(buffer, false);
				do
				{
					byte[] encoding = TlsUtilities.ReadOpaque16(memoryStream);
					ResponderID instance = ResponderID.GetInstance(TlsUtilities.ReadDerObject(encoding));
					list.Add(instance);
				}
				while (memoryStream.Position < memoryStream.Length);
			}
			X509Extensions requestExtensions = null;
			int num2 = TlsUtilities.ReadUint16(input);
			if (num2 > 0)
			{
				byte[] encoding2 = TlsUtilities.ReadFully(num2, input);
				requestExtensions = X509Extensions.GetInstance(TlsUtilities.ReadDerObject(encoding2));
			}
			return new OcspStatusRequest(list, requestExtensions);
		}

		// Token: 0x04001A05 RID: 6661
		protected readonly IList mResponderIDList;

		// Token: 0x04001A06 RID: 6662
		protected readonly X509Extensions mRequestExtensions;
	}
}
