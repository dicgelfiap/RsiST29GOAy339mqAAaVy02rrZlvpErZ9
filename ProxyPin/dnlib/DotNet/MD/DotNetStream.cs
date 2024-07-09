using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000989 RID: 2441
	[DebuggerDisplay("{dataReader.Length} {streamHeader.Name}")]
	[ComVisible(true)]
	public abstract class DotNetStream : IFileSection, IDisposable
	{
		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x001C3014 File Offset: 0x001C3014
		public FileOffset StartOffset
		{
			get
			{
				return (FileOffset)this.dataReader.StartOffset;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x001C3024 File Offset: 0x001C3024
		public FileOffset EndOffset
		{
			get
			{
				return (FileOffset)this.dataReader.EndOffset;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x001C3034 File Offset: 0x001C3034
		public uint StreamLength
		{
			get
			{
				return this.dataReader.Length;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x001C3044 File Offset: 0x001C3044
		public StreamHeader StreamHeader
		{
			get
			{
				return this.streamHeader;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x001C304C File Offset: 0x001C304C
		public string Name
		{
			get
			{
				if (this.streamHeader != null)
				{
					return this.streamHeader.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x001C306C File Offset: 0x001C306C
		public DataReader CreateReader()
		{
			return this.dataReader;
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x001C3074 File Offset: 0x001C3074
		protected DotNetStream()
		{
			this.streamHeader = null;
			this.dataReader = default(DataReader);
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x001C3090 File Offset: 0x001C3090
		protected DotNetStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader)
		{
			this.mdReaderFactory = mdReaderFactory;
			mdReaderFactory.DataReaderInvalidated += this.DataReaderFactory_DataReaderInvalidated;
			this.mdReaderFactory = mdReaderFactory;
			this.metadataBaseOffset = metadataBaseOffset;
			this.streamHeader = streamHeader;
			this.RecreateReader(mdReaderFactory, metadataBaseOffset, streamHeader, false);
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x001C30E0 File Offset: 0x001C30E0
		private void DataReaderFactory_DataReaderInvalidated(object sender, EventArgs e)
		{
			this.RecreateReader(this.mdReaderFactory, this.metadataBaseOffset, this.streamHeader, true);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x001C30FC File Offset: 0x001C30FC
		private void RecreateReader(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader, bool notifyThisClass)
		{
			if (mdReaderFactory == null || streamHeader == null)
			{
				this.dataReader = default(DataReader);
			}
			else
			{
				this.dataReader = mdReaderFactory.CreateReader(metadataBaseOffset + streamHeader.Offset, streamHeader.StreamSize);
			}
			if (notifyThisClass)
			{
				this.OnReaderRecreated();
			}
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x001C3154 File Offset: 0x001C3154
		protected virtual void OnReaderRecreated()
		{
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x001C3158 File Offset: 0x001C3158
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x001C3168 File Offset: 0x001C3168
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				DataReaderFactory dataReaderFactory = this.mdReaderFactory;
				if (dataReaderFactory != null)
				{
					dataReaderFactory.DataReaderInvalidated -= this.DataReaderFactory_DataReaderInvalidated;
				}
				this.streamHeader = null;
				this.mdReaderFactory = null;
			}
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x001C31AC File Offset: 0x001C31AC
		public virtual bool IsValidIndex(uint index)
		{
			return this.IsValidOffset(index);
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x001C31B8 File Offset: 0x001C31B8
		public bool IsValidOffset(uint offset)
		{
			return offset == 0U || offset < this.dataReader.Length;
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x001C31D0 File Offset: 0x001C31D0
		public bool IsValidOffset(uint offset, int size)
		{
			if (size == 0)
			{
				return this.IsValidOffset(offset);
			}
			return size > 0 && (ulong)offset + (ulong)size <= (ulong)this.dataReader.Length;
		}

		// Token: 0x04002DE8 RID: 11752
		protected DataReader dataReader;

		// Token: 0x04002DE9 RID: 11753
		private StreamHeader streamHeader;

		// Token: 0x04002DEA RID: 11754
		private DataReaderFactory mdReaderFactory;

		// Token: 0x04002DEB RID: 11755
		private uint metadataBaseOffset;
	}
}
