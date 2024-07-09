using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000819 RID: 2073
	[ComVisible(true)]
	public class ModuleContext
	{
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x0017E88C File Offset: 0x0017E88C
		// (set) Token: 0x06004BA8 RID: 19368 RVA: 0x0017E8B4 File Offset: 0x0017E8B4
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				if (this.assemblyResolver == null)
				{
					Interlocked.CompareExchange<IAssemblyResolver>(ref this.assemblyResolver, NullResolver.Instance, null);
				}
				return this.assemblyResolver;
			}
			set
			{
				this.assemblyResolver = value;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x0017E8C0 File Offset: 0x0017E8C0
		// (set) Token: 0x06004BAA RID: 19370 RVA: 0x0017E8E8 File Offset: 0x0017E8E8
		public IResolver Resolver
		{
			get
			{
				if (this.resolver == null)
				{
					Interlocked.CompareExchange<IResolver>(ref this.resolver, NullResolver.Instance, null);
				}
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0017E8F4 File Offset: 0x0017E8F4
		public ModuleContext()
		{
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x0017E8FC File Offset: 0x0017E8FC
		public ModuleContext(IAssemblyResolver assemblyResolver) : this(assemblyResolver, new Resolver(assemblyResolver))
		{
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0017E90C File Offset: 0x0017E90C
		public ModuleContext(IResolver resolver) : this(null, resolver)
		{
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0017E918 File Offset: 0x0017E918
		public ModuleContext(IAssemblyResolver assemblyResolver, IResolver resolver)
		{
			this.assemblyResolver = assemblyResolver;
			this.resolver = resolver;
			if (resolver == null && assemblyResolver != null)
			{
				this.resolver = new Resolver(assemblyResolver);
			}
		}

		// Token: 0x040025D4 RID: 9684
		private IAssemblyResolver assemblyResolver;

		// Token: 0x040025D5 RID: 9685
		private IResolver resolver;
	}
}
