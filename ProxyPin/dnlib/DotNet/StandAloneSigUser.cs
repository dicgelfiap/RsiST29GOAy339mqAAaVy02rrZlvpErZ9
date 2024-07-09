using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000850 RID: 2128
	[ComVisible(true)]
	public class StandAloneSigUser : StandAloneSig
	{
		// Token: 0x06005065 RID: 20581 RVA: 0x0018FA94 File Offset: 0x0018FA94
		public StandAloneSigUser()
		{
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0018FA9C File Offset: 0x0018FA9C
		public StandAloneSigUser(LocalSig localSig)
		{
			this.signature = localSig;
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0018FAAC File Offset: 0x0018FAAC
		public StandAloneSigUser(MethodSig methodSig)
		{
			this.signature = methodSig;
		}
	}
}
