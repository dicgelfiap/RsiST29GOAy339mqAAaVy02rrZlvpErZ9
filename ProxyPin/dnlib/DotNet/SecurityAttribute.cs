using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000843 RID: 2115
	[ComVisible(true)]
	public sealed class SecurityAttribute : ICustomAttribute
	{
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x0018784C File Offset: 0x0018784C
		// (set) Token: 0x06004EFC RID: 20220 RVA: 0x00187854 File Offset: 0x00187854
		public ITypeDefOrRef AttributeType
		{
			get
			{
				return this.attrType;
			}
			set
			{
				this.attrType = value;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x00187860 File Offset: 0x00187860
		public string TypeFullName
		{
			get
			{
				ITypeDefOrRef typeDefOrRef = this.attrType;
				if (typeDefOrRef != null)
				{
					return typeDefOrRef.FullName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0018788C File Offset: 0x0018788C
		public IList<CANamedArgument> NamedArguments
		{
			get
			{
				return this.namedArguments;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00187894 File Offset: 0x00187894
		public bool HasNamedArguments
		{
			get
			{
				return this.namedArguments.Count > 0;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x001878A4 File Offset: 0x001878A4
		public IEnumerable<CANamedArgument> Fields
		{
			get
			{
				IList<CANamedArgument> namedArguments = this.namedArguments;
				int count = namedArguments.Count;
				int num;
				for (int i = 0; i < count; i = num + 1)
				{
					CANamedArgument canamedArgument = namedArguments[i];
					if (canamedArgument.IsField)
					{
						yield return canamedArgument;
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x001878B4 File Offset: 0x001878B4
		public IEnumerable<CANamedArgument> Properties
		{
			get
			{
				IList<CANamedArgument> namedArguments = this.namedArguments;
				int count = namedArguments.Count;
				int num;
				for (int i = 0; i < count; i = num + 1)
				{
					CANamedArgument canamedArgument = namedArguments[i];
					if (canamedArgument.IsProperty)
					{
						yield return canamedArgument;
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x001878C4 File Offset: 0x001878C4
		public static SecurityAttribute CreateFromXml(ModuleDef module, string xml)
		{
			ITypeDefOrRef typeRef = module.CorLibTypes.GetTypeRef("System.Security.Permissions", "PermissionSetAttribute");
			UTF8String value = new UTF8String(xml);
			CANamedArgument item = new CANamedArgument(false, module.CorLibTypes.String, "XML", new CAArgument(module.CorLibTypes.String, value));
			List<CANamedArgument> list = new List<CANamedArgument>
			{
				item
			};
			return new SecurityAttribute(typeRef, list);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x00187934 File Offset: 0x00187934
		public SecurityAttribute() : this(null, null)
		{
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x00187940 File Offset: 0x00187940
		public SecurityAttribute(ITypeDefOrRef attrType) : this(attrType, null)
		{
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0018794C File Offset: 0x0018794C
		public SecurityAttribute(ITypeDefOrRef attrType, IList<CANamedArgument> namedArguments)
		{
			this.attrType = attrType;
			this.namedArguments = (namedArguments ?? new List<CANamedArgument>());
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x00187970 File Offset: 0x00187970
		public override string ToString()
		{
			return this.TypeFullName;
		}

		// Token: 0x040026EC RID: 9964
		private ITypeDefOrRef attrType;

		// Token: 0x040026ED RID: 9965
		private readonly IList<CANamedArgument> namedArguments;
	}
}
