using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000902 RID: 2306
	[ComVisible(true)]
	public sealed class PdbTupleElementNames
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x001B5B8C File Offset: 0x001B5B8C
		// (set) Token: 0x06005952 RID: 22866 RVA: 0x001B5BC0 File Offset: 0x001B5BC0
		public string Name
		{
			get
			{
				string text = this.name;
				if (text != null)
				{
					return text;
				}
				Local local = this.local;
				if (local == null)
				{
					return null;
				}
				return local.Name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x001B5BCC File Offset: 0x001B5BCC
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x001B5BD4 File Offset: 0x001B5BD4
		public Local Local
		{
			get
			{
				return this.local;
			}
			set
			{
				this.local = value;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x001B5BE0 File Offset: 0x001B5BE0
		public bool IsConstant
		{
			get
			{
				return this.local == null;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06005956 RID: 22870 RVA: 0x001B5BEC File Offset: 0x001B5BEC
		public bool IsVariable
		{
			get
			{
				return this.local != null;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x001B5BFC File Offset: 0x001B5BFC
		// (set) Token: 0x06005958 RID: 22872 RVA: 0x001B5C04 File Offset: 0x001B5C04
		public Instruction ScopeStart
		{
			get
			{
				return this.scopeStart;
			}
			set
			{
				this.scopeStart = value;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x001B5C10 File Offset: 0x001B5C10
		// (set) Token: 0x0600595A RID: 22874 RVA: 0x001B5C18 File Offset: 0x001B5C18
		public Instruction ScopeEnd
		{
			get
			{
				return this.scopeEnd;
			}
			set
			{
				this.scopeEnd = value;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x0600595B RID: 22875 RVA: 0x001B5C24 File Offset: 0x001B5C24
		public IList<string> TupleElementNames
		{
			get
			{
				return this.tupleElementNames;
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001B5C2C File Offset: 0x001B5C2C
		public PdbTupleElementNames()
		{
			this.tupleElementNames = new List<string>();
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x001B5C40 File Offset: 0x001B5C40
		public PdbTupleElementNames(int capacity)
		{
			this.tupleElementNames = new List<string>(capacity);
		}

		// Token: 0x04002B49 RID: 11081
		private readonly IList<string> tupleElementNames;

		// Token: 0x04002B4A RID: 11082
		private string name;

		// Token: 0x04002B4B RID: 11083
		private Local local;

		// Token: 0x04002B4C RID: 11084
		private Instruction scopeStart;

		// Token: 0x04002B4D RID: 11085
		private Instruction scopeEnd;
	}
}
