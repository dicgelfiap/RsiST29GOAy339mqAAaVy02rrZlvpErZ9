using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F0 RID: 2288
	[ComVisible(true)]
	public static class CustomDebugInfoGuids
	{
		// Token: 0x04002B10 RID: 11024
		public static readonly Guid AsyncMethodSteppingInformationBlob = new Guid("54FD2AC5-E925-401A-9C2A-F94F171072F8");

		// Token: 0x04002B11 RID: 11025
		public static readonly Guid DefaultNamespace = new Guid("58B2EAB6-209F-4E4E-A22C-B2D0F910C782");

		// Token: 0x04002B12 RID: 11026
		public static readonly Guid DynamicLocalVariables = new Guid("83C563C4-B4F3-47D5-B824-BA5441477EA8");

		// Token: 0x04002B13 RID: 11027
		public static readonly Guid EmbeddedSource = new Guid("0E8A571B-6926-466E-B4AD-8AB04611F5FE");

		// Token: 0x04002B14 RID: 11028
		public static readonly Guid EncLambdaAndClosureMap = new Guid("A643004C-0240-496F-A783-30D64F4979DE");

		// Token: 0x04002B15 RID: 11029
		public static readonly Guid EncLocalSlotMap = new Guid("755F52A8-91C5-45BE-B4B8-209571E552BD");

		// Token: 0x04002B16 RID: 11030
		public static readonly Guid SourceLink = new Guid("CC110556-A091-4D38-9FEC-25AB9A351A6A");

		// Token: 0x04002B17 RID: 11031
		public static readonly Guid StateMachineHoistedLocalScopes = new Guid("6DA9A61E-F8C7-4874-BE62-68BC5630DF71");

		// Token: 0x04002B18 RID: 11032
		public static readonly Guid TupleElementNames = new Guid("ED9FDF71-8879-4747-8ED3-FE5EDE3CE710");
	}
}
