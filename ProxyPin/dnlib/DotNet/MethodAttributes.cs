using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200080C RID: 2060
	[Flags]
	[ComVisible(true)]
	public enum MethodAttributes : ushort
	{
		// Token: 0x04002572 RID: 9586
		MemberAccessMask = 7,
		// Token: 0x04002573 RID: 9587
		PrivateScope = 0,
		// Token: 0x04002574 RID: 9588
		CompilerControlled = 0,
		// Token: 0x04002575 RID: 9589
		Private = 1,
		// Token: 0x04002576 RID: 9590
		FamANDAssem = 2,
		// Token: 0x04002577 RID: 9591
		Assembly = 3,
		// Token: 0x04002578 RID: 9592
		Family = 4,
		// Token: 0x04002579 RID: 9593
		FamORAssem = 5,
		// Token: 0x0400257A RID: 9594
		Public = 6,
		// Token: 0x0400257B RID: 9595
		Static = 16,
		// Token: 0x0400257C RID: 9596
		Final = 32,
		// Token: 0x0400257D RID: 9597
		Virtual = 64,
		// Token: 0x0400257E RID: 9598
		HideBySig = 128,
		// Token: 0x0400257F RID: 9599
		VtableLayoutMask = 256,
		// Token: 0x04002580 RID: 9600
		ReuseSlot = 0,
		// Token: 0x04002581 RID: 9601
		NewSlot = 256,
		// Token: 0x04002582 RID: 9602
		CheckAccessOnOverride = 512,
		// Token: 0x04002583 RID: 9603
		Abstract = 1024,
		// Token: 0x04002584 RID: 9604
		SpecialName = 2048,
		// Token: 0x04002585 RID: 9605
		PinvokeImpl = 8192,
		// Token: 0x04002586 RID: 9606
		UnmanagedExport = 8,
		// Token: 0x04002587 RID: 9607
		RTSpecialName = 4096,
		// Token: 0x04002588 RID: 9608
		HasSecurity = 16384,
		// Token: 0x04002589 RID: 9609
		RequireSecObject = 32768
	}
}
