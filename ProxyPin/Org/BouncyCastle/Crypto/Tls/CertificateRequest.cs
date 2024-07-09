using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D2 RID: 1234
	public class CertificateRequest
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x000D00A4 File Offset: 0x000D00A4
		public CertificateRequest(byte[] certificateTypes, IList supportedSignatureAlgorithms, IList certificateAuthorities)
		{
			this.mCertificateTypes = certificateTypes;
			this.mSupportedSignatureAlgorithms = supportedSignatureAlgorithms;
			this.mCertificateAuthorities = certificateAuthorities;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000D00C4 File Offset: 0x000D00C4
		public virtual byte[] CertificateTypes
		{
			get
			{
				return this.mCertificateTypes;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000D00CC File Offset: 0x000D00CC
		public virtual IList SupportedSignatureAlgorithms
		{
			get
			{
				return this.mSupportedSignatureAlgorithms;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000D00D4 File Offset: 0x000D00D4
		public virtual IList CertificateAuthorities
		{
			get
			{
				return this.mCertificateAuthorities;
			}
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000D00DC File Offset: 0x000D00DC
		public virtual void Encode(Stream output)
		{
			if (this.mCertificateTypes == null || this.mCertificateTypes.Length == 0)
			{
				TlsUtilities.WriteUint8(0, output);
			}
			else
			{
				TlsUtilities.WriteUint8ArrayWithUint8Length(this.mCertificateTypes, output);
			}
			if (this.mSupportedSignatureAlgorithms != null)
			{
				TlsUtilities.EncodeSupportedSignatureAlgorithms(this.mSupportedSignatureAlgorithms, false, output);
			}
			if (this.mCertificateAuthorities == null || this.mCertificateAuthorities.Count < 1)
			{
				TlsUtilities.WriteUint16(0, output);
				return;
			}
			IList list = Platform.CreateArrayList(this.mCertificateAuthorities.Count);
			int num = 0;
			foreach (object obj in this.mCertificateAuthorities)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				byte[] encoded = asn1Encodable.GetEncoded("DER");
				list.Add(encoded);
				num += encoded.Length + 2;
			}
			TlsUtilities.CheckUint16(num);
			TlsUtilities.WriteUint16(num, output);
			foreach (object obj2 in list)
			{
				byte[] buf = (byte[])obj2;
				TlsUtilities.WriteOpaque16(buf, output);
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000D0238 File Offset: 0x000D0238
		public static CertificateRequest Parse(TlsContext context, Stream input)
		{
			int num = (int)TlsUtilities.ReadUint8(input);
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = TlsUtilities.ReadUint8(input);
			}
			IList supportedSignatureAlgorithms = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				supportedSignatureAlgorithms = TlsUtilities.ParseSupportedSignatureAlgorithms(false, input);
			}
			IList list = Platform.CreateArrayList();
			byte[] buffer = TlsUtilities.ReadOpaque16(input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			while (memoryStream.Position < memoryStream.Length)
			{
				byte[] encoding = TlsUtilities.ReadOpaque16(memoryStream);
				Asn1Object obj = TlsUtilities.ReadDerObject(encoding);
				list.Add(X509Name.GetInstance(obj));
			}
			return new CertificateRequest(array, supportedSignatureAlgorithms, list);
		}

		// Token: 0x040017DC RID: 6108
		protected readonly byte[] mCertificateTypes;

		// Token: 0x040017DD RID: 6109
		protected readonly IList mSupportedSignatureAlgorithms;

		// Token: 0x040017DE RID: 6110
		protected readonly IList mCertificateAuthorities;
	}
}
