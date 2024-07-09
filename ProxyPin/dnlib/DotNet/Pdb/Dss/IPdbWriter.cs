using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000971 RID: 2417
	[ComVisible(true)]
	[Guid("98ECEE1E-752D-11d3-8D56-00C04F680B2B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IPdbWriter
	{
		// Token: 0x06005CD7 RID: 23767
		void _VtblGap1_4();

		// Token: 0x06005CD8 RID: 23768
		void GetSignatureAge(out uint sig, out uint age);
	}
}
