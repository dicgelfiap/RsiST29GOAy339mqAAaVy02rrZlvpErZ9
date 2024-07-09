using System;
using System.Diagnostics;
using System.Text;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000943 RID: 2371
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	internal sealed class SymbolDocumentImpl : SymbolDocument
	{
		// Token: 0x06005B2B RID: 23339 RVA: 0x001BD51C File Offset: 0x001BD51C
		private string GetDebuggerString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.language == PdbDocumentConstants.LanguageCSharp)
			{
				stringBuilder.Append("C#");
			}
			else if (this.language == PdbDocumentConstants.LanguageVisualBasic)
			{
				stringBuilder.Append("VB");
			}
			else if (this.language == PdbDocumentConstants.LanguageFSharp)
			{
				stringBuilder.Append("F#");
			}
			else
			{
				stringBuilder.Append(this.language.ToString());
			}
			stringBuilder.Append(", ");
			if (this.checkSumAlgorithmId == PdbDocumentConstants.HashSHA1)
			{
				stringBuilder.Append("SHA-1");
			}
			else if (this.checkSumAlgorithmId == PdbDocumentConstants.HashSHA256)
			{
				stringBuilder.Append("SHA-256");
			}
			else
			{
				stringBuilder.Append(this.checkSumAlgorithmId.ToString());
			}
			stringBuilder.Append(": ");
			stringBuilder.Append(this.url);
			return stringBuilder.ToString();
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06005B2C RID: 23340 RVA: 0x001BD64C File Offset: 0x001BD64C
		public override string URL
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x001BD654 File Offset: 0x001BD654
		public override Guid Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x001BD65C File Offset: 0x001BD65C
		public override Guid LanguageVendor
		{
			get
			{
				return this.languageVendor;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x001BD664 File Offset: 0x001BD664
		public override Guid DocumentType
		{
			get
			{
				return this.documentType;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x001BD66C File Offset: 0x001BD66C
		public override Guid CheckSumAlgorithmId
		{
			get
			{
				return this.checkSumAlgorithmId;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x001BD674 File Offset: 0x001BD674
		public override byte[] CheckSum
		{
			get
			{
				return this.checkSum;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x001BD67C File Offset: 0x001BD67C
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x001BD684 File Offset: 0x001BD684
		public SymbolDocumentImpl(string url, Guid language, Guid languageVendor, Guid documentType, Guid checkSumAlgorithmId, byte[] checkSum, PdbCustomDebugInfo[] customDebugInfos)
		{
			this.url = url;
			this.language = language;
			this.languageVendor = languageVendor;
			this.documentType = documentType;
			this.checkSumAlgorithmId = checkSumAlgorithmId;
			this.checkSum = checkSum;
			this.customDebugInfos = customDebugInfos;
		}

		// Token: 0x04002C0A RID: 11274
		private readonly string url;

		// Token: 0x04002C0B RID: 11275
		private Guid language;

		// Token: 0x04002C0C RID: 11276
		private Guid languageVendor;

		// Token: 0x04002C0D RID: 11277
		private Guid documentType;

		// Token: 0x04002C0E RID: 11278
		private Guid checkSumAlgorithmId;

		// Token: 0x04002C0F RID: 11279
		private readonly byte[] checkSum;

		// Token: 0x04002C10 RID: 11280
		private readonly PdbCustomDebugInfo[] customDebugInfos;
	}
}
