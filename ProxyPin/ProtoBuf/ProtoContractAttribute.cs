using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C30 RID: 3120
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProtoContractAttribute : Attribute
	{
		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06007BBE RID: 31678 RVA: 0x002470D4 File Offset: 0x002470D4
		// (set) Token: 0x06007BBF RID: 31679 RVA: 0x002470DC File Offset: 0x002470DC
		public string Name { get; set; }

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06007BC0 RID: 31680 RVA: 0x002470E8 File Offset: 0x002470E8
		// (set) Token: 0x06007BC1 RID: 31681 RVA: 0x002470F0 File Offset: 0x002470F0
		public int ImplicitFirstTag
		{
			get
			{
				return this.implicitFirstTag;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ImplicitFirstTag");
				}
				this.implicitFirstTag = value;
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06007BC2 RID: 31682 RVA: 0x0024710C File Offset: 0x0024710C
		// (set) Token: 0x06007BC3 RID: 31683 RVA: 0x00247118 File Offset: 0x00247118
		public bool UseProtoMembersOnly
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value);
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x00247124 File Offset: 0x00247124
		// (set) Token: 0x06007BC5 RID: 31685 RVA: 0x00247130 File Offset: 0x00247130
		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value);
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06007BC6 RID: 31686 RVA: 0x0024713C File Offset: 0x0024713C
		// (set) Token: 0x06007BC7 RID: 31687 RVA: 0x00247144 File Offset: 0x00247144
		public ImplicitFields ImplicitFields { get; set; }

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06007BC8 RID: 31688 RVA: 0x00247150 File Offset: 0x00247150
		// (set) Token: 0x06007BC9 RID: 31689 RVA: 0x0024715C File Offset: 0x0024715C
		public bool InferTagFromName
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value);
				this.SetFlag(2, true);
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06007BCA RID: 31690 RVA: 0x00247170 File Offset: 0x00247170
		internal bool InferTagFromNameHasValue
		{
			get
			{
				return this.HasFlag(2);
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06007BCB RID: 31691 RVA: 0x0024717C File Offset: 0x0024717C
		// (set) Token: 0x06007BCC RID: 31692 RVA: 0x00247184 File Offset: 0x00247184
		public int DataMemberOffset { get; set; }

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06007BCD RID: 31693 RVA: 0x00247190 File Offset: 0x00247190
		// (set) Token: 0x06007BCE RID: 31694 RVA: 0x0024719C File Offset: 0x0024719C
		public bool SkipConstructor
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value);
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06007BCF RID: 31695 RVA: 0x002471A8 File Offset: 0x002471A8
		// (set) Token: 0x06007BD0 RID: 31696 RVA: 0x002471B4 File Offset: 0x002471B4
		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value);
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06007BD1 RID: 31697 RVA: 0x002471C0 File Offset: 0x002471C0
		// (set) Token: 0x06007BD2 RID: 31698 RVA: 0x002471D0 File Offset: 0x002471D0
		public bool IsGroup
		{
			get
			{
				return this.HasFlag(256);
			}
			set
			{
				this.SetFlag(256, value);
			}
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x002471E0 File Offset: 0x002471E0
		private bool HasFlag(ushort flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x002471F0 File Offset: 0x002471F0
		private void SetFlag(ushort flag, bool value)
		{
			if (value)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06007BD5 RID: 31701 RVA: 0x00247218 File Offset: 0x00247218
		// (set) Token: 0x06007BD6 RID: 31702 RVA: 0x00247224 File Offset: 0x00247224
		public bool EnumPassthru
		{
			get
			{
				return this.HasFlag(64);
			}
			set
			{
				this.SetFlag(64, value);
				this.SetFlag(128, true);
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06007BD7 RID: 31703 RVA: 0x0024723C File Offset: 0x0024723C
		// (set) Token: 0x06007BD8 RID: 31704 RVA: 0x00247244 File Offset: 0x00247244
		public Type Surrogate { get; set; }

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06007BD9 RID: 31705 RVA: 0x00247250 File Offset: 0x00247250
		internal bool EnumPassthruHasValue
		{
			get
			{
				return this.HasFlag(128);
			}
		}

		// Token: 0x04003BCE RID: 15310
		private int implicitFirstTag;

		// Token: 0x04003BD1 RID: 15313
		private ushort flags;

		// Token: 0x04003BD2 RID: 15314
		private const ushort OPTIONS_InferTagFromName = 1;

		// Token: 0x04003BD3 RID: 15315
		private const ushort OPTIONS_InferTagFromNameHasValue = 2;

		// Token: 0x04003BD4 RID: 15316
		private const ushort OPTIONS_UseProtoMembersOnly = 4;

		// Token: 0x04003BD5 RID: 15317
		private const ushort OPTIONS_SkipConstructor = 8;

		// Token: 0x04003BD6 RID: 15318
		private const ushort OPTIONS_IgnoreListHandling = 16;

		// Token: 0x04003BD7 RID: 15319
		private const ushort OPTIONS_AsReferenceDefault = 32;

		// Token: 0x04003BD8 RID: 15320
		private const ushort OPTIONS_EnumPassthru = 64;

		// Token: 0x04003BD9 RID: 15321
		private const ushort OPTIONS_EnumPassthruHasValue = 128;

		// Token: 0x04003BDA RID: 15322
		private const ushort OPTIONS_IsGroup = 256;
	}
}
