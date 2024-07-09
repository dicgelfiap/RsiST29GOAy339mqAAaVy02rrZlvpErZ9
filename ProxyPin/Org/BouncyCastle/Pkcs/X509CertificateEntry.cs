using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000689 RID: 1673
	public class X509CertificateEntry : Pkcs12Entry
	{
		// Token: 0x06003A59 RID: 14937 RVA: 0x0013A95C File Offset: 0x0013A95C
		public X509CertificateEntry(X509Certificate cert) : base(Platform.CreateHashtable())
		{
			this.cert = cert;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x0013A970 File Offset: 0x0013A970
		[Obsolete]
		public X509CertificateEntry(X509Certificate cert, Hashtable attributes) : base(attributes)
		{
			this.cert = cert;
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x0013A980 File Offset: 0x0013A980
		public X509CertificateEntry(X509Certificate cert, IDictionary attributes) : base(attributes)
		{
			this.cert = cert;
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x0013A990 File Offset: 0x0013A990
		public X509Certificate Certificate
		{
			get
			{
				return this.cert;
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x0013A998 File Offset: 0x0013A998
		public override bool Equals(object obj)
		{
			X509CertificateEntry x509CertificateEntry = obj as X509CertificateEntry;
			return x509CertificateEntry != null && this.cert.Equals(x509CertificateEntry.cert);
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0013A9CC File Offset: 0x0013A9CC
		public override int GetHashCode()
		{
			return ~this.cert.GetHashCode();
		}

		// Token: 0x04001E47 RID: 7751
		private readonly X509Certificate cert;
	}
}
