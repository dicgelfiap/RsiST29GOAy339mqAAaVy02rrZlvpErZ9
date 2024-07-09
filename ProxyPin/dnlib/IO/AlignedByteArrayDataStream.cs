using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x02000760 RID: 1888
	internal sealed class AlignedByteArrayDataStream : DataStream
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x00164418 File Offset: 0x00164418
		public AlignedByteArrayDataStream(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x00164428 File Offset: 0x00164428
		public unsafe override void ReadBytes(uint offset, void* destination, int length)
		{
			Marshal.Copy(this.data, (int)offset, (IntPtr)destination, length);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x00164440 File Offset: 0x00164440
		public override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length)
		{
			Array.Copy(this.data, (int)offset, destination, destinationIndex, length);
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x00164454 File Offset: 0x00164454
		public override byte ReadByte(uint offset)
		{
			return this.data[(int)offset];
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x00164460 File Offset: 0x00164460
		public override ushort ReadUInt16(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			return (ushort)(array2[(int)offset] | (int)array[num] << 8);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x00164488 File Offset: 0x00164488
		public override uint ReadUInt32(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			return (uint)(array2[(int)offset] | (int)array[num++] << 8 | (int)array[num++] << 16 | (int)array[num] << 24);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x001644C8 File Offset: 0x001644C8
		public override ulong ReadUInt64(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			return array2[(int)offset] | (ulong)array[num++] << 8 | (ulong)array[num++] << 16 | (ulong)array[num++] << 24 | (ulong)array[num++] << 32 | (ulong)array[num++] << 40 | (ulong)array[num++] << 48 | (ulong)array[num] << 56;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x0016453C File Offset: 0x0016453C
		public unsafe override float ReadSingle(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			uint num2 = (uint)(array2[(int)offset] | (int)array[num++] << 8 | (int)array[num++] << 16 | (int)array[num] << 24);
			return *(float*)(&num2);
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x00164580 File Offset: 0x00164580
		public unsafe override double ReadDouble(uint offset)
		{
			byte[] array = this.data;
			byte[] array2 = array;
			int num = (int)(offset + 1U);
			ulong num2 = array2[(int)offset] | (ulong)array[num++] << 8 | (ulong)array[num++] << 16 | (ulong)array[num++] << 24 | (ulong)array[num++] << 32 | (ulong)array[num++] << 40 | (ulong)array[num++] << 48 | (ulong)array[num] << 56;
			return *(double*)(&num2);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x001645F8 File Offset: 0x001645F8
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

		// Token: 0x0600420E RID: 16910 RVA: 0x0016463C File Offset: 0x0016463C
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

		// Token: 0x0600420F RID: 16911 RVA: 0x00164680 File Offset: 0x00164680
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

		// Token: 0x0400237E RID: 9086
		private readonly byte[] data;
	}
}
