using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200083F RID: 2111
	[ComVisible(true)]
	public sealed class AssemblyLinkedResource : Resource
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00187338 File Offset: 0x00187338
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.AssemblyLinked;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x0018733C File Offset: 0x0018733C
		// (set) Token: 0x06004EE5 RID: 20197 RVA: 0x00187344 File Offset: 0x00187344
		public AssemblyRef Assembly
		{
			get
			{
				return this.asmRef;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.asmRef = value;
			}
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x00187360 File Offset: 0x00187360
		public AssemblyLinkedResource(UTF8String name, AssemblyRef asmRef, ManifestResourceAttributes flags) : base(name, flags)
		{
			if (asmRef == null)
			{
				throw new ArgumentNullException("asmRef");
			}
			this.asmRef = asmRef;
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x00187384 File Offset: 0x00187384
		public override string ToString()
		{
			return UTF8String.ToSystemStringOrEmpty(base.Name) + " - assembly: " + this.asmRef.FullName;
		}

		// Token: 0x040026D3 RID: 9939
		private AssemblyRef asmRef;
	}
}
