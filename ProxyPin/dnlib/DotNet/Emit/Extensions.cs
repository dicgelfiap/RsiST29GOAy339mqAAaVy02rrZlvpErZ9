using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009D9 RID: 2521
	[ComVisible(true)]
	public static class Extensions
	{
		// Token: 0x06006099 RID: 24729 RVA: 0x001CC828 File Offset: 0x001CC828
		public static OpCode ToOpCode(this Code code)
		{
			int num = (int)(code >> 8);
			int num2 = (int)((byte)code);
			if (num == 0)
			{
				return OpCodes.OneByteOpCodes[num2];
			}
			if (num == 254)
			{
				return OpCodes.TwoByteOpCodes[num2];
			}
			if (code == Code.UNKNOWN1)
			{
				return OpCodes.UNKNOWN1;
			}
			if (code == Code.UNKNOWN2)
			{
				return OpCodes.UNKNOWN2;
			}
			return null;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x001CC88C File Offset: 0x001CC88C
		public static OpCode GetOpCode(this Instruction self)
		{
			return ((self != null) ? self.OpCode : null) ?? OpCodes.UNKNOWN1;
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x001CC8AC File Offset: 0x001CC8AC
		public static object GetOperand(this Instruction self)
		{
			if (self == null)
			{
				return null;
			}
			return self.Operand;
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x001CC8BC File Offset: 0x001CC8BC
		public static uint GetOffset(this Instruction self)
		{
			if (self == null)
			{
				return 0U;
			}
			return self.Offset;
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x001CC8CC File Offset: 0x001CC8CC
		public static SequencePoint GetSequencePoint(this Instruction self)
		{
			if (self == null)
			{
				return null;
			}
			return self.SequencePoint;
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x001CC8DC File Offset: 0x001CC8DC
		public static IMDTokenProvider ResolveToken(this IInstructionOperandResolver self, uint token)
		{
			return self.ResolveToken(token, default(GenericParamContext));
		}
	}
}
