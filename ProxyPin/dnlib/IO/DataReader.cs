using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.DotNet.Writer;

namespace dnlib.IO
{
	// Token: 0x02000764 RID: 1892
	[DebuggerDisplay("{StartOffset,h}-{EndOffset,h} Length={Length} BytesLeft={BytesLeft}")]
	[ComVisible(true)]
	public struct DataReader
	{
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x00164B48 File Offset: 0x00164B48
		public readonly uint StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x00164B50 File Offset: 0x00164B50
		public readonly uint EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x00164B58 File Offset: 0x00164B58
		public readonly uint Length
		{
			get
			{
				return this.endOffset - this.startOffset;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x00164B68 File Offset: 0x00164B68
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x00164B70 File Offset: 0x00164B70
		public uint CurrentOffset
		{
			readonly get
			{
				return this.currentOffset;
			}
			set
			{
				if (value < this.startOffset || value > this.endOffset)
				{
					DataReader.ThrowDataReaderException("Invalid new CurrentOffset");
				}
				this.currentOffset = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x00164B9C File Offset: 0x00164B9C
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x00164BAC File Offset: 0x00164BAC
		public uint Position
		{
			readonly get
			{
				return this.currentOffset - this.startOffset;
			}
			set
			{
				if (value > this.Length)
				{
					DataReader.ThrowDataReaderException("Invalid new Position");
				}
				this.currentOffset = this.startOffset + value;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x00164BD4 File Offset: 0x00164BD4
		public readonly uint BytesLeft
		{
			get
			{
				return this.endOffset - this.currentOffset;
			}
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x00164BE4 File Offset: 0x00164BE4
		public DataReader(DataStream stream, uint offset, uint length)
		{
			this.stream = stream;
			this.startOffset = offset;
			this.endOffset = offset + length;
			this.currentOffset = offset;
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x00164C04 File Offset: 0x00164C04
		[Conditional("DEBUG")]
		private readonly void VerifyState()
		{
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x00164C08 File Offset: 0x00164C08
		private static void ThrowNoMoreBytesLeft()
		{
			throw new DataReaderException("There's not enough bytes left to read");
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x00164C14 File Offset: 0x00164C14
		private static void ThrowDataReaderException(string message)
		{
			throw new DataReaderException(message);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x00164C1C File Offset: 0x00164C1C
		private static void ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x00164C24 File Offset: 0x00164C24
		private static void ThrowArgumentNullException(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x00164C2C File Offset: 0x00164C2C
		private static void ThrowInvalidArgument(string paramName)
		{
			throw new DataReaderException("Invalid argument value");
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x00164C38 File Offset: 0x00164C38
		public void Reset()
		{
			this.currentOffset = this.startOffset;
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x00164C48 File Offset: 0x00164C48
		public readonly DataReader Slice(uint start, uint length)
		{
			if ((ulong)start + (ulong)length > (ulong)this.Length)
			{
				DataReader.ThrowInvalidArgument("length");
			}
			return new DataReader(this.stream, this.startOffset + start, length);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00164C7C File Offset: 0x00164C7C
		public readonly DataReader Slice(uint start)
		{
			if (start > this.Length)
			{
				DataReader.ThrowInvalidArgument("start");
			}
			return this.Slice(start, this.Length - start);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x00164CA4 File Offset: 0x00164CA4
		public readonly DataReader Slice(int start, int length)
		{
			if (start < 0)
			{
				DataReader.ThrowInvalidArgument("start");
			}
			if (length < 0)
			{
				DataReader.ThrowInvalidArgument("length");
			}
			return this.Slice((uint)start, (uint)length);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x00164CD0 File Offset: 0x00164CD0
		public readonly DataReader Slice(int start)
		{
			if (start < 0)
			{
				DataReader.ThrowInvalidArgument("start");
			}
			if (start > (int)this.Length)
			{
				DataReader.ThrowInvalidArgument("start");
			}
			return this.Slice((uint)start, this.Length - (uint)start);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00164D08 File Offset: 0x00164D08
		public readonly bool CanRead(int length)
		{
			return length >= 0 && length <= (int)this.BytesLeft;
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00164D20 File Offset: 0x00164D20
		public readonly bool CanRead(uint length)
		{
			return length <= this.BytesLeft;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x00164D30 File Offset: 0x00164D30
		public bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x00164D3C File Offset: 0x00164D3C
		public char ReadChar()
		{
			return (char)this.ReadUInt16();
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00164D44 File Offset: 0x00164D44
		public sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x00164D50 File Offset: 0x00164D50
		public byte ReadByte()
		{
			uint num = this.currentOffset;
			if (num == this.endOffset)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			byte result = this.stream.ReadByte(num);
			this.currentOffset = num + 1U;
			return result;
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00164D90 File Offset: 0x00164D90
		public short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00164D9C File Offset: 0x00164D9C
		public ushort ReadUInt16()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 2U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			ushort result = this.stream.ReadUInt16(num);
			this.currentOffset = num + 2U;
			return result;
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00164DDC File Offset: 0x00164DDC
		public int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00164DE4 File Offset: 0x00164DE4
		public uint ReadUInt32()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 4U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			uint result = this.stream.ReadUInt32(num);
			this.currentOffset = num + 4U;
			return result;
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00164E24 File Offset: 0x00164E24
		internal byte Unsafe_ReadByte()
		{
			uint num = this.currentOffset;
			byte result = this.stream.ReadByte(num);
			this.currentOffset = num + 1U;
			return result;
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x00164E54 File Offset: 0x00164E54
		internal ushort Unsafe_ReadUInt16()
		{
			uint num = this.currentOffset;
			ushort result = this.stream.ReadUInt16(num);
			this.currentOffset = num + 2U;
			return result;
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00164E84 File Offset: 0x00164E84
		internal uint Unsafe_ReadUInt32()
		{
			uint num = this.currentOffset;
			uint result = this.stream.ReadUInt32(num);
			this.currentOffset = num + 4U;
			return result;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00164EB4 File Offset: 0x00164EB4
		public long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00164EBC File Offset: 0x00164EBC
		public ulong ReadUInt64()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 8U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			ulong result = this.stream.ReadUInt64(num);
			this.currentOffset = num + 8U;
			return result;
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00164EFC File Offset: 0x00164EFC
		public float ReadSingle()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 4U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			float result = this.stream.ReadSingle(num);
			this.currentOffset = num + 4U;
			return result;
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x00164F3C File Offset: 0x00164F3C
		public double ReadDouble()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 8U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			double result = this.stream.ReadDouble(num);
			this.currentOffset = num + 8U;
			return result;
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x00164F7C File Offset: 0x00164F7C
		public Guid ReadGuid()
		{
			uint num = this.currentOffset;
			if (this.endOffset - num < 16U)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			Guid result = this.stream.ReadGuid(num);
			this.currentOffset = num + 16U;
			return result;
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x00164FC0 File Offset: 0x00164FC0
		public decimal ReadDecimal()
		{
			return new decimal(new int[]
			{
				this.ReadInt32(),
				this.ReadInt32(),
				this.ReadInt32(),
				this.ReadInt32()
			});
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x00165000 File Offset: 0x00165000
		public string ReadUtf16String(int chars)
		{
			if (chars < 0)
			{
				DataReader.ThrowInvalidArgument("chars");
			}
			if (chars == 0)
			{
				return string.Empty;
			}
			uint num = (uint)(chars * 2);
			uint num2 = this.currentOffset;
			if (this.endOffset - num2 < num)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			string result = (num == 0U) ? string.Empty : this.stream.ReadUtf16String(num2, chars);
			this.currentOffset = num2 + num;
			return result;
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00165074 File Offset: 0x00165074
		public unsafe void ReadBytes(void* destination, int length)
		{
			if (destination == null && length != 0)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (length < 0)
			{
				DataReader.ThrowInvalidArgument("length");
			}
			if (length == 0)
			{
				return;
			}
			uint num = this.currentOffset;
			if (this.endOffset - num < (uint)length)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			this.stream.ReadBytes(num, destination, length);
			this.currentOffset = num + (uint)length;
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x001650E4 File Offset: 0x001650E4
		public void ReadBytes(byte[] destination, int destinationIndex, int length)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (destinationIndex < 0)
			{
				DataReader.ThrowInvalidArgument("destinationIndex");
			}
			if (length < 0)
			{
				DataReader.ThrowInvalidArgument("length");
			}
			if (length == 0)
			{
				return;
			}
			uint num = this.currentOffset;
			if (this.endOffset - num < (uint)length)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			this.stream.ReadBytes(num, destination, destinationIndex, length);
			this.currentOffset = num + (uint)length;
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x00165160 File Offset: 0x00165160
		public byte[] ReadBytes(int length)
		{
			if (length < 0)
			{
				DataReader.ThrowInvalidArgument("length");
			}
			if (length == 0)
			{
				return Array2.Empty<byte>();
			}
			byte[] array = new byte[length];
			this.ReadBytes(array, 0, length);
			return array;
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x001651A0 File Offset: 0x001651A0
		public bool TryReadCompressedUInt32(out uint value)
		{
			uint num = this.currentOffset;
			uint num2 = this.endOffset - num;
			if (num2 == 0U)
			{
				value = 0U;
				return false;
			}
			DataStream dataStream = this.stream;
			byte b = dataStream.ReadByte(num++);
			if ((b & 128) == 0)
			{
				value = (uint)b;
				this.currentOffset = num;
				return true;
			}
			if ((b & 192) == 128)
			{
				if (num2 < 2U)
				{
					value = 0U;
					return false;
				}
				value = (uint)((int)(b & 63) << 8 | (int)dataStream.ReadByte(num++));
				this.currentOffset = num;
				return true;
			}
			else
			{
				if (num2 < 4U)
				{
					value = 0U;
					return false;
				}
				value = (uint)((int)(b & 31) << 24 | (int)dataStream.ReadByte(num++) << 16 | (int)dataStream.ReadByte(num++) << 8 | (int)dataStream.ReadByte(num++));
				this.currentOffset = num;
				return true;
			}
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00165278 File Offset: 0x00165278
		public uint ReadCompressedUInt32()
		{
			uint result;
			if (!this.TryReadCompressedUInt32(out result))
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			return result;
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0016529C File Offset: 0x0016529C
		public bool TryReadCompressedInt32(out int value)
		{
			uint num = this.currentOffset;
			uint num2 = this.endOffset - num;
			if (num2 == 0U)
			{
				value = 0;
				return false;
			}
			DataStream dataStream = this.stream;
			byte b = dataStream.ReadByte(num++);
			if ((b & 128) == 0)
			{
				if ((b & 1) != 0)
				{
					value = (-64 | b >> 1);
				}
				else
				{
					value = b >> 1;
				}
				this.currentOffset = num;
				return true;
			}
			if ((b & 192) == 128)
			{
				if (num2 < 2U)
				{
					value = 0;
					return false;
				}
				uint num3 = (uint)((int)(b & 63) << 8 | (int)dataStream.ReadByte(num++));
				if ((num3 & 1U) != 0U)
				{
					value = (int)(4294959104U | num3 >> 1);
				}
				else
				{
					value = (int)(num3 >> 1);
				}
				this.currentOffset = num;
				return true;
			}
			else
			{
				if ((b & 224) != 192)
				{
					value = 0;
					return false;
				}
				if (num2 < 4U)
				{
					value = 0;
					return false;
				}
				uint num4 = (uint)((int)(b & 31) << 24 | (int)dataStream.ReadByte(num++) << 16 | (int)dataStream.ReadByte(num++) << 8 | (int)dataStream.ReadByte(num++));
				if ((num4 & 1U) != 0U)
				{
					value = (int)(4026531840U | num4 >> 1);
				}
				else
				{
					value = (int)(num4 >> 1);
				}
				this.currentOffset = num;
				return true;
			}
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x001653E0 File Offset: 0x001653E0
		public int ReadCompressedInt32()
		{
			int result;
			if (!this.TryReadCompressedInt32(out result))
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			return result;
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x00165404 File Offset: 0x00165404
		public uint Read7BitEncodedUInt32()
		{
			uint num = 0U;
			int num2 = 0;
			for (int i = 0; i < 5; i++)
			{
				byte b = this.ReadByte();
				num |= (uint)((uint)(b & 127) << num2);
				if ((b & 128) == 0)
				{
					return num;
				}
				num2 += 7;
			}
			DataReader.ThrowDataReaderException("Invalid encoded UInt32");
			return 0U;
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00165458 File Offset: 0x00165458
		public int Read7BitEncodedInt32()
		{
			return (int)this.Read7BitEncodedUInt32();
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x00165460 File Offset: 0x00165460
		public string ReadSerializedString()
		{
			return this.ReadSerializedString(Encoding.UTF8);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x00165470 File Offset: 0x00165470
		public string ReadSerializedString(Encoding encoding)
		{
			if (encoding == null)
			{
				DataReader.ThrowArgumentNullException("encoding");
			}
			int num = this.Read7BitEncodedInt32();
			if (num < 0)
			{
				DataReader.ThrowNoMoreBytesLeft();
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return this.ReadString(num, encoding);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x001654B8 File Offset: 0x001654B8
		public readonly byte[] ToArray()
		{
			int length = (int)this.Length;
			if (length < 0)
			{
				DataReader.ThrowInvalidOperationException();
			}
			if (length == 0)
			{
				return Array2.Empty<byte>();
			}
			byte[] array = new byte[length];
			this.stream.ReadBytes(this.startOffset, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00165508 File Offset: 0x00165508
		public byte[] ReadRemainingBytes()
		{
			int bytesLeft = (int)this.BytesLeft;
			if (bytesLeft < 0)
			{
				DataReader.ThrowInvalidOperationException();
			}
			return this.ReadBytes(bytesLeft);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x00165534 File Offset: 0x00165534
		public byte[] TryReadBytesUntil(byte value)
		{
			uint num = this.currentOffset;
			uint num2 = this.endOffset;
			if (num == num2)
			{
				return null;
			}
			uint num3;
			if (!this.stream.TryGetOffsetOf(num, num2, value, out num3))
			{
				return null;
			}
			int num4 = (int)(num3 - num);
			if (num4 < 0)
			{
				return null;
			}
			return this.ReadBytes(num4);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x00165588 File Offset: 0x00165588
		public string TryReadZeroTerminatedUtf8String()
		{
			return this.TryReadZeroTerminatedString(Encoding.UTF8);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x00165598 File Offset: 0x00165598
		public string TryReadZeroTerminatedString(Encoding encoding)
		{
			if (encoding == null)
			{
				DataReader.ThrowArgumentNullException("encoding");
			}
			uint num = this.currentOffset;
			uint num2 = this.endOffset;
			if (num == num2)
			{
				return null;
			}
			uint num3;
			if (!this.stream.TryGetOffsetOf(num, num2, 0, out num3))
			{
				return null;
			}
			int num4 = (int)(num3 - num);
			if (num4 < 0)
			{
				return null;
			}
			string result = (num4 == 0) ? string.Empty : this.stream.ReadString(num, num4, encoding);
			this.currentOffset = num3 + 1U;
			return result;
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x0016561C File Offset: 0x0016561C
		public string ReadUtf8String(int byteCount)
		{
			return this.ReadString(byteCount, Encoding.UTF8);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0016562C File Offset: 0x0016562C
		public string ReadString(int byteCount, Encoding encoding)
		{
			if (byteCount < 0)
			{
				DataReader.ThrowInvalidArgument("byteCount");
			}
			if (encoding == null)
			{
				DataReader.ThrowArgumentNullException("encoding");
			}
			if (byteCount == 0)
			{
				return string.Empty;
			}
			if (byteCount > (int)this.Length)
			{
				DataReader.ThrowInvalidArgument("byteCount");
			}
			uint num = this.currentOffset;
			string result = this.stream.ReadString(num, byteCount, encoding);
			this.currentOffset = num + (uint)byteCount;
			return result;
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x001656A0 File Offset: 0x001656A0
		public readonly Stream AsStream()
		{
			return new DataReaderStream(ref this);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x001656A8 File Offset: 0x001656A8
		private readonly byte[] AllocTempBuffer()
		{
			return new byte[Math.Min(8192U, this.BytesLeft)];
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x001656C0 File Offset: 0x001656C0
		public void CopyTo(DataWriter destination)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (this.Position >= this.Length)
			{
				return;
			}
			this.CopyTo(destination.InternalStream, this.AllocTempBuffer());
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x00165708 File Offset: 0x00165708
		public void CopyTo(DataWriter destination, byte[] dataBuffer)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			this.CopyTo(destination.InternalStream, dataBuffer);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x00165728 File Offset: 0x00165728
		public void CopyTo(BinaryWriter destination)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (this.Position >= this.Length)
			{
				return;
			}
			this.CopyTo(destination.BaseStream, this.AllocTempBuffer());
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x00165770 File Offset: 0x00165770
		public void CopyTo(BinaryWriter destination, byte[] dataBuffer)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			this.CopyTo(destination.BaseStream, dataBuffer);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x00165790 File Offset: 0x00165790
		public void CopyTo(Stream destination)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (this.Position >= this.Length)
			{
				return;
			}
			this.CopyTo(destination, this.AllocTempBuffer());
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x001657C4 File Offset: 0x001657C4
		public void CopyTo(Stream destination, byte[] dataBuffer)
		{
			if (destination == null)
			{
				DataReader.ThrowArgumentNullException("destination");
			}
			if (dataBuffer == null)
			{
				DataReader.ThrowArgumentNullException("dataBuffer");
			}
			if (this.Position >= this.Length)
			{
				return;
			}
			if (dataBuffer.Length == 0)
			{
				DataReader.ThrowInvalidArgument("dataBuffer");
			}
			uint num = this.BytesLeft;
			while (num > 0U)
			{
				int num2 = (int)Math.Min((uint)dataBuffer.Length, num);
				num -= (uint)num2;
				this.ReadBytes(dataBuffer, 0, num2);
				destination.Write(dataBuffer, 0, num2);
			}
		}

		// Token: 0x04002384 RID: 9092
		private readonly DataStream stream;

		// Token: 0x04002385 RID: 9093
		private readonly uint startOffset;

		// Token: 0x04002386 RID: 9094
		private readonly uint endOffset;

		// Token: 0x04002387 RID: 9095
		private uint currentOffset;
	}
}
