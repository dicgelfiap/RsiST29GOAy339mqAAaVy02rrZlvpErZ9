using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000840 RID: 2112
	[ComVisible(true)]
	public sealed class LinkedResource : Resource
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x001873A8 File Offset: 0x001873A8
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Linked;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x001873AC File Offset: 0x001873AC
		// (set) Token: 0x06004EEA RID: 20202 RVA: 0x001873B4 File Offset: 0x001873B4
		public FileDef File
		{
			get
			{
				return this.file;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.file = value;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x001873D0 File Offset: 0x001873D0
		// (set) Token: 0x06004EEC RID: 20204 RVA: 0x001873E0 File Offset: 0x001873E0
		public byte[] Hash
		{
			get
			{
				return this.file.HashValue;
			}
			set
			{
				this.file.HashValue = value;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x001873F0 File Offset: 0x001873F0
		public UTF8String FileName
		{
			get
			{
				if (this.file != null)
				{
					return this.file.Name;
				}
				return UTF8String.Empty;
			}
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00187410 File Offset: 0x00187410
		public LinkedResource(UTF8String name, FileDef file, ManifestResourceAttributes flags) : base(name, flags)
		{
			this.file = file;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x00187424 File Offset: 0x00187424
		public override string ToString()
		{
			return UTF8String.ToSystemStringOrEmpty(base.Name) + " - file: " + UTF8String.ToSystemStringOrEmpty(this.FileName);
		}

		// Token: 0x040026D4 RID: 9940
		private FileDef file;
	}
}
