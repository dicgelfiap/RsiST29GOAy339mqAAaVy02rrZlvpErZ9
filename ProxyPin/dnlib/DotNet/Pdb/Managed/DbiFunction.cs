using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000949 RID: 2377
	internal sealed class DbiFunction : SymbolMethod
	{
		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x001BDD20 File Offset: 0x001BDD20
		public override int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x001BDD28 File Offset: 0x001BDD28
		// (set) Token: 0x06005B59 RID: 23385 RVA: 0x001BDD30 File Offset: 0x001BDD30
		public string Name { get; private set; }

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x001BDD3C File Offset: 0x001BDD3C
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x001BDD44 File Offset: 0x001BDD44
		public PdbAddress Address { get; private set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x001BDD50 File Offset: 0x001BDD50
		// (set) Token: 0x06005B5D RID: 23389 RVA: 0x001BDD58 File Offset: 0x001BDD58
		public DbiScope Root { get; private set; }

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x001BDD64 File Offset: 0x001BDD64
		// (set) Token: 0x06005B5F RID: 23391 RVA: 0x001BDD6C File Offset: 0x001BDD6C
		public List<SymbolSequencePoint> Lines
		{
			get
			{
				return this.lines;
			}
			set
			{
				this.lines = value;
			}
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x001BDD78 File Offset: 0x001BDD78
		public void Read(ref DataReader reader, uint recEnd)
		{
			reader.Position += 4U;
			uint scopeEnd = reader.ReadUInt32();
			reader.Position += 4U;
			uint length = reader.ReadUInt32();
			reader.Position += 8U;
			this.token = reader.ReadInt32();
			this.Address = PdbAddress.ReadAddress(ref reader);
			reader.Position += 3U;
			this.Name = PdbReader.ReadCString(ref reader);
			reader.Position = recEnd;
			this.Root = new DbiScope(this, null, "", this.Address.Offset, length);
			this.Root.Read(default(RecursionCounter), ref reader, scopeEnd);
			this.FixOffsets(default(RecursionCounter), this.Root);
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x001BDE44 File Offset: 0x001BDE44
		private void FixOffsets(RecursionCounter counter, DbiScope scope)
		{
			if (!counter.Increment())
			{
				return;
			}
			scope.startOffset -= (int)this.Address.Offset;
			scope.endOffset -= (int)this.Address.Offset;
			IList<SymbolScope> children = scope.Children;
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				this.FixOffsets(counter, (DbiScope)children[i]);
			}
			counter.Decrement();
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x001BDECC File Offset: 0x001BDECC
		public override SymbolScope RootScope
		{
			get
			{
				return this.Root;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x001BDED4 File Offset: 0x001BDED4
		public override IList<SymbolSequencePoint> SequencePoints
		{
			get
			{
				List<SymbolSequencePoint> list = this.lines;
				if (list == null)
				{
					return Array2.Empty<SymbolSequencePoint>();
				}
				return list;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06005B64 RID: 23396 RVA: 0x001BDEFC File Offset: 0x001BDEFC
		public int AsyncKickoffMethod
		{
			get
			{
				byte[] symAttribute = this.Root.GetSymAttribute("asyncMethodInfo");
				if (symAttribute == null || symAttribute.Length < 4)
				{
					return 0;
				}
				return BitConverter.ToInt32(symAttribute, 0);
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x001BDF38 File Offset: 0x001BDF38
		public uint? AsyncCatchHandlerILOffset
		{
			get
			{
				byte[] symAttribute = this.Root.GetSymAttribute("asyncMethodInfo");
				if (symAttribute == null || symAttribute.Length < 8)
				{
					return null;
				}
				uint num = BitConverter.ToUInt32(symAttribute, 4);
				if (num != 4294967295U)
				{
					return new uint?(num);
				}
				return null;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x001BDF94 File Offset: 0x001BDF94
		public IList<SymbolAsyncStepInfo> AsyncStepInfos
		{
			get
			{
				if (this.asyncStepInfos == null)
				{
					this.asyncStepInfos = this.CreateSymbolAsyncStepInfos();
				}
				return this.asyncStepInfos;
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x001BDFBC File Offset: 0x001BDFBC
		private SymbolAsyncStepInfo[] CreateSymbolAsyncStepInfos()
		{
			byte[] symAttribute = this.Root.GetSymAttribute("asyncMethodInfo");
			if (symAttribute == null || symAttribute.Length < 12)
			{
				return Array2.Empty<SymbolAsyncStepInfo>();
			}
			int num = 8;
			int num2 = BitConverter.ToInt32(symAttribute, num);
			num += 4;
			if ((long)num + (long)num2 * 12L > (long)symAttribute.Length)
			{
				return Array2.Empty<SymbolAsyncStepInfo>();
			}
			if (num2 == 0)
			{
				return Array2.Empty<SymbolAsyncStepInfo>();
			}
			SymbolAsyncStepInfo[] array = new SymbolAsyncStepInfo[num2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SymbolAsyncStepInfo(BitConverter.ToUInt32(symAttribute, num), BitConverter.ToUInt32(symAttribute, num + 8), BitConverter.ToUInt32(symAttribute, num + 4));
				num += 12;
			}
			return array;
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x001BE070 File Offset: 0x001BE070
		public override void GetCustomDebugInfos(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			this.reader.GetCustomDebugInfos(this, method, body, result);
		}

		// Token: 0x04002C2E RID: 11310
		internal int token;

		// Token: 0x04002C2F RID: 11311
		internal PdbReader reader;

		// Token: 0x04002C33 RID: 11315
		private List<SymbolSequencePoint> lines;

		// Token: 0x04002C34 RID: 11316
		private const string asyncMethodInfoAttributeName = "asyncMethodInfo";

		// Token: 0x04002C35 RID: 11317
		private volatile SymbolAsyncStepInfo[] asyncStepInfos;
	}
}
