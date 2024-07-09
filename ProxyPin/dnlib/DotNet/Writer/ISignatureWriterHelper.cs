using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D8 RID: 2264
	[ComVisible(true)]
	public interface ISignatureWriterHelper : IWriterError
	{
		// Token: 0x06005816 RID: 22550
		uint ToEncodedToken(ITypeDefOrRef typeDefOrRef);
	}
}
