using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C38 RID: 3128
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	public class ProtoMemberAttribute : Attribute, IComparable, IComparable<ProtoMemberAttribute>
	{
		// Token: 0x06007BF7 RID: 31735 RVA: 0x00247414 File Offset: 0x00247414
		public int CompareTo(object other)
		{
			return this.CompareTo(other as ProtoMemberAttribute);
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x00247424 File Offset: 0x00247424
		public int CompareTo(ProtoMemberAttribute other)
		{
			if (other == null)
			{
				return -1;
			}
			if (this == other)
			{
				return 0;
			}
			int num = this.tag.CompareTo(other.tag);
			if (num == 0)
			{
				num = string.CompareOrdinal(this.name, other.name);
			}
			return num;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x00247474 File Offset: 0x00247474
		public ProtoMemberAttribute(int tag) : this(tag, false)
		{
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x00247480 File Offset: 0x00247480
		internal ProtoMemberAttribute(int tag, bool forced)
		{
			if (tag <= 0 && !forced)
			{
				throw new ArgumentOutOfRangeException("tag");
			}
			this.tag = tag;
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x06007BFB RID: 31739 RVA: 0x002474A8 File Offset: 0x002474A8
		// (set) Token: 0x06007BFC RID: 31740 RVA: 0x002474B0 File Offset: 0x002474B0
		public string Name
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

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x06007BFD RID: 31741 RVA: 0x002474BC File Offset: 0x002474BC
		// (set) Token: 0x06007BFE RID: 31742 RVA: 0x002474C4 File Offset: 0x002474C4
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.dataFormat = value;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x06007BFF RID: 31743 RVA: 0x002474D0 File Offset: 0x002474D0
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x002474D8 File Offset: 0x002474D8
		internal void Rebase(int tag)
		{
			this.tag = tag;
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06007C01 RID: 31745 RVA: 0x002474E4 File Offset: 0x002474E4
		// (set) Token: 0x06007C02 RID: 31746 RVA: 0x002474F4 File Offset: 0x002474F4
		public bool IsRequired
		{
			get
			{
				return (this.options & MemberSerializationOptions.Required) == MemberSerializationOptions.Required;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Required;
					return;
				}
				this.options &= ~MemberSerializationOptions.Required;
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06007C03 RID: 31747 RVA: 0x0024751C File Offset: 0x0024751C
		// (set) Token: 0x06007C04 RID: 31748 RVA: 0x0024752C File Offset: 0x0024752C
		public bool IsPacked
		{
			get
			{
				return (this.options & MemberSerializationOptions.Packed) == MemberSerializationOptions.Packed;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Packed;
					return;
				}
				this.options &= ~MemberSerializationOptions.Packed;
			}
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06007C05 RID: 31749 RVA: 0x00247554 File Offset: 0x00247554
		// (set) Token: 0x06007C06 RID: 31750 RVA: 0x00247564 File Offset: 0x00247564
		public bool OverwriteList
		{
			get
			{
				return (this.options & MemberSerializationOptions.OverwriteList) == MemberSerializationOptions.OverwriteList;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.OverwriteList;
					return;
				}
				this.options &= ~MemberSerializationOptions.OverwriteList;
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06007C07 RID: 31751 RVA: 0x0024758C File Offset: 0x0024758C
		// (set) Token: 0x06007C08 RID: 31752 RVA: 0x0024759C File Offset: 0x0024759C
		public bool AsReference
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReference) == MemberSerializationOptions.AsReference;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReference;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReference;
				}
				this.options |= MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x002475D8 File Offset: 0x002475D8
		// (set) Token: 0x06007C0A RID: 31754 RVA: 0x002475E8 File Offset: 0x002475E8
		internal bool AsReferenceHasValue
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReferenceHasValue) == MemberSerializationOptions.AsReferenceHasValue;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReferenceHasValue;
					return;
				}
				this.options &= ~MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x00247610 File Offset: 0x00247610
		// (set) Token: 0x06007C0C RID: 31756 RVA: 0x00247620 File Offset: 0x00247620
		public bool DynamicType
		{
			get
			{
				return (this.options & MemberSerializationOptions.DynamicType) == MemberSerializationOptions.DynamicType;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.DynamicType;
					return;
				}
				this.options &= ~MemberSerializationOptions.DynamicType;
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x06007C0D RID: 31757 RVA: 0x00247648 File Offset: 0x00247648
		// (set) Token: 0x06007C0E RID: 31758 RVA: 0x00247650 File Offset: 0x00247650
		public MemberSerializationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x04003BE6 RID: 15334
		internal MemberInfo Member;

		// Token: 0x04003BE7 RID: 15335
		internal MemberInfo BackingMember;

		// Token: 0x04003BE8 RID: 15336
		internal bool TagIsPinned;

		// Token: 0x04003BE9 RID: 15337
		private string name;

		// Token: 0x04003BEA RID: 15338
		private DataFormat dataFormat;

		// Token: 0x04003BEB RID: 15339
		private int tag;

		// Token: 0x04003BEC RID: 15340
		private MemberSerializationOptions options;
	}
}
