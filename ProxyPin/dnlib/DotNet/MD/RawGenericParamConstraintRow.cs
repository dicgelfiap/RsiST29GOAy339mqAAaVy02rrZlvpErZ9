using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C7 RID: 2503
	[ComVisible(true)]
	public readonly struct RawGenericParamConstraintRow
	{
		// Token: 0x06005FA7 RID: 24487 RVA: 0x001C990C File Offset: 0x001C990C
		public RawGenericParamConstraintRow(uint Owner, uint Constraint)
		{
			this.Owner = Owner;
			this.Constraint = Constraint;
		}

		// Token: 0x1700140A RID: 5130
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index != 0)
				{
					if (index != 1)
					{
						result = 0U;
					}
					else
					{
						result = this.Constraint;
					}
				}
				else
				{
					result = this.Owner;
				}
				return result;
			}
		}

		// Token: 0x04002ED9 RID: 11993
		public readonly uint Owner;

		// Token: 0x04002EDA RID: 11994
		public readonly uint Constraint;
	}
}
