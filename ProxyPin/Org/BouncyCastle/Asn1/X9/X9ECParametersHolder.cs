using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x020000C6 RID: 198
	public abstract class X9ECParametersHolder
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000402A4 File Offset: 0x000402A4
		public X9ECParameters Parameters
		{
			get
			{
				X9ECParameters result;
				lock (this)
				{
					if (this.parameters == null)
					{
						this.parameters = this.CreateParameters();
					}
					result = this.parameters;
				}
				return result;
			}
		}

		// Token: 0x060007D6 RID: 2006
		protected abstract X9ECParameters CreateParameters();

		// Token: 0x04000595 RID: 1429
		private X9ECParameters parameters;
	}
}
