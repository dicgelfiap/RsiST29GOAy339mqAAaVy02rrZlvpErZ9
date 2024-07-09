using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C3C RID: 3132
	[ComVisible(true)]
	public sealed class ProtoWriter : IDisposable
	{
		// Token: 0x06007C60 RID: 31840 RVA: 0x002496A8 File Offset: 0x002496A8
		public static void WriteObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer);
			if (key >= 0)
			{
				writer.model.Serialize(key, value, writer);
			}
			else if (writer.model == null || !writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false, null))
			{
				TypeModel.ThrowUnexpectedType(value.GetType());
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x0024973C File Offset: 0x0024973C
		public static void WriteRecursionSafeObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			writer.model.Serialize(key, value, writer);
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x00249794 File Offset: 0x00249794
		internal static void WriteObject(object value, int key, ProtoWriter writer, PrefixStyle style, int fieldNumber)
		{
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (style != PrefixStyle.Base128)
			{
				if (style - PrefixStyle.Fixed32 > 1)
				{
					throw new ArgumentOutOfRangeException("style");
				}
				writer.fieldNumber = 0;
				writer.wireType = WireType.Fixed32;
			}
			else
			{
				writer.wireType = WireType.String;
				writer.fieldNumber = fieldNumber;
				if (fieldNumber > 0)
				{
					ProtoWriter.WriteHeaderCore(fieldNumber, WireType.String, writer);
				}
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer, true);
			if (key < 0)
			{
				if (!writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false, null))
				{
					TypeModel.ThrowUnexpectedType(value.GetType());
				}
			}
			else
			{
				writer.model.Serialize(key, value, writer);
			}
			ProtoWriter.EndSubItem(token, writer, style);
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x00249878 File Offset: 0x00249878
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x06007C64 RID: 31844 RVA: 0x00249888 File Offset: 0x00249888
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x06007C65 RID: 31845 RVA: 0x00249890 File Offset: 0x00249890
		internal WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x00249898 File Offset: 0x00249898
		public static void WriteFieldHeader(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot write a ",
					wireType.ToString(),
					" header until the ",
					writer.wireType.ToString(),
					" data has been written"
				}));
			}
			if (fieldNumber < 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer.packedFieldNumber == 0)
			{
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
				ProtoWriter.WriteHeaderCore(fieldNumber, wireType, writer);
				return;
			}
			if (writer.packedFieldNumber != fieldNumber)
			{
				throw new InvalidOperationException("Field mismatch during packed encoding; expected " + writer.packedFieldNumber.ToString() + " but received " + fieldNumber.ToString());
			}
			if (wireType > WireType.Fixed64 && wireType != WireType.Fixed32 && wireType != WireType.SignedVariant)
			{
				throw new InvalidOperationException("Wire-type cannot be encoded as packed: " + wireType.ToString());
			}
			writer.fieldNumber = fieldNumber;
			writer.wireType = wireType;
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x002499BC File Offset: 0x002499BC
		internal static void WriteHeaderCore(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			uint value = (uint)(fieldNumber << 3 | (int)(wireType & (WireType)7));
			ProtoWriter.WriteUInt32Variant(value, writer);
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x002499DC File Offset: 0x002499DC
		public static void WriteBytes(byte[] data, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ProtoWriter.WriteBytes(data, 0, data.Length, writer);
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x002499FC File Offset: 0x002499FC
		public static void WriteBytes(byte[] data, int offset, int length, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed64:
				if (length != 8)
				{
					throw new ArgumentException("length");
				}
				goto IL_C7;
			case WireType.String:
				ProtoWriter.WriteUInt32Variant((uint)length, writer);
				writer.wireType = WireType.None;
				if (length == 0)
				{
					return;
				}
				if (writer.flushLock == 0 && length > writer.ioBuffer.Length)
				{
					ProtoWriter.Flush(writer);
					writer.dest.Write(data, offset, length);
					writer.position64 += (long)length;
					return;
				}
				goto IL_C7;
			case WireType.Fixed32:
				if (length != 4)
				{
					throw new ArgumentException("length");
				}
				goto IL_C7;
			}
			throw ProtoWriter.CreateException(writer);
			IL_C7:
			ProtoWriter.DemandSpace(length, writer);
			Buffer.BlockCopy(data, offset, writer.ioBuffer, writer.ioIndex, length);
			ProtoWriter.IncrementedAndReset(length, writer);
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x00249AF8 File Offset: 0x00249AF8
		private static void CopyRawFromStream(Stream source, ProtoWriter writer)
		{
			byte[] array = writer.ioBuffer;
			int num = array.Length - writer.ioIndex;
			int num2 = 1;
			while (num > 0 && (num2 = source.Read(array, writer.ioIndex, num)) > 0)
			{
				writer.ioIndex += num2;
				writer.position64 += (long)num2;
				num -= num2;
			}
			if (num2 <= 0)
			{
				return;
			}
			if (writer.flushLock == 0)
			{
				ProtoWriter.Flush(writer);
				while ((num2 = source.Read(array, 0, array.Length)) > 0)
				{
					writer.dest.Write(array, 0, num2);
					writer.position64 += (long)num2;
				}
				return;
			}
			for (;;)
			{
				ProtoWriter.DemandSpace(128, writer);
				if ((num2 = source.Read(writer.ioBuffer, writer.ioIndex, writer.ioBuffer.Length - writer.ioIndex)) <= 0)
				{
					break;
				}
				writer.position64 += (long)num2;
				writer.ioIndex += num2;
			}
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x00249BFC File Offset: 0x00249BFC
		private static void IncrementedAndReset(int length, ProtoWriter writer)
		{
			writer.ioIndex += length;
			writer.position64 += (long)length;
			writer.wireType = WireType.None;
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x00249C24 File Offset: 0x00249C24
		public static SubItemToken StartSubItem(object instance, ProtoWriter writer)
		{
			return ProtoWriter.StartSubItem(instance, writer, false);
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x00249C30 File Offset: 0x00249C30
		private void CheckRecursionStackAndPush(object instance)
		{
			int num;
			if (this.recursionStack == null)
			{
				this.recursionStack = new MutableList();
			}
			else if (instance != null && (num = this.recursionStack.IndexOfReference(instance)) >= 0)
			{
				throw new ProtoException("Possible recursion detected (offset: " + (this.recursionStack.Count - num).ToString() + " level(s)): " + instance.ToString());
			}
			this.recursionStack.Add(instance);
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x00249CB4 File Offset: 0x00249CB4
		private void PopRecursionStack()
		{
			this.recursionStack.RemoveLast();
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x00249CC4 File Offset: 0x00249CC4
		private static SubItemToken StartSubItem(object instance, ProtoWriter writer, bool allowFixed)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = writer.depth + 1;
			writer.depth = num;
			if (num > 25)
			{
				writer.CheckRecursionStackAndPush(instance);
			}
			if (writer.packedFieldNumber != 0)
			{
				throw new InvalidOperationException("Cannot begin a sub-item while performing packed encoding");
			}
			switch (writer.wireType)
			{
			case WireType.String:
				writer.wireType = WireType.None;
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				writer.position64 += 1L;
				num = writer.ioIndex;
				writer.ioIndex = num + 1;
				return new SubItemToken((long)num);
			case WireType.StartGroup:
				writer.wireType = WireType.None;
				return new SubItemToken((long)(-(long)writer.fieldNumber));
			case WireType.Fixed32:
			{
				if (!allowFixed)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				SubItemToken result = new SubItemToken((long)writer.ioIndex);
				ProtoWriter.IncrementedAndReset(4, writer);
				return result;
			}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x00249DD8 File Offset: 0x00249DD8
		public static void EndSubItem(SubItemToken token, ProtoWriter writer)
		{
			ProtoWriter.EndSubItem(token, writer, PrefixStyle.Base128);
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x00249DE4 File Offset: 0x00249DE4
		private static void EndSubItem(SubItemToken token, ProtoWriter writer, PrefixStyle style)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			int num = (int)token.value64;
			if (writer.depth <= 0)
			{
				throw ProtoWriter.CreateException(writer);
			}
			int num2 = writer.depth;
			writer.depth = num2 - 1;
			if (num2 > 25)
			{
				writer.PopRecursionStack();
			}
			writer.packedFieldNumber = 0;
			if (num < 0)
			{
				ProtoWriter.WriteHeaderCore(-num, WireType.EndGroup, writer);
				writer.wireType = WireType.None;
				return;
			}
			switch (style)
			{
			case PrefixStyle.Base128:
			{
				int num3 = writer.ioIndex - num - 1;
				int num4 = 0;
				uint num5 = (uint)num3;
				while ((num5 >>= 7) != 0U)
				{
					num4++;
				}
				if (num4 == 0)
				{
					writer.ioBuffer[num] = (byte)(num3 & 127);
				}
				else
				{
					ProtoWriter.DemandSpace(num4, writer);
					byte[] array = writer.ioBuffer;
					Buffer.BlockCopy(array, num + 1, array, num + 1 + num4, num3);
					num5 = (uint)num3;
					do
					{
						array[num++] = (byte)((num5 & 127U) | 128U);
					}
					while ((num5 >>= 7) != 0U);
					array[num - 1] = (byte)((int)array[num - 1] & -129);
					writer.position64 += (long)num4;
					writer.ioIndex += num4;
				}
				break;
			}
			case PrefixStyle.Fixed32:
			{
				int num3 = writer.ioIndex - num - 4;
				ProtoWriter.WriteInt32ToBuffer(num3, writer.ioBuffer, num);
				break;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num3 = writer.ioIndex - num - 4;
				byte[] array2 = writer.ioBuffer;
				ProtoWriter.WriteInt32ToBuffer(num3, array2, num);
				byte b = array2[num];
				array2[num] = array2[num + 3];
				array2[num + 3] = b;
				b = array2[num + 1];
				array2[num + 1] = array2[num + 2];
				array2[num + 2] = b;
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			num2 = writer.flushLock - 1;
			writer.flushLock = num2;
			if (num2 == 0 && writer.ioIndex >= 1024)
			{
				ProtoWriter.Flush(writer);
			}
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x00249FE4 File Offset: 0x00249FE4
		public static ProtoWriter Create(Stream dest, TypeModel model, SerializationContext context = null)
		{
			return new ProtoWriter(dest, model, context);
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x00249FF0 File Offset: 0x00249FF0
		[Obsolete("Please use ProtoWriter.Create; this API may be removed in a future version", false)]
		public ProtoWriter(Stream dest, TypeModel model, SerializationContext context)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (!dest.CanWrite)
			{
				throw new ArgumentException("Cannot write to stream", "dest");
			}
			this.dest = dest;
			this.ioBuffer = BufferPool.GetBuffer();
			this.model = model;
			this.wireType = WireType.None;
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			this.context = context;
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06007C74 RID: 31860 RVA: 0x0024A080 File Offset: 0x0024A080
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x0024A088 File Offset: 0x0024A088
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x0024A090 File Offset: 0x0024A090
		private void Dispose()
		{
			if (this.dest != null)
			{
				ProtoWriter.Flush(this);
				this.dest = null;
			}
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x0024A0BC File Offset: 0x0024A0BC
		internal static long GetLongPosition(ProtoWriter writer)
		{
			return writer.position64;
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x0024A0C4 File Offset: 0x0024A0C4
		internal static int GetPosition(ProtoWriter writer)
		{
			return checked((int)writer.position64);
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x0024A0D0 File Offset: 0x0024A0D0
		private static void DemandSpace(int required, ProtoWriter writer)
		{
			if (writer.ioBuffer.Length - writer.ioIndex < required)
			{
				ProtoWriter.TryFlushOrResize(required, writer);
			}
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x0024A0F0 File Offset: 0x0024A0F0
		private static void TryFlushOrResize(int required, ProtoWriter writer)
		{
			if (writer.flushLock == 0)
			{
				ProtoWriter.Flush(writer);
				if (writer.ioBuffer.Length - writer.ioIndex >= required)
				{
					return;
				}
			}
			BufferPool.ResizeAndFlushLeft(ref writer.ioBuffer, required + writer.ioIndex, 0, writer.ioIndex);
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x0024A144 File Offset: 0x0024A144
		public void Close()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("Unable to close stream in an incomplete state");
			}
			this.Dispose();
		}

		// Token: 0x06007C7C RID: 31868 RVA: 0x0024A170 File Offset: 0x0024A170
		internal void CheckDepthFlushlock()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("The writer is in an incomplete state");
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06007C7D RID: 31869 RVA: 0x0024A194 File Offset: 0x0024A194
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x0024A19C File Offset: 0x0024A19C
		internal static void Flush(ProtoWriter writer)
		{
			if (writer.flushLock == 0 && writer.ioIndex != 0)
			{
				writer.dest.Write(writer.ioBuffer, 0, writer.ioIndex);
				writer.ioIndex = 0;
			}
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x0024A1D4 File Offset: 0x0024A1D4
		private static void WriteUInt32Variant(uint value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(5, writer);
			int num = 0;
			do
			{
				byte[] array = writer.ioBuffer;
				int num2 = writer.ioIndex;
				writer.ioIndex = num2 + 1;
				array[num2] = (byte)((value & 127U) | 128U);
				num++;
			}
			while ((value >>= 7) != 0U);
			byte[] array2 = writer.ioBuffer;
			int num3 = writer.ioIndex - 1;
			array2[num3] &= 127;
			writer.position64 += (long)num;
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x0024A248 File Offset: 0x0024A248
		internal static uint Zig(int value)
		{
			return (uint)(value << 1 ^ value >> 31);
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x0024A254 File Offset: 0x0024A254
		internal static ulong Zig(long value)
		{
			return (ulong)(value << 1 ^ value >> 63);
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x0024A260 File Offset: 0x0024A260
		private static void WriteUInt64Variant(ulong value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(10, writer);
			int num = 0;
			do
			{
				byte[] array = writer.ioBuffer;
				int num2 = writer.ioIndex;
				writer.ioIndex = num2 + 1;
				array[num2] = (byte)((value & 127UL) | 128UL);
				num++;
			}
			while ((value >>= 7) != 0UL);
			byte[] array2 = writer.ioBuffer;
			int num3 = writer.ioIndex - 1;
			array2[num3] &= 127;
			writer.position64 += (long)num;
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x0024A2D8 File Offset: 0x0024A2D8
		public static void WriteString(string value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.String)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				ProtoWriter.WriteUInt32Variant(0U, writer);
				writer.wireType = WireType.None;
				return;
			}
			int byteCount = ProtoWriter.encoding.GetByteCount(value);
			ProtoWriter.WriteUInt32Variant((uint)byteCount, writer);
			ProtoWriter.DemandSpace(byteCount, writer);
			int bytes = ProtoWriter.encoding.GetBytes(value, 0, value.Length, writer.ioBuffer, writer.ioIndex);
			ProtoWriter.IncrementedAndReset(bytes, writer);
		}

		// Token: 0x06007C84 RID: 31876 RVA: 0x0024A37C File Offset: 0x0024A37C
		public static void WriteUInt64(ulong value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Variant)
			{
				ProtoWriter.WriteUInt64Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			}
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			}
			if (wireType != WireType.Fixed32)
			{
				throw ProtoWriter.CreateException(writer);
			}
			ProtoWriter.WriteUInt32(checked((uint)value), writer);
		}

		// Token: 0x06007C85 RID: 31877 RVA: 0x0024A3E4 File Offset: 0x0024A3E4
		public static void WriteInt64(long value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType != WireType.Variant)
				{
					if (wireType == WireType.Fixed64)
					{
						ProtoWriter.DemandSpace(8, writer);
						byte[] array = writer.ioBuffer;
						int num = writer.ioIndex;
						array[num] = (byte)value;
						array[num + 1] = (byte)(value >> 8);
						array[num + 2] = (byte)(value >> 16);
						array[num + 3] = (byte)(value >> 24);
						array[num + 4] = (byte)(value >> 32);
						array[num + 5] = (byte)(value >> 40);
						array[num + 6] = (byte)(value >> 48);
						array[num + 7] = (byte)(value >> 56);
						ProtoWriter.IncrementedAndReset(8, writer);
						return;
					}
				}
				else
				{
					if (value >= 0L)
					{
						ProtoWriter.WriteUInt64Variant((ulong)value, writer);
						writer.wireType = WireType.None;
						return;
					}
					ProtoWriter.DemandSpace(10, writer);
					byte[] array = writer.ioBuffer;
					int num = writer.ioIndex;
					array[num] = (byte)(value | 128L);
					array[num + 1] = (byte)((int)(value >> 7) | 128);
					array[num + 2] = (byte)((int)(value >> 14) | 128);
					array[num + 3] = (byte)((int)(value >> 21) | 128);
					array[num + 4] = (byte)((int)(value >> 28) | 128);
					array[num + 5] = (byte)((int)(value >> 35) | 128);
					array[num + 6] = (byte)((int)(value >> 42) | 128);
					array[num + 7] = (byte)((int)(value >> 49) | 128);
					array[num + 8] = (byte)((int)(value >> 56) | 128);
					array[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
					return;
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					ProtoWriter.WriteInt32(checked((int)value), writer);
					return;
				}
				if (wireType == WireType.SignedVariant)
				{
					ProtoWriter.WriteUInt64Variant(ProtoWriter.Zig(value), writer);
					writer.wireType = WireType.None;
					return;
				}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x0024A594 File Offset: 0x0024A594
		public static void WriteUInt32(uint value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Variant)
			{
				ProtoWriter.WriteUInt32Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			}
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			}
			if (wireType == WireType.Fixed32)
			{
				ProtoWriter.WriteInt32((int)value, writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x0024A5F8 File Offset: 0x0024A5F8
		public static void WriteInt16(short value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x06007C88 RID: 31880 RVA: 0x0024A604 File Offset: 0x0024A604
		public static void WriteUInt16(ushort value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x0024A610 File Offset: 0x0024A610
		public static void WriteByte(byte value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x0024A61C File Offset: 0x0024A61C
		public static void WriteSByte(sbyte value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x0024A628 File Offset: 0x0024A628
		private static void WriteInt32ToBuffer(int value, byte[] buffer, int index)
		{
			buffer[index] = (byte)value;
			buffer[index + 1] = (byte)(value >> 8);
			buffer[index + 2] = (byte)(value >> 16);
			buffer[index + 3] = (byte)(value >> 24);
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x0024A64C File Offset: 0x0024A64C
		public static void WriteInt32(int value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType != WireType.Variant)
				{
					if (wireType == WireType.Fixed64)
					{
						ProtoWriter.DemandSpace(8, writer);
						byte[] array = writer.ioBuffer;
						int num = writer.ioIndex;
						array[num] = (byte)value;
						array[num + 1] = (byte)(value >> 8);
						array[num + 2] = (byte)(value >> 16);
						array[num + 3] = (byte)(value >> 24);
						array[num + 4] = (array[num + 5] = (array[num + 6] = (array[num + 7] = 0)));
						ProtoWriter.IncrementedAndReset(8, writer);
						return;
					}
				}
				else
				{
					if (value >= 0)
					{
						ProtoWriter.WriteUInt32Variant((uint)value, writer);
						writer.wireType = WireType.None;
						return;
					}
					ProtoWriter.DemandSpace(10, writer);
					byte[] array = writer.ioBuffer;
					int num = writer.ioIndex;
					array[num] = (byte)(value | 128);
					array[num + 1] = (byte)(value >> 7 | 128);
					array[num + 2] = (byte)(value >> 14 | 128);
					array[num + 3] = (byte)(value >> 21 | 128);
					array[num + 4] = (byte)(value >> 28 | 128);
					array[num + 5] = (array[num + 6] = (array[num + 7] = (array[num + 8] = byte.MaxValue)));
					array[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
					return;
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					ProtoWriter.DemandSpace(4, writer);
					ProtoWriter.WriteInt32ToBuffer(value, writer.ioBuffer, writer.ioIndex);
					ProtoWriter.IncrementedAndReset(4, writer);
					return;
				}
				if (wireType == WireType.SignedVariant)
				{
					ProtoWriter.WriteUInt32Variant(ProtoWriter.Zig(value), writer);
					writer.wireType = WireType.None;
					return;
				}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x0024A7E4 File Offset: 0x0024A7E4
		public unsafe static void WriteDouble(double value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64(*(long*)(&value), writer);
				return;
			}
			if (wireType != WireType.Fixed32)
			{
				throw ProtoWriter.CreateException(writer);
			}
			float num = (float)value;
			if (float.IsInfinity(num) && !double.IsInfinity(value))
			{
				throw new OverflowException();
			}
			ProtoWriter.WriteSingle(num, writer);
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x0024A854 File Offset: 0x0024A854
		public unsafe static void WriteSingle(float value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteDouble((double)value, writer);
				return;
			}
			if (wireType == WireType.Fixed32)
			{
				ProtoWriter.WriteInt32(*(int*)(&value), writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x0024A8A8 File Offset: 0x0024A8A8
		public static void ThrowEnumException(ProtoWriter writer, object enumValue)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string str = (enumValue == null) ? "<null>" : (enumValue.GetType().FullName + "." + enumValue.ToString());
			throw new ProtoException("No wire-value is mapped to the enum " + str + " at position " + writer.position64.ToString());
		}

		// Token: 0x06007C90 RID: 31888 RVA: 0x0024A918 File Offset: 0x0024A918
		internal static Exception CreateException(ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			return new ProtoException("Invalid serialization operation with wire-type " + writer.wireType.ToString() + " at position " + writer.position64.ToString());
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x0024A96C File Offset: 0x0024A96C
		public static void WriteBoolean(bool value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32(value ? 1U : 0U, writer);
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x0024A984 File Offset: 0x0024A984
		public static void AppendExtensionData(IExtensible instance, ProtoWriter writer)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			IExtension extensionObject = instance.GetExtensionObject(false);
			if (extensionObject != null)
			{
				Stream stream = extensionObject.BeginQuery();
				try
				{
					ProtoWriter.CopyRawFromStream(stream, writer);
				}
				finally
				{
					extensionObject.EndQuery(stream);
				}
			}
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x0024AA00 File Offset: 0x0024AA00
		public static void SetPackedField(int fieldNumber, ProtoWriter writer)
		{
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.packedFieldNumber = fieldNumber;
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x0024AA2C File Offset: 0x0024AA2C
		public static void ClearPackedField(int fieldNumber, ProtoWriter writer)
		{
			if (fieldNumber != writer.packedFieldNumber)
			{
				throw new InvalidOperationException("Field mismatch during packed encoding; expected " + writer.packedFieldNumber.ToString() + " but received " + fieldNumber.ToString());
			}
			writer.packedFieldNumber = 0;
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x0024AA68 File Offset: 0x0024AA68
		public static void WritePackedPrefix(int elementCount, WireType wireType, ProtoWriter writer)
		{
			if (writer.WireType != WireType.String)
			{
				throw new InvalidOperationException("Invalid wire-type: " + writer.WireType.ToString());
			}
			if (elementCount < 0)
			{
				throw new ArgumentOutOfRangeException("elementCount");
			}
			ulong value;
			if (wireType != WireType.Fixed64)
			{
				if (wireType != WireType.Fixed32)
				{
					throw new ArgumentOutOfRangeException("wireType", "Invalid wire-type: " + wireType.ToString());
				}
				value = (ulong)((ulong)((long)elementCount) << 2);
			}
			else
			{
				value = (ulong)((ulong)((long)elementCount) << 3);
			}
			ProtoWriter.WriteUInt64Variant(value, writer);
			writer.wireType = WireType.None;
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x0024AB10 File Offset: 0x0024AB10
		internal string SerializeType(Type type)
		{
			return TypeModel.SerializeType(this.model, type);
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x0024AB20 File Offset: 0x0024AB20
		public void SetRootObject(object value)
		{
			this.NetCache.SetKeyedObject(0, value);
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x0024AB30 File Offset: 0x0024AB30
		public static void WriteType(Type value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ProtoWriter.WriteString(writer.SerializeType(value), writer);
		}

		// Token: 0x04003C0D RID: 15373
		private Stream dest;

		// Token: 0x04003C0E RID: 15374
		private TypeModel model;

		// Token: 0x04003C0F RID: 15375
		private readonly NetObjectCache netCache = new NetObjectCache();

		// Token: 0x04003C10 RID: 15376
		private int fieldNumber;

		// Token: 0x04003C11 RID: 15377
		private int flushLock;

		// Token: 0x04003C12 RID: 15378
		private WireType wireType;

		// Token: 0x04003C13 RID: 15379
		private int depth;

		// Token: 0x04003C14 RID: 15380
		private const int RecursionCheckDepth = 25;

		// Token: 0x04003C15 RID: 15381
		private MutableList recursionStack;

		// Token: 0x04003C16 RID: 15382
		private readonly SerializationContext context;

		// Token: 0x04003C17 RID: 15383
		private byte[] ioBuffer;

		// Token: 0x04003C18 RID: 15384
		private int ioIndex;

		// Token: 0x04003C19 RID: 15385
		private long position64;

		// Token: 0x04003C1A RID: 15386
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04003C1B RID: 15387
		private int packedFieldNumber;
	}
}
