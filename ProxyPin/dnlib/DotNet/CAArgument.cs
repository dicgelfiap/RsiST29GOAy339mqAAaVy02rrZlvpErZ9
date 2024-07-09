using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000798 RID: 1944
	[ComVisible(true)]
	public struct CAArgument : ICloneable
	{
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x0016D9B0 File Offset: 0x0016D9B0
		// (set) Token: 0x06004579 RID: 17785 RVA: 0x0016D9B8 File Offset: 0x0016D9B8
		public TypeSig Type
		{
			readonly get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x0016D9C4 File Offset: 0x0016D9C4
		// (set) Token: 0x0600457B RID: 17787 RVA: 0x0016D9CC File Offset: 0x0016D9CC
		public object Value
		{
			readonly get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x0016D9D8 File Offset: 0x0016D9D8
		public CAArgument(TypeSig type)
		{
			this.type = type;
			this.value = null;
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x0016D9E8 File Offset: 0x0016D9E8
		public CAArgument(TypeSig type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x0016D9F8 File Offset: 0x0016D9F8
		readonly object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x0016DA08 File Offset: 0x0016DA08
		public readonly CAArgument Clone()
		{
			object obj = this.value;
			if (obj is CAArgument)
			{
				obj = ((CAArgument)obj).Clone();
			}
			else
			{
				IList<CAArgument> list = obj as IList<CAArgument>;
				if (list != null)
				{
					List<CAArgument> list2 = new List<CAArgument>(list.Count);
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						list2.Add(list[i].Clone());
					}
					obj = list2;
				}
			}
			return new CAArgument(this.type, obj);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x0016DAA0 File Offset: 0x0016DAA0
		public override readonly string ToString()
		{
			return string.Format("{0} ({1})", this.value ?? "null", this.type);
		}

		// Token: 0x04002448 RID: 9288
		private TypeSig type;

		// Token: 0x04002449 RID: 9289
		private object value;
	}
}
