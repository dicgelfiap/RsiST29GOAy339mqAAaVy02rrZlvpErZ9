using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002DA RID: 730
	public class CmsContentInfoParser
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x00073938 File Offset: 0x00073938
		protected CmsContentInfoParser(Stream data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
			try
			{
				Asn1StreamParser asn1StreamParser = new Asn1StreamParser(data);
				this.contentInfo = new ContentInfoParser((Asn1SequenceParser)asn1StreamParser.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Unexpected object reading content.", e2);
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000739BC File Offset: 0x000739BC
		public void Close()
		{
			Platform.Dispose(this.data);
		}

		// Token: 0x04000F12 RID: 3858
		protected ContentInfoParser contentInfo;

		// Token: 0x04000F13 RID: 3859
		protected Stream data;
	}
}
