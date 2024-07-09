using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007A6 RID: 1958
	internal sealed class EventDefMD : EventDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x0016F830 File Offset: 0x0016F830
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0016F838 File Offset: 0x0016F838
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Event, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0016F8AC File Offset: 0x0016F8AC
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), new GenericParamContext(this.declaringType2), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0016F904 File Offset: 0x0016F904
		public EventDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawEventRow rawEventRow;
			readerModule.TablesStream.TryReadEventRow(this.origRid, out rawEventRow);
			this.attributes = (int)rawEventRow.EventFlags;
			this.name = readerModule.StringsStream.ReadNoNull(rawEventRow.Name);
			this.declaringType2 = readerModule.GetOwnerType(this);
			this.eventType = readerModule.ResolveTypeDefOrRef(rawEventRow.EventType, new GenericParamContext(this.declaringType2));
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x0016F994 File Offset: 0x0016F994
		internal EventDefMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.Attributes);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.EventType);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			MemberMDInitializer.Initialize(base.AddMethod);
			MemberMDInitializer.Initialize(base.InvokeMethod);
			MemberMDInitializer.Initialize(base.RemoveMethod);
			MemberMDInitializer.Initialize<MethodDef>(base.OtherMethods);
			MemberMDInitializer.Initialize(base.DeclaringType);
			return this;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0016FA10 File Offset: 0x0016FA10
		protected override void InitializeEventMethods_NoLock()
		{
			TypeDefMD typeDefMD = this.declaringType2 as TypeDefMD;
			IList<MethodDef> otherMethods;
			if (typeDefMD == null)
			{
				otherMethods = new List<MethodDef>();
			}
			else
			{
				typeDefMD.InitializeEvent(this, out this.addMethod, out this.invokeMethod, out this.removeMethod, out otherMethods);
			}
			this.otherMethods = otherMethods;
		}

		// Token: 0x04002496 RID: 9366
		private readonly ModuleDefMD readerModule;

		// Token: 0x04002497 RID: 9367
		private readonly uint origRid;
	}
}
