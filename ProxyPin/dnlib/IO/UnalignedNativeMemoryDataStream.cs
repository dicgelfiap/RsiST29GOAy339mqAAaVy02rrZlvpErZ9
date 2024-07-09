using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x02000772 RID: 1906
	internal sealed class UnalignedNativeMemoryDataStream : DataStream
	{
		// Token: 0x060042D4 RID: 17108 RVA: 0x0016653C File Offset: 0x0016653C
		public unsafe UnalignedNativeMemoryDataStream(byte* data)
		{
			this.data = data;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0016654C File Offset: 0x0016654C
		public unsafe override void ReadBytes(uint offset, void* destination, int length)
		{
			byte* ptr = this.data + offset;
			byte* ptr2 = (byte*)destination;
			int num = length / 4;
			length %= 4;
			for (int i = 0; i < num; i++)
			{
				*(int*)ptr2 = (int)(*(uint*)ptr);
				ptr2 += 4;
				ptr += 4;
			}
			int j = 0;
			while (j < length)
			{
				*ptr2 = *ptr;
				j++;
				ptr++;
				ptr2++;
			}
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x001665AC File Offset: 0x001665AC
		public unsafe override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length)
		{
			Marshal.Copy((IntPtr)((void*)(this.data + offset)), destination, destinationIndex, length);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x001665C8 File Offset: 0x001665C8
		public unsafe override byte ReadByte(uint offset)
		{
			return this.data[offset];
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x001665D4 File Offset: 0x001665D4
		public unsafe override ushort ReadUInt16(uint offset)
		{
			return *(ushort*)(this.data + offset);
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x001665E0 File Offset: 0x001665E0
		public unsafe override uint ReadUInt32(uint offset)
		{
			return *(uint*)(this.data + offset);
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x001665EC File Offset: 0x001665EC
		public unsafe override ulong ReadUInt64(uint offset)
		{
			return (ulong)(*(long*)(this.data + offset));
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001665F8 File Offset: 0x001665F8
		public unsafe override float ReadSingle(uint offset)
		{
			return *(float*)(this.data + offset);
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x00166604 File Offset: 0x00166604
		public unsafe override double ReadDouble(uint offset)
		{
			return *(double*)(this.data + offset);
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x00166610 File Offset: 0x00166610
		public unsafe override Guid ReadGuid(uint offset)
		{
			return *(Guid*)(this.data + offset);
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x00166620 File Offset: 0x00166620
		public unsafe override string ReadUtf16String(uint offset, int chars)
		{
			return new string((char*)(this.data + offset), 0, chars);
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00166634 File Offset: 0x00166634
		public unsafe override string ReadString(uint offset, int length, Encoding encoding)
		{
			return new string((sbyte*)(this.data + offset), 0, length, encoding);
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x00166648 File Offset: 0x00166648
		public unsafe override bool TryGetOffsetOf(uint offset, uint endOffset, byte value, out uint valueOffset)
		{
			byte* ptr = this.data;
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

		// Token: 0x0400239F RID: 9119
		private unsafe readonly byte* data;
	}
}
