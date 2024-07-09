using System;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000980 RID: 2432
	internal sealed class SymbolVariableImpl : SymbolVariable
	{
		// Token: 0x06005DB5 RID: 23989 RVA: 0x001C1FE0 File Offset: 0x001C1FE0
		public SymbolVariableImpl(ISymUnmanagedVariable variable)
		{
			this.variable = variable;
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x001C1FF0 File Offset: 0x001C1FF0
		public override int Index
		{
			get
			{
				uint result;
				this.variable.GetAddressField1(out result);
				return (int)result;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06005DB7 RID: 23991 RVA: 0x001C2010 File Offset: 0x001C2010
		public override PdbLocalAttributes Attributes
		{
			get
			{
				uint num;
				this.variable.GetAttributes(out num);
				if ((num & 1U) != 0U)
				{
					return PdbLocalAttributes.DebuggerHidden;
				}
				return PdbLocalAttributes.None;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x001C203C File Offset: 0x001C203C
		public override string Name
		{
			get
			{
				uint num;
				this.variable.GetName(0U, out num, null);
				char[] array = new char[num];
				this.variable.GetName((uint)array.Length, out num, array);
				if (array.Length == 0)
				{
					return string.Empty;
				}
				return new string(array, 0, array.Length - 1);
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x001C2090 File Offset: 0x001C2090
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				return Array2.Empty<PdbCustomDebugInfo>();
			}
		}

		// Token: 0x04002D75 RID: 11637
		private readonly ISymUnmanagedVariable variable;
	}
}
