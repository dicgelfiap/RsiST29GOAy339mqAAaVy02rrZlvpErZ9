using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C37 RID: 3127
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[ComVisible(true)]
	public class ProtoMapAttribute : Attribute
	{
		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06007BF0 RID: 31728 RVA: 0x002473D0 File Offset: 0x002473D0
		// (set) Token: 0x06007BF1 RID: 31729 RVA: 0x002473D8 File Offset: 0x002473D8
		public DataFormat KeyFormat { get; set; }

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06007BF2 RID: 31730 RVA: 0x002473E4 File Offset: 0x002473E4
		// (set) Token: 0x06007BF3 RID: 31731 RVA: 0x002473EC File Offset: 0x002473EC
		public DataFormat ValueFormat { get; set; }

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06007BF4 RID: 31732 RVA: 0x002473F8 File Offset: 0x002473F8
		// (set) Token: 0x06007BF5 RID: 31733 RVA: 0x00247400 File Offset: 0x00247400
		public bool DisableMap { get; set; }
	}
}
