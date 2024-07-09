using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B0 RID: 2224
	[ComVisible(true)]
	public sealed class MetadataOptions
	{
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x0019F3B8 File Offset: 0x0019F3B8
		// (set) Token: 0x0600552C RID: 21804 RVA: 0x0019F3E4 File Offset: 0x0019F3E4
		public MetadataHeaderOptions MetadataHeaderOptions
		{
			get
			{
				MetadataHeaderOptions result;
				if ((result = this.metadataHeaderOptions) == null)
				{
					result = (this.metadataHeaderOptions = new MetadataHeaderOptions());
				}
				return result;
			}
			set
			{
				this.metadataHeaderOptions = value;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x0019F3F0 File Offset: 0x0019F3F0
		// (set) Token: 0x0600552E RID: 21806 RVA: 0x0019F41C File Offset: 0x0019F41C
		public MetadataHeaderOptions DebugMetadataHeaderOptions
		{
			get
			{
				MetadataHeaderOptions result;
				if ((result = this.debugMetadataHeaderOptions) == null)
				{
					result = (this.debugMetadataHeaderOptions = MetadataHeaderOptions.CreatePortablePdbV1_0());
				}
				return result;
			}
			set
			{
				this.debugMetadataHeaderOptions = value;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x0019F428 File Offset: 0x0019F428
		// (set) Token: 0x06005530 RID: 21808 RVA: 0x0019F454 File Offset: 0x0019F454
		public TablesHeapOptions TablesHeapOptions
		{
			get
			{
				TablesHeapOptions result;
				if ((result = this.tablesHeapOptions) == null)
				{
					result = (this.tablesHeapOptions = new TablesHeapOptions());
				}
				return result;
			}
			set
			{
				this.tablesHeapOptions = value;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x0019F460 File Offset: 0x0019F460
		// (set) Token: 0x06005532 RID: 21810 RVA: 0x0019F48C File Offset: 0x0019F48C
		public TablesHeapOptions DebugTablesHeapOptions
		{
			get
			{
				TablesHeapOptions result;
				if ((result = this.tablesHeapOptions) == null)
				{
					result = (this.tablesHeapOptions = TablesHeapOptions.CreatePortablePdbV1_0());
				}
				return result;
			}
			set
			{
				this.tablesHeapOptions = value;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x0019F498 File Offset: 0x0019F498
		public List<IHeap> CustomHeaps
		{
			get
			{
				List<IHeap> result;
				if ((result = this.customHeaps) == null)
				{
					result = (this.customHeaps = new List<IHeap>());
				}
				return result;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06005534 RID: 21812 RVA: 0x0019F4C4 File Offset: 0x0019F4C4
		// (remove) Token: 0x06005535 RID: 21813 RVA: 0x0019F500 File Offset: 0x0019F500
		public event EventHandler2<MetadataHeapsAddedEventArgs> MetadataHeapsAdded;

		// Token: 0x06005536 RID: 21814 RVA: 0x0019F53C File Offset: 0x0019F53C
		internal void RaiseMetadataHeapsAdded(MetadataHeapsAddedEventArgs e)
		{
			EventHandler2<MetadataHeapsAddedEventArgs> metadataHeapsAdded = this.MetadataHeapsAdded;
			if (metadataHeapsAdded == null)
			{
				return;
			}
			metadataHeapsAdded(e.Metadata, e);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0019F55C File Offset: 0x0019F55C
		public void PreserveHeapOrder(ModuleDef module, bool addCustomHeaps)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			ModuleDefMD moduleDefMD = module as ModuleDefMD;
			if (moduleDefMD != null)
			{
				if (addCustomHeaps)
				{
					IEnumerable<DataReaderHeap> source = from a in moduleDefMD.Metadata.AllStreams
					where a.GetType() == typeof(CustomDotNetStream)
					select new DataReaderHeap(a);
					this.CustomHeaps.AddRange(source.OfType<IHeap>());
				}
				Dictionary<DotNetStream, int> streamToOrder = new Dictionary<DotNetStream, int>(moduleDefMD.Metadata.AllStreams.Count);
				int i = 0;
				int num = 0;
				while (i < moduleDefMD.Metadata.AllStreams.Count)
				{
					DotNetStream dotNetStream = moduleDefMD.Metadata.AllStreams[i];
					if (dotNetStream.StartOffset != (FileOffset)0U)
					{
						streamToOrder.Add(dotNetStream, num++);
					}
					i++;
				}
				Dictionary<string, int> nameToOrder = new Dictionary<string, int>(moduleDefMD.Metadata.AllStreams.Count, StringComparer.Ordinal);
				int j = 0;
				int num2 = 0;
				while (j < moduleDefMD.Metadata.AllStreams.Count)
				{
					DotNetStream dotNetStream2 = moduleDefMD.Metadata.AllStreams[j];
					if (dotNetStream2.StartOffset != (FileOffset)0U)
					{
						bool flag = dotNetStream2 is BlobStream || dotNetStream2 is GuidStream || dotNetStream2 is PdbStream || dotNetStream2 is StringsStream || dotNetStream2 is TablesStream || dotNetStream2 is USStream;
						if (!nameToOrder.ContainsKey(dotNetStream2.Name) || flag)
						{
							nameToOrder[dotNetStream2.Name] = num2;
						}
						num2++;
					}
					j++;
				}
				Comparison<IHeap> <>9__3;
				this.MetadataHeapsAdded += delegate(object s, MetadataHeapsAddedEventArgs e)
				{
					List<IHeap> heaps = e.Heaps;
					Comparison<IHeap> comparison;
					if ((comparison = <>9__3) == null)
					{
						comparison = (<>9__3 = delegate(IHeap a, IHeap b)
						{
							int order = MetadataOptions.GetOrder(streamToOrder, nameToOrder, a);
							int order2 = MetadataOptions.GetOrder(streamToOrder, nameToOrder, b);
							int num3 = order - order2;
							if (num3 != 0)
							{
								return num3;
							}
							return StringComparer.Ordinal.Compare(a.Name, b.Name);
						});
					}
					heaps.Sort(comparison);
				};
			}
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0019F770 File Offset: 0x0019F770
		private static int GetOrder(Dictionary<DotNetStream, int> streamToOrder, Dictionary<string, int> nameToOrder, IHeap heap)
		{
			DataReaderHeap dataReaderHeap = heap as DataReaderHeap;
			int result;
			if (dataReaderHeap != null)
			{
				DotNetStream optionalOriginalStream = dataReaderHeap.OptionalOriginalStream;
				if (optionalOriginalStream != null && streamToOrder.TryGetValue(optionalOriginalStream, out result))
				{
					return result;
				}
			}
			if (nameToOrder.TryGetValue(heap.Name, out result))
			{
				return result;
			}
			return int.MaxValue;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0019F7C8 File Offset: 0x0019F7C8
		public MetadataOptions()
		{
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0019F7D0 File Offset: 0x0019F7D0
		public MetadataOptions(MetadataFlags flags)
		{
			this.Flags = flags;
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0019F7E0 File Offset: 0x0019F7E0
		public MetadataOptions(MetadataHeaderOptions mdhOptions)
		{
			this.metadataHeaderOptions = mdhOptions;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0019F7F0 File Offset: 0x0019F7F0
		public MetadataOptions(MetadataHeaderOptions mdhOptions, MetadataFlags flags)
		{
			this.Flags = flags;
			this.metadataHeaderOptions = mdhOptions;
		}

		// Token: 0x040028E5 RID: 10469
		private MetadataHeaderOptions metadataHeaderOptions;

		// Token: 0x040028E6 RID: 10470
		private MetadataHeaderOptions debugMetadataHeaderOptions;

		// Token: 0x040028E7 RID: 10471
		private TablesHeapOptions tablesHeapOptions;

		// Token: 0x040028E8 RID: 10472
		private List<IHeap> customHeaps;

		// Token: 0x040028E9 RID: 10473
		public MetadataFlags Flags;
	}
}
