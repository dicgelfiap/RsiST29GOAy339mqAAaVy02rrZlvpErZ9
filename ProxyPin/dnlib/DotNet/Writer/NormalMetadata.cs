using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CC RID: 2252
	internal sealed class NormalMetadata : Metadata
	{
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x001ABBD8 File Offset: 0x001ABBD8
		protected override int NumberOfMethods
		{
			get
			{
				return this.methodDefInfos.Count;
			}
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x001ABBE8 File Offset: 0x001ABBE8
		public NormalMetadata(ModuleDef module, UniqueChunkList<ByteArrayChunk> constants, MethodBodyChunks methodBodies, NetResources netResources, MetadataOptions options, DebugMetadataKind debugKind, bool isStandaloneDebugMetadata) : base(module, constants, methodBodies, netResources, options, debugKind, isStandaloneDebugMetadata)
		{
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x001ABC84 File Offset: 0x001ABC84
		protected override TypeDef[] GetAllTypeDefs()
		{
			return this.module.GetTypes().ToArray<TypeDef>();
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x001ABC98 File Offset: 0x001ABC98
		protected override void AllocateTypeDefRids()
		{
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (typeDef != null)
				{
					uint rid = this.tablesHeap.TypeDefTable.Create(default(RawTypeDefRow));
					this.typeDefInfos.Add(typeDef, rid);
				}
			}
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x001ABCF8 File Offset: 0x001ABCF8
		protected override void AllocateMemberDefRids()
		{
			int num = this.allTypeDefs.Length;
			int num2 = 0;
			int num3 = 0;
			int num4 = num / 5;
			uint fieldList = 1U;
			uint methodList = 1U;
			uint eventList = 1U;
			uint propertyList = 1U;
			uint paramList = 1U;
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (num2++ == num4 && num3 < 5)
				{
					base.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids, (double)num2 / (double)num);
					num3++;
					num4 = (int)((double)num / 5.0 * (double)(num3 + 1));
				}
				if (typeDef != null)
				{
					uint rid = this.GetRid(typeDef);
					RawTypeDefRow rawTypeDefRow = this.tablesHeap.TypeDefTable[rid];
					rawTypeDefRow = new RawTypeDefRow(rawTypeDefRow.Flags, rawTypeDefRow.Name, rawTypeDefRow.Namespace, rawTypeDefRow.Extends, fieldList, methodList);
					this.tablesHeap.TypeDefTable[rid] = rawTypeDefRow;
					IList<FieldDef> fields = typeDef.Fields;
					int count = fields.Count;
					for (int j = 0; j < count; j++)
					{
						FieldDef fieldDef = fields[j];
						if (fieldDef != null)
						{
							uint num5 = fieldList++;
							if (num5 != this.tablesHeap.FieldTable.Create(default(RawFieldRow)))
							{
								throw new ModuleWriterException("Invalid field rid");
							}
							this.fieldDefInfos.Add(fieldDef, num5);
						}
					}
					IList<MethodDef> methods = typeDef.Methods;
					count = methods.Count;
					for (int k = 0; k < count; k++)
					{
						MethodDef methodDef = methods[k];
						if (methodDef != null)
						{
							uint num6 = methodList++;
							RawMethodRow row = new RawMethodRow(0U, 0, 0, 0U, 0U, paramList);
							if (num6 != this.tablesHeap.MethodTable.Create(row))
							{
								throw new ModuleWriterException("Invalid method rid");
							}
							this.methodDefInfos.Add(methodDef, num6);
							foreach (ParamDef paramDef in Metadata.Sort(methodDef.ParamDefs))
							{
								if (paramDef != null)
								{
									uint num7 = paramList++;
									if (num7 != this.tablesHeap.ParamTable.Create(default(RawParamRow)))
									{
										throw new ModuleWriterException("Invalid param rid");
									}
									this.paramDefInfos.Add(paramDef, num7);
								}
							}
						}
					}
					if (!Metadata.IsEmpty<EventDef>(typeDef.Events))
					{
						uint rid2 = this.tablesHeap.EventMapTable.Create(new RawEventMapRow(rid, eventList));
						this.eventMapInfos.Add(typeDef, rid2);
						IList<EventDef> events = typeDef.Events;
						count = events.Count;
						for (int l = 0; l < count; l++)
						{
							EventDef eventDef = events[l];
							if (eventDef != null)
							{
								uint num8 = eventList++;
								if (num8 != this.tablesHeap.EventTable.Create(default(RawEventRow)))
								{
									throw new ModuleWriterException("Invalid event rid");
								}
								this.eventDefInfos.Add(eventDef, num8);
							}
						}
					}
					if (!Metadata.IsEmpty<PropertyDef>(typeDef.Properties))
					{
						uint rid3 = this.tablesHeap.PropertyMapTable.Create(new RawPropertyMapRow(rid, propertyList));
						this.propertyMapInfos.Add(typeDef, rid3);
						IList<PropertyDef> properties = typeDef.Properties;
						count = properties.Count;
						for (int m = 0; m < count; m++)
						{
							PropertyDef propertyDef = properties[m];
							if (propertyDef != null)
							{
								uint num9 = propertyList++;
								if (num9 != this.tablesHeap.PropertyTable.Create(default(RawPropertyRow)))
								{
									throw new ModuleWriterException("Invalid property rid");
								}
								this.propertyDefInfos.Add(propertyDef, num9);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x001AC0F0 File Offset: 0x001AC0F0
		public override uint GetRid(TypeRef tr)
		{
			uint result;
			this.typeRefInfos.TryGetRid(tr, out result);
			return result;
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x001AC114 File Offset: 0x001AC114
		public override uint GetRid(TypeDef td)
		{
			uint result;
			if (this.typeDefInfos.TryGetRid(td, out result))
			{
				return result;
			}
			if (td == null)
			{
				base.Error("TypeDef is null", new object[0]);
			}
			else
			{
				base.Error("TypeDef {0} ({1:X8}) is not defined in this module ({2}). A type was removed that is still referenced by this module.", new object[]
				{
					td,
					td.MDToken.Raw,
					this.module
				});
			}
			return 0U;
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x001AC18C File Offset: 0x001AC18C
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

		// Token: 0x06005779 RID: 22393 RVA: 0x001AC204 File Offset: 0x001AC204
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

		// Token: 0x0600577A RID: 22394 RVA: 0x001AC27C File Offset: 0x001AC27C
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

		// Token: 0x0600577B RID: 22395 RVA: 0x001AC2F4 File Offset: 0x001AC2F4
		public override uint GetRid(MemberRef mr)
		{
			uint result;
			this.memberRefInfos.TryGetRid(mr, out result);
			return result;
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001AC318 File Offset: 0x001AC318
		public override uint GetRid(StandAloneSig sas)
		{
			uint result;
			this.standAloneSigInfos.TryGetRid(sas, out result);
			return result;
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x001AC33C File Offset: 0x001AC33C
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

		// Token: 0x0600577E RID: 22398 RVA: 0x001AC3B4 File Offset: 0x001AC3B4
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

		// Token: 0x0600577F RID: 22399 RVA: 0x001AC42C File Offset: 0x001AC42C
		public override uint GetRid(TypeSpec ts)
		{
			uint result;
			this.typeSpecInfos.TryGetRid(ts, out result);
			return result;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x001AC450 File Offset: 0x001AC450
		public override uint GetRid(MethodSpec ms)
		{
			uint result;
			this.methodSpecInfos.TryGetRid(ms, out result);
			return result;
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x001AC474 File Offset: 0x001AC474
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
			RawTypeRefRow row = new RawTypeRefRow(base.AddResolutionScope(tr.ResolutionScope), this.stringsHeap.Add(tr.Name), this.stringsHeap.Add(tr.Namespace));
			num = this.tablesHeap.TypeRefTable.Add(row);
			this.typeRefInfos.SetRid(tr, num);
			base.AddCustomAttributes(Table.TypeRef, num, tr);
			base.AddCustomDebugInformationList(Table.TypeRef, num, tr);
			return num;
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x001AC554 File Offset: 0x001AC554
		protected override uint AddTypeSpec(TypeSpec ts)
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
			RawTypeSpecRow row = new RawTypeSpecRow(base.GetSignature(ts.TypeSig, ts.ExtraData));
			num = this.tablesHeap.TypeSpecTable.Add(row);
			this.typeSpecInfos.SetRid(ts, num);
			base.AddCustomAttributes(Table.TypeSpec, num, ts);
			base.AddCustomDebugInformationList(Table.TypeSpec, num, ts);
			return num;
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x001AC61C File Offset: 0x001AC61C
		protected override uint AddMemberRef(MemberRef mr)
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
			RawMemberRefRow row = new RawMemberRefRow(base.AddMemberRefParent(mr.Class), this.stringsHeap.Add(mr.Name), base.GetSignature(mr.Signature));
			num = this.tablesHeap.MemberRefTable.Add(row);
			this.memberRefInfos.Add(mr, num);
			base.AddCustomAttributes(Table.MemberRef, num, mr);
			base.AddCustomDebugInformationList(Table.MemberRef, num, mr);
			return num;
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001AC6C0 File Offset: 0x001AC6C0
		protected override uint AddStandAloneSig(StandAloneSig sas)
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
			RawStandAloneSigRow row = new RawStandAloneSigRow(base.GetSignature(sas.Signature));
			num = this.tablesHeap.StandAloneSigTable.Add(row);
			this.standAloneSigInfos.Add(sas, num);
			base.AddCustomAttributes(Table.StandAloneSig, num, sas);
			base.AddCustomDebugInformationList(Table.StandAloneSig, num, sas);
			return num;
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x001AC748 File Offset: 0x001AC748
		protected override uint AddMethodSpec(MethodSpec ms)
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
			RawMethodSpecRow row = new RawMethodSpecRow(base.AddMethodDefOrRef(ms.Method), base.GetSignature(ms.Instantiation));
			num = this.tablesHeap.MethodSpecTable.Add(row);
			this.methodSpecInfos.Add(ms, num);
			base.AddCustomAttributes(Table.MethodSpec, num, ms);
			base.AddCustomDebugInformationList(Table.MethodSpec, num, ms);
			return num;
		}

		// Token: 0x040029F5 RID: 10741
		private readonly Metadata.Rows<TypeRef> typeRefInfos = new Metadata.Rows<TypeRef>();

		// Token: 0x040029F6 RID: 10742
		private readonly Metadata.Rows<TypeDef> typeDefInfos = new Metadata.Rows<TypeDef>();

		// Token: 0x040029F7 RID: 10743
		private readonly Metadata.Rows<FieldDef> fieldDefInfos = new Metadata.Rows<FieldDef>();

		// Token: 0x040029F8 RID: 10744
		private readonly Metadata.Rows<MethodDef> methodDefInfos = new Metadata.Rows<MethodDef>();

		// Token: 0x040029F9 RID: 10745
		private readonly Metadata.Rows<ParamDef> paramDefInfos = new Metadata.Rows<ParamDef>();

		// Token: 0x040029FA RID: 10746
		private readonly Metadata.Rows<MemberRef> memberRefInfos = new Metadata.Rows<MemberRef>();

		// Token: 0x040029FB RID: 10747
		private readonly Metadata.Rows<StandAloneSig> standAloneSigInfos = new Metadata.Rows<StandAloneSig>();

		// Token: 0x040029FC RID: 10748
		private readonly Metadata.Rows<EventDef> eventDefInfos = new Metadata.Rows<EventDef>();

		// Token: 0x040029FD RID: 10749
		private readonly Metadata.Rows<PropertyDef> propertyDefInfos = new Metadata.Rows<PropertyDef>();

		// Token: 0x040029FE RID: 10750
		private readonly Metadata.Rows<TypeSpec> typeSpecInfos = new Metadata.Rows<TypeSpec>();

		// Token: 0x040029FF RID: 10751
		private readonly Metadata.Rows<MethodSpec> methodSpecInfos = new Metadata.Rows<MethodSpec>();
	}
}
