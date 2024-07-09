using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.IO
{
	// Token: 0x02000761 RID: 1889
	internal sealed class AlignedNativeMemoryDataStream : DataStream
	{
		// Token: 0x06004210 RID: 16912 RVA: 0x00164764 File Offset: 0x00164764
		public unsafe AlignedNativeMemoryDataStream(byte* data)
		{
			this.data = data;
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x00164774 File Offset: 0x00164774
		public unsafe override void ReadBytes(uint offset, void* destination, int length)
		{
			byte* ptr = this.data + offset;
			byte* ptr2 = (byte*)destination;
			int num = length / 4;
			length %= 4;
			for (int i = 0; i < num; i++)
			{
				*ptr2 = *ptr;
				ptr2++;
				ptr++;
				*ptr2 = *ptr;
				ptr2++;
				ptr++;
				*ptr2 = *ptr;
				ptr2++;
				ptr++;
				*ptr2 = *ptr;
				ptr2++;
				ptr++;
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

		// Token: 0x06004212 RID: 16914 RVA: 0x001647F8 File Offset: 0x001647F8
		public unsafe override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length)
		{
			Marshal.Copy((IntPtr)((void*)(this.data + offset)), destination, destinationIndex, length);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00164814 File Offset: 0x00164814
		public unsafe override byte ReadByte(uint offset)
		{
			return this.data[offset];
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00164820 File Offset: 0x00164820
		public unsafe override ushort ReadUInt16(uint offset)
		{
			byte* ptr = this.data + offset;
			return (ushort)((int)(*(ptr++)) | (int)(*ptr) << 8);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x00164848 File Offset: 0x00164848
		public unsafe override uint ReadUInt32(uint offset)
		{
			byte* ptr = this.data + offset;
			return (uint)((int)(*(ptr++)) | (int)(*(ptr++)) << 8 | (int)(*(ptr++)) << 16 | (int)(*ptr) << 24);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00164884 File Offset: 0x00164884
		public unsafe override ulong ReadUInt64(uint offset)
		{
			byte* ptr = this.data + offset;
			return (ulong)(*(ptr++)) | (ulong)(*(ptr++)) << 8 | (ulong)(*(ptr++)) << 16 | (ulong)(*(ptr++)) << 24 | (ulong)(*(ptr++)) << 32 | (ulong)(*(ptr++)) << 40 | (ulong)(*(ptr++)) << 48 | (ulong)(*ptr) << 56;
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x001648F0 File Offset: 0x001648F0
		public unsafe override float ReadSingle(uint offset)
		{
			byte* ptr = this.data + offset;
			uint num = (uint)((int)(*(ptr++)) | (int)(*(ptr++)) << 8 | (int)(*(ptr++)) << 16 | (int)(*ptr) << 24);
			return *(float*)(&num);
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00164930 File Offset: 0x00164930
		public unsafe override double ReadDouble(uint offset)
		{
			byte* ptr = this.data + offset;
			ulong num = (ulong)(*(ptr++)) | (ulong)(*(ptr++)) << 8 | (ulong)(*(ptr++)) << 16 | (ulong)(*(ptr++)) << 24 | (ulong)(*(ptr++)) << 32 | (ulong)(*(ptr++)) << 40 | (ulong)(*(ptr++)) << 48 | (ulong)(*ptr) << 56;
			return *(double*)(&num);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x001649A0 File Offset: 0x001649A0
		public unsafe override string ReadUtf16String(uint offset, int chars)
		{
			return new string((char*)(this.data + offset), 0, chars);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x001649B4 File Offset: 0x001649B4
		public unsafe override string ReadString(uint offset, int length, Encoding encoding)
		{
			return new string((sbyte*)(this.data + offset), 0, length, encoding);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x001649C8 File Offset: 0x001649C8
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

		// Token: 0x0400237F RID: 9087
		private unsafe readonly byte* data;
	}
}
