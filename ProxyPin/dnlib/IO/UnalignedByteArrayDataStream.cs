using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x02000771 RID: 1905
	internal sealed class UnalignedByteArrayDataStream : DataStream
	{
		// Token: 0x060042C7 RID: 17095 RVA: 0x00166230 File Offset: 0x00166230
		public UnalignedByteArrayDataStream(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x00166240 File Offset: 0x00166240
		public unsafe override void ReadBytes(uint offset, void* destination, int length)
		{
			Marshal.Copy(this.data, (int)offset, (IntPtr)destination, length);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x00166258 File Offset: 0x00166258
		public override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length)
		{
			Array.Copy(this.data, (int)offset, destination, destinationIndex, length);
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0016626C File Offset: 0x0016626C
		public override byte ReadByte(uint offset)
		{
			return this.data[(int)offset];
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x00166278 File Offset: 0x00166278
		public override ushort ReadUInt16(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			return (ushort)(array2[(int)offset] | (int)array[num] << 8);
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x001662A0 File Offset: 0x001662A0
		public unsafe override uint ReadUInt32(uint offset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return *(uint*)(ptr + offset);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x001662DC File Offset: 0x001662DC
		public unsafe override ulong ReadUInt64(uint offset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return (ulong)(*(long*)(ptr + offset));
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x00166318 File Offset: 0x00166318
		public unsafe override float ReadSingle(uint offset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return *(float*)(ptr + offset);
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x00166354 File Offset: 0x00166354
		public unsafe override double ReadDouble(uint offset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return *(double*)(ptr + offset);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x00166390 File Offset: 0x00166390
		public unsafe override Guid ReadGuid(uint offset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return *(Guid*)(ptr + offset);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x001663D0 File Offset: 0x001663D0
		public unsafe override string ReadUtf16String(uint offset, int chars)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return new string((char*)(ptr + offset), 0, chars);
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x00166414 File Offset: 0x00166414
		public unsafe override string ReadString(uint offset, int length, Encoding encoding)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return new string((sbyte*)(ptr + offset), 0, length, encoding);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x00166458 File Offset: 0x00166458
		public unsafe override bool TryGetOffsetOf(uint offset, uint endOffset, byte value, out uint valueOffset)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + offset;
			uint num = (endOffset - offset) / 4U;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				if (*ptr2 == value)
				{
					valueOffset = (uint)((long)(ptr2 - ptr));
					return true;
				}
				ptr2++;
				if (*ptr2 == value)
				{
					valueOffset = (uint)((long)(ptr2 - ptr));
					return true;
				}
				ptr2++;
				if (*ptr2 == value)
				{
					valueOffset = (uint)((long)(ptr2 - ptr));
					return true;
				}
				ptr2++;
				if (*ptr2 == value)
				{
					valueOffset = (uint)((long)(ptr2 - ptr));
					return true;
				}
				ptr2++;
			}
			byte* ptr3 = ptr + endOffset;
			while (ptr2 != ptr3)
			{
				if (*ptr2 == value)
				{
					valueOffset = (uint)((long)(ptr2 - ptr));
					return true;
				}
				ptr2++;
			}
			valueOffset = 0U;
			return false;
		}

		// Token: 0x0400239E RID: 9118
		private readonly byte[] data;
	}
}
