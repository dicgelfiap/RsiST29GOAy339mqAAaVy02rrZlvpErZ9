using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AA6 RID: 2726
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06006C89 RID: 27785 RVA: 0x0020BC20 File Offset: 0x0020BC20
		public Type Type { get; }

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06006C8A RID: 27786 RVA: 0x0020BC28 File Offset: 0x0020BC28
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x06006C8B RID: 27787 RVA: 0x0020BC30 File Offset: 0x0020BC30
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}
