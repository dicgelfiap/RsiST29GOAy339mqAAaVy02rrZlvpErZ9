using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000E4 RID: 228
	public abstract class DerStringBase : Asn1Object, IAsn1String
	{
		// Token: 0x06000872 RID: 2162
		public abstract string GetString();

		// Token: 0x06000873 RID: 2163 RVA: 0x000424E4 File Offset: 0x000424E4
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000424EC File Offset: 0x000424EC
		protected override int Asn1GetHashCode()
		{
			return this.GetString().GetHashCode();
		}
	}
}
