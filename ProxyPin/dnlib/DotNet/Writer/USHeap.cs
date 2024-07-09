using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008E0 RID: 2272
	[ComVisible(true)]
	public sealed class USHeap : HeapBase, IOffsetHeap<string>
	{
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x001B2E34 File Offset: 0x001B2E34
		public override string Name
		{
			get
			{
				return "#US";
			}
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x001B2E3C File Offset: 0x001B2E3C
		public void Populate(USStream usStream)
		{
			if (this.originalData != null)
			{
				throw new InvalidOperationException("Can't call method twice");
			}
			if (this.nextOffset != 1U)
			{
				throw new InvalidOperationException("Add() has already been called");
			}
			if (usStream == null || usStream.StreamLength == 0U)
			{
				return;
			}
			DataReader dataReader = usStream.CreateReader();
			this.originalData = dataReader.ToArray();
			this.nextOffset = (uint)this.originalData.Length;
			this.Populate(ref dataReader);
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x001B2EB8 File Offset: 0x001B2EB8
		private void Populate(ref DataReader reader)
		{
			reader.Position = 1U;
			while (reader.Position < reader.Length)
			{
				uint position = reader.Position;
				uint num;
				if (!reader.TryReadCompressedUInt32(out num))
				{
					if (position == reader.Position)
					{
						uint position2 = reader.Position;
						reader.Position = position2 + 1U;
					}
				}
				else if (num != 0U && (ulong)reader.Position + (ulong)num <= (ulong)reader.Length)
				{
					int chars = (int)(num / 2U);
					string key = reader.ReadUtf16String(chars);
					if ((num & 1U) != 0U)
					{
						reader.ReadByte();
					}
					if (!this.cachedDict.ContainsKey(key))
					{
						this.cachedDict[key] = position;
					}
				}
			}
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x001B2F74 File Offset: 0x001B2F74
		public uint Add(string s)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #US when it's read-only");
			}
			if (s == null)
			{
				s = string.Empty;
			}
			uint result;
			if (this.cachedDict.TryGetValue(s, out result))
			{
				return result;
			}
			return this.AddToCache(s);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x001B2FC4 File Offset: 0x001B2FC4
		public uint Create(string s)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #US when it's read-only");
			}
			return this.AddToCache(s ?? string.Empty);
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x001B2FF0 File Offset: 0x001B2FF0
		private uint AddToCache(string s)
		{
			this.cached.Add(s);
			uint num = this.cachedDict[s] = this.nextOffset;
			this.nextOffset += (uint)this.GetRawDataSize(s);
			if (num > 16777215U)
			{
				throw new ModuleWriterException("#US heap is too big");
			}
			return num;
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x001B304C File Offset: 0x001B304C
		public override uint GetRawLength()
		{
			return this.nextOffset;
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x001B3054 File Offset: 0x001B3054
		protected override void WriteToImpl(DataWriter writer)
		{
			if (this.originalData != null)
			{
				writer.WriteBytes(this.originalData);
			}
			else
			{
				writer.WriteByte(0);
			}
			uint num = (uint)((this.originalData != null) ? this.originalData.Length : 1);
			foreach (string text in this.cached)
			{
				int rawDataSize = this.GetRawDataSize(text);
				byte[] array;
				if (this.userRawData != null && this.userRawData.TryGetValue(num, out array))
				{
					if (array.Length != rawDataSize)
					{
						throw new InvalidOperationException("Invalid length of raw data");
					}
					writer.WriteBytes(array);
				}
				else
				{
					this.WriteString(writer, text);
				}
				num += (uint)rawDataSize;
			}
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x001B3138 File Offset: 0x001B3138
		private void WriteString(DataWriter writer, string s)
		{
			writer.WriteCompressedUInt32((uint)(s.Length * 2 + 1));
			byte value = 0;
			foreach (ushort num in s)
			{
				writer.WriteUInt16(num);
				if (num > 255 || (1 <= num && num <= 8) || (14 <= num && num <= 31) || num == 39 || num == 45 || num == 127)
				{
					value = 1;
				}
			}
			writer.WriteByte(value);
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x001B31C8 File Offset: 0x001B31C8
		public int GetRawDataSize(string data)
		{
			return DataWriter.GetCompressedUInt32Length((uint)(data.Length * 2 + 1)) + data.Length * 2 + 1;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x001B31E4 File Offset: 0x001B31E4
		public void SetRawData(uint offset, byte[] rawData)
		{
			if (this.userRawData == null)
			{
				this.userRawData = new Dictionary<uint, byte[]>();
			}
			Dictionary<uint, byte[]> dictionary = this.userRawData;
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			dictionary[offset] = rawData;
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x001B321C File Offset: 0x001B321C
		public IEnumerable<KeyValuePair<uint, byte[]>> GetAllRawData()
		{
			MemoryStream memStream = new MemoryStream();
			DataWriter writer = new DataWriter(memStream);
			uint offset = (uint)((this.originalData != null) ? this.originalData.Length : 1);
			foreach (string s in this.cached)
			{
				memStream.Position = 0L;
				memStream.SetLength(0L);
				this.WriteString(writer, s);
				yield return new KeyValuePair<uint, byte[]>(offset, memStream.ToArray());
				offset += (uint)memStream.Length;
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04002ACC RID: 10956
		private readonly Dictionary<string, uint> cachedDict = new Dictionary<string, uint>(StringComparer.Ordinal);

		// Token: 0x04002ACD RID: 10957
		private readonly List<string> cached = new List<string>();

		// Token: 0x04002ACE RID: 10958
		private uint nextOffset = 1U;

		// Token: 0x04002ACF RID: 10959
		private byte[] originalData;

		// Token: 0x04002AD0 RID: 10960
		private Dictionary<uint, byte[]> userRawData;
	}
}
