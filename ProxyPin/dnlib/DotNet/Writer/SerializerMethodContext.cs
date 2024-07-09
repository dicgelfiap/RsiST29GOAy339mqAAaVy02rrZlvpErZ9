using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D7 RID: 2263
	internal sealed class SerializerMethodContext
	{
		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06005810 RID: 22544 RVA: 0x001B06AC File Offset: 0x001B06AC
		public bool HasBody
		{
			get
			{
				return this.body != null;
			}
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x001B06BC File Offset: 0x001B06BC
		public SerializerMethodContext(IWriterError helper)
		{
			this.toOffset = new Dictionary<Instruction, uint>();
			this.helper = helper;
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x001B06D8 File Offset: 0x001B06D8
		internal void SetBody(MethodDef method)
		{
			if (this.method != method)
			{
				this.toOffset.Clear();
				this.method = method;
				this.body = ((method != null) ? method.Body : null);
				this.dictInitd = false;
			}
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001B0718 File Offset: 0x001B0718
		public uint GetOffset(Instruction instr)
		{
			if (!this.dictInitd)
			{
				if (this.body == null)
				{
					return 0U;
				}
				this.InitializeDict();
			}
			if (instr == null)
			{
				return this.bodySize;
			}
			uint result;
			if (this.toOffset.TryGetValue(instr, out result))
			{
				return result;
			}
			this.helper.Error("Couldn't find an instruction, maybe it was removed. It's still being referenced by some code or by the PDB");
			return this.bodySize;
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x001B0780 File Offset: 0x001B0780
		public bool IsSameMethod(MethodDef method)
		{
			return this.method == method;
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x001B078C File Offset: 0x001B078C
		private void InitializeDict()
		{
			uint num = 0U;
			IList<Instruction> instructions = this.body.Instructions;
			for (int i = 0; i < instructions.Count; i++)
			{
				Instruction instruction = instructions[i];
				this.toOffset[instruction] = num;
				num += (uint)instruction.GetSize();
			}
			this.bodySize = num;
			this.dictInitd = true;
		}

		// Token: 0x04002A60 RID: 10848
		private readonly Dictionary<Instruction, uint> toOffset;

		// Token: 0x04002A61 RID: 10849
		private readonly IWriterError helper;

		// Token: 0x04002A62 RID: 10850
		private MethodDef method;

		// Token: 0x04002A63 RID: 10851
		private CilBody body;

		// Token: 0x04002A64 RID: 10852
		private uint bodySize;

		// Token: 0x04002A65 RID: 10853
		private bool dictInitd;
	}
}
