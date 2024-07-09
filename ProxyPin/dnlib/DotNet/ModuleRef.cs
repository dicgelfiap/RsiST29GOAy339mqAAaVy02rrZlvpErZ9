using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000822 RID: 2082
	[ComVisible(true)]
	public abstract class ModuleRef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IMemberRefParent, IFullName, IHasCustomDebugInformation, IResolutionScope, IModule, IScope, IOwnerModule
	{
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x00184AF8 File Offset: 0x00184AF8
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ModuleRef, this.rid);
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x00184B08 File Offset: 0x00184B08
		// (set) Token: 0x06004DAD RID: 19885 RVA: 0x00184B10 File Offset: 0x00184B10
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x00184B1C File Offset: 0x00184B1C
		public int HasCustomAttributeTag
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x00184B20 File Offset: 0x00184B20
		public int MemberRefParentTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00184B24 File Offset: 0x00184B24
		public int ResolutionScopeTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x00184B28 File Offset: 0x00184B28
		public ScopeType ScopeType
		{
			get
			{
				return ScopeType.ModuleRef;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00184B2C File Offset: 0x00184B2C
		public string ScopeName
		{
			get
			{
				return this.FullName;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x00184B34 File Offset: 0x00184B34
		// (set) Token: 0x06004DB4 RID: 19892 RVA: 0x00184B3C File Offset: 0x00184B3C
		public UTF8String Name
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

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x00184B48 File Offset: 0x00184B48
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x00184B64 File Offset: 0x00184B64
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x00184B78 File Offset: 0x00184B78
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x00184B88 File Offset: 0x00184B88
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x00184B8C File Offset: 0x00184B8C
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x00184B9C File Offset: 0x00184B9C
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00184BB8 File Offset: 0x00184BB8
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x00184BCC File Offset: 0x00184BCC
		public ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x00184BD4 File Offset: 0x00184BD4
		public ModuleDef DefinitionModule
		{
			get
			{
				if (this.module == null)
				{
					return null;
				}
				UTF8String a = this.name;
				if (UTF8String.CaseInsensitiveEquals(a, this.module.Name))
				{
					return this.module;
				}
				AssemblyDef definitionAssembly = this.DefinitionAssembly;
				if (definitionAssembly == null)
				{
					return null;
				}
				return definitionAssembly.FindModule(a);
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00184C2C File Offset: 0x00184C2C
		public AssemblyDef DefinitionAssembly
		{
			get
			{
				ModuleDef moduleDef = this.module;
				if (moduleDef == null)
				{
					return null;
				}
				return moduleDef.Assembly;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x00184C44 File Offset: 0x00184C44
		public string FullName
		{
			get
			{
				return UTF8String.ToSystemStringOrEmpty(this.name);
			}
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00184C54 File Offset: 0x00184C54
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04002638 RID: 9784
		protected uint rid;

		// Token: 0x04002639 RID: 9785
		protected ModuleDef module;

		// Token: 0x0400263A RID: 9786
		protected UTF8String name;

		// Token: 0x0400263B RID: 9787
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400263C RID: 9788
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
