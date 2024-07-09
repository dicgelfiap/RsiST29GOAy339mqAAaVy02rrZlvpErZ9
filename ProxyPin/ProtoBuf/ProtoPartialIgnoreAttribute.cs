using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C35 RID: 3125
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProtoPartialIgnoreAttribute : ProtoIgnoreAttribute
	{
		// Token: 0x06007BE7 RID: 31719 RVA: 0x002472E0 File Offset: 0x002472E0
		public ProtoPartialIgnoreAttribute(string memberName)
		{
			if (string.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.MemberName = memberName;
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x00247308 File Offset: 0x00247308
		public string MemberName { get; }
	}
}
