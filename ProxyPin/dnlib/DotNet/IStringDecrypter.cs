using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007E3 RID: 2019
	[ComVisible(true)]
	public interface IStringDecrypter
	{
		// Token: 0x060048CD RID: 18637
		string ReadUserString(uint token);
	}
}
