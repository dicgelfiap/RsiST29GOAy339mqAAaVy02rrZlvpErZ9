using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000974 RID: 2420
	internal sealed class DataReaderIStream : IStream, IDisposable
	{
		// Token: 0x06005CDE RID: 23774 RVA: 0x001BFD80 File Offset: 0x001BFD80
		public DataReaderIStream(DataReaderFactory dataReaderFactory) : this(dataReaderFactory, dataReaderFactory.CreateReader(), string.Empty)
		{
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x001BFD94 File Offset: 0x001BFD94
		private DataReaderIStream(DataReaderFactory dataReaderFactory, DataReader reader, string name)
		{
			if (dataReaderFactory == null)
			{
				throw new ArgumentNullException("dataReaderFactory");
			}
			this.dataReaderFactory = dataReaderFactory;
			this.reader = reader;
			this.name = (name ?? string.Empty);
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x001BFDD0 File Offset: 0x001BFDD0
		public void Clone(out IStream ppstm)
		{
			ppstm = new DataReaderIStream(this.dataReaderFactory, this.reader, this.name);
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x001BFDEC File Offset: 0x001BFDEC
		public void Commit(int grfCommitFlags)
		{
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x001BFDF0 File Offset: 0x001BFDF0
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			if (cb > 2147483647L)
			{
				cb = 2147483647L;
			}
			else if (cb < 0L)
			{
				cb = 0L;
			}
			int num = (int)cb;
			if ((ulong)this.reader.Position + (ulong)num > (ulong)this.reader.Length)
			{
				num = (int)(this.reader.Length - Math.Min(this.reader.Position, this.reader.Length));
			}
			byte[] array = new byte[num];
			this.Read(array, num, pcbRead);
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbRead, (long)Marshal.ReadInt32(pcbRead));
			}
			pstm.Write(array, array.Length, pcbWritten);
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbWritten, (long)Marshal.ReadInt32(pcbWritten));
			}
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x001BFECC File Offset: 0x001BFECC
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x001BFED8 File Offset: 0x001BFED8
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			if (cb < 0)
			{
				cb = 0;
			}
			cb = (int)Math.Min(this.reader.BytesLeft, (uint)cb);
			this.reader.ReadBytes(pv, 0, cb);
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbRead, cb);
			}
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x001BFF2C File Offset: 0x001BFF2C
		public void Revert()
		{
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x001BFF30 File Offset: 0x001BFF30
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			switch (dwOrigin)
			{
			case 0:
				this.reader.Position = (uint)dlibMove;
				break;
			case 1:
				this.reader.Position = (uint)((ulong)this.reader.Position + (ulong)dlibMove);
				break;
			case 2:
				this.reader.Position = (uint)((ulong)this.reader.Length + (ulong)dlibMove);
				break;
			}
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, (long)((ulong)this.reader.Position));
			}
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x001BFFC8 File Offset: 0x001BFFC8
		public void SetSize(long libNewSize)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x001BFFD4 File Offset: 0x001BFFD4
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			System.Runtime.InteropServices.ComTypes.STATSTG statstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			statstg.cbSize = (long)((ulong)this.reader.Length);
			statstg.clsid = Guid.Empty;
			statstg.grfLocksSupported = 0;
			statstg.grfMode = 0;
			statstg.grfStateBits = 0;
			if ((grfStatFlag & 1) == 0)
			{
				statstg.pwcsName = this.name;
			}
			statstg.reserved = 0;
			statstg.type = 2;
			pstatstg = statstg;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x001C0050 File Offset: 0x001C0050
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001C005C File Offset: 0x001C005C
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			Marshal.ThrowExceptionForHR(-2147286781);
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001C0068 File Offset: 0x001C0068
		public void Dispose()
		{
		}

		// Token: 0x04002D4D RID: 11597
		private readonly DataReaderFactory dataReaderFactory;

		// Token: 0x04002D4E RID: 11598
		private DataReader reader;

		// Token: 0x04002D4F RID: 11599
		private readonly string name;

		// Token: 0x04002D50 RID: 11600
		private const int STG_E_INVALIDFUNCTION = -2147287039;

		// Token: 0x04002D51 RID: 11601
		private const int STG_E_CANTSAVE = -2147286781;

		// Token: 0x02001038 RID: 4152
		private enum STREAM_SEEK
		{
			// Token: 0x04004512 RID: 17682
			SET,
			// Token: 0x04004513 RID: 17683
			CUR,
			// Token: 0x04004514 RID: 17684
			END
		}

		// Token: 0x02001039 RID: 4153
		private enum STATFLAG
		{
			// Token: 0x04004516 RID: 17686
			DEFAULT,
			// Token: 0x04004517 RID: 17687
			NONAME,
			// Token: 0x04004518 RID: 17688
			NOOPEN
		}

		// Token: 0x0200103A RID: 4154
		private enum STGTY
		{
			// Token: 0x0400451A RID: 17690
			STORAGE = 1,
			// Token: 0x0400451B RID: 17691
			STREAM,
			// Token: 0x0400451C RID: 17692
			LOCKBYTES,
			// Token: 0x0400451D RID: 17693
			PROPERTY
		}
	}
}
