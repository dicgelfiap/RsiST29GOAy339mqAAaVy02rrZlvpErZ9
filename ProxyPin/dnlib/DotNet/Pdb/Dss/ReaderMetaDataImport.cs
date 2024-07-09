using System;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000977 RID: 2423
	internal sealed class ReaderMetaDataImport : MetaDataImport, IDisposable
	{
		// Token: 0x06005D61 RID: 23905 RVA: 0x001C0850 File Offset: 0x001C0850
		public unsafe ReaderMetaDataImport(Metadata metadata)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			this.metadata = metadata;
			DataReader dataReader = metadata.BlobStream.CreateReader();
			this.addrToFree = Marshal.AllocHGlobal((int)dataReader.BytesLeft);
			this.blobPtr = (byte*)((void*)this.addrToFree);
			if (this.blobPtr == null)
			{
				throw new OutOfMemoryException();
			}
			dataReader.ReadBytes((void*)this.blobPtr, (int)dataReader.BytesLeft);
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001C08D4 File Offset: 0x001C08D4
		~ReaderMetaDataImport()
		{
			this.Dispose(false);
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x001C0904 File Offset: 0x001C0904
		public unsafe override void GetTypeRefProps(uint tr, uint* ptkResolutionScope, ushort* szName, uint cchName, uint* pchName)
		{
			MDToken mdtoken = new MDToken(tr);
			if (mdtoken.Table != Table.TypeRef)
			{
				throw new ArgumentException();
			}
			RawTypeRefRow rawTypeRefRow;
			if (!this.metadata.TablesStream.TryReadTypeRefRow(mdtoken.Rid, out rawTypeRefRow))
			{
				throw new ArgumentException();
			}
			if (ptkResolutionScope != null)
			{
				*ptkResolutionScope = rawTypeRefRow.ResolutionScope;
			}
			if (szName != null || pchName != null)
			{
				UTF8String s = this.metadata.StringsStream.ReadNoNull(rawTypeRefRow.Namespace);
				UTF8String s2 = this.metadata.StringsStream.ReadNoNull(rawTypeRefRow.Name);
				base.CopyTypeName(s, s2, szName, cchName, pchName);
			}
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001C09B4 File Offset: 0x001C09B4
		public unsafe override void GetTypeDefProps(uint td, ushort* szTypeDef, uint cchTypeDef, uint* pchTypeDef, uint* pdwTypeDefFlags, uint* ptkExtends)
		{
			MDToken mdtoken = new MDToken(td);
			if (mdtoken.Table != Table.TypeDef)
			{
				throw new ArgumentException();
			}
			RawTypeDefRow rawTypeDefRow;
			if (!this.metadata.TablesStream.TryReadTypeDefRow(mdtoken.Rid, out rawTypeDefRow))
			{
				throw new ArgumentException();
			}
			if (pdwTypeDefFlags != null)
			{
				*pdwTypeDefFlags = rawTypeDefRow.Flags;
			}
			if (ptkExtends != null)
			{
				*ptkExtends = rawTypeDefRow.Extends;
			}
			if (szTypeDef != null || pchTypeDef != null)
			{
				UTF8String s = this.metadata.StringsStream.ReadNoNull(rawTypeDefRow.Namespace);
				UTF8String s2 = this.metadata.StringsStream.ReadNoNull(rawTypeDefRow.Name);
				base.CopyTypeName(s, s2, szTypeDef, cchTypeDef, pchTypeDef);
			}
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x001C0A78 File Offset: 0x001C0A78
		public unsafe override void GetSigFromToken(uint mdSig, byte** ppvSig, uint* pcbSig)
		{
			MDToken mdtoken = new MDToken(mdSig);
			if (mdtoken.Table != Table.StandAloneSig)
			{
				throw new ArgumentException();
			}
			RawStandAloneSigRow rawStandAloneSigRow;
			if (!this.metadata.TablesStream.TryReadStandAloneSigRow(mdtoken.Rid, out rawStandAloneSigRow))
			{
				throw new ArgumentException();
			}
			DataReader dataReader;
			if (!this.metadata.BlobStream.TryCreateReader(rawStandAloneSigRow.Signature, out dataReader))
			{
				throw new ArgumentException();
			}
			if (ppvSig != null)
			{
				*(IntPtr*)ppvSig = this.blobPtr + (dataReader.StartOffset - (uint)this.metadata.BlobStream.StartOffset);
			}
			if (pcbSig != null)
			{
				*pcbSig = dataReader.Length;
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x001C0B24 File Offset: 0x001C0B24
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x001C0B34 File Offset: 0x001C0B34
		private void Dispose(bool disposing)
		{
			this.metadata = null;
			IntPtr intPtr = Interlocked.Exchange(ref this.addrToFree, IntPtr.Zero);
			this.blobPtr = null;
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x04002D55 RID: 11605
		private Metadata metadata;

		// Token: 0x04002D56 RID: 11606
		private unsafe byte* blobPtr;

		// Token: 0x04002D57 RID: 11607
		private IntPtr addrToFree;
	}
}
