using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E6 RID: 2534
	[ComVisible(true)]
	public sealed class CilBody : MethodBody
	{
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x001CECAC File Offset: 0x001CECAC
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x001CECB4 File Offset: 0x001CECB4
		public bool KeepOldMaxStack
		{
			get
			{
				return this.keepOldMaxStack;
			}
			set
			{
				this.keepOldMaxStack = value;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x001CECC0 File Offset: 0x001CECC0
		// (set) Token: 0x06006125 RID: 24869 RVA: 0x001CECC8 File Offset: 0x001CECC8
		public bool InitLocals
		{
			get
			{
				return this.initLocals;
			}
			set
			{
				this.initLocals = value;
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x001CECD4 File Offset: 0x001CECD4
		// (set) Token: 0x06006127 RID: 24871 RVA: 0x001CECDC File Offset: 0x001CECDC
		public byte HeaderSize
		{
			get
			{
				return this.headerSize;
			}
			set
			{
				this.headerSize = value;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06006128 RID: 24872 RVA: 0x001CECE8 File Offset: 0x001CECE8
		public bool IsSmallHeader
		{
			get
			{
				return this.headerSize == 1;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x001CECF4 File Offset: 0x001CECF4
		public bool IsBigHeader
		{
			get
			{
				return this.headerSize != 1;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x001CED04 File Offset: 0x001CED04
		// (set) Token: 0x0600612B RID: 24875 RVA: 0x001CED0C File Offset: 0x001CED0C
		public ushort MaxStack
		{
			get
			{
				return this.maxStack;
			}
			set
			{
				this.maxStack = value;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x001CED18 File Offset: 0x001CED18
		// (set) Token: 0x0600612D RID: 24877 RVA: 0x001CED20 File Offset: 0x001CED20
		public uint LocalVarSigTok
		{
			get
			{
				return this.localVarSigTok;
			}
			set
			{
				this.localVarSigTok = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x0600612E RID: 24878 RVA: 0x001CED2C File Offset: 0x001CED2C
		public bool HasInstructions
		{
			get
			{
				return this.instructions.Count > 0;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x001CED3C File Offset: 0x001CED3C
		public IList<Instruction> Instructions
		{
			get
			{
				return this.instructions;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x001CED44 File Offset: 0x001CED44
		public bool HasExceptionHandlers
		{
			get
			{
				return this.exceptionHandlers.Count > 0;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x001CED54 File Offset: 0x001CED54
		public IList<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				return this.exceptionHandlers;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06006132 RID: 24882 RVA: 0x001CED5C File Offset: 0x001CED5C
		public bool HasVariables
		{
			get
			{
				return this.localList.Count > 0;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x001CED6C File Offset: 0x001CED6C
		public LocalList Variables
		{
			get
			{
				return this.localList;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x001CED74 File Offset: 0x001CED74
		// (set) Token: 0x06006135 RID: 24885 RVA: 0x001CED7C File Offset: 0x001CED7C
		public PdbMethod PdbMethod
		{
			get
			{
				return this.pdbMethod;
			}
			set
			{
				this.pdbMethod = value;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x001CED88 File Offset: 0x001CED88
		public bool HasPdbMethod
		{
			get
			{
				return this.PdbMethod != null;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x001CED98 File Offset: 0x001CED98
		// (set) Token: 0x06006138 RID: 24888 RVA: 0x001CEDA0 File Offset: 0x001CEDA0
		internal uint MetadataBodySize { get; set; }

		// Token: 0x06006139 RID: 24889 RVA: 0x001CEDAC File Offset: 0x001CEDAC
		public CilBody()
		{
			this.initLocals = true;
			this.instructions = new List<Instruction>();
			this.exceptionHandlers = new List<ExceptionHandler>();
			this.localList = new LocalList();
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x001CEDDC File Offset: 0x001CEDDC
		public CilBody(bool initLocals, IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers, IList<Local> locals)
		{
			this.initLocals = initLocals;
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.localList = new LocalList(locals);
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x001CEE08 File Offset: 0x001CEE08
		public void SimplifyMacros(IList<Parameter> parameters)
		{
			this.instructions.SimplifyMacros(this.localList, parameters);
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x001CEE1C File Offset: 0x001CEE1C
		public void OptimizeMacros()
		{
			this.instructions.OptimizeMacros();
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x001CEE2C File Offset: 0x001CEE2C
		public void SimplifyBranches()
		{
			this.instructions.SimplifyBranches();
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x001CEE3C File Offset: 0x001CEE3C
		public void OptimizeBranches()
		{
			this.instructions.OptimizeBranches();
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x001CEE4C File Offset: 0x001CEE4C
		public uint UpdateInstructionOffsets()
		{
			return this.instructions.UpdateInstructionOffsets();
		}

		// Token: 0x040030A6 RID: 12454
		private bool keepOldMaxStack;

		// Token: 0x040030A7 RID: 12455
		private bool initLocals;

		// Token: 0x040030A8 RID: 12456
		private byte headerSize;

		// Token: 0x040030A9 RID: 12457
		private ushort maxStack;

		// Token: 0x040030AA RID: 12458
		private uint localVarSigTok;

		// Token: 0x040030AB RID: 12459
		private readonly IList<Instruction> instructions;

		// Token: 0x040030AC RID: 12460
		private readonly IList<ExceptionHandler> exceptionHandlers;

		// Token: 0x040030AD RID: 12461
		private readonly LocalList localList;

		// Token: 0x040030AE RID: 12462
		public const byte SMALL_HEADER_SIZE = 1;

		// Token: 0x040030AF RID: 12463
		private PdbMethod pdbMethod;
	}
}
