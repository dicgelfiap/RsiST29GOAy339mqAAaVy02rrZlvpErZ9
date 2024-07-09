using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E3 RID: 2531
	[ComVisible(true)]
	public sealed class Local : IVariable
	{
		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x0600610F RID: 24847 RVA: 0x001CEB8C File Offset: 0x001CEB8C
		// (set) Token: 0x06006110 RID: 24848 RVA: 0x001CEB94 File Offset: 0x001CEB94
		public TypeSig Type
		{
			get
			{
				return this.typeSig;
			}
			set
			{
				this.typeSig = value;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x001CEBA0 File Offset: 0x001CEBA0
		// (set) Token: 0x06006112 RID: 24850 RVA: 0x001CEBA8 File Offset: 0x001CEBA8
		public int Index
		{
			get
			{
				return this.index;
			}
			internal set
			{
				this.index = value;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x001CEBB4 File Offset: 0x001CEBB4
		// (set) Token: 0x06006114 RID: 24852 RVA: 0x001CEBBC File Offset: 0x001CEBBC
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

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x001CEBC8 File Offset: 0x001CEBC8
		// (set) Token: 0x06006116 RID: 24854 RVA: 0x001CEBD0 File Offset: 0x001CEBD0
		public PdbLocalAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x001CEBDC File Offset: 0x001CEBDC
		internal void SetName(string name)
		{
			this.name = name;
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x001CEBE8 File Offset: 0x001CEBE8
		internal void SetAttributes(PdbLocalAttributes attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x001CEBF4 File Offset: 0x001CEBF4
		public Local(TypeSig typeSig)
		{
			this.typeSig = typeSig;
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x001CEC04 File Offset: 0x001CEC04
		public Local(TypeSig typeSig, string name)
		{
			this.typeSig = typeSig;
			this.name = name;
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x001CEC1C File Offset: 0x001CEC1C
		public Local(TypeSig typeSig, string name, int index)
		{
			this.typeSig = typeSig;
			this.name = name;
			this.index = index;
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x001CEC3C File Offset: 0x001CEC3C
		public override string ToString()
		{
			string text = this.name;
			if (string.IsNullOrEmpty(text))
			{
				return string.Format("V_{0}", this.Index);
			}
			return text;
		}

		// Token: 0x040030A1 RID: 12449
		private TypeSig typeSig;

		// Token: 0x040030A2 RID: 12450
		private int index;

		// Token: 0x040030A3 RID: 12451
		private string name;

		// Token: 0x040030A4 RID: 12452
		private PdbLocalAttributes attributes;
	}
}
