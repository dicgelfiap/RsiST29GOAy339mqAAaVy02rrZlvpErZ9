using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000188 RID: 392
	public class VerisignCzagExtension : DerIA5String
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x0005288C File Offset: 0x0005288C
		public VerisignCzagExtension(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0005289C File Offset: 0x0005289C
		public override string ToString()
		{
			return "VerisignCzagExtension: " + this.GetString();
		}
	}
}
