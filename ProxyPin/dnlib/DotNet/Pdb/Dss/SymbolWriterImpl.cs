using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb.WindowsPdb;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000981 RID: 2433
	internal sealed class SymbolWriterImpl : SymbolWriter
	{
		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x001C2098 File Offset: 0x001C2098
		public override bool IsDeterministic
		{
			get
			{
				return this.isDeterministic;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x001C20A0 File Offset: 0x001C20A0
		public override bool SupportsAsyncMethods
		{
			get
			{
				return this.asyncMethodWriter != null;
			}
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x001C20B0 File Offset: 0x001C20B0
		public SymbolWriterImpl(ISymUnmanagedWriter2 writer, string pdbFileName, Stream pdbStream, PdbWriterOptions options, bool ownsStream)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.asyncMethodWriter = (writer as ISymUnmanagedAsyncMethodPropertiesWriter);
			if (pdbStream == null)
			{
				throw new ArgumentNullException("pdbStream");
			}
			this.pdbStream = pdbStream;
			this.pdbFileName = pdbFileName;
			this.ownsStream = ownsStream;
			this.isDeterministic = ((options & PdbWriterOptions.Deterministic) != PdbWriterOptions.None && writer is ISymUnmanagedWriter6);
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x001C2134 File Offset: 0x001C2134
		public override void Close()
		{
			if (this.closeCalled)
			{
				return;
			}
			this.closeCalled = true;
			this.writer.Close();
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x001C2154 File Offset: 0x001C2154
		public override void CloseMethod()
		{
			this.writer.CloseMethod();
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x001C2164 File Offset: 0x001C2164
		public override void CloseScope(int endOffset)
		{
			this.writer.CloseScope((uint)endOffset);
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x001C2174 File Offset: 0x001C2174
		public override void DefineAsyncStepInfo(uint[] yieldOffsets, uint[] breakpointOffset, uint[] breakpointMethod)
		{
			if (this.asyncMethodWriter == null)
			{
				throw new InvalidOperationException();
			}
			if (yieldOffsets.Length != breakpointOffset.Length || yieldOffsets.Length != breakpointMethod.Length)
			{
				throw new ArgumentException();
			}
			this.asyncMethodWriter.DefineAsyncStepInfo((uint)yieldOffsets.Length, yieldOffsets, breakpointOffset, breakpointMethod);
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x001C21B4 File Offset: 0x001C21B4
		public override void DefineCatchHandlerILOffset(uint catchHandlerOffset)
		{
			if (this.asyncMethodWriter == null)
			{
				throw new InvalidOperationException();
			}
			this.asyncMethodWriter.DefineCatchHandlerILOffset(catchHandlerOffset);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x001C21D4 File Offset: 0x001C21D4
		public override void DefineConstant(string name, object value, uint sigToken)
		{
			this.writer.DefineConstant2(name, value, sigToken);
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x001C21E4 File Offset: 0x001C21E4
		public override ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			ISymUnmanagedDocumentWriter symUnmanagedDocumentWriter;
			this.writer.DefineDocument(url, ref language, ref languageVendor, ref documentType, out symUnmanagedDocumentWriter);
			if (symUnmanagedDocumentWriter != null)
			{
				return new SymbolDocumentWriter(symUnmanagedDocumentWriter);
			}
			return null;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x001C2218 File Offset: 0x001C2218
		public override void DefineKickoffMethod(uint kickoffMethod)
		{
			if (this.asyncMethodWriter == null)
			{
				throw new InvalidOperationException();
			}
			this.asyncMethodWriter.DefineKickoffMethod(kickoffMethod);
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x001C2238 File Offset: 0x001C2238
		public override void DefineSequencePoints(ISymbolDocumentWriter document, uint arraySize, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
		{
			SymbolDocumentWriter symbolDocumentWriter = document as SymbolDocumentWriter;
			if (symbolDocumentWriter == null)
			{
				throw new ArgumentException("document isn't a non-null SymbolDocumentWriter instance");
			}
			this.writer.DefineSequencePoints(symbolDocumentWriter.SymUnmanagedDocumentWriter, arraySize, offsets, lines, columns, endLines, endColumns);
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x001C227C File Offset: 0x001C227C
		public override void OpenMethod(MDToken method)
		{
			this.writer.OpenMethod(method.Raw);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x001C2290 File Offset: 0x001C2290
		public override int OpenScope(int startOffset)
		{
			uint result;
			this.writer.OpenScope((uint)startOffset, out result);
			return (int)result;
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x001C22B0 File Offset: 0x001C22B0
		public override void SetSymAttribute(MDToken parent, string name, byte[] data)
		{
			this.writer.SetSymAttribute(parent.Raw, name, (uint)data.Length, data);
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x001C22CC File Offset: 0x001C22CC
		public override void SetUserEntryPoint(MDToken entryMethod)
		{
			this.writer.SetUserEntryPoint(entryMethod.Raw);
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x001C22E0 File Offset: 0x001C22E0
		public override void UsingNamespace(string fullName)
		{
			this.writer.UsingNamespace(fullName);
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x001C22F0 File Offset: 0x001C22F0
		public unsafe override bool GetDebugInfo(ChecksumAlgorithm pdbChecksumAlgorithm, ref uint pdbAge, out Guid guid, out uint stamp, out IMAGE_DEBUG_DIRECTORY pIDD, out byte[] codeViewData)
		{
			pIDD = default(IMAGE_DEBUG_DIRECTORY);
			codeViewData = null;
			if (this.isDeterministic)
			{
				((ISymUnmanagedWriter3)this.writer).Commit();
				long position = this.pdbStream.Position;
				this.pdbStream.Position = 0L;
				byte[] array = Hasher.Hash(pdbChecksumAlgorithm, this.pdbStream, this.pdbStream.Length);
				this.pdbStream.Position = position;
				ISymUnmanagedWriter8 symUnmanagedWriter = this.writer as ISymUnmanagedWriter8;
				if (symUnmanagedWriter != null)
				{
					RoslynContentIdProvider.GetContentId(array, out guid, out stamp);
					symUnmanagedWriter.UpdateSignature(guid, stamp, pdbAge);
					return true;
				}
				ISymUnmanagedWriter7 symUnmanagedWriter2 = this.writer as ISymUnmanagedWriter7;
				if (symUnmanagedWriter2 != null)
				{
					byte[] array2;
					byte* value;
					if ((array2 = array) == null || array2.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array2[0];
					}
					symUnmanagedWriter2.UpdateSignatureByHashingContent(new IntPtr((void*)value), (uint)array.Length);
					array2 = null;
				}
			}
			uint num;
			this.writer.GetDebugInfo(out pIDD, 0U, out num, null);
			codeViewData = new byte[num];
			this.writer.GetDebugInfo(out pIDD, num, out num, codeViewData);
			IPdbWriter pdbWriter = this.writer as IPdbWriter;
			if (pdbWriter != null)
			{
				byte[] array3 = new byte[16];
				Array.Copy(codeViewData, 4, array3, 0, 16);
				guid = new Guid(array3);
				uint num2;
				pdbWriter.GetSignatureAge(out stamp, out num2);
				pdbAge = num2;
				return true;
			}
			guid = default(Guid);
			stamp = 0U;
			return false;
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x001C2464 File Offset: 0x001C2464
		public override void DefineLocalVariable(string name, uint attributes, uint sigToken, uint addrKind, uint addr1, uint addr2, uint addr3, uint startOffset, uint endOffset)
		{
			this.writer.DefineLocalVariable2(name, attributes, sigToken, addrKind, addr1, addr2, addr3, startOffset, endOffset);
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x001C2490 File Offset: 0x001C2490
		public override void Initialize(Metadata metadata)
		{
			if (this.isDeterministic)
			{
				((ISymUnmanagedWriter6)this.writer).InitializeDeterministic(new MDEmitter(metadata), new StreamIStream(this.pdbStream));
				return;
			}
			this.writer.Initialize(new MDEmitter(metadata), this.pdbFileName, new StreamIStream(this.pdbStream), true);
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x001C24F4 File Offset: 0x001C24F4
		public unsafe override void SetSourceServerData(byte[] data)
		{
			if (data == null)
			{
				return;
			}
			ISymUnmanagedWriter8 symUnmanagedWriter = this.writer as ISymUnmanagedWriter8;
			if (symUnmanagedWriter != null)
			{
				fixed (byte[] array = data)
				{
					void* value;
					if (data == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = (void*)(&array[0]);
					}
					symUnmanagedWriter.SetSourceServerData(new IntPtr(value), (uint)data.Length);
				}
			}
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x001C2550 File Offset: 0x001C2550
		public unsafe override void SetSourceLinkData(byte[] data)
		{
			if (data == null)
			{
				return;
			}
			ISymUnmanagedWriter8 symUnmanagedWriter = this.writer as ISymUnmanagedWriter8;
			if (symUnmanagedWriter != null)
			{
				fixed (byte[] array = data)
				{
					void* value;
					if (data == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = (void*)(&array[0]);
					}
					symUnmanagedWriter.SetSourceLinkData(new IntPtr(value), (uint)data.Length);
				}
			}
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x001C25AC File Offset: 0x001C25AC
		public override void Dispose()
		{
			Marshal.FinalReleaseComObject(this.writer);
			if (this.ownsStream)
			{
				this.pdbStream.Dispose();
			}
		}

		// Token: 0x04002D76 RID: 11638
		private readonly ISymUnmanagedWriter2 writer;

		// Token: 0x04002D77 RID: 11639
		private readonly ISymUnmanagedAsyncMethodPropertiesWriter asyncMethodWriter;

		// Token: 0x04002D78 RID: 11640
		private readonly string pdbFileName;

		// Token: 0x04002D79 RID: 11641
		private readonly Stream pdbStream;

		// Token: 0x04002D7A RID: 11642
		private readonly bool ownsStream;

		// Token: 0x04002D7B RID: 11643
		private readonly bool isDeterministic;

		// Token: 0x04002D7C RID: 11644
		private bool closeCalled;
	}
}
