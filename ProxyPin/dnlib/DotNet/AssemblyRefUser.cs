using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200077F RID: 1919
	[ComVisible(true)]
	public class AssemblyRefUser : AssemblyRef
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x0016A8C4 File Offset: 0x0016A8C4
		public static AssemblyRefUser CreateMscorlibReferenceCLR10()
		{
			return new AssemblyRefUser("mscorlib", new Version(1, 0, 3300, 0), new PublicKeyToken("b77a5c561934e089"));
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0016A8EC File Offset: 0x0016A8EC
		public static AssemblyRefUser CreateMscorlibReferenceCLR11()
		{
			return new AssemblyRefUser("mscorlib", new Version(1, 0, 5000, 0), new PublicKeyToken("b77a5c561934e089"));
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0016A914 File Offset: 0x0016A914
		public static AssemblyRefUser CreateMscorlibReferenceCLR20()
		{
			return new AssemblyRefUser("mscorlib", new Version(2, 0, 0, 0), new PublicKeyToken("b77a5c561934e089"));
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0016A938 File Offset: 0x0016A938
		public static AssemblyRefUser CreateMscorlibReferenceCLR40()
		{
			return new AssemblyRefUser("mscorlib", new Version(4, 0, 0, 0), new PublicKeyToken("b77a5c561934e089"));
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0016A95C File Offset: 0x0016A95C
		public AssemblyRefUser() : this(UTF8String.Empty)
		{
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x0016A96C File Offset: 0x0016A96C
		public AssemblyRefUser(UTF8String name) : this(name, new Version(0, 0, 0, 0))
		{
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x0016A980 File Offset: 0x0016A980
		public AssemblyRefUser(UTF8String name, Version version) : this(name, version, new PublicKey())
		{
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x0016A990 File Offset: 0x0016A990
		public AssemblyRefUser(UTF8String name, Version version, PublicKeyBase publicKey) : this(name, version, publicKey, UTF8String.Empty)
		{
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x0016A9A0 File Offset: 0x0016A9A0
		public AssemblyRefUser(UTF8String name, Version version, PublicKeyBase publicKey, UTF8String locale)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			this.name = name;
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.version = version;
			this.publicKeyOrToken = publicKey;
			this.culture = locale;
			this.attributes = ((publicKey is PublicKey) ? 1 : 0);
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x0016AA24 File Offset: 0x0016AA24
		public AssemblyRefUser(AssemblyName asmName) : this(new AssemblyNameInfo(asmName))
		{
			this.attributes = (int)asmName.Flags;
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x0016AA40 File Offset: 0x0016AA40
		public AssemblyRefUser(IAssembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("asmName");
			}
			this.version = (assembly.Version ?? new Version(0, 0, 0, 0));
			this.publicKeyOrToken = assembly.PublicKeyOrToken;
			this.name = (UTF8String.IsNullOrEmpty(assembly.Name) ? UTF8String.Empty : assembly.Name);
			this.culture = assembly.Culture;
			this.attributes = (int)(((this.publicKeyOrToken is PublicKey) ? AssemblyAttributes.PublicKey : AssemblyAttributes.None) | assembly.ContentType);
		}
	}
}
