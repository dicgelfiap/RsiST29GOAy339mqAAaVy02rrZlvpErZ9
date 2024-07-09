using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x020007B7 RID: 1975
	[DebuggerDisplay("{Name.String}")]
	[ComVisible(true)]
	public abstract class GenericParam : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation, IMemberDef, IDnlibDef, IFullName, IMemberRef, IOwnerModule, IIsTypeOrMethod, IListListener<GenericParamConstraint>
	{
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x0017679C File Offset: 0x0017679C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.GenericParam, this.rid);
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x001767AC File Offset: 0x001767AC
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x001767B4 File Offset: 0x001767B4
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

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x001767C0 File Offset: 0x001767C0
		public int HasCustomAttributeTag
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x001767C4 File Offset: 0x001767C4
		// (set) Token: 0x060047EB RID: 18411 RVA: 0x001767CC File Offset: 0x001767CC
		public ITypeOrMethodDef Owner
		{
			get
			{
				return this.owner;
			}
			internal set
			{
				this.owner = value;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x001767D8 File Offset: 0x001767D8
		public TypeDef DeclaringType
		{
			get
			{
				return this.owner as TypeDef;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x001767E8 File Offset: 0x001767E8
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.owner as TypeDef;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x001767F8 File Offset: 0x001767F8
		public MethodDef DeclaringMethod
		{
			get
			{
				return this.owner as MethodDef;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00176808 File Offset: 0x00176808
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x00176810 File Offset: 0x00176810
		public ushort Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x0017681C File Offset: 0x0017681C
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00176828 File Offset: 0x00176828
		public GenericParamAttributes Flags
		{
			get
			{
				return (GenericParamAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x00176834 File Offset: 0x00176834
		// (set) Token: 0x060047F4 RID: 18420 RVA: 0x0017683C File Offset: 0x0017683C
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

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060047F5 RID: 18421 RVA: 0x00176848 File Offset: 0x00176848
		// (set) Token: 0x060047F6 RID: 18422 RVA: 0x00176850 File Offset: 0x00176850
		public ITypeDefOrRef Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x0017685C File Offset: 0x0017685C
		public IList<GenericParamConstraint> GenericParamConstraints
		{
			get
			{
				if (this.genericParamConstraints == null)
				{
					this.InitializeGenericParamConstraints();
				}
				return this.genericParamConstraints;
			}
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x00176878 File Offset: 0x00176878
		protected virtual void InitializeGenericParamConstraints()
		{
			Interlocked.CompareExchange<LazyList<GenericParamConstraint>>(ref this.genericParamConstraints, new LazyList<GenericParamConstraint>(this), null);
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x00176890 File Offset: 0x00176890
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

		// Token: 0x060047FA RID: 18426 RVA: 0x001768AC File Offset: 0x001768AC
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x001768C0 File Offset: 0x001768C0
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x001768D0 File Offset: 0x001768D0
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x001768D4 File Offset: 0x001768D4
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x001768E4 File Offset: 0x001768E4
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

		// Token: 0x060047FF RID: 18431 RVA: 0x00176900 File Offset: 0x00176900
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x00176914 File Offset: 0x00176914
		public bool HasGenericParamConstraints
		{
			get
			{
				return this.GenericParamConstraints.Count > 0;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x00176924 File Offset: 0x00176924
		public ModuleDef Module
		{
			get
			{
				ITypeOrMethodDef typeOrMethodDef = this.owner;
				if (typeOrMethodDef == null)
				{
					return null;
				}
				return typeOrMethodDef.Module;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x0017693C File Offset: 0x0017693C
		public string FullName
		{
			get
			{
				return UTF8String.ToSystemStringOrEmpty(this.name);
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x0017694C File Offset: 0x0017694C
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00176950 File Offset: 0x00176950
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00176954 File Offset: 0x00176954
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x00176958 File Offset: 0x00176958
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x0017695C File Offset: 0x0017695C
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x00176960 File Offset: 0x00176960
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x00176964 File Offset: 0x00176964
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x00176968 File Offset: 0x00176968
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x0017696C File Offset: 0x0017696C
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x00176970 File Offset: 0x00176970
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x00176974 File Offset: 0x00176974
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x00176978 File Offset: 0x00176978
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x0017697C File Offset: 0x0017697C
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x00176980 File Offset: 0x00176980
		private void ModifyAttributes(GenericParamAttributes andMask, GenericParamAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x00176994 File Offset: 0x00176994
		private void ModifyAttributes(bool set, GenericParamAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x001769BC File Offset: 0x001769BC
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x001769C8 File Offset: 0x001769C8
		public GenericParamAttributes Variance
		{
			get
			{
				return (GenericParamAttributes)this.attributes & GenericParamAttributes.VarianceMask;
			}
			set
			{
				this.ModifyAttributes(~GenericParamAttributes.VarianceMask, value & GenericParamAttributes.VarianceMask);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x001769D8 File Offset: 0x001769D8
		public bool IsNonVariant
		{
			get
			{
				return this.Variance == GenericParamAttributes.NonVariant;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x001769E4 File Offset: 0x001769E4
		public bool IsCovariant
		{
			get
			{
				return this.Variance == GenericParamAttributes.Covariant;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x001769F0 File Offset: 0x001769F0
		public bool IsContravariant
		{
			get
			{
				return this.Variance == GenericParamAttributes.Contravariant;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x001769FC File Offset: 0x001769FC
		// (set) Token: 0x06004818 RID: 18456 RVA: 0x00176A08 File Offset: 0x00176A08
		public GenericParamAttributes SpecialConstraint
		{
			get
			{
				return (GenericParamAttributes)this.attributes & GenericParamAttributes.SpecialConstraintMask;
			}
			set
			{
				this.ModifyAttributes(~GenericParamAttributes.SpecialConstraintMask, value & GenericParamAttributes.SpecialConstraintMask);
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x00176A1C File Offset: 0x00176A1C
		public bool HasNoSpecialConstraint
		{
			get
			{
				return ((ushort)this.attributes & 28) == 0;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x00176A2C File Offset: 0x00176A2C
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x00176A3C File Offset: 0x00176A3C
		public bool HasReferenceTypeConstraint
		{
			get
			{
				return ((ushort)this.attributes & 4) > 0;
			}
			set
			{
				this.ModifyAttributes(value, GenericParamAttributes.ReferenceTypeConstraint);
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00176A48 File Offset: 0x00176A48
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x00176A58 File Offset: 0x00176A58
		public bool HasNotNullableValueTypeConstraint
		{
			get
			{
				return ((ushort)this.attributes & 8) > 0;
			}
			set
			{
				this.ModifyAttributes(value, GenericParamAttributes.NotNullableValueTypeConstraint);
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x00176A64 File Offset: 0x00176A64
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x00176A74 File Offset: 0x00176A74
		public bool HasDefaultConstructorConstraint
		{
			get
			{
				return ((ushort)this.attributes & 16) > 0;
			}
			set
			{
				this.ModifyAttributes(value, GenericParamAttributes.DefaultConstructorConstraint);
			}
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x00176A80 File Offset: 0x00176A80
		void IListListener<GenericParamConstraint>.OnLazyAdd(int index, ref GenericParamConstraint value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x00176A8C File Offset: 0x00176A8C
		internal virtual void OnLazyAdd2(int index, ref GenericParamConstraint value)
		{
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x00176A90 File Offset: 0x00176A90
		void IListListener<GenericParamConstraint>.OnAdd(int index, GenericParamConstraint value)
		{
			if (value.Owner != null)
			{
				throw new InvalidOperationException("Generic param constraint is already owned by another generic param. Set Owner to null first.");
			}
			value.Owner = this;
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00176AB0 File Offset: 0x00176AB0
		void IListListener<GenericParamConstraint>.OnRemove(int index, GenericParamConstraint value)
		{
			value.Owner = null;
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x00176ABC File Offset: 0x00176ABC
		void IListListener<GenericParamConstraint>.OnResize(int index)
		{
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x00176AC0 File Offset: 0x00176AC0
		void IListListener<GenericParamConstraint>.OnClear()
		{
			foreach (GenericParamConstraint genericParamConstraint in this.genericParamConstraints.GetEnumerable_NoLock())
			{
				genericParamConstraint.Owner = null;
			}
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x00176B1C File Offset: 0x00176B1C
		public override string ToString()
		{
			ITypeOrMethodDef typeOrMethodDef = this.owner;
			if (typeOrMethodDef is TypeDef)
			{
				return string.Format("!{0}", this.number);
			}
			if (typeOrMethodDef is MethodDef)
			{
				return string.Format("!!{0}", this.number);
			}
			return string.Format("??{0}", this.number);
		}

		// Token: 0x040024EE RID: 9454
		protected uint rid;

		// Token: 0x040024EF RID: 9455
		protected ITypeOrMethodDef owner;

		// Token: 0x040024F0 RID: 9456
		protected ushort number;

		// Token: 0x040024F1 RID: 9457
		protected int attributes;

		// Token: 0x040024F2 RID: 9458
		protected UTF8String name;

		// Token: 0x040024F3 RID: 9459
		protected ITypeDefOrRef kind;

		// Token: 0x040024F4 RID: 9460
		protected LazyList<GenericParamConstraint> genericParamConstraints;

		// Token: 0x040024F5 RID: 9461
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040024F6 RID: 9462
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
