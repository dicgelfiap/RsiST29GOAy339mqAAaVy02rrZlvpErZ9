using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AC5 RID: 2757
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06006DAD RID: 28077 RVA: 0x00213374 File Offset: 0x00213374
		// (set) Token: 0x06006DAE RID: 28078 RVA: 0x0021337C File Offset: 0x0021337C
		public Type MemberType { get; set; }

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06006DAF RID: 28079 RVA: 0x00213388 File Offset: 0x00213388
		// (set) Token: 0x06006DB0 RID: 28080 RVA: 0x00213390 File Offset: 0x00213390
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Func<object, object> Getter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06006DB1 RID: 28081 RVA: 0x0021339C File Offset: 0x0021339C
		// (set) Token: 0x06006DB2 RID: 28082 RVA: 0x002133A4 File Offset: 0x002133A4
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> Setter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }
	}
}
