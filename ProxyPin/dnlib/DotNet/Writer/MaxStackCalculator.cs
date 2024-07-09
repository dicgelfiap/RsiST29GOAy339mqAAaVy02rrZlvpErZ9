using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AA RID: 2218
	[ComVisible(true)]
	public struct MaxStackCalculator
	{
		// Token: 0x060054D2 RID: 21714 RVA: 0x0019D5B4 File Offset: 0x0019D5B4
		public static uint GetMaxStack(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers)
		{
			uint result;
			new MaxStackCalculator(instructions, exceptionHandlers).Calculate(out result);
			return result;
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0019D5D8 File Offset: 0x0019D5D8
		public static bool GetMaxStack(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers, out uint maxStack)
		{
			return new MaxStackCalculator(instructions, exceptionHandlers).Calculate(out maxStack);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0019D5FC File Offset: 0x0019D5FC
		internal static MaxStackCalculator Create()
		{
			return new MaxStackCalculator(true);
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0019D604 File Offset: 0x0019D604
		private MaxStackCalculator(bool dummy)
		{
			this.instructions = null;
			this.exceptionHandlers = null;
			this.stackHeights = new Dictionary<Instruction, int>();
			this.hasError = false;
			this.currentMaxStack = 0;
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x0019D630 File Offset: 0x0019D630
		private MaxStackCalculator(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers)
		{
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.stackHeights = new Dictionary<Instruction, int>();
			this.hasError = false;
			this.currentMaxStack = 0;
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0019D65C File Offset: 0x0019D65C
		internal void Reset(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers)
		{
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.stackHeights.Clear();
			this.hasError = false;
			this.currentMaxStack = 0;
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0019D688 File Offset: 0x0019D688
		internal bool Calculate(out uint maxStack)
		{
			IList<ExceptionHandler> list = this.exceptionHandlers;
			Dictionary<Instruction, int> dictionary = this.stackHeights;
			for (int i = 0; i < list.Count; i++)
			{
				ExceptionHandler exceptionHandler = list[i];
				if (exceptionHandler != null)
				{
					Instruction key;
					if ((key = exceptionHandler.TryStart) != null)
					{
						dictionary[key] = 0;
					}
					if ((key = exceptionHandler.FilterStart) != null)
					{
						dictionary[key] = 1;
						this.currentMaxStack = 1;
					}
					if ((key = exceptionHandler.HandlerStart) != null)
					{
						if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch || exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
						{
							dictionary[key] = 1;
							this.currentMaxStack = 1;
						}
						else
						{
							dictionary[key] = 0;
						}
					}
				}
			}
			int num = 0;
			bool flag = false;
			IList<Instruction> list2 = this.instructions;
			for (int j = 0; j < list2.Count; j++)
			{
				Instruction instruction = list2[j];
				if (instruction != null)
				{
					if (flag)
					{
						dictionary.TryGetValue(instruction, out num);
						flag = false;
					}
					num = this.WriteStack(instruction, num);
					OpCode opCode = instruction.OpCode;
					Code code = opCode.Code;
					if (code == Code.Jmp)
					{
						if (num != 0)
						{
							this.hasError = true;
						}
					}
					else
					{
						int num2;
						int num3;
						instruction.CalculateStackUsage(out num2, out num3);
						if (num3 == -1)
						{
							num = 0;
						}
						else
						{
							num -= num3;
							if (num < 0)
							{
								this.hasError = true;
								num = 0;
							}
							num += num2;
						}
					}
					if (num < 0)
					{
						this.hasError = true;
						num = 0;
					}
					switch (opCode.FlowControl)
					{
					case FlowControl.Branch:
						this.WriteStack(instruction.Operand as Instruction, num);
						flag = true;
						break;
					case FlowControl.Call:
						if (code == Code.Jmp)
						{
							flag = true;
						}
						break;
					case FlowControl.Cond_Branch:
						if (code == Code.Switch)
						{
							IList<Instruction> list3 = instruction.Operand as IList<Instruction>;
							if (list3 != null)
							{
								for (int k = 0; k < list3.Count; k++)
								{
									this.WriteStack(list3[k], num);
								}
							}
						}
						else
						{
							this.WriteStack(instruction.Operand as Instruction, num);
						}
						break;
					case FlowControl.Return:
					case FlowControl.Throw:
						flag = true;
						break;
					}
				}
			}
			maxStack = (uint)this.currentMaxStack;
			return !this.hasError;
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x0019D904 File Offset: 0x0019D904
		private int WriteStack(Instruction instr, int stack)
		{
			if (instr == null)
			{
				this.hasError = true;
				return stack;
			}
			Dictionary<Instruction, int> dictionary = this.stackHeights;
			int num;
			if (dictionary.TryGetValue(instr, out num))
			{
				if (stack != num)
				{
					this.hasError = true;
				}
				return num;
			}
			dictionary[instr] = stack;
			if (stack > this.currentMaxStack)
			{
				this.currentMaxStack = stack;
			}
			return stack;
		}

		// Token: 0x040028BB RID: 10427
		private IList<Instruction> instructions;

		// Token: 0x040028BC RID: 10428
		private IList<ExceptionHandler> exceptionHandlers;

		// Token: 0x040028BD RID: 10429
		private readonly Dictionary<Instruction, int> stackHeights;

		// Token: 0x040028BE RID: 10430
		private bool hasError;

		// Token: 0x040028BF RID: 10431
		private int currentMaxStack;
	}
}
