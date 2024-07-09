using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000944 RID: 2372
	internal sealed class SymbolMethodImpl : SymbolMethod
	{
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x001BD6C4 File Offset: 0x001BD6C4
		public override int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x001BD6CC File Offset: 0x001BD6CC
		public override SymbolScope RootScope
		{
			get
			{
				return this.rootScope;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x001BD6D4 File Offset: 0x001BD6D4
		public override IList<SymbolSequencePoint> SequencePoints
		{
			get
			{
				return this.sequencePoints;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06005B37 RID: 23351 RVA: 0x001BD6DC File Offset: 0x001BD6DC
		public int KickoffMethod
		{
			get
			{
				return this.kickoffMethod;
			}
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x001BD6E4 File Offset: 0x001BD6E4
		public SymbolMethodImpl(PortablePdbReader reader, int token, SymbolScope rootScope, SymbolSequencePoint[] sequencePoints, int kickoffMethod)
		{
			this.reader = reader;
			this.token = token;
			this.rootScope = rootScope;
			this.sequencePoints = sequencePoints;
			this.kickoffMethod = kickoffMethod;
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x001BD714 File Offset: 0x001BD714
		public override void GetCustomDebugInfos(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			this.reader.GetCustomDebugInfos(this, method, body, result);
		}

		// Token: 0x04002C11 RID: 11281
		private readonly PortablePdbReader reader;

		// Token: 0x04002C12 RID: 11282
		private readonly int token;

		// Token: 0x04002C13 RID: 11283
		private readonly SymbolScope rootScope;

		// Token: 0x04002C14 RID: 11284
		private readonly SymbolSequencePoint[] sequencePoints;

		// Token: 0x04002C15 RID: 11285
		private readonly int kickoffMethod;
	}
}
