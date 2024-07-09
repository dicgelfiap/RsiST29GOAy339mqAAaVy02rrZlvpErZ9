using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FF RID: 2303
	[ComVisible(true)]
	public sealed class PdbEditAndContinueLocalSlotMapCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x001B5AE0 File Offset: 0x001B5AE0
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.EditAndContinueLocalSlotMap;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x001B5AE4 File Offset: 0x001B5AE4
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.EncLocalSlotMap;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x001B5AEC File Offset: 0x001B5AEC
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x001B5AF4 File Offset: 0x001B5AF4
		public PdbEditAndContinueLocalSlotMapCustomDebugInfo(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
		}

		// Token: 0x04002B46 RID: 11078
		private readonly byte[] data;
	}
}
