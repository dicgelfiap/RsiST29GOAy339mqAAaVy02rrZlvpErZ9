using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C2 RID: 1986
	[ComVisible(true)]
	public interface IAssembly : IFullName
	{
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004859 RID: 18521
		// (set) Token: 0x0600485A RID: 18522
		Version Version { get; set; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600485B RID: 18523
		// (set) Token: 0x0600485C RID: 18524
		AssemblyAttributes Attributes { get; set; }

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600485D RID: 18525
		PublicKeyBase PublicKeyOrToken { get; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600485E RID: 18526
		// (set) Token: 0x0600485F RID: 18527
		UTF8String Culture { get; set; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004860 RID: 18528
		string FullNameToken { get; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004861 RID: 18529
		// (set) Token: 0x06004862 RID: 18530
		bool HasPublicKey { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06004863 RID: 18531
		// (set) Token: 0x06004864 RID: 18532
		AssemblyAttributes ProcessorArchitecture { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06004865 RID: 18533
		// (set) Token: 0x06004866 RID: 18534
		AssemblyAttributes ProcessorArchitectureFull { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06004867 RID: 18535
		bool IsProcessorArchitectureNone { get; }

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06004868 RID: 18536
		bool IsProcessorArchitectureMSIL { get; }

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004869 RID: 18537
		bool IsProcessorArchitectureX86 { get; }

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600486A RID: 18538
		bool IsProcessorArchitectureIA64 { get; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600486B RID: 18539
		bool IsProcessorArchitectureX64 { get; }

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600486C RID: 18540
		bool IsProcessorArchitectureARM { get; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600486D RID: 18541
		bool IsProcessorArchitectureNoPlatform { get; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600486E RID: 18542
		// (set) Token: 0x0600486F RID: 18543
		bool IsProcessorArchitectureSpecified { get; set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06004870 RID: 18544
		// (set) Token: 0x06004871 RID: 18545
		bool EnableJITcompileTracking { get; set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06004872 RID: 18546
		// (set) Token: 0x06004873 RID: 18547
		bool DisableJITcompileOptimizer { get; set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06004874 RID: 18548
		// (set) Token: 0x06004875 RID: 18549
		bool IsRetargetable { get; set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004876 RID: 18550
		// (set) Token: 0x06004877 RID: 18551
		AssemblyAttributes ContentType { get; set; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06004878 RID: 18552
		bool IsContentTypeDefault { get; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06004879 RID: 18553
		bool IsContentTypeWindowsRuntime { get; }
	}
}
