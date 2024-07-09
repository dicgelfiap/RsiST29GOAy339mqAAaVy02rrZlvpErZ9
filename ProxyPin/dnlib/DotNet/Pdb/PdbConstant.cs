using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F3 RID: 2291
	[ComVisible(true)]
	public sealed class PdbConstant : IHasCustomDebugInformation
	{
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x001B5708 File Offset: 0x001B5708
		// (set) Token: 0x06005905 RID: 22789 RVA: 0x001B5710 File Offset: 0x001B5710
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x001B571C File Offset: 0x001B571C
		// (set) Token: 0x06005907 RID: 22791 RVA: 0x001B5724 File Offset: 0x001B5724
		public TypeSig Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06005908 RID: 22792 RVA: 0x001B5730 File Offset: 0x001B5730
		// (set) Token: 0x06005909 RID: 22793 RVA: 0x001B5738 File Offset: 0x001B5738
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x001B5744 File Offset: 0x001B5744
		public PdbConstant()
		{
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x001B5758 File Offset: 0x001B5758
		public PdbConstant(string name, TypeSig type, object value)
		{
			this.name = name;
			this.type = type;
			this.value = value;
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x001B5780 File Offset: 0x001B5780
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x0600590D RID: 22797 RVA: 0x001B5784 File Offset: 0x001B5784
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x001B5794 File Offset: 0x001B5794
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x001B579C File Offset: 0x001B579C
		public override string ToString()
		{
			TypeSig typeSig = this.Type;
			return string.Concat(new string[]
			{
				(typeSig == null) ? "" : typeSig.ToString(),
				" ",
				this.Name,
				" = ",
				(this.Value == null) ? "null" : (this.Value.ToString() + " (" + this.Value.GetType().FullName + ")")
			});
		}

		// Token: 0x04002B21 RID: 11041
		private string name;

		// Token: 0x04002B22 RID: 11042
		private TypeSig type;

		// Token: 0x04002B23 RID: 11043
		private object value;

		// Token: 0x04002B24 RID: 11044
		private readonly IList<PdbCustomDebugInfo> customDebugInfos = new List<PdbCustomDebugInfo>();
	}
}
