using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.DotNet.Pdb.WindowsPdb;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097D RID: 2429
	internal sealed class SymbolReaderImpl : SymbolReader
	{
		// Token: 0x06005D8D RID: 23949 RVA: 0x001C1424 File Offset: 0x001C1424
		public SymbolReaderImpl(ISymUnmanagedReader reader, object[] objsToKeepAlive)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
			if (objsToKeepAlive == null)
			{
				throw new ArgumentNullException("objsToKeepAlive");
			}
			this.objsToKeepAlive = objsToKeepAlive;
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001C1460 File Offset: 0x001C1460
		~SymbolReaderImpl()
		{
			this.Dispose(false);
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x001C1490 File Offset: 0x001C1490
		public override PdbFileKind PdbFileKind
		{
			get
			{
				return PdbFileKind.WindowsPDB;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x001C1494 File Offset: 0x001C1494
		public override int UserEntryPoint
		{
			get
			{
				uint result;
				int userEntryPoint = this.reader.GetUserEntryPoint(out result);
				if (userEntryPoint == -2147467259)
				{
					result = 0U;
				}
				else
				{
					Marshal.ThrowExceptionForHR(userEntryPoint);
				}
				return (int)result;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x001C14CC File Offset: 0x001C14CC
		public override IList<SymbolDocument> Documents
		{
			get
			{
				if (this.documents == null)
				{
					uint num;
					this.reader.GetDocuments(0U, out num, null);
					ISymUnmanagedDocument[] array = new ISymUnmanagedDocument[num];
					this.reader.GetDocuments((uint)array.Length, out num, array);
					SymbolDocument[] array2 = new SymbolDocument[num];
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						array2[(int)num2] = new SymbolDocumentImpl(array[(int)num2]);
					}
					this.documents = array2;
				}
				return this.documents;
			}
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x001C154C File Offset: 0x001C154C
		public override void Initialize(ModuleDef module)
		{
			this.module = module;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x001C1558 File Offset: 0x001C1558
		public override SymbolMethod GetMethod(MethodDef method, int version)
		{
			ISymUnmanagedMethod symUnmanagedMethod;
			int methodByVersion = this.reader.GetMethodByVersion(method.MDToken.Raw, version, out symUnmanagedMethod);
			if (methodByVersion == -2147467259)
			{
				return null;
			}
			Marshal.ThrowExceptionForHR(methodByVersion);
			if (symUnmanagedMethod != null)
			{
				return new SymbolMethodImpl(this, symUnmanagedMethod);
			}
			return null;
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x001C15A8 File Offset: 0x001C15A8
		internal void GetCustomDebugInfos(SymbolMethodImpl symMethod, MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo = PseudoCustomDebugInfoFactory.TryCreateAsyncMethod(method.Module, method, body, symMethod.AsyncKickoffMethod, symMethod.AsyncStepInfos, symMethod.AsyncCatchHandlerILOffset);
			if (pdbAsyncMethodCustomDebugInfo != null)
			{
				result.Add(pdbAsyncMethodCustomDebugInfo);
			}
			uint num;
			this.reader.GetSymAttribute(method.MDToken.Raw, "MD2", 0U, out num, null);
			if (num == 0U)
			{
				return;
			}
			byte[] array = new byte[num];
			this.reader.GetSymAttribute(method.MDToken.Raw, "MD2", (uint)array.Length, out num, array);
			PdbCustomDebugInfoReader.Read(method, body, result, array);
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x001C1648 File Offset: 0x001C1648
		public override void GetCustomDebugInfos(int token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result)
		{
			if (token == 1)
			{
				this.GetCustomDebugInfos_ModuleDef(result);
			}
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x001C1658 File Offset: 0x001C1658
		private void GetCustomDebugInfos_ModuleDef(IList<PdbCustomDebugInfo> result)
		{
			byte[] sourceLinkData = this.GetSourceLinkData();
			if (sourceLinkData != null)
			{
				result.Add(new PdbSourceLinkCustomDebugInfo(sourceLinkData));
			}
			byte[] sourceServerData = this.GetSourceServerData();
			if (sourceServerData != null)
			{
				result.Add(new PdbSourceServerCustomDebugInfo(sourceServerData));
			}
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x001C169C File Offset: 0x001C169C
		private byte[] GetSourceLinkData()
		{
			ISymUnmanagedReader4 symUnmanagedReader = this.reader as ISymUnmanagedReader4;
			IntPtr source;
			int num;
			if (symUnmanagedReader == null || symUnmanagedReader.GetSourceServerData(out source, out num) != 0)
			{
				return null;
			}
			if (num == 0)
			{
				return Array2.Empty<byte>();
			}
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, array.Length);
			return array;
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x001C16F0 File Offset: 0x001C16F0
		private byte[] GetSourceServerData()
		{
			ISymUnmanagedSourceServerModule symUnmanagedSourceServerModule = this.reader as ISymUnmanagedSourceServerModule;
			if (symUnmanagedSourceServerModule != null)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					int num;
					if (symUnmanagedSourceServerModule.GetSourceServerData(out num, out zero) == 0)
					{
						if (num == 0)
						{
							return Array2.Empty<byte>();
						}
						byte[] array = new byte[num];
						Marshal.Copy(zero, array, 0, array.Length);
						return array;
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(zero);
					}
				}
			}
			return null;
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x001C1784 File Offset: 0x001C1784
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x001C1794 File Offset: 0x001C1794
		private void Dispose(bool disposing)
		{
			ISymUnmanagedDispose symUnmanagedDispose = this.reader as ISymUnmanagedDispose;
			if (symUnmanagedDispose != null)
			{
				symUnmanagedDispose.Destroy();
			}
			object[] array = this.objsToKeepAlive;
			if (array != null)
			{
				object[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					IDisposable disposable = array2[i] as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			this.module = null;
			this.reader = null;
			this.objsToKeepAlive = null;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x001C1810 File Offset: 0x001C1810
		public bool MatchesModule(Guid pdbId, uint stamp, uint age)
		{
			ISymUnmanagedReader4 symUnmanagedReader = this.reader as ISymUnmanagedReader4;
			bool flag;
			return symUnmanagedReader == null || (symUnmanagedReader.MatchesModule(pdbId, stamp, age, out flag) >= 0 && flag);
		}

		// Token: 0x04002D65 RID: 11621
		private ModuleDef module;

		// Token: 0x04002D66 RID: 11622
		private ISymUnmanagedReader reader;

		// Token: 0x04002D67 RID: 11623
		private object[] objsToKeepAlive;

		// Token: 0x04002D68 RID: 11624
		private const int E_FAIL = -2147467259;

		// Token: 0x04002D69 RID: 11625
		private volatile SymbolDocument[] documents;
	}
}
