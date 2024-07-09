using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x020007AB RID: 1963
	[ComVisible(true)]
	public abstract class FieldDef : IHasConstant, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName, IHasFieldMarshal, IMemberForwarded, IMemberRef, IOwnerModule, IIsTypeOrMethod, IHasCustomDebugInformation, IField, ITokenOperand, IMemberDef, IDnlibDef
	{
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x00170510 File Offset: 0x00170510
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Field, this.rid);
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x00170520 File Offset: 0x00170520
		// (set) Token: 0x0600469C RID: 18076 RVA: 0x00170528 File Offset: 0x00170528
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

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x00170534 File Offset: 0x00170534
		public int HasConstantTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x00170538 File Offset: 0x00170538
		public int HasCustomAttributeTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x0017053C File Offset: 0x0017053C
		public int HasFieldMarshalTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x00170540 File Offset: 0x00170540
		public int MemberForwardedTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x00170544 File Offset: 0x00170544
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

		// Token: 0x060046A2 RID: 18082 RVA: 0x00170560 File Offset: 0x00170560
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00170574 File Offset: 0x00170574
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00170578 File Offset: 0x00170578
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x00170588 File Offset: 0x00170588
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

		// Token: 0x060046A6 RID: 18086 RVA: 0x001705A4 File Offset: 0x001705A4
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060046A7 RID: 18087 RVA: 0x001705B8 File Offset: 0x001705B8
		// (set) Token: 0x060046A8 RID: 18088 RVA: 0x001705C4 File Offset: 0x001705C4
		public FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060046A9 RID: 18089 RVA: 0x001705D0 File Offset: 0x001705D0
		// (set) Token: 0x060046AA RID: 18090 RVA: 0x001705D8 File Offset: 0x001705D8
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

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x001705E4 File Offset: 0x001705E4
		// (set) Token: 0x060046AC RID: 18092 RVA: 0x001705EC File Offset: 0x001705EC
		public CallingConventionSig Signature
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x001705F8 File Offset: 0x001705F8
		// (set) Token: 0x060046AE RID: 18094 RVA: 0x00170614 File Offset: 0x00170614
		public uint? FieldOffset
		{
			get
			{
				if (!this.fieldOffset_isInitialized)
				{
					this.InitializeFieldOffset();
				}
				return this.fieldOffset;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.fieldOffset = value;
					this.fieldOffset_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0017065C File Offset: 0x0017065C
		private void InitializeFieldOffset()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.fieldOffset_isInitialized)
				{
					this.fieldOffset = this.GetFieldOffset_NoLock();
					this.fieldOffset_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001706B8 File Offset: 0x001706B8
		protected virtual uint? GetFieldOffset_NoLock()
		{
			return null;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060046B1 RID: 18097 RVA: 0x001706D4 File Offset: 0x001706D4
		// (set) Token: 0x060046B2 RID: 18098 RVA: 0x001706F0 File Offset: 0x001706F0
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

		// Token: 0x060046B3 RID: 18099 RVA: 0x00170738 File Offset: 0x00170738
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

		// Token: 0x060046B4 RID: 18100 RVA: 0x00170794 File Offset: 0x00170794
		protected virtual MarshalType GetMarshalType_NoLock()
		{
			return null;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x00170798 File Offset: 0x00170798
		protected void ResetMarshalType()
		{
			this.marshalType_isInitialized = false;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x001707A4 File Offset: 0x001707A4
		// (set) Token: 0x060046B7 RID: 18103 RVA: 0x001707C0 File Offset: 0x001707C0
		public RVA RVA
		{
			get
			{
				if (!this.rva_isInitialized)
				{
					this.InitializeRVA();
				}
				return this.rva;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.rva = value;
					this.rva_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x00170808 File Offset: 0x00170808
		private void InitializeRVA()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.rva_isInitialized)
				{
					this.rva = this.GetRVA_NoLock();
					this.rva_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x00170864 File Offset: 0x00170864
		protected virtual RVA GetRVA_NoLock()
		{
			return (RVA)0U;
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00170868 File Offset: 0x00170868
		protected void ResetRVA()
		{
			this.rva_isInitialized = false;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x00170874 File Offset: 0x00170874
		// (set) Token: 0x060046BC RID: 18108 RVA: 0x00170890 File Offset: 0x00170890
		public byte[] InitialValue
		{
			get
			{
				if (!this.initialValue_isInitialized)
				{
					this.InitializeInitialValue();
				}
				return this.initialValue;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.initialValue = value;
					this.initialValue_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x001708D8 File Offset: 0x001708D8
		private void InitializeInitialValue()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.initialValue_isInitialized)
				{
					this.initialValue = this.GetInitialValue_NoLock();
					this.initialValue_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00170934 File Offset: 0x00170934
		protected virtual byte[] GetInitialValue_NoLock()
		{
			return null;
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00170938 File Offset: 0x00170938
		protected void ResetInitialValue()
		{
			this.initialValue_isInitialized = false;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060046C0 RID: 18112 RVA: 0x00170944 File Offset: 0x00170944
		// (set) Token: 0x060046C1 RID: 18113 RVA: 0x00170960 File Offset: 0x00170960
		public ImplMap ImplMap
		{
			get
			{
				if (!this.implMap_isInitialized)
				{
					this.InitializeImplMap();
				}
				return this.implMap;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.implMap = value;
					this.implMap_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x001709A8 File Offset: 0x001709A8
		private void InitializeImplMap()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.implMap_isInitialized)
				{
					this.implMap = this.GetImplMap_NoLock();
					this.implMap_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00170A04 File Offset: 0x00170A04
		protected virtual ImplMap GetImplMap_NoLock()
		{
			return null;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x00170A08 File Offset: 0x00170A08
		// (set) Token: 0x060046C5 RID: 18117 RVA: 0x00170A24 File Offset: 0x00170A24
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

		// Token: 0x060046C6 RID: 18118 RVA: 0x00170A6C File Offset: 0x00170A6C
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

		// Token: 0x060046C7 RID: 18119 RVA: 0x00170AC8 File Offset: 0x00170AC8
		protected virtual Constant GetConstant_NoLock()
		{
			return null;
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x00170ACC File Offset: 0x00170ACC
		protected void ResetConstant()
		{
			this.constant_isInitialized = false;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x00170AD8 File Offset: 0x00170AD8
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x00170AE8 File Offset: 0x00170AE8
		public bool HasImplMap
		{
			get
			{
				return this.ImplMap != null;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x00170AF8 File Offset: 0x00170AF8
		// (set) Token: 0x060046CC RID: 18124 RVA: 0x00170B00 File Offset: 0x00170B00
		public TypeDef DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				TypeDef typeDef = this.DeclaringType2;
				if (typeDef == value)
				{
					return;
				}
				if (typeDef != null)
				{
					typeDef.Fields.Remove(this);
				}
				if (value != null)
				{
					value.Fields.Add(this);
				}
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x00170B48 File Offset: 0x00170B48
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x00170B50 File Offset: 0x00170B50
		// (set) Token: 0x060046CF RID: 18127 RVA: 0x00170B58 File Offset: 0x00170B58
		public TypeDef DeclaringType2
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				this.declaringType2 = value;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x00170B64 File Offset: 0x00170B64
		// (set) Token: 0x060046D1 RID: 18129 RVA: 0x00170B74 File Offset: 0x00170B74
		public FieldSig FieldSig
		{
			get
			{
				return this.signature as FieldSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x00170B80 File Offset: 0x00170B80
		public ModuleDef Module
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				if (typeDef == null)
				{
					return null;
				}
				return typeDef.Module;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x00170B98 File Offset: 0x00170B98
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x00170B9C File Offset: 0x00170B9C
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x00170BA0 File Offset: 0x00170BA0
		bool IMemberRef.IsField
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x00170BA4 File Offset: 0x00170BA4
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x00170BA8 File Offset: 0x00170BA8
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x00170BAC File Offset: 0x00170BAC
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x00170BB0 File Offset: 0x00170BB0
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x00170BB4 File Offset: 0x00170BB4
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x00170BB8 File Offset: 0x00170BB8
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x00170BBC File Offset: 0x00170BBC
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x00170BC0 File Offset: 0x00170BC0
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x00170BC4 File Offset: 0x00170BC4
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x00170BC8 File Offset: 0x00170BC8
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x00170BCC File Offset: 0x00170BCC
		public bool HasLayoutInfo
		{
			get
			{
				return this.FieldOffset != null;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x00170BF4 File Offset: 0x00170BF4
		public bool HasConstant
		{
			get
			{
				return this.Constant != null;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x00170C04 File Offset: 0x00170C04
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

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x00170C2C File Offset: 0x00170C2C
		public bool HasMarshalType
		{
			get
			{
				return this.MarshalType != null;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x00170C3C File Offset: 0x00170C3C
		// (set) Token: 0x060046E5 RID: 18149 RVA: 0x00170C4C File Offset: 0x00170C4C
		public TypeSig FieldType
		{
			get
			{
				return this.FieldSig.GetFieldType();
			}
			set
			{
				FieldSig fieldSig = this.FieldSig;
				if (fieldSig != null)
				{
					fieldSig.Type = value;
				}
			}
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x00170C74 File Offset: 0x00170C74
		private void ModifyAttributes(FieldAttributes andMask, FieldAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00170C88 File Offset: 0x00170C88
		private void ModifyAttributes(bool set, FieldAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x00170CB0 File Offset: 0x00170CB0
		// (set) Token: 0x060046E9 RID: 18153 RVA: 0x00170CBC File Offset: 0x00170CBC
		public FieldAttributes Access
		{
			get
			{
				return (FieldAttributes)this.attributes & FieldAttributes.FieldAccessMask;
			}
			set
			{
				this.ModifyAttributes(~FieldAttributes.FieldAccessMask, value & FieldAttributes.FieldAccessMask);
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x00170CCC File Offset: 0x00170CCC
		public bool IsCompilerControlled
		{
			get
			{
				return this.IsPrivateScope;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x00170CD4 File Offset: 0x00170CD4
		public bool IsPrivateScope
		{
			get
			{
				return ((ushort)this.attributes & 7) == 0;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x00170CE4 File Offset: 0x00170CE4
		public bool IsPrivate
		{
			get
			{
				return ((ushort)this.attributes & 7) == 1;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x00170CF4 File Offset: 0x00170CF4
		public bool IsFamilyAndAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 2;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x00170D04 File Offset: 0x00170D04
		public bool IsAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 3;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x00170D14 File Offset: 0x00170D14
		public bool IsFamily
		{
			get
			{
				return ((ushort)this.attributes & 7) == 4;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x00170D24 File Offset: 0x00170D24
		public bool IsFamilyOrAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 5;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x00170D34 File Offset: 0x00170D34
		public bool IsPublic
		{
			get
			{
				return ((ushort)this.attributes & 7) == 6;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x00170D44 File Offset: 0x00170D44
		// (set) Token: 0x060046F3 RID: 18163 RVA: 0x00170D54 File Offset: 0x00170D54
		public bool IsStatic
		{
			get
			{
				return ((ushort)this.attributes & 16) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.Static);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00170D60 File Offset: 0x00170D60
		// (set) Token: 0x060046F5 RID: 18165 RVA: 0x00170D70 File Offset: 0x00170D70
		public bool IsInitOnly
		{
			get
			{
				return ((ushort)this.attributes & 32) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.InitOnly);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x00170D7C File Offset: 0x00170D7C
		// (set) Token: 0x060046F7 RID: 18167 RVA: 0x00170D8C File Offset: 0x00170D8C
		public bool IsLiteral
		{
			get
			{
				return ((ushort)this.attributes & 64) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.Literal);
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x00170D98 File Offset: 0x00170D98
		// (set) Token: 0x060046F9 RID: 18169 RVA: 0x00170DAC File Offset: 0x00170DAC
		public bool IsNotSerialized
		{
			get
			{
				return ((ushort)this.attributes & 128) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.NotSerialized);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x00170DBC File Offset: 0x00170DBC
		// (set) Token: 0x060046FB RID: 18171 RVA: 0x00170DD0 File Offset: 0x00170DD0
		public bool IsSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 512) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.SpecialName);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x00170DE0 File Offset: 0x00170DE0
		// (set) Token: 0x060046FD RID: 18173 RVA: 0x00170DF4 File Offset: 0x00170DF4
		public bool IsPinvokeImpl
		{
			get
			{
				return ((ushort)this.attributes & 8192) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.PinvokeImpl);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x00170E04 File Offset: 0x00170E04
		// (set) Token: 0x060046FF RID: 18175 RVA: 0x00170E18 File Offset: 0x00170E18
		public bool IsRuntimeSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 1024) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.RTSpecialName);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x00170E28 File Offset: 0x00170E28
		// (set) Token: 0x06004701 RID: 18177 RVA: 0x00170E3C File Offset: 0x00170E3C
		public bool HasFieldMarshal
		{
			get
			{
				return ((ushort)this.attributes & 4096) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.HasFieldMarshal);
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x00170E4C File Offset: 0x00170E4C
		// (set) Token: 0x06004703 RID: 18179 RVA: 0x00170E60 File Offset: 0x00170E60
		public bool HasDefault
		{
			get
			{
				return ((ushort)this.attributes & 32768) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.HasDefault);
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004704 RID: 18180 RVA: 0x00170E70 File Offset: 0x00170E70
		// (set) Token: 0x06004705 RID: 18181 RVA: 0x00170E84 File Offset: 0x00170E84
		public bool HasFieldRVA
		{
			get
			{
				return ((ushort)this.attributes & 256) > 0;
			}
			set
			{
				this.ModifyAttributes(value, FieldAttributes.HasFieldRVA);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x00170E94 File Offset: 0x00170E94
		public string FullName
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				return FullNameFactory.FieldFullName((typeDef != null) ? typeDef.FullName : null, this.name, this.FieldSig, null, null);
			}
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00170EC8 File Offset: 0x00170EC8
		public uint GetFieldSize()
		{
			uint result;
			if (!this.GetFieldSize(out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00170EEC File Offset: 0x00170EEC
		public bool GetFieldSize(out uint size)
		{
			return this.GetFieldSize(this.declaringType2, this.FieldSig, out size);
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00170F04 File Offset: 0x00170F04
		protected bool GetFieldSize(TypeDef declaringType, FieldSig fieldSig, out uint size)
		{
			return this.GetFieldSize(declaringType, fieldSig, this.GetPointerSize(declaringType), out size);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00170F18 File Offset: 0x00170F18
		protected bool GetFieldSize(TypeDef declaringType, FieldSig fieldSig, int ptrSize, out uint size)
		{
			size = 0U;
			return fieldSig != null && this.GetClassSize(declaringType, fieldSig.Type, ptrSize, out size);
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00170F38 File Offset: 0x00170F38
		private bool GetClassSize(TypeDef declaringType, TypeSig ts, int ptrSize, out uint size)
		{
			size = 0U;
			ts = ts.RemovePinnedAndModifiers();
			if (ts == null)
			{
				return false;
			}
			int primitiveSize = ts.ElementType.GetPrimitiveSize(ptrSize);
			if (primitiveSize >= 0)
			{
				size = (uint)primitiveSize;
				return true;
			}
			TypeDefOrRefSig typeDefOrRefSig = ts as TypeDefOrRefSig;
			if (typeDefOrRefSig == null)
			{
				return false;
			}
			TypeDef typeDef = typeDefOrRefSig.TypeDef;
			if (typeDef != null)
			{
				return TypeDef.GetClassSize(typeDef, out size);
			}
			TypeRef typeRef = typeDefOrRefSig.TypeRef;
			return typeRef != null && TypeDef.GetClassSize(typeRef.Resolve(), out size);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00170FB8 File Offset: 0x00170FB8
		private int GetPointerSize(TypeDef declaringType)
		{
			if (declaringType == null)
			{
				return 4;
			}
			ModuleDef module = declaringType.Module;
			if (module == null)
			{
				return 4;
			}
			return module.GetPointerSize();
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00170FE8 File Offset: 0x00170FE8
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040024BB RID: 9403
		protected uint rid;

		// Token: 0x040024BC RID: 9404
		private readonly Lock theLock = Lock.Create();

		// Token: 0x040024BD RID: 9405
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040024BE RID: 9406
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x040024BF RID: 9407
		protected int attributes;

		// Token: 0x040024C0 RID: 9408
		protected UTF8String name;

		// Token: 0x040024C1 RID: 9409
		protected CallingConventionSig signature;

		// Token: 0x040024C2 RID: 9410
		protected uint? fieldOffset;

		// Token: 0x040024C3 RID: 9411
		protected bool fieldOffset_isInitialized;

		// Token: 0x040024C4 RID: 9412
		protected MarshalType marshalType;

		// Token: 0x040024C5 RID: 9413
		protected bool marshalType_isInitialized;

		// Token: 0x040024C6 RID: 9414
		protected RVA rva;

		// Token: 0x040024C7 RID: 9415
		protected bool rva_isInitialized;

		// Token: 0x040024C8 RID: 9416
		protected byte[] initialValue;

		// Token: 0x040024C9 RID: 9417
		protected bool initialValue_isInitialized;

		// Token: 0x040024CA RID: 9418
		protected ImplMap implMap;

		// Token: 0x040024CB RID: 9419
		protected bool implMap_isInitialized;

		// Token: 0x040024CC RID: 9420
		protected Constant constant;

		// Token: 0x040024CD RID: 9421
		protected bool constant_isInitialized;

		// Token: 0x040024CE RID: 9422
		protected TypeDef declaringType2;
	}
}
