using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C32 RID: 3122
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class ProtoEnumAttribute : Attribute
	{
		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x06007BDC RID: 31708 RVA: 0x00247270 File Offset: 0x00247270
		// (set) Token: 0x06007BDD RID: 31709 RVA: 0x00247278 File Offset: 0x00247278
		public int Value
		{
			get
			{
				return this.enumValue;
			}
			set
			{
				this.enumValue = value;
				this.hasValue = true;
			}
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x00247288 File Offset: 0x00247288
		public bool HasValue()
		{
			return this.hasValue;
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06007BDF RID: 31711 RVA: 0x00247290 File Offset: 0x00247290
		// (set) Token: 0x06007BE0 RID: 31712 RVA: 0x00247298 File Offset: 0x00247298
		public string Name { get; set; }

		// Token: 0x04003BDC RID: 15324
		private bool hasValue;

		// Token: 0x04003BDD RID: 15325
		private int enumValue;
	}
}
