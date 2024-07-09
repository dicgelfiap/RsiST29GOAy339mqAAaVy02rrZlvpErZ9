using System;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000979 RID: 2425
	internal sealed class SymbolDocumentImpl : SymbolDocument
	{
		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x001C0E64 File Offset: 0x001C0E64
		public ISymUnmanagedDocument SymUnmanagedDocument
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x001C0E6C File Offset: 0x001C0E6C
		public SymbolDocumentImpl(ISymUnmanagedDocument document)
		{
			this.document = document;
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x001C0E7C File Offset: 0x001C0E7C
		public override Guid CheckSumAlgorithmId
		{
			get
			{
				Guid result;
				this.document.GetCheckSumAlgorithmId(out result);
				return result;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x001C0E9C File Offset: 0x001C0E9C
		public override Guid DocumentType
		{
			get
			{
				Guid result;
				this.document.GetDocumentType(out result);
				return result;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x001C0EBC File Offset: 0x001C0EBC
		public override Guid Language
		{
			get
			{
				Guid result;
				this.document.GetLanguage(out result);
				return result;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x001C0EDC File Offset: 0x001C0EDC
		public override Guid LanguageVendor
		{
			get
			{
				Guid result;
				this.document.GetLanguageVendor(out result);
				return result;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x001C0EFC File Offset: 0x001C0EFC
		public override string URL
		{
			get
			{
				uint num;
				this.document.GetURL(0U, out num, null);
				char[] array = new char[num];
				this.document.GetURL((uint)array.Length, out num, array);
				if (array.Length == 0)
				{
					return string.Empty;
				}
				return new string(array, 0, array.Length - 1);
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x001C0F50 File Offset: 0x001C0F50
		public override byte[] CheckSum
		{
			get
			{
				uint num;
				this.document.GetCheckSum(0U, out num, null);
				byte[] array = new byte[num];
				this.document.GetCheckSum((uint)array.Length, out num, array);
				return array;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x001C0F8C File Offset: 0x001C0F8C
		private byte[] SourceCode
		{
			get
			{
				int num;
				if (this.document.GetSourceLength(out num) < 0)
				{
					return null;
				}
				if (num <= 0)
				{
					return null;
				}
				byte[] array = new byte[num];
				int num2;
				if (this.document.GetSourceRange(0U, 0U, 2147483647U, 2147483647U, num, out num2, array) < 0)
				{
					return null;
				}
				if (num2 <= 0)
				{
					return null;
				}
				if (num2 != array.Length)
				{
					Array.Resize<byte>(ref array, num2);
				}
				return array;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x001C1000 File Offset: 0x001C1000
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					byte[] sourceCode = this.SourceCode;
					if (sourceCode != null)
					{
						this.customDebugInfos = new PdbCustomDebugInfo[]
						{
							new PdbEmbeddedSourceCustomDebugInfo(sourceCode)
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

		// Token: 0x04002D5B RID: 11611
		private readonly ISymUnmanagedDocument document;

		// Token: 0x04002D5C RID: 11612
		private PdbCustomDebugInfo[] customDebugInfos;
	}
}
