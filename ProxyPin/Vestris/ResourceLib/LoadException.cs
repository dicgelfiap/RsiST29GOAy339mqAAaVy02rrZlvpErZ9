using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D23 RID: 3363
	[ComVisible(true)]
	public class LoadException : Exception
	{
		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x060088CB RID: 35019 RVA: 0x002930A4 File Offset: 0x002930A4
		public Exception OuterException
		{
			get
			{
				return this._outerException;
			}
		}

		// Token: 0x060088CC RID: 35020 RVA: 0x002930AC File Offset: 0x002930AC
		public LoadException(string message, Exception innerException, Exception outerException) : base(message, innerException)
		{
			this._outerException = outerException;
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x060088CD RID: 35021 RVA: 0x002930C0 File Offset: 0x002930C0
		public override string Message
		{
			get
			{
				if (this._outerException == null)
				{
					return base.Message;
				}
				return string.Format("{0} {1}.", base.Message, this._outerException.Message);
			}
		}

		// Token: 0x04003EB6 RID: 16054
		private Exception _outerException;
	}
}
