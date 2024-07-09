using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A77 RID: 2679
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x001FB4B8 File Offset: 0x001FB4B8
		// (set) Token: 0x06006845 RID: 26693 RVA: 0x001FB4C0 File Offset: 0x001FB4C0
		public string Id { get; set; }

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06006846 RID: 26694 RVA: 0x001FB4CC File Offset: 0x001FB4CC
		// (set) Token: 0x06006847 RID: 26695 RVA: 0x001FB4D4 File Offset: 0x001FB4D4
		public string Title { get; set; }

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06006848 RID: 26696 RVA: 0x001FB4E0 File Offset: 0x001FB4E0
		// (set) Token: 0x06006849 RID: 26697 RVA: 0x001FB4E8 File Offset: 0x001FB4E8
		public string Description { get; set; }

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x0600684A RID: 26698 RVA: 0x001FB4F4 File Offset: 0x001FB4F4
		// (set) Token: 0x0600684B RID: 26699 RVA: 0x001FB4FC File Offset: 0x001FB4FC
		public Type ItemConverterType { get; set; }

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x0600684C RID: 26700 RVA: 0x001FB508 File Offset: 0x001FB508
		// (set) Token: 0x0600684D RID: 26701 RVA: 0x001FB510 File Offset: 0x001FB510
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ItemConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x0600684E RID: 26702 RVA: 0x001FB51C File Offset: 0x001FB51C
		// (set) Token: 0x0600684F RID: 26703 RVA: 0x001FB524 File Offset: 0x001FB524
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x001FB534 File Offset: 0x001FB534
		// (set) Token: 0x06006851 RID: 26705 RVA: 0x001FB53C File Offset: 0x001FB53C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06006852 RID: 26706 RVA: 0x001FB54C File Offset: 0x001FB54C
		// (set) Token: 0x06006853 RID: 26707 RVA: 0x001FB554 File Offset: 0x001FB554
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x001FB560 File Offset: 0x001FB560
		// (set) Token: 0x06006855 RID: 26709 RVA: 0x001FB570 File Offset: 0x001FB570
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06006856 RID: 26710 RVA: 0x001FB580 File Offset: 0x001FB580
		// (set) Token: 0x06006857 RID: 26711 RVA: 0x001FB590 File Offset: 0x001FB590
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06006858 RID: 26712 RVA: 0x001FB5A0 File Offset: 0x001FB5A0
		// (set) Token: 0x06006859 RID: 26713 RVA: 0x001FB5B0 File Offset: 0x001FB5B0
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x001FB5C0 File Offset: 0x001FB5C0
		// (set) Token: 0x0600685B RID: 26715 RVA: 0x001FB5D0 File Offset: 0x001FB5D0
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x001FB5E0 File Offset: 0x001FB5E0
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x001FB5E8 File Offset: 0x001FB5E8
		[NullableContext(1)]
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x0400352E RID: 13614
		internal bool? _isReference;

		// Token: 0x0400352F RID: 13615
		internal bool? _itemIsReference;

		// Token: 0x04003530 RID: 13616
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04003531 RID: 13617
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x04003532 RID: 13618
		private Type _namingStrategyType;

		// Token: 0x04003533 RID: 13619
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private object[] _namingStrategyParameters;
	}
}
