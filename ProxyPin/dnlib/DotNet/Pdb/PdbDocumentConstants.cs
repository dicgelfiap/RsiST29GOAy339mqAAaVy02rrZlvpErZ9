using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200090E RID: 2318
	[ComVisible(true)]
	public static class PdbDocumentConstants
	{
		// Token: 0x04002B64 RID: 11108
		public static readonly Guid LanguageCSharp = new Guid("3F5162F8-07C6-11D3-9053-00C04FA302A1");

		// Token: 0x04002B65 RID: 11109
		public static readonly Guid LanguageVisualBasic = new Guid("3A12D0B8-C26C-11D0-B442-00A0244A1DD2");

		// Token: 0x04002B66 RID: 11110
		public static readonly Guid LanguageFSharp = new Guid("AB4F38C9-B6E6-43BA-BE3B-58080B2CCCE3");

		// Token: 0x04002B67 RID: 11111
		public static readonly Guid HashSHA1 = new Guid("FF1816EC-AA5E-4D10-87F7-6F4963833460");

		// Token: 0x04002B68 RID: 11112
		public static readonly Guid HashSHA256 = new Guid("8829D00F-11B8-4213-878B-770E8597AC16");

		// Token: 0x04002B69 RID: 11113
		public static readonly Guid LanguageVendorMicrosoft = new Guid("994B45C4-E6E9-11D2-903F-00C04FA302A1");

		// Token: 0x04002B6A RID: 11114
		public static readonly Guid DocumentTypeText = new Guid("5A869D0B-6611-11D3-BD2A-0000F80849BD");
	}
}
