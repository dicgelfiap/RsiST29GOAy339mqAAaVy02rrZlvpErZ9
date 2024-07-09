using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200088D RID: 2189
	[ComVisible(true)]
	public struct ArrayWriter
	{
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060053C6 RID: 21446 RVA: 0x00198A28 File Offset: 0x00198A28
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00198A30 File Offset: 0x00198A30
		public ArrayWriter(byte[] data)
		{
			this.data = data;
			this.position = 0;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00198A40 File Offset: 0x00198A40
		public void WriteSByte(sbyte value)
		{
			byte[] array = this.data;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00198A6C File Offset: 0x00198A6C
		public void WriteByte(byte value)
		{
			byte[] array = this.data;
			int num = this.position;
			this.position = num + 1;
			array[num] = value;
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00198A98 File Offset: 0x00198A98
		public void WriteInt16(short value)
		{
			byte[] array = this.data;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.data;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x00198AE0 File Offset: 0x00198AE0
		public void WriteUInt16(ushort value)
		{
			byte[] array = this.data;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.data;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00198B28 File Offset: 0x00198B28
		public unsafe void WriteInt32(int value)
		{
			int num = this.position;
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
			*(int*)(ptr + num) = value;
			array = null;
			this.position = num + 4;
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x00198B78 File Offset: 0x00198B78
		public unsafe void WriteUInt32(uint value)
		{
			int num = this.position;
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
			*(int*)(ptr + num) = (int)value;
			array = null;
			this.position = num + 4;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00198BC8 File Offset: 0x00198BC8
		public unsafe void WriteInt64(long value)
		{
			int num = this.position;
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
			*(long*)(ptr + num) = value;
			array = null;
			this.position = num + 8;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00198C18 File Offset: 0x00198C18
		public unsafe void WriteUInt64(ulong value)
		{
			int num = this.position;
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
			*(long*)(ptr + num) = (long)value;
			array = null;
			this.position = num + 8;
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x00198C68 File Offset: 0x00198C68
		public unsafe void WriteSingle(float value)
		{
			int num = this.position;
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
			*(float*)(ptr + num) = value;
			array = null;
			this.position = num + 4;
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00198CB8 File Offset: 0x00198CB8
		public unsafe void WriteDouble(double value)
		{
			int num = this.position;
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
			*(double*)(ptr + num) = value;
			array = null;
			this.position = num + 8;
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00198D08 File Offset: 0x00198D08
		public void WriteBytes(byte[] source)
		{
			this.WriteBytes(source, 0, source.Length);
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x00198D18 File Offset: 0x00198D18
		public void WriteBytes(byte[] source, int index, int length)
		{
			Array.Copy(source, index, this.data, this.position, length);
			this.position += length;
		}

		// Token: 0x04002849 RID: 10313
		private readonly byte[] data;

		// Token: 0x0400284A RID: 10314
		private int position;
	}
}
