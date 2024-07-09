using System;
using System.Runtime.InteropServices;

namespace PeNet.Asn1
{
	// Token: 0x02000B7D RID: 2941
	[ComVisible(true)]
	public class Asn1ParsingException : Exception
	{
		// Token: 0x06007689 RID: 30345 RVA: 0x00237D58 File Offset: 0x00237D58
		public Asn1ParsingException(string message) : base(message)
		{
		}
	}
}
