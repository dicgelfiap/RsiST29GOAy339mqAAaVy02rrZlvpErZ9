using System;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094D RID: 2381
	internal sealed class DbiVariable : SymbolVariable
	{
		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06005B8F RID: 23439 RVA: 0x001BEB7C File Offset: 0x001BEB7C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06005B90 RID: 23440 RVA: 0x001BEB84 File Offset: 0x001BEB84
		public override PdbLocalAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06005B91 RID: 23441 RVA: 0x001BEB8C File Offset: 0x001BEB8C
		public override int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06005B92 RID: 23442 RVA: 0x001BEB94 File Offset: 0x001BEB94
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				return Array2.Empty<PdbCustomDebugInfo>();
			}
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x001BEB9C File Offset: 0x001BEB9C
		public bool Read(ref DataReader reader)
		{
			this.index = reader.ReadInt32();
			reader.Position += 10U;
			ushort num = reader.ReadUInt16();
			this.attributes = DbiVariable.GetAttributes((uint)num);
			this.name = PdbReader.ReadCString(ref reader);
			return (num & 1) == 0;
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x001BEBF0 File Offset: 0x001BEBF0
		private static PdbLocalAttributes GetAttributes(uint flags)
		{
			PdbLocalAttributes pdbLocalAttributes = PdbLocalAttributes.None;
			if ((flags & 4U) != 0U)
			{
				pdbLocalAttributes |= PdbLocalAttributes.DebuggerHidden;
			}
			return pdbLocalAttributes;
		}

		// Token: 0x04002C4A RID: 11338
		private string name;

		// Token: 0x04002C4B RID: 11339
		private PdbLocalAttributes attributes;

		// Token: 0x04002C4C RID: 11340
		private int index;
	}
}
