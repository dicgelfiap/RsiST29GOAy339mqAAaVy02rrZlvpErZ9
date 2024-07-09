using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CDB RID: 3291
	internal static class SpanHelpers
	{
		// Token: 0x06008519 RID: 34073 RVA: 0x0026D4DC File Offset: 0x0026D4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : object, IComparable<T>
		{
			if (comparable == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparable);
			}
			return SpanHelpers.BinarySearch<T, TComparable>(MemoryMarshal.GetReference<T>(span), span.Length, comparable);
		}

		// Token: 0x0600851A RID: 34074 RVA: 0x0026D504 File Offset: 0x0026D504
		public unsafe static int BinarySearch<T, TComparable>(ref T spanStart, int length, TComparable comparable) where TComparable : object, IComparable<T>
		{
			int i = 0;
			int num = length - 1;
			while (i <= num)
			{
				int num2 = (int)((uint)(num + i) >> 1);
				int num3 = comparable.CompareTo(*Unsafe.Add<T>(ref spanStart, num2));
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 > 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x0026D564 File Offset: 0x0026D564
		public static int IndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf(Unsafe.Add<byte>(ref searchSpace, num2), value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num2 + 1), ref second, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x0026D5DC File Offset: 0x0026D5DC
		public unsafe static int IndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x0600851D RID: 34077 RVA: 0x0026D62C File Offset: 0x0026D62C
		public unsafe static int LastIndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x0026D670 File Offset: 0x0026D670
		public unsafe static int IndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)(Vector<byte>.Count - num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector2;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_254;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
						{
							goto IL_25C;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
						{
							goto IL_26A;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
						{
							goto IL_278;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_254;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						vector2 = Vector.Equals<byte>(vector, Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr)));
						if (!Vector<byte>.Zero.Equals(vector2))
						{
							goto IL_213;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
					{
						goto IL_254;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
					{
						goto IL_25C;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
					{
						goto IL_26A;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
					{
						goto IL_278;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
					{
						goto IL_286;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
					{
						goto IL_294;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
					{
						goto IL_2A2;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
					{
						goto IL_2B0;
					}
					intPtr += 8;
				}
			}
			IL_213:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector2);
			IL_254:
			return (void*)intPtr;
			IL_25C:
			return (void*)(intPtr + 1);
			IL_26A:
			return (void*)(intPtr + 2);
			IL_278:
			return (void*)(intPtr + 3);
			IL_286:
			return (void*)(intPtr + 4);
			IL_294:
			return (void*)(intPtr + 5);
			IL_2A2:
			return (void*)(intPtr + 6);
			IL_2B0:
			return (void*)(intPtr + 7);
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x0026D940 File Offset: 0x0026D940
		public static int LastIndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num4 + 1), ref second, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06008520 RID: 34080 RVA: 0x0026D9B0 File Offset: 0x0026D9B0
		public unsafe static int LastIndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)((length & Vector<byte>.Count - 1) + num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector2;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
						{
							goto IL_28A;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
						{
							goto IL_27C;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
						{
							goto IL_26E;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_266;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						vector2 = Vector.Equals<byte>(vector, Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count)));
						if (!Vector<byte>.Zero.Equals(vector2))
						{
							goto IL_22B;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
					{
						goto IL_2C2;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
					{
						goto IL_2B4;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
					{
						goto IL_2A6;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
					{
						goto IL_298;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
					{
						goto IL_28A;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
					{
						goto IL_27C;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
					{
						goto IL_26E;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
					{
						break;
					}
				}
			}
			goto IL_266;
			IL_22B:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector2);
			IL_266:
			return (void*)intPtr;
			IL_26E:
			return (void*)(intPtr + 1);
			IL_27C:
			return (void*)(intPtr + 2);
			IL_28A:
			return (void*)(intPtr + 3);
			IL_298:
			return (void*)(intPtr + 4);
			IL_2A6:
			return (void*)(intPtr + 5);
			IL_2B4:
			return (void*)(intPtr + 6);
			IL_2C2:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06008521 RID: 34081 RVA: 0x0026DC90 File Offset: 0x0026DC90
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)(Vector<byte>.Count - num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector3;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_30E;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_316;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_324;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_332;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_30E;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						Vector<byte> left = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						vector3 = Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, vector), Vector.Equals<byte>(left, vector2));
						if (!Vector<byte>.Zero.Equals(vector3))
						{
							goto IL_2CD;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_30E;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_316;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_324;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_332;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_340;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_34E;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_35C;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_36A;
					}
					intPtr += 8;
				}
			}
			IL_2CD:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector3);
			IL_30E:
			return (void*)intPtr;
			IL_316:
			return (void*)(intPtr + 1);
			IL_324:
			return (void*)(intPtr + 2);
			IL_332:
			return (void*)(intPtr + 3);
			IL_340:
			return (void*)(intPtr + 4);
			IL_34E:
			return (void*)(intPtr + 5);
			IL_35C:
			return (void*)(intPtr + 6);
			IL_36A:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x0026E018 File Offset: 0x0026E018
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)(Vector<byte>.Count - num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector4;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3A2;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3AA;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3B8;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3C6;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3A2;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					Vector<byte> vector3 = SpanHelpers.GetVector(value2);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						Vector<byte> left = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						vector4 = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, vector), Vector.Equals<byte>(left, vector2)), Vector.Equals<byte>(left, vector3));
						if (!Vector<byte>.Zero.Equals(vector4))
						{
							goto IL_35D;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3A2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3AA;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3B8;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3C6;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3D4;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3E2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3F0;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3FE;
					}
					intPtr += 8;
				}
			}
			IL_35D:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector4);
			IL_3A2:
			return (void*)intPtr;
			IL_3AA:
			return (void*)(intPtr + 1);
			IL_3B8:
			return (void*)(intPtr + 2);
			IL_3C6:
			return (void*)(intPtr + 3);
			IL_3D4:
			return (void*)(intPtr + 4);
			IL_3E2:
			return (void*)(intPtr + 5);
			IL_3F0:
			return (void*)(intPtr + 6);
			IL_3FE:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x0026E434 File Offset: 0x0026E434
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)((length & Vector<byte>.Count - 1) + num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector3;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_347;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_339;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_32B;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2)
						{
							break;
						}
						if ((uint)value1 == num2)
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_323;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						Vector<byte> left = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count));
						vector3 = Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, vector), Vector.Equals<byte>(left, vector2));
						if (!Vector<byte>.Zero.Equals(vector3))
						{
							goto IL_2E5;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_37F;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_371;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_363;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_355;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_347;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_339;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_32B;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						break;
					}
				}
			}
			goto IL_323;
			IL_2E5:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector3);
			IL_323:
			return (void*)intPtr;
			IL_32B:
			return (void*)(intPtr + 1);
			IL_339:
			return (void*)(intPtr + 2);
			IL_347:
			return (void*)(intPtr + 3);
			IL_355:
			return (void*)(intPtr + 4);
			IL_363:
			return (void*)(intPtr + 5);
			IL_371:
			return (void*)(intPtr + 6);
			IL_37F:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06008524 RID: 34084 RVA: 0x0026E7D4 File Offset: 0x0026E7D4
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
				intPtr2 = (IntPtr)((length & Vector<byte>.Count - 1) + num & Vector<byte>.Count - 1);
			}
			Vector<byte> vector4;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3DB;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3CD;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3BF;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							break;
						}
						if ((uint)value2 == num2)
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_3B7;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					Vector<byte> vector3 = SpanHelpers.GetVector(value2);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						Vector<byte> left = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count));
						vector4 = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, vector), Vector.Equals<byte>(left, vector2)), Vector.Equals<byte>(left, vector3));
						if (!Vector<byte>.Zero.Equals(vector4))
						{
							goto IL_377;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_413;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_405;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3F7;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3E9;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3DB;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3CD;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_3BF;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						break;
					}
				}
			}
			goto IL_3B7;
			IL_377:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector4);
			IL_3B7:
			return (void*)intPtr;
			IL_3BF:
			return (void*)(intPtr + 1);
			IL_3CD:
			return (void*)(intPtr + 2);
			IL_3DB:
			return (void*)(intPtr + 3);
			IL_3E9:
			return (void*)(intPtr + 4);
			IL_3F7:
			return (void*)(intPtr + 5);
			IL_405:
			return (void*)(intPtr + 6);
			IL_413:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x0026EC08 File Offset: 0x0026EC08
		public unsafe static bool SequenceEqual(ref byte first, ref byte second, NUInt length)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)((void*)length);
				if (Vector.IsHardwareAccelerated && (void*)intPtr2 >= Vector<byte>.Count)
				{
					intPtr2 -= Vector<byte>.Count;
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += Vector<byte>.Count;
					}
					return Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				if ((void*)intPtr2 >= sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += sizeof(UIntPtr);
					}
					return Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				while ((void*)intPtr2 != (void*)intPtr)
				{
					if (*Unsafe.AddByteOffset<byte>(ref first, intPtr) != *Unsafe.AddByteOffset<byte>(ref second, intPtr))
					{
						return false;
					}
					intPtr += 1;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06008526 RID: 34086 RVA: 0x0026ED6C File Offset: 0x0026ED6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateFirstFoundByte(num);
		}

		// Token: 0x06008527 RID: 34087 RVA: 0x0026EDB4 File Offset: 0x0026EDB4
		public unsafe static int SequenceCompareTo(ref byte first, int firstLength, ref byte second, int secondLength)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr value = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)((void*)value);
				if (Vector.IsHardwareAccelerated && (void*)intPtr2 != Vector<byte>.Count)
				{
					intPtr2 -= Vector<byte>.Count;
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							break;
						}
						intPtr += Vector<byte>.Count;
					}
				}
				else if ((void*)intPtr2 != sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							break;
						}
						intPtr += sizeof(UIntPtr);
					}
				}
				while ((void*)value != (void*)intPtr)
				{
					int num = Unsafe.AddByteOffset<byte>(ref first, intPtr).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, intPtr));
					if (num != 0)
					{
						return num;
					}
					intPtr += 1;
				}
			}
			return firstLength - secondLength;
		}

		// Token: 0x06008528 RID: 34088 RVA: 0x0026EF04 File Offset: 0x0026EF04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateLastFoundByte(num);
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x0026EF50 File Offset: 0x0026EF50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(ulong match)
		{
			ulong num = match ^ match - 1UL;
			return (int)(num * 283686952306184UL >> 57);
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x0026EF78 File Offset: 0x0026EF78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(ulong match)
		{
			int num = 7;
			while (match > 0UL)
			{
				match <<= 8;
				num--;
			}
			return num;
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x0026EFA0 File Offset: 0x0026EFA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector<byte> GetVector(byte vectorByte)
		{
			return Vector.AsVectorByte<uint>(new Vector<uint>((uint)vectorByte * 16843009U));
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x0026EFB4 File Offset: 0x0026EFB4
		public unsafe static int SequenceCompareTo(ref char first, int firstLength, ref char second, int secondLength)
		{
			int result = firstLength - secondLength;
			if (!Unsafe.AreSame<char>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr2 = (IntPtr)0;
				if ((void*)intPtr >= sizeof(UIntPtr) / 2)
				{
					if (Vector.IsHardwareAccelerated && (void*)intPtr >= Vector<ushort>.Count)
					{
						IntPtr value = intPtr - Vector<ushort>.Count;
						while (!(Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
						{
							intPtr2 += Vector<ushort>.Count;
							if ((void*)value < (void*)intPtr2)
							{
								break;
							}
						}
					}
					while ((void*)intPtr >= (void*)(intPtr2 + sizeof(UIntPtr) / 2) && !(Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
					{
						intPtr2 += sizeof(UIntPtr) / 2;
					}
				}
				if (sizeof(UIntPtr) > 4 && (void*)intPtr >= (void*)(intPtr2 + 2) && Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) == Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2))))
				{
					intPtr2 += 2;
				}
				while ((void*)intPtr2 < (void*)intPtr)
				{
					int num = Unsafe.Add<char>(ref first, intPtr2).CompareTo(*Unsafe.Add<char>(ref second, intPtr2));
					if (num != 0)
					{
						return num;
					}
					intPtr2 += 1;
				}
			}
			return result;
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x0026F15C File Offset: 0x0026F15C
		public unsafe static int IndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + length;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					int num = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
					length = (Vector<ushort>.Count - num & Vector<ushort>.Count - 1);
				}
				Vector<ushort> vector;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							if (*ptr3 == value)
							{
								goto IL_145;
							}
							ptr3++;
						}
						if (!Vector.IsHardwareAccelerated || ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							vector = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr3));
							if (!Vector<ushort>.Zero.Equals(vector))
							{
								goto IL_10E;
							}
							ptr3 += Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3));
					}
					else
					{
						length -= 4;
						if (*ptr3 == value)
						{
							goto IL_145;
						}
						if (ptr3[1] == value)
						{
							goto IL_141;
						}
						if (ptr3[2] == value)
						{
							goto IL_13D;
						}
						if (ptr3[3] == value)
						{
							goto IL_139;
						}
						ptr3 += 4;
					}
				}
				IL_10E:
				return (int)((long)(ptr3 - ptr2)) + SpanHelpers.LocateFirstFoundChar(vector);
				IL_139:
				ptr3++;
				IL_13D:
				ptr3++;
				IL_141:
				ptr3++;
				IL_145:
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x0026F2BC File Offset: 0x0026F2BC
		public unsafe static int LastIndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + length;
				char* ptr4 = ptr2;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					length = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
				}
				char* ptr5;
				Vector<ushort> vector;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							ptr3--;
							if (*ptr3 == value)
							{
								goto IL_135;
							}
						}
						if (!Vector.IsHardwareAccelerated || ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							ptr5 = ptr3 - Vector<ushort>.Count;
							vector = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr5));
							if (!Vector<ushort>.Zero.Equals(vector))
							{
								goto IL_109;
							}
							ptr3 -= Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4));
					}
					else
					{
						length -= 4;
						ptr3 -= 4;
						if (ptr3[3] == value)
						{
							goto IL_151;
						}
						if (ptr3[2] == value)
						{
							goto IL_147;
						}
						if (ptr3[1] == value)
						{
							goto IL_13D;
						}
						if (*ptr3 == value)
						{
							goto IL_135;
						}
					}
				}
				IL_109:
				return (int)((long)(ptr5 - ptr4)) + SpanHelpers.LocateLastFoundChar(vector);
				IL_135:
				return (int)((long)(ptr3 - ptr4));
				IL_13D:
				return (int)((long)(ptr3 - ptr4)) + 1;
				IL_147:
				return (int)((long)(ptr3 - ptr4)) + 2;
				IL_151:
				return (int)((long)(ptr3 - ptr4)) + 3;
			}
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x0026F428 File Offset: 0x0026F428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateFirstFoundChar(num);
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x0026F470 File Offset: 0x0026F470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(ulong match)
		{
			ulong num = match ^ match - 1UL;
			return (int)(num * 4295098372UL >> 49);
		}

		// Token: 0x06008531 RID: 34097 RVA: 0x0026F498 File Offset: 0x0026F498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateLastFoundChar(num);
		}

		// Token: 0x06008532 RID: 34098 RVA: 0x0026F4E4 File Offset: 0x0026F4E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(ulong match)
		{
			int num = 3;
			while (match > 0UL)
			{
				match <<= 16;
				num--;
			}
			return num;
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x0026F50C File Offset: 0x0026F50C
		public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf<T>(Unsafe.Add<T>(ref searchSpace, num2), value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num2 + 1), ref second, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x0026F588 File Offset: 0x0026F588
		public unsafe static int IndexOf<T>(ref T searchSpace, T value, int length) where T : object, IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					IL_20E:
					return (void*)intPtr;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					IL_216:
					return (void*)(intPtr + 1);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					IL_224:
					return (void*)(intPtr + 2);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					IL_232:
					return (void*)(intPtr + 3);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)))
				{
					return (void*)(intPtr + 4);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)))
				{
					return (void*)(intPtr + 5);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)))
				{
					return (void*)(intPtr + 6);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_20E;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					goto IL_216;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					goto IL_224;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					goto IL_232;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_20E;
				}
				intPtr += 1;
				length--;
			}
			return -1;
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x0026F810 File Offset: 0x0026F810
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : object, IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2DD:
					return i + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2E1:
					return i + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2E5:
					return i + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2DD;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2E1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2E5;
				}
				i += 4;
			}
			while (i < length)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06008536 RID: 34102 RVA: 0x0026FB1C File Offset: 0x0026FB1C
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : object, IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3D7:
					return i + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3DB:
					return i + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3DF:
					return i + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3D7;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3DB;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3DF;
				}
				i += 4;
			}
			while (i < length)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06008537 RID: 34103 RVA: 0x0026FF20 File Offset: 0x0026FF20
		public unsafe static int IndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06008538 RID: 34104 RVA: 0x0026FF74 File Offset: 0x0026FF74
		public static int LastIndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf<T>(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num4 + 1), ref second, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x0026FFE8 File Offset: 0x0026FFE8
		public unsafe static int LastIndexOf<T>(ref T searchSpace, T value, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 7)))
				{
					return length + 7;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 6)))
				{
					return length + 6;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 5)))
				{
					return length + 5;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 4)))
				{
					return length + 4;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					IL_1D1:
					return length + 3;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					IL_1CD:
					return length + 2;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					IL_1C9:
					return length + 1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					goto IL_1D1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					goto IL_1CD;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					goto IL_1C9;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x0600853A RID: 34106 RVA: 0x002701E0 File Offset: 0x002701E0
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 7;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2E2:
					return length + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2DE:
					return length + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2DA:
					return length + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2E2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2DE;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2DA;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other))
				{
					return length;
				}
				if (value1.Equals(other))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x0600853B RID: 34107 RVA: 0x002704E8 File Offset: 0x002704E8
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 7;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3EF:
					return length + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3EA:
					return length + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3E5:
					return length + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3EF;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3EA;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3E5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
				if (value2.Equals(other))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x0600853C RID: 34108 RVA: 0x00270900 File Offset: 0x00270900
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x0600853D RID: 34109 RVA: 0x00270948 File Offset: 0x00270948
		public unsafe static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : object, IEquatable<T>
		{
			if (!Unsafe.AreSame<T>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				while (length >= 8)
				{
					length -= 8;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)) || !Unsafe.Add<T>(ref first, intPtr + 4).Equals(*Unsafe.Add<T>(ref second, intPtr + 4)) || !Unsafe.Add<T>(ref first, intPtr + 5).Equals(*Unsafe.Add<T>(ref second, intPtr + 5)) || !Unsafe.Add<T>(ref first, intPtr + 6).Equals(*Unsafe.Add<T>(ref second, intPtr + 6)) || !Unsafe.Add<T>(ref first, intPtr + 7).Equals(*Unsafe.Add<T>(ref second, intPtr + 7)))
					{
						return false;
					}
					intPtr += 8;
				}
				if (length >= 4)
				{
					length -= 4;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)))
					{
						return false;
					}
					intPtr += 4;
				}
				while (length > 0)
				{
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)))
					{
						return false;
					}
					intPtr += 1;
					length--;
				}
			}
			return true;
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x00270BF4 File Offset: 0x00270BF4
		public unsafe static int SequenceCompareTo<T>(ref T first, int firstLength, ref T second, int secondLength) where T : object, IComparable<T>
		{
			int num = firstLength;
			if (num > secondLength)
			{
				num = secondLength;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = Unsafe.Add<T>(ref first, i).CompareTo(*Unsafe.Add<T>(ref second, i));
				if (num2 != 0)
				{
					return num2;
				}
			}
			return firstLength.CompareTo(secondLength);
		}

		// Token: 0x0600853F RID: 34111 RVA: 0x00270C50 File Offset: 0x00270C50
		public unsafe static void CopyTo<T>(ref T dst, int dstLength, ref T src, int srcLength)
		{
			IntPtr value = Unsafe.ByteOffset<T>(ref src, Unsafe.Add<T>(ref src, srcLength));
			IntPtr value2 = Unsafe.ByteOffset<T>(ref dst, Unsafe.Add<T>(ref dst, dstLength));
			IntPtr value3 = Unsafe.ByteOffset<T>(ref src, ref dst);
			if (!((sizeof(IntPtr) == 4) ? ((int)value3 < (int)value || (int)value3 > -(int)value2) : ((long)value3 < (long)value || (long)value3 > -(long)value2)) && !SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ref byte source = ref Unsafe.As<T, byte>(ref dst);
				ref byte source2 = ref Unsafe.As<T, byte>(ref src);
				ulong num = (ulong)((long)value);
				uint num3;
				for (ulong num2 = 0UL; num2 < num; num2 += (ulong)num3)
				{
					num3 = ((num - num2 > (ulong)-1) ? uint.MaxValue : ((uint)(num - num2)));
					Unsafe.CopyBlock(Unsafe.Add<byte>(ref source, (IntPtr)((long)num2)), Unsafe.Add<byte>(ref source2, (IntPtr)((long)num2)), num3);
				}
				return;
			}
			bool flag = (sizeof(IntPtr) == 4) ? ((int)value3 > -(int)value2) : ((long)value3 > -(long)value2);
			int num4 = flag ? 1 : -1;
			int num5 = flag ? 0 : (srcLength - 1);
			int i;
			for (i = 0; i < (srcLength & -8); i += 8)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 4) = *Unsafe.Add<T>(ref src, num5 + num4 * 4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 5) = *Unsafe.Add<T>(ref src, num5 + num4 * 5);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 6) = *Unsafe.Add<T>(ref src, num5 + num4 * 6);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 7) = *Unsafe.Add<T>(ref src, num5 + num4 * 7);
				num5 += num4 * 8;
			}
			if (i < (srcLength & -4))
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				num5 += num4 * 4;
				i += 4;
			}
			while (i < srcLength)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				num5 += num4;
				i++;
			}
		}

		// Token: 0x06008540 RID: 34112 RVA: 0x00270FB4 File Offset: 0x00270FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static IntPtr Add<T>(this IntPtr start, int index)
		{
			if (sizeof(IntPtr) == 4)
			{
				uint num = (uint)(index * Unsafe.SizeOf<T>());
				return (IntPtr)((void*)((byte*)((void*)start) + num));
			}
			ulong num2 = (ulong)((long)index * (long)Unsafe.SizeOf<T>());
			return (IntPtr)((void*)((byte*)((void*)start) + num2));
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x00271000 File Offset: 0x00271000
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return SpanHelpers.PerTypeValues<T>.IsReferenceOrContainsReferences;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x00271008 File Offset: 0x00271008
		private static bool IsReferenceOrContainsReferencesCore(Type type)
		{
			if (type.GetTypeInfo().IsPrimitive)
			{
				return false;
			}
			if (!type.GetTypeInfo().IsValueType)
			{
				return true;
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (type.GetTypeInfo().IsEnum)
			{
				return false;
			}
			foreach (FieldInfo fieldInfo in type.GetTypeInfo().DeclaredFields)
			{
				if (!fieldInfo.IsStatic && SpanHelpers.IsReferenceOrContainsReferencesCore(fieldInfo.FieldType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008543 RID: 34115 RVA: 0x002710D0 File Offset: 0x002710D0
		public unsafe static void ClearLessThanPointerSized(byte* ptr, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned((void*)ptr, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)-1);
			Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
			num -= (ulong)num2;
			ptr += num2;
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)-1) ? uint.MaxValue : ((uint)num));
				Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
				ptr += num2;
				num -= (ulong)num2;
			}
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x0027114C File Offset: 0x0027114C
		public static void ClearLessThanPointerSized(ref byte b, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned(ref b, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)-1);
			Unsafe.InitBlockUnaligned(ref b, 0, num2);
			num -= (ulong)num2;
			long num3 = (long)((ulong)num2);
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)-1) ? uint.MaxValue : ((uint)num));
				ref byte startAddress = ref Unsafe.Add<byte>(ref b, (IntPtr)num3);
				Unsafe.InitBlockUnaligned(ref startAddress, 0, num2);
				num3 += (long)((ulong)num2);
				num -= (ulong)num2;
			}
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x002711D0 File Offset: 0x002711D0
		public unsafe static void ClearPointerSizedWithoutReferences(ref byte b, UIntPtr byteLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			while (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg64)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg64>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg64);
				intPtr += sizeof(SpanHelpers.Reg64);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg32)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg32>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg32);
				intPtr += sizeof(SpanHelpers.Reg32);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg16)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg16>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg16);
				intPtr += sizeof(SpanHelpers.Reg16);
			}
			if (intPtr.LessThanEqual(byteLength - 8))
			{
				*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, intPtr)) = 0L;
				intPtr += 8;
			}
			if (sizeof(IntPtr) == 4 && intPtr.LessThanEqual(byteLength - 4))
			{
				*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, intPtr)) = 0;
				intPtr += 4;
			}
		}

		// Token: 0x06008546 RID: 34118 RVA: 0x002712E8 File Offset: 0x002712E8
		public unsafe static void ClearPointerSizedWithReferences(ref IntPtr ip, UIntPtr pointerSizeLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			while ((intPtr2 = intPtr + 8).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 4) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 5) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 6) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 7) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 4).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 2).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr + 1).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr) = 0;
			}
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x00271478 File Offset: 0x00271478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool LessThanEqual(this IntPtr index, UIntPtr length)
		{
			if (sizeof(UIntPtr) != 4)
			{
				return (long)index <= (long)((ulong)length);
			}
			return (int)index <= (int)((uint)length);
		}

		// Token: 0x04003DBA RID: 15802
		private const ulong XorPowerOfTwoToHighByte = 283686952306184UL;

		// Token: 0x04003DBB RID: 15803
		private const ulong XorPowerOfTwoToHighChar = 4295098372UL;

		// Token: 0x020011C0 RID: 4544
		internal struct ComparerComparable<T, TComparer> : IComparable<T> where TComparer : object, IComparer<T>
		{
			// Token: 0x06009666 RID: 38502 RVA: 0x002CBF08 File Offset: 0x002CBF08
			public ComparerComparable(T value, TComparer comparer)
			{
				this._value = value;
				this._comparer = comparer;
			}

			// Token: 0x06009667 RID: 38503 RVA: 0x002CBF18 File Offset: 0x002CBF18
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(T other)
			{
				TComparer comparer = this._comparer;
				return comparer.Compare(this._value, other);
			}

			// Token: 0x04004C61 RID: 19553
			private readonly T _value;

			// Token: 0x04004C62 RID: 19554
			private readonly TComparer _comparer;
		}

		// Token: 0x020011C1 RID: 4545
		private struct Reg64
		{
		}

		// Token: 0x020011C2 RID: 4546
		private struct Reg32
		{
		}

		// Token: 0x020011C3 RID: 4547
		private struct Reg16
		{
		}

		// Token: 0x020011C4 RID: 4548
		public static class PerTypeValues<T>
		{
			// Token: 0x06009668 RID: 38504 RVA: 0x002CBF44 File Offset: 0x002CBF44
			private static IntPtr MeasureArrayAdjustment()
			{
				T[] array = new T[1];
				return Unsafe.ByteOffset<T>(ref Unsafe.As<Pinnable<T>>(array).Data, ref array[0]);
			}

			// Token: 0x04004C63 RID: 19555
			public static readonly bool IsReferenceOrContainsReferences = SpanHelpers.IsReferenceOrContainsReferencesCore(typeof(T));

			// Token: 0x04004C64 RID: 19556
			public static readonly T[] EmptyArray = new T[0];

			// Token: 0x04004C65 RID: 19557
			public static readonly IntPtr ArrayAdjustment = SpanHelpers.PerTypeValues<T>.MeasureArrayAdjustment();
		}
	}
}
