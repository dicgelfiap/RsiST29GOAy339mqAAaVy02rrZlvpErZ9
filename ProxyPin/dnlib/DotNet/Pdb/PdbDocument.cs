using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200090D RID: 2317
	[DebuggerDisplay("{Url}")]
	[ComVisible(true)]
	public sealed class PdbDocument : IHasCustomDebugInformation
	{
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06005997 RID: 22935 RVA: 0x001B5EBC File Offset: 0x001B5EBC
		// (set) Token: 0x06005998 RID: 22936 RVA: 0x001B5EC4 File Offset: 0x001B5EC4
		public string Url { get; set; }

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x001B5ED0 File Offset: 0x001B5ED0
		// (set) Token: 0x0600599A RID: 22938 RVA: 0x001B5ED8 File Offset: 0x001B5ED8
		public Guid Language { get; set; }

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x0600599B RID: 22939 RVA: 0x001B5EE4 File Offset: 0x001B5EE4
		// (set) Token: 0x0600599C RID: 22940 RVA: 0x001B5EEC File Offset: 0x001B5EEC
		public Guid LanguageVendor { get; set; }

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x001B5EF8 File Offset: 0x001B5EF8
		// (set) Token: 0x0600599E RID: 22942 RVA: 0x001B5F00 File Offset: 0x001B5F00
		public Guid DocumentType { get; set; }

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x001B5F0C File Offset: 0x001B5F0C
		// (set) Token: 0x060059A0 RID: 22944 RVA: 0x001B5F14 File Offset: 0x001B5F14
		public Guid CheckSumAlgorithmId { get; set; }

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x060059A1 RID: 22945 RVA: 0x001B5F20 File Offset: 0x001B5F20
		// (set) Token: 0x060059A2 RID: 22946 RVA: 0x001B5F28 File Offset: 0x001B5F28
		public byte[] CheckSum { get; set; }

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x001B5F34 File Offset: 0x001B5F34
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x001B5F38 File Offset: 0x001B5F38
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x001B5F48 File Offset: 0x001B5F48
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x001B5F50 File Offset: 0x001B5F50
		public PdbDocument()
		{
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x001B5F58 File Offset: 0x001B5F58
		public PdbDocument(SymbolDocument symDoc) : this(symDoc, false)
		{
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x001B5F64 File Offset: 0x001B5F64
		private PdbDocument(SymbolDocument symDoc, bool partial)
		{
			if (symDoc == null)
			{
				throw new ArgumentNullException("symDoc");
			}
			this.Url = symDoc.URL;
			if (!partial)
			{
				this.Initialize(symDoc);
			}
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x001B5F98 File Offset: 0x001B5F98
		internal static PdbDocument CreatePartialForCompare(SymbolDocument symDoc)
		{
			return new PdbDocument(symDoc, true);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x001B5FA4 File Offset: 0x001B5FA4
		internal void Initialize(SymbolDocument symDoc)
		{
			this.Language = symDoc.Language;
			this.LanguageVendor = symDoc.LanguageVendor;
			this.DocumentType = symDoc.DocumentType;
			this.CheckSumAlgorithmId = symDoc.CheckSumAlgorithmId;
			this.CheckSum = symDoc.CheckSum;
			this.customDebugInfos = new List<PdbCustomDebugInfo>();
			foreach (PdbCustomDebugInfo item in symDoc.CustomDebugInfos)
			{
				this.customDebugInfos.Add(item);
			}
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x001B6028 File Offset: 0x001B6028
		public PdbDocument(string url, Guid language, Guid languageVendor, Guid documentType, Guid checkSumAlgorithmId, byte[] checkSum)
		{
			this.Url = url;
			this.Language = language;
			this.LanguageVendor = languageVendor;
			this.DocumentType = documentType;
			this.CheckSumAlgorithmId = checkSumAlgorithmId;
			this.CheckSum = checkSum;
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x001B606C File Offset: 0x001B606C
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Url ?? string.Empty);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x001B608C File Offset: 0x001B608C
		public override bool Equals(object obj)
		{
			PdbDocument pdbDocument = obj as PdbDocument;
			return pdbDocument != null && StringComparer.OrdinalIgnoreCase.Equals(this.Url ?? string.Empty, pdbDocument.Url ?? string.Empty);
		}

		// Token: 0x04002B63 RID: 11107
		private IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
