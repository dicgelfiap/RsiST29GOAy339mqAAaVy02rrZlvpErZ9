using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DB RID: 2267
	[ComVisible(true)]
	public sealed class StringsHeap : HeapBase, IOffsetHeap<UTF8String>
	{
		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06005837 RID: 22583 RVA: 0x001B1438 File Offset: 0x001B1438
		public override string Name
		{
			get
			{
				return "#Strings";
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x001B1440 File Offset: 0x001B1440
		public void Populate(StringsStream stringsStream)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Strings when it's read-only");
			}
			if (this.originalData != null)
			{
				throw new InvalidOperationException("Can't call method twice");
			}
			if (this.nextOffset != 1U)
			{
				throw new InvalidOperationException("Add() has already been called");
			}
			if (stringsStream == null || stringsStream.StreamLength == 0U)
			{
				return;
			}
			DataReader dataReader = stringsStream.CreateReader();
			this.originalData = dataReader.ToArray();
			this.nextOffset = (uint)this.originalData.Length;
			this.Populate(ref dataReader);
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x001B14D0 File Offset: 0x001B14D0
		private void Populate(ref DataReader reader)
		{
			reader.Position = 1U;
			while (reader.Position < reader.Length)
			{
				uint position = reader.Position;
				byte[] array = reader.TryReadBytesUntil(0);
				if (array == null)
				{
					break;
				}
				reader.ReadByte();
				if (array.Length != 0)
				{
					UTF8String key = new UTF8String(array);
					if (!this.cachedDict.ContainsKey(key))
					{
						this.cachedDict[key] = position;
					}
				}
			}
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001B1544 File Offset: 0x001B1544
		internal void AddOptimizedStringsAndSetReadOnly()
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Strings when it's read-only");
			}
			base.SetReadOnly();
			this.stringsOffsetInfos.Sort(StringsHeap.Comparison_StringsOffsetInfoSorter);
			StringsHeap.StringsOffsetInfo stringsOffsetInfo = null;
			foreach (StringsHeap.StringsOffsetInfo stringsOffsetInfo2 in this.stringsOffsetInfos)
			{
				if (stringsOffsetInfo != null && StringsHeap.EndsWith(stringsOffsetInfo.Value, stringsOffsetInfo2.Value))
				{
					stringsOffsetInfo2.StringsOffset = stringsOffsetInfo.StringsOffset + (uint)(stringsOffsetInfo.Value.Data.Length - stringsOffsetInfo2.Value.Data.Length);
				}
				else
				{
					stringsOffsetInfo2.StringsOffset = this.AddToCache(stringsOffsetInfo2.Value);
				}
				stringsOffsetInfo = stringsOffsetInfo2;
			}
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x001B1624 File Offset: 0x001B1624
		private static bool EndsWith(UTF8String s, UTF8String value)
		{
			byte[] data = s.Data;
			byte[] data2 = value.Data;
			int num = data.Length - data2.Length;
			if (num < 0)
			{
				return false;
			}
			for (int i = 0; i < data2.Length; i++)
			{
				if (data[num] != data2[i])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x001B1678 File Offset: 0x001B1678
		private static int StringsOffsetInfoSorter(StringsHeap.StringsOffsetInfo a, StringsHeap.StringsOffsetInfo b)
		{
			byte[] data = a.Value.Data;
			byte[] data2 = b.Value.Data;
			int num = data.Length - 1;
			int num2 = data2.Length - 1;
			for (int i = Math.Min(data.Length, data2.Length); i > 0; i--)
			{
				int num3 = (int)(data[num] - data2[num2]);
				if (num3 != 0)
				{
					return num3;
				}
				num--;
				num2--;
			}
			return data2.Length - data.Length;
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x001B16EC File Offset: 0x001B16EC
		public uint Add(UTF8String s)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Strings when it's read-only");
			}
			if (UTF8String.IsNullOrEmpty(s))
			{
				return 0U;
			}
			StringsHeap.StringsOffsetInfo stringsOffsetInfo;
			if (this.toStringsOffsetInfo.TryGetValue(s, out stringsOffsetInfo))
			{
				return stringsOffsetInfo.StringsId;
			}
			uint result;
			if (this.cachedDict.TryGetValue(s, out result))
			{
				return result;
			}
			if (Array.IndexOf<byte>(s.Data, 0) >= 0)
			{
				throw new ArgumentException("Strings in the #Strings heap can't contain NUL bytes");
			}
			uint num = this.stringsId;
			this.stringsId = num + 1U;
			stringsOffsetInfo = new StringsHeap.StringsOffsetInfo(s, num);
			this.toStringsOffsetInfo[s] = stringsOffsetInfo;
			this.offsetIdToInfo[stringsOffsetInfo.StringsId] = stringsOffsetInfo;
			this.stringsOffsetInfos.Add(stringsOffsetInfo);
			return stringsOffsetInfo.StringsId;
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001B17B8 File Offset: 0x001B17B8
		public uint GetOffset(uint offsetId)
		{
			if (!this.isReadOnly)
			{
				throw new ModuleWriterException("This method can only be called after all strings have been added and this heap is read-only");
			}
			if ((offsetId & 2147483648U) == 0U)
			{
				return offsetId;
			}
			StringsHeap.StringsOffsetInfo stringsOffsetInfo;
			if (this.offsetIdToInfo.TryGetValue(offsetId, out stringsOffsetInfo))
			{
				return stringsOffsetInfo.StringsOffset;
			}
			throw new ArgumentOutOfRangeException("offsetId");
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001B1814 File Offset: 0x001B1814
		public uint Create(UTF8String s)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Strings when it's read-only");
			}
			if (UTF8String.IsNullOrEmpty(s))
			{
				s = UTF8String.Empty;
			}
			if (Array.IndexOf<byte>(s.Data, 0) >= 0)
			{
				throw new ArgumentException("Strings in the #Strings heap can't contain NUL bytes");
			}
			return this.AddToCache(s);
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001B1874 File Offset: 0x001B1874
		private uint AddToCache(UTF8String s)
		{
			this.cached.Add(s);
			uint result = this.cachedDict[s] = this.nextOffset;
			this.nextOffset += (uint)(s.Data.Length + 1);
			return result;
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x001B18C0 File Offset: 0x001B18C0
		public override uint GetRawLength()
		{
			return this.nextOffset;
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x001B18C8 File Offset: 0x001B18C8
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
			foreach (UTF8String utf8String in this.cached)
			{
				byte[] array;
				if (this.userRawData != null && this.userRawData.TryGetValue(num, out array))
				{
					if (array.Length != utf8String.Data.Length + 1)
					{
						throw new InvalidOperationException("Invalid length of raw data");
					}
					writer.WriteBytes(array);
				}
				else
				{
					writer.WriteBytes(utf8String.Data);
					writer.WriteByte(0);
				}
				num += (uint)(utf8String.Data.Length + 1);
			}
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x001B19C0 File Offset: 0x001B19C0
		public int GetRawDataSize(UTF8String data)
		{
			return data.Data.Length + 1;
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001B19CC File Offset: 0x001B19CC
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

		// Token: 0x06005845 RID: 22597 RVA: 0x001B1A04 File Offset: 0x001B1A04
		public IEnumerable<KeyValuePair<uint, byte[]>> GetAllRawData()
		{
			uint offset = (uint)((this.originalData != null) ? this.originalData.Length : 1);
			foreach (UTF8String utf8String in this.cached)
			{
				byte[] rawData = new byte[utf8String.Data.Length + 1];
				Array.Copy(utf8String.Data, rawData, utf8String.Data.Length);
				yield return new KeyValuePair<uint, byte[]>(offset, rawData);
				offset += (uint)rawData.Length;
				rawData = null;
			}
			List<UTF8String>.Enumerator enumerator = default(List<UTF8String>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04002A75 RID: 10869
		private readonly Dictionary<UTF8String, uint> cachedDict = new Dictionary<UTF8String, uint>(UTF8StringEqualityComparer.Instance);

		// Token: 0x04002A76 RID: 10870
		private readonly List<UTF8String> cached = new List<UTF8String>();

		// Token: 0x04002A77 RID: 10871
		private uint nextOffset = 1U;

		// Token: 0x04002A78 RID: 10872
		private byte[] originalData;

		// Token: 0x04002A79 RID: 10873
		private Dictionary<uint, byte[]> userRawData;

		// Token: 0x04002A7A RID: 10874
		private readonly Dictionary<UTF8String, StringsHeap.StringsOffsetInfo> toStringsOffsetInfo = new Dictionary<UTF8String, StringsHeap.StringsOffsetInfo>(UTF8StringEqualityComparer.Instance);

		// Token: 0x04002A7B RID: 10875
		private readonly Dictionary<uint, StringsHeap.StringsOffsetInfo> offsetIdToInfo = new Dictionary<uint, StringsHeap.StringsOffsetInfo>();

		// Token: 0x04002A7C RID: 10876
		private readonly List<StringsHeap.StringsOffsetInfo> stringsOffsetInfos = new List<StringsHeap.StringsOffsetInfo>();

		// Token: 0x04002A7D RID: 10877
		private const uint STRINGS_ID_FLAG = 2147483648U;

		// Token: 0x04002A7E RID: 10878
		private uint stringsId = 2147483648U;

		// Token: 0x04002A7F RID: 10879
		private static readonly Comparison<StringsHeap.StringsOffsetInfo> Comparison_StringsOffsetInfoSorter = new Comparison<StringsHeap.StringsOffsetInfo>(StringsHeap.StringsOffsetInfoSorter);

		// Token: 0x02001029 RID: 4137
		private sealed class StringsOffsetInfo
		{
			// Token: 0x06008F96 RID: 36758 RVA: 0x002ACB14 File Offset: 0x002ACB14
			public StringsOffsetInfo(UTF8String value, uint stringsId)
			{
				this.Value = value;
				this.StringsId = stringsId;
			}

			// Token: 0x06008F97 RID: 36759 RVA: 0x002ACB2C File Offset: 0x002ACB2C
			public override string ToString()
			{
				return string.Format("{0:X8} {1:X4} {2}", this.StringsId, this.StringsOffset, this.Value.String);
			}

			// Token: 0x040044E0 RID: 17632
			public readonly UTF8String Value;

			// Token: 0x040044E1 RID: 17633
			public readonly uint StringsId;

			// Token: 0x040044E2 RID: 17634
			public uint StringsOffset;
		}
	}
}
