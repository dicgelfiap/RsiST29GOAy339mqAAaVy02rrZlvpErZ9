using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D2 RID: 2258
	internal sealed class PreserveTokensMetadata : Metadata
	{
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x001AD78C File Offset: 0x001AD78C
		protected override int NumberOfMethods
		{
			get
			{
				return this.methodDefInfos.Count;
			}
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x001AD79C File Offset: 0x001AD79C
		public PreserveTokensMetadata(ModuleDef module, UniqueChunkList<ByteArrayChunk> constants, MethodBodyChunks methodBodies, NetResources netResources, MetadataOptions options, DebugMetadataKind debugKind, bool isStandaloneDebugMetadata) : base(module, constants, methodBodies, netResources, options, debugKind, isStandaloneDebugMetadata)
		{
			this.mod = (module as ModuleDefMD);
			if (this.mod == null)
			{
				throw new ModuleWriterException("Not a ModuleDefMD");
			}
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x001AD830 File Offset: 0x001AD830
		public override uint GetRid(TypeRef tr)
		{
			uint result;
			this.typeRefInfos.TryGetRid(tr, out result);
			return result;
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x001AD854 File Offset: 0x001AD854
		public override uint GetRid(TypeDef td)
		{
			if (td == null)
			{
				base.Error("TypeDef is null", new object[0]);
				return 0U;
			}
			uint result;
			if (this.typeToRid.TryGetValue(td, out result))
			{
				return result;
			}
			base.Error("TypeDef {0} ({1:X8}) is not defined in this module ({2}). A type was removed that is still referenced by this module.", new object[]
			{
				td,
				td.MDToken.Raw,
				this.module
			});
			return 0U;
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x001AD8C8 File Offset: 0x001AD8C8
		public override uint GetRid(FieldDef fd)
		{
			uint result;
			if (this.fieldDefInfos.TryGetRid(fd, out result))
			{
				return result;
			}
			if (fd == null)
			{
				base.Error("Field is null", new object[0]);
			}
			else
			{
				base.Error("Field {0} ({1:X8}) is not defined in this module ({2}). A field was removed that is still referenced by this module.", new object[]
				{
					fd,
					fd.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001AD940 File Offset: 0x001AD940
		public override uint GetRid(MethodDef md)
		{
			uint result;
			if (this.methodDefInfos.TryGetRid(md, out result))
			{
				return result;
			}
			if (md == null)
			{
				base.Error("Method is null", new object[0]);
			}
			else
			{
				base.Error("Method {0} ({1:X8}) is not defined in this module ({2}). A method was removed that is still referenced by this module.", new object[]
				{
					md,
					md.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x001AD9B8 File Offset: 0x001AD9B8
		public override uint GetRid(ParamDef pd)
		{
			uint result;
			if (this.paramDefInfos.TryGetRid(pd, out result))
			{
				return result;
			}
			if (pd == null)
			{
				base.Error("Param is null", new object[0]);
			}
			else
			{
				base.Error("Param {0} ({1:X8}) is not defined in this module ({2}). A parameter was removed that is still referenced by this module.", new object[]
				{
					pd,
					pd.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x001ADA30 File Offset: 0x001ADA30
		public override uint GetRid(MemberRef mr)
		{
			uint result;
			this.memberRefInfos.TryGetRid(mr, out result);
			return result;
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x001ADA54 File Offset: 0x001ADA54
		public override uint GetRid(StandAloneSig sas)
		{
			uint result;
			this.standAloneSigInfos.TryGetRid(sas, out result);
			return result;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x001ADA78 File Offset: 0x001ADA78
		public override uint GetRid(EventDef ed)
		{
			uint result;
			if (this.eventDefInfos.TryGetRid(ed, out result))
			{
				return result;
			}
			if (ed == null)
			{
				base.Error("Event is null", new object[0]);
			}
			else
			{
				base.Error("Event {0} ({1:X8}) is not defined in this module ({2}). An event was removed that is still referenced by this module.", new object[]
				{
					ed,
					ed.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x001ADAF0 File Offset: 0x001ADAF0
		public override uint GetRid(PropertyDef pd)
		{
			uint result;
			if (this.propertyDefInfos.TryGetRid(pd, out result))
			{
				return result;
			}
			if (pd == null)
			{
				base.Error("Property is null", new object[0]);
			}
			else
			{
				base.Error("Property {0} ({1:X8}) is not defined in this module ({2}). A property was removed that is still referenced by this module.", new object[]
				{
					pd,
					pd.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001ADB68 File Offset: 0x001ADB68
		public override uint GetRid(TypeSpec ts)
		{
			uint result;
			this.typeSpecInfos.TryGetRid(ts, out result);
			return result;
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001ADB8C File Offset: 0x001ADB8C
		public override uint GetRid(MethodSpec ms)
		{
			uint result;
			this.methodSpecInfos.TryGetRid(ms, out result);
			return result;
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x001ADBB0 File Offset: 0x001ADBB0
		protected override void Initialize()
		{
			this.fieldDefInfos = new PreserveTokensMetadata.MemberDefDict<FieldDef>(typeof(FieldDefMD), base.PreserveFieldRids);
			this.methodDefInfos = new PreserveTokensMetadata.MemberDefDict<MethodDef>(typeof(MethodDefMD), base.PreserveMethodRids, true);
			this.paramDefInfos = new PreserveTokensMetadata.MemberDefDict<ParamDef>(typeof(ParamDefMD), base.PreserveParamRids);
			this.eventDefInfos = new PreserveTokensMetadata.MemberDefDict<EventDef>(typeof(EventDefMD), base.PreserveEventRids);
			this.propertyDefInfos = new PreserveTokensMetadata.MemberDefDict<PropertyDef>(typeof(PropertyDefMD), base.PreservePropertyRids);
			this.CreateEmptyTableRows();
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x001ADC50 File Offset: 0x001ADC50
		protected override TypeDef[] GetAllTypeDefs()
		{
			if (!base.PreserveTypeDefRids)
			{
				TypeDef[] array = this.module.GetTypes().ToArray<TypeDef>();
				this.InitializeTypeToRid(array);
				return array;
			}
			Dictionary<TypeDef, uint> typeToIndex = new Dictionary<TypeDef, uint>();
			List<TypeDef> list = new List<TypeDef>();
			uint num = 0U;
			foreach (TypeDef typeDef in this.module.GetTypes())
			{
				if (typeDef != null)
				{
					list.Add(typeDef);
					uint num2 = num++;
					if (typeDef.GetType() == typeof(TypeDefMD))
					{
						num2 |= 2147483648U;
					}
					typeToIndex[typeDef] = num2;
				}
			}
			TypeDef globalType = list[0];
			list.Sort(delegate(TypeDef a, TypeDef b)
			{
				if (a == b)
				{
					return 0;
				}
				if (a == globalType)
				{
					return -1;
				}
				if (b == globalType)
				{
					return 1;
				}
				uint num5 = typeToIndex[a];
				uint num6 = typeToIndex[b];
				bool flag = (num5 & 2147483648U) > 0U;
				bool flag2 = (num6 & 2147483648U) > 0U;
				if (flag == flag2)
				{
					if (flag)
					{
						return a.Rid.CompareTo(b.Rid);
					}
					return (num5 & 16777215U).CompareTo(num6 & 16777215U);
				}
				else
				{
					if (flag)
					{
						return -1;
					}
					return 1;
				}
			});
			List<TypeDef> list2 = new List<TypeDef>(list.Count);
			uint num3 = 1U;
			list2.Add(globalType);
			for (int i = 1; i < list.Count; i++)
			{
				TypeDef typeDef2 = list[i];
				if (typeDef2.GetType() != typeof(TypeDefMD))
				{
					while (i < list.Count)
					{
						list2.Add(list[i++]);
					}
					break;
				}
				uint rid = typeDef2.Rid;
				int num4 = (int)(rid - num3 - 1U);
				if (num4 != 0)
				{
					for (int j = 0; j < num4; j++)
					{
						list2.Add(new TypeDefUser("dummy", Guid.NewGuid().ToString("B"), this.module.CorLibTypes.Object.TypeDefOrRef));
					}
				}
				list2.Add(typeDef2);
				num3 = rid;
			}
			TypeDef[] array2 = list2.ToArray();
			this.InitializeTypeToRid(array2);
			return array2;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x001ADE68 File Offset: 0x001ADE68
		private void InitializeTypeToRid(TypeDef[] types)
		{
			uint num = 1U;
			foreach (TypeDef typeDef in types)
			{
				if (typeDef != null && !this.typeToRid.ContainsKey(typeDef))
				{
					this.typeToRid[typeDef] = num++;
				}
			}
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x001ADEC0 File Offset: 0x001ADEC0
		protected override void AllocateTypeDefRids()
		{
			foreach (TypeDef key in this.allTypeDefs)
			{
				uint num = this.tablesHeap.TypeDefTable.Create(default(RawTypeDefRow));
				if (this.typeToRid[key] != num)
				{
					throw new ModuleWriterException("Got a different rid than expected");
				}
			}
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x001ADF2C File Offset: 0x001ADF2C
		private void CreateEmptyTableRows()
		{
			if (base.PreserveTypeRefRids)
			{
				uint rows = this.mod.TablesStream.TypeRefTable.Rows;
				for (uint num = 0U; num < rows; num += 1U)
				{
					this.tablesHeap.TypeRefTable.Create(default(RawTypeRefRow));
				}
			}
			if (base.PreserveMemberRefRids)
			{
				uint rows = this.mod.TablesStream.MemberRefTable.Rows;
				for (uint num2 = 0U; num2 < rows; num2 += 1U)
				{
					this.tablesHeap.MemberRefTable.Create(default(RawMemberRefRow));
				}
			}
			if (base.PreserveStandAloneSigRids)
			{
				uint rows = this.mod.TablesStream.StandAloneSigTable.Rows;
				for (uint num3 = 0U; num3 < rows; num3 += 1U)
				{
					this.tablesHeap.StandAloneSigTable.Create(default(RawStandAloneSigRow));
				}
			}
			if (base.PreserveTypeSpecRids)
			{
				uint rows = this.mod.TablesStream.TypeSpecTable.Rows;
				for (uint num4 = 0U; num4 < rows; num4 += 1U)
				{
					this.tablesHeap.TypeSpecTable.Create(default(RawTypeSpecRow));
				}
			}
			if (base.PreserveMethodSpecRids)
			{
				uint rows = this.mod.TablesStream.MethodSpecTable.Rows;
				for (uint num5 = 0U; num5 < rows; num5 += 1U)
				{
					this.tablesHeap.MethodSpecTable.Create(default(RawMethodSpecRow));
				}
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x001AE0C0 File Offset: 0x001AE0C0
		private void InitializeUninitializedTableRows()
		{
			this.InitializeTypeRefTableRows();
			this.InitializeMemberRefTableRows();
			this.InitializeStandAloneSigTableRows();
			this.InitializeTypeSpecTableRows();
			this.InitializeMethodSpecTableRows();
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x001AE0F0 File Offset: 0x001AE0F0
		private void InitializeTypeRefTableRows()
		{
			if (!base.PreserveTypeRefRids || this.initdTypeRef)
			{
				return;
			}
			this.initdTypeRef = true;
			uint rows = this.mod.TablesStream.TypeRefTable.Rows;
			for (uint num = 1U; num <= rows; num += 1U)
			{
				this.AddTypeRef(this.mod.ResolveTypeRef(num));
			}
			this.tablesHeap.TypeRefTable.ReAddRows();
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001AE168 File Offset: 0x001AE168
		private void InitializeMemberRefTableRows()
		{
			if (!base.PreserveMemberRefRids || this.initdMemberRef)
			{
				return;
			}
			this.initdMemberRef = true;
			uint rows = this.mod.TablesStream.MemberRefTable.Rows;
			for (uint num = 1U; num <= rows; num += 1U)
			{
				if (this.tablesHeap.MemberRefTable[num].Class == 0U)
				{
					this.AddMemberRef(this.mod.ResolveMemberRef(num), true);
				}
			}
			this.tablesHeap.MemberRefTable.ReAddRows();
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x001AE1FC File Offset: 0x001AE1FC
		private void InitializeStandAloneSigTableRows()
		{
			if (!base.PreserveStandAloneSigRids || this.initdStandAloneSig)
			{
				return;
			}
			this.initdStandAloneSig = true;
			uint rows = this.mod.TablesStream.StandAloneSigTable.Rows;
			for (uint num = 1U; num <= rows; num += 1U)
			{
				if (this.tablesHeap.StandAloneSigTable[num].Signature == 0U)
				{
					this.AddStandAloneSig(this.mod.ResolveStandAloneSig(num), true);
				}
			}
			this.tablesHeap.StandAloneSigTable.ReAddRows();
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001AE290 File Offset: 0x001AE290
		private void InitializeTypeSpecTableRows()
		{
			if (!base.PreserveTypeSpecRids || this.initdTypeSpec)
			{
				return;
			}
			this.initdTypeSpec = true;
			uint rows = this.mod.TablesStream.TypeSpecTable.Rows;
			for (uint num = 1U; num <= rows; num += 1U)
			{
				if (this.tablesHeap.TypeSpecTable[num].Signature == 0U)
				{
					this.AddTypeSpec(this.mod.ResolveTypeSpec(num), true);
				}
			}
			this.tablesHeap.TypeSpecTable.ReAddRows();
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x001AE324 File Offset: 0x001AE324
		private void InitializeMethodSpecTableRows()
		{
			if (!base.PreserveMethodSpecRids || this.initdMethodSpec)
			{
				return;
			}
			this.initdMethodSpec = true;
			uint rows = this.mod.TablesStream.MethodSpecTable.Rows;
			for (uint num = 1U; num <= rows; num += 1U)
			{
				if (this.tablesHeap.MethodSpecTable[num].Method == 0U)
				{
					this.AddMethodSpec(this.mod.ResolveMethodSpec(num), true);
				}
			}
			this.tablesHeap.MethodSpecTable.ReAddRows();
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x001AE3B8 File Offset: 0x001AE3B8
		protected override void AllocateMemberDefRids()
		{
			this.FindMemberDefs();
			base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, 0.0);
			for (int i = 1; i <= this.fieldDefInfos.TableSize; i++)
			{
				if (i != (int)this.tablesHeap.FieldTable.Create(default(RawFieldRow)))
				{
					throw new ModuleWriterException("Invalid field rid");
				}
			}
			for (int j = 1; j <= this.methodDefInfos.TableSize; j++)
			{
				if (j != (int)this.tablesHeap.MethodTable.Create(default(RawMethodRow)))
				{
					throw new ModuleWriterException("Invalid method rid");
				}
			}
			for (int k = 1; k <= this.paramDefInfos.TableSize; k++)
			{
				if (k != (int)this.tablesHeap.ParamTable.Create(default(RawParamRow)))
				{
					throw new ModuleWriterException("Invalid param rid");
				}
			}
			for (int l = 1; l <= this.eventDefInfos.TableSize; l++)
			{
				if (l != (int)this.tablesHeap.EventTable.Create(default(RawEventRow)))
				{
					throw new ModuleWriterException("Invalid event rid");
				}
			}
			for (int m = 1; m <= this.propertyDefInfos.TableSize; m++)
			{
				if (m != (int)this.tablesHeap.PropertyTable.Create(default(RawPropertyRow)))
				{
					throw new ModuleWriterException("Invalid property rid");
				}
			}
			this.SortFields();
			this.SortMethods();
			this.SortParameters();
			this.SortEvents();
			this.SortProperties();
			base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, 0.2);
			if (this.fieldDefInfos.NeedPtrTable)
			{
				for (int n = 0; n < this.fieldDefInfos.Count; n++)
				{
					PreserveTokensMetadata.MemberDefInfo<FieldDef> sorted = this.fieldDefInfos.GetSorted(n);
					if (n + 1 != (int)this.tablesHeap.FieldPtrTable.Add(new RawFieldPtrRow(sorted.Rid)))
					{
						throw new ModuleWriterException("Invalid field ptr rid");
					}
				}
				this.ReUseDeletedFieldRows();
			}
			if (this.methodDefInfos.NeedPtrTable)
			{
				for (int num = 0; num < this.methodDefInfos.Count; num++)
				{
					PreserveTokensMetadata.MemberDefInfo<MethodDef> sorted2 = this.methodDefInfos.GetSorted(num);
					if (num + 1 != (int)this.tablesHeap.MethodPtrTable.Add(new RawMethodPtrRow(sorted2.Rid)))
					{
						throw new ModuleWriterException("Invalid method ptr rid");
					}
				}
				this.ReUseDeletedMethodRows();
			}
			if (this.paramDefInfos.NeedPtrTable)
			{
				for (int num2 = 0; num2 < this.paramDefInfos.Count; num2++)
				{
					PreserveTokensMetadata.MemberDefInfo<ParamDef> sorted3 = this.paramDefInfos.GetSorted(num2);
					if (num2 + 1 != (int)this.tablesHeap.ParamPtrTable.Add(new RawParamPtrRow(sorted3.Rid)))
					{
						throw new ModuleWriterException("Invalid param ptr rid");
					}
				}
				this.ReUseDeletedParamRows();
			}
			if (this.eventDefInfos.NeedPtrTable)
			{
				for (int num3 = 0; num3 < this.eventDefInfos.Count; num3++)
				{
					PreserveTokensMetadata.MemberDefInfo<EventDef> sorted4 = this.eventDefInfos.GetSorted(num3);
					if (num3 + 1 != (int)this.tablesHeap.EventPtrTable.Add(new RawEventPtrRow(sorted4.Rid)))
					{
						throw new ModuleWriterException("Invalid event ptr rid");
					}
				}
			}
			if (this.propertyDefInfos.NeedPtrTable)
			{
				for (int num4 = 0; num4 < this.propertyDefInfos.Count; num4++)
				{
					PreserveTokensMetadata.MemberDefInfo<PropertyDef> sorted5 = this.propertyDefInfos.GetSorted(num4);
					if (num4 + 1 != (int)this.tablesHeap.PropertyPtrTable.Add(new RawPropertyPtrRow(sorted5.Rid)))
					{
						throw new ModuleWriterException("Invalid property ptr rid");
					}
				}
			}
			base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, 0.4);
			this.InitializeMethodAndFieldList();
			this.InitializeParamList();
			this.InitializeEventMap();
			this.InitializePropertyMap();
			base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, 0.6);
			if (this.eventDefInfos.NeedPtrTable)
			{
				this.ReUseDeletedEventRows();
			}
			if (this.propertyDefInfos.NeedPtrTable)
			{
				this.ReUseDeletedPropertyRows();
			}
			base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, 0.8);
			this.InitializeTypeRefTableRows();
			this.InitializeTypeSpecTableRows();
			this.InitializeMemberRefTableRows();
			this.InitializeMethodSpecTableRows();
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x001AE828 File Offset: 0x001AE828
		private void ReUseDeletedFieldRows()
		{
			if (this.tablesHeap.FieldPtrTable.IsEmpty)
			{
				return;
			}
			if (this.fieldDefInfos.TableSize == this.tablesHeap.FieldPtrTable.Rows)
			{
				return;
			}
			bool[] array = new bool[this.fieldDefInfos.TableSize];
			for (int i = 0; i < this.fieldDefInfos.Count; i++)
			{
				array[(int)(this.fieldDefInfos.Get(i).Rid - 1U)] = true;
			}
			this.CreateDummyPtrTableType();
			uint signature = base.GetSignature(new FieldSig(this.module.CorLibTypes.Byte));
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					uint num = (uint)(j + 1);
					RawFieldRow value = new RawFieldRow(22, this.stringsHeap.Add(string.Format("f{0:X6}", num)), signature);
					this.tablesHeap.FieldTable[num] = value;
					this.tablesHeap.FieldPtrTable.Create(new RawFieldPtrRow(num));
				}
			}
			if (this.fieldDefInfos.TableSize != this.tablesHeap.FieldPtrTable.Rows)
			{
				throw new ModuleWriterException("Didn't create all dummy fields");
			}
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x001AE974 File Offset: 0x001AE974
		private void ReUseDeletedMethodRows()
		{
			if (this.tablesHeap.MethodPtrTable.IsEmpty)
			{
				return;
			}
			if (this.methodDefInfos.TableSize == this.tablesHeap.MethodPtrTable.Rows)
			{
				return;
			}
			bool[] array = new bool[this.methodDefInfos.TableSize];
			for (int i = 0; i < this.methodDefInfos.Count; i++)
			{
				array[(int)(this.methodDefInfos.Get(i).Rid - 1U)] = true;
			}
			this.CreateDummyPtrTableType();
			uint signature = base.GetSignature(MethodSig.CreateInstance(this.module.CorLibTypes.Void));
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					uint num = (uint)(j + 1);
					RawMethodRow value = new RawMethodRow(0U, 0, 1478, this.stringsHeap.Add(string.Format("m{0:X6}", num)), signature, (uint)this.paramDefInfos.Count);
					this.tablesHeap.MethodTable[num] = value;
					this.tablesHeap.MethodPtrTable.Create(new RawMethodPtrRow(num));
				}
			}
			if (this.methodDefInfos.TableSize != this.tablesHeap.MethodPtrTable.Rows)
			{
				throw new ModuleWriterException("Didn't create all dummy methods");
			}
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x001AEAD0 File Offset: 0x001AEAD0
		private void ReUseDeletedParamRows()
		{
			if (this.tablesHeap.ParamPtrTable.IsEmpty)
			{
				return;
			}
			if (this.paramDefInfos.TableSize == this.tablesHeap.ParamPtrTable.Rows)
			{
				return;
			}
			bool[] array = new bool[this.paramDefInfos.TableSize];
			for (int i = 0; i < this.paramDefInfos.Count; i++)
			{
				array[(int)(this.paramDefInfos.Get(i).Rid - 1U)] = true;
			}
			this.CreateDummyPtrTableType();
			uint signature = base.GetSignature(MethodSig.CreateInstance(this.module.CorLibTypes.Void));
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					uint num = (uint)(j + 1);
					RawParamRow value = new RawParamRow(0, 0, this.stringsHeap.Add(string.Format("p{0:X6}", num)));
					this.tablesHeap.ParamTable[num] = value;
					uint paramList = this.tablesHeap.ParamPtrTable.Create(new RawParamPtrRow(num));
					RawMethodRow row = new RawMethodRow(0U, 0, 1478, this.stringsHeap.Add(string.Format("mp{0:X6}", num)), signature, paramList);
					uint method = this.tablesHeap.MethodTable.Create(row);
					if (this.tablesHeap.MethodPtrTable.Rows > 0)
					{
						this.tablesHeap.MethodPtrTable.Create(new RawMethodPtrRow(method));
					}
				}
			}
			if (this.paramDefInfos.TableSize != this.tablesHeap.ParamPtrTable.Rows)
			{
				throw new ModuleWriterException("Didn't create all dummy params");
			}
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x001AEC94 File Offset: 0x001AEC94
		private void ReUseDeletedEventRows()
		{
			if (this.tablesHeap.EventPtrTable.IsEmpty)
			{
				return;
			}
			if (this.eventDefInfos.TableSize == this.tablesHeap.EventPtrTable.Rows)
			{
				return;
			}
			bool[] array = new bool[this.eventDefInfos.TableSize];
			for (int i = 0; i < this.eventDefInfos.Count; i++)
			{
				array[(int)(this.eventDefInfos.Get(i).Rid - 1U)] = true;
			}
			uint parent = this.CreateDummyPtrTableType();
			this.tablesHeap.EventMapTable.Create(new RawEventMapRow(parent, (uint)(this.tablesHeap.EventPtrTable.Rows + 1)));
			uint eventType = base.AddTypeDefOrRef(this.module.CorLibTypes.Object.TypeDefOrRef);
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					uint num = (uint)(j + 1);
					RawEventRow value = new RawEventRow(0, this.stringsHeap.Add(string.Format("E{0:X6}", num)), eventType);
					this.tablesHeap.EventTable[num] = value;
					this.tablesHeap.EventPtrTable.Create(new RawEventPtrRow(num));
				}
			}
			if (this.eventDefInfos.TableSize != this.tablesHeap.EventPtrTable.Rows)
			{
				throw new ModuleWriterException("Didn't create all dummy events");
			}
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001AEE10 File Offset: 0x001AEE10
		private void ReUseDeletedPropertyRows()
		{
			if (this.tablesHeap.PropertyPtrTable.IsEmpty)
			{
				return;
			}
			if (this.propertyDefInfos.TableSize == this.tablesHeap.PropertyPtrTable.Rows)
			{
				return;
			}
			bool[] array = new bool[this.propertyDefInfos.TableSize];
			for (int i = 0; i < this.propertyDefInfos.Count; i++)
			{
				array[(int)(this.propertyDefInfos.Get(i).Rid - 1U)] = true;
			}
			uint parent = this.CreateDummyPtrTableType();
			this.tablesHeap.PropertyMapTable.Create(new RawPropertyMapRow(parent, (uint)(this.tablesHeap.PropertyPtrTable.Rows + 1)));
			uint signature = base.GetSignature(PropertySig.CreateStatic(this.module.CorLibTypes.Object));
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					uint num = (uint)(j + 1);
					RawPropertyRow value = new RawPropertyRow(0, this.stringsHeap.Add(string.Format("P{0:X6}", num)), signature);
					this.tablesHeap.PropertyTable[num] = value;
					this.tablesHeap.PropertyPtrTable.Create(new RawPropertyPtrRow(num));
				}
			}
			if (this.propertyDefInfos.TableSize != this.tablesHeap.PropertyPtrTable.Rows)
			{
				throw new ModuleWriterException("Didn't create all dummy properties");
			}
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001AEF8C File Offset: 0x001AEF8C
		private uint CreateDummyPtrTableType()
		{
			if (this.dummyPtrTableTypeRid != 0U)
			{
				return this.dummyPtrTableTypeRid;
			}
			TypeAttributes flags = TypeAttributes.Abstract;
			int num = this.fieldDefInfos.NeedPtrTable ? this.fieldDefInfos.Count : this.fieldDefInfos.TableSize;
			int num2 = this.methodDefInfos.NeedPtrTable ? this.methodDefInfos.Count : this.methodDefInfos.TableSize;
			RawTypeDefRow row = new RawTypeDefRow((uint)flags, this.stringsHeap.Add(Guid.NewGuid().ToString("B")), this.stringsHeap.Add("dummy_ptr"), base.AddTypeDefOrRef(this.module.CorLibTypes.Object.TypeDefOrRef), (uint)(num + 1), (uint)(num2 + 1));
			this.dummyPtrTableTypeRid = this.tablesHeap.TypeDefTable.Create(row);
			if (this.dummyPtrTableTypeRid == 1U)
			{
				throw new ModuleWriterException("Dummy ptr type is the first type");
			}
			return this.dummyPtrTableTypeRid;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x001AF0A4 File Offset: 0x001AF0A4
		private void FindMemberDefs()
		{
			Dictionary<object, bool> dictionary = new Dictionary<object, bool>();
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (typeDef != null)
				{
					int num = 0;
					IList<FieldDef> fields = typeDef.Fields;
					int count = fields.Count;
					for (int j = 0; j < count; j++)
					{
						FieldDef fieldDef = fields[j];
						if (fieldDef != null)
						{
							this.fieldDefInfos.Add(fieldDef, num++);
						}
					}
					num = 0;
					IList<MethodDef> methods = typeDef.Methods;
					count = methods.Count;
					for (int k = 0; k < count; k++)
					{
						MethodDef methodDef = methods[k];
						if (methodDef != null)
						{
							this.methodDefInfos.Add(methodDef, num++);
						}
					}
					num = 0;
					IList<EventDef> events = typeDef.Events;
					count = events.Count;
					for (int l = 0; l < count; l++)
					{
						EventDef eventDef = events[l];
						if (eventDef != null && !dictionary.ContainsKey(eventDef))
						{
							dictionary[eventDef] = true;
							this.eventDefInfos.Add(eventDef, num++);
						}
					}
					num = 0;
					IList<PropertyDef> properties = typeDef.Properties;
					count = properties.Count;
					for (int m = 0; m < count; m++)
					{
						PropertyDef propertyDef = properties[m];
						if (propertyDef != null && !dictionary.ContainsKey(propertyDef))
						{
							dictionary[propertyDef] = true;
							this.propertyDefInfos.Add(propertyDef, num++);
						}
					}
				}
			}
			this.fieldDefInfos.SortDefs();
			this.methodDefInfos.SortDefs();
			this.eventDefInfos.SortDefs();
			this.propertyDefInfos.SortDefs();
			for (int n = 0; n < this.methodDefInfos.Count; n++)
			{
				MethodDef def = this.methodDefInfos.Get(n).Def;
				int num = 0;
				foreach (ParamDef paramDef in Metadata.Sort(def.ParamDefs))
				{
					if (paramDef != null)
					{
						this.paramDefInfos.Add(paramDef, num++);
					}
				}
			}
			this.paramDefInfos.SortDefs();
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001AF314 File Offset: 0x001AF314
		private void SortFields()
		{
			this.fieldDefInfos.Sort(delegate(PreserveTokensMetadata.MemberDefInfo<FieldDef> a, PreserveTokensMetadata.MemberDefInfo<FieldDef> b)
			{
				uint num = (a.Def.DeclaringType == null) ? 0U : this.typeToRid[a.Def.DeclaringType];
				uint num2 = (b.Def.DeclaringType == null) ? 0U : this.typeToRid[b.Def.DeclaringType];
				if (num == 0U || num2 == 0U)
				{
					return a.Rid.CompareTo(b.Rid);
				}
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return this.fieldDefInfos.GetCollectionPosition(a.Def).CompareTo(this.fieldDefInfos.GetCollectionPosition(b.Def));
			});
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x001AF330 File Offset: 0x001AF330
		private void SortMethods()
		{
			this.methodDefInfos.Sort(delegate(PreserveTokensMetadata.MemberDefInfo<MethodDef> a, PreserveTokensMetadata.MemberDefInfo<MethodDef> b)
			{
				uint num = (a.Def.DeclaringType == null) ? 0U : this.typeToRid[a.Def.DeclaringType];
				uint num2 = (b.Def.DeclaringType == null) ? 0U : this.typeToRid[b.Def.DeclaringType];
				if (num == 0U || num2 == 0U)
				{
					return a.Rid.CompareTo(b.Rid);
				}
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return this.methodDefInfos.GetCollectionPosition(a.Def).CompareTo(this.methodDefInfos.GetCollectionPosition(b.Def));
			});
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x001AF34C File Offset: 0x001AF34C
		private void SortParameters()
		{
			this.paramDefInfos.Sort(delegate(PreserveTokensMetadata.MemberDefInfo<ParamDef> a, PreserveTokensMetadata.MemberDefInfo<ParamDef> b)
			{
				uint num = (a.Def.DeclaringMethod == null) ? 0U : this.methodDefInfos.Rid(a.Def.DeclaringMethod);
				uint num2 = (b.Def.DeclaringMethod == null) ? 0U : this.methodDefInfos.Rid(b.Def.DeclaringMethod);
				if (num == 0U || num2 == 0U)
				{
					return a.Rid.CompareTo(b.Rid);
				}
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return this.paramDefInfos.GetCollectionPosition(a.Def).CompareTo(this.paramDefInfos.GetCollectionPosition(b.Def));
			});
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x001AF368 File Offset: 0x001AF368
		private void SortEvents()
		{
			this.eventDefInfos.Sort(delegate(PreserveTokensMetadata.MemberDefInfo<EventDef> a, PreserveTokensMetadata.MemberDefInfo<EventDef> b)
			{
				uint num = (a.Def.DeclaringType == null) ? 0U : this.typeToRid[a.Def.DeclaringType];
				uint num2 = (b.Def.DeclaringType == null) ? 0U : this.typeToRid[b.Def.DeclaringType];
				if (num == 0U || num2 == 0U)
				{
					return a.Rid.CompareTo(b.Rid);
				}
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return this.eventDefInfos.GetCollectionPosition(a.Def).CompareTo(this.eventDefInfos.GetCollectionPosition(b.Def));
			});
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x001AF384 File Offset: 0x001AF384
		private void SortProperties()
		{
			this.propertyDefInfos.Sort(delegate(PreserveTokensMetadata.MemberDefInfo<PropertyDef> a, PreserveTokensMetadata.MemberDefInfo<PropertyDef> b)
			{
				uint num = (a.Def.DeclaringType == null) ? 0U : this.typeToRid[a.Def.DeclaringType];
				uint num2 = (b.Def.DeclaringType == null) ? 0U : this.typeToRid[b.Def.DeclaringType];
				if (num == 0U || num2 == 0U)
				{
					return a.Rid.CompareTo(b.Rid);
				}
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return this.propertyDefInfos.GetCollectionPosition(a.Def).CompareTo(this.propertyDefInfos.GetCollectionPosition(b.Def));
			});
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001AF3A0 File Offset: 0x001AF3A0
		private void InitializeMethodAndFieldList()
		{
			uint num = 1U;
			uint num2 = 1U;
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				uint rid = this.typeToRid[typeDef];
				RawTypeDefRow rawTypeDefRow = this.tablesHeap.TypeDefTable[rid];
				rawTypeDefRow = new RawTypeDefRow(rawTypeDefRow.Flags, rawTypeDefRow.Name, rawTypeDefRow.Namespace, rawTypeDefRow.Extends, num, num2);
				this.tablesHeap.TypeDefTable[rid] = rawTypeDefRow;
				num += (uint)typeDef.Fields.Count;
				num2 += (uint)typeDef.Methods.Count;
			}
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001AF454 File Offset: 0x001AF454
		private void InitializeParamList()
		{
			uint num = 1U;
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)this.methodDefInfos.TableSize))
			{
				PreserveTokensMetadata.MemberDefInfo<MethodDef> byRid = this.methodDefInfos.GetByRid(num2);
				RawMethodRow rawMethodRow = this.tablesHeap.MethodTable[num2];
				rawMethodRow = new RawMethodRow(rawMethodRow.RVA, rawMethodRow.ImplFlags, rawMethodRow.Flags, rawMethodRow.Name, rawMethodRow.Signature, num);
				this.tablesHeap.MethodTable[num2] = rawMethodRow;
				if (byRid != null)
				{
					num += (uint)byRid.Def.ParamDefs.Count;
				}
				num2 += 1U;
			}
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x001AF4F8 File Offset: 0x001AF4F8
		private void InitializeEventMap()
		{
			if (!this.tablesHeap.EventMapTable.IsEmpty)
			{
				throw new ModuleWriterException("EventMap table isn't empty");
			}
			TypeDef typeDef = null;
			for (int i = 0; i < this.eventDefInfos.Count; i++)
			{
				PreserveTokensMetadata.MemberDefInfo<EventDef> sorted = this.eventDefInfos.GetSorted(i);
				if (typeDef != sorted.Def.DeclaringType)
				{
					typeDef = sorted.Def.DeclaringType;
					RawEventMapRow row = new RawEventMapRow(this.typeToRid[typeDef], sorted.NewRid);
					uint rid = this.tablesHeap.EventMapTable.Create(row);
					this.eventMapInfos.Add(typeDef, rid);
				}
			}
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001AF5A8 File Offset: 0x001AF5A8
		private void InitializePropertyMap()
		{
			if (!this.tablesHeap.PropertyMapTable.IsEmpty)
			{
				throw new ModuleWriterException("PropertyMap table isn't empty");
			}
			TypeDef typeDef = null;
			for (int i = 0; i < this.propertyDefInfos.Count; i++)
			{
				PreserveTokensMetadata.MemberDefInfo<PropertyDef> sorted = this.propertyDefInfos.GetSorted(i);
				if (typeDef != sorted.Def.DeclaringType)
				{
					typeDef = sorted.Def.DeclaringType;
					RawPropertyMapRow row = new RawPropertyMapRow(this.typeToRid[typeDef], sorted.NewRid);
					uint rid = this.tablesHeap.PropertyMapTable.Create(row);
					this.propertyMapInfos.Add(typeDef, rid);
				}
			}
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x001AF658 File Offset: 0x001AF658
		protected override uint AddTypeRef(TypeRef tr)
		{
			if (tr == null)
			{
				base.Error("TypeRef is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.typeRefInfos.TryGetRid(tr, out num))
			{
				if (num == 0U)
				{
					base.Error("TypeRef {0:X8} has an infinite ResolutionScope loop", new object[]
					{
						tr.MDToken.Raw
					});
				}
				return num;
			}
			this.typeRefInfos.Add(tr, 0U);
			bool flag = base.PreserveTypeRefRids && this.mod.ResolveTypeRef(tr.Rid) == tr;
			RawTypeRefRow rawTypeRefRow = new RawTypeRefRow(base.AddResolutionScope(tr.ResolutionScope), this.stringsHeap.Add(tr.Name), this.stringsHeap.Add(tr.Namespace));
			if (flag)
			{
				num = tr.Rid;
				this.tablesHeap.TypeRefTable[tr.Rid] = rawTypeRefRow;
			}
			else
			{
				num = this.tablesHeap.TypeRefTable.Add(rawTypeRefRow);
			}
			this.typeRefInfos.SetRid(tr, num);
			base.AddCustomAttributes(Table.TypeRef, num, tr);
			base.AddCustomDebugInformationList(Table.TypeRef, num, tr);
			return num;
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001AF788 File Offset: 0x001AF788
		protected override uint AddTypeSpec(TypeSpec ts)
		{
			return this.AddTypeSpec(ts, false);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001AF794 File Offset: 0x001AF794
		private uint AddTypeSpec(TypeSpec ts, bool forceIsOld)
		{
			if (ts == null)
			{
				base.Error("TypeSpec is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.typeSpecInfos.TryGetRid(ts, out num))
			{
				if (num == 0U)
				{
					base.Error("TypeSpec {0:X8} has an infinite TypeSig loop", new object[]
					{
						ts.MDToken.Raw
					});
				}
				return num;
			}
			this.typeSpecInfos.Add(ts, 0U);
			bool flag = forceIsOld || (base.PreserveTypeSpecRids && this.mod.ResolveTypeSpec(ts.Rid) == ts);
			RawTypeSpecRow rawTypeSpecRow = new RawTypeSpecRow(base.GetSignature(ts.TypeSig, ts.ExtraData));
			if (flag)
			{
				num = ts.Rid;
				this.tablesHeap.TypeSpecTable[ts.Rid] = rawTypeSpecRow;
			}
			else
			{
				num = this.tablesHeap.TypeSpecTable.Add(rawTypeSpecRow);
			}
			this.typeSpecInfos.SetRid(ts, num);
			base.AddCustomAttributes(Table.TypeSpec, num, ts);
			base.AddCustomDebugInformationList(Table.TypeSpec, num, ts);
			return num;
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x001AF8B4 File Offset: 0x001AF8B4
		protected override uint AddMemberRef(MemberRef mr)
		{
			return this.AddMemberRef(mr, false);
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x001AF8C0 File Offset: 0x001AF8C0
		private uint AddMemberRef(MemberRef mr, bool forceIsOld)
		{
			if (mr == null)
			{
				base.Error("MemberRef is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.memberRefInfos.TryGetRid(mr, out num))
			{
				return num;
			}
			bool flag = forceIsOld || (base.PreserveMemberRefRids && this.mod.ResolveMemberRef(mr.Rid) == mr);
			RawMemberRefRow rawMemberRefRow = new RawMemberRefRow(base.AddMemberRefParent(mr.Class), this.stringsHeap.Add(mr.Name), base.GetSignature(mr.Signature));
			if (flag)
			{
				num = mr.Rid;
				this.tablesHeap.MemberRefTable[mr.Rid] = rawMemberRefRow;
			}
			else
			{
				num = this.tablesHeap.MemberRefTable.Add(rawMemberRefRow);
			}
			this.memberRefInfos.Add(mr, num);
			base.AddCustomAttributes(Table.MemberRef, num, mr);
			base.AddCustomDebugInformationList(Table.MemberRef, num, mr);
			return num;
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001AF9BC File Offset: 0x001AF9BC
		protected override uint AddStandAloneSig(StandAloneSig sas)
		{
			return this.AddStandAloneSig(sas, false);
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x001AF9C8 File Offset: 0x001AF9C8
		private uint AddStandAloneSig(StandAloneSig sas, bool forceIsOld)
		{
			if (sas == null)
			{
				base.Error("StandAloneSig is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.standAloneSigInfos.TryGetRid(sas, out num))
			{
				return num;
			}
			bool flag = forceIsOld || (base.PreserveStandAloneSigRids && this.mod.ResolveStandAloneSig(sas.Rid) == sas);
			RawStandAloneSigRow rawStandAloneSigRow = new RawStandAloneSigRow(base.GetSignature(sas.Signature));
			if (flag)
			{
				num = sas.Rid;
				this.tablesHeap.StandAloneSigTable[sas.Rid] = rawStandAloneSigRow;
			}
			else
			{
				num = this.tablesHeap.StandAloneSigTable.Add(rawStandAloneSigRow);
			}
			this.standAloneSigInfos.Add(sas, num);
			base.AddCustomAttributes(Table.StandAloneSig, num, sas);
			base.AddCustomDebugInformationList(Table.StandAloneSig, num, sas);
			return num;
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x001AFAA8 File Offset: 0x001AFAA8
		public override MDToken GetToken(IList<TypeSig> locals, uint origToken)
		{
			if (!base.PreserveStandAloneSigRids || !this.IsValidStandAloneSigToken(origToken))
			{
				return base.GetToken(locals, origToken);
			}
			uint num = this.AddStandAloneSig(new LocalSig(locals, false), origToken);
			if (num == 0U)
			{
				return base.GetToken(locals, origToken);
			}
			return new MDToken(Table.StandAloneSig, num);
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x001AFB00 File Offset: 0x001AFB00
		protected override uint AddStandAloneSig(MethodSig methodSig, uint origToken)
		{
			if (!base.PreserveStandAloneSigRids || !this.IsValidStandAloneSigToken(origToken))
			{
				return base.AddStandAloneSig(methodSig, origToken);
			}
			uint num = this.AddStandAloneSig(methodSig, origToken);
			if (num == 0U)
			{
				return base.AddStandAloneSig(methodSig, origToken);
			}
			return num;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x001AFB4C File Offset: 0x001AFB4C
		protected override uint AddStandAloneSig(FieldSig fieldSig, uint origToken)
		{
			if (!base.PreserveStandAloneSigRids || !this.IsValidStandAloneSigToken(origToken))
			{
				return base.AddStandAloneSig(fieldSig, origToken);
			}
			uint num = this.AddStandAloneSig(fieldSig, origToken);
			if (num == 0U)
			{
				return base.AddStandAloneSig(fieldSig, origToken);
			}
			return num;
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x001AFB98 File Offset: 0x001AFB98
		private uint AddStandAloneSig(CallingConventionSig callConvSig, uint origToken)
		{
			uint signature = base.GetSignature(callConvSig);
			uint num;
			if (this.callConvTokenToSignature.TryGetValue(origToken, out num))
			{
				if (signature == num)
				{
					return MDToken.ToRID(origToken);
				}
				base.Warning("Could not preserve StandAloneSig token {0:X8}", new object[]
				{
					origToken
				});
				return 0U;
			}
			else
			{
				uint rid = MDToken.ToRID(origToken);
				StandAloneSig standAloneSig = this.mod.ResolveStandAloneSig(rid);
				if (this.standAloneSigInfos.Exists(standAloneSig))
				{
					base.Warning("StandAloneSig {0:X8} already exists", new object[]
					{
						origToken
					});
					return 0U;
				}
				CallingConventionSig signature2 = standAloneSig.Signature;
				try
				{
					standAloneSig.Signature = callConvSig;
					this.AddStandAloneSig(standAloneSig, true);
				}
				finally
				{
					standAloneSig.Signature = signature2;
				}
				this.callConvTokenToSignature.Add(origToken, signature);
				return MDToken.ToRID(origToken);
			}
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x001AFC74 File Offset: 0x001AFC74
		private bool IsValidStandAloneSigToken(uint token)
		{
			if (MDToken.ToTable(token) != Table.StandAloneSig)
			{
				return false;
			}
			uint rid = MDToken.ToRID(token);
			return this.mod.TablesStream.StandAloneSigTable.IsValidRID(rid);
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x001AFCB4 File Offset: 0x001AFCB4
		protected override uint AddMethodSpec(MethodSpec ms)
		{
			return this.AddMethodSpec(ms, false);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x001AFCC0 File Offset: 0x001AFCC0
		private uint AddMethodSpec(MethodSpec ms, bool forceIsOld)
		{
			if (ms == null)
			{
				base.Error("MethodSpec is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.methodSpecInfos.TryGetRid(ms, out num))
			{
				return num;
			}
			bool flag = forceIsOld || (base.PreserveMethodSpecRids && this.mod.ResolveMethodSpec(ms.Rid) == ms);
			RawMethodSpecRow rawMethodSpecRow = new RawMethodSpecRow(base.AddMethodDefOrRef(ms.Method), base.GetSignature(ms.Instantiation));
			if (flag)
			{
				num = ms.Rid;
				this.tablesHeap.MethodSpecTable[ms.Rid] = rawMethodSpecRow;
			}
			else
			{
				num = this.tablesHeap.MethodSpecTable.Add(rawMethodSpecRow);
			}
			this.methodSpecInfos.Add(ms, num);
			base.AddCustomAttributes(Table.MethodSpec, num, ms);
			base.AddCustomDebugInformationList(Table.MethodSpec, num, ms);
			return num;
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001AFDAC File Offset: 0x001AFDAC
		protected override void BeforeSortingCustomAttributes()
		{
			this.InitializeUninitializedTableRows();
		}

		// Token: 0x04002A3D RID: 10813
		private readonly ModuleDefMD mod;

		// Token: 0x04002A3E RID: 10814
		private readonly Metadata.Rows<TypeRef> typeRefInfos = new Metadata.Rows<TypeRef>();

		// Token: 0x04002A3F RID: 10815
		private readonly Dictionary<TypeDef, uint> typeToRid = new Dictionary<TypeDef, uint>();

		// Token: 0x04002A40 RID: 10816
		private PreserveTokensMetadata.MemberDefDict<FieldDef> fieldDefInfos;

		// Token: 0x04002A41 RID: 10817
		private PreserveTokensMetadata.MemberDefDict<MethodDef> methodDefInfos;

		// Token: 0x04002A42 RID: 10818
		private PreserveTokensMetadata.MemberDefDict<ParamDef> paramDefInfos;

		// Token: 0x04002A43 RID: 10819
		private readonly Metadata.Rows<MemberRef> memberRefInfos = new Metadata.Rows<MemberRef>();

		// Token: 0x04002A44 RID: 10820
		private readonly Metadata.Rows<StandAloneSig> standAloneSigInfos = new Metadata.Rows<StandAloneSig>();

		// Token: 0x04002A45 RID: 10821
		private PreserveTokensMetadata.MemberDefDict<EventDef> eventDefInfos;

		// Token: 0x04002A46 RID: 10822
		private PreserveTokensMetadata.MemberDefDict<PropertyDef> propertyDefInfos;

		// Token: 0x04002A47 RID: 10823
		private readonly Metadata.Rows<TypeSpec> typeSpecInfos = new Metadata.Rows<TypeSpec>();

		// Token: 0x04002A48 RID: 10824
		private readonly Metadata.Rows<MethodSpec> methodSpecInfos = new Metadata.Rows<MethodSpec>();

		// Token: 0x04002A49 RID: 10825
		private readonly Dictionary<uint, uint> callConvTokenToSignature = new Dictionary<uint, uint>();

		// Token: 0x04002A4A RID: 10826
		private bool initdTypeRef;

		// Token: 0x04002A4B RID: 10827
		private bool initdMemberRef;

		// Token: 0x04002A4C RID: 10828
		private bool initdStandAloneSig;

		// Token: 0x04002A4D RID: 10829
		private bool initdTypeSpec;

		// Token: 0x04002A4E RID: 10830
		private bool initdMethodSpec;

		// Token: 0x04002A4F RID: 10831
		private uint dummyPtrTableTypeRid;

		// Token: 0x02001025 RID: 4133
		[DebuggerDisplay("{Rid} -> {NewRid} {Def}")]
		private sealed class MemberDefInfo<T> where T : IMDTokenProvider
		{
			// Token: 0x06008F84 RID: 36740 RVA: 0x002AC624 File Offset: 0x002AC624
			public MemberDefInfo(T def, uint rid)
			{
				this.Def = def;
				this.Rid = rid;
				this.NewRid = rid;
			}

			// Token: 0x040044CB RID: 17611
			public readonly T Def;

			// Token: 0x040044CC RID: 17612
			public uint Rid;

			// Token: 0x040044CD RID: 17613
			public uint NewRid;
		}

		// Token: 0x02001026 RID: 4134
		[DebuggerDisplay("Count = {Count}")]
		private sealed class MemberDefDict<T> where T : IMDTokenProvider
		{
			// Token: 0x17001E0A RID: 7690
			// (get) Token: 0x06008F85 RID: 36741 RVA: 0x002AC644 File Offset: 0x002AC644
			public int Count
			{
				get
				{
					return this.defs.Count;
				}
			}

			// Token: 0x17001E0B RID: 7691
			// (get) Token: 0x06008F86 RID: 36742 RVA: 0x002AC654 File Offset: 0x002AC654
			public int TableSize
			{
				get
				{
					return this.tableSize;
				}
			}

			// Token: 0x17001E0C RID: 7692
			// (get) Token: 0x06008F87 RID: 36743 RVA: 0x002AC65C File Offset: 0x002AC65C
			public bool NeedPtrTable
			{
				get
				{
					return this.preserveRids && !this.wasSorted;
				}
			}

			// Token: 0x06008F88 RID: 36744 RVA: 0x002AC674 File Offset: 0x002AC674
			public MemberDefDict(Type defMDType, bool preserveRids) : this(defMDType, preserveRids, false)
			{
			}

			// Token: 0x06008F89 RID: 36745 RVA: 0x002AC680 File Offset: 0x002AC680
			public MemberDefDict(Type defMDType, bool preserveRids, bool enableRidToInfo)
			{
				this.defMDType = defMDType;
				this.preserveRids = preserveRids;
				this.enableRidToInfo = enableRidToInfo;
			}

			// Token: 0x06008F8A RID: 36746 RVA: 0x002AC6E0 File Offset: 0x002AC6E0
			public uint Rid(T def)
			{
				return this.defToInfo[def].Rid;
			}

			// Token: 0x06008F8B RID: 36747 RVA: 0x002AC6F4 File Offset: 0x002AC6F4
			public bool TryGetRid(T def, out uint rid)
			{
				PreserveTokensMetadata.MemberDefInfo<T> memberDefInfo;
				if (def == null || !this.defToInfo.TryGetValue(def, out memberDefInfo))
				{
					rid = 0U;
					return false;
				}
				rid = memberDefInfo.Rid;
				return true;
			}

			// Token: 0x06008F8C RID: 36748 RVA: 0x002AC734 File Offset: 0x002AC734
			public void Sort(Comparison<PreserveTokensMetadata.MemberDefInfo<T>> comparer)
			{
				if (!this.preserveRids)
				{
					this.sortedDefs = this.defs;
					return;
				}
				this.sortedDefs = new List<PreserveTokensMetadata.MemberDefInfo<T>>(this.defs);
				this.sortedDefs.Sort(comparer);
				this.wasSorted = true;
				for (int i = 0; i < this.sortedDefs.Count; i++)
				{
					PreserveTokensMetadata.MemberDefInfo<T> memberDefInfo = this.sortedDefs[i];
					uint num = (uint)(i + 1);
					memberDefInfo.NewRid = num;
					if (memberDefInfo.Rid != num)
					{
						this.wasSorted = false;
					}
				}
			}

			// Token: 0x06008F8D RID: 36749 RVA: 0x002AC7C4 File Offset: 0x002AC7C4
			public PreserveTokensMetadata.MemberDefInfo<T> Get(int i)
			{
				return this.defs[i];
			}

			// Token: 0x06008F8E RID: 36750 RVA: 0x002AC7D4 File Offset: 0x002AC7D4
			public PreserveTokensMetadata.MemberDefInfo<T> GetSorted(int i)
			{
				return this.sortedDefs[i];
			}

			// Token: 0x06008F8F RID: 36751 RVA: 0x002AC7E4 File Offset: 0x002AC7E4
			public PreserveTokensMetadata.MemberDefInfo<T> GetByRid(uint rid)
			{
				PreserveTokensMetadata.MemberDefInfo<T> result;
				this.ridToInfo.TryGetValue(rid, out result);
				return result;
			}

			// Token: 0x06008F90 RID: 36752 RVA: 0x002AC808 File Offset: 0x002AC808
			public void Add(T def, int collPos)
			{
				uint rid;
				if (def.GetType() == this.defMDType)
				{
					this.numDefMDs++;
					uint num2;
					if (!this.preserveRids)
					{
						uint num = this.newRid;
						this.newRid = num + 1U;
						num2 = num;
					}
					else
					{
						num2 = def.Rid;
					}
					rid = num2;
				}
				else
				{
					this.numDefUsers++;
					uint num3;
					if (!this.preserveRids)
					{
						uint num = this.newRid;
						this.newRid = num + 1U;
						num3 = num;
					}
					else
					{
						uint num = this.userRid;
						this.userRid = num + 1U;
						num3 = num;
					}
					rid = num3;
				}
				PreserveTokensMetadata.MemberDefInfo<T> memberDefInfo = new PreserveTokensMetadata.MemberDefInfo<T>(def, rid);
				this.defToInfo[def] = memberDefInfo;
				this.defs.Add(memberDefInfo);
				this.collectionPositions.Add(def, collPos);
			}

			// Token: 0x06008F91 RID: 36753 RVA: 0x002AC8E8 File Offset: 0x002AC8E8
			public void SortDefs()
			{
				if (this.preserveRids)
				{
					this.defs.Sort((PreserveTokensMetadata.MemberDefInfo<T> a, PreserveTokensMetadata.MemberDefInfo<T> b) => a.Rid.CompareTo(b.Rid));
					uint num = (this.numDefMDs == 0) ? 1U : (this.defs[this.numDefMDs - 1].Rid + 1U);
					for (int i = this.numDefMDs; i < this.defs.Count; i++)
					{
						this.defs[i].Rid = num++;
					}
					this.tableSize = (int)(num - 1U);
				}
				else
				{
					this.tableSize = this.defs.Count;
				}
				if (this.enableRidToInfo)
				{
					this.ridToInfo = new Dictionary<uint, PreserveTokensMetadata.MemberDefInfo<T>>(this.defs.Count);
					foreach (PreserveTokensMetadata.MemberDefInfo<T> memberDefInfo in this.defs)
					{
						this.ridToInfo.Add(memberDefInfo.Rid, memberDefInfo);
					}
				}
				if (this.tableSize > 16777215)
				{
					throw new ModuleWriterException("Table is too big");
				}
			}

			// Token: 0x06008F92 RID: 36754 RVA: 0x002ACA40 File Offset: 0x002ACA40
			public int GetCollectionPosition(T def)
			{
				return this.collectionPositions[def];
			}

			// Token: 0x040044CE RID: 17614
			private readonly Type defMDType;

			// Token: 0x040044CF RID: 17615
			private uint userRid = 16777216U;

			// Token: 0x040044D0 RID: 17616
			private uint newRid = 1U;

			// Token: 0x040044D1 RID: 17617
			private int numDefMDs;

			// Token: 0x040044D2 RID: 17618
			private int numDefUsers;

			// Token: 0x040044D3 RID: 17619
			private int tableSize;

			// Token: 0x040044D4 RID: 17620
			private bool wasSorted;

			// Token: 0x040044D5 RID: 17621
			private readonly bool preserveRids;

			// Token: 0x040044D6 RID: 17622
			private readonly bool enableRidToInfo;

			// Token: 0x040044D7 RID: 17623
			private readonly Dictionary<T, PreserveTokensMetadata.MemberDefInfo<T>> defToInfo = new Dictionary<T, PreserveTokensMetadata.MemberDefInfo<T>>();

			// Token: 0x040044D8 RID: 17624
			private Dictionary<uint, PreserveTokensMetadata.MemberDefInfo<T>> ridToInfo;

			// Token: 0x040044D9 RID: 17625
			private readonly List<PreserveTokensMetadata.MemberDefInfo<T>> defs = new List<PreserveTokensMetadata.MemberDefInfo<T>>();

			// Token: 0x040044DA RID: 17626
			private List<PreserveTokensMetadata.MemberDefInfo<T>> sortedDefs;

			// Token: 0x040044DB RID: 17627
			private readonly Dictionary<T, int> collectionPositions = new Dictionary<T, int>();
		}
	}
}
