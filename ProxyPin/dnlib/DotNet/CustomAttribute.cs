using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000797 RID: 1943
	[ComVisible(true)]
	public sealed class CustomAttribute : ICustomAttribute
	{
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x0016D6E0 File Offset: 0x0016D6E0
		// (set) Token: 0x0600455E RID: 17758 RVA: 0x0016D6E8 File Offset: 0x0016D6E8
		public ICustomAttributeType Constructor
		{
			get
			{
				return this.ctor;
			}
			set
			{
				this.ctor = value;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x0016D6F4 File Offset: 0x0016D6F4
		public ITypeDefOrRef AttributeType
		{
			get
			{
				ICustomAttributeType customAttributeType = this.ctor;
				if (customAttributeType == null)
				{
					return null;
				}
				return customAttributeType.DeclaringType;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x0016D70C File Offset: 0x0016D70C
		public string TypeFullName
		{
			get
			{
				MemberRef memberRef = this.ctor as MemberRef;
				if (memberRef != null)
				{
					return memberRef.GetDeclaringTypeFullName() ?? string.Empty;
				}
				MethodDef methodDef = this.ctor as MethodDef;
				if (methodDef != null)
				{
					TypeDef declaringType = methodDef.DeclaringType;
					if (declaringType != null)
					{
						return declaringType.FullName;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x0016D770 File Offset: 0x0016D770
		public bool IsRawBlob
		{
			get
			{
				return this.rawData != null;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x0016D780 File Offset: 0x0016D780
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x0016D788 File Offset: 0x0016D788
		public IList<CAArgument> ConstructorArguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x0016D790 File Offset: 0x0016D790
		public bool HasConstructorArguments
		{
			get
			{
				return this.arguments.Count > 0;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x0016D7A0 File Offset: 0x0016D7A0
		public IList<CANamedArgument> NamedArguments
		{
			get
			{
				return this.namedArguments;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x0016D7A8 File Offset: 0x0016D7A8
		public bool HasNamedArguments
		{
			get
			{
				return this.namedArguments.Count > 0;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x0016D7B8 File Offset: 0x0016D7B8
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

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x0016D7C8 File Offset: 0x0016D7C8
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

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0016D7D8 File Offset: 0x0016D7D8
		public uint BlobOffset
		{
			get
			{
				return this.caBlobOffset;
			}
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x0016D7E0 File Offset: 0x0016D7E0
		public CustomAttribute(ICustomAttributeType ctor, byte[] rawData) : this(ctor, null, null, 0U)
		{
			this.rawData = rawData;
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x0016D7F4 File Offset: 0x0016D7F4
		public CustomAttribute(ICustomAttributeType ctor) : this(ctor, null, null, 0U)
		{
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x0016D800 File Offset: 0x0016D800
		public CustomAttribute(ICustomAttributeType ctor, IEnumerable<CAArgument> arguments) : this(ctor, arguments, null)
		{
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x0016D80C File Offset: 0x0016D80C
		public CustomAttribute(ICustomAttributeType ctor, IEnumerable<CANamedArgument> namedArguments) : this(ctor, null, namedArguments)
		{
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x0016D818 File Offset: 0x0016D818
		public CustomAttribute(ICustomAttributeType ctor, IEnumerable<CAArgument> arguments, IEnumerable<CANamedArgument> namedArguments) : this(ctor, arguments, namedArguments, 0U)
		{
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x0016D824 File Offset: 0x0016D824
		public CustomAttribute(ICustomAttributeType ctor, IEnumerable<CAArgument> arguments, IEnumerable<CANamedArgument> namedArguments, uint caBlobOffset)
		{
			this.ctor = ctor;
			this.arguments = ((arguments == null) ? new List<CAArgument>() : new List<CAArgument>(arguments));
			this.namedArguments = ((namedArguments == null) ? new List<CANamedArgument>() : new List<CANamedArgument>(namedArguments));
			this.caBlobOffset = caBlobOffset;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x0016D884 File Offset: 0x0016D884
		internal CustomAttribute(ICustomAttributeType ctor, List<CAArgument> arguments, List<CANamedArgument> namedArguments, uint caBlobOffset)
		{
			this.ctor = ctor;
			this.arguments = (arguments ?? new List<CAArgument>());
			this.namedArguments = (namedArguments ?? new List<CANamedArgument>());
			this.caBlobOffset = caBlobOffset;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x0016D8C4 File Offset: 0x0016D8C4
		public CANamedArgument GetField(string name)
		{
			return this.GetNamedArgument(name, true);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x0016D8D0 File Offset: 0x0016D8D0
		public CANamedArgument GetField(UTF8String name)
		{
			return this.GetNamedArgument(name, true);
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x0016D8DC File Offset: 0x0016D8DC
		public CANamedArgument GetProperty(string name)
		{
			return this.GetNamedArgument(name, false);
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x0016D8E8 File Offset: 0x0016D8E8
		public CANamedArgument GetProperty(UTF8String name)
		{
			return this.GetNamedArgument(name, false);
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x0016D8F4 File Offset: 0x0016D8F4
		public CANamedArgument GetNamedArgument(string name, bool isField)
		{
			IList<CANamedArgument> list = this.namedArguments;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				CANamedArgument canamedArgument = list[i];
				if (canamedArgument.IsField == isField && UTF8String.ToSystemStringOrEmpty(canamedArgument.Name) == name)
				{
					return canamedArgument;
				}
			}
			return null;
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x0016D950 File Offset: 0x0016D950
		public CANamedArgument GetNamedArgument(UTF8String name, bool isField)
		{
			IList<CANamedArgument> list = this.namedArguments;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				CANamedArgument canamedArgument = list[i];
				if (canamedArgument.IsField == isField && UTF8String.Equals(canamedArgument.Name, name))
				{
					return canamedArgument;
				}
			}
			return null;
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x0016D9A8 File Offset: 0x0016D9A8
		public override string ToString()
		{
			return this.TypeFullName;
		}

		// Token: 0x04002443 RID: 9283
		private ICustomAttributeType ctor;

		// Token: 0x04002444 RID: 9284
		private byte[] rawData;

		// Token: 0x04002445 RID: 9285
		private readonly IList<CAArgument> arguments;

		// Token: 0x04002446 RID: 9286
		private readonly IList<CANamedArgument> namedArguments;

		// Token: 0x04002447 RID: 9287
		private uint caBlobOffset;
	}
}
