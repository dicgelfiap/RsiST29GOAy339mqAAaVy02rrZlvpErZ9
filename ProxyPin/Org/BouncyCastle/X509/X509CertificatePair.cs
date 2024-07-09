using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000719 RID: 1817
	public class X509CertificatePair
	{
		// Token: 0x06003FA5 RID: 16293 RVA: 0x0015CF7C File Offset: 0x0015CF7C
		public X509CertificatePair(X509Certificate forward, X509Certificate reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x0015CF94 File Offset: 0x0015CF94
		public X509CertificatePair(CertificatePair pair)
		{
			if (pair.Forward != null)
			{
				this.forward = new X509Certificate(pair.Forward);
			}
			if (pair.Reverse != null)
			{
				this.reverse = new X509Certificate(pair.Reverse);
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0015CFD4 File Offset: 0x0015CFD4
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				X509CertificateStructure x509CertificateStructure = null;
				X509CertificateStructure x509CertificateStructure2 = null;
				if (this.forward != null)
				{
					x509CertificateStructure = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.forward.GetEncoded()));
					if (x509CertificateStructure == null)
					{
						throw new CertificateEncodingException("unable to get encoding for forward");
					}
				}
				if (this.reverse != null)
				{
					x509CertificateStructure2 = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.reverse.GetEncoded()));
					if (x509CertificateStructure2 == null)
					{
						throw new CertificateEncodingException("unable to get encoding for reverse");
					}
				}
				derEncoded = new CertificatePair(x509CertificateStructure, x509CertificateStructure2).GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CertificateEncodingException(ex.Message, ex);
			}
			return derEncoded;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x0015D07C File Offset: 0x0015D07C
		public X509Certificate Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x0015D084 File Offset: 0x0015D084
		public X509Certificate Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x0015D08C File Offset: 0x0015D08C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && object.Equals(this.forward, x509CertificatePair.forward) && object.Equals(this.reverse, x509CertificatePair.reverse);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x0015D0E0 File Offset: 0x0015D0E0
		public override int GetHashCode()
		{
			int num = -1;
			if (this.forward != null)
			{
				num ^= this.forward.GetHashCode();
			}
			if (this.reverse != null)
			{
				num *= 17;
				num ^= this.reverse.GetHashCode();
			}
			return num;
		}

		// Token: 0x040020A7 RID: 8359
		private readonly X509Certificate forward;

		// Token: 0x040020A8 RID: 8360
		private readonly X509Certificate reverse;
	}
}
