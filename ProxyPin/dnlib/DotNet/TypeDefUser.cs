using System;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200085B RID: 2139
	[ComVisible(true)]
	public class TypeDefUser : TypeDef
	{
		// Token: 0x06005196 RID: 20886 RVA: 0x00192C48 File Offset: 0x00192C48
		public TypeDefUser(UTF8String name) : this(null, name, null)
		{
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00192C54 File Offset: 0x00192C54
		public TypeDefUser(UTF8String @namespace, UTF8String name) : this(@namespace, name, null)
		{
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x00192C60 File Offset: 0x00192C60
		public TypeDefUser(UTF8String name, ITypeDefOrRef baseType) : this(null, name, baseType)
		{
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00192C6C File Offset: 0x00192C6C
		public TypeDefUser(UTF8String @namespace, UTF8String name, ITypeDefOrRef baseType)
		{
			this.fields = new LazyList<FieldDef>(this);
			this.methods = new LazyList<MethodDef>(this);
			this.genericParameters = new LazyList<GenericParam>(this);
			this.nestedTypes = new LazyList<TypeDef>(this);
			this.events = new LazyList<EventDef>(this);
			this.properties = new LazyList<PropertyDef>(this);
			this.@namespace = @namespace;
			this.name = name;
			this.baseType = baseType;
			this.baseType_isInitialized = true;
		}
	}
}
