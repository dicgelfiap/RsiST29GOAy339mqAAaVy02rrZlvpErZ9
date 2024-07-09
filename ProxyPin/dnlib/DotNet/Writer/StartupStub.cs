using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DA RID: 2266
	[ComVisible(true)]
	public sealed class StartupStub : IChunk
	{
		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x001B1278 File Offset: 0x001B1278
		// (set) Token: 0x06005829 RID: 22569 RVA: 0x001B1280 File Offset: 0x001B1280
		public ImportDirectory ImportDirectory { get; set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x001B128C File Offset: 0x001B128C
		// (set) Token: 0x0600582B RID: 22571 RVA: 0x001B1294 File Offset: 0x001B1294
		public PEHeaders PEHeaders { get; set; }

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x001B12A0 File Offset: 0x001B12A0
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x001B12A8 File Offset: 0x001B12A8
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x001B12B0 File Offset: 0x001B12B0
		public RVA EntryPointRVA
		{
			get
			{
				return this.rva + ((this.cpuArch == null) ? 0U : this.cpuArch.GetStubCodeOffset(StubType.EntryPoint));
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x0600582F RID: 22575 RVA: 0x001B12D8 File Offset: 0x001B12D8
		// (set) Token: 0x06005830 RID: 22576 RVA: 0x001B12E0 File Offset: 0x001B12E0
		internal bool Enable { get; set; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x001B12EC File Offset: 0x001B12EC
		internal uint Alignment
		{
			get
			{
				if (this.cpuArch != null)
				{
					return this.cpuArch.GetStubAlignment(StubType.EntryPoint);
				}
				return 1U;
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x001B1308 File Offset: 0x001B1308
		internal StartupStub(RelocDirectory relocDirectory, Machine machine, Action<string, object[]> logError)
		{
			this.relocDirectory = relocDirectory;
			this.machine = machine;
			this.logError = logError;
			CpuArch.TryGetCpuArch(machine, out this.cpuArch);
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x001B1334 File Offset: 0x001B1334
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			if (!this.Enable)
			{
				return;
			}
			if (this.cpuArch == null)
			{
				this.logError("The module needs an unmanaged entry point but the CPU architecture isn't supported: {0} (0x{1:X4})", new object[]
				{
					this.machine,
					(ushort)this.machine
				});
				return;
			}
			this.cpuArch.WriteStubRelocs(StubType.EntryPoint, this.relocDirectory, this, 0U);
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x001B13B4 File Offset: 0x001B13B4
		public uint GetFileLength()
		{
			if (!this.Enable)
			{
				return 0U;
			}
			if (this.cpuArch == null)
			{
				return 0U;
			}
			return this.cpuArch.GetStubSize(StubType.EntryPoint);
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x001B13DC File Offset: 0x001B13DC
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x001B13E4 File Offset: 0x001B13E4
		public void WriteTo(DataWriter writer)
		{
			if (!this.Enable)
			{
				return;
			}
			if (this.cpuArch == null)
			{
				return;
			}
			this.cpuArch.WriteStub(StubType.EntryPoint, writer, this.PEHeaders.ImageBase, (uint)this.rva, (uint)this.ImportDirectory.IatCorXxxMainRVA);
		}

		// Token: 0x04002A6B RID: 10859
		private const StubType stubType = StubType.EntryPoint;

		// Token: 0x04002A6C RID: 10860
		private readonly RelocDirectory relocDirectory;

		// Token: 0x04002A6D RID: 10861
		private readonly Machine machine;

		// Token: 0x04002A6E RID: 10862
		private readonly CpuArch cpuArch;

		// Token: 0x04002A6F RID: 10863
		private readonly Action<string, object[]> logError;

		// Token: 0x04002A70 RID: 10864
		private FileOffset offset;

		// Token: 0x04002A71 RID: 10865
		private RVA rva;
	}
}
