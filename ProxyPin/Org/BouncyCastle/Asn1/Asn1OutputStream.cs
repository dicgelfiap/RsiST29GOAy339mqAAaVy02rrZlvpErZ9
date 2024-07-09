using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200023F RID: 575
	public class Asn1OutputStream : DerOutputStream
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x0006871C File Offset: 0x0006871C
		public Asn1OutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00068728 File Offset: 0x00068728
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public override void WriteObject(object obj)
		{
			if (obj == null)
			{
				base.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not Asn1Encodable");
		}
	}
}
