using System;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000897 RID: 2199
	[ComVisible(true)]
	public sealed class DataWriter
	{
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600542A RID: 21546 RVA: 0x0019AD78 File Offset: 0x0019AD78
		internal Stream InternalStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x0019AD80 File Offset: 0x0019AD80
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x0019AD90 File Offset: 0x0019AD90
		public long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0019ADA0 File Offset: 0x0019ADA0
		public DataWriter(Stream stream)
		{
			if (stream == null)
			{
				DataWriter.ThrowArgumentNullException("stream");
			}
			this.stream = stream;
			this.buffer = new byte[8];
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0019ADCC File Offset: 0x0019ADCC
		private static void ThrowArgumentNullException(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0019ADD4 File Offset: 0x0019ADD4
		private static void ThrowArgumentOutOfRangeException(string message)
		{
			throw new ArgumentOutOfRangeException(message);
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0019ADDC File Offset: 0x0019ADDC
		public void WriteBoolean(bool value)
		{
			this.stream.WriteByte(value ? 1 : 0);
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0019ADF8 File Offset: 0x0019ADF8
		public void WriteSByte(sbyte value)
		{
			this.stream.WriteByte((byte)value);
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0019AE08 File Offset: 0x0019AE08
		public void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0019AE18 File Offset: 0x0019AE18
		public void WriteInt16(short value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			this.stream.Write(array, 0, 2);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0019AE4C File Offset: 0x0019AE4C
		public void WriteUInt16(ushort value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			this.stream.Write(array, 0, 2);
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0019AE80 File Offset: 0x0019AE80
		public void WriteInt32(int value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			array[2] = (byte)(value >> 16);
			array[3] = (byte)(value >> 24);
			this.stream.Write(array, 0, 4);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0019AEC4 File Offset: 0x0019AEC4
		public void WriteUInt32(uint value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			array[2] = (byte)(value >> 16);
			array[3] = (byte)(value >> 24);
			this.stream.Write(array, 0, 4);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0019AF08 File Offset: 0x0019AF08
		public void WriteInt64(long value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			array[2] = (byte)(value >> 16);
			array[3] = (byte)(value >> 24);
			array[4] = (byte)(value >> 32);
			array[5] = (byte)(value >> 40);
			array[6] = (byte)(value >> 48);
			array[7] = (byte)(value >> 56);
			this.stream.Write(array, 0, 8);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0019AF6C File Offset: 0x0019AF6C
		public void WriteUInt64(ulong value)
		{
			byte[] array = this.buffer;
			array[0] = (byte)value;
			array[1] = (byte)(value >> 8);
			array[2] = (byte)(value >> 16);
			array[3] = (byte)(value >> 24);
			array[4] = (byte)(value >> 32);
			array[5] = (byte)(value >> 40);
			array[6] = (byte)(value >> 48);
			array[7] = (byte)(value >> 56);
			this.stream.Write(array, 0, 8);
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0019AFD0 File Offset: 0x0019AFD0
		public unsafe void WriteSingle(float value)
		{
			uint num = *(uint*)(&value);
			byte[] array = this.buffer;
			array[0] = (byte)num;
			array[1] = (byte)(num >> 8);
			array[2] = (byte)(num >> 16);
			array[3] = (byte)(num >> 24);
			this.stream.Write(array, 0, 4);
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0019B018 File Offset: 0x0019B018
		public unsafe void WriteDouble(double value)
		{
			ulong num = (ulong)(*(long*)(&value));
			byte[] array = this.buffer;
			array[0] = (byte)num;
			array[1] = (byte)(num >> 8);
			array[2] = (byte)(num >> 16);
			array[3] = (byte)(num >> 24);
			array[4] = (byte)(num >> 32);
			array[5] = (byte)(num >> 40);
			array[6] = (byte)(num >> 48);
			array[7] = (byte)(num >> 56);
			this.stream.Write(array, 0, 8);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0019B080 File Offset: 0x0019B080
		public void WriteBytes(byte[] source)
		{
			this.stream.Write(source, 0, source.Length);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0019B094 File Offset: 0x0019B094
		public void WriteBytes(byte[] source, int index, int length)
		{
			this.stream.Write(source, index, length);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0019B0A4 File Offset: 0x0019B0A4
		public void WriteCompressedUInt32(uint value)
		{
			Stream stream = this.stream;
			if (value <= 127U)
			{
				stream.WriteByte((byte)value);
				return;
			}
			if (value <= 16383U)
			{
				stream.WriteByte((byte)(value >> 8 | 128U));
				stream.WriteByte((byte)value);
				return;
			}
			if (value <= 536870911U)
			{
				byte[] array = this.buffer;
				array[0] = (byte)(value >> 24 | 192U);
				array[1] = (byte)(value >> 16);
				array[2] = (byte)(value >> 8);
				array[3] = (byte)value;
				stream.Write(array, 0, 4);
				return;
			}
			DataWriter.ThrowArgumentOutOfRangeException("UInt32 value can't be compressed");
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0019B13C File Offset: 0x0019B13C
		public void WriteCompressedInt32(int value)
		{
			Stream stream = this.stream;
			uint num = (uint)value >> 31;
			if (-64 <= value && value <= 63)
			{
				uint num2 = (uint)((value & 63) << 1 | (int)num);
				stream.WriteByte((byte)num2);
				return;
			}
			if (-8192 <= value && value <= 8191)
			{
				uint num3 = (uint)((value & 8191) << 1 | (int)num);
				stream.WriteByte((byte)(num3 >> 8 | 128U));
				stream.WriteByte((byte)num3);
				return;
			}
			if (-268435456 <= value && value <= 268435455)
			{
				uint num4 = (uint)((value & 268435455) << 1 | (int)num);
				byte[] array = this.buffer;
				array[0] = (byte)(num4 >> 24 | 192U);
				array[1] = (byte)(num4 >> 16);
				array[2] = (byte)(num4 >> 8);
				array[3] = (byte)num4;
				stream.Write(array, 0, 4);
				return;
			}
			DataWriter.ThrowArgumentOutOfRangeException("Int32 value can't be compressed");
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0019B220 File Offset: 0x0019B220
		public static int GetCompressedUInt32Length(uint value)
		{
			if (value <= 127U)
			{
				return 1;
			}
			if (value <= 16383U)
			{
				return 2;
			}
			if (value <= 536870911U)
			{
				return 4;
			}
			DataWriter.ThrowArgumentOutOfRangeException("UInt32 value can't be compressed");
			return 0;
		}

		// Token: 0x0400286C RID: 10348
		private readonly Stream stream;

		// Token: 0x0400286D RID: 10349
		private readonly byte[] buffer;

		// Token: 0x0400286E RID: 10350
		private const int BUFFER_LEN = 8;
	}
}
