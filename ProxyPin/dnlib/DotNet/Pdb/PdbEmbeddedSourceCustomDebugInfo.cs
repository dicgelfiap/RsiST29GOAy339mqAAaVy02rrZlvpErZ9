using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000907 RID: 2311
	[ComVisible(true)]
	public sealed class PdbEmbeddedSourceCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x001B5D4C File Offset: 0x001B5D4C
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.EmbeddedSource;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06005976 RID: 22902 RVA: 0x001B5D54 File Offset: 0x001B5D54
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.EmbeddedSource;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x001B5D5C File Offset: 0x001B5D5C
		// (set) Token: 0x06005978 RID: 22904 RVA: 0x001B5D64 File Offset: 0x001B5D64
		public byte[] SourceCodeBlob { get; set; }

		// Token: 0x06005979 RID: 22905 RVA: 0x001B5D70 File Offset: 0x001B5D70
		public PdbEmbeddedSourceCustomDebugInfo()
		{
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x001B5D78 File Offset: 0x001B5D78
		public PdbEmbeddedSourceCustomDebugInfo(byte[] sourceCodeBlob)
		{
			this.SourceCodeBlob = sourceCodeBlob;
		}
	}
}
