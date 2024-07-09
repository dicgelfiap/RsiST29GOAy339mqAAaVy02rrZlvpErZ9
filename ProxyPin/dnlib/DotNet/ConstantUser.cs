using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200078E RID: 1934
	[ComVisible(true)]
	public class ConstantUser : Constant
	{
		// Token: 0x06004517 RID: 17687 RVA: 0x0016CA20 File Offset: 0x0016CA20
		public ConstantUser()
		{
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x0016CA28 File Offset: 0x0016CA28
		public ConstantUser(object value)
		{
			this.type = ConstantUser.GetElementType(value);
			this.value = value;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x0016CA44 File Offset: 0x0016CA44
		public ConstantUser(object value, ElementType type)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0016CA5C File Offset: 0x0016CA5C
		private static ElementType GetElementType(object value)
		{
			if (value == null)
			{
				return ElementType.Class;
			}
			switch (System.Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				return ElementType.Boolean;
			case TypeCode.Char:
				return ElementType.Char;
			case TypeCode.SByte:
				return ElementType.I1;
			case TypeCode.Byte:
				return ElementType.U1;
			case TypeCode.Int16:
				return ElementType.I2;
			case TypeCode.UInt16:
				return ElementType.U2;
			case TypeCode.Int32:
				return ElementType.I4;
			case TypeCode.UInt32:
				return ElementType.U4;
			case TypeCode.Int64:
				return ElementType.I8;
			case TypeCode.UInt64:
				return ElementType.U8;
			case TypeCode.Single:
				return ElementType.R4;
			case TypeCode.Double:
				return ElementType.R8;
			case TypeCode.String:
				return ElementType.String;
			}
			return ElementType.Void;
		}
	}
}
