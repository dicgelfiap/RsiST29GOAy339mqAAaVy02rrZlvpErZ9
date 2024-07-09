using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;
using dnlib.Threading;
using dnlib.Utils;

namespace dnlib.W32Resources
{
	// Token: 0x02000738 RID: 1848
	[ComVisible(true)]
	public sealed class Win32ResourcesPE : Win32Resources
	{
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00161C80 File Offset: 0x00161C80
		// (set) Token: 0x060040D7 RID: 16599 RVA: 0x00161C90 File Offset: 0x00161C90
		public override ResourceDirectory Root
		{
			get
			{
				return this.root.Value;
			}
			set
			{
				if (this.root.IsValueInitialized && this.root.Value == value)
				{
					return;
				}
				this.root.Value = value;
			}
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00161CC0 File Offset: 0x00161CC0
		internal DataReader GetResourceReader()
		{
			return this.rsrcReader_factory.CreateReader(this.rsrcReader_offset, this.rsrcReader_length);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00161CDC File Offset: 0x00161CDC
		public Win32ResourcesPE(IRvaFileOffsetConverter rvaConverter, DataReaderFactory rsrcReader_factory, uint rsrcReader_offset, uint rsrcReader_length, bool owns_rsrcReader_factory, DataReaderFactory dataReader_factory, uint dataReader_offset, uint dataReader_length, bool owns_dataReader_factory)
		{
			if (rvaConverter == null)
			{
				throw new ArgumentNullException("rvaConverter");
			}
			this.rvaConverter = rvaConverter;
			if (rsrcReader_factory == null)
			{
				throw new ArgumentNullException("rsrcReader_factory");
			}
			this.rsrcReader_factory = rsrcReader_factory;
			this.rsrcReader_offset = rsrcReader_offset;
			this.rsrcReader_length = rsrcReader_length;
			this.owns_rsrcReader_factory = owns_rsrcReader_factory;
			if (dataReader_factory == null)
			{
				throw new ArgumentNullException("dataReader_factory");
			}
			this.dataReader_factory = dataReader_factory;
			this.dataReader_offset = dataReader_offset;
			this.dataReader_length = dataReader_length;
			this.owns_dataReader_factory = owns_dataReader_factory;
			this.Initialize();
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x00161D80 File Offset: 0x00161D80
		public Win32ResourcesPE(IPEImage peImage) : this(peImage, null, 0U, 0U, false)
		{
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00161D90 File Offset: 0x00161D90
		public Win32ResourcesPE(IPEImage peImage, DataReaderFactory rsrcReader_factory, uint rsrcReader_offset, uint rsrcReader_length, bool owns_rsrcReader_factory)
		{
			if (peImage == null)
			{
				throw new ArgumentNullException("peImage");
			}
			this.rvaConverter = peImage;
			this.dataReader_factory = peImage.DataReaderFactory;
			this.dataReader_offset = 0U;
			this.dataReader_length = this.dataReader_factory.Length;
			if (rsrcReader_factory != null)
			{
				this.rsrcReader_factory = rsrcReader_factory;
				this.rsrcReader_offset = rsrcReader_offset;
				this.rsrcReader_length = rsrcReader_length;
				this.owns_rsrcReader_factory = owns_rsrcReader_factory;
			}
			else
			{
				ImageDataDirectory imageDataDirectory = peImage.ImageNTHeaders.OptionalHeader.DataDirectories[2];
				if (imageDataDirectory.VirtualAddress != (RVA)0U && imageDataDirectory.Size != 0U)
				{
					DataReader dataReader = peImage.CreateReader(imageDataDirectory.VirtualAddress, imageDataDirectory.Size);
					this.rsrcReader_factory = peImage.DataReaderFactory;
					this.rsrcReader_offset = dataReader.StartOffset;
					this.rsrcReader_length = dataReader.Length;
				}
				else
				{
					this.rsrcReader_factory = ByteArrayDataReaderFactory.Create(Array2.Empty<byte>(), null);
					this.rsrcReader_offset = 0U;
					this.rsrcReader_length = 0U;
				}
			}
			this.Initialize();
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00161EA8 File Offset: 0x00161EA8
		private void Initialize()
		{
			this.root.ReadOriginalValue = delegate()
			{
				DataReaderFactory dataReaderFactory = this.rsrcReader_factory;
				if (dataReaderFactory == null)
				{
					return null;
				}
				DataReader dataReader = dataReaderFactory.CreateReader(this.rsrcReader_offset, this.rsrcReader_length);
				return new ResourceDirectoryPE(0U, new ResourceName("root"), this, ref dataReader);
			};
			this.root.Lock = this.theLock;
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x00161ED4 File Offset: 0x00161ED4
		public DataReader CreateReader(RVA rva, uint size)
		{
			DataReaderFactory dataReaderFactory;
			uint offset;
			uint length;
			this.GetDataReaderInfo(rva, size, out dataReaderFactory, out offset, out length);
			return dataReaderFactory.CreateReader(offset, length);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00161EFC File Offset: 0x00161EFC
		internal void GetDataReaderInfo(RVA rva, uint size, out DataReaderFactory dataReaderFactory, out uint dataOffset, out uint dataLength)
		{
			dataOffset = (uint)this.rvaConverter.ToFileOffset(rva);
			if ((ulong)dataOffset + (ulong)size <= (ulong)this.dataReader_factory.Length)
			{
				dataReaderFactory = this.dataReader_factory;
				dataLength = size;
				return;
			}
			dataReaderFactory = ByteArrayDataReaderFactory.Create(Array2.Empty<byte>(), null);
			dataOffset = 0U;
			dataLength = 0U;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00161F58 File Offset: 0x00161F58
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.owns_dataReader_factory)
			{
				DataReaderFactory dataReaderFactory = this.dataReader_factory;
				if (dataReaderFactory != null)
				{
					dataReaderFactory.Dispose();
				}
			}
			if (this.owns_rsrcReader_factory)
			{
				DataReaderFactory dataReaderFactory2 = this.rsrcReader_factory;
				if (dataReaderFactory2 != null)
				{
					dataReaderFactory2.Dispose();
				}
			}
			this.dataReader_factory = null;
			this.rsrcReader_factory = null;
			base.Dispose(disposing);
		}

		// Token: 0x0400227D RID: 8829
		private readonly IRvaFileOffsetConverter rvaConverter;

		// Token: 0x0400227E RID: 8830
		private DataReaderFactory dataReader_factory;

		// Token: 0x0400227F RID: 8831
		private uint dataReader_offset;

		// Token: 0x04002280 RID: 8832
		private uint dataReader_length;

		// Token: 0x04002281 RID: 8833
		private bool owns_dataReader_factory;

		// Token: 0x04002282 RID: 8834
		private DataReaderFactory rsrcReader_factory;

		// Token: 0x04002283 RID: 8835
		private uint rsrcReader_offset;

		// Token: 0x04002284 RID: 8836
		private uint rsrcReader_length;

		// Token: 0x04002285 RID: 8837
		private bool owns_rsrcReader_factory;

		// Token: 0x04002286 RID: 8838
		private UserValue<ResourceDirectory> root;

		// Token: 0x04002287 RID: 8839
		private readonly Lock theLock = Lock.Create();
	}
}
