using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200085A RID: 2138
	[ComVisible(true)]
	public abstract class TypeDef : ITypeDefOrRef, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IMemberRefParent, IFullName, IType, IOwnerModule, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter, ITokenOperand, IMemberRef, IHasDeclSecurity, ITypeOrMethodDef, IHasCustomDebugInformation, IListListener<FieldDef>, IListListener<MethodDef>, IListListener<TypeDef>, IListListener<EventDef>, IListListener<PropertyDef>, IListListener<GenericParam>, IMemberRefResolver, IMemberDef, IDnlibDef
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x00190DB8 File Offset: 0x00190DB8
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.TypeDef, this.rid);
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x00190DC8 File Offset: 0x00190DC8
		// (set) Token: 0x060050A8 RID: 20648 RVA: 0x00190DD0 File Offset: 0x00190DD0
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

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060050A9 RID: 20649 RVA: 0x00190DDC File Offset: 0x00190DDC
		public int TypeDefOrRefTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x00190DE0 File Offset: 0x00190DE0
		public int HasCustomAttributeTag
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x00190DE4 File Offset: 0x00190DE4
		public int HasDeclSecurityTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x00190DE8 File Offset: 0x00190DE8
		public int MemberRefParentTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x00190DEC File Offset: 0x00190DEC
		public int TypeOrMethodDefTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x00190DF0 File Offset: 0x00190DF0
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				return this.GenericParameters.Count;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x00190E00 File Offset: 0x00190E00
		string IType.TypeName
		{
			get
			{
				return FullNameFactory.Name(this, false, null);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060050B0 RID: 20656 RVA: 0x00190E0C File Offset: 0x00190E0C
		public string ReflectionName
		{
			get
			{
				return FullNameFactory.Name(this, true, null);
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x00190E18 File Offset: 0x00190E18
		string IType.Namespace
		{
			get
			{
				return FullNameFactory.Namespace(this, false, null);
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060050B2 RID: 20658 RVA: 0x00190E24 File Offset: 0x00190E24
		public string ReflectionNamespace
		{
			get
			{
				return FullNameFactory.Namespace(this, true, null);
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x00190E30 File Offset: 0x00190E30
		public string FullName
		{
			get
			{
				return FullNameFactory.FullName(this, false, null, null);
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x00190E3C File Offset: 0x00190E3C
		public string ReflectionFullName
		{
			get
			{
				return FullNameFactory.FullName(this, true, null, null);
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x00190E48 File Offset: 0x00190E48
		public string AssemblyQualifiedName
		{
			get
			{
				return FullNameFactory.AssemblyQualifiedName(this, null, null);
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x00190E54 File Offset: 0x00190E54
		public IAssembly DefinitionAssembly
		{
			get
			{
				return FullNameFactory.DefinitionAssembly(this);
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x00190E5C File Offset: 0x00190E5C
		public IScope Scope
		{
			get
			{
				return this.Module;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x00190E64 File Offset: 0x00190E64
		public ITypeDefOrRef ScopeType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x00190E68 File Offset: 0x00190E68
		public bool ContainsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x00190E6C File Offset: 0x00190E6C
		public ModuleDef Module
		{
			get
			{
				return FullNameFactory.OwnerModule(this);
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00190E74 File Offset: 0x00190E74
		// (set) Token: 0x060050BC RID: 20668 RVA: 0x00190E90 File Offset: 0x00190E90
		internal ModuleDef Module2
		{
			get
			{
				if (!this.module2_isInitialized)
				{
					this.InitializeModule2();
				}
				return this.module2;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.module2 = value;
					this.module2_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x00190ED8 File Offset: 0x00190ED8
		private void InitializeModule2()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.module2_isInitialized)
				{
					this.module2 = this.GetModule2_NoLock();
					this.module2_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x00190F34 File Offset: 0x00190F34
		protected virtual ModuleDef GetModule2_NoLock()
		{
			return null;
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x00190F38 File Offset: 0x00190F38
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060050C0 RID: 20672 RVA: 0x00190F3C File Offset: 0x00190F3C
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x00190F40 File Offset: 0x00190F40
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x00190F44 File Offset: 0x00190F44
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x00190F48 File Offset: 0x00190F48
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x00190F4C File Offset: 0x00190F4C
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x00190F50 File Offset: 0x00190F50
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x00190F54 File Offset: 0x00190F54
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x00190F58 File Offset: 0x00190F58
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x00190F5C File Offset: 0x00190F5C
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060050C9 RID: 20681 RVA: 0x00190F60 File Offset: 0x00190F60
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x060050CA RID: 20682 RVA: 0x00190F64 File Offset: 0x00190F64
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x060050CB RID: 20683 RVA: 0x00190F68 File Offset: 0x00190F68
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x00190F6C File Offset: 0x00190F6C
		// (set) Token: 0x060050CD RID: 20685 RVA: 0x00190F74 File Offset: 0x00190F74
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x060050CE RID: 20686 RVA: 0x00190F80 File Offset: 0x00190F80
		// (set) Token: 0x060050CF RID: 20687 RVA: 0x00190F88 File Offset: 0x00190F88
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

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x060050D0 RID: 20688 RVA: 0x00190F94 File Offset: 0x00190F94
		// (set) Token: 0x060050D1 RID: 20689 RVA: 0x00190F9C File Offset: 0x00190F9C
		public UTF8String Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060050D2 RID: 20690 RVA: 0x00190FA8 File Offset: 0x00190FA8
		// (set) Token: 0x060050D3 RID: 20691 RVA: 0x00190FC4 File Offset: 0x00190FC4
		public ITypeDefOrRef BaseType
		{
			get
			{
				if (!this.baseType_isInitialized)
				{
					this.InitializeBaseType();
				}
				return this.baseType;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.baseType = value;
					this.baseType_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0019100C File Offset: 0x0019100C
		private void InitializeBaseType()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.baseType_isInitialized)
				{
					this.baseType = this.GetBaseType_NoLock();
					this.baseType_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x00191068 File Offset: 0x00191068
		protected virtual ITypeDefOrRef GetBaseType_NoLock()
		{
			return null;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x0019106C File Offset: 0x0019106C
		protected void ResetBaseType()
		{
			this.baseType_isInitialized = false;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x00191078 File Offset: 0x00191078
		public IList<FieldDef> Fields
		{
			get
			{
				if (this.fields == null)
				{
					this.InitializeFields();
				}
				return this.fields;
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00191094 File Offset: 0x00191094
		protected virtual void InitializeFields()
		{
			Interlocked.CompareExchange<LazyList<FieldDef>>(ref this.fields, new LazyList<FieldDef>(this), null);
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060050D9 RID: 20697 RVA: 0x001910AC File Offset: 0x001910AC
		public IList<MethodDef> Methods
		{
			get
			{
				if (this.methods == null)
				{
					this.InitializeMethods();
				}
				return this.methods;
			}
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x001910C8 File Offset: 0x001910C8
		protected virtual void InitializeMethods()
		{
			Interlocked.CompareExchange<LazyList<MethodDef>>(ref this.methods, new LazyList<MethodDef>(this), null);
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x001910E0 File Offset: 0x001910E0
		public IList<GenericParam> GenericParameters
		{
			get
			{
				if (this.genericParameters == null)
				{
					this.InitializeGenericParameters();
				}
				return this.genericParameters;
			}
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x001910FC File Offset: 0x001910FC
		protected virtual void InitializeGenericParameters()
		{
			Interlocked.CompareExchange<LazyList<GenericParam>>(ref this.genericParameters, new LazyList<GenericParam>(this), null);
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00191114 File Offset: 0x00191114
		public IList<InterfaceImpl> Interfaces
		{
			get
			{
				if (this.interfaces == null)
				{
					this.InitializeInterfaces();
				}
				return this.interfaces;
			}
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x00191130 File Offset: 0x00191130
		protected virtual void InitializeInterfaces()
		{
			Interlocked.CompareExchange<IList<InterfaceImpl>>(ref this.interfaces, new List<InterfaceImpl>(), null);
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x00191144 File Offset: 0x00191144
		public IList<DeclSecurity> DeclSecurities
		{
			get
			{
				if (this.declSecurities == null)
				{
					this.InitializeDeclSecurities();
				}
				return this.declSecurities;
			}
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00191160 File Offset: 0x00191160
		protected virtual void InitializeDeclSecurities()
		{
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, new List<DeclSecurity>(), null);
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x00191174 File Offset: 0x00191174
		// (set) Token: 0x060050E2 RID: 20706 RVA: 0x00191190 File Offset: 0x00191190
		public ClassLayout ClassLayout
		{
			get
			{
				if (!this.classLayout_isInitialized)
				{
					this.InitializeClassLayout();
				}
				return this.classLayout;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.classLayout = value;
					this.classLayout_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x001911D8 File Offset: 0x001911D8
		private void InitializeClassLayout()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.classLayout_isInitialized)
				{
					this.classLayout = this.GetClassLayout_NoLock();
					this.classLayout_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x00191234 File Offset: 0x00191234
		private ClassLayout GetOrCreateClassLayout()
		{
			ClassLayout classLayout = this.ClassLayout;
			if (classLayout != null)
			{
				return classLayout;
			}
			Interlocked.CompareExchange<ClassLayout>(ref this.classLayout, new ClassLayoutUser(0, 0U), null);
			return this.classLayout;
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00191270 File Offset: 0x00191270
		protected virtual ClassLayout GetClassLayout_NoLock()
		{
			return null;
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x00191274 File Offset: 0x00191274
		public bool HasDeclSecurities
		{
			get
			{
				return this.DeclSecurities.Count > 0;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060050E7 RID: 20711 RVA: 0x00191284 File Offset: 0x00191284
		// (set) Token: 0x060050E8 RID: 20712 RVA: 0x001912A0 File Offset: 0x001912A0
		public TypeDef DeclaringType
		{
			get
			{
				if (!this.declaringType2_isInitialized)
				{
					this.InitializeDeclaringType2();
				}
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
					typeDef.NestedTypes.Remove(this);
				}
				if (value != null)
				{
					value.NestedTypes.Add(this);
				}
				this.Module2 = null;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x001912EC File Offset: 0x001912EC
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x001912F4 File Offset: 0x001912F4
		// (set) Token: 0x060050EB RID: 20715 RVA: 0x00191310 File Offset: 0x00191310
		public TypeDef DeclaringType2
		{
			get
			{
				if (!this.declaringType2_isInitialized)
				{
					this.InitializeDeclaringType2();
				}
				return this.declaringType2;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.declaringType2 = value;
					this.declaringType2_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x00191358 File Offset: 0x00191358
		private void InitializeDeclaringType2()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.declaringType2_isInitialized)
				{
					this.declaringType2 = this.GetDeclaringType2_NoLock();
					this.declaringType2_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x001913B4 File Offset: 0x001913B4
		protected virtual TypeDef GetDeclaringType2_NoLock()
		{
			return null;
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060050EE RID: 20718 RVA: 0x001913B8 File Offset: 0x001913B8
		public IList<TypeDef> NestedTypes
		{
			get
			{
				if (this.nestedTypes == null)
				{
					this.InitializeNestedTypes();
				}
				return this.nestedTypes;
			}
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x001913D4 File Offset: 0x001913D4
		protected virtual void InitializeNestedTypes()
		{
			Interlocked.CompareExchange<LazyList<TypeDef>>(ref this.nestedTypes, new LazyList<TypeDef>(this), null);
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060050F0 RID: 20720 RVA: 0x001913EC File Offset: 0x001913EC
		public IList<EventDef> Events
		{
			get
			{
				if (this.events == null)
				{
					this.InitializeEvents();
				}
				return this.events;
			}
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00191408 File Offset: 0x00191408
		protected virtual void InitializeEvents()
		{
			Interlocked.CompareExchange<LazyList<EventDef>>(ref this.events, new LazyList<EventDef>(this), null);
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x00191420 File Offset: 0x00191420
		public IList<PropertyDef> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.InitializeProperties();
				}
				return this.properties;
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0019143C File Offset: 0x0019143C
		protected virtual void InitializeProperties()
		{
			Interlocked.CompareExchange<LazyList<PropertyDef>>(ref this.properties, new LazyList<PropertyDef>(this), null);
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060050F4 RID: 20724 RVA: 0x00191454 File Offset: 0x00191454
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

		// Token: 0x060050F5 RID: 20725 RVA: 0x00191470 File Offset: 0x00191470
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x00191484 File Offset: 0x00191484
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x00191494 File Offset: 0x00191494
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x00191498 File Offset: 0x00191498
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060050F9 RID: 20729 RVA: 0x001914A8 File Offset: 0x001914A8
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

		// Token: 0x060050FA RID: 20730 RVA: 0x001914C4 File Offset: 0x001914C4
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x001914D8 File Offset: 0x001914D8
		public bool HasFields
		{
			get
			{
				return this.Fields.Count > 0;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x001914E8 File Offset: 0x001914E8
		public bool HasMethods
		{
			get
			{
				return this.Methods.Count > 0;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x001914F8 File Offset: 0x001914F8
		public bool HasGenericParameters
		{
			get
			{
				return this.GenericParameters.Count > 0;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00191508 File Offset: 0x00191508
		public bool HasEvents
		{
			get
			{
				return this.Events.Count > 0;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x00191518 File Offset: 0x00191518
		public bool HasProperties
		{
			get
			{
				return this.Properties.Count > 0;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x00191528 File Offset: 0x00191528
		public bool HasNestedTypes
		{
			get
			{
				return this.NestedTypes.Count > 0;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x00191538 File Offset: 0x00191538
		public bool HasInterfaces
		{
			get
			{
				return this.Interfaces.Count > 0;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x00191548 File Offset: 0x00191548
		public bool HasClassLayout
		{
			get
			{
				return this.ClassLayout != null;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x00191558 File Offset: 0x00191558
		// (set) Token: 0x06005104 RID: 20740 RVA: 0x00191584 File Offset: 0x00191584
		public ushort PackingSize
		{
			get
			{
				ClassLayout classLayout = this.ClassLayout;
				if (classLayout != null)
				{
					return classLayout.PackingSize;
				}
				return ushort.MaxValue;
			}
			set
			{
				this.GetOrCreateClassLayout().PackingSize = value;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x00191594 File Offset: 0x00191594
		// (set) Token: 0x06005106 RID: 20742 RVA: 0x001915BC File Offset: 0x001915BC
		public uint ClassSize
		{
			get
			{
				ClassLayout classLayout = this.ClassLayout;
				if (classLayout != null)
				{
					return classLayout.ClassSize;
				}
				return uint.MaxValue;
			}
			set
			{
				this.GetOrCreateClassLayout().ClassSize = value;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x001915CC File Offset: 0x001915CC
		public bool IsValueType
		{
			get
			{
				if ((this.Attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					return false;
				}
				ITypeDefOrRef typeDefOrRef = this.BaseType;
				if (typeDefOrRef == null)
				{
					return false;
				}
				if (!typeDefOrRef.DefinitionAssembly.IsCorLib())
				{
					return false;
				}
				TypeRef typeRef = typeDefOrRef as TypeRef;
				UTF8String left;
				UTF8String left2;
				if (typeRef != null)
				{
					left = typeRef.Name;
					left2 = typeRef.Namespace;
				}
				else
				{
					TypeDef typeDef = typeDefOrRef as TypeDef;
					if (typeDef == null)
					{
						return false;
					}
					left = typeDef.Name;
					left2 = typeDef.Namespace;
				}
				return !(left2 != TypeDef.systemString) && (!(left != TypeDef.valueTypeString) || !(left != TypeDef.enumString)) && (!this.DefinitionAssembly.IsCorLib() || !(this.Name == TypeDef.enumString) || !(this.Namespace == TypeDef.systemString));
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005108 RID: 20744 RVA: 0x001916C0 File Offset: 0x001916C0
		public bool IsEnum
		{
			get
			{
				if ((this.Attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					return false;
				}
				ITypeDefOrRef typeDefOrRef = this.BaseType;
				if (typeDefOrRef == null)
				{
					return false;
				}
				if (!typeDefOrRef.DefinitionAssembly.IsCorLib())
				{
					return false;
				}
				TypeRef typeRef = typeDefOrRef as TypeRef;
				if (typeRef != null)
				{
					return typeRef.Namespace == TypeDef.systemString && typeRef.Name == TypeDef.enumString;
				}
				TypeDef typeDef = typeDefOrRef as TypeDef;
				return typeDef != null && typeDef.Namespace == TypeDef.systemString && typeDef.Name == TypeDef.enumString;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x00191770 File Offset: 0x00191770
		public bool IsDelegate
		{
			get
			{
				if ((this.Attributes & (TypeAttributes.ClassSemanticsMask | TypeAttributes.Abstract)) != TypeAttributes.NotPublic)
				{
					return false;
				}
				ITypeDefOrRef typeDefOrRef = this.BaseType;
				if (typeDefOrRef == null)
				{
					return false;
				}
				if (!typeDefOrRef.DefinitionAssembly.IsCorLib())
				{
					return false;
				}
				TypeRef typeRef = typeDefOrRef as TypeRef;
				if (typeRef != null)
				{
					return typeRef.Namespace == TypeDef.systemString && typeRef.Name == TypeDef.multicastDelegateString;
				}
				TypeDef typeDef = typeDefOrRef as TypeDef;
				return typeDef != null && typeDef.Namespace == TypeDef.systemString && typeDef.Name == TypeDef.multicastDelegateString;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x00191820 File Offset: 0x00191820
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x00191830 File Offset: 0x00191830
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitive();
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x00191838 File Offset: 0x00191838
		public bool IsEquivalent
		{
			get
			{
				return TIAHelper.IsTypeDefEquivalent(this);
			}
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00191840 File Offset: 0x00191840
		private void ModifyAttributes(TypeAttributes andMask, TypeAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00191854 File Offset: 0x00191854
		private void ModifyAttributes(bool set, TypeAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600510F RID: 20751 RVA: 0x0019187C File Offset: 0x0019187C
		// (set) Token: 0x06005110 RID: 20752 RVA: 0x00191888 File Offset: 0x00191888
		public TypeAttributes Visibility
		{
			get
			{
				return (TypeAttributes)(this.attributes & 7);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.VisibilityMask, value & TypeAttributes.VisibilityMask);
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x00191898 File Offset: 0x00191898
		public bool IsNotPublic
		{
			get
			{
				return (this.attributes & 7) == 0;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x001918A8 File Offset: 0x001918A8
		public bool IsPublic
		{
			get
			{
				return (this.attributes & 7) == 1;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x001918B8 File Offset: 0x001918B8
		public bool IsNestedPublic
		{
			get
			{
				return (this.attributes & 7) == 2;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x001918C8 File Offset: 0x001918C8
		public bool IsNestedPrivate
		{
			get
			{
				return (this.attributes & 7) == 3;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x001918D8 File Offset: 0x001918D8
		public bool IsNestedFamily
		{
			get
			{
				return (this.attributes & 7) == 4;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x001918E8 File Offset: 0x001918E8
		public bool IsNestedAssembly
		{
			get
			{
				return (this.attributes & 7) == 5;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x001918F8 File Offset: 0x001918F8
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return (this.attributes & 7) == 6;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x00191908 File Offset: 0x00191908
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return (this.attributes & 7) == 7;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x00191918 File Offset: 0x00191918
		// (set) Token: 0x0600511A RID: 20762 RVA: 0x00191924 File Offset: 0x00191924
		public TypeAttributes Layout
		{
			get
			{
				return (TypeAttributes)(this.attributes & 24);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.LayoutMask, value & TypeAttributes.LayoutMask);
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x00191934 File Offset: 0x00191934
		public bool IsAutoLayout
		{
			get
			{
				return (this.attributes & 24) == 0;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x00191944 File Offset: 0x00191944
		public bool IsSequentialLayout
		{
			get
			{
				return (this.attributes & 24) == 8;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x00191954 File Offset: 0x00191954
		public bool IsExplicitLayout
		{
			get
			{
				return (this.attributes & 24) == 16;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x00191964 File Offset: 0x00191964
		// (set) Token: 0x0600511F RID: 20767 RVA: 0x00191974 File Offset: 0x00191974
		public bool IsInterface
		{
			get
			{
				return (this.attributes & 32) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.ClassSemanticsMask);
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06005120 RID: 20768 RVA: 0x00191980 File Offset: 0x00191980
		// (set) Token: 0x06005121 RID: 20769 RVA: 0x00191990 File Offset: 0x00191990
		public bool IsClass
		{
			get
			{
				return (this.attributes & 32) == 0;
			}
			set
			{
				this.ModifyAttributes(!value, TypeAttributes.ClassSemanticsMask);
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x001919A0 File Offset: 0x001919A0
		// (set) Token: 0x06005123 RID: 20771 RVA: 0x001919B4 File Offset: 0x001919B4
		public bool IsAbstract
		{
			get
			{
				return (this.attributes & 128) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Abstract);
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x001919C4 File Offset: 0x001919C4
		// (set) Token: 0x06005125 RID: 20773 RVA: 0x001919D8 File Offset: 0x001919D8
		public bool IsSealed
		{
			get
			{
				return (this.attributes & 256) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Sealed);
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x001919E8 File Offset: 0x001919E8
		// (set) Token: 0x06005127 RID: 20775 RVA: 0x001919FC File Offset: 0x001919FC
		public bool IsSpecialName
		{
			get
			{
				return (this.attributes & 1024) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.SpecialName);
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x00191A0C File Offset: 0x00191A0C
		// (set) Token: 0x06005129 RID: 20777 RVA: 0x00191A20 File Offset: 0x00191A20
		public bool IsImport
		{
			get
			{
				return (this.attributes & 4096) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Import);
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x00191A30 File Offset: 0x00191A30
		// (set) Token: 0x0600512B RID: 20779 RVA: 0x00191A44 File Offset: 0x00191A44
		public bool IsSerializable
		{
			get
			{
				return (this.attributes & 8192) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Serializable);
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x00191A54 File Offset: 0x00191A54
		// (set) Token: 0x0600512D RID: 20781 RVA: 0x00191A68 File Offset: 0x00191A68
		public bool IsWindowsRuntime
		{
			get
			{
				return (this.attributes & 16384) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.WindowsRuntime);
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x00191A78 File Offset: 0x00191A78
		// (set) Token: 0x0600512F RID: 20783 RVA: 0x00191A88 File Offset: 0x00191A88
		public TypeAttributes StringFormat
		{
			get
			{
				return (TypeAttributes)(this.attributes & 196608);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.StringFormatMask, value & TypeAttributes.StringFormatMask);
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005130 RID: 20784 RVA: 0x00191A9C File Offset: 0x00191A9C
		public bool IsAnsiClass
		{
			get
			{
				return (this.attributes & 196608) == 0;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x00191AB0 File Offset: 0x00191AB0
		public bool IsUnicodeClass
		{
			get
			{
				return (this.attributes & 196608) == 65536;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06005132 RID: 20786 RVA: 0x00191AC8 File Offset: 0x00191AC8
		public bool IsAutoClass
		{
			get
			{
				return (this.attributes & 196608) == 131072;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x00191AE0 File Offset: 0x00191AE0
		public bool IsCustomFormatClass
		{
			get
			{
				return (this.attributes & 196608) == 196608;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x00191AF8 File Offset: 0x00191AF8
		// (set) Token: 0x06005135 RID: 20789 RVA: 0x00191B0C File Offset: 0x00191B0C
		public bool IsBeforeFieldInit
		{
			get
			{
				return (this.attributes & 1048576) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.BeforeFieldInit);
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06005136 RID: 20790 RVA: 0x00191B1C File Offset: 0x00191B1C
		// (set) Token: 0x06005137 RID: 20791 RVA: 0x00191B30 File Offset: 0x00191B30
		public bool IsForwarder
		{
			get
			{
				return (this.attributes & 2097152) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Forwarder);
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x00191B40 File Offset: 0x00191B40
		// (set) Token: 0x06005139 RID: 20793 RVA: 0x00191B54 File Offset: 0x00191B54
		public bool IsRuntimeSpecialName
		{
			get
			{
				return (this.attributes & 2048) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.RTSpecialName);
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600513A RID: 20794 RVA: 0x00191B64 File Offset: 0x00191B64
		// (set) Token: 0x0600513B RID: 20795 RVA: 0x00191B78 File Offset: 0x00191B78
		public bool HasSecurity
		{
			get
			{
				return (this.attributes & 262144) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.HasSecurity);
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x00191B88 File Offset: 0x00191B88
		public bool IsGlobalModuleType
		{
			get
			{
				ModuleDef module = this.Module;
				return module != null && module.GlobalType == this;
			}
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x00191BB4 File Offset: 0x00191BB4
		public IEnumerable<TypeDef> GetTypes()
		{
			return AllTypesHelper.Types(this.NestedTypes);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x00191BC4 File Offset: 0x00191BC4
		public TypeSig GetEnumUnderlyingType()
		{
			IList<FieldDef> list = this.Fields;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				FieldDef fieldDef = list[i];
				if (!fieldDef.IsLiteral && !fieldDef.IsStatic)
				{
					FieldSig fieldSig = fieldDef.FieldSig;
					if (fieldSig != null)
					{
						return fieldSig.Type;
					}
				}
			}
			return null;
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x00191C28 File Offset: 0x00191C28
		public IMemberForwarded Resolve(MemberRef memberRef)
		{
			return this.Resolve(memberRef, (SigComparerOptions)0U);
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x00191C34 File Offset: 0x00191C34
		public IMemberForwarded Resolve(MemberRef memberRef, SigComparerOptions options)
		{
			if (memberRef == null)
			{
				return null;
			}
			MethodSig methodSig = memberRef.MethodSig;
			if (methodSig != null)
			{
				return this.FindMethodCheckBaseType(memberRef.Name, methodSig, options, memberRef.Module);
			}
			FieldSig fieldSig = memberRef.FieldSig;
			if (fieldSig != null)
			{
				return this.FindFieldCheckBaseType(memberRef.Name, fieldSig, options, memberRef.Module);
			}
			return null;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x00191C94 File Offset: 0x00191C94
		public MethodDef FindMethod(UTF8String name, MethodSig sig)
		{
			return this.FindMethod(name, sig, (SigComparerOptions)0U, null);
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x00191CA0 File Offset: 0x00191CA0
		public MethodDef FindMethod(UTF8String name, MethodSig sig, SigComparerOptions options)
		{
			return this.FindMethod(name, sig, options, null);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00191CAC File Offset: 0x00191CAC
		public MethodDef FindMethod(UTF8String name, MethodSig sig, SigComparerOptions options, ModuleDef sourceModule)
		{
			if (UTF8String.IsNull(name) || sig == null)
			{
				return null;
			}
			SigComparer sigComparer = new SigComparer(options, sourceModule);
			bool flag = (options & SigComparerOptions.PrivateScopeMethodIsComparable) > (SigComparerOptions)0U;
			IList<MethodDef> list = this.Methods;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MethodDef methodDef = list[i];
				if ((flag || !methodDef.IsPrivateScope || sourceModule == this.Module) && UTF8String.Equals(methodDef.Name, name) && sigComparer.Equals(methodDef.MethodSig, sig))
				{
					return methodDef;
				}
			}
			return null;
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x00191D5C File Offset: 0x00191D5C
		public MethodDef FindMethod(UTF8String name)
		{
			IList<MethodDef> list = this.Methods;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MethodDef methodDef = list[i];
				if (UTF8String.Equals(methodDef.Name, name))
				{
					return methodDef;
				}
			}
			return null;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x00191DA8 File Offset: 0x00191DA8
		public IEnumerable<MethodDef> FindMethods(UTF8String name)
		{
			IList<MethodDef> methods = this.Methods;
			int count = methods.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				MethodDef methodDef = methods[i];
				if (UTF8String.Equals(methodDef.Name, name))
				{
					yield return methodDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x00191DC0 File Offset: 0x00191DC0
		public MethodDef FindStaticConstructor()
		{
			IList<MethodDef> list = this.Methods;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MethodDef methodDef = list[i];
				if (methodDef.IsStaticConstructor)
				{
					return methodDef;
				}
			}
			return null;
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00191E04 File Offset: 0x00191E04
		public MethodDef FindOrCreateStaticConstructor()
		{
			MethodDef methodDef = this.FindStaticConstructor();
			if (methodDef != null)
			{
				return methodDef;
			}
			MethodImplAttributes implFlags = MethodImplAttributes.IL;
			MethodAttributes flags = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			ModuleDef module = this.Module;
			methodDef = module.UpdateRowId<MethodDefUser>(new MethodDefUser(MethodDef.StaticConstructorName, MethodSig.CreateStatic(module.CorLibTypes.Void), implFlags, flags));
			methodDef.Body = new CilBody
			{
				InitLocals = true,
				MaxStack = 8,
				Instructions = 
				{
					OpCodes.Ret.ToInstruction()
				}
			};
			this.Methods.Add(methodDef);
			return methodDef;
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00191E98 File Offset: 0x00191E98
		public IEnumerable<MethodDef> FindInstanceConstructors()
		{
			IList<MethodDef> methods = this.Methods;
			int count = methods.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				MethodDef methodDef = methods[i];
				if (methodDef.IsInstanceConstructor)
				{
					yield return methodDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x00191EA8 File Offset: 0x00191EA8
		public IEnumerable<MethodDef> FindConstructors()
		{
			IList<MethodDef> methods = this.Methods;
			int count = methods.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				MethodDef methodDef = methods[i];
				if (methodDef.IsConstructor)
				{
					yield return methodDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x00191EB8 File Offset: 0x00191EB8
		public MethodDef FindDefaultConstructor()
		{
			IList<MethodDef> list = this.Methods;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MethodDef methodDef = list[i];
				if (methodDef.IsInstanceConstructor)
				{
					MethodSig methodSig = methodDef.MethodSig;
					if (methodSig != null && methodSig.Params.Count == 0)
					{
						return methodDef;
					}
				}
			}
			return null;
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x00191F1C File Offset: 0x00191F1C
		public FieldDef FindField(UTF8String name, FieldSig sig)
		{
			return this.FindField(name, sig, (SigComparerOptions)0U, null);
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00191F28 File Offset: 0x00191F28
		public FieldDef FindField(UTF8String name, FieldSig sig, SigComparerOptions options)
		{
			return this.FindField(name, sig, options, null);
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00191F34 File Offset: 0x00191F34
		public FieldDef FindField(UTF8String name, FieldSig sig, SigComparerOptions options, ModuleDef sourceModule)
		{
			if (UTF8String.IsNull(name) || sig == null)
			{
				return null;
			}
			SigComparer sigComparer = new SigComparer(options, sourceModule);
			bool flag = (options & SigComparerOptions.PrivateScopeFieldIsComparable) > (SigComparerOptions)0U;
			IList<FieldDef> list = this.Fields;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				FieldDef fieldDef = list[i];
				if ((flag || !fieldDef.IsPrivateScope || sourceModule == this.Module) && UTF8String.Equals(fieldDef.Name, name) && sigComparer.Equals(fieldDef.FieldSig, sig))
				{
					return fieldDef;
				}
			}
			return null;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00191FE4 File Offset: 0x00191FE4
		public FieldDef FindField(UTF8String name)
		{
			IList<FieldDef> list = this.Fields;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				FieldDef fieldDef = list[i];
				if (UTF8String.Equals(fieldDef.Name, name))
				{
					return fieldDef;
				}
			}
			return null;
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00192030 File Offset: 0x00192030
		public IEnumerable<FieldDef> FindFields(UTF8String name)
		{
			IList<FieldDef> fields = this.Fields;
			int count = fields.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				FieldDef fieldDef = fields[i];
				if (UTF8String.Equals(fieldDef.Name, name))
				{
					yield return fieldDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x00192048 File Offset: 0x00192048
		public EventDef FindEvent(UTF8String name, IType type)
		{
			return this.FindEvent(name, type, (SigComparerOptions)0U, null);
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x00192054 File Offset: 0x00192054
		public EventDef FindEvent(UTF8String name, IType type, SigComparerOptions options)
		{
			return this.FindEvent(name, type, options, null);
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00192060 File Offset: 0x00192060
		public EventDef FindEvent(UTF8String name, IType type, SigComparerOptions options, ModuleDef sourceModule)
		{
			if (UTF8String.IsNull(name) || type == null)
			{
				return null;
			}
			SigComparer sigComparer = new SigComparer(options, sourceModule);
			IList<EventDef> list = this.Events;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				EventDef eventDef = list[i];
				if (UTF8String.Equals(eventDef.Name, name) && sigComparer.Equals(eventDef.EventType, type))
				{
					return eventDef;
				}
			}
			return null;
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x001920E0 File Offset: 0x001920E0
		public EventDef FindEvent(UTF8String name)
		{
			IList<EventDef> list = this.Events;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				EventDef eventDef = list[i];
				if (UTF8String.Equals(eventDef.Name, name))
				{
					return eventDef;
				}
			}
			return null;
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0019212C File Offset: 0x0019212C
		public IEnumerable<EventDef> FindEvents(UTF8String name)
		{
			IList<EventDef> events = this.Events;
			int count = events.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				EventDef eventDef = events[i];
				if (UTF8String.Equals(eventDef.Name, name))
				{
					yield return eventDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x00192144 File Offset: 0x00192144
		public PropertyDef FindProperty(UTF8String name, CallingConventionSig propSig)
		{
			return this.FindProperty(name, propSig, (SigComparerOptions)0U, null);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00192150 File Offset: 0x00192150
		public PropertyDef FindProperty(UTF8String name, CallingConventionSig propSig, SigComparerOptions options)
		{
			return this.FindProperty(name, propSig, options, null);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0019215C File Offset: 0x0019215C
		public PropertyDef FindProperty(UTF8String name, CallingConventionSig propSig, SigComparerOptions options, ModuleDef sourceModule)
		{
			if (UTF8String.IsNull(name) || propSig == null)
			{
				return null;
			}
			SigComparer sigComparer = new SigComparer(options, sourceModule);
			IList<PropertyDef> list = this.Properties;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				PropertyDef propertyDef = list[i];
				if (UTF8String.Equals(propertyDef.Name, name) && sigComparer.Equals(propertyDef.Type, propSig))
				{
					return propertyDef;
				}
			}
			return null;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x001921DC File Offset: 0x001921DC
		public PropertyDef FindProperty(UTF8String name)
		{
			IList<PropertyDef> list = this.Properties;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				PropertyDef propertyDef = list[i];
				if (UTF8String.Equals(propertyDef.Name, name))
				{
					return propertyDef;
				}
			}
			return null;
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x00192228 File Offset: 0x00192228
		public IEnumerable<PropertyDef> FindProperties(UTF8String name)
		{
			IList<PropertyDef> properties = this.Properties;
			int count = properties.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				PropertyDef propertyDef = properties[i];
				if (UTF8String.Equals(propertyDef.Name, name))
				{
					yield return propertyDef;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x00192240 File Offset: 0x00192240
		public MethodDef FindMethodCheckBaseType(UTF8String name, MethodSig sig)
		{
			return this.FindMethodCheckBaseType(name, sig, (SigComparerOptions)0U, null);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x0019224C File Offset: 0x0019224C
		public MethodDef FindMethodCheckBaseType(UTF8String name, MethodSig sig, SigComparerOptions options)
		{
			return this.FindMethodCheckBaseType(name, sig, options, null);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x00192258 File Offset: 0x00192258
		public MethodDef FindMethodCheckBaseType(UTF8String name, MethodSig sig, SigComparerOptions options, ModuleDef sourceModule)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				MethodDef methodDef = typeDef.FindMethod(name, sig, options, sourceModule);
				if (methodDef != null)
				{
					return methodDef;
				}
			}
			return null;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00192294 File Offset: 0x00192294
		public MethodDef FindMethodCheckBaseType(UTF8String name)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				MethodDef methodDef = typeDef.FindMethod(name);
				if (methodDef != null)
				{
					return methodDef;
				}
			}
			return null;
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x001922CC File Offset: 0x001922CC
		public FieldDef FindFieldCheckBaseType(UTF8String name, FieldSig sig)
		{
			return this.FindFieldCheckBaseType(name, sig, (SigComparerOptions)0U, null);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x001922D8 File Offset: 0x001922D8
		public FieldDef FindFieldCheckBaseType(UTF8String name, FieldSig sig, SigComparerOptions options)
		{
			return this.FindFieldCheckBaseType(name, sig, options, null);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x001922E4 File Offset: 0x001922E4
		public FieldDef FindFieldCheckBaseType(UTF8String name, FieldSig sig, SigComparerOptions options, ModuleDef sourceModule)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				FieldDef fieldDef = typeDef.FindField(name, sig, options, sourceModule);
				if (fieldDef != null)
				{
					return fieldDef;
				}
			}
			return null;
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x00192320 File Offset: 0x00192320
		public FieldDef FindFieldCheckBaseType(UTF8String name)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				FieldDef fieldDef = typeDef.FindField(name);
				if (fieldDef != null)
				{
					return fieldDef;
				}
			}
			return null;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00192358 File Offset: 0x00192358
		public EventDef FindEventCheckBaseType(UTF8String name, ITypeDefOrRef eventType)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				EventDef eventDef = typeDef.FindEvent(name, eventType);
				if (eventDef != null)
				{
					return eventDef;
				}
			}
			return null;
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x00192394 File Offset: 0x00192394
		public EventDef FindEventCheckBaseType(UTF8String name)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				EventDef eventDef = typeDef.FindEvent(name);
				if (eventDef != null)
				{
					return eventDef;
				}
			}
			return null;
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x001923CC File Offset: 0x001923CC
		public PropertyDef FindPropertyCheckBaseType(UTF8String name, PropertySig sig)
		{
			return this.FindPropertyCheckBaseType(name, sig, (SigComparerOptions)0U, null);
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x001923D8 File Offset: 0x001923D8
		public PropertyDef FindPropertyCheckBaseType(UTF8String name, PropertySig sig, SigComparerOptions options)
		{
			return this.FindPropertyCheckBaseType(name, sig, options, null);
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x001923E4 File Offset: 0x001923E4
		public PropertyDef FindPropertyCheckBaseType(UTF8String name, PropertySig sig, SigComparerOptions options, ModuleDef sourceModule)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				PropertyDef propertyDef = typeDef.FindProperty(name, sig, options, sourceModule);
				if (propertyDef != null)
				{
					return propertyDef;
				}
			}
			return null;
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00192420 File Offset: 0x00192420
		public PropertyDef FindPropertyCheckBaseType(UTF8String name)
		{
			for (TypeDef typeDef = this; typeDef != null; typeDef = typeDef.BaseType.ResolveTypeDef())
			{
				PropertyDef propertyDef = typeDef.FindProperty(name);
				if (propertyDef != null)
				{
					return propertyDef;
				}
			}
			return null;
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00192458 File Offset: 0x00192458
		public void Remove(MethodDef method)
		{
			this.Remove(method, false);
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00192464 File Offset: 0x00192464
		public void Remove(MethodDef method, bool removeEmptyPropertiesEvents)
		{
			if (method == null)
			{
				return;
			}
			IList<PropertyDef> list = this.Properties;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				PropertyDef propertyDef = list[i];
				propertyDef.GetMethods.Remove(method);
				propertyDef.SetMethods.Remove(method);
				propertyDef.OtherMethods.Remove(method);
			}
			IList<EventDef> list2 = this.Events;
			count = list2.Count;
			for (int j = 0; j < count; j++)
			{
				EventDef eventDef = list2[j];
				if (eventDef.AddMethod == method)
				{
					eventDef.AddMethod = null;
				}
				if (eventDef.RemoveMethod == method)
				{
					eventDef.RemoveMethod = null;
				}
				if (eventDef.InvokeMethod == method)
				{
					eventDef.InvokeMethod = null;
				}
				eventDef.OtherMethods.Remove(method);
			}
			if (removeEmptyPropertiesEvents)
			{
				this.RemoveEmptyProperties();
				this.RemoveEmptyEvents();
			}
			this.Methods.Remove(method);
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x00192560 File Offset: 0x00192560
		private void RemoveEmptyProperties()
		{
			IList<PropertyDef> list = this.Properties;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].IsEmpty)
				{
					list.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x001925A8 File Offset: 0x001925A8
		private void RemoveEmptyEvents()
		{
			IList<EventDef> list = this.Events;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].IsEmpty)
				{
					list.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x001925F0 File Offset: 0x001925F0
		void IListListener<FieldDef>.OnLazyAdd(int index, ref FieldDef value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x001925FC File Offset: 0x001925FC
		internal virtual void OnLazyAdd2(int index, ref FieldDef value)
		{
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00192600 File Offset: 0x00192600
		void IListListener<FieldDef>.OnAdd(int index, FieldDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Field is already owned by another type. Set DeclaringType to null first.");
			}
			value.DeclaringType2 = this;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x00192620 File Offset: 0x00192620
		void IListListener<FieldDef>.OnRemove(int index, FieldDef value)
		{
			value.DeclaringType2 = null;
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0019262C File Offset: 0x0019262C
		void IListListener<FieldDef>.OnResize(int index)
		{
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00192630 File Offset: 0x00192630
		void IListListener<FieldDef>.OnClear()
		{
			foreach (FieldDef fieldDef in this.fields.GetEnumerable_NoLock())
			{
				fieldDef.DeclaringType2 = null;
			}
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0019268C File Offset: 0x0019268C
		void IListListener<MethodDef>.OnLazyAdd(int index, ref MethodDef value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00192698 File Offset: 0x00192698
		internal virtual void OnLazyAdd2(int index, ref MethodDef value)
		{
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0019269C File Offset: 0x0019269C
		void IListListener<MethodDef>.OnAdd(int index, MethodDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Method is already owned by another type. Set DeclaringType to null first.");
			}
			value.DeclaringType2 = this;
			value.Parameters.UpdateThisParameterType(this);
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x001926C8 File Offset: 0x001926C8
		void IListListener<MethodDef>.OnRemove(int index, MethodDef value)
		{
			value.DeclaringType2 = null;
			value.Parameters.UpdateThisParameterType(null);
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x001926E0 File Offset: 0x001926E0
		void IListListener<MethodDef>.OnResize(int index)
		{
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x001926E4 File Offset: 0x001926E4
		void IListListener<MethodDef>.OnClear()
		{
			foreach (MethodDef methodDef in this.methods.GetEnumerable_NoLock())
			{
				methodDef.DeclaringType2 = null;
				methodDef.Parameters.UpdateThisParameterType(null);
			}
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0019274C File Offset: 0x0019274C
		void IListListener<TypeDef>.OnLazyAdd(int index, ref TypeDef value)
		{
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x00192750 File Offset: 0x00192750
		void IListListener<TypeDef>.OnAdd(int index, TypeDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Nested type is already owned by another type. Set DeclaringType to null first.");
			}
			if (value.Module != null)
			{
				throw new InvalidOperationException("Type is already owned by another module. Remove it from that module's type list.");
			}
			value.DeclaringType2 = this;
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x00192788 File Offset: 0x00192788
		void IListListener<TypeDef>.OnRemove(int index, TypeDef value)
		{
			value.DeclaringType2 = null;
			value.Module2 = null;
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x00192798 File Offset: 0x00192798
		void IListListener<TypeDef>.OnResize(int index)
		{
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0019279C File Offset: 0x0019279C
		void IListListener<TypeDef>.OnClear()
		{
			foreach (TypeDef typeDef in this.nestedTypes.GetEnumerable_NoLock())
			{
				typeDef.DeclaringType2 = null;
			}
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x001927F8 File Offset: 0x001927F8
		void IListListener<EventDef>.OnLazyAdd(int index, ref EventDef value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x00192804 File Offset: 0x00192804
		internal virtual void OnLazyAdd2(int index, ref EventDef value)
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00192808 File Offset: 0x00192808
		void IListListener<EventDef>.OnAdd(int index, EventDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Event is already owned by another type. Set DeclaringType to null first.");
			}
			value.DeclaringType2 = this;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00192828 File Offset: 0x00192828
		void IListListener<EventDef>.OnRemove(int index, EventDef value)
		{
			value.DeclaringType2 = null;
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00192834 File Offset: 0x00192834
		void IListListener<EventDef>.OnResize(int index)
		{
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x00192838 File Offset: 0x00192838
		void IListListener<EventDef>.OnClear()
		{
			foreach (EventDef eventDef in this.events.GetEnumerable_NoLock())
			{
				eventDef.DeclaringType2 = null;
			}
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00192894 File Offset: 0x00192894
		void IListListener<PropertyDef>.OnLazyAdd(int index, ref PropertyDef value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x001928A0 File Offset: 0x001928A0
		internal virtual void OnLazyAdd2(int index, ref PropertyDef value)
		{
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x001928A4 File Offset: 0x001928A4
		void IListListener<PropertyDef>.OnAdd(int index, PropertyDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Property is already owned by another type. Set DeclaringType to null first.");
			}
			value.DeclaringType2 = this;
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x001928C4 File Offset: 0x001928C4
		void IListListener<PropertyDef>.OnRemove(int index, PropertyDef value)
		{
			value.DeclaringType2 = null;
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x001928D0 File Offset: 0x001928D0
		void IListListener<PropertyDef>.OnResize(int index)
		{
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x001928D4 File Offset: 0x001928D4
		void IListListener<PropertyDef>.OnClear()
		{
			foreach (PropertyDef propertyDef in this.properties.GetEnumerable_NoLock())
			{
				propertyDef.DeclaringType2 = null;
			}
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00192930 File Offset: 0x00192930
		void IListListener<GenericParam>.OnLazyAdd(int index, ref GenericParam value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0019293C File Offset: 0x0019293C
		internal virtual void OnLazyAdd2(int index, ref GenericParam value)
		{
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00192940 File Offset: 0x00192940
		void IListListener<GenericParam>.OnAdd(int index, GenericParam value)
		{
			if (value.Owner != null)
			{
				throw new InvalidOperationException("Generic param is already owned by another type/method. Set Owner to null first.");
			}
			value.Owner = this;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00192960 File Offset: 0x00192960
		void IListListener<GenericParam>.OnRemove(int index, GenericParam value)
		{
			value.Owner = null;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0019296C File Offset: 0x0019296C
		void IListListener<GenericParam>.OnResize(int index)
		{
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00192970 File Offset: 0x00192970
		void IListListener<GenericParam>.OnClear()
		{
			foreach (GenericParam genericParam in this.genericParameters.GetEnumerable_NoLock())
			{
				genericParam.Owner = null;
			}
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x001929CC File Offset: 0x001929CC
		public IList<FieldDef> GetFields(UTF8String name)
		{
			List<FieldDef> list = new List<FieldDef>();
			IList<FieldDef> list2 = this.Fields;
			int count = list2.Count;
			for (int i = 0; i < count; i++)
			{
				FieldDef fieldDef = list2[i];
				if (fieldDef.Name == name)
				{
					list.Add(fieldDef);
				}
			}
			return list;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00192A24 File Offset: 0x00192A24
		public FieldDef GetField(UTF8String name)
		{
			IList<FieldDef> list = this.Fields;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				FieldDef fieldDef = list[i];
				if (fieldDef.Name == name)
				{
					return fieldDef;
				}
			}
			return null;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00192A70 File Offset: 0x00192A70
		internal static bool GetClassSize(TypeDef td, out uint size)
		{
			size = 0U;
			if (td == null)
			{
				return false;
			}
			if (!td.IsValueType)
			{
				return false;
			}
			if (!td.IsSequentialLayout && !td.IsExplicitLayout)
			{
				if (td.Fields.Count != 1)
				{
					return false;
				}
				FieldDef fieldDef = td.Fields[0];
				return fieldDef != null && fieldDef.GetFieldSize(out size);
			}
			else
			{
				ClassLayout classLayout = td.ClassLayout;
				if (classLayout == null)
				{
					return false;
				}
				uint classSize = classLayout.ClassSize;
				if (classSize != 0U)
				{
					size = classSize;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x00192B04 File Offset: 0x00192B04
		protected MethodDef FindMethodImplMethod(IMethodDefOrRef mdr)
		{
			MethodDef methodDef = mdr as MethodDef;
			if (methodDef != null)
			{
				return methodDef;
			}
			MemberRef memberRef = mdr as MemberRef;
			if (memberRef == null)
			{
				return null;
			}
			IMemberRefParent memberRefParent = memberRef.Class;
			methodDef = (memberRefParent as MethodDef);
			if (methodDef != null)
			{
				return methodDef;
			}
			for (int i = 0; i < 10; i++)
			{
				TypeSpec typeSpec = memberRefParent as TypeSpec;
				if (typeSpec == null)
				{
					break;
				}
				GenericInstSig genericInstSig = typeSpec.TypeSig as GenericInstSig;
				if (genericInstSig == null || genericInstSig.GenericType == null)
				{
					return null;
				}
				memberRefParent = genericInstSig.GenericType.TypeDefOrRef;
			}
			TypeDef typeDef = memberRefParent as TypeDef;
			if (typeDef == null)
			{
				TypeRef typeRef = memberRefParent as TypeRef;
				if (typeRef != null && this.Module != null)
				{
					typeDef = this.Module.Find(typeRef);
				}
			}
			if (typeDef == null)
			{
				return null;
			}
			return typeDef.FindMethod(memberRef.Name, memberRef.MethodSig);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x00192BEC File Offset: 0x00192BEC
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0400278F RID: 10127
		protected uint rid;

		// Token: 0x04002790 RID: 10128
		private readonly Lock theLock = Lock.Create();

		// Token: 0x04002791 RID: 10129
		protected ModuleDef module2;

		// Token: 0x04002792 RID: 10130
		protected bool module2_isInitialized;

		// Token: 0x04002793 RID: 10131
		protected int attributes;

		// Token: 0x04002794 RID: 10132
		protected UTF8String name;

		// Token: 0x04002795 RID: 10133
		protected UTF8String @namespace;

		// Token: 0x04002796 RID: 10134
		protected ITypeDefOrRef baseType;

		// Token: 0x04002797 RID: 10135
		protected bool baseType_isInitialized;

		// Token: 0x04002798 RID: 10136
		protected LazyList<FieldDef> fields;

		// Token: 0x04002799 RID: 10137
		protected LazyList<MethodDef> methods;

		// Token: 0x0400279A RID: 10138
		protected LazyList<GenericParam> genericParameters;

		// Token: 0x0400279B RID: 10139
		protected IList<InterfaceImpl> interfaces;

		// Token: 0x0400279C RID: 10140
		protected IList<DeclSecurity> declSecurities;

		// Token: 0x0400279D RID: 10141
		protected ClassLayout classLayout;

		// Token: 0x0400279E RID: 10142
		protected bool classLayout_isInitialized;

		// Token: 0x0400279F RID: 10143
		protected TypeDef declaringType2;

		// Token: 0x040027A0 RID: 10144
		protected bool declaringType2_isInitialized;

		// Token: 0x040027A1 RID: 10145
		protected LazyList<TypeDef> nestedTypes;

		// Token: 0x040027A2 RID: 10146
		protected LazyList<EventDef> events;

		// Token: 0x040027A3 RID: 10147
		protected LazyList<PropertyDef> properties;

		// Token: 0x040027A4 RID: 10148
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040027A5 RID: 10149
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x040027A6 RID: 10150
		private static readonly UTF8String systemString = new UTF8String("System");

		// Token: 0x040027A7 RID: 10151
		private static readonly UTF8String enumString = new UTF8String("Enum");

		// Token: 0x040027A8 RID: 10152
		private static readonly UTF8String valueTypeString = new UTF8String("ValueType");

		// Token: 0x040027A9 RID: 10153
		private static readonly UTF8String multicastDelegateString = new UTF8String("MulticastDelegate");
	}
}
