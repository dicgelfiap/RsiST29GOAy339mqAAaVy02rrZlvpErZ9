using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D6 RID: 2518
	[ComVisible(true)]
	public sealed class TablesStream : DotNetStream
	{
		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x001CA010 File Offset: 0x001CA010
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x001CA018 File Offset: 0x001CA018
		public MDTable ModuleTable { get; private set; }

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x001CA024 File Offset: 0x001CA024
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x001CA02C File Offset: 0x001CA02C
		public MDTable TypeRefTable { get; private set; }

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x001CA038 File Offset: 0x001CA038
		// (set) Token: 0x06005FDA RID: 24538 RVA: 0x001CA040 File Offset: 0x001CA040
		public MDTable TypeDefTable { get; private set; }

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x001CA04C File Offset: 0x001CA04C
		// (set) Token: 0x06005FDC RID: 24540 RVA: 0x001CA054 File Offset: 0x001CA054
		public MDTable FieldPtrTable { get; private set; }

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x001CA060 File Offset: 0x001CA060
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x001CA068 File Offset: 0x001CA068
		public MDTable FieldTable { get; private set; }

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x001CA074 File Offset: 0x001CA074
		// (set) Token: 0x06005FE0 RID: 24544 RVA: 0x001CA07C File Offset: 0x001CA07C
		public MDTable MethodPtrTable { get; private set; }

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x001CA088 File Offset: 0x001CA088
		// (set) Token: 0x06005FE2 RID: 24546 RVA: 0x001CA090 File Offset: 0x001CA090
		public MDTable MethodTable { get; private set; }

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x001CA09C File Offset: 0x001CA09C
		// (set) Token: 0x06005FE4 RID: 24548 RVA: 0x001CA0A4 File Offset: 0x001CA0A4
		public MDTable ParamPtrTable { get; private set; }

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x001CA0B0 File Offset: 0x001CA0B0
		// (set) Token: 0x06005FE6 RID: 24550 RVA: 0x001CA0B8 File Offset: 0x001CA0B8
		public MDTable ParamTable { get; private set; }

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06005FE7 RID: 24551 RVA: 0x001CA0C4 File Offset: 0x001CA0C4
		// (set) Token: 0x06005FE8 RID: 24552 RVA: 0x001CA0CC File Offset: 0x001CA0CC
		public MDTable InterfaceImplTable { get; private set; }

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005FE9 RID: 24553 RVA: 0x001CA0D8 File Offset: 0x001CA0D8
		// (set) Token: 0x06005FEA RID: 24554 RVA: 0x001CA0E0 File Offset: 0x001CA0E0
		public MDTable MemberRefTable { get; private set; }

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06005FEB RID: 24555 RVA: 0x001CA0EC File Offset: 0x001CA0EC
		// (set) Token: 0x06005FEC RID: 24556 RVA: 0x001CA0F4 File Offset: 0x001CA0F4
		public MDTable ConstantTable { get; private set; }

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x001CA100 File Offset: 0x001CA100
		// (set) Token: 0x06005FEE RID: 24558 RVA: 0x001CA108 File Offset: 0x001CA108
		public MDTable CustomAttributeTable { get; private set; }

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x001CA114 File Offset: 0x001CA114
		// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x001CA11C File Offset: 0x001CA11C
		public MDTable FieldMarshalTable { get; private set; }

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06005FF1 RID: 24561 RVA: 0x001CA128 File Offset: 0x001CA128
		// (set) Token: 0x06005FF2 RID: 24562 RVA: 0x001CA130 File Offset: 0x001CA130
		public MDTable DeclSecurityTable { get; private set; }

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x001CA13C File Offset: 0x001CA13C
		// (set) Token: 0x06005FF4 RID: 24564 RVA: 0x001CA144 File Offset: 0x001CA144
		public MDTable ClassLayoutTable { get; private set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x001CA150 File Offset: 0x001CA150
		// (set) Token: 0x06005FF6 RID: 24566 RVA: 0x001CA158 File Offset: 0x001CA158
		public MDTable FieldLayoutTable { get; private set; }

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x001CA164 File Offset: 0x001CA164
		// (set) Token: 0x06005FF8 RID: 24568 RVA: 0x001CA16C File Offset: 0x001CA16C
		public MDTable StandAloneSigTable { get; private set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x001CA178 File Offset: 0x001CA178
		// (set) Token: 0x06005FFA RID: 24570 RVA: 0x001CA180 File Offset: 0x001CA180
		public MDTable EventMapTable { get; private set; }

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x001CA18C File Offset: 0x001CA18C
		// (set) Token: 0x06005FFC RID: 24572 RVA: 0x001CA194 File Offset: 0x001CA194
		public MDTable EventPtrTable { get; private set; }

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x001CA1A0 File Offset: 0x001CA1A0
		// (set) Token: 0x06005FFE RID: 24574 RVA: 0x001CA1A8 File Offset: 0x001CA1A8
		public MDTable EventTable { get; private set; }

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x001CA1B4 File Offset: 0x001CA1B4
		// (set) Token: 0x06006000 RID: 24576 RVA: 0x001CA1BC File Offset: 0x001CA1BC
		public MDTable PropertyMapTable { get; private set; }

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x001CA1C8 File Offset: 0x001CA1C8
		// (set) Token: 0x06006002 RID: 24578 RVA: 0x001CA1D0 File Offset: 0x001CA1D0
		public MDTable PropertyPtrTable { get; private set; }

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x001CA1DC File Offset: 0x001CA1DC
		// (set) Token: 0x06006004 RID: 24580 RVA: 0x001CA1E4 File Offset: 0x001CA1E4
		public MDTable PropertyTable { get; private set; }

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x001CA1F0 File Offset: 0x001CA1F0
		// (set) Token: 0x06006006 RID: 24582 RVA: 0x001CA1F8 File Offset: 0x001CA1F8
		public MDTable MethodSemanticsTable { get; private set; }

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x001CA204 File Offset: 0x001CA204
		// (set) Token: 0x06006008 RID: 24584 RVA: 0x001CA20C File Offset: 0x001CA20C
		public MDTable MethodImplTable { get; private set; }

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x001CA218 File Offset: 0x001CA218
		// (set) Token: 0x0600600A RID: 24586 RVA: 0x001CA220 File Offset: 0x001CA220
		public MDTable ModuleRefTable { get; private set; }

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x001CA22C File Offset: 0x001CA22C
		// (set) Token: 0x0600600C RID: 24588 RVA: 0x001CA234 File Offset: 0x001CA234
		public MDTable TypeSpecTable { get; private set; }

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x001CA240 File Offset: 0x001CA240
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x001CA248 File Offset: 0x001CA248
		public MDTable ImplMapTable { get; private set; }

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x001CA254 File Offset: 0x001CA254
		// (set) Token: 0x06006010 RID: 24592 RVA: 0x001CA25C File Offset: 0x001CA25C
		public MDTable FieldRVATable { get; private set; }

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06006011 RID: 24593 RVA: 0x001CA268 File Offset: 0x001CA268
		// (set) Token: 0x06006012 RID: 24594 RVA: 0x001CA270 File Offset: 0x001CA270
		public MDTable ENCLogTable { get; private set; }

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x001CA27C File Offset: 0x001CA27C
		// (set) Token: 0x06006014 RID: 24596 RVA: 0x001CA284 File Offset: 0x001CA284
		public MDTable ENCMapTable { get; private set; }

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x001CA290 File Offset: 0x001CA290
		// (set) Token: 0x06006016 RID: 24598 RVA: 0x001CA298 File Offset: 0x001CA298
		public MDTable AssemblyTable { get; private set; }

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x001CA2A4 File Offset: 0x001CA2A4
		// (set) Token: 0x06006018 RID: 24600 RVA: 0x001CA2AC File Offset: 0x001CA2AC
		public MDTable AssemblyProcessorTable { get; private set; }

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x001CA2B8 File Offset: 0x001CA2B8
		// (set) Token: 0x0600601A RID: 24602 RVA: 0x001CA2C0 File Offset: 0x001CA2C0
		public MDTable AssemblyOSTable { get; private set; }

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x001CA2CC File Offset: 0x001CA2CC
		// (set) Token: 0x0600601C RID: 24604 RVA: 0x001CA2D4 File Offset: 0x001CA2D4
		public MDTable AssemblyRefTable { get; private set; }

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x001CA2E0 File Offset: 0x001CA2E0
		// (set) Token: 0x0600601E RID: 24606 RVA: 0x001CA2E8 File Offset: 0x001CA2E8
		public MDTable AssemblyRefProcessorTable { get; private set; }

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x001CA2F4 File Offset: 0x001CA2F4
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x001CA2FC File Offset: 0x001CA2FC
		public MDTable AssemblyRefOSTable { get; private set; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x001CA308 File Offset: 0x001CA308
		// (set) Token: 0x06006022 RID: 24610 RVA: 0x001CA310 File Offset: 0x001CA310
		public MDTable FileTable { get; private set; }

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x001CA31C File Offset: 0x001CA31C
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x001CA324 File Offset: 0x001CA324
		public MDTable ExportedTypeTable { get; private set; }

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x001CA330 File Offset: 0x001CA330
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x001CA338 File Offset: 0x001CA338
		public MDTable ManifestResourceTable { get; private set; }

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x001CA344 File Offset: 0x001CA344
		// (set) Token: 0x06006028 RID: 24616 RVA: 0x001CA34C File Offset: 0x001CA34C
		public MDTable NestedClassTable { get; private set; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x001CA358 File Offset: 0x001CA358
		// (set) Token: 0x0600602A RID: 24618 RVA: 0x001CA360 File Offset: 0x001CA360
		public MDTable GenericParamTable { get; private set; }

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x001CA36C File Offset: 0x001CA36C
		// (set) Token: 0x0600602C RID: 24620 RVA: 0x001CA374 File Offset: 0x001CA374
		public MDTable MethodSpecTable { get; private set; }

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x001CA380 File Offset: 0x001CA380
		// (set) Token: 0x0600602E RID: 24622 RVA: 0x001CA388 File Offset: 0x001CA388
		public MDTable GenericParamConstraintTable { get; private set; }

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x001CA394 File Offset: 0x001CA394
		// (set) Token: 0x06006030 RID: 24624 RVA: 0x001CA39C File Offset: 0x001CA39C
		public MDTable DocumentTable { get; private set; }

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x001CA3A8 File Offset: 0x001CA3A8
		// (set) Token: 0x06006032 RID: 24626 RVA: 0x001CA3B0 File Offset: 0x001CA3B0
		public MDTable MethodDebugInformationTable { get; private set; }

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x001CA3BC File Offset: 0x001CA3BC
		// (set) Token: 0x06006034 RID: 24628 RVA: 0x001CA3C4 File Offset: 0x001CA3C4
		public MDTable LocalScopeTable { get; private set; }

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06006035 RID: 24629 RVA: 0x001CA3D0 File Offset: 0x001CA3D0
		// (set) Token: 0x06006036 RID: 24630 RVA: 0x001CA3D8 File Offset: 0x001CA3D8
		public MDTable LocalVariableTable { get; private set; }

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06006037 RID: 24631 RVA: 0x001CA3E4 File Offset: 0x001CA3E4
		// (set) Token: 0x06006038 RID: 24632 RVA: 0x001CA3EC File Offset: 0x001CA3EC
		public MDTable LocalConstantTable { get; private set; }

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06006039 RID: 24633 RVA: 0x001CA3F8 File Offset: 0x001CA3F8
		// (set) Token: 0x0600603A RID: 24634 RVA: 0x001CA400 File Offset: 0x001CA400
		public MDTable ImportScopeTable { get; private set; }

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x001CA40C File Offset: 0x001CA40C
		// (set) Token: 0x0600603C RID: 24636 RVA: 0x001CA414 File Offset: 0x001CA414
		public MDTable StateMachineMethodTable { get; private set; }

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x001CA420 File Offset: 0x001CA420
		// (set) Token: 0x0600603E RID: 24638 RVA: 0x001CA428 File Offset: 0x001CA428
		public MDTable CustomDebugInformationTable { get; private set; }

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x001CA434 File Offset: 0x001CA434
		// (set) Token: 0x06006040 RID: 24640 RVA: 0x001CA43C File Offset: 0x001CA43C
		public IColumnReader ColumnReader
		{
			get
			{
				return this.columnReader;
			}
			set
			{
				this.columnReader = value;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06006041 RID: 24641 RVA: 0x001CA448 File Offset: 0x001CA448
		// (set) Token: 0x06006042 RID: 24642 RVA: 0x001CA450 File Offset: 0x001CA450
		public IRowReader<RawMethodRow> MethodRowReader
		{
			get
			{
				return this.methodRowReader;
			}
			set
			{
				this.methodRowReader = value;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06006043 RID: 24643 RVA: 0x001CA45C File Offset: 0x001CA45C
		public uint Reserved1
		{
			get
			{
				return this.reserved1;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x001CA464 File Offset: 0x001CA464
		public ushort Version
		{
			get
			{
				return (ushort)((int)this.majorVersion << 8 | (int)this.minorVersion);
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06006045 RID: 24645 RVA: 0x001CA478 File Offset: 0x001CA478
		public MDStreamFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06006046 RID: 24646 RVA: 0x001CA480 File Offset: 0x001CA480
		public byte Log2Rid
		{
			get
			{
				return this.log2Rid;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06006047 RID: 24647 RVA: 0x001CA488 File Offset: 0x001CA488
		public ulong ValidMask
		{
			get
			{
				return this.validMask;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x001CA490 File Offset: 0x001CA490
		public ulong SortedMask
		{
			get
			{
				return this.sortedMask;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06006049 RID: 24649 RVA: 0x001CA498 File Offset: 0x001CA498
		public uint ExtraData
		{
			get
			{
				return this.extraData;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x001CA4A0 File Offset: 0x001CA4A0
		public MDTable[] MDTables
		{
			get
			{
				return this.mdTables;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x001CA4A8 File Offset: 0x001CA4A8
		public bool HasBigStrings
		{
			get
			{
				return (this.flags & MDStreamFlags.BigStrings) > (MDStreamFlags)0;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x001CA4B8 File Offset: 0x001CA4B8
		public bool HasBigGUID
		{
			get
			{
				return (this.flags & MDStreamFlags.BigGUID) > (MDStreamFlags)0;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x0600604D RID: 24653 RVA: 0x001CA4C8 File Offset: 0x001CA4C8
		public bool HasBigBlob
		{
			get
			{
				return (this.flags & MDStreamFlags.BigBlob) > (MDStreamFlags)0;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x001CA4D8 File Offset: 0x001CA4D8
		public bool HasPadding
		{
			get
			{
				return this.runtime == CLRRuntimeReaderKind.CLR && (this.flags & MDStreamFlags.Padding) > (MDStreamFlags)0;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x001CA4F4 File Offset: 0x001CA4F4
		public bool HasDeltaOnly
		{
			get
			{
				return this.runtime == CLRRuntimeReaderKind.CLR && (this.flags & MDStreamFlags.DeltaOnly) > (MDStreamFlags)0;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x001CA510 File Offset: 0x001CA510
		public bool HasExtraData
		{
			get
			{
				return this.runtime == CLRRuntimeReaderKind.CLR && (this.flags & MDStreamFlags.ExtraData) > (MDStreamFlags)0;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x001CA52C File Offset: 0x001CA52C
		public bool HasDelete
		{
			get
			{
				return this.runtime == CLRRuntimeReaderKind.CLR && (this.flags & MDStreamFlags.HasDelete) > (MDStreamFlags)0;
			}
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001CA54C File Offset: 0x001CA54C
		public TablesStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : this(mdReaderFactory, metadataBaseOffset, streamHeader, CLRRuntimeReaderKind.CLR)
		{
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001CA558 File Offset: 0x001CA558
		public TablesStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader, CLRRuntimeReaderKind runtime) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
			this.runtime = runtime;
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001CA56C File Offset: 0x001CA56C
		public void Initialize(uint[] typeSystemTableRows)
		{
			if (this.initialized)
			{
				throw new Exception("Initialize() has already been called");
			}
			this.initialized = true;
			DataReader dataReader = this.dataReader;
			this.reserved1 = dataReader.ReadUInt32();
			this.majorVersion = dataReader.ReadByte();
			this.minorVersion = dataReader.ReadByte();
			this.flags = (MDStreamFlags)dataReader.ReadByte();
			this.log2Rid = dataReader.ReadByte();
			this.validMask = dataReader.ReadUInt64();
			this.sortedMask = dataReader.ReadUInt64();
			DotNetTableSizes dotNetTableSizes = new DotNetTableSizes();
			int num;
			TableInfo[] array = dotNetTableSizes.CreateTables(this.majorVersion, this.minorVersion, out num);
			if (typeSystemTableRows != null)
			{
				num = 56;
			}
			this.mdTables = new MDTable[array.Length];
			ulong num2 = this.validMask;
			uint[] array2 = new uint[64];
			for (int i = 0; i < 64; i++)
			{
				uint num3 = ((num2 & 1UL) == 0UL) ? 0U : dataReader.ReadUInt32();
				num3 &= 16777215U;
				if (i >= num)
				{
					num3 = 0U;
				}
				array2[i] = num3;
				if (i < this.mdTables.Length)
				{
					this.mdTables[i] = new MDTable((Table)i, num3, array[i]);
				}
				num2 >>= 1;
			}
			if (this.HasExtraData)
			{
				this.extraData = dataReader.ReadUInt32();
			}
			uint[] array3 = array2;
			if (typeSystemTableRows != null)
			{
				array3 = new uint[array2.Length];
				for (int j = 0; j < 64; j++)
				{
					if (DotNetTableSizes.IsSystemTable((Table)j))
					{
						array3[j] = typeSystemTableRows[j];
					}
					else
					{
						array3[j] = array2[j];
					}
				}
			}
			dotNetTableSizes.InitializeSizes(this.HasBigStrings, this.HasBigGUID, this.HasBigBlob, array2, array3);
			this.mdTablesPos = dataReader.Position;
			this.InitializeMdTableReaders();
			this.InitializeTables();
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x001CA758 File Offset: 0x001CA758
		protected override void OnReaderRecreated()
		{
			this.InitializeMdTableReaders();
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x001CA760 File Offset: 0x001CA760
		private void InitializeMdTableReaders()
		{
			DataReader dataReader = this.dataReader;
			dataReader.Position = this.mdTablesPos;
			uint num = dataReader.Position;
			foreach (MDTable mdtable in this.mdTables)
			{
				uint num2 = (uint)(mdtable.TableInfo.RowSize * (int)mdtable.Rows);
				if (num > dataReader.Length)
				{
					num = dataReader.Length;
				}
				if ((ulong)num + (ulong)num2 > (ulong)dataReader.Length)
				{
					num2 = dataReader.Length - num;
				}
				mdtable.DataReader = dataReader.Slice(num, num2);
				uint num3 = num + num2;
				if (num3 < num)
				{
					throw new BadImageFormatException("Too big MD table");
				}
				num = num3;
			}
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001CA824 File Offset: 0x001CA824
		private void InitializeTables()
		{
			this.ModuleTable = this.mdTables[0];
			this.TypeRefTable = this.mdTables[1];
			this.TypeDefTable = this.mdTables[2];
			this.FieldPtrTable = this.mdTables[3];
			this.FieldTable = this.mdTables[4];
			this.MethodPtrTable = this.mdTables[5];
			this.MethodTable = this.mdTables[6];
			this.ParamPtrTable = this.mdTables[7];
			this.ParamTable = this.mdTables[8];
			this.InterfaceImplTable = this.mdTables[9];
			this.MemberRefTable = this.mdTables[10];
			this.ConstantTable = this.mdTables[11];
			this.CustomAttributeTable = this.mdTables[12];
			this.FieldMarshalTable = this.mdTables[13];
			this.DeclSecurityTable = this.mdTables[14];
			this.ClassLayoutTable = this.mdTables[15];
			this.FieldLayoutTable = this.mdTables[16];
			this.StandAloneSigTable = this.mdTables[17];
			this.EventMapTable = this.mdTables[18];
			this.EventPtrTable = this.mdTables[19];
			this.EventTable = this.mdTables[20];
			this.PropertyMapTable = this.mdTables[21];
			this.PropertyPtrTable = this.mdTables[22];
			this.PropertyTable = this.mdTables[23];
			this.MethodSemanticsTable = this.mdTables[24];
			this.MethodImplTable = this.mdTables[25];
			this.ModuleRefTable = this.mdTables[26];
			this.TypeSpecTable = this.mdTables[27];
			this.ImplMapTable = this.mdTables[28];
			this.FieldRVATable = this.mdTables[29];
			this.ENCLogTable = this.mdTables[30];
			this.ENCMapTable = this.mdTables[31];
			this.AssemblyTable = this.mdTables[32];
			this.AssemblyProcessorTable = this.mdTables[33];
			this.AssemblyOSTable = this.mdTables[34];
			this.AssemblyRefTable = this.mdTables[35];
			this.AssemblyRefProcessorTable = this.mdTables[36];
			this.AssemblyRefOSTable = this.mdTables[37];
			this.FileTable = this.mdTables[38];
			this.ExportedTypeTable = this.mdTables[39];
			this.ManifestResourceTable = this.mdTables[40];
			this.NestedClassTable = this.mdTables[41];
			this.GenericParamTable = this.mdTables[42];
			this.MethodSpecTable = this.mdTables[43];
			this.GenericParamConstraintTable = this.mdTables[44];
			this.DocumentTable = this.mdTables[48];
			this.MethodDebugInformationTable = this.mdTables[49];
			this.LocalScopeTable = this.mdTables[50];
			this.LocalVariableTable = this.mdTables[51];
			this.LocalConstantTable = this.mdTables[52];
			this.ImportScopeTable = this.mdTables[53];
			this.StateMachineMethodTable = this.mdTables[54];
			this.CustomDebugInformationTable = this.mdTables[55];
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001CAC1C File Offset: 0x001CAC1C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				MDTable[] array = this.mdTables;
				if (array != null)
				{
					foreach (MDTable mdtable in array)
					{
						if (mdtable != null)
						{
							mdtable.Dispose();
						}
					}
					this.mdTables = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x001CAC78 File Offset: 0x001CAC78
		public MDTable Get(Table table)
		{
			if ((int)table >= this.mdTables.Length)
			{
				return null;
			}
			return this.mdTables[(int)table];
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x001CACA8 File Offset: 0x001CACA8
		public bool HasTable(Table table)
		{
			return (int)table < this.mdTables.Length;
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x001CACB8 File Offset: 0x001CACB8
		public bool IsSorted(MDTable table)
		{
			int table2 = (int)table.Table;
			return table2 < 64 && (this.sortedMask & 1UL << table2) > 0UL;
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x001CACEC File Offset: 0x001CACEC
		public bool TryReadModuleRow(uint rid, out RawModuleRow row)
		{
			MDTable moduleTable = this.ModuleTable;
			if (moduleTable.IsInvalidRID(rid))
			{
				row = default(RawModuleRow);
				return false;
			}
			DataReader dataReader = moduleTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)moduleTable.TableInfo.RowSize;
			row = new RawModuleRow(dataReader.Unsafe_ReadUInt16(), moduleTable.Column1.Unsafe_Read24(ref dataReader), moduleTable.Column2.Unsafe_Read24(ref dataReader), moduleTable.Column3.Unsafe_Read24(ref dataReader), moduleTable.Column4.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x001CAD80 File Offset: 0x001CAD80
		public bool TryReadTypeRefRow(uint rid, out RawTypeRefRow row)
		{
			MDTable typeRefTable = this.TypeRefTable;
			if (typeRefTable.IsInvalidRID(rid))
			{
				row = default(RawTypeRefRow);
				return false;
			}
			DataReader dataReader = typeRefTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)typeRefTable.TableInfo.RowSize;
			row = new RawTypeRefRow(typeRefTable.Column0.Unsafe_Read24(ref dataReader), typeRefTable.Column1.Unsafe_Read24(ref dataReader), typeRefTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x001CAE00 File Offset: 0x001CAE00
		public bool TryReadTypeDefRow(uint rid, out RawTypeDefRow row)
		{
			MDTable typeDefTable = this.TypeDefTable;
			if (typeDefTable.IsInvalidRID(rid))
			{
				row = default(RawTypeDefRow);
				return false;
			}
			DataReader dataReader = typeDefTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)typeDefTable.TableInfo.RowSize;
			row = new RawTypeDefRow(dataReader.Unsafe_ReadUInt32(), typeDefTable.Column1.Unsafe_Read24(ref dataReader), typeDefTable.Column2.Unsafe_Read24(ref dataReader), typeDefTable.Column3.Unsafe_Read24(ref dataReader), typeDefTable.Column4.Unsafe_Read24(ref dataReader), typeDefTable.Column5.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x001CAEA0 File Offset: 0x001CAEA0
		public bool TryReadFieldPtrRow(uint rid, out RawFieldPtrRow row)
		{
			MDTable fieldPtrTable = this.FieldPtrTable;
			if (fieldPtrTable.IsInvalidRID(rid))
			{
				row = default(RawFieldPtrRow);
				return false;
			}
			DataReader dataReader = fieldPtrTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fieldPtrTable.TableInfo.RowSize;
			row = new RawFieldPtrRow(fieldPtrTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x001CAF04 File Offset: 0x001CAF04
		public bool TryReadFieldRow(uint rid, out RawFieldRow row)
		{
			MDTable fieldTable = this.FieldTable;
			if (fieldTable.IsInvalidRID(rid))
			{
				row = default(RawFieldRow);
				return false;
			}
			DataReader dataReader = fieldTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fieldTable.TableInfo.RowSize;
			row = new RawFieldRow(dataReader.Unsafe_ReadUInt16(), fieldTable.Column1.Unsafe_Read24(ref dataReader), fieldTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x001CAF7C File Offset: 0x001CAF7C
		public bool TryReadMethodPtrRow(uint rid, out RawMethodPtrRow row)
		{
			MDTable methodPtrTable = this.MethodPtrTable;
			if (methodPtrTable.IsInvalidRID(rid))
			{
				row = default(RawMethodPtrRow);
				return false;
			}
			DataReader dataReader = methodPtrTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodPtrTable.TableInfo.RowSize;
			row = new RawMethodPtrRow(methodPtrTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x001CAFE0 File Offset: 0x001CAFE0
		public bool TryReadMethodRow(uint rid, out RawMethodRow row)
		{
			MDTable methodTable = this.MethodTable;
			if (methodTable.IsInvalidRID(rid))
			{
				row = default(RawMethodRow);
				return false;
			}
			IRowReader<RawMethodRow> rowReader = this.methodRowReader;
			if (rowReader != null && rowReader.TryReadRow(rid, out row))
			{
				return true;
			}
			DataReader dataReader = methodTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodTable.TableInfo.RowSize;
			row = new RawMethodRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), methodTable.Column3.Unsafe_Read24(ref dataReader), methodTable.Column4.Unsafe_Read24(ref dataReader), methodTable.Column5.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x001CB090 File Offset: 0x001CB090
		public bool TryReadParamPtrRow(uint rid, out RawParamPtrRow row)
		{
			MDTable paramPtrTable = this.ParamPtrTable;
			if (paramPtrTable.IsInvalidRID(rid))
			{
				row = default(RawParamPtrRow);
				return false;
			}
			DataReader dataReader = paramPtrTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)paramPtrTable.TableInfo.RowSize;
			row = new RawParamPtrRow(paramPtrTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x001CB0F4 File Offset: 0x001CB0F4
		public bool TryReadParamRow(uint rid, out RawParamRow row)
		{
			MDTable paramTable = this.ParamTable;
			if (paramTable.IsInvalidRID(rid))
			{
				row = default(RawParamRow);
				return false;
			}
			DataReader dataReader = paramTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)paramTable.TableInfo.RowSize;
			row = new RawParamRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), paramTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x001CB168 File Offset: 0x001CB168
		public bool TryReadInterfaceImplRow(uint rid, out RawInterfaceImplRow row)
		{
			MDTable interfaceImplTable = this.InterfaceImplTable;
			if (interfaceImplTable.IsInvalidRID(rid))
			{
				row = default(RawInterfaceImplRow);
				return false;
			}
			DataReader dataReader = interfaceImplTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)interfaceImplTable.TableInfo.RowSize;
			row = new RawInterfaceImplRow(interfaceImplTable.Column0.Unsafe_Read24(ref dataReader), interfaceImplTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001CB1D8 File Offset: 0x001CB1D8
		public bool TryReadMemberRefRow(uint rid, out RawMemberRefRow row)
		{
			MDTable memberRefTable = this.MemberRefTable;
			if (memberRefTable.IsInvalidRID(rid))
			{
				row = default(RawMemberRefRow);
				return false;
			}
			DataReader dataReader = memberRefTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)memberRefTable.TableInfo.RowSize;
			row = new RawMemberRefRow(memberRefTable.Column0.Unsafe_Read24(ref dataReader), memberRefTable.Column1.Unsafe_Read24(ref dataReader), memberRefTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x001CB258 File Offset: 0x001CB258
		public bool TryReadConstantRow(uint rid, out RawConstantRow row)
		{
			MDTable constantTable = this.ConstantTable;
			if (constantTable.IsInvalidRID(rid))
			{
				row = default(RawConstantRow);
				return false;
			}
			DataReader dataReader = constantTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)constantTable.TableInfo.RowSize;
			row = new RawConstantRow(dataReader.Unsafe_ReadByte(), dataReader.Unsafe_ReadByte(), constantTable.Column2.Unsafe_Read24(ref dataReader), constantTable.Column3.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x001CB2D8 File Offset: 0x001CB2D8
		public bool TryReadCustomAttributeRow(uint rid, out RawCustomAttributeRow row)
		{
			MDTable customAttributeTable = this.CustomAttributeTable;
			if (customAttributeTable.IsInvalidRID(rid))
			{
				row = default(RawCustomAttributeRow);
				return false;
			}
			DataReader dataReader = customAttributeTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)customAttributeTable.TableInfo.RowSize;
			row = new RawCustomAttributeRow(customAttributeTable.Column0.Unsafe_Read24(ref dataReader), customAttributeTable.Column1.Unsafe_Read24(ref dataReader), customAttributeTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x001CB358 File Offset: 0x001CB358
		public bool TryReadFieldMarshalRow(uint rid, out RawFieldMarshalRow row)
		{
			MDTable fieldMarshalTable = this.FieldMarshalTable;
			if (fieldMarshalTable.IsInvalidRID(rid))
			{
				row = default(RawFieldMarshalRow);
				return false;
			}
			DataReader dataReader = fieldMarshalTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fieldMarshalTable.TableInfo.RowSize;
			row = new RawFieldMarshalRow(fieldMarshalTable.Column0.Unsafe_Read24(ref dataReader), fieldMarshalTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x001CB3C8 File Offset: 0x001CB3C8
		public bool TryReadDeclSecurityRow(uint rid, out RawDeclSecurityRow row)
		{
			MDTable declSecurityTable = this.DeclSecurityTable;
			if (declSecurityTable.IsInvalidRID(rid))
			{
				row = default(RawDeclSecurityRow);
				return false;
			}
			DataReader dataReader = declSecurityTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)declSecurityTable.TableInfo.RowSize;
			row = new RawDeclSecurityRow((short)dataReader.Unsafe_ReadUInt16(), declSecurityTable.Column1.Unsafe_Read24(ref dataReader), declSecurityTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x001CB440 File Offset: 0x001CB440
		public bool TryReadClassLayoutRow(uint rid, out RawClassLayoutRow row)
		{
			MDTable classLayoutTable = this.ClassLayoutTable;
			if (classLayoutTable.IsInvalidRID(rid))
			{
				row = default(RawClassLayoutRow);
				return false;
			}
			DataReader dataReader = classLayoutTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)classLayoutTable.TableInfo.RowSize;
			row = new RawClassLayoutRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt32(), classLayoutTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x001CB4B4 File Offset: 0x001CB4B4
		public bool TryReadFieldLayoutRow(uint rid, out RawFieldLayoutRow row)
		{
			MDTable fieldLayoutTable = this.FieldLayoutTable;
			if (fieldLayoutTable.IsInvalidRID(rid))
			{
				row = default(RawFieldLayoutRow);
				return false;
			}
			DataReader dataReader = fieldLayoutTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fieldLayoutTable.TableInfo.RowSize;
			row = new RawFieldLayoutRow(dataReader.Unsafe_ReadUInt32(), fieldLayoutTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x001CB520 File Offset: 0x001CB520
		public bool TryReadStandAloneSigRow(uint rid, out RawStandAloneSigRow row)
		{
			MDTable standAloneSigTable = this.StandAloneSigTable;
			if (standAloneSigTable.IsInvalidRID(rid))
			{
				row = default(RawStandAloneSigRow);
				return false;
			}
			DataReader dataReader = standAloneSigTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)standAloneSigTable.TableInfo.RowSize;
			row = new RawStandAloneSigRow(standAloneSigTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x001CB584 File Offset: 0x001CB584
		public bool TryReadEventMapRow(uint rid, out RawEventMapRow row)
		{
			MDTable eventMapTable = this.EventMapTable;
			if (eventMapTable.IsInvalidRID(rid))
			{
				row = default(RawEventMapRow);
				return false;
			}
			DataReader dataReader = eventMapTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)eventMapTable.TableInfo.RowSize;
			row = new RawEventMapRow(eventMapTable.Column0.Unsafe_Read24(ref dataReader), eventMapTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x001CB5F4 File Offset: 0x001CB5F4
		public bool TryReadEventPtrRow(uint rid, out RawEventPtrRow row)
		{
			MDTable eventPtrTable = this.EventPtrTable;
			if (eventPtrTable.IsInvalidRID(rid))
			{
				row = default(RawEventPtrRow);
				return false;
			}
			DataReader dataReader = eventPtrTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)eventPtrTable.TableInfo.RowSize;
			row = new RawEventPtrRow(eventPtrTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x001CB658 File Offset: 0x001CB658
		public bool TryReadEventRow(uint rid, out RawEventRow row)
		{
			MDTable eventTable = this.EventTable;
			if (eventTable.IsInvalidRID(rid))
			{
				row = default(RawEventRow);
				return false;
			}
			DataReader dataReader = eventTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)eventTable.TableInfo.RowSize;
			row = new RawEventRow(dataReader.Unsafe_ReadUInt16(), eventTable.Column1.Unsafe_Read24(ref dataReader), eventTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x001CB6D0 File Offset: 0x001CB6D0
		public bool TryReadPropertyMapRow(uint rid, out RawPropertyMapRow row)
		{
			MDTable propertyMapTable = this.PropertyMapTable;
			if (propertyMapTable.IsInvalidRID(rid))
			{
				row = default(RawPropertyMapRow);
				return false;
			}
			DataReader dataReader = propertyMapTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)propertyMapTable.TableInfo.RowSize;
			row = new RawPropertyMapRow(propertyMapTable.Column0.Unsafe_Read24(ref dataReader), propertyMapTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x001CB740 File Offset: 0x001CB740
		public bool TryReadPropertyPtrRow(uint rid, out RawPropertyPtrRow row)
		{
			MDTable propertyPtrTable = this.PropertyPtrTable;
			if (propertyPtrTable.IsInvalidRID(rid))
			{
				row = default(RawPropertyPtrRow);
				return false;
			}
			DataReader dataReader = propertyPtrTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)propertyPtrTable.TableInfo.RowSize;
			row = new RawPropertyPtrRow(propertyPtrTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x001CB7A4 File Offset: 0x001CB7A4
		public bool TryReadPropertyRow(uint rid, out RawPropertyRow row)
		{
			MDTable propertyTable = this.PropertyTable;
			if (propertyTable.IsInvalidRID(rid))
			{
				row = default(RawPropertyRow);
				return false;
			}
			DataReader dataReader = propertyTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)propertyTable.TableInfo.RowSize;
			row = new RawPropertyRow(dataReader.Unsafe_ReadUInt16(), propertyTable.Column1.Unsafe_Read24(ref dataReader), propertyTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x001CB81C File Offset: 0x001CB81C
		public bool TryReadMethodSemanticsRow(uint rid, out RawMethodSemanticsRow row)
		{
			MDTable methodSemanticsTable = this.MethodSemanticsTable;
			if (methodSemanticsTable.IsInvalidRID(rid))
			{
				row = default(RawMethodSemanticsRow);
				return false;
			}
			DataReader dataReader = methodSemanticsTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodSemanticsTable.TableInfo.RowSize;
			row = new RawMethodSemanticsRow(dataReader.Unsafe_ReadUInt16(), methodSemanticsTable.Column1.Unsafe_Read24(ref dataReader), methodSemanticsTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x001CB894 File Offset: 0x001CB894
		public bool TryReadMethodImplRow(uint rid, out RawMethodImplRow row)
		{
			MDTable methodImplTable = this.MethodImplTable;
			if (methodImplTable.IsInvalidRID(rid))
			{
				row = default(RawMethodImplRow);
				return false;
			}
			DataReader dataReader = methodImplTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodImplTable.TableInfo.RowSize;
			row = new RawMethodImplRow(methodImplTable.Column0.Unsafe_Read24(ref dataReader), methodImplTable.Column1.Unsafe_Read24(ref dataReader), methodImplTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x001CB914 File Offset: 0x001CB914
		public bool TryReadModuleRefRow(uint rid, out RawModuleRefRow row)
		{
			MDTable moduleRefTable = this.ModuleRefTable;
			if (moduleRefTable.IsInvalidRID(rid))
			{
				row = default(RawModuleRefRow);
				return false;
			}
			DataReader dataReader = moduleRefTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)moduleRefTable.TableInfo.RowSize;
			row = new RawModuleRefRow(moduleRefTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x001CB978 File Offset: 0x001CB978
		public bool TryReadTypeSpecRow(uint rid, out RawTypeSpecRow row)
		{
			MDTable typeSpecTable = this.TypeSpecTable;
			if (typeSpecTable.IsInvalidRID(rid))
			{
				row = default(RawTypeSpecRow);
				return false;
			}
			DataReader dataReader = typeSpecTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)typeSpecTable.TableInfo.RowSize;
			row = new RawTypeSpecRow(typeSpecTable.Column0.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x001CB9DC File Offset: 0x001CB9DC
		public bool TryReadImplMapRow(uint rid, out RawImplMapRow row)
		{
			MDTable implMapTable = this.ImplMapTable;
			if (implMapTable.IsInvalidRID(rid))
			{
				row = default(RawImplMapRow);
				return false;
			}
			DataReader dataReader = implMapTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)implMapTable.TableInfo.RowSize;
			row = new RawImplMapRow(dataReader.Unsafe_ReadUInt16(), implMapTable.Column1.Unsafe_Read24(ref dataReader), implMapTable.Column2.Unsafe_Read24(ref dataReader), implMapTable.Column3.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x001CBA60 File Offset: 0x001CBA60
		public bool TryReadFieldRVARow(uint rid, out RawFieldRVARow row)
		{
			MDTable fieldRVATable = this.FieldRVATable;
			if (fieldRVATable.IsInvalidRID(rid))
			{
				row = default(RawFieldRVARow);
				return false;
			}
			DataReader dataReader = fieldRVATable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fieldRVATable.TableInfo.RowSize;
			row = new RawFieldRVARow(dataReader.Unsafe_ReadUInt32(), fieldRVATable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x001CBACC File Offset: 0x001CBACC
		public bool TryReadENCLogRow(uint rid, out RawENCLogRow row)
		{
			MDTable enclogTable = this.ENCLogTable;
			if (enclogTable.IsInvalidRID(rid))
			{
				row = default(RawENCLogRow);
				return false;
			}
			DataReader dataReader = enclogTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)enclogTable.TableInfo.RowSize;
			row = new RawENCLogRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x001CBB30 File Offset: 0x001CBB30
		public bool TryReadENCMapRow(uint rid, out RawENCMapRow row)
		{
			MDTable encmapTable = this.ENCMapTable;
			if (encmapTable.IsInvalidRID(rid))
			{
				row = default(RawENCMapRow);
				return false;
			}
			DataReader dataReader = encmapTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)encmapTable.TableInfo.RowSize;
			row = new RawENCMapRow(dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x001CBB90 File Offset: 0x001CBB90
		public bool TryReadAssemblyRow(uint rid, out RawAssemblyRow row)
		{
			MDTable assemblyTable = this.AssemblyTable;
			if (assemblyTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyRow);
				return false;
			}
			DataReader dataReader = assemblyTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyTable.TableInfo.RowSize;
			row = new RawAssemblyRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt32(), assemblyTable.Column6.Unsafe_Read24(ref dataReader), assemblyTable.Column7.Unsafe_Read24(ref dataReader), assemblyTable.Column8.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x001CBC38 File Offset: 0x001CBC38
		public bool TryReadAssemblyProcessorRow(uint rid, out RawAssemblyProcessorRow row)
		{
			MDTable assemblyProcessorTable = this.AssemblyProcessorTable;
			if (assemblyProcessorTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyProcessorRow);
				return false;
			}
			DataReader dataReader = assemblyProcessorTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyProcessorTable.TableInfo.RowSize;
			row = new RawAssemblyProcessorRow(dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001CBC98 File Offset: 0x001CBC98
		public bool TryReadAssemblyOSRow(uint rid, out RawAssemblyOSRow row)
		{
			MDTable assemblyOSTable = this.AssemblyOSTable;
			if (assemblyOSTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyOSRow);
				return false;
			}
			DataReader dataReader = assemblyOSTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyOSTable.TableInfo.RowSize;
			row = new RawAssemblyOSRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001CBD04 File Offset: 0x001CBD04
		public bool TryReadAssemblyRefRow(uint rid, out RawAssemblyRefRow row)
		{
			MDTable assemblyRefTable = this.AssemblyRefTable;
			if (assemblyRefTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyRefRow);
				return false;
			}
			DataReader dataReader = assemblyRefTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyRefTable.TableInfo.RowSize;
			row = new RawAssemblyRefRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt32(), assemblyRefTable.Column5.Unsafe_Read24(ref dataReader), assemblyRefTable.Column6.Unsafe_Read24(ref dataReader), assemblyRefTable.Column7.Unsafe_Read24(ref dataReader), assemblyRefTable.Column8.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x001CBDB4 File Offset: 0x001CBDB4
		public bool TryReadAssemblyRefProcessorRow(uint rid, out RawAssemblyRefProcessorRow row)
		{
			MDTable assemblyRefProcessorTable = this.AssemblyRefProcessorTable;
			if (assemblyRefProcessorTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyRefProcessorRow);
				return false;
			}
			DataReader dataReader = assemblyRefProcessorTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyRefProcessorTable.TableInfo.RowSize;
			row = new RawAssemblyRefProcessorRow(dataReader.Unsafe_ReadUInt32(), assemblyRefProcessorTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x001CBE20 File Offset: 0x001CBE20
		public bool TryReadAssemblyRefOSRow(uint rid, out RawAssemblyRefOSRow row)
		{
			MDTable assemblyRefOSTable = this.AssemblyRefOSTable;
			if (assemblyRefOSTable.IsInvalidRID(rid))
			{
				row = default(RawAssemblyRefOSRow);
				return false;
			}
			DataReader dataReader = assemblyRefOSTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)assemblyRefOSTable.TableInfo.RowSize;
			row = new RawAssemblyRefOSRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32(), assemblyRefOSTable.Column3.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x001CBE98 File Offset: 0x001CBE98
		public bool TryReadFileRow(uint rid, out RawFileRow row)
		{
			MDTable fileTable = this.FileTable;
			if (fileTable.IsInvalidRID(rid))
			{
				row = default(RawFileRow);
				return false;
			}
			DataReader dataReader = fileTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)fileTable.TableInfo.RowSize;
			row = new RawFileRow(dataReader.Unsafe_ReadUInt32(), fileTable.Column1.Unsafe_Read24(ref dataReader), fileTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x001CBF10 File Offset: 0x001CBF10
		public bool TryReadExportedTypeRow(uint rid, out RawExportedTypeRow row)
		{
			MDTable exportedTypeTable = this.ExportedTypeTable;
			if (exportedTypeTable.IsInvalidRID(rid))
			{
				row = default(RawExportedTypeRow);
				return false;
			}
			DataReader dataReader = exportedTypeTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)exportedTypeTable.TableInfo.RowSize;
			row = new RawExportedTypeRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32(), exportedTypeTable.Column2.Unsafe_Read24(ref dataReader), exportedTypeTable.Column3.Unsafe_Read24(ref dataReader), exportedTypeTable.Column4.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x001CBF9C File Offset: 0x001CBF9C
		public bool TryReadManifestResourceRow(uint rid, out RawManifestResourceRow row)
		{
			MDTable manifestResourceTable = this.ManifestResourceTable;
			if (manifestResourceTable.IsInvalidRID(rid))
			{
				row = default(RawManifestResourceRow);
				return false;
			}
			DataReader dataReader = manifestResourceTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)manifestResourceTable.TableInfo.RowSize;
			row = new RawManifestResourceRow(dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32(), manifestResourceTable.Column2.Unsafe_Read24(ref dataReader), manifestResourceTable.Column3.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x001CC01C File Offset: 0x001CC01C
		public bool TryReadNestedClassRow(uint rid, out RawNestedClassRow row)
		{
			MDTable nestedClassTable = this.NestedClassTable;
			if (nestedClassTable.IsInvalidRID(rid))
			{
				row = default(RawNestedClassRow);
				return false;
			}
			DataReader dataReader = nestedClassTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)nestedClassTable.TableInfo.RowSize;
			row = new RawNestedClassRow(nestedClassTable.Column0.Unsafe_Read24(ref dataReader), nestedClassTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x001CC08C File Offset: 0x001CC08C
		public bool TryReadGenericParamRow(uint rid, out RawGenericParamRow row)
		{
			MDTable genericParamTable = this.GenericParamTable;
			if (genericParamTable.IsInvalidRID(rid))
			{
				row = default(RawGenericParamRow);
				return false;
			}
			DataReader dataReader = genericParamTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)genericParamTable.TableInfo.RowSize;
			if (genericParamTable.Column4 == null)
			{
				row = new RawGenericParamRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), genericParamTable.Column2.Unsafe_Read24(ref dataReader), genericParamTable.Column3.Unsafe_Read24(ref dataReader));
				return true;
			}
			row = new RawGenericParamRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), genericParamTable.Column2.Unsafe_Read24(ref dataReader), genericParamTable.Column3.Unsafe_Read24(ref dataReader), genericParamTable.Column4.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x001CC158 File Offset: 0x001CC158
		public bool TryReadMethodSpecRow(uint rid, out RawMethodSpecRow row)
		{
			MDTable methodSpecTable = this.MethodSpecTable;
			if (methodSpecTable.IsInvalidRID(rid))
			{
				row = default(RawMethodSpecRow);
				return false;
			}
			DataReader dataReader = methodSpecTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodSpecTable.TableInfo.RowSize;
			row = new RawMethodSpecRow(methodSpecTable.Column0.Unsafe_Read24(ref dataReader), methodSpecTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001CC1C8 File Offset: 0x001CC1C8
		public bool TryReadGenericParamConstraintRow(uint rid, out RawGenericParamConstraintRow row)
		{
			MDTable genericParamConstraintTable = this.GenericParamConstraintTable;
			if (genericParamConstraintTable.IsInvalidRID(rid))
			{
				row = default(RawGenericParamConstraintRow);
				return false;
			}
			DataReader dataReader = genericParamConstraintTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)genericParamConstraintTable.TableInfo.RowSize;
			row = new RawGenericParamConstraintRow(genericParamConstraintTable.Column0.Unsafe_Read24(ref dataReader), genericParamConstraintTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x001CC238 File Offset: 0x001CC238
		public bool TryReadDocumentRow(uint rid, out RawDocumentRow row)
		{
			MDTable documentTable = this.DocumentTable;
			if (documentTable.IsInvalidRID(rid))
			{
				row = default(RawDocumentRow);
				return false;
			}
			DataReader dataReader = documentTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)documentTable.TableInfo.RowSize;
			row = new RawDocumentRow(documentTable.Column0.Unsafe_Read24(ref dataReader), documentTable.Column1.Unsafe_Read24(ref dataReader), documentTable.Column2.Unsafe_Read24(ref dataReader), documentTable.Column3.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x001CC2C4 File Offset: 0x001CC2C4
		public bool TryReadMethodDebugInformationRow(uint rid, out RawMethodDebugInformationRow row)
		{
			MDTable methodDebugInformationTable = this.MethodDebugInformationTable;
			if (methodDebugInformationTable.IsInvalidRID(rid))
			{
				row = default(RawMethodDebugInformationRow);
				return false;
			}
			DataReader dataReader = methodDebugInformationTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)methodDebugInformationTable.TableInfo.RowSize;
			row = new RawMethodDebugInformationRow(methodDebugInformationTable.Column0.Unsafe_Read24(ref dataReader), methodDebugInformationTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x001CC334 File Offset: 0x001CC334
		public bool TryReadLocalScopeRow(uint rid, out RawLocalScopeRow row)
		{
			MDTable localScopeTable = this.LocalScopeTable;
			if (localScopeTable.IsInvalidRID(rid))
			{
				row = default(RawLocalScopeRow);
				return false;
			}
			DataReader dataReader = localScopeTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)localScopeTable.TableInfo.RowSize;
			row = new RawLocalScopeRow(localScopeTable.Column0.Unsafe_Read24(ref dataReader), localScopeTable.Column1.Unsafe_Read24(ref dataReader), localScopeTable.Column2.Unsafe_Read24(ref dataReader), localScopeTable.Column3.Unsafe_Read24(ref dataReader), dataReader.Unsafe_ReadUInt32(), dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x001CC3CC File Offset: 0x001CC3CC
		public bool TryReadLocalVariableRow(uint rid, out RawLocalVariableRow row)
		{
			MDTable localVariableTable = this.LocalVariableTable;
			if (localVariableTable.IsInvalidRID(rid))
			{
				row = default(RawLocalVariableRow);
				return false;
			}
			DataReader dataReader = localVariableTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)localVariableTable.TableInfo.RowSize;
			row = new RawLocalVariableRow(dataReader.Unsafe_ReadUInt16(), dataReader.Unsafe_ReadUInt16(), localVariableTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x001CC440 File Offset: 0x001CC440
		public bool TryReadLocalConstantRow(uint rid, out RawLocalConstantRow row)
		{
			MDTable localConstantTable = this.LocalConstantTable;
			if (localConstantTable.IsInvalidRID(rid))
			{
				row = default(RawLocalConstantRow);
				return false;
			}
			DataReader dataReader = localConstantTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)localConstantTable.TableInfo.RowSize;
			row = new RawLocalConstantRow(localConstantTable.Column0.Unsafe_Read24(ref dataReader), localConstantTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x001CC4B0 File Offset: 0x001CC4B0
		public bool TryReadImportScopeRow(uint rid, out RawImportScopeRow row)
		{
			MDTable importScopeTable = this.ImportScopeTable;
			if (importScopeTable.IsInvalidRID(rid))
			{
				row = default(RawImportScopeRow);
				return false;
			}
			DataReader dataReader = importScopeTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)importScopeTable.TableInfo.RowSize;
			row = new RawImportScopeRow(importScopeTable.Column0.Unsafe_Read24(ref dataReader), importScopeTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x001CC520 File Offset: 0x001CC520
		public bool TryReadStateMachineMethodRow(uint rid, out RawStateMachineMethodRow row)
		{
			MDTable stateMachineMethodTable = this.StateMachineMethodTable;
			if (stateMachineMethodTable.IsInvalidRID(rid))
			{
				row = default(RawStateMachineMethodRow);
				return false;
			}
			DataReader dataReader = stateMachineMethodTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)stateMachineMethodTable.TableInfo.RowSize;
			row = new RawStateMachineMethodRow(stateMachineMethodTable.Column0.Unsafe_Read24(ref dataReader), stateMachineMethodTable.Column1.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x001CC590 File Offset: 0x001CC590
		public bool TryReadCustomDebugInformationRow(uint rid, out RawCustomDebugInformationRow row)
		{
			MDTable customDebugInformationTable = this.CustomDebugInformationTable;
			if (customDebugInformationTable.IsInvalidRID(rid))
			{
				row = default(RawCustomDebugInformationRow);
				return false;
			}
			DataReader dataReader = customDebugInformationTable.DataReader;
			dataReader.Position = (rid - 1U) * (uint)customDebugInformationTable.TableInfo.RowSize;
			row = new RawCustomDebugInformationRow(customDebugInformationTable.Column0.Unsafe_Read24(ref dataReader), customDebugInformationTable.Column1.Unsafe_Read24(ref dataReader), customDebugInformationTable.Column2.Unsafe_Read24(ref dataReader));
			return true;
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001CC610 File Offset: 0x001CC610
		public bool TryReadColumn(MDTable table, uint rid, int colIndex, out uint value)
		{
			return this.TryReadColumn(table, rid, table.TableInfo.Columns[colIndex], out value);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x001CC63C File Offset: 0x001CC63C
		public bool TryReadColumn(MDTable table, uint rid, ColumnInfo column, out uint value)
		{
			if (table.IsInvalidRID(rid))
			{
				value = 0U;
				return false;
			}
			IColumnReader columnReader = this.columnReader;
			if (columnReader != null && columnReader.ReadColumn(table, rid, column, out value))
			{
				return true;
			}
			DataReader dataReader = table.DataReader;
			dataReader.Position = (rid - 1U) * (uint)table.TableInfo.RowSize + (uint)column.Offset;
			value = column.Read(ref dataReader);
			return true;
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x001CC6B0 File Offset: 0x001CC6B0
		internal bool TryReadColumn24(MDTable table, uint rid, int colIndex, out uint value)
		{
			return this.TryReadColumn24(table, rid, table.TableInfo.Columns[colIndex], out value);
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001CC6DC File Offset: 0x001CC6DC
		internal bool TryReadColumn24(MDTable table, uint rid, ColumnInfo column, out uint value)
		{
			if (table.IsInvalidRID(rid))
			{
				value = 0U;
				return false;
			}
			IColumnReader columnReader = this.columnReader;
			if (columnReader != null && columnReader.ReadColumn(table, rid, column, out value))
			{
				return true;
			}
			DataReader dataReader = table.DataReader;
			dataReader.Position = (rid - 1U) * (uint)table.TableInfo.RowSize + (uint)column.Offset;
			value = ((column.Size == 2) ? ((uint)dataReader.Unsafe_ReadUInt16()) : dataReader.Unsafe_ReadUInt32());
			return true;
		}

		// Token: 0x04002F37 RID: 12087
		private bool initialized;

		// Token: 0x04002F38 RID: 12088
		private uint reserved1;

		// Token: 0x04002F39 RID: 12089
		private byte majorVersion;

		// Token: 0x04002F3A RID: 12090
		private byte minorVersion;

		// Token: 0x04002F3B RID: 12091
		private MDStreamFlags flags;

		// Token: 0x04002F3C RID: 12092
		private byte log2Rid;

		// Token: 0x04002F3D RID: 12093
		private ulong validMask;

		// Token: 0x04002F3E RID: 12094
		private ulong sortedMask;

		// Token: 0x04002F3F RID: 12095
		private uint extraData;

		// Token: 0x04002F40 RID: 12096
		private MDTable[] mdTables;

		// Token: 0x04002F41 RID: 12097
		private uint mdTablesPos;

		// Token: 0x04002F42 RID: 12098
		private IColumnReader columnReader;

		// Token: 0x04002F43 RID: 12099
		private IRowReader<RawMethodRow> methodRowReader;

		// Token: 0x04002F44 RID: 12100
		private readonly CLRRuntimeReaderKind runtime;
	}
}
