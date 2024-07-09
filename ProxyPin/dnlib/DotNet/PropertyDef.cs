using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x0200082F RID: 2095
	[ComVisible(true)]
	public abstract class PropertyDef : IHasConstant, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName, IHasSemantic, IMemberRef, IOwnerModule, IIsTypeOrMethod, IHasCustomDebugInformation, IMemberDef, IDnlibDef
	{
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00185D90 File Offset: 0x00185D90
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Property, this.rid);
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x00185DA0 File Offset: 0x00185DA0
		// (set) Token: 0x06004E3C RID: 20028 RVA: 0x00185DA8 File Offset: 0x00185DA8
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

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00185DB4 File Offset: 0x00185DB4
		public int HasConstantTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x00185DB8 File Offset: 0x00185DB8
		public int HasCustomAttributeTag
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x00185DBC File Offset: 0x00185DBC
		public int HasSemanticTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06004E40 RID: 20032 RVA: 0x00185DC0 File Offset: 0x00185DC0
		// (set) Token: 0x06004E41 RID: 20033 RVA: 0x00185DCC File Offset: 0x00185DCC
		public PropertyAttributes Attributes
		{
			get
			{
				return (PropertyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x00185DD8 File Offset: 0x00185DD8
		// (set) Token: 0x06004E43 RID: 20035 RVA: 0x00185DE0 File Offset: 0x00185DE0
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

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00185DEC File Offset: 0x00185DEC
		// (set) Token: 0x06004E45 RID: 20037 RVA: 0x00185DF4 File Offset: 0x00185DF4
		public CallingConventionSig Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00185E00 File Offset: 0x00185E00
		// (set) Token: 0x06004E47 RID: 20039 RVA: 0x00185E1C File Offset: 0x00185E1C
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

		// Token: 0x06004E48 RID: 20040 RVA: 0x00185E64 File Offset: 0x00185E64
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

		// Token: 0x06004E49 RID: 20041 RVA: 0x00185EC0 File Offset: 0x00185EC0
		protected virtual Constant GetConstant_NoLock()
		{
			return null;
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00185EC4 File Offset: 0x00185EC4
		protected void ResetConstant()
		{
			this.constant_isInitialized = false;
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06004E4B RID: 20043 RVA: 0x00185ED0 File Offset: 0x00185ED0
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

		// Token: 0x06004E4C RID: 20044 RVA: 0x00185EEC File Offset: 0x00185EEC
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06004E4D RID: 20045 RVA: 0x00185F00 File Offset: 0x00185F00
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x00185F04 File Offset: 0x00185F04
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x00185F14 File Offset: 0x00185F14
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

		// Token: 0x06004E50 RID: 20048 RVA: 0x00185F30 File Offset: 0x00185F30
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00185F44 File Offset: 0x00185F44
		// (set) Token: 0x06004E52 RID: 20050 RVA: 0x00185F78 File Offset: 0x00185F78
		public MethodDef GetMethod
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				if (this.getMethods.Count != 0)
				{
					return this.getMethods[0];
				}
				return null;
			}
			set
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				if (value == null)
				{
					this.getMethods.Clear();
					return;
				}
				if (this.getMethods.Count == 0)
				{
					this.getMethods.Add(value);
					return;
				}
				this.getMethods[0] = value;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00185FD8 File Offset: 0x00185FD8
		// (set) Token: 0x06004E54 RID: 20052 RVA: 0x0018600C File Offset: 0x0018600C
		public MethodDef SetMethod
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				if (this.setMethods.Count != 0)
				{
					return this.setMethods[0];
				}
				return null;
			}
			set
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				if (value == null)
				{
					this.setMethods.Clear();
					return;
				}
				if (this.setMethods.Count == 0)
				{
					this.setMethods.Add(value);
					return;
				}
				this.setMethods[0] = value;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x0018606C File Offset: 0x0018606C
		public IList<MethodDef> GetMethods
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				return this.getMethods;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06004E56 RID: 20054 RVA: 0x00186088 File Offset: 0x00186088
		public IList<MethodDef> SetMethods
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				return this.setMethods;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06004E57 RID: 20055 RVA: 0x001860A4 File Offset: 0x001860A4
		public IList<MethodDef> OtherMethods
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods();
				}
				return this.otherMethods;
			}
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x001860C0 File Offset: 0x001860C0
		private void InitializePropertyMethods()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (this.otherMethods == null)
				{
					this.InitializePropertyMethods_NoLock();
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x0018610C File Offset: 0x0018610C
		protected virtual void InitializePropertyMethods_NoLock()
		{
			this.getMethods = new List<MethodDef>();
			this.setMethods = new List<MethodDef>();
			this.otherMethods = new List<MethodDef>();
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00186130 File Offset: 0x00186130
		protected void ResetMethods()
		{
			this.otherMethods = null;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x0018613C File Offset: 0x0018613C
		public bool IsEmpty
		{
			get
			{
				return this.GetMethods.Count == 0 && this.setMethods.Count == 0 && this.otherMethods.Count == 0;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x00186170 File Offset: 0x00186170
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00186180 File Offset: 0x00186180
		public bool HasOtherMethods
		{
			get
			{
				return this.OtherMethods.Count > 0;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x00186190 File Offset: 0x00186190
		public bool HasConstant
		{
			get
			{
				return this.Constant != null;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x001861A0 File Offset: 0x001861A0
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

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x001861C8 File Offset: 0x001861C8
		// (set) Token: 0x06004E61 RID: 20065 RVA: 0x001861D8 File Offset: 0x001861D8
		public PropertySig PropertySig
		{
			get
			{
				return this.type as PropertySig;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x001861E4 File Offset: 0x001861E4
		// (set) Token: 0x06004E63 RID: 20067 RVA: 0x001861EC File Offset: 0x001861EC
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
					typeDef.Properties.Remove(this);
				}
				if (value != null)
				{
					value.Properties.Add(this);
				}
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00186234 File Offset: 0x00186234
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x0018623C File Offset: 0x0018623C
		// (set) Token: 0x06004E66 RID: 20070 RVA: 0x00186244 File Offset: 0x00186244
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

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x00186250 File Offset: 0x00186250
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

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x00186268 File Offset: 0x00186268
		public string FullName
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				return FullNameFactory.PropertyFullName((typeDef != null) ? typeDef.FullName : null, this.name, this.type, null, null);
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x00186298 File Offset: 0x00186298
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x0018629C File Offset: 0x0018629C
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x001862A0 File Offset: 0x001862A0
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x001862A4 File Offset: 0x001862A4
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x001862A8 File Offset: 0x001862A8
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x001862AC File Offset: 0x001862AC
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x001862B0 File Offset: 0x001862B0
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x001862B4 File Offset: 0x001862B4
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x001862B8 File Offset: 0x001862B8
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x001862BC File Offset: 0x001862BC
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x001862C0 File Offset: 0x001862C0
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x001862C4 File Offset: 0x001862C4
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x001862C8 File Offset: 0x001862C8
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x001862CC File Offset: 0x001862CC
		private void ModifyAttributes(bool set, PropertyAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x001862F4 File Offset: 0x001862F4
		// (set) Token: 0x06004E78 RID: 20088 RVA: 0x00186308 File Offset: 0x00186308
		public bool IsSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 512) > 0;
			}
			set
			{
				this.ModifyAttributes(value, PropertyAttributes.SpecialName);
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x00186318 File Offset: 0x00186318
		// (set) Token: 0x06004E7A RID: 20090 RVA: 0x0018632C File Offset: 0x0018632C
		public bool IsRuntimeSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 1024) > 0;
			}
			set
			{
				this.ModifyAttributes(value, PropertyAttributes.RTSpecialName);
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x0018633C File Offset: 0x0018633C
		// (set) Token: 0x06004E7C RID: 20092 RVA: 0x00186350 File Offset: 0x00186350
		public bool HasDefault
		{
			get
			{
				return ((ushort)this.attributes & 4096) > 0;
			}
			set
			{
				this.ModifyAttributes(value, PropertyAttributes.HasDefault);
			}
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00186360 File Offset: 0x00186360
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040026B1 RID: 9905
		protected uint rid;

		// Token: 0x040026B2 RID: 9906
		private readonly Lock theLock = Lock.Create();

		// Token: 0x040026B3 RID: 9907
		protected int attributes;

		// Token: 0x040026B4 RID: 9908
		protected UTF8String name;

		// Token: 0x040026B5 RID: 9909
		protected CallingConventionSig type;

		// Token: 0x040026B6 RID: 9910
		protected Constant constant;

		// Token: 0x040026B7 RID: 9911
		protected bool constant_isInitialized;

		// Token: 0x040026B8 RID: 9912
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040026B9 RID: 9913
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x040026BA RID: 9914
		protected IList<MethodDef> getMethods;

		// Token: 0x040026BB RID: 9915
		protected IList<MethodDef> setMethods;

		// Token: 0x040026BC RID: 9916
		protected IList<MethodDef> otherMethods;

		// Token: 0x040026BD RID: 9917
		protected TypeDef declaringType2;
	}
}
