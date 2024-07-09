using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FE RID: 2302
	[ComVisible(true)]
	public sealed class PdbDynamicLocal
	{
		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x001B5A40 File Offset: 0x001B5A40
		public IList<byte> Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x001B5A48 File Offset: 0x001B5A48
		// (set) Token: 0x0600593D RID: 22845 RVA: 0x001B5A7C File Offset: 0x001B5A7C
		public string Name
		{
			get
			{
				string text = this.name;
				if (text != null)
				{
					return text;
				}
				Local local = this.local;
				if (local == null)
				{
					return null;
				}
				return local.Name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x001B5A88 File Offset: 0x001B5A88
		public bool IsConstant
		{
			get
			{
				return this.Local == null;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x0600593F RID: 22847 RVA: 0x001B5A94 File Offset: 0x001B5A94
		public bool IsVariable
		{
			get
			{
				return this.Local != null;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x001B5AA4 File Offset: 0x001B5AA4
		// (set) Token: 0x06005941 RID: 22849 RVA: 0x001B5AAC File Offset: 0x001B5AAC
		public Local Local
		{
			get
			{
				return this.local;
			}
			set
			{
				this.local = value;
			}
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x001B5AB8 File Offset: 0x001B5AB8
		public PdbDynamicLocal()
		{
			this.flags = new List<byte>();
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x001B5ACC File Offset: 0x001B5ACC
		public PdbDynamicLocal(int capacity)
		{
			this.flags = new List<byte>(capacity);
		}

		// Token: 0x04002B43 RID: 11075
		private readonly IList<byte> flags;

		// Token: 0x04002B44 RID: 11076
		private string name;

		// Token: 0x04002B45 RID: 11077
		private Local local;
	}
}
