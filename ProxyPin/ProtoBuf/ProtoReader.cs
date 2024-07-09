using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C3B RID: 3131
	[ComVisible(true)]
	public sealed class ProtoReader : IDisposable
	{
		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x06007C12 RID: 31762 RVA: 0x00247698 File Offset: 0x00247698
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06007C13 RID: 31763 RVA: 0x002476A0 File Offset: 0x002476A0
		public WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x002476A8 File Offset: 0x002476A8
		[Obsolete("Please use ProtoReader.Create; this API may be removed in a future version", false)]
		public ProtoReader(Stream source, TypeModel model, SerializationContext context)
		{
			ProtoReader.Init(this, source, model, context, -1L);
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06007C15 RID: 31765 RVA: 0x002476BC File Offset: 0x002476BC
		// (set) Token: 0x06007C16 RID: 31766 RVA: 0x002476C4 File Offset: 0x002476C4
		public bool InternStrings
		{
			get
			{
				return this.internStrings;
			}
			set
			{
				this.internStrings = value;
			}
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x002476D0 File Offset: 0x002476D0
		[Obsolete("Please use ProtoReader.Create; this API may be removed in a future version", false)]
		public ProtoReader(Stream source, TypeModel model, SerializationContext context, int length)
		{
			ProtoReader.Init(this, source, model, context, (long)length);
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x002476E4 File Offset: 0x002476E4
		[Obsolete("Please use ProtoReader.Create; this API may be removed in a future version", false)]
		public ProtoReader(Stream source, TypeModel model, SerializationContext context, long length)
		{
			ProtoReader.Init(this, source, model, context, length);
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x002476F8 File Offset: 0x002476F8
		private static void Init(ProtoReader reader, Stream source, TypeModel model, SerializationContext context, long length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!source.CanRead)
			{
				throw new ArgumentException("Cannot read from stream", "source");
			}
			reader.source = source;
			reader.ioBuffer = BufferPool.GetBuffer();
			reader.model = model;
			bool flag = length >= 0L;
			reader.isFixedLength = flag;
			reader.dataRemaining64 = (flag ? length : 0L);
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			reader.context = context;
			reader.position64 = 0L;
			reader.available = (reader.depth = (reader.fieldNumber = (reader.ioIndex = 0)));
			reader.blockEnd64 = long.MaxValue;
			reader.internStrings = RuntimeTypeModel.Default.InternStrings;
			reader.wireType = WireType.None;
			reader.trapCount = 1U;
			if (reader.netCache == null)
			{
				reader.netCache = new NetObjectCache();
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x00247800 File Offset: 0x00247800
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x00247808 File Offset: 0x00247808
		public void Dispose()
		{
			this.source = null;
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
			if (this.stringInterner != null)
			{
				this.stringInterner.Clear();
				this.stringInterner = null;
			}
			if (this.netCache != null)
			{
				this.netCache.Clear();
			}
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x00247868 File Offset: 0x00247868
		internal int TryReadUInt32VariantWithoutMoving(bool trimNegative, out uint value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0U;
				return 0;
			}
			int num = this.ioIndex;
			value = (uint)this.ioBuffer[num++];
			if ((value & 128U) == 0U)
			{
				return 1;
			}
			value &= 127U;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			uint num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 7;
			if ((num2 & 128U) == 0U)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 14;
			if ((num2 & 128U) == 0U)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 21;
			if ((num2 & 128U) == 0U)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num];
			value |= num2 << 28;
			if ((num2 & 240U) == 0U)
			{
				return 5;
			}
			if (trimNegative && (num2 & 240U) == 240U && this.available >= 10 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[num + 1] == 1)
			{
				return 10;
			}
			throw ProtoReader.AddErrorData(new OverflowException(), this);
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x00247A48 File Offset: 0x00247A48
		private uint ReadUInt32Variant(bool trimNegative)
		{
			uint result;
			int num = this.TryReadUInt32VariantWithoutMoving(trimNegative, out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position64 += (long)num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x00247AA0 File Offset: 0x00247AA0
		private bool TryReadUInt32Variant(out uint value)
		{
			int num = this.TryReadUInt32VariantWithoutMoving(false, out value);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position64 += (long)num;
				return true;
			}
			return false;
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x00247AF0 File Offset: 0x00247AF0
		public uint ReadUInt32()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Variant)
			{
				return this.ReadUInt32Variant(false);
			}
			if (wireType == WireType.Fixed64)
			{
				ulong num = this.ReadUInt64();
				return checked((uint)num);
			}
			if (wireType != WireType.Fixed32)
			{
				throw this.CreateWireTypeException();
			}
			if (this.available < 4)
			{
				this.Ensure(4, true);
			}
			this.position64 += 4L;
			this.available -= 4;
			byte[] array = this.ioBuffer;
			int num2 = this.ioIndex;
			this.ioIndex = num2 + 1;
			uint num3 = array[num2];
			byte[] array2 = this.ioBuffer;
			num2 = this.ioIndex;
			this.ioIndex = num2 + 1;
			uint num4 = num3 | array2[num2] << 8;
			byte[] array3 = this.ioBuffer;
			num2 = this.ioIndex;
			this.ioIndex = num2 + 1;
			uint num5 = num4 | array3[num2] << 16;
			byte[] array4 = this.ioBuffer;
			num2 = this.ioIndex;
			this.ioIndex = num2 + 1;
			return num5 | array4[num2] << 24;
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06007C20 RID: 31776 RVA: 0x00247BD8 File Offset: 0x00247BD8
		public int Position
		{
			get
			{
				return checked((int)this.position64);
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06007C21 RID: 31777 RVA: 0x00247BE4 File Offset: 0x00247BE4
		public long LongPosition
		{
			get
			{
				return this.position64;
			}
		}

		// Token: 0x06007C22 RID: 31778 RVA: 0x00247BEC File Offset: 0x00247BEC
		internal void Ensure(int count, bool strict)
		{
			if (count > this.ioBuffer.Length)
			{
				BufferPool.ResizeAndFlushLeft(ref this.ioBuffer, count, this.ioIndex, this.available);
				this.ioIndex = 0;
			}
			else if (this.ioIndex + count >= this.ioBuffer.Length)
			{
				Buffer.BlockCopy(this.ioBuffer, this.ioIndex, this.ioBuffer, 0, this.available);
				this.ioIndex = 0;
			}
			count -= this.available;
			int num = this.ioIndex + this.available;
			int num2 = this.ioBuffer.Length - num;
			if (this.isFixedLength && this.dataRemaining64 < (long)num2)
			{
				num2 = (int)this.dataRemaining64;
			}
			int num3;
			while (count > 0 && num2 > 0 && (num3 = this.source.Read(this.ioBuffer, num, num2)) > 0)
			{
				this.available += num3;
				count -= num3;
				num2 -= num3;
				num += num3;
				if (this.isFixedLength)
				{
					this.dataRemaining64 -= (long)num3;
				}
			}
			if (strict && count > 0)
			{
				throw ProtoReader.EoF(this);
			}
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x00247D20 File Offset: 0x00247D20
		public short ReadInt16()
		{
			return checked((short)this.ReadInt32());
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x00247D2C File Offset: 0x00247D2C
		public ushort ReadUInt16()
		{
			return checked((ushort)this.ReadUInt32());
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x00247D38 File Offset: 0x00247D38
		public byte ReadByte()
		{
			return checked((byte)this.ReadUInt32());
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x00247D44 File Offset: 0x00247D44
		public sbyte ReadSByte()
		{
			return checked((sbyte)this.ReadInt32());
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x00247D50 File Offset: 0x00247D50
		public int ReadInt32()
		{
			WireType wireType = this.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType == WireType.Variant)
				{
					return (int)this.ReadUInt32Variant(true);
				}
				if (wireType == WireType.Fixed64)
				{
					long num = this.ReadInt64();
					return checked((int)num);
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					if (this.available < 4)
					{
						this.Ensure(4, true);
					}
					this.position64 += 4L;
					this.available -= 4;
					byte[] array = this.ioBuffer;
					int num2 = this.ioIndex;
					this.ioIndex = num2 + 1;
					int num3 = array[num2];
					byte[] array2 = this.ioBuffer;
					num2 = this.ioIndex;
					this.ioIndex = num2 + 1;
					int num4 = num3 | array2[num2] << 8;
					byte[] array3 = this.ioBuffer;
					num2 = this.ioIndex;
					this.ioIndex = num2 + 1;
					int num5 = num4 | array3[num2] << 16;
					byte[] array4 = this.ioBuffer;
					num2 = this.ioIndex;
					this.ioIndex = num2 + 1;
					return num5 | array4[num2] << 24;
				}
				if (wireType == WireType.SignedVariant)
				{
					return ProtoReader.Zag(this.ReadUInt32Variant(true));
				}
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x00247E58 File Offset: 0x00247E58
		private static int Zag(uint ziggedValue)
		{
			return (int)(-(ziggedValue & 1U) ^ (uint)((int)ziggedValue >> 1 & int.MaxValue));
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x00247E7C File Offset: 0x00247E7C
		private static long Zag(ulong ziggedValue)
		{
			return (long)(-(long)(ziggedValue & 1UL) ^ (ziggedValue >> 1 & 9223372036854775807UL));
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x00247EA4 File Offset: 0x00247EA4
		public long ReadInt64()
		{
			WireType wireType = this.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType == WireType.Variant)
				{
					return (long)this.ReadUInt64Variant();
				}
				if (wireType == WireType.Fixed64)
				{
					if (this.available < 8)
					{
						this.Ensure(8, true);
					}
					this.position64 += 8L;
					this.available -= 8;
					byte[] array = this.ioBuffer;
					int num = this.ioIndex;
					this.ioIndex = num + 1;
					long num2 = (long)array[num];
					byte[] array2 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num3 = num2 | (long)((long)array2[num] << 8);
					byte[] array3 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num4 = num3 | (long)((long)array3[num] << 16);
					byte[] array4 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num5 = num4 | (long)((long)array4[num] << 24);
					byte[] array5 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num6 = num5 | (long)((long)array5[num] << 32);
					byte[] array6 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num7 = num6 | (long)((long)array6[num] << 40);
					byte[] array7 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num8 = num7 | (long)((long)array7[num] << 48);
					byte[] array8 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					return num8 | (long)((long)array8[num] << 56);
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					return (long)this.ReadInt32();
				}
				if (wireType == WireType.SignedVariant)
				{
					return ProtoReader.Zag(this.ReadUInt64Variant());
				}
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x00248020 File Offset: 0x00248020
		private int TryReadUInt64VariantWithoutMoving(out ulong value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0UL;
				return 0;
			}
			int num = this.ioIndex;
			value = (ulong)this.ioBuffer[num++];
			if ((value & 128UL) == 0UL)
			{
				return 1;
			}
			value &= 127UL;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			ulong num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 7;
			if ((num2 & 128UL) == 0UL)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 14;
			if ((num2 & 128UL) == 0UL)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 21;
			if ((num2 & 128UL) == 0UL)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 28;
			if ((num2 & 128UL) == 0UL)
			{
				return 5;
			}
			if (this.available == 5)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 35;
			if ((num2 & 128UL) == 0UL)
			{
				return 6;
			}
			if (this.available == 6)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 42;
			if ((num2 & 128UL) == 0UL)
			{
				return 7;
			}
			if (this.available == 7)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 49;
			if ((num2 & 128UL) == 0UL)
			{
				return 8;
			}
			if (this.available == 8)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 56;
			if ((num2 & 128UL) == 0UL)
			{
				return 9;
			}
			if (this.available == 9)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num];
			value |= num2 << 63;
			if ((num2 & 18446744073709551614UL) != 0UL)
			{
				throw ProtoReader.AddErrorData(new OverflowException(), this);
			}
			return 10;
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x002482B0 File Offset: 0x002482B0
		private ulong ReadUInt64Variant()
		{
			ulong result;
			int num = this.TryReadUInt64VariantWithoutMoving(out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position64 += (long)num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x00248304 File Offset: 0x00248304
		private string Intern(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			string text;
			if (this.stringInterner == null)
			{
				this.stringInterner = new Dictionary<string, string>
				{
					{
						value,
						value
					}
				};
			}
			else if (this.stringInterner.TryGetValue(value, out text))
			{
				value = text;
			}
			else
			{
				this.stringInterner.Add(value, value);
			}
			return value;
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x0024837C File Offset: 0x0024837C
		public string ReadString()
		{
			if (this.wireType != WireType.String)
			{
				throw this.CreateWireTypeException();
			}
			int num = (int)this.ReadUInt32Variant(false);
			if (num == 0)
			{
				return "";
			}
			if (this.available < num)
			{
				this.Ensure(num, true);
			}
			string text = ProtoReader.encoding.GetString(this.ioBuffer, this.ioIndex, num);
			if (this.internStrings)
			{
				text = this.Intern(text);
			}
			this.available -= num;
			this.position64 += (long)num;
			this.ioIndex += num;
			return text;
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x00248420 File Offset: 0x00248420
		public void ThrowEnumException(Type type, int value)
		{
			string str = (type == null) ? "<null>" : type.FullName;
			throw ProtoReader.AddErrorData(new ProtoException("No " + str + " enum is mapped to the wire-value " + value.ToString()), this);
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x00248470 File Offset: 0x00248470
		private Exception CreateWireTypeException()
		{
			return this.CreateException("Invalid wire-type; this usually means you have over-written a file without truncating or setting the length; see https://stackoverflow.com/q/2152978/23354");
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x00248480 File Offset: 0x00248480
		private Exception CreateException(string message)
		{
			return ProtoReader.AddErrorData(new ProtoException(message), this);
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x00248490 File Offset: 0x00248490
		public unsafe double ReadDouble()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Fixed64)
			{
				long num = this.ReadInt64();
				return *(double*)(&num);
			}
			if (wireType == WireType.Fixed32)
			{
				return (double)this.ReadSingle();
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x002484D0 File Offset: 0x002484D0
		public static object ReadObject(object value, int key, ProtoReader reader)
		{
			return ProtoReader.ReadTypedObject(value, key, reader, null);
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x002484DC File Offset: 0x002484DC
		internal static object ReadTypedObject(object value, int key, ProtoReader reader, Type type)
		{
			if (reader.model == null)
			{
				throw ProtoReader.AddErrorData(new InvalidOperationException("Cannot deserialize sub-objects unless a model is provided"), reader);
			}
			SubItemToken token = ProtoReader.StartSubItem(reader);
			if (key >= 0)
			{
				value = reader.model.Deserialize(key, value, reader);
			}
			else if (!(type != null) || !reader.model.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false, null))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
			ProtoReader.EndSubItem(token, reader);
			return value;
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x00248564 File Offset: 0x00248564
		public static void EndSubItem(SubItemToken token, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			long value = token.value64;
			WireType wireType = reader.wireType;
			if (wireType == WireType.EndGroup)
			{
				if (value >= 0L)
				{
					throw ProtoReader.AddErrorData(new ArgumentException("token"), reader);
				}
				if (-(int)value != reader.fieldNumber)
				{
					throw reader.CreateException("Wrong group was ended");
				}
				reader.wireType = WireType.None;
				reader.depth--;
				return;
			}
			else
			{
				if (value < reader.position64)
				{
					throw reader.CreateException(string.Format("Sub-message not read entirely; expected {0}, was {1}", value, reader.position64));
				}
				if (reader.blockEnd64 != reader.position64 && reader.blockEnd64 != 9223372036854775807L)
				{
					throw reader.CreateException("Sub-message not read correctly");
				}
				reader.blockEnd64 = value;
				reader.depth--;
				return;
			}
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x00248658 File Offset: 0x00248658
		public static SubItemToken StartSubItem(ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				if (wireType == WireType.StartGroup)
				{
					reader.wireType = WireType.None;
					reader.depth++;
					return new SubItemToken((long)(-(long)reader.fieldNumber));
				}
				throw reader.CreateWireTypeException();
			}
			else
			{
				long num = (long)reader.ReadUInt64Variant();
				if (num < 0L)
				{
					throw ProtoReader.AddErrorData(new InvalidOperationException(), reader);
				}
				long value = reader.blockEnd64;
				reader.blockEnd64 = reader.position64 + num;
				reader.depth++;
				return new SubItemToken(value);
			}
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x00248700 File Offset: 0x00248700
		public int ReadFieldHeader()
		{
			if (this.blockEnd64 <= this.position64 || this.wireType == WireType.EndGroup)
			{
				return 0;
			}
			uint num;
			if (this.TryReadUInt32Variant(out num) && num != 0U)
			{
				this.wireType = (WireType)(num & 7U);
				this.fieldNumber = (int)(num >> 3);
				if (this.fieldNumber < 1)
				{
					throw new ProtoException("Invalid field in source data: " + this.fieldNumber.ToString());
				}
			}
			else
			{
				this.wireType = WireType.None;
				this.fieldNumber = 0;
			}
			if (this.wireType != WireType.EndGroup)
			{
				return this.fieldNumber;
			}
			if (this.depth > 0)
			{
				return 0;
			}
			throw new ProtoException("Unexpected end-group in source data; this usually means the source data is corrupt");
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x002487B8 File Offset: 0x002487B8
		public bool TryReadFieldHeader(int field)
		{
			if (this.blockEnd64 <= this.position64 || this.wireType == WireType.EndGroup)
			{
				return false;
			}
			uint num2;
			int num = this.TryReadUInt32VariantWithoutMoving(false, out num2);
			WireType wireType;
			if (num > 0 && (int)num2 >> 3 == field && (wireType = (WireType)(num2 & 7U)) != WireType.EndGroup)
			{
				this.wireType = wireType;
				this.fieldNumber = field;
				this.position64 += (long)num;
				this.ioIndex += num;
				this.available -= num;
				return true;
			}
			return false;
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06007C39 RID: 31801 RVA: 0x0024884C File Offset: 0x0024884C
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x00248854 File Offset: 0x00248854
		public void Hint(WireType wireType)
		{
			if (this.wireType != wireType && (wireType & (WireType)7) == this.wireType)
			{
				this.wireType = wireType;
			}
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x00248878 File Offset: 0x00248878
		public void Assert(WireType wireType)
		{
			if (this.wireType == wireType)
			{
				return;
			}
			if ((wireType & (WireType)7) == this.wireType)
			{
				this.wireType = wireType;
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x002488A4 File Offset: 0x002488A4
		public void SkipField()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
			case WireType.SignedVariant:
				this.ReadUInt64Variant();
				return;
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.available -= 8;
				this.ioIndex += 8;
				this.position64 += 8L;
				return;
			case WireType.String:
			{
				long num = (long)this.ReadUInt64Variant();
				if (num <= (long)this.available)
				{
					this.available -= (int)num;
					this.ioIndex += (int)num;
					this.position64 += num;
					return;
				}
				this.position64 += num;
				num -= (long)this.available;
				this.ioIndex = (this.available = 0);
				if (this.isFixedLength)
				{
					if (num > this.dataRemaining64)
					{
						throw ProtoReader.EoF(this);
					}
					this.dataRemaining64 -= num;
				}
				ProtoReader.Seek(this.source, num, this.ioBuffer);
				return;
			}
			case WireType.StartGroup:
			{
				int num2 = this.fieldNumber;
				this.depth++;
				while (this.ReadFieldHeader() > 0)
				{
					this.SkipField();
				}
				this.depth--;
				if (this.wireType == WireType.EndGroup && this.fieldNumber == num2)
				{
					this.wireType = WireType.None;
					return;
				}
				throw this.CreateWireTypeException();
			}
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.available -= 4;
				this.ioIndex += 4;
				this.position64 += 4L;
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x00248A88 File Offset: 0x00248A88
		public ulong ReadUInt64()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Variant)
			{
				return this.ReadUInt64Variant();
			}
			if (wireType == WireType.Fixed64)
			{
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position64 += 8L;
				this.available -= 8;
				byte[] array = this.ioBuffer;
				int num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num2 = array[num];
				byte[] array2 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num3 = num2 | array2[num] << 8;
				byte[] array3 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num4 = num3 | array3[num] << 16;
				byte[] array4 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num5 = num4 | array4[num] << 24;
				byte[] array5 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num6 = num5 | array5[num] << 32;
				byte[] array6 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num7 = num6 | array6[num] << 40;
				byte[] array7 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num8 = num7 | array7[num] << 48;
				byte[] array8 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				return num8 | array8[num] << 56;
			}
			if (wireType != WireType.Fixed32)
			{
				throw this.CreateWireTypeException();
			}
			return (ulong)this.ReadUInt32();
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x00248BE4 File Offset: 0x00248BE4
		public unsafe float ReadSingle()
		{
			WireType wireType = this.wireType;
			if (wireType != WireType.Fixed64)
			{
				if (wireType == WireType.Fixed32)
				{
					int num = this.ReadInt32();
					return *(float*)(&num);
				}
				throw this.CreateWireTypeException();
			}
			else
			{
				double num2 = this.ReadDouble();
				float num3 = (float)num2;
				if (float.IsInfinity(num3) && !double.IsInfinity(num2))
				{
					throw ProtoReader.AddErrorData(new OverflowException(), this);
				}
				return num3;
			}
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x00248C4C File Offset: 0x00248C4C
		public bool ReadBoolean()
		{
			uint num = this.ReadUInt32();
			if (num == 0U)
			{
				return false;
			}
			if (num != 1U)
			{
				throw this.CreateException("Unexpected boolean value");
			}
			return true;
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x00248C88 File Offset: 0x00248C88
		public static byte[] AppendBytes(byte[] value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType == WireType.Variant)
			{
				return new byte[0];
			}
			if (wireType != WireType.String)
			{
				throw reader.CreateWireTypeException();
			}
			int i = (int)reader.ReadUInt32Variant(false);
			reader.wireType = WireType.None;
			if (i == 0)
			{
				return value ?? ProtoReader.EmptyBlob;
			}
			int num;
			if (value == null || value.Length == 0)
			{
				num = 0;
				value = new byte[i];
			}
			else
			{
				num = value.Length;
				byte[] array = new byte[value.Length + i];
				Buffer.BlockCopy(value, 0, array, 0, value.Length);
				value = array;
			}
			reader.position64 += (long)i;
			while (i > reader.available)
			{
				if (reader.available > 0)
				{
					Buffer.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, reader.available);
					i -= reader.available;
					num += reader.available;
					reader.ioIndex = (reader.available = 0);
				}
				int num2 = (i > reader.ioBuffer.Length) ? reader.ioBuffer.Length : i;
				if (num2 > 0)
				{
					reader.Ensure(num2, true);
				}
			}
			if (i > 0)
			{
				Buffer.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, i);
				reader.ioIndex += i;
				reader.available -= i;
			}
			return value;
		}

		// Token: 0x06007C41 RID: 31809 RVA: 0x00248DFC File Offset: 0x00248DFC
		private static int ReadByteOrThrow(Stream source)
		{
			int num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			return num;
		}

		// Token: 0x06007C42 RID: 31810 RVA: 0x00248E24 File Offset: 0x00248E24
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber)
		{
			int num;
			return ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out num);
		}

		// Token: 0x06007C43 RID: 31811 RVA: 0x00248E40 File Offset: 0x00248E40
		public static int DirectReadLittleEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x00248E68 File Offset: 0x00248E68
		public static int DirectReadBigEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x00248E90 File Offset: 0x00248E90
		public static int DirectReadVarintInt32(Stream source)
		{
			ulong num2;
			int num = ProtoReader.TryReadUInt64Variant(source, out num2);
			if (num <= 0)
			{
				throw ProtoReader.EoF(null);
			}
			return checked((int)num2);
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x00248EBC File Offset: 0x00248EBC
		public static void DirectReadBytes(Stream source, byte[] buffer, int offset, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			while (count > 0 && (num = source.Read(buffer, offset, count)) > 0)
			{
				count -= num;
				offset += num;
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x00248F0C File Offset: 0x00248F0C
		public static byte[] DirectReadBytes(Stream source, int count)
		{
			byte[] array = new byte[count];
			ProtoReader.DirectReadBytes(source, array, 0, count);
			return array;
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x00248F30 File Offset: 0x00248F30
		public static string DirectReadString(Stream source, int length)
		{
			byte[] array = new byte[length];
			ProtoReader.DirectReadBytes(source, array, 0, length);
			return Encoding.UTF8.GetString(array, 0, length);
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x00248F60 File Offset: 0x00248F60
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber, out int bytesRead)
		{
			if (style == PrefixStyle.None)
			{
				bytesRead = (fieldNumber = 0);
				return int.MaxValue;
			}
			long num = ProtoReader.ReadLongLengthPrefix(source, expectHeader, style, out fieldNumber, out bytesRead);
			return checked((int)num);
		}

		// Token: 0x06007C4A RID: 31818 RVA: 0x00248F94 File Offset: 0x00248F94
		public static long ReadLongLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber, out int bytesRead)
		{
			fieldNumber = 0;
			switch (style)
			{
			case PrefixStyle.None:
				bytesRead = 0;
				return long.MaxValue;
			case PrefixStyle.Base128:
				bytesRead = 0;
				if (expectHeader)
				{
					ulong num2;
					int num = ProtoReader.TryReadUInt64Variant(source, out num2);
					bytesRead += num;
					if (num <= 0)
					{
						bytesRead = 0;
						return -1L;
					}
					if ((num2 & 7UL) != 2UL)
					{
						throw new InvalidOperationException();
					}
					fieldNumber = (int)(num2 >> 3);
					num = ProtoReader.TryReadUInt64Variant(source, out num2);
					bytesRead += num;
					if (bytesRead == 0)
					{
						throw ProtoReader.EoF(null);
					}
					return (long)num2;
				}
				else
				{
					ulong num2;
					int num = ProtoReader.TryReadUInt64Variant(source, out num2);
					bytesRead += num;
					if (bytesRead >= 0)
					{
						return (long)num2;
					}
					return -1L;
				}
				break;
			case PrefixStyle.Fixed32:
			{
				int num3 = source.ReadByte();
				if (num3 < 0)
				{
					bytesRead = 0;
					return -1L;
				}
				bytesRead = 4;
				return (long)(num3 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24);
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num4 = source.ReadByte();
				if (num4 < 0)
				{
					bytesRead = 0;
					return -1L;
				}
				bytesRead = 4;
				return (long)(num4 << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source));
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x002490D0 File Offset: 0x002490D0
		private static int TryReadUInt64Variant(Stream source, out ulong value)
		{
			value = 0UL;
			int num = source.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			value = (ulong)num;
			if ((value & 128UL) == 0UL)
			{
				return 1;
			}
			value &= 127UL;
			int i = 1;
			int num2 = 7;
			while (i < 9)
			{
				num = source.ReadByte();
				if (num < 0)
				{
					throw ProtoReader.EoF(null);
				}
				value |= (ulong)((ulong)((long)num & 127L) << num2);
				num2 += 7;
				i++;
				if ((num & 128) == 0)
				{
					return i;
				}
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			if ((num & 1) == 0)
			{
				value |= (ulong)((ulong)((long)num & 127L) << num2);
				return i + 1;
			}
			throw new OverflowException();
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x00249194 File Offset: 0x00249194
		internal static void Seek(Stream source, long count, byte[] buffer)
		{
			if (source.CanSeek)
			{
				source.Seek(count, SeekOrigin.Current);
				count = 0L;
			}
			else if (buffer != null)
			{
				while (count > (long)buffer.Length)
				{
					int num;
					if ((num = source.Read(buffer, 0, buffer.Length)) <= 0)
					{
						break;
					}
					count -= (long)num;
				}
				while (count > 0L)
				{
					int num;
					if ((num = source.Read(buffer, 0, (int)count)) <= 0)
					{
						break;
					}
					count -= (long)num;
				}
			}
			else
			{
				buffer = BufferPool.GetBuffer();
				try
				{
					int num2;
					while (count > (long)buffer.Length)
					{
						if ((num2 = source.Read(buffer, 0, buffer.Length)) <= 0)
						{
							break;
						}
						count -= (long)num2;
					}
					while (count > 0L && (num2 = source.Read(buffer, 0, (int)count)) > 0)
					{
						count -= (long)num2;
					}
				}
				finally
				{
					BufferPool.ReleaseBufferToPool(ref buffer);
				}
			}
			if (count > 0L)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x0024928C File Offset: 0x0024928C
		internal static Exception AddErrorData(Exception exception, ProtoReader source)
		{
			if (exception != null && source != null && !exception.Data.Contains("protoSource"))
			{
				exception.Data.Add("protoSource", string.Format("tag={0}; wire-type={1}; offset={2}; depth={3}", new object[]
				{
					source.fieldNumber,
					source.wireType,
					source.position64,
					source.depth
				}));
			}
			return exception;
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x00249318 File Offset: 0x00249318
		private static Exception EoF(ProtoReader source)
		{
			return ProtoReader.AddErrorData(new EndOfStreamException(), source);
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x00249328 File Offset: 0x00249328
		public void AppendExtensionData(IExtensible instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IExtension extensionObject = instance.GetExtensionObject(true);
			bool commit = false;
			Stream stream = extensionObject.BeginAppend();
			try
			{
				using (ProtoWriter protoWriter = ProtoWriter.Create(stream, this.model, null))
				{
					this.AppendExtensionField(protoWriter);
					protoWriter.Close();
				}
				commit = true;
			}
			finally
			{
				extensionObject.EndAppend(stream, commit);
			}
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x002493B0 File Offset: 0x002493B0
		private void AppendExtensionField(ProtoWriter writer)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, writer);
			switch (this.wireType)
			{
			case WireType.Variant:
			case WireType.Fixed64:
			case WireType.SignedVariant:
				ProtoWriter.WriteInt64(this.ReadInt64(), writer);
				return;
			case WireType.String:
				ProtoWriter.WriteBytes(ProtoReader.AppendBytes(null, this), writer);
				return;
			case WireType.StartGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(this);
				SubItemToken token2 = ProtoWriter.StartSubItem(null, writer);
				while (this.ReadFieldHeader() > 0)
				{
					this.AppendExtensionField(writer);
				}
				ProtoReader.EndSubItem(token, this);
				ProtoWriter.EndSubItem(token2, writer);
				return;
			}
			case WireType.Fixed32:
				ProtoWriter.WriteInt32(this.ReadInt32(), writer);
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06007C51 RID: 31825 RVA: 0x00249470 File Offset: 0x00249470
		public static bool HasSubValue(WireType wireType, ProtoReader source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.blockEnd64 <= source.position64 || wireType == WireType.EndGroup)
			{
				return false;
			}
			source.wireType = wireType;
			return true;
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x002494A8 File Offset: 0x002494A8
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x06007C53 RID: 31827 RVA: 0x002494B8 File Offset: 0x002494B8
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x06007C54 RID: 31828 RVA: 0x002494C0 File Offset: 0x002494C0
		internal Type DeserializeType(string value)
		{
			return TypeModel.DeserializeType(this.model, value);
		}

		// Token: 0x06007C55 RID: 31829 RVA: 0x002494D0 File Offset: 0x002494D0
		internal void SetRootObject(object value)
		{
			this.netCache.SetKeyedObject(0, value);
			this.trapCount -= 1U;
		}

		// Token: 0x06007C56 RID: 31830 RVA: 0x002494F0 File Offset: 0x002494F0
		public static void NoteObject(object value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.trapCount != 0U)
			{
				reader.netCache.RegisterTrappedObject(value);
				reader.trapCount -= 1U;
			}
		}

		// Token: 0x06007C57 RID: 31831 RVA: 0x00249528 File Offset: 0x00249528
		public Type ReadType()
		{
			return TypeModel.DeserializeType(this.model, this.ReadString());
		}

		// Token: 0x06007C58 RID: 31832 RVA: 0x0024953C File Offset: 0x0024953C
		internal void TrapNextObject(int newObjectKey)
		{
			this.trapCount += 1U;
			this.netCache.SetKeyedObject(newObjectKey, null);
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x0024955C File Offset: 0x0024955C
		internal void CheckFullyConsumed()
		{
			if (this.isFixedLength)
			{
				if (this.dataRemaining64 != 0L)
				{
					throw new ProtoException("Incorrect number of bytes consumed");
				}
			}
			else if (this.available != 0)
			{
				throw new ProtoException("Unconsumed data left in the buffer; this suggests corrupt input");
			}
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x00249598 File Offset: 0x00249598
		public static object Merge(ProtoReader parent, object from, object to)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			TypeModel typeModel = parent.Model;
			SerializationContext serializationContext = parent.Context;
			if (typeModel == null)
			{
				throw new InvalidOperationException("Types cannot be merged unless a type-model has been specified");
			}
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				typeModel.Serialize(memoryStream, from, serializationContext);
				memoryStream.Position = 0L;
				result = typeModel.Deserialize(memoryStream, to, null);
			}
			return result;
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x0024961C File Offset: 0x0024961C
		internal static ProtoReader Create(Stream source, TypeModel model, SerializationContext context, int len)
		{
			return ProtoReader.Create(source, model, context, (long)len);
		}

		// Token: 0x06007C5C RID: 31836 RVA: 0x00249628 File Offset: 0x00249628
		public static ProtoReader Create(Stream source, TypeModel model, SerializationContext context = null, long length = -1L)
		{
			ProtoReader recycled = ProtoReader.GetRecycled();
			if (recycled == null)
			{
				return new ProtoReader(source, model, context, length);
			}
			ProtoReader.Init(recycled, source, model, context, length);
			return recycled;
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x0024965C File Offset: 0x0024965C
		private static ProtoReader GetRecycled()
		{
			ProtoReader result = ProtoReader.lastReader;
			ProtoReader.lastReader = null;
			return result;
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x0024967C File Offset: 0x0024967C
		internal static void Recycle(ProtoReader reader)
		{
			if (reader != null)
			{
				reader.Dispose();
				ProtoReader.lastReader = reader;
			}
		}

		// Token: 0x04003BF6 RID: 15350
		private Stream source;

		// Token: 0x04003BF7 RID: 15351
		private byte[] ioBuffer;

		// Token: 0x04003BF8 RID: 15352
		private TypeModel model;

		// Token: 0x04003BF9 RID: 15353
		private int fieldNumber;

		// Token: 0x04003BFA RID: 15354
		private int depth;

		// Token: 0x04003BFB RID: 15355
		private int ioIndex;

		// Token: 0x04003BFC RID: 15356
		private int available;

		// Token: 0x04003BFD RID: 15357
		private long position64;

		// Token: 0x04003BFE RID: 15358
		private long blockEnd64;

		// Token: 0x04003BFF RID: 15359
		private long dataRemaining64;

		// Token: 0x04003C00 RID: 15360
		private WireType wireType;

		// Token: 0x04003C01 RID: 15361
		private bool isFixedLength;

		// Token: 0x04003C02 RID: 15362
		private bool internStrings;

		// Token: 0x04003C03 RID: 15363
		private NetObjectCache netCache;

		// Token: 0x04003C04 RID: 15364
		private uint trapCount;

		// Token: 0x04003C05 RID: 15365
		internal const long TO_EOF = -1L;

		// Token: 0x04003C06 RID: 15366
		private SerializationContext context;

		// Token: 0x04003C07 RID: 15367
		private const long Int64Msb = -9223372036854775808L;

		// Token: 0x04003C08 RID: 15368
		private const int Int32Msb = -2147483648;

		// Token: 0x04003C09 RID: 15369
		private Dictionary<string, string> stringInterner;

		// Token: 0x04003C0A RID: 15370
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04003C0B RID: 15371
		private static readonly byte[] EmptyBlob = new byte[0];

		// Token: 0x04003C0C RID: 15372
		[ThreadStatic]
		private static ProtoReader lastReader;
	}
}
