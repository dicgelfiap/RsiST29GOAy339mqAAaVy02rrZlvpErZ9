using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007CA RID: 1994
	[ComVisible(true)]
	public interface IMemberRef : ICodedToken, IMDTokenProvider, IFullName, IOwnerModule, IIsTypeOrMethod
	{
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06004882 RID: 18562
		ITypeDefOrRef DeclaringType { get; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06004883 RID: 18563
		bool IsField { get; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06004884 RID: 18564
		bool IsTypeSpec { get; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06004885 RID: 18565
		bool IsTypeRef { get; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06004886 RID: 18566
		bool IsTypeDef { get; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06004887 RID: 18567
		bool IsMethodSpec { get; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06004888 RID: 18568
		bool IsMethodDef { get; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06004889 RID: 18569
		bool IsMemberRef { get; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600488A RID: 18570
		bool IsFieldDef { get; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600488B RID: 18571
		bool IsPropertyDef { get; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600488C RID: 18572
		bool IsEventDef { get; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600488D RID: 18573
		bool IsGenericParam { get; }
	}
}
