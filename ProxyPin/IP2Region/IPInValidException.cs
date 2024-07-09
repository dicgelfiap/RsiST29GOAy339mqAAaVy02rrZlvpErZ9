using System;
using System.Runtime.InteropServices;

namespace IP2Region
{
	// Token: 0x02000A5B RID: 2651
	[ComVisible(true)]
	public class IPInValidException : Exception
	{
		// Token: 0x06006805 RID: 26629 RVA: 0x001FAC38 File Offset: 0x001FAC38
		public IPInValidException() : base("IP Illigel. Please input a valid IP.")
		{
		}

		// Token: 0x040034F8 RID: 13560
		private const string ERROR_MSG = "IP Illigel. Please input a valid IP.";
	}
}
