using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200079E RID: 1950
	[DebuggerDisplay("{Action} Count={SecurityAttributes.Count}")]
	[ComVisible(true)]
	public abstract class DeclSecurity : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x0016EDC8 File Offset: 0x0016EDC8
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.DeclSecurity, this.rid);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x0016EDD8 File Offset: 0x0016EDD8
		// (set) Token: 0x060045C4 RID: 17860 RVA: 0x0016EDE0 File Offset: 0x0016EDE0
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

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x0016EDEC File Offset: 0x0016EDEC
		public int HasCustomAttributeTag
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x0016EDF0 File Offset: 0x0016EDF0
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x0016EDF8 File Offset: 0x0016EDF8
		public SecurityAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x0016EE04 File Offset: 0x0016EE04
		public IList<SecurityAttribute> SecurityAttributes
		{
			get
			{
				if (this.securityAttributes == null)
				{
					this.InitializeSecurityAttributes();
				}
				return this.securityAttributes;
			}
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x0016EE20 File Offset: 0x0016EE20
		protected virtual void InitializeSecurityAttributes()
		{
			Interlocked.CompareExchange<IList<SecurityAttribute>>(ref this.securityAttributes, new List<SecurityAttribute>(), null);
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x0016EE34 File Offset: 0x0016EE34
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

		// Token: 0x060045CB RID: 17867 RVA: 0x0016EE50 File Offset: 0x0016EE50
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x0016EE64 File Offset: 0x0016EE64
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x0016EE74 File Offset: 0x0016EE74
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x0016EE78 File Offset: 0x0016EE78
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x0016EE88 File Offset: 0x0016EE88
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

		// Token: 0x060045D0 RID: 17872 RVA: 0x0016EEA4 File Offset: 0x0016EEA4
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x0016EEB8 File Offset: 0x0016EEB8
		public bool HasSecurityAttributes
		{
			get
			{
				return this.SecurityAttributes.Count > 0;
			}
		}

		// Token: 0x060045D2 RID: 17874
		public abstract byte[] GetBlob();

		// Token: 0x060045D3 RID: 17875 RVA: 0x0016EEC8 File Offset: 0x0016EEC8
		public string GetNet1xXmlString()
		{
			return DeclSecurity.GetNet1xXmlStringInternal(this.SecurityAttributes);
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x0016EED8 File Offset: 0x0016EED8
		internal static string GetNet1xXmlStringInternal(IList<SecurityAttribute> secAttrs)
		{
			if (secAttrs == null || secAttrs.Count != 1)
			{
				return null;
			}
			SecurityAttribute securityAttribute = secAttrs[0];
			if (securityAttribute == null || securityAttribute.TypeFullName != "System.Security.Permissions.PermissionSetAttribute")
			{
				return null;
			}
			if (securityAttribute.NamedArguments.Count != 1)
			{
				return null;
			}
			CANamedArgument canamedArgument = securityAttribute.NamedArguments[0];
			if (canamedArgument == null || !canamedArgument.IsProperty || canamedArgument.Name != "XML")
			{
				return null;
			}
			if (canamedArgument.ArgumentType.GetElementType() != ElementType.String)
			{
				return null;
			}
			CAArgument argument = canamedArgument.Argument;
			if (argument.Type.GetElementType() != ElementType.String)
			{
				return null;
			}
			UTF8String utf8String = argument.Value as UTF8String;
			if (utf8String != null)
			{
				return utf8String;
			}
			string text = argument.Value as string;
			if (text != null)
			{
				return text;
			}
			return null;
		}

		// Token: 0x04002456 RID: 9302
		protected uint rid;

		// Token: 0x04002457 RID: 9303
		protected SecurityAction action;

		// Token: 0x04002458 RID: 9304
		protected IList<SecurityAttribute> securityAttributes;

		// Token: 0x04002459 RID: 9305
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400245A RID: 9306
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
