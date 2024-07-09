using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CDD RID: 3293
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x04003DBD RID: 15805
		public T Data;
	}
}
