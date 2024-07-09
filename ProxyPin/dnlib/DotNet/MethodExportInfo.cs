using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000810 RID: 2064
	[DebuggerDisplay("{Ordinal} {Name} {Options}")]
	[ComVisible(true)]
	public sealed class MethodExportInfo
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x0017DE78 File Offset: 0x0017DE78
		// (set) Token: 0x06004B64 RID: 19300 RVA: 0x0017DE80 File Offset: 0x0017DE80
		public ushort? Ordinal
		{
			get
			{
				return this.ordinal;
			}
			set
			{
				this.ordinal = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x0017DE8C File Offset: 0x0017DE8C
		// (set) Token: 0x06004B66 RID: 19302 RVA: 0x0017DE94 File Offset: 0x0017DE94
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x0017DEA0 File Offset: 0x0017DEA0
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x0017DEA8 File Offset: 0x0017DEA8
		public MethodExportInfoOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0017DEB4 File Offset: 0x0017DEB4
		public MethodExportInfo()
		{
			this.options = MethodExportInfoOptions.FromUnmanaged;
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0017DEC4 File Offset: 0x0017DEC4
		public MethodExportInfo(string name)
		{
			this.options = MethodExportInfoOptions.FromUnmanaged;
			this.name = name;
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0017DEDC File Offset: 0x0017DEDC
		public MethodExportInfo(ushort ordinal)
		{
			this.options = MethodExportInfoOptions.FromUnmanaged;
			this.ordinal = new ushort?(ordinal);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0017DEF8 File Offset: 0x0017DEF8
		public MethodExportInfo(string name, ushort? ordinal)
		{
			this.options = MethodExportInfoOptions.FromUnmanaged;
			this.name = name;
			this.ordinal = ordinal;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0017DF18 File Offset: 0x0017DF18
		public MethodExportInfo(string name, ushort? ordinal, MethodExportInfoOptions options)
		{
			this.options = options;
			this.name = name;
			this.ordinal = ordinal;
		}

		// Token: 0x040025A6 RID: 9638
		private MethodExportInfoOptions options;

		// Token: 0x040025A7 RID: 9639
		private ushort? ordinal;

		// Token: 0x040025A8 RID: 9640
		private string name;

		// Token: 0x040025A9 RID: 9641
		private const MethodExportInfoOptions DefaultOptions = MethodExportInfoOptions.FromUnmanaged;
	}
}
