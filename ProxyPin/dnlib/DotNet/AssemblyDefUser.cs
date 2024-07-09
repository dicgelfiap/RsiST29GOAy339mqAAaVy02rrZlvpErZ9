using System;
using System.Reflection;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x02000776 RID: 1910
	[ComVisible(true)]
	public class AssemblyDefUser : AssemblyDef
	{
		// Token: 0x0600433C RID: 17212 RVA: 0x00167300 File Offset: 0x00167300
		public AssemblyDefUser() : this(UTF8String.Empty, new Version(0, 0, 0, 0))
		{
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x00167318 File Offset: 0x00167318
		public AssemblyDefUser(UTF8String name) : this(name, new Version(0, 0, 0, 0), new PublicKey())
		{
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x00167330 File Offset: 0x00167330
		public AssemblyDefUser(UTF8String name, Version version) : this(name, version, new PublicKey())
		{
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x00167340 File Offset: 0x00167340
		public AssemblyDefUser(UTF8String name, Version version, PublicKey publicKey) : this(name, version, publicKey, UTF8String.Empty)
		{
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x00167350 File Offset: 0x00167350
		public AssemblyDefUser(UTF8String name, Version version, PublicKey publicKey, UTF8String locale)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			this.modules = new LazyList<ModuleDef>(this);
			this.name = name;
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.version = version;
			this.publicKey = (publicKey ?? new PublicKey());
			this.culture = locale;
			this.attributes = 0;
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x001673D8 File Offset: 0x001673D8
		public AssemblyDefUser(AssemblyName asmName) : this(new AssemblyNameInfo(asmName))
		{
			this.hashAlgorithm = (AssemblyHashAlgorithm)asmName.HashAlgorithm;
			this.attributes = (int)asmName.Flags;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x00167400 File Offset: 0x00167400
		public AssemblyDefUser(IAssembly asmName)
		{
			if (asmName == null)
			{
				throw new ArgumentNullException("asmName");
			}
			this.modules = new LazyList<ModuleDef>(this);
			this.name = asmName.Name;
			this.version = (asmName.Version ?? new Version(0, 0, 0, 0));
			this.publicKey = ((asmName.PublicKeyOrToken as PublicKey) ?? new PublicKey());
			this.culture = asmName.Culture;
			this.attributes = 0;
			this.hashAlgorithm = AssemblyHashAlgorithm.SHA1;
		}
	}
}
