using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000CDC RID: 3292
	internal struct NUInt
	{
		// Token: 0x06008548 RID: 34120 RVA: 0x002714AC File Offset: 0x002714AC
		private NUInt(uint value)
		{
			this._value = value;
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x002714B8 File Offset: 0x002714B8
		private NUInt(ulong value)
		{
			this._value = value;
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x002714C4 File Offset: 0x002714C4
		public static implicit operator NUInt(uint value)
		{
			return new NUInt(value);
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x002714CC File Offset: 0x002714CC
		public static implicit operator IntPtr(NUInt value)
		{
			return (IntPtr)value._value;
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x002714DC File Offset: 0x002714DC
		public static explicit operator NUInt(int value)
		{
			return new NUInt((uint)value);
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x002714E4 File Offset: 0x002714E4
		public unsafe static explicit operator void*(NUInt value)
		{
			return value._value;
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x002714EC File Offset: 0x002714EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static NUInt operator *(NUInt left, NUInt right)
		{
			if (sizeof(IntPtr) != 4)
			{
				return new NUInt(left._value * right._value);
			}
			return new NUInt(left._value * right._value);
		}

		// Token: 0x04003DBC RID: 15804
		private unsafe readonly void* _value;
	}
}
