using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x020005AF RID: 1455
	public abstract class AbstractF2mFieldElement : ECFieldElement
	{
		// Token: 0x06002F07 RID: 12039 RVA: 0x000F6E60 File Offset: 0x000F6E60
		public virtual ECFieldElement HalfTrace()
		{
			int fieldSize = this.FieldSize;
			if ((fieldSize & 1) == 0)
			{
				throw new InvalidOperationException("Half-trace only defined for odd m");
			}
			int num = fieldSize + 1 >> 1;
			int i = 31 - Integers.NumberOfLeadingZeros(num);
			int num2 = 1;
			ECFieldElement ecfieldElement = this;
			while (i > 0)
			{
				ecfieldElement = ecfieldElement.SquarePow(num2 << 1).Add(ecfieldElement);
				num2 = num >> (--i & 31);
				if ((num2 & 1) != 0)
				{
					ecfieldElement = ecfieldElement.SquarePow(2).Add(this);
				}
			}
			return ecfieldElement;
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000F6EE4 File Offset: 0x000F6EE4
		public virtual bool HasFastTrace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000F6EE8 File Offset: 0x000F6EE8
		public virtual int Trace()
		{
			int fieldSize = this.FieldSize;
			int i = 31 - Integers.NumberOfLeadingZeros(fieldSize);
			int num = 1;
			ECFieldElement ecfieldElement = this;
			while (i > 0)
			{
				ecfieldElement = ecfieldElement.SquarePow(num).Add(ecfieldElement);
				num = fieldSize >> (--i & 31);
				if ((num & 1) != 0)
				{
					ecfieldElement = ecfieldElement.Square().Add(this);
				}
			}
			if (ecfieldElement.IsZero)
			{
				return 0;
			}
			if (ecfieldElement.IsOne)
			{
				return 1;
			}
			throw new InvalidOperationException("Internal error in trace calculation");
		}
	}
}
