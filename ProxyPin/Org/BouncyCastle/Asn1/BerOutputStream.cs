using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000256 RID: 598
	public class BerOutputStream : DerOutputStream
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x0006A04C File Offset: 0x0006A04C
		public BerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006A058 File Offset: 0x0006A058
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
			throw new IOException("object not BerEncodable");
		}
	}
}
