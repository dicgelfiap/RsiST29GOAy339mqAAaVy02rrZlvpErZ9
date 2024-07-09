using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000935 RID: 2357
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public struct SymbolSequencePoint
	{
		// Token: 0x06005ACB RID: 23243 RVA: 0x001B9E84 File Offset: 0x001B9E84
		private readonly string GetDebuggerString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Line == 16707566 && this.EndLine == 16707566)
			{
				stringBuilder.Append("<hidden>");
			}
			else
			{
				stringBuilder.Append("(");
				stringBuilder.Append(this.Line);
				stringBuilder.Append(",");
				stringBuilder.Append(this.Column);
				stringBuilder.Append(")-(");
				stringBuilder.Append(this.EndLine);
				stringBuilder.Append(",");
				stringBuilder.Append(this.EndColumn);
				stringBuilder.Append(")");
			}
			stringBuilder.Append(": ");
			stringBuilder.Append(this.Document.URL);
			return stringBuilder.ToString();
		}

		// Token: 0x04002BD9 RID: 11225
		public int Offset;

		// Token: 0x04002BDA RID: 11226
		public SymbolDocument Document;

		// Token: 0x04002BDB RID: 11227
		public int Line;

		// Token: 0x04002BDC RID: 11228
		public int Column;

		// Token: 0x04002BDD RID: 11229
		public int EndLine;

		// Token: 0x04002BDE RID: 11230
		public int EndColumn;
	}
}
