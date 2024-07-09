using System;
using System.Diagnostics.SymbolStore;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097A RID: 2426
	internal sealed class SymbolDocumentWriter : ISymbolDocumentWriter
	{
		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x001C1054 File Offset: 0x001C1054
		public ISymUnmanagedDocumentWriter SymUnmanagedDocumentWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x001C105C File Offset: 0x001C105C
		public SymbolDocumentWriter(ISymUnmanagedDocumentWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x001C106C File Offset: 0x001C106C
		public void SetCheckSum(Guid algorithmId, byte[] checkSum)
		{
			if (checkSum != null && checkSum.Length != 0 && algorithmId != Guid.Empty)
			{
				this.writer.SetCheckSum(algorithmId, (uint)checkSum.Length, checkSum);
			}
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x001C109C File Offset: 0x001C109C
		public void SetSource(byte[] source)
		{
			this.writer.SetSource((uint)source.Length, source);
		}

		// Token: 0x04002D5D RID: 11613
		private readonly ISymUnmanagedDocumentWriter writer;
	}
}
