using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F6 RID: 2294
	[ComVisible(true)]
	public sealed class PdbUnknownCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x001B583C File Offset: 0x001B583C
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x001B5844 File Offset: 0x001B5844
		public override Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x001B584C File Offset: 0x001B584C
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x001B5854 File Offset: 0x001B5854
		public PdbUnknownCustomDebugInfo(PdbCustomDebugInfoKind kind, byte[] data)
		{
			this.kind = kind;
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
			this.guid = Guid.Empty;
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x001B5888 File Offset: 0x001B5888
		public PdbUnknownCustomDebugInfo(Guid guid, byte[] data)
		{
			this.kind = PdbCustomDebugInfoKind.Unknown;
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
			this.guid = guid;
		}

		// Token: 0x04002B38 RID: 11064
		private readonly PdbCustomDebugInfoKind kind;

		// Token: 0x04002B39 RID: 11065
		private readonly Guid guid;

		// Token: 0x04002B3A RID: 11066
		private readonly byte[] data;
	}
}
