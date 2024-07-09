using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009EE RID: 2542
	[ComVisible(true)]
	public static class OpCodes
	{
		// Token: 0x060061B3 RID: 25011 RVA: 0x001D174C File Offset: 0x001D174C
		static OpCodes()
		{
			for (int i = 0; i < OpCodes.OneByteOpCodes.Length; i++)
			{
				if (OpCodes.OneByteOpCodes[i] == null)
				{
					OpCodes.OneByteOpCodes[i] = OpCodes.UNKNOWN1;
				}
			}
			for (int j = 0; j < OpCodes.TwoByteOpCodes.Length; j++)
			{
				if (OpCodes.TwoByteOpCodes[j] == null)
				{
					OpCodes.TwoByteOpCodes[j] = OpCodes.UNKNOWN2;
				}
			}
		}

		// Token: 0x040030D6 RID: 12502
		public static readonly OpCode[] OneByteOpCodes = new OpCode[256];

		// Token: 0x040030D7 RID: 12503
		public static readonly OpCode[] TwoByteOpCodes = new OpCode[256];

		// Token: 0x040030D8 RID: 12504
		public static readonly OpCode UNKNOWN1 = new OpCode("UNKNOWN1", Code.UNKNOWN1, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040030D9 RID: 12505
		public static readonly OpCode UNKNOWN2 = new OpCode("UNKNOWN2", Code.UNKNOWN2, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040030DA RID: 12506
		public static readonly OpCode Nop = new OpCode("nop", Code.Nop, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040030DB RID: 12507
		public static readonly OpCode Break = new OpCode("break", Code.Break, OperandType.InlineNone, FlowControl.Break, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040030DC RID: 12508
		public static readonly OpCode Ldarg_0 = new OpCode("ldarg.0", Code.Ldarg_0, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030DD RID: 12509
		public static readonly OpCode Ldarg_1 = new OpCode("ldarg.1", Code.Ldarg_1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030DE RID: 12510
		public static readonly OpCode Ldarg_2 = new OpCode("ldarg.2", Code.Ldarg_2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030DF RID: 12511
		public static readonly OpCode Ldarg_3 = new OpCode("ldarg.3", Code.Ldarg_3, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E0 RID: 12512
		public static readonly OpCode Ldloc_0 = new OpCode("ldloc.0", Code.Ldloc_0, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E1 RID: 12513
		public static readonly OpCode Ldloc_1 = new OpCode("ldloc.1", Code.Ldloc_1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E2 RID: 12514
		public static readonly OpCode Ldloc_2 = new OpCode("ldloc.2", Code.Ldloc_2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E3 RID: 12515
		public static readonly OpCode Ldloc_3 = new OpCode("ldloc.3", Code.Ldloc_3, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E4 RID: 12516
		public static readonly OpCode Stloc_0 = new OpCode("stloc.0", Code.Stloc_0, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030E5 RID: 12517
		public static readonly OpCode Stloc_1 = new OpCode("stloc.1", Code.Stloc_1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030E6 RID: 12518
		public static readonly OpCode Stloc_2 = new OpCode("stloc.2", Code.Stloc_2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030E7 RID: 12519
		public static readonly OpCode Stloc_3 = new OpCode("stloc.3", Code.Stloc_3, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030E8 RID: 12520
		public static readonly OpCode Ldarg_S = new OpCode("ldarg.s", Code.Ldarg_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030E9 RID: 12521
		public static readonly OpCode Ldarga_S = new OpCode("ldarga.s", Code.Ldarga_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030EA RID: 12522
		public static readonly OpCode Starg_S = new OpCode("starg.s", Code.Starg_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030EB RID: 12523
		public static readonly OpCode Ldloc_S = new OpCode("ldloc.s", Code.Ldloc_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040030EC RID: 12524
		public static readonly OpCode Ldloca_S = new OpCode("ldloca.s", Code.Ldloca_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030ED RID: 12525
		public static readonly OpCode Stloc_S = new OpCode("stloc.s", Code.Stloc_S, OperandType.ShortInlineVar, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040030EE RID: 12526
		public static readonly OpCode Ldnull = new OpCode("ldnull", Code.Ldnull, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushref, StackBehaviour.Pop0);

		// Token: 0x040030EF RID: 12527
		public static readonly OpCode Ldc_I4_M1 = new OpCode("ldc.i4.m1", Code.Ldc_I4_M1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F0 RID: 12528
		public static readonly OpCode Ldc_I4_0 = new OpCode("ldc.i4.0", Code.Ldc_I4_0, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F1 RID: 12529
		public static readonly OpCode Ldc_I4_1 = new OpCode("ldc.i4.1", Code.Ldc_I4_1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F2 RID: 12530
		public static readonly OpCode Ldc_I4_2 = new OpCode("ldc.i4.2", Code.Ldc_I4_2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F3 RID: 12531
		public static readonly OpCode Ldc_I4_3 = new OpCode("ldc.i4.3", Code.Ldc_I4_3, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F4 RID: 12532
		public static readonly OpCode Ldc_I4_4 = new OpCode("ldc.i4.4", Code.Ldc_I4_4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F5 RID: 12533
		public static readonly OpCode Ldc_I4_5 = new OpCode("ldc.i4.5", Code.Ldc_I4_5, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F6 RID: 12534
		public static readonly OpCode Ldc_I4_6 = new OpCode("ldc.i4.6", Code.Ldc_I4_6, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F7 RID: 12535
		public static readonly OpCode Ldc_I4_7 = new OpCode("ldc.i4.7", Code.Ldc_I4_7, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F8 RID: 12536
		public static readonly OpCode Ldc_I4_8 = new OpCode("ldc.i4.8", Code.Ldc_I4_8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030F9 RID: 12537
		public static readonly OpCode Ldc_I4_S = new OpCode("ldc.i4.s", Code.Ldc_I4_S, OperandType.ShortInlineI, FlowControl.Next, OpCodeType.Macro, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030FA RID: 12538
		public static readonly OpCode Ldc_I4 = new OpCode("ldc.i4", Code.Ldc_I4, OperandType.InlineI, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040030FB RID: 12539
		public static readonly OpCode Ldc_I8 = new OpCode("ldc.i8", Code.Ldc_I8, OperandType.InlineI8, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop0);

		// Token: 0x040030FC RID: 12540
		public static readonly OpCode Ldc_R4 = new OpCode("ldc.r4", Code.Ldc_R4, OperandType.ShortInlineR, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr4, StackBehaviour.Pop0);

		// Token: 0x040030FD RID: 12541
		public static readonly OpCode Ldc_R8 = new OpCode("ldc.r8", Code.Ldc_R8, OperandType.InlineR, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr8, StackBehaviour.Pop0);

		// Token: 0x040030FE RID: 12542
		public static readonly OpCode Dup = new OpCode("dup", Code.Dup, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1_push1, StackBehaviour.Pop1);

		// Token: 0x040030FF RID: 12543
		public static readonly OpCode Pop = new OpCode("pop", Code.Pop, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x04003100 RID: 12544
		public static readonly OpCode Jmp = new OpCode("jmp", Code.Jmp, OperandType.InlineMethod, FlowControl.Call, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x04003101 RID: 12545
		public static readonly OpCode Call = new OpCode("call", Code.Call, OperandType.InlineMethod, FlowControl.Call, OpCodeType.Primitive, StackBehaviour.Varpush, StackBehaviour.Varpop);

		// Token: 0x04003102 RID: 12546
		public static readonly OpCode Calli = new OpCode("calli", Code.Calli, OperandType.InlineSig, FlowControl.Call, OpCodeType.Primitive, StackBehaviour.Varpush, StackBehaviour.Varpop);

		// Token: 0x04003103 RID: 12547
		public static readonly OpCode Ret = new OpCode("ret", Code.Ret, OperandType.InlineNone, FlowControl.Return, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Varpop);

		// Token: 0x04003104 RID: 12548
		public static readonly OpCode Br_S = new OpCode("br.s", Code.Br_S, OperandType.ShortInlineBrTarget, FlowControl.Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x04003105 RID: 12549
		public static readonly OpCode Brfalse_S = new OpCode("brfalse.s", Code.Brfalse_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x04003106 RID: 12550
		public static readonly OpCode Brtrue_S = new OpCode("brtrue.s", Code.Brtrue_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x04003107 RID: 12551
		public static readonly OpCode Beq_S = new OpCode("beq.s", Code.Beq_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003108 RID: 12552
		public static readonly OpCode Bge_S = new OpCode("bge.s", Code.Bge_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003109 RID: 12553
		public static readonly OpCode Bgt_S = new OpCode("bgt.s", Code.Bgt_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310A RID: 12554
		public static readonly OpCode Ble_S = new OpCode("ble.s", Code.Ble_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310B RID: 12555
		public static readonly OpCode Blt_S = new OpCode("blt.s", Code.Blt_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310C RID: 12556
		public static readonly OpCode Bne_Un_S = new OpCode("bne.un.s", Code.Bne_Un_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310D RID: 12557
		public static readonly OpCode Bge_Un_S = new OpCode("bge.un.s", Code.Bge_Un_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310E RID: 12558
		public static readonly OpCode Bgt_Un_S = new OpCode("bgt.un.s", Code.Bgt_Un_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400310F RID: 12559
		public static readonly OpCode Ble_Un_S = new OpCode("ble.un.s", Code.Ble_Un_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003110 RID: 12560
		public static readonly OpCode Blt_Un_S = new OpCode("blt.un.s", Code.Blt_Un_S, OperandType.ShortInlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003111 RID: 12561
		public static readonly OpCode Br = new OpCode("br", Code.Br, OperandType.InlineBrTarget, FlowControl.Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x04003112 RID: 12562
		public static readonly OpCode Brfalse = new OpCode("brfalse", Code.Brfalse, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x04003113 RID: 12563
		public static readonly OpCode Brtrue = new OpCode("brtrue", Code.Brtrue, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x04003114 RID: 12564
		public static readonly OpCode Beq = new OpCode("beq", Code.Beq, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003115 RID: 12565
		public static readonly OpCode Bge = new OpCode("bge", Code.Bge, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003116 RID: 12566
		public static readonly OpCode Bgt = new OpCode("bgt", Code.Bgt, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003117 RID: 12567
		public static readonly OpCode Ble = new OpCode("ble", Code.Ble, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003118 RID: 12568
		public static readonly OpCode Blt = new OpCode("blt", Code.Blt, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x04003119 RID: 12569
		public static readonly OpCode Bne_Un = new OpCode("bne.un", Code.Bne_Un, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400311A RID: 12570
		public static readonly OpCode Bge_Un = new OpCode("bge.un", Code.Bge_Un, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400311B RID: 12571
		public static readonly OpCode Bgt_Un = new OpCode("bgt.un", Code.Bgt_Un, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400311C RID: 12572
		public static readonly OpCode Ble_Un = new OpCode("ble.un", Code.Ble_Un, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400311D RID: 12573
		public static readonly OpCode Blt_Un = new OpCode("blt.un", Code.Blt_Un, OperandType.InlineBrTarget, FlowControl.Cond_Branch, OpCodeType.Macro, StackBehaviour.Push0, StackBehaviour.Pop1_pop1);

		// Token: 0x0400311E RID: 12574
		public static readonly OpCode Switch = new OpCode("switch", Code.Switch, OperandType.InlineSwitch, FlowControl.Cond_Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x0400311F RID: 12575
		public static readonly OpCode Ldind_I1 = new OpCode("ldind.i1", Code.Ldind_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003120 RID: 12576
		public static readonly OpCode Ldind_U1 = new OpCode("ldind.u1", Code.Ldind_U1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003121 RID: 12577
		public static readonly OpCode Ldind_I2 = new OpCode("ldind.i2", Code.Ldind_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003122 RID: 12578
		public static readonly OpCode Ldind_U2 = new OpCode("ldind.u2", Code.Ldind_U2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003123 RID: 12579
		public static readonly OpCode Ldind_I4 = new OpCode("ldind.i4", Code.Ldind_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003124 RID: 12580
		public static readonly OpCode Ldind_U4 = new OpCode("ldind.u4", Code.Ldind_U4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003125 RID: 12581
		public static readonly OpCode Ldind_I8 = new OpCode("ldind.i8", Code.Ldind_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Popi);

		// Token: 0x04003126 RID: 12582
		public static readonly OpCode Ldind_I = new OpCode("ldind.i", Code.Ldind_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x04003127 RID: 12583
		public static readonly OpCode Ldind_R4 = new OpCode("ldind.r4", Code.Ldind_R4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr4, StackBehaviour.Popi);

		// Token: 0x04003128 RID: 12584
		public static readonly OpCode Ldind_R8 = new OpCode("ldind.r8", Code.Ldind_R8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr8, StackBehaviour.Popi);

		// Token: 0x04003129 RID: 12585
		public static readonly OpCode Ldind_Ref = new OpCode("ldind.ref", Code.Ldind_Ref, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushref, StackBehaviour.Popi);

		// Token: 0x0400312A RID: 12586
		public static readonly OpCode Stind_Ref = new OpCode("stind.ref", Code.Stind_Ref, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x0400312B RID: 12587
		public static readonly OpCode Stind_I1 = new OpCode("stind.i1", Code.Stind_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x0400312C RID: 12588
		public static readonly OpCode Stind_I2 = new OpCode("stind.i2", Code.Stind_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x0400312D RID: 12589
		public static readonly OpCode Stind_I4 = new OpCode("stind.i4", Code.Stind_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x0400312E RID: 12590
		public static readonly OpCode Stind_I8 = new OpCode("stind.i8", Code.Stind_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi8);

		// Token: 0x0400312F RID: 12591
		public static readonly OpCode Stind_R4 = new OpCode("stind.r4", Code.Stind_R4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popr4);

		// Token: 0x04003130 RID: 12592
		public static readonly OpCode Stind_R8 = new OpCode("stind.r8", Code.Stind_R8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popr8);

		// Token: 0x04003131 RID: 12593
		public static readonly OpCode Add = new OpCode("add", Code.Add, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003132 RID: 12594
		public static readonly OpCode Sub = new OpCode("sub", Code.Sub, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003133 RID: 12595
		public static readonly OpCode Mul = new OpCode("mul", Code.Mul, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003134 RID: 12596
		public static readonly OpCode Div = new OpCode("div", Code.Div, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003135 RID: 12597
		public static readonly OpCode Div_Un = new OpCode("div.un", Code.Div_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003136 RID: 12598
		public static readonly OpCode Rem = new OpCode("rem", Code.Rem, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003137 RID: 12599
		public static readonly OpCode Rem_Un = new OpCode("rem.un", Code.Rem_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003138 RID: 12600
		public static readonly OpCode And = new OpCode("and", Code.And, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003139 RID: 12601
		public static readonly OpCode Or = new OpCode("or", Code.Or, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400313A RID: 12602
		public static readonly OpCode Xor = new OpCode("xor", Code.Xor, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400313B RID: 12603
		public static readonly OpCode Shl = new OpCode("shl", Code.Shl, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400313C RID: 12604
		public static readonly OpCode Shr = new OpCode("shr", Code.Shr, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400313D RID: 12605
		public static readonly OpCode Shr_Un = new OpCode("shr.un", Code.Shr_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400313E RID: 12606
		public static readonly OpCode Neg = new OpCode("neg", Code.Neg, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1);

		// Token: 0x0400313F RID: 12607
		public static readonly OpCode Not = new OpCode("not", Code.Not, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1);

		// Token: 0x04003140 RID: 12608
		public static readonly OpCode Conv_I1 = new OpCode("conv.i1", Code.Conv_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003141 RID: 12609
		public static readonly OpCode Conv_I2 = new OpCode("conv.i2", Code.Conv_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003142 RID: 12610
		public static readonly OpCode Conv_I4 = new OpCode("conv.i4", Code.Conv_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003143 RID: 12611
		public static readonly OpCode Conv_I8 = new OpCode("conv.i8", Code.Conv_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x04003144 RID: 12612
		public static readonly OpCode Conv_R4 = new OpCode("conv.r4", Code.Conv_R4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr4, StackBehaviour.Pop1);

		// Token: 0x04003145 RID: 12613
		public static readonly OpCode Conv_R8 = new OpCode("conv.r8", Code.Conv_R8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr8, StackBehaviour.Pop1);

		// Token: 0x04003146 RID: 12614
		public static readonly OpCode Conv_U4 = new OpCode("conv.u4", Code.Conv_U4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003147 RID: 12615
		public static readonly OpCode Conv_U8 = new OpCode("conv.u8", Code.Conv_U8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x04003148 RID: 12616
		public static readonly OpCode Callvirt = new OpCode("callvirt", Code.Callvirt, OperandType.InlineMethod, FlowControl.Call, OpCodeType.Objmodel, StackBehaviour.Varpush, StackBehaviour.Varpop);

		// Token: 0x04003149 RID: 12617
		public static readonly OpCode Cpobj = new OpCode("cpobj", Code.Cpobj, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x0400314A RID: 12618
		public static readonly OpCode Ldobj = new OpCode("ldobj", Code.Ldobj, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push1, StackBehaviour.Popi);

		// Token: 0x0400314B RID: 12619
		public static readonly OpCode Ldstr = new OpCode("ldstr", Code.Ldstr, OperandType.InlineString, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushref, StackBehaviour.Pop0);

		// Token: 0x0400314C RID: 12620
		public static readonly OpCode Newobj = new OpCode("newobj", Code.Newobj, OperandType.InlineMethod, FlowControl.Call, OpCodeType.Objmodel, StackBehaviour.Pushref, StackBehaviour.Varpop);

		// Token: 0x0400314D RID: 12621
		public static readonly OpCode Castclass = new OpCode("castclass", Code.Castclass, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushref, StackBehaviour.Popref);

		// Token: 0x0400314E RID: 12622
		public static readonly OpCode Isinst = new OpCode("isinst", Code.Isinst, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref);

		// Token: 0x0400314F RID: 12623
		public static readonly OpCode Conv_R_Un = new OpCode("conv.r.un", Code.Conv_R_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr8, StackBehaviour.Pop1);

		// Token: 0x04003150 RID: 12624
		public static readonly OpCode Unbox = new OpCode("unbox", Code.Unbox, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popref);

		// Token: 0x04003151 RID: 12625
		public static readonly OpCode Throw = new OpCode("throw", Code.Throw, OperandType.InlineNone, FlowControl.Throw, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref);

		// Token: 0x04003152 RID: 12626
		public static readonly OpCode Ldfld = new OpCode("ldfld", Code.Ldfld, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push1, StackBehaviour.Popref);

		// Token: 0x04003153 RID: 12627
		public static readonly OpCode Ldflda = new OpCode("ldflda", Code.Ldflda, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref);

		// Token: 0x04003154 RID: 12628
		public static readonly OpCode Stfld = new OpCode("stfld", Code.Stfld, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_pop1);

		// Token: 0x04003155 RID: 12629
		public static readonly OpCode Ldsfld = new OpCode("ldsfld", Code.Ldsfld, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x04003156 RID: 12630
		public static readonly OpCode Ldsflda = new OpCode("ldsflda", Code.Ldsflda, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x04003157 RID: 12631
		public static readonly OpCode Stsfld = new OpCode("stsfld", Code.Stsfld, OperandType.InlineField, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x04003158 RID: 12632
		public static readonly OpCode Stobj = new OpCode("stobj", Code.Stobj, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_pop1);

		// Token: 0x04003159 RID: 12633
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode("conv.ovf.i1.un", Code.Conv_Ovf_I1_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400315A RID: 12634
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode("conv.ovf.i2.un", Code.Conv_Ovf_I2_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400315B RID: 12635
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode("conv.ovf.i4.un", Code.Conv_Ovf_I4_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400315C RID: 12636
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode("conv.ovf.i8.un", Code.Conv_Ovf_I8_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x0400315D RID: 12637
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode("conv.ovf.u1.un", Code.Conv_Ovf_U1_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400315E RID: 12638
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode("conv.ovf.u2.un", Code.Conv_Ovf_U2_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400315F RID: 12639
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode("conv.ovf.u4.un", Code.Conv_Ovf_U4_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003160 RID: 12640
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode("conv.ovf.u8.un", Code.Conv_Ovf_U8_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x04003161 RID: 12641
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode("conv.ovf.i.un", Code.Conv_Ovf_I_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003162 RID: 12642
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode("conv.ovf.u.un", Code.Conv_Ovf_U_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003163 RID: 12643
		public static readonly OpCode Box = new OpCode("box", Code.Box, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushref, StackBehaviour.Pop1);

		// Token: 0x04003164 RID: 12644
		public static readonly OpCode Newarr = new OpCode("newarr", Code.Newarr, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushref, StackBehaviour.Popi);

		// Token: 0x04003165 RID: 12645
		public static readonly OpCode Ldlen = new OpCode("ldlen", Code.Ldlen, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref);

		// Token: 0x04003166 RID: 12646
		public static readonly OpCode Ldelema = new OpCode("ldelema", Code.Ldelema, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x04003167 RID: 12647
		public static readonly OpCode Ldelem_I1 = new OpCode("ldelem.i1", Code.Ldelem_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x04003168 RID: 12648
		public static readonly OpCode Ldelem_U1 = new OpCode("ldelem.u1", Code.Ldelem_U1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x04003169 RID: 12649
		public static readonly OpCode Ldelem_I2 = new OpCode("ldelem.i2", Code.Ldelem_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x0400316A RID: 12650
		public static readonly OpCode Ldelem_U2 = new OpCode("ldelem.u2", Code.Ldelem_U2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x0400316B RID: 12651
		public static readonly OpCode Ldelem_I4 = new OpCode("ldelem.i4", Code.Ldelem_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x0400316C RID: 12652
		public static readonly OpCode Ldelem_U4 = new OpCode("ldelem.u4", Code.Ldelem_U4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x0400316D RID: 12653
		public static readonly OpCode Ldelem_I8 = new OpCode("ldelem.i8", Code.Ldelem_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi8, StackBehaviour.Popref_popi);

		// Token: 0x0400316E RID: 12654
		public static readonly OpCode Ldelem_I = new OpCode("ldelem.i", Code.Ldelem_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushi, StackBehaviour.Popref_popi);

		// Token: 0x0400316F RID: 12655
		public static readonly OpCode Ldelem_R4 = new OpCode("ldelem.r4", Code.Ldelem_R4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushr4, StackBehaviour.Popref_popi);

		// Token: 0x04003170 RID: 12656
		public static readonly OpCode Ldelem_R8 = new OpCode("ldelem.r8", Code.Ldelem_R8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushr8, StackBehaviour.Popref_popi);

		// Token: 0x04003171 RID: 12657
		public static readonly OpCode Ldelem_Ref = new OpCode("ldelem.ref", Code.Ldelem_Ref, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Pushref, StackBehaviour.Popref_popi);

		// Token: 0x04003172 RID: 12658
		public static readonly OpCode Stelem_I = new OpCode("stelem.i", Code.Stelem_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popi);

		// Token: 0x04003173 RID: 12659
		public static readonly OpCode Stelem_I1 = new OpCode("stelem.i1", Code.Stelem_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popi);

		// Token: 0x04003174 RID: 12660
		public static readonly OpCode Stelem_I2 = new OpCode("stelem.i2", Code.Stelem_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popi);

		// Token: 0x04003175 RID: 12661
		public static readonly OpCode Stelem_I4 = new OpCode("stelem.i4", Code.Stelem_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popi);

		// Token: 0x04003176 RID: 12662
		public static readonly OpCode Stelem_I8 = new OpCode("stelem.i8", Code.Stelem_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popi8);

		// Token: 0x04003177 RID: 12663
		public static readonly OpCode Stelem_R4 = new OpCode("stelem.r4", Code.Stelem_R4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popr4);

		// Token: 0x04003178 RID: 12664
		public static readonly OpCode Stelem_R8 = new OpCode("stelem.r8", Code.Stelem_R8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popr8);

		// Token: 0x04003179 RID: 12665
		public static readonly OpCode Stelem_Ref = new OpCode("stelem.ref", Code.Stelem_Ref, OperandType.InlineNone, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_popref);

		// Token: 0x0400317A RID: 12666
		public static readonly OpCode Ldelem = new OpCode("ldelem", Code.Ldelem, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push1, StackBehaviour.Popref_popi);

		// Token: 0x0400317B RID: 12667
		public static readonly OpCode Stelem = new OpCode("stelem", Code.Stelem, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popref_popi_pop1);

		// Token: 0x0400317C RID: 12668
		public static readonly OpCode Unbox_Any = new OpCode("unbox.any", Code.Unbox_Any, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push1, StackBehaviour.Popref);

		// Token: 0x0400317D RID: 12669
		public static readonly OpCode Conv_Ovf_I1 = new OpCode("conv.ovf.i1", Code.Conv_Ovf_I1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400317E RID: 12670
		public static readonly OpCode Conv_Ovf_U1 = new OpCode("conv.ovf.u1", Code.Conv_Ovf_U1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400317F RID: 12671
		public static readonly OpCode Conv_Ovf_I2 = new OpCode("conv.ovf.i2", Code.Conv_Ovf_I2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003180 RID: 12672
		public static readonly OpCode Conv_Ovf_U2 = new OpCode("conv.ovf.u2", Code.Conv_Ovf_U2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003181 RID: 12673
		public static readonly OpCode Conv_Ovf_I4 = new OpCode("conv.ovf.i4", Code.Conv_Ovf_I4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003182 RID: 12674
		public static readonly OpCode Conv_Ovf_U4 = new OpCode("conv.ovf.u4", Code.Conv_Ovf_U4, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003183 RID: 12675
		public static readonly OpCode Conv_Ovf_I8 = new OpCode("conv.ovf.i8", Code.Conv_Ovf_I8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x04003184 RID: 12676
		public static readonly OpCode Conv_Ovf_U8 = new OpCode("conv.ovf.u8", Code.Conv_Ovf_U8, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi8, StackBehaviour.Pop1);

		// Token: 0x04003185 RID: 12677
		public static readonly OpCode Refanyval = new OpCode("refanyval", Code.Refanyval, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003186 RID: 12678
		public static readonly OpCode Ckfinite = new OpCode("ckfinite", Code.Ckfinite, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushr8, StackBehaviour.Pop1);

		// Token: 0x04003187 RID: 12679
		public static readonly OpCode Mkrefany = new OpCode("mkrefany", Code.Mkrefany, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Popi);

		// Token: 0x04003188 RID: 12680
		public static readonly OpCode Ldtoken = new OpCode("ldtoken", Code.Ldtoken, OperandType.InlineTok, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x04003189 RID: 12681
		public static readonly OpCode Conv_U2 = new OpCode("conv.u2", Code.Conv_U2, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400318A RID: 12682
		public static readonly OpCode Conv_U1 = new OpCode("conv.u1", Code.Conv_U1, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400318B RID: 12683
		public static readonly OpCode Conv_I = new OpCode("conv.i", Code.Conv_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400318C RID: 12684
		public static readonly OpCode Conv_Ovf_I = new OpCode("conv.ovf.i", Code.Conv_Ovf_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400318D RID: 12685
		public static readonly OpCode Conv_Ovf_U = new OpCode("conv.ovf.u", Code.Conv_Ovf_U, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x0400318E RID: 12686
		public static readonly OpCode Add_Ovf = new OpCode("add.ovf", Code.Add_Ovf, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x0400318F RID: 12687
		public static readonly OpCode Add_Ovf_Un = new OpCode("add.ovf.un", Code.Add_Ovf_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003190 RID: 12688
		public static readonly OpCode Mul_Ovf = new OpCode("mul.ovf", Code.Mul_Ovf, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003191 RID: 12689
		public static readonly OpCode Mul_Ovf_Un = new OpCode("mul.ovf.un", Code.Mul_Ovf_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003192 RID: 12690
		public static readonly OpCode Sub_Ovf = new OpCode("sub.ovf", Code.Sub_Ovf, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003193 RID: 12691
		public static readonly OpCode Sub_Ovf_Un = new OpCode("sub.ovf.un", Code.Sub_Ovf_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop1_pop1);

		// Token: 0x04003194 RID: 12692
		public static readonly OpCode Endfinally = new OpCode("endfinally", Code.Endfinally, OperandType.InlineNone, FlowControl.Return, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.PopAll);

		// Token: 0x04003195 RID: 12693
		public static readonly OpCode Leave = new OpCode("leave", Code.Leave, OperandType.InlineBrTarget, FlowControl.Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.PopAll);

		// Token: 0x04003196 RID: 12694
		public static readonly OpCode Leave_S = new OpCode("leave.s", Code.Leave_S, OperandType.ShortInlineBrTarget, FlowControl.Branch, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.PopAll);

		// Token: 0x04003197 RID: 12695
		public static readonly OpCode Stind_I = new OpCode("stind.i", Code.Stind_I, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi);

		// Token: 0x04003198 RID: 12696
		public static readonly OpCode Conv_U = new OpCode("conv.u", Code.Conv_U, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x04003199 RID: 12697
		public static readonly OpCode Prefix7 = new OpCode("prefix7", Code.Prefix7, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319A RID: 12698
		public static readonly OpCode Prefix6 = new OpCode("prefix6", Code.Prefix6, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319B RID: 12699
		public static readonly OpCode Prefix5 = new OpCode("prefix5", Code.Prefix5, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319C RID: 12700
		public static readonly OpCode Prefix4 = new OpCode("prefix4", Code.Prefix4, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319D RID: 12701
		public static readonly OpCode Prefix3 = new OpCode("prefix3", Code.Prefix3, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319E RID: 12702
		public static readonly OpCode Prefix2 = new OpCode("prefix2", Code.Prefix2, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x0400319F RID: 12703
		public static readonly OpCode Prefix1 = new OpCode("prefix1", Code.Prefix1, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031A0 RID: 12704
		public static readonly OpCode Prefixref = new OpCode("prefixref", Code.Prefixref, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Nternal, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031A1 RID: 12705
		public static readonly OpCode Arglist = new OpCode("arglist", Code.Arglist, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040031A2 RID: 12706
		public static readonly OpCode Ceq = new OpCode("ceq", Code.Ceq, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1_pop1);

		// Token: 0x040031A3 RID: 12707
		public static readonly OpCode Cgt = new OpCode("cgt", Code.Cgt, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1_pop1);

		// Token: 0x040031A4 RID: 12708
		public static readonly OpCode Cgt_Un = new OpCode("cgt.un", Code.Cgt_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1_pop1);

		// Token: 0x040031A5 RID: 12709
		public static readonly OpCode Clt = new OpCode("clt", Code.Clt, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1_pop1);

		// Token: 0x040031A6 RID: 12710
		public static readonly OpCode Clt_Un = new OpCode("clt.un", Code.Clt_Un, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1_pop1);

		// Token: 0x040031A7 RID: 12711
		public static readonly OpCode Ldftn = new OpCode("ldftn", Code.Ldftn, OperandType.InlineMethod, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040031A8 RID: 12712
		public static readonly OpCode Ldvirtftn = new OpCode("ldvirtftn", Code.Ldvirtftn, OperandType.InlineMethod, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popref);

		// Token: 0x040031A9 RID: 12713
		public static readonly OpCode Ldarg = new OpCode("ldarg", Code.Ldarg, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040031AA RID: 12714
		public static readonly OpCode Ldarga = new OpCode("ldarga", Code.Ldarga, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040031AB RID: 12715
		public static readonly OpCode Starg = new OpCode("starg", Code.Starg, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040031AC RID: 12716
		public static readonly OpCode Ldloc = new OpCode("ldloc", Code.Ldloc, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push1, StackBehaviour.Pop0);

		// Token: 0x040031AD RID: 12717
		public static readonly OpCode Ldloca = new OpCode("ldloca", Code.Ldloca, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040031AE RID: 12718
		public static readonly OpCode Stloc = new OpCode("stloc", Code.Stloc, OperandType.InlineVar, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Pop1);

		// Token: 0x040031AF RID: 12719
		public static readonly OpCode Localloc = new OpCode("localloc", Code.Localloc, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Popi);

		// Token: 0x040031B0 RID: 12720
		public static readonly OpCode Endfilter = new OpCode("endfilter", Code.Endfilter, OperandType.InlineNone, FlowControl.Return, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x040031B1 RID: 12721
		public static readonly OpCode Unaligned = new OpCode("unaligned.", Code.Unaligned, OperandType.ShortInlineI, FlowControl.Meta, OpCodeType.Prefix, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031B2 RID: 12722
		public static readonly OpCode Volatile = new OpCode("volatile.", Code.Volatile, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Prefix, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031B3 RID: 12723
		public static readonly OpCode Tailcall = new OpCode("tail.", Code.Tailcall, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Prefix, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031B4 RID: 12724
		public static readonly OpCode Initobj = new OpCode("initobj", Code.Initobj, OperandType.InlineType, FlowControl.Next, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Popi);

		// Token: 0x040031B5 RID: 12725
		public static readonly OpCode Constrained = new OpCode("constrained.", Code.Constrained, OperandType.InlineType, FlowControl.Meta, OpCodeType.Prefix, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031B6 RID: 12726
		public static readonly OpCode Cpblk = new OpCode("cpblk", Code.Cpblk, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi_popi);

		// Token: 0x040031B7 RID: 12727
		public static readonly OpCode Initblk = new OpCode("initblk", Code.Initblk, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Push0, StackBehaviour.Popi_popi_popi);

		// Token: 0x040031B8 RID: 12728
		public static readonly OpCode Rethrow = new OpCode("rethrow", Code.Rethrow, OperandType.InlineNone, FlowControl.Throw, OpCodeType.Objmodel, StackBehaviour.Push0, StackBehaviour.Pop0);

		// Token: 0x040031B9 RID: 12729
		public static readonly OpCode Sizeof = new OpCode("sizeof", Code.Sizeof, OperandType.InlineType, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop0);

		// Token: 0x040031BA RID: 12730
		public static readonly OpCode Refanytype = new OpCode("refanytype", Code.Refanytype, OperandType.InlineNone, FlowControl.Next, OpCodeType.Primitive, StackBehaviour.Pushi, StackBehaviour.Pop1);

		// Token: 0x040031BB RID: 12731
		public static readonly OpCode Readonly = new OpCode("readonly.", Code.Readonly, OperandType.InlineNone, FlowControl.Meta, OpCodeType.Prefix, StackBehaviour.Push0, StackBehaviour.Pop0);
	}
}
