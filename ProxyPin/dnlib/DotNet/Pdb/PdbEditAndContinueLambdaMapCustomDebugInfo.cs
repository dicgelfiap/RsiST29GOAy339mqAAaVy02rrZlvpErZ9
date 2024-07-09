using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000900 RID: 2304
	[ComVisible(true)]
	public sealed class PdbEditAndContinueLambdaMapCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x001B5B18 File Offset: 0x001B5B18
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.EditAndContinueLambdaMap;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x001B5B1C File Offset: 0x001B5B1C
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.EncLambdaAndClosureMap;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x001B5B24 File Offset: 0x001B5B24
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x001B5B2C File Offset: 0x001B5B2C
		public PdbEditAndContinueLambdaMapCustomDebugInfo(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
		}

		// Token: 0x04002B47 RID: 11079
		private readonly byte[] data;
	}
}
