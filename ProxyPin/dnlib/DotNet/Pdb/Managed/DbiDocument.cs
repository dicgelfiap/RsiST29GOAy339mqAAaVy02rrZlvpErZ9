using System;
using System.Diagnostics.SymbolStore;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000948 RID: 2376
	internal sealed class DbiDocument : SymbolDocument
	{
		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x001BDBFC File Offset: 0x001BDBFC
		public override string URL
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x001BDC04 File Offset: 0x001BDC04
		public override Guid Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x001BDC0C File Offset: 0x001BDC0C
		public override Guid LanguageVendor
		{
			get
			{
				return this.languageVendor;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x001BDC14 File Offset: 0x001BDC14
		public override Guid DocumentType
		{
			get
			{
				return this.documentType;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x001BDC1C File Offset: 0x001BDC1C
		public override Guid CheckSumAlgorithmId
		{
			get
			{
				return this.checkSumAlgorithmId;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x001BDC24 File Offset: 0x001BDC24
		public override byte[] CheckSum
		{
			get
			{
				return this.checkSum;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x001BDC2C File Offset: 0x001BDC2C
		private byte[] SourceCode
		{
			get
			{
				return this.sourceCode;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x001BDC34 File Offset: 0x001BDC34
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					byte[] array = this.SourceCode;
					if (array != null)
					{
						this.customDebugInfos = new PdbCustomDebugInfo[]
						{
							new PdbEmbeddedSourceCustomDebugInfo(array)
						};
					}
					else
					{
						this.customDebugInfos = Array2.Empty<PdbCustomDebugInfo>();
					}
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x001BDC88 File Offset: 0x001BDC88
		public DbiDocument(string url)
		{
			this.url = url;
			this.documentType = SymDocumentType.Text;
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x001BDCA4 File Offset: 0x001BDCA4
		public void Read(ref DataReader reader)
		{
			reader.Position = 0U;
			this.language = reader.ReadGuid();
			this.languageVendor = reader.ReadGuid();
			this.documentType = reader.ReadGuid();
			this.checkSumAlgorithmId = reader.ReadGuid();
			int length = reader.ReadInt32();
			int num = reader.ReadInt32();
			this.checkSum = reader.ReadBytes(length);
			this.sourceCode = ((num == 0) ? null : reader.ReadBytes(num));
		}

		// Token: 0x04002C26 RID: 11302
		private readonly string url;

		// Token: 0x04002C27 RID: 11303
		private Guid language;

		// Token: 0x04002C28 RID: 11304
		private Guid languageVendor;

		// Token: 0x04002C29 RID: 11305
		private Guid documentType;

		// Token: 0x04002C2A RID: 11306
		private Guid checkSumAlgorithmId;

		// Token: 0x04002C2B RID: 11307
		private byte[] checkSum;

		// Token: 0x04002C2C RID: 11308
		private byte[] sourceCode;

		// Token: 0x04002C2D RID: 11309
		private PdbCustomDebugInfo[] customDebugInfos;
	}
}
