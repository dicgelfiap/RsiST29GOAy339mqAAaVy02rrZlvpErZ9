using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000698 RID: 1688
	[Serializable]
	public class PkixCertPathValidatorException : GeneralSecurityException
	{
		// Token: 0x06003ACB RID: 15051 RVA: 0x0013CE04 File Offset: 0x0013CE04
		public PkixCertPathValidatorException()
		{
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0013CE14 File Offset: 0x0013CE14
		public PkixCertPathValidatorException(string message) : base(message)
		{
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0013CE24 File Offset: 0x0013CE24
		public PkixCertPathValidatorException(string message, Exception cause) : base(message)
		{
			this.cause = cause;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0013CE3C File Offset: 0x0013CE3C
		public PkixCertPathValidatorException(string message, Exception cause, PkixCertPath certPath, int index) : base(message)
		{
			if (certPath == null && index != -1)
			{
				throw new ArgumentNullException("certPath = null and index != -1");
			}
			if (index < -1 || (certPath != null && index >= certPath.Certificates.Count))
			{
				throw new IndexOutOfRangeException(" index < -1 or out of bound of certPath.getCertificates()");
			}
			this.cause = cause;
			this.certPath = certPath;
			this.index = index;
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x0013CEB8 File Offset: 0x0013CEB8
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (message != null)
				{
					return message;
				}
				if (this.cause != null)
				{
					return this.cause.Message;
				}
				return null;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x0013CEF0 File Offset: 0x0013CEF0
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x0013CEF8 File Offset: 0x0013CEF8
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001E6C RID: 7788
		private Exception cause;

		// Token: 0x04001E6D RID: 7789
		private PkixCertPath certPath;

		// Token: 0x04001E6E RID: 7790
		private int index = -1;
	}
}
