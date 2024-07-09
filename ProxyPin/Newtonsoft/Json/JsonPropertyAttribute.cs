using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A85 RID: 2693
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x060068D8 RID: 26840 RVA: 0x001FC650 File Offset: 0x001FC650
		// (set) Token: 0x060068D9 RID: 26841 RVA: 0x001FC658 File Offset: 0x001FC658
		public Type ItemConverterType { get; set; }

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x060068DA RID: 26842 RVA: 0x001FC664 File Offset: 0x001FC664
		// (set) Token: 0x060068DB RID: 26843 RVA: 0x001FC66C File Offset: 0x001FC66C
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

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x060068DC RID: 26844 RVA: 0x001FC678 File Offset: 0x001FC678
		// (set) Token: 0x060068DD RID: 26845 RVA: 0x001FC680 File Offset: 0x001FC680
		public Type NamingStrategyType { get; set; }

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x060068DE RID: 26846 RVA: 0x001FC68C File Offset: 0x001FC68C
		// (set) Token: 0x060068DF RID: 26847 RVA: 0x001FC694 File Offset: 0x001FC694
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x060068E0 RID: 26848 RVA: 0x001FC6A0 File Offset: 0x001FC6A0
		// (set) Token: 0x060068E1 RID: 26849 RVA: 0x001FC6B0 File Offset: 0x001FC6B0
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x060068E2 RID: 26850 RVA: 0x001FC6C0 File Offset: 0x001FC6C0
		// (set) Token: 0x060068E3 RID: 26851 RVA: 0x001FC6D0 File Offset: 0x001FC6D0
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x001FC6E0 File Offset: 0x001FC6E0
		// (set) Token: 0x060068E5 RID: 26853 RVA: 0x001FC6F0 File Offset: 0x001FC6F0
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x060068E6 RID: 26854 RVA: 0x001FC700 File Offset: 0x001FC700
		// (set) Token: 0x060068E7 RID: 26855 RVA: 0x001FC710 File Offset: 0x001FC710
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x060068E8 RID: 26856 RVA: 0x001FC720 File Offset: 0x001FC720
		// (set) Token: 0x060068E9 RID: 26857 RVA: 0x001FC730 File Offset: 0x001FC730
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x060068EA RID: 26858 RVA: 0x001FC740 File Offset: 0x001FC740
		// (set) Token: 0x060068EB RID: 26859 RVA: 0x001FC750 File Offset: 0x001FC750
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

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x060068EC RID: 26860 RVA: 0x001FC760 File Offset: 0x001FC760
		// (set) Token: 0x060068ED RID: 26861 RVA: 0x001FC770 File Offset: 0x001FC770
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x060068EE RID: 26862 RVA: 0x001FC780 File Offset: 0x001FC780
		// (set) Token: 0x060068EF RID: 26863 RVA: 0x001FC790 File Offset: 0x001FC790
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x060068F0 RID: 26864 RVA: 0x001FC7A0 File Offset: 0x001FC7A0
		// (set) Token: 0x060068F1 RID: 26865 RVA: 0x001FC7A8 File Offset: 0x001FC7A8
		public string PropertyName { get; set; }

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x060068F2 RID: 26866 RVA: 0x001FC7B4 File Offset: 0x001FC7B4
		// (set) Token: 0x060068F3 RID: 26867 RVA: 0x001FC7C4 File Offset: 0x001FC7C4
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

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x060068F4 RID: 26868 RVA: 0x001FC7D4 File Offset: 0x001FC7D4
		// (set) Token: 0x060068F5 RID: 26869 RVA: 0x001FC7E4 File Offset: 0x001FC7E4
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

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x060068F6 RID: 26870 RVA: 0x001FC7F4 File Offset: 0x001FC7F4
		// (set) Token: 0x060068F7 RID: 26871 RVA: 0x001FC804 File Offset: 0x001FC804
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

		// Token: 0x060068F8 RID: 26872 RVA: 0x001FC814 File Offset: 0x001FC814
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x001FC81C File Offset: 0x001FC81C
		[NullableContext(1)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x0400354E RID: 13646
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x0400354F RID: 13647
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04003550 RID: 13648
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04003551 RID: 13649
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04003552 RID: 13650
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04003553 RID: 13651
		internal bool? _isReference;

		// Token: 0x04003554 RID: 13652
		internal int? _order;

		// Token: 0x04003555 RID: 13653
		internal Required? _required;

		// Token: 0x04003556 RID: 13654
		internal bool? _itemIsReference;

		// Token: 0x04003557 RID: 13655
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04003558 RID: 13656
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
