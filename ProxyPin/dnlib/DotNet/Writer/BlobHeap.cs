using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200088E RID: 2190
	[ComVisible(true)]
	public sealed class BlobHeap : HeapBase, IOffsetHeap<byte[]>
	{
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x00198D3C File Offset: 0x00198D3C
		public override string Name
		{
			get
			{
				return "#Blob";
			}
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x00198D44 File Offset: 0x00198D44
		public void Populate(BlobStream blobStream)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Blob when it's read-only");
			}
			if (this.originalData != null)
			{
				throw new InvalidOperationException("Can't call method twice");
			}
			if (this.nextOffset != 1U)
			{
				throw new InvalidOperationException("Add() has already been called");
			}
			if (blobStream == null || blobStream.StreamLength == 0U)
			{
				return;
			}
			DataReader dataReader = blobStream.CreateReader();
			this.originalData = dataReader.ToArray();
			this.nextOffset = (uint)this.originalData.Length;
			this.Populate(ref dataReader);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00198DD4 File Offset: 0x00198DD4
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
					byte[] key = reader.ReadBytes((int)num);
					if (!this.cachedDict.ContainsKey(key))
					{
						this.cachedDict[key] = position;
					}
				}
			}
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00198E7C File Offset: 0x00198E7C
		public uint Add(byte[] data)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Blob when it's read-only");
			}
			if (data == null || data.Length == 0)
			{
				return 0U;
			}
			uint result;
			if (this.cachedDict.TryGetValue(data, out result))
			{
				return result;
			}
			return this.AddToCache(data);
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00198ED0 File Offset: 0x00198ED0
		public uint Create(byte[] data)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #Blob when it's read-only");
			}
			return this.AddToCache(data ?? Array2.Empty<byte>());
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x00198EFC File Offset: 0x00198EFC
		private uint AddToCache(byte[] data)
		{
			this.cached.Add(data);
			uint result = this.cachedDict[data] = this.nextOffset;
			this.nextOffset += (uint)this.GetRawDataSize(data);
			return result;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00198F44 File Offset: 0x00198F44
		public override uint GetRawLength()
		{
			return this.nextOffset;
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x00198F4C File Offset: 0x00198F4C
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
			foreach (byte[] array in this.cached)
			{
				int rawDataSize = this.GetRawDataSize(array);
				byte[] array2;
				if (this.userRawData != null && this.userRawData.TryGetValue(num, out array2))
				{
					if (array2.Length != rawDataSize)
					{
						throw new InvalidOperationException("Invalid length of raw data");
					}
					writer.WriteBytes(array2);
				}
				else
				{
					writer.WriteCompressedUInt32((uint)array.Length);
					writer.WriteBytes(array);
				}
				num += (uint)rawDataSize;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00199038 File Offset: 0x00199038
		public int GetRawDataSize(byte[] data)
		{
			return DataWriter.GetCompressedUInt32Length((uint)data.Length) + data.Length;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00199048 File Offset: 0x00199048
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

		// Token: 0x060053DE RID: 21470 RVA: 0x00199080 File Offset: 0x00199080
		public IEnumerable<KeyValuePair<uint, byte[]>> GetAllRawData()
		{
			MemoryStream memStream = new MemoryStream();
			DataWriter writer = new DataWriter(memStream);
			uint offset = (uint)((this.originalData != null) ? this.originalData.Length : 1);
			foreach (byte[] array in this.cached)
			{
				memStream.Position = 0L;
				memStream.SetLength(0L);
				writer.WriteCompressedUInt32((uint)array.Length);
				writer.WriteBytes(array);
				yield return new KeyValuePair<uint, byte[]>(offset, memStream.ToArray());
				offset += (uint)memStream.Length;
			}
			List<byte[]>.Enumerator enumerator = default(List<byte[]>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400284B RID: 10315
		private readonly Dictionary<byte[], uint> cachedDict = new Dictionary<byte[], uint>(ByteArrayEqualityComparer.Instance);

		// Token: 0x0400284C RID: 10316
		private readonly List<byte[]> cached = new List<byte[]>();

		// Token: 0x0400284D RID: 10317
		private uint nextOffset = 1U;

		// Token: 0x0400284E RID: 10318
		private byte[] originalData;

		// Token: 0x0400284F RID: 10319
		private Dictionary<uint, byte[]> userRawData;
	}
}
