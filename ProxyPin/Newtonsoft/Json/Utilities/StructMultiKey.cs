using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ACC RID: 2764
	[NullableContext(1)]
	[Nullable(0)]
	[Newtonsoft.Json.IsReadOnly]
	internal struct StructMultiKey<[Nullable(2)] T1, [Nullable(2)] T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		// Token: 0x06006E13 RID: 28179 RVA: 0x00215148 File Offset: 0x00215148
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		// Token: 0x06006E14 RID: 28180 RVA: 0x00215158 File Offset: 0x00215158
		public override int GetHashCode()
		{
			T1 value = this.Value1;
			ref T1 ptr = ref value;
			T1 t = default(T1);
			int num;
			if (t == null)
			{
				t = value;
				ptr = ref t;
				if (t == null)
				{
					num = 0;
					goto IL_41;
				}
			}
			num = ptr.GetHashCode();
			IL_41:
			T2 value2 = this.Value2;
			ref T2 ptr2 = ref value2;
			T2 t2 = default(T2);
			int num2;
			if (t2 == null)
			{
				t2 = value2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					num2 = 0;
					goto IL_82;
				}
			}
			num2 = ptr2.GetHashCode();
			IL_82:
			return num ^ num2;
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x002151EC File Offset: 0x002151EC
		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> other = (StructMultiKey<T1, T2>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x00215220 File Offset: 0x00215220
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1
		})] StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		// Token: 0x0400370A RID: 14090
		public readonly T1 Value1;

		// Token: 0x0400370B RID: 14091
		public readonly T2 Value2;
	}
}
