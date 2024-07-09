using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000828 RID: 2088
	[DebuggerDisplay("{Sequence} {Name}")]
	[ComVisible(true)]
	public abstract class ParamDef : IHasConstant, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName, IHasFieldMarshal, IHasCustomDebugInformation
	{
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x00184DDC File Offset: 0x00184DDC
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Param, this.rid);
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x00184DEC File Offset: 0x00184DEC
		// (set) Token: 0x06004DCF RID: 19919 RVA: 0x00184DF4 File Offset: 0x00184DF4
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

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x00184E00 File Offset: 0x00184E00
		public int HasConstantTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x00184E04 File Offset: 0x00184E04
		public int HasCustomAttributeTag
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x00184E08 File Offset: 0x00184E08
		public int HasFieldMarshalTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x00184E0C File Offset: 0x00184E0C
		// (set) Token: 0x06004DD4 RID: 19924 RVA: 0x00184E14 File Offset: 0x00184E14
		public MethodDef DeclaringMethod
		{
			get
			{
				return this.declaringMethod;
			}
			internal set
			{
				this.declaringMethod = value;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004DD5 RID: 19925 RVA: 0x00184E20 File Offset: 0x00184E20
		// (set) Token: 0x06004DD6 RID: 19926 RVA: 0x00184E2C File Offset: 0x00184E2C
		public ParamAttributes Attributes
		{
			get
			{
				return (ParamAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x00184E38 File Offset: 0x00184E38
		// (set) Token: 0x06004DD8 RID: 19928 RVA: 0x00184E40 File Offset: 0x00184E40
		public ushort Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x00184E4C File Offset: 0x00184E4C
		// (set) Token: 0x06004DDA RID: 19930 RVA: 0x00184E54 File Offset: 0x00184E54
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

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x00184E60 File Offset: 0x00184E60
		// (set) Token: 0x06004DDC RID: 19932 RVA: 0x00184E7C File Offset: 0x00184E7C
		public MarshalType MarshalType
		{
			get
			{
				if (!this.marshalType_isInitialized)
				{
					this.InitializeMarshalType();
				}
				return this.marshalType;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.marshalType = value;
					this.marshalType_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00184EC4 File Offset: 0x00184EC4
		private void InitializeMarshalType()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.marshalType_isInitialized)
				{
					this.marshalType = this.GetMarshalType_NoLock();
					this.marshalType_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00184F20 File Offset: 0x00184F20
		protected virtual MarshalType GetMarshalType_NoLock()
		{
			return null;
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00184F24 File Offset: 0x00184F24
		protected void ResetMarshalType()
		{
			this.marshalType_isInitialized = false;
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x00184F30 File Offset: 0x00184F30
		// (set) Token: 0x06004DE1 RID: 19937 RVA: 0x00184F4C File Offset: 0x00184F4C
		public Constant Constant
		{
			get
			{
				if (!this.constant_isInitialized)
				{
					this.InitializeConstant();
				}
				return this.constant;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.constant = value;
					this.constant_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x00184F94 File Offset: 0x00184F94
		private void InitializeConstant()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.constant_isInitialized)
				{
					this.constant = this.GetConstant_NoLock();
					this.constant_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00184FF0 File Offset: 0x00184FF0
		protected virtual Constant GetConstant_NoLock()
		{
			return null;
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00184FF4 File Offset: 0x00184FF4
		protected void ResetConstant()
		{
			this.constant_isInitialized = false;
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004DE5 RID: 19941 RVA: 0x00185000 File Offset: 0x00185000
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0018501C File Offset: 0x0018501C
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x00185030 File Offset: 0x00185030
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x00185040 File Offset: 0x00185040
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x00185044 File Offset: 0x00185044
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x00185054 File Offset: 0x00185054
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00185070 File Offset: 0x00185070
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x00185084 File Offset: 0x00185084
		public bool HasConstant
		{
			get
			{
				return this.Constant != null;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x00185094 File Offset: 0x00185094
		public ElementType ElementType
		{
			get
			{
				Constant constant = this.Constant;
				if (constant != null)
				{
					return constant.Type;
				}
				return ElementType.End;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x001850BC File Offset: 0x001850BC
		public bool HasMarshalType
		{
			get
			{
				return this.MarshalType != null;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x001850CC File Offset: 0x001850CC
		public string FullName
		{
			get
			{
				UTF8String utf8String = this.name;
				if (UTF8String.IsNullOrEmpty(utf8String))
				{
					return string.Format("A_{0}", this.sequence);
				}
				return utf8String.String;
			}
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0018510C File Offset: 0x0018510C
		private void ModifyAttributes(bool set, ParamAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x00185134 File Offset: 0x00185134
		// (set) Token: 0x06004DF2 RID: 19954 RVA: 0x00185144 File Offset: 0x00185144
		public bool IsIn
		{
			get
			{
				return ((ushort)this.attributes & 1) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.In);
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x00185150 File Offset: 0x00185150
		// (set) Token: 0x06004DF4 RID: 19956 RVA: 0x00185160 File Offset: 0x00185160
		public bool IsOut
		{
			get
			{
				return ((ushort)this.attributes & 2) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.Out);
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x0018516C File Offset: 0x0018516C
		// (set) Token: 0x06004DF6 RID: 19958 RVA: 0x0018517C File Offset: 0x0018517C
		public bool IsLcid
		{
			get
			{
				return ((ushort)this.attributes & 4) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.Lcid);
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x00185188 File Offset: 0x00185188
		// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x00185198 File Offset: 0x00185198
		public bool IsRetval
		{
			get
			{
				return ((ushort)this.attributes & 8) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.Retval);
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x001851A4 File Offset: 0x001851A4
		// (set) Token: 0x06004DFA RID: 19962 RVA: 0x001851B4 File Offset: 0x001851B4
		public bool IsOptional
		{
			get
			{
				return ((ushort)this.attributes & 16) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.Optional);
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x001851C0 File Offset: 0x001851C0
		// (set) Token: 0x06004DFC RID: 19964 RVA: 0x001851D4 File Offset: 0x001851D4
		public bool HasDefault
		{
			get
			{
				return ((ushort)this.attributes & 4096) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.HasDefault);
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x001851E4 File Offset: 0x001851E4
		// (set) Token: 0x06004DFE RID: 19966 RVA: 0x001851F8 File Offset: 0x001851F8
		public bool HasFieldMarshal
		{
			get
			{
				return ((ushort)this.attributes & 8192) > 0;
			}
			set
			{
				this.ModifyAttributes(value, ParamAttributes.HasFieldMarshal);
			}
		}

		// Token: 0x0400267B RID: 9851
		protected uint rid;

		// Token: 0x0400267C RID: 9852
		private readonly Lock theLock = Lock.Create();

		// Token: 0x0400267D RID: 9853
		protected MethodDef declaringMethod;

		// Token: 0x0400267E RID: 9854
		protected int attributes;

		// Token: 0x0400267F RID: 9855
		protected ushort sequence;

		// Token: 0x04002680 RID: 9856
		protected UTF8String name;

		// Token: 0x04002681 RID: 9857
		protected MarshalType marshalType;

		// Token: 0x04002682 RID: 9858
		protected bool marshalType_isInitialized;

		// Token: 0x04002683 RID: 9859
		protected Constant constant;

		// Token: 0x04002684 RID: 9860
		protected bool constant_isInitialized;

		// Token: 0x04002685 RID: 9861
		protected CustomAttributeCollection customAttributes;

		// Token: 0x04002686 RID: 9862
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
