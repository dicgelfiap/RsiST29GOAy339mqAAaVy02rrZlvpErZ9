using System;
using System.Collections.Generic;
using System.IO;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x0200092A RID: 2346
	internal sealed class PdbCustomDebugInfoWriterContext
	{
		// Token: 0x06005A6D RID: 23149 RVA: 0x001B8374 File Offset: 0x001B8374
		public PdbCustomDebugInfoWriterContext()
		{
			this.MemoryStream = new MemoryStream();
			this.Writer = new DataWriter(this.MemoryStream);
			this.InstructionToOffsetDict = new Dictionary<Instruction, uint>();
		}

		// Token: 0x04002BBF RID: 11199
		public ILogger Logger;

		// Token: 0x04002BC0 RID: 11200
		public readonly MemoryStream MemoryStream;

		// Token: 0x04002BC1 RID: 11201
		public readonly DataWriter Writer;

		// Token: 0x04002BC2 RID: 11202
		public readonly Dictionary<Instruction, uint> InstructionToOffsetDict;
	}
}
