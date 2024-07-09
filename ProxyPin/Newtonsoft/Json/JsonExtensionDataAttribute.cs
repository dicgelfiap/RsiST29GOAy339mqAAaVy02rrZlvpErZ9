using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000A7F RID: 2687
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x060068BE RID: 26814 RVA: 0x001FC270 File Offset: 0x001FC270
		// (set) Token: 0x060068BF RID: 26815 RVA: 0x001FC278 File Offset: 0x001FC278
		public bool WriteData { get; set; }

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x060068C0 RID: 26816 RVA: 0x001FC284 File Offset: 0x001FC284
		// (set) Token: 0x060068C1 RID: 26817 RVA: 0x001FC28C File Offset: 0x001FC28C
		public bool ReadData { get; set; }

		// Token: 0x060068C2 RID: 26818 RVA: 0x001FC298 File Offset: 0x001FC298
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}
