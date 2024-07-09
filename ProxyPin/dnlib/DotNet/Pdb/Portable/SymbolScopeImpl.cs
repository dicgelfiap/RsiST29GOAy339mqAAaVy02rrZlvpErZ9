using System;
using System.Collections.Generic;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000946 RID: 2374
	internal sealed class SymbolScopeImpl : SymbolScope
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x001BD98C File Offset: 0x001BD98C
		public override SymbolMethod Method
		{
			get
			{
				if (this.method != null)
				{
					return this.method;
				}
				SymbolScopeImpl symbolScopeImpl = this.parent;
				if (symbolScopeImpl == null)
				{
					return this.method;
				}
				while (symbolScopeImpl.parent != null)
				{
					symbolScopeImpl = symbolScopeImpl.parent;
				}
				return this.method = symbolScopeImpl.method;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06005B3D RID: 23357 RVA: 0x001BD9E8 File Offset: 0x001BD9E8
		public override SymbolScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x001BD9F0 File Offset: 0x001BD9F0
		public override int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x001BD9F8 File Offset: 0x001BD9F8
		public override int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x001BDA00 File Offset: 0x001BDA00
		public override IList<SymbolScope> Children
		{
			get
			{
				return this.childrenList;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x001BDA08 File Offset: 0x001BDA08
		public override IList<SymbolVariable> Locals
		{
			get
			{
				return this.localsList;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x001BDA10 File Offset: 0x001BDA10
		public override IList<SymbolNamespace> Namespaces
		{
			get
			{
				return Array2.Empty<SymbolNamespace>();
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x001BDA18 File Offset: 0x001BDA18
		public override IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x001BDA20 File Offset: 0x001BDA20
		public override PdbImportScope ImportScope
		{
			get
			{
				return this.importScope;
			}
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x001BDA28 File Offset: 0x001BDA28
		public SymbolScopeImpl(PortablePdbReader owner, SymbolScopeImpl parent, int startOffset, int endOffset, PdbCustomDebugInfo[] customDebugInfos)
		{
			this.owner = owner;
			this.method = null;
			this.parent = parent;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
			this.childrenList = new List<SymbolScope>();
			this.localsList = new List<SymbolVariable>();
			this.customDebugInfos = customDebugInfos;
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x001BDA84 File Offset: 0x001BDA84
		internal void SetConstants(Metadata metadata, uint constantList, uint constantListEnd)
		{
			this.constantsMetadata = metadata;
			this.constantList = constantList;
			this.constantListEnd = constantListEnd;
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x001BDA9C File Offset: 0x001BDA9C
		public override IList<PdbConstant> GetConstants(ModuleDef module, GenericParamContext gpContext)
		{
			if (this.constantList >= this.constantListEnd)
			{
				return Array2.Empty<PdbConstant>();
			}
			PdbConstant[] array = new PdbConstant[this.constantListEnd - this.constantList];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				uint rid = this.constantList + (uint)i;
				RawLocalConstantRow rawLocalConstantRow;
				this.constantsMetadata.TablesStream.TryReadLocalConstantRow(rid, out rawLocalConstantRow);
				UTF8String s = this.constantsMetadata.StringsStream.Read(rawLocalConstantRow.Name);
				DataReader dataReader;
				TypeSig type;
				object value;
				if (this.constantsMetadata.BlobStream.TryCreateReader(rawLocalConstantRow.Signature, out dataReader) && new LocalConstantSigBlobReader(module, ref dataReader, gpContext).Read(out type, out value))
				{
					PdbConstant pdbConstant = new PdbConstant(s, type, value);
					int token = new MDToken(Table.LocalConstant, rid).ToInt32();
					this.owner.GetCustomDebugInfos(token, gpContext, pdbConstant.CustomDebugInfos);
					array[num++] = pdbConstant;
				}
			}
			if (array.Length != num)
			{
				Array.Resize<PdbConstant>(ref array, num);
			}
			return array;
		}

		// Token: 0x04002C16 RID: 11286
		private readonly PortablePdbReader owner;

		// Token: 0x04002C17 RID: 11287
		internal SymbolMethod method;

		// Token: 0x04002C18 RID: 11288
		private readonly SymbolScopeImpl parent;

		// Token: 0x04002C19 RID: 11289
		private readonly int startOffset;

		// Token: 0x04002C1A RID: 11290
		private readonly int endOffset;

		// Token: 0x04002C1B RID: 11291
		internal readonly List<SymbolScope> childrenList;

		// Token: 0x04002C1C RID: 11292
		internal readonly List<SymbolVariable> localsList;

		// Token: 0x04002C1D RID: 11293
		internal PdbImportScope importScope;

		// Token: 0x04002C1E RID: 11294
		private readonly PdbCustomDebugInfo[] customDebugInfos;

		// Token: 0x04002C1F RID: 11295
		private Metadata constantsMetadata;

		// Token: 0x04002C20 RID: 11296
		private uint constantList;

		// Token: 0x04002C21 RID: 11297
		private uint constantListEnd;
	}
}
