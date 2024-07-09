using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000187 RID: 391
	public class NetscapeRevocationUrl : DerIA5String
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00052868 File Offset: 0x00052868
		public NetscapeRevocationUrl(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00052878 File Offset: 0x00052878
		public override string ToString()
		{
			return "NetscapeRevocationUrl: " + this.GetString();
		}
	}
}
