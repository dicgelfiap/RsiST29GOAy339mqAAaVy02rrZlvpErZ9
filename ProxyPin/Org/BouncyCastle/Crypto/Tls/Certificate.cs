using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D1 RID: 1233
	public class Certificate
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x000CFEB4 File Offset: 0x000CFEB4
		public Certificate(X509CertificateStructure[] certificateList)
		{
			if (certificateList == null)
			{
				throw new ArgumentNullException("certificateList");
			}
			this.mCertificateList = certificateList;
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000CFED4 File Offset: 0x000CFED4
		public virtual X509CertificateStructure[] GetCertificateList()
		{
			return this.CloneCertificateList();
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000CFEDC File Offset: 0x000CFEDC
		public virtual X509CertificateStructure GetCertificateAt(int index)
		{
			return this.mCertificateList[index];
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000CFEEC File Offset: 0x000CFEEC
		public virtual int Length
		{
			get
			{
				return this.mCertificateList.Length;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000CFEF8 File Offset: 0x000CFEF8
		public virtual bool IsEmpty
		{
			get
			{
				return this.mCertificateList.Length == 0;
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000CFF08 File Offset: 0x000CFF08
		public virtual void Encode(Stream output)
		{
			IList list = Platform.CreateArrayList(this.mCertificateList.Length);
			int num = 0;
			foreach (X509CertificateStructure asn1Encodable in this.mCertificateList)
			{
				byte[] encoded = asn1Encodable.GetEncoded("DER");
				list.Add(encoded);
				num += encoded.Length + 3;
			}
			TlsUtilities.CheckUint24(num);
			TlsUtilities.WriteUint24(num, output);
			foreach (object obj in list)
			{
				byte[] buf = (byte[])obj;
				TlsUtilities.WriteOpaque24(buf, output);
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000CFFCC File Offset: 0x000CFFCC
		public static Certificate Parse(Stream input)
		{
			int num = TlsUtilities.ReadUint24(input);
			if (num == 0)
			{
				return Certificate.EmptyChain;
			}
			byte[] buffer = TlsUtilities.ReadFully(num, input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				byte[] encoding = TlsUtilities.ReadOpaque24(memoryStream);
				Asn1Object obj = TlsUtilities.ReadAsn1Object(encoding);
				list.Add(X509CertificateStructure.GetInstance(obj));
			}
			X509CertificateStructure[] array = new X509CertificateStructure[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = (X509CertificateStructure)list[i];
			}
			return new Certificate(array);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000D007C File Offset: 0x000D007C
		protected virtual X509CertificateStructure[] CloneCertificateList()
		{
			return (X509CertificateStructure[])this.mCertificateList.Clone();
		}

		// Token: 0x040017DA RID: 6106
		public static readonly Certificate EmptyChain = new Certificate(new X509CertificateStructure[0]);

		// Token: 0x040017DB RID: 6107
		protected readonly X509CertificateStructure[] mCertificateList;
	}
}
