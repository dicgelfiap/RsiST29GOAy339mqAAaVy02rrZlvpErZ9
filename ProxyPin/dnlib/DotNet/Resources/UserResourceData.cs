using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using dnlib.IO;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008ED RID: 2285
	[ComVisible(true)]
	public abstract class UserResourceData : IResourceData, IFileSection
	{
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x001B550C File Offset: 0x001B550C
		public string TypeName
		{
			get
			{
				return this.type.Name;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x060058F3 RID: 22771 RVA: 0x001B551C File Offset: 0x001B551C
		public ResourceTypeCode Code
		{
			get
			{
				return this.type.Code;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x001B552C File Offset: 0x001B552C
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x001B5534 File Offset: 0x001B5534
		public FileOffset StartOffset { get; set; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x060058F6 RID: 22774 RVA: 0x001B5540 File Offset: 0x001B5540
		// (set) Token: 0x060058F7 RID: 22775 RVA: 0x001B5548 File Offset: 0x001B5548
		public FileOffset EndOffset { get; set; }

		// Token: 0x060058F8 RID: 22776 RVA: 0x001B5554 File Offset: 0x001B5554
		public UserResourceData(UserResourceType type)
		{
			this.type = type;
		}

		// Token: 0x060058F9 RID: 22777
		public abstract void WriteData(BinaryWriter writer, IFormatter formatter);

		// Token: 0x04002B0A RID: 11018
		private readonly UserResourceType type;
	}
}
