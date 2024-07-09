using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C36 RID: 3126
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProtoIncludeAttribute : Attribute
	{
		// Token: 0x06007BE9 RID: 31721 RVA: 0x00247310 File Offset: 0x00247310
		public ProtoIncludeAttribute(int tag, Type knownType) : this(tag, (knownType == null) ? "" : knownType.AssemblyQualifiedName)
		{
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x00247344 File Offset: 0x00247344
		public ProtoIncludeAttribute(int tag, string knownTypeName)
		{
			if (tag <= 0)
			{
				throw new ArgumentOutOfRangeException("tag", "Tags must be positive integers");
			}
			if (string.IsNullOrEmpty(knownTypeName))
			{
				throw new ArgumentNullException("knownTypeName", "Known type cannot be blank");
			}
			this.Tag = tag;
			this.KnownTypeName = knownTypeName;
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x06007BEB RID: 31723 RVA: 0x0024739C File Offset: 0x0024739C
		public int Tag { get; }

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x06007BEC RID: 31724 RVA: 0x002473A4 File Offset: 0x002473A4
		public string KnownTypeName { get; }

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06007BED RID: 31725 RVA: 0x002473AC File Offset: 0x002473AC
		public Type KnownType
		{
			get
			{
				return TypeModel.ResolveKnownType(this.KnownTypeName, null, null);
			}
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06007BEE RID: 31726 RVA: 0x002473BC File Offset: 0x002473BC
		// (set) Token: 0x06007BEF RID: 31727 RVA: 0x002473C4 File Offset: 0x002473C4
		[DefaultValue(DataFormat.Default)]
		public DataFormat DataFormat { get; set; }
	}
}
