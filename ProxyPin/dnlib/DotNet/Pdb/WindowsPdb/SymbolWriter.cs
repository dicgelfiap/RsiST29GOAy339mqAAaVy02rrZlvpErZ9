using System;
using System.Diagnostics.SymbolStore;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x0200092D RID: 2349
	internal abstract class SymbolWriter : IDisposable
	{
		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06005A7A RID: 23162
		public abstract bool IsDeterministic { get; }

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06005A7B RID: 23163
		public abstract bool SupportsAsyncMethods { get; }

		// Token: 0x06005A7C RID: 23164
		public abstract void Initialize(Metadata metadata);

		// Token: 0x06005A7D RID: 23165
		public abstract void Close();

		// Token: 0x06005A7E RID: 23166
		public abstract bool GetDebugInfo(ChecksumAlgorithm pdbChecksumAlgorithm, ref uint pdbAge, out Guid guid, out uint stamp, out IMAGE_DEBUG_DIRECTORY pIDD, out byte[] codeViewData);

		// Token: 0x06005A7F RID: 23167
		public abstract void SetUserEntryPoint(MDToken entryMethod);

		// Token: 0x06005A80 RID: 23168
		public abstract ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x06005A81 RID: 23169
		public abstract void SetSourceServerData(byte[] data);

		// Token: 0x06005A82 RID: 23170
		public abstract void SetSourceLinkData(byte[] data);

		// Token: 0x06005A83 RID: 23171
		public abstract void OpenMethod(MDToken method);

		// Token: 0x06005A84 RID: 23172
		public abstract void CloseMethod();

		// Token: 0x06005A85 RID: 23173
		public abstract int OpenScope(int startOffset);

		// Token: 0x06005A86 RID: 23174
		public abstract void CloseScope(int endOffset);

		// Token: 0x06005A87 RID: 23175
		public abstract void SetSymAttribute(MDToken parent, string name, byte[] data);

		// Token: 0x06005A88 RID: 23176
		public abstract void UsingNamespace(string fullName);

		// Token: 0x06005A89 RID: 23177
		public abstract void DefineSequencePoints(ISymbolDocumentWriter document, uint arraySize, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x06005A8A RID: 23178
		public abstract void DefineLocalVariable(string name, uint attributes, uint sigToken, uint addrKind, uint addr1, uint addr2, uint addr3, uint startOffset, uint endOffset);

		// Token: 0x06005A8B RID: 23179
		public abstract void DefineConstant(string name, object value, uint sigToken);

		// Token: 0x06005A8C RID: 23180
		public abstract void DefineKickoffMethod(uint kickoffMethod);

		// Token: 0x06005A8D RID: 23181
		public abstract void DefineCatchHandlerILOffset(uint catchHandlerOffset);

		// Token: 0x06005A8E RID: 23182
		public abstract void DefineAsyncStepInfo(uint[] yieldOffsets, uint[] breakpointOffset, uint[] breakpointMethod);

		// Token: 0x06005A8F RID: 23183
		public abstract void Dispose();
	}
}
