using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000804 RID: 2052
	[ComVisible(true)]
	public sealed class CustomMarshalType : MarshalType
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0017A61C File Offset: 0x0017A61C
		// (set) Token: 0x060049D8 RID: 18904 RVA: 0x0017A624 File Offset: 0x0017A624
		public UTF8String Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x0017A630 File Offset: 0x0017A630
		// (set) Token: 0x060049DA RID: 18906 RVA: 0x0017A638 File Offset: 0x0017A638
		public UTF8String NativeTypeName
		{
			get
			{
				return this.nativeTypeName;
			}
			set
			{
				this.nativeTypeName = value;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x0017A644 File Offset: 0x0017A644
		// (set) Token: 0x060049DC RID: 18908 RVA: 0x0017A64C File Offset: 0x0017A64C
		public ITypeDefOrRef CustomMarshaler
		{
			get
			{
				return this.custMarshaler;
			}
			set
			{
				this.custMarshaler = value;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x0017A658 File Offset: 0x0017A658
		// (set) Token: 0x060049DE RID: 18910 RVA: 0x0017A660 File Offset: 0x0017A660
		public UTF8String Cookie
		{
			get
			{
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0017A66C File Offset: 0x0017A66C
		public CustomMarshalType() : this(null, null, null, null)
		{
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x0017A678 File Offset: 0x0017A678
		public CustomMarshalType(UTF8String guid) : this(guid, null, null, null)
		{
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0017A684 File Offset: 0x0017A684
		public CustomMarshalType(UTF8String guid, UTF8String nativeTypeName) : this(guid, nativeTypeName, null, null)
		{
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x0017A690 File Offset: 0x0017A690
		public CustomMarshalType(UTF8String guid, UTF8String nativeTypeName, ITypeDefOrRef custMarshaler) : this(guid, nativeTypeName, custMarshaler, null)
		{
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0017A69C File Offset: 0x0017A69C
		public CustomMarshalType(UTF8String guid, UTF8String nativeTypeName, ITypeDefOrRef custMarshaler, UTF8String cookie) : base(NativeType.CustomMarshaler)
		{
			this.guid = guid;
			this.nativeTypeName = nativeTypeName;
			this.custMarshaler = custMarshaler;
			this.cookie = cookie;
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0017A6C4 File Offset: 0x0017A6C4
		public override string ToString()
		{
			return string.Format("{0} ({1}, {2}, {3}, {4})", new object[]
			{
				this.nativeType,
				this.guid,
				this.nativeTypeName,
				this.custMarshaler,
				this.cookie
			});
		}

		// Token: 0x0400254E RID: 9550
		private UTF8String guid;

		// Token: 0x0400254F RID: 9551
		private UTF8String nativeTypeName;

		// Token: 0x04002550 RID: 9552
		private ITypeDefOrRef custMarshaler;

		// Token: 0x04002551 RID: 9553
		private UTF8String cookie;
	}
}
