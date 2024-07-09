using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200085C RID: 2140
	internal sealed class TypeDefMD : TypeDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x00192CE8 File Offset: 0x00192CE8
		internal ModuleDefMD ReaderModule
		{
			get
			{
				return this.readerModule;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x00192CF0 File Offset: 0x00192CF0
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x00192CF8 File Offset: 0x00192CF8
		protected override ITypeDefOrRef GetBaseType_NoLock()
		{
			return this.readerModule.ResolveTypeDefOrRef(this.extendsCodedToken, new GenericParamContext(this));
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x00192D14 File Offset: 0x00192D14
		protected override void InitializeFields()
		{
			RidList fieldRidList = this.readerModule.Metadata.GetFieldRidList(this.origRid);
			LazyList<FieldDef, RidList> value = new LazyList<FieldDef, RidList>(fieldRidList.Count, this, fieldRidList, (RidList list2, int index) => this.readerModule.ResolveField(list2[index]));
			Interlocked.CompareExchange<LazyList<FieldDef>>(ref this.fields, value, null);
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00192D68 File Offset: 0x00192D68
		protected override void InitializeMethods()
		{
			RidList methodRidList = this.readerModule.Metadata.GetMethodRidList(this.origRid);
			LazyList<MethodDef, RidList> value = new LazyList<MethodDef, RidList>(methodRidList.Count, this, methodRidList, (RidList list2, int index) => this.readerModule.ResolveMethod(list2[index]));
			Interlocked.CompareExchange<LazyList<MethodDef>>(ref this.methods, value, null);
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00192DBC File Offset: 0x00192DBC
		protected override void InitializeGenericParameters()
		{
			RidList genericParamRidList = this.readerModule.Metadata.GetGenericParamRidList(Table.TypeDef, this.origRid);
			LazyList<GenericParam, RidList> value = new LazyList<GenericParam, RidList>(genericParamRidList.Count, this, genericParamRidList, (RidList list2, int index) => this.readerModule.ResolveGenericParam(list2[index]));
			Interlocked.CompareExchange<LazyList<GenericParam>>(ref this.genericParameters, value, null);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00192E10 File Offset: 0x00192E10
		protected override void InitializeInterfaces()
		{
			RidList interfaceImplRidList = this.readerModule.Metadata.GetInterfaceImplRidList(this.origRid);
			LazyList<InterfaceImpl, RidList> value = new LazyList<InterfaceImpl, RidList>(interfaceImplRidList.Count, interfaceImplRidList, (RidList list2, int index) => this.readerModule.ResolveInterfaceImpl(list2[index], new GenericParamContext(this)));
			Interlocked.CompareExchange<IList<InterfaceImpl>>(ref this.interfaces, value, null);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00192E60 File Offset: 0x00192E60
		protected override void InitializeDeclSecurities()
		{
			RidList declSecurityRidList = this.readerModule.Metadata.GetDeclSecurityRidList(Table.TypeDef, this.origRid);
			LazyList<DeclSecurity, RidList> value = new LazyList<DeclSecurity, RidList>(declSecurityRidList.Count, declSecurityRidList, (RidList list2, int index) => this.readerModule.ResolveDeclSecurity(list2[index]));
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, value, null);
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00192EB4 File Offset: 0x00192EB4
		protected override ClassLayout GetClassLayout_NoLock()
		{
			return this.readerModule.ResolveClassLayout(this.readerModule.Metadata.GetClassLayoutRid(this.origRid));
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00192ED8 File Offset: 0x00192ED8
		protected override TypeDef GetDeclaringType2_NoLock()
		{
			RawNestedClassRow rawNestedClassRow;
			if (!this.readerModule.TablesStream.TryReadNestedClassRow(this.readerModule.Metadata.GetNestedClassRid(this.origRid), out rawNestedClassRow))
			{
				return null;
			}
			return this.readerModule.ResolveTypeDef(rawNestedClassRow.EnclosingClass);
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060051A4 RID: 20900 RVA: 0x00192F2C File Offset: 0x00192F2C
		private TypeDef DeclaringType2_NoLock
		{
			get
			{
				if (!this.declaringType2_isInitialized)
				{
					this.declaringType2 = this.GetDeclaringType2_NoLock();
					this.declaringType2_isInitialized = true;
				}
				return this.declaringType2;
			}
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00192F54 File Offset: 0x00192F54
		protected override void InitializeEvents()
		{
			uint eventMapRid = this.readerModule.Metadata.GetEventMapRid(this.origRid);
			RidList eventRidList = this.readerModule.Metadata.GetEventRidList(eventMapRid);
			LazyList<EventDef, RidList> value = new LazyList<EventDef, RidList>(eventRidList.Count, this, eventRidList, (RidList list2, int index) => this.readerModule.ResolveEvent(list2[index]));
			Interlocked.CompareExchange<LazyList<EventDef>>(ref this.events, value, null);
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x00192FB8 File Offset: 0x00192FB8
		protected override void InitializeProperties()
		{
			uint propertyMapRid = this.readerModule.Metadata.GetPropertyMapRid(this.origRid);
			RidList propertyRidList = this.readerModule.Metadata.GetPropertyRidList(propertyMapRid);
			LazyList<PropertyDef, RidList> value = new LazyList<PropertyDef, RidList>(propertyRidList.Count, this, propertyRidList, (RidList list2, int index) => this.readerModule.ResolveProperty(list2[index]));
			Interlocked.CompareExchange<LazyList<PropertyDef>>(ref this.properties, value, null);
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x0019301C File Offset: 0x0019301C
		protected override void InitializeNestedTypes()
		{
			RidList nestedClassRidList = this.readerModule.Metadata.GetNestedClassRidList(this.origRid);
			LazyList<TypeDef, RidList> value = new LazyList<TypeDef, RidList>(nestedClassRidList.Count, this, nestedClassRidList, (RidList list2, int index) => this.readerModule.ResolveTypeDef(list2[index]));
			Interlocked.CompareExchange<LazyList<TypeDef>>(ref this.nestedTypes, value, null);
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00193070 File Offset: 0x00193070
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.TypeDef, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x001930E4 File Offset: 0x001930E4
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), new GenericParamContext(this), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00193134 File Offset: 0x00193134
		protected override ModuleDef GetModule2_NoLock()
		{
			if (this.DeclaringType2_NoLock == null)
			{
				return this.readerModule;
			}
			return null;
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0019314C File Offset: 0x0019314C
		public TypeDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawTypeDefRow rawTypeDefRow;
			readerModule.TablesStream.TryReadTypeDefRow(this.origRid, out rawTypeDefRow);
			this.extendsCodedToken = rawTypeDefRow.Extends;
			this.attributes = (int)rawTypeDefRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawTypeDefRow.Name);
			this.@namespace = readerModule.StringsStream.ReadNoNull(rawTypeDefRow.Namespace);
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x001931D4 File Offset: 0x001931D4
		internal IList<MethodOverride> GetMethodOverrides(MethodDefMD method, GenericParamContext gpContext)
		{
			if (method == null)
			{
				return new List<MethodOverride>();
			}
			if (this.methodRidToOverrides == null)
			{
				this.InitializeMethodOverrides();
			}
			IList<TypeDefMD.MethodOverrideTokens> list;
			if (this.methodRidToOverrides.TryGetValue(method.OrigRid, out list))
			{
				List<MethodOverride> list2 = new List<MethodOverride>(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					TypeDefMD.MethodOverrideTokens methodOverrideTokens = list[i];
					IMethodDefOrRef methodBody = (IMethodDefOrRef)this.readerModule.ResolveToken(methodOverrideTokens.MethodBodyToken, gpContext);
					IMethodDefOrRef methodDeclaration = (IMethodDefOrRef)this.readerModule.ResolveToken(methodOverrideTokens.MethodDeclarationToken, gpContext);
					list2.Add(new MethodOverride(methodBody, methodDeclaration));
				}
				return list2;
			}
			return new List<MethodOverride>();
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0019328C File Offset: 0x0019328C
		private void InitializeMethodOverrides()
		{
			Dictionary<uint, IList<TypeDefMD.MethodOverrideTokens>> dictionary = new Dictionary<uint, IList<TypeDefMD.MethodOverrideTokens>>();
			RidList methodImplRidList = this.readerModule.Metadata.GetMethodImplRidList(this.origRid);
			for (int i = 0; i < methodImplRidList.Count; i++)
			{
				RawMethodImplRow rawMethodImplRow;
				if (this.readerModule.TablesStream.TryReadMethodImplRow(methodImplRidList[i], out rawMethodImplRow))
				{
					IMethodDefOrRef methodDefOrRef = this.readerModule.ResolveMethodDefOrRef(rawMethodImplRow.MethodBody);
					IMethodDefOrRef methodDefOrRef2 = this.readerModule.ResolveMethodDefOrRef(rawMethodImplRow.MethodDeclaration);
					if (methodDefOrRef != null && methodDefOrRef2 != null)
					{
						MethodDef methodDef = base.FindMethodImplMethod(methodDefOrRef);
						if (methodDef != null && methodDef.DeclaringType == this)
						{
							uint rid = methodDef.Rid;
							IList<TypeDefMD.MethodOverrideTokens> list;
							if (!dictionary.TryGetValue(rid, out list))
							{
								list = (dictionary[rid] = new List<TypeDefMD.MethodOverrideTokens>());
							}
							list.Add(new TypeDefMD.MethodOverrideTokens(methodDefOrRef.MDToken.Raw, methodDefOrRef2.MDToken.Raw));
						}
					}
				}
			}
			Interlocked.CompareExchange<Dictionary<uint, IList<TypeDefMD.MethodOverrideTokens>>>(ref this.methodRidToOverrides, dictionary, null);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x001933A8 File Offset: 0x001933A8
		internal void InitializeMethodSemanticsAttributes()
		{
			uint num = this.readerModule.Metadata.GetPropertyMapRid(this.origRid);
			RidList ridList = this.readerModule.Metadata.GetPropertyRidList(num);
			for (int i = 0; i < ridList.Count; i++)
			{
				RidList methodSemanticsRidList = this.readerModule.Metadata.GetMethodSemanticsRidList(Table.Property, ridList[i]);
				for (int j = 0; j < methodSemanticsRidList.Count; j++)
				{
					RawMethodSemanticsRow rawMethodSemanticsRow;
					if (this.readerModule.TablesStream.TryReadMethodSemanticsRow(methodSemanticsRidList[j], out rawMethodSemanticsRow))
					{
						MethodDef methodDef = this.readerModule.ResolveMethod(rawMethodSemanticsRow.Method);
						if (methodDef != null)
						{
							Interlocked.CompareExchange(ref methodDef.semAttrs, (int)rawMethodSemanticsRow.Semantic | MethodDef.SEMATTRS_INITD, 0);
						}
					}
				}
			}
			num = this.readerModule.Metadata.GetEventMapRid(this.origRid);
			ridList = this.readerModule.Metadata.GetEventRidList(num);
			for (int k = 0; k < ridList.Count; k++)
			{
				RidList methodSemanticsRidList2 = this.readerModule.Metadata.GetMethodSemanticsRidList(Table.Event, ridList[k]);
				for (int l = 0; l < methodSemanticsRidList2.Count; l++)
				{
					RawMethodSemanticsRow rawMethodSemanticsRow2;
					if (this.readerModule.TablesStream.TryReadMethodSemanticsRow(methodSemanticsRidList2[l], out rawMethodSemanticsRow2))
					{
						MethodDef methodDef2 = this.readerModule.ResolveMethod(rawMethodSemanticsRow2.Method);
						if (methodDef2 != null)
						{
							Interlocked.CompareExchange(ref methodDef2.semAttrs, (int)rawMethodSemanticsRow2.Semantic | MethodDef.SEMATTRS_INITD, 0);
						}
					}
				}
			}
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00193558 File Offset: 0x00193558
		internal void InitializeProperty(PropertyDefMD prop, out IList<MethodDef> getMethods, out IList<MethodDef> setMethods, out IList<MethodDef> otherMethods)
		{
			getMethods = new List<MethodDef>();
			setMethods = new List<MethodDef>();
			otherMethods = new List<MethodDef>();
			if (prop == null)
			{
				return;
			}
			RidList methodSemanticsRidList = this.readerModule.Metadata.GetMethodSemanticsRidList(Table.Property, prop.OrigRid);
			for (int i = 0; i < methodSemanticsRidList.Count; i++)
			{
				RawMethodSemanticsRow rawMethodSemanticsRow;
				if (this.readerModule.TablesStream.TryReadMethodSemanticsRow(methodSemanticsRidList[i], out rawMethodSemanticsRow))
				{
					MethodDef methodDef = this.readerModule.ResolveMethod(rawMethodSemanticsRow.Method);
					if (methodDef != null && methodDef.DeclaringType == prop.DeclaringType)
					{
						switch (rawMethodSemanticsRow.Semantic)
						{
						case 1:
							if (!setMethods.Contains(methodDef))
							{
								setMethods.Add(methodDef);
							}
							break;
						case 2:
							if (!getMethods.Contains(methodDef))
							{
								getMethods.Add(methodDef);
							}
							break;
						case 4:
							if (!otherMethods.Contains(methodDef))
							{
								otherMethods.Add(methodDef);
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00193670 File Offset: 0x00193670
		internal void InitializeEvent(EventDefMD evt, out MethodDef addMethod, out MethodDef invokeMethod, out MethodDef removeMethod, out IList<MethodDef> otherMethods)
		{
			addMethod = null;
			invokeMethod = null;
			removeMethod = null;
			otherMethods = new List<MethodDef>();
			if (evt == null)
			{
				return;
			}
			RidList methodSemanticsRidList = this.readerModule.Metadata.GetMethodSemanticsRidList(Table.Event, evt.OrigRid);
			for (int i = 0; i < methodSemanticsRidList.Count; i++)
			{
				RawMethodSemanticsRow rawMethodSemanticsRow;
				if (this.readerModule.TablesStream.TryReadMethodSemanticsRow(methodSemanticsRidList[i], out rawMethodSemanticsRow))
				{
					MethodDef methodDef = this.readerModule.ResolveMethod(rawMethodSemanticsRow.Method);
					if (methodDef != null && methodDef.DeclaringType == evt.DeclaringType)
					{
						MethodSemanticsAttributes semantic = (MethodSemanticsAttributes)rawMethodSemanticsRow.Semantic;
						if (semantic <= MethodSemanticsAttributes.AddOn)
						{
							if (semantic != MethodSemanticsAttributes.Other)
							{
								if (semantic == MethodSemanticsAttributes.AddOn)
								{
									if (addMethod == null)
									{
										addMethod = methodDef;
									}
								}
							}
							else if (!otherMethods.Contains(methodDef))
							{
								otherMethods.Add(methodDef);
							}
						}
						else if (semantic != MethodSemanticsAttributes.RemoveOn)
						{
							if (semantic == MethodSemanticsAttributes.Fire)
							{
								if (invokeMethod == null)
								{
									invokeMethod = methodDef;
								}
							}
						}
						else if (removeMethod == null)
						{
							removeMethod = methodDef;
						}
					}
				}
			}
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00193798 File Offset: 0x00193798
		internal override void OnLazyAdd2(int index, ref FieldDef value)
		{
			if (value.DeclaringType != this)
			{
				value = this.readerModule.ForceUpdateRowId<FieldDefMD>(this.readerModule.ReadField(value.Rid).InitializeAll());
				value.DeclaringType2 = this;
			}
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x001937E4 File Offset: 0x001937E4
		internal override void OnLazyAdd2(int index, ref MethodDef value)
		{
			if (value.DeclaringType != this)
			{
				value = this.readerModule.ForceUpdateRowId<MethodDefMD>(this.readerModule.ReadMethod(value.Rid).InitializeAll());
				value.DeclaringType2 = this;
				value.Parameters.UpdateThisParameterType(this);
			}
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0019383C File Offset: 0x0019383C
		internal override void OnLazyAdd2(int index, ref EventDef value)
		{
			if (value.DeclaringType != this)
			{
				value = this.readerModule.ForceUpdateRowId<EventDefMD>(this.readerModule.ReadEvent(value.Rid).InitializeAll());
				value.DeclaringType2 = this;
			}
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x00193888 File Offset: 0x00193888
		internal override void OnLazyAdd2(int index, ref PropertyDef value)
		{
			if (value.DeclaringType != this)
			{
				value = this.readerModule.ForceUpdateRowId<PropertyDefMD>(this.readerModule.ReadProperty(value.Rid).InitializeAll());
				value.DeclaringType2 = this;
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x001938D4 File Offset: 0x001938D4
		internal override void OnLazyAdd2(int index, ref GenericParam value)
		{
			if (value.Owner != this)
			{
				value = this.readerModule.ForceUpdateRowId<GenericParamMD>(this.readerModule.ReadGenericParam(value.Rid).InitializeAll());
				value.Owner = this;
			}
		}

		// Token: 0x040027AA RID: 10154
		private readonly ModuleDefMD readerModule;

		// Token: 0x040027AB RID: 10155
		private readonly uint origRid;

		// Token: 0x040027AC RID: 10156
		private readonly uint extendsCodedToken;

		// Token: 0x040027AD RID: 10157
		private Dictionary<uint, IList<TypeDefMD.MethodOverrideTokens>> methodRidToOverrides;

		// Token: 0x02001000 RID: 4096
		private readonly struct MethodOverrideTokens
		{
			// Token: 0x06008EE2 RID: 36578 RVA: 0x002AAF0C File Offset: 0x002AAF0C
			public MethodOverrideTokens(uint methodBodyToken, uint methodDeclarationToken)
			{
				this.MethodBodyToken = methodBodyToken;
				this.MethodDeclarationToken = methodDeclarationToken;
			}

			// Token: 0x04004445 RID: 17477
			public readonly uint MethodBodyToken;

			// Token: 0x04004446 RID: 17478
			public readonly uint MethodDeclarationToken;
		}
	}
}
