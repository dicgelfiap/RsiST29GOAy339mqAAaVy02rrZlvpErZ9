using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x0200083D RID: 2109
	[ComVisible(true)]
	public abstract class Resource : IMDTokenProvider
	{
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x001871E4 File Offset: 0x001871E4
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ManifestResource, this.rid);
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001871F4 File Offset: 0x001871F4
		// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x001871FC File Offset: 0x001871FC
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

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x00187208 File Offset: 0x00187208
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x00187210 File Offset: 0x00187210
		public uint? Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0018721C File Offset: 0x0018721C
		// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x00187224 File Offset: 0x00187224
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x00187230 File Offset: 0x00187230
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x00187238 File Offset: 0x00187238
		public ManifestResourceAttributes Attributes
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06004ED7 RID: 20183
		public abstract ResourceType ResourceType { get; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x00187244 File Offset: 0x00187244
		// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x00187250 File Offset: 0x00187250
		public ManifestResourceAttributes Visibility
		{
			get
			{
				return this.flags & ManifestResourceAttributes.VisibilityMask;
			}
			set
			{
				this.flags = ((this.flags & ~ManifestResourceAttributes.VisibilityMask) | (value & ManifestResourceAttributes.VisibilityMask));
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x00187268 File Offset: 0x00187268
		public bool IsPublic
		{
			get
			{
				return (this.flags & ManifestResourceAttributes.VisibilityMask) == ManifestResourceAttributes.Public;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00187278 File Offset: 0x00187278
		public bool IsPrivate
		{
			get
			{
				return (this.flags & ManifestResourceAttributes.VisibilityMask) == ManifestResourceAttributes.Private;
			}
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x00187288 File Offset: 0x00187288
		protected Resource(UTF8String name, ManifestResourceAttributes flags)
		{
			this.name = name;
			this.flags = flags;
		}

		// Token: 0x040026CC RID: 9932
		private uint rid;

		// Token: 0x040026CD RID: 9933
		private uint? offset;

		// Token: 0x040026CE RID: 9934
		private UTF8String name;

		// Token: 0x040026CF RID: 9935
		private ManifestResourceAttributes flags;
	}
}
