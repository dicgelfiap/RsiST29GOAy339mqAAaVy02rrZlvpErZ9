using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C3A RID: 3130
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProtoPartialMemberAttribute : ProtoMemberAttribute
	{
		// Token: 0x06007C0F RID: 31759 RVA: 0x0024765C File Offset: 0x0024765C
		public ProtoPartialMemberAttribute(int tag, string memberName) : base(tag)
		{
			if (string.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.MemberName = memberName;
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x06007C10 RID: 31760 RVA: 0x00247684 File Offset: 0x00247684
		// (set) Token: 0x06007C11 RID: 31761 RVA: 0x0024768C File Offset: 0x0024768C
		public string MemberName { get; private set; }
	}
}
