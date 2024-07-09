using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E0 RID: 2528
	[ComVisible(true)]
	public static class InstructionPrinter
	{
		// Token: 0x060060ED RID: 24813 RVA: 0x001CE594 File Offset: 0x001CE594
		public static string ToString(Instruction instr)
		{
			if (instr == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("IL_{0:X4}: ", instr.Offset));
			stringBuilder.Append(instr.OpCode.Name);
			InstructionPrinter.AddOperandString(stringBuilder, instr, " ");
			return stringBuilder.ToString();
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x001CE5F8 File Offset: 0x001CE5F8
		public static string GetOperandString(Instruction instr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			InstructionPrinter.AddOperandString(stringBuilder, instr, string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x001CE610 File Offset: 0x001CE610
		public static void AddOperandString(StringBuilder sb, Instruction instr)
		{
			InstructionPrinter.AddOperandString(sb, instr, string.Empty);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x001CE620 File Offset: 0x001CE620
		public static void AddOperandString(StringBuilder sb, Instruction instr, string extra)
		{
			object operand = instr.Operand;
			switch (instr.OpCode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.ShortInlineBrTarget:
				sb.Append(extra);
				InstructionPrinter.AddInstructionTarget(sb, operand as Instruction);
				return;
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				sb.Append(extra);
				if (operand is IFullName)
				{
					sb.Append((operand as IFullName).FullName);
					return;
				}
				if (operand != null)
				{
					sb.Append(operand.ToString());
					return;
				}
				sb.Append("null");
				return;
			case OperandType.InlineI:
			case OperandType.InlineI8:
			case OperandType.InlineR:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineR:
				sb.Append(string.Format("{0}{1}", extra, operand));
				return;
			case OperandType.InlineNone:
			case OperandType.InlinePhi:
			case OperandType.NOT_USED_8:
				break;
			case OperandType.InlineSig:
				sb.Append(extra);
				sb.Append(FullNameFactory.MethodFullName(null, null, operand as MethodSig, null, null, null, null));
				return;
			case OperandType.InlineString:
				sb.Append(extra);
				InstructionPrinter.EscapeString(sb, operand as string, true);
				return;
			case OperandType.InlineSwitch:
			{
				IList<Instruction> list = operand as IList<Instruction>;
				if (list == null)
				{
					sb.Append("null");
					return;
				}
				sb.Append('(');
				for (int i = 0; i < list.Count; i++)
				{
					if (i != 0)
					{
						sb.Append(',');
					}
					InstructionPrinter.AddInstructionTarget(sb, list[i]);
				}
				sb.Append(')');
				return;
			}
			case OperandType.InlineVar:
			case OperandType.ShortInlineVar:
				sb.Append(extra);
				if (operand == null)
				{
					sb.Append("null");
					return;
				}
				sb.Append(operand.ToString());
				break;
			default:
				return;
			}
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x001CE7C8 File Offset: 0x001CE7C8
		private static void AddInstructionTarget(StringBuilder sb, Instruction targetInstr)
		{
			if (targetInstr == null)
			{
				sb.Append("null");
				return;
			}
			sb.Append(string.Format("IL_{0:X4}", targetInstr.Offset));
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x001CE7FC File Offset: 0x001CE7FC
		private static void EscapeString(StringBuilder sb, string s, bool addQuotes)
		{
			if (s == null)
			{
				sb.Append("null");
				return;
			}
			if (addQuotes)
			{
				sb.Append('"');
			}
			foreach (char c in s)
			{
				if (c < ' ')
				{
					switch (c)
					{
					case '\a':
						sb.Append("\\a");
						break;
					case '\b':
						sb.Append("\\b");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					case '\n':
						sb.Append("\\n");
						break;
					case '\v':
						sb.Append("\\v");
						break;
					case '\f':
						sb.Append("\\f");
						break;
					case '\r':
						sb.Append("\\r");
						break;
					default:
						sb.Append(string.Format("\\u{0:X4}", (int)c));
						break;
					}
				}
				else if (c == '\\' || c == '"')
				{
					sb.Append('\\');
					sb.Append(c);
				}
				else
				{
					sb.Append(c);
				}
			}
			if (addQuotes)
			{
				sb.Append('"');
			}
		}
	}
}
