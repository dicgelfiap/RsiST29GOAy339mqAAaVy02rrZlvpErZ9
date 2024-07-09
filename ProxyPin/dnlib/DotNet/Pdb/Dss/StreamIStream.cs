using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000978 RID: 2424
	internal sealed class StreamIStream : IStream
	{
		// Token: 0x06005D68 RID: 23912 RVA: 0x001C0B7C File Offset: 0x001C0B7C
		public StreamIStream(Stream stream) : this(stream, string.Empty)
		{
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001C0B8C File Offset: 0x001C0B8C
		public StreamIStream(Stream stream, string name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.name = (name ?? string.Empty);
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001C0BC0 File Offset: 0x001C0BC0
		public void Clone(out IStream ppstm)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
			throw new Exception();
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x001C0BD4 File Offset: 0x001C0BD4
		public void Commit(int grfCommitFlags)
		{
			this.stream.Flush();
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x001C0BE4 File Offset: 0x001C0BE4
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
			if (this.stream.Position + (long)num < (long)num || this.stream.Position + (long)num > this.stream.Length)
			{
				num = (int)(this.stream.Length - Math.Min(this.stream.Position, this.stream.Length));
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

		// Token: 0x06005D6D RID: 23917 RVA: 0x001C0CD4 File Offset: 0x001C0CD4
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x001C0CE0 File Offset: 0x001C0CE0
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			if (cb < 0)
			{
				cb = 0;
			}
			cb = this.stream.Read(pv, 0, cb);
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbRead, cb);
			}
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001C0D14 File Offset: 0x001C0D14
		public void Revert()
		{
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x001C0D18 File Offset: 0x001C0D18
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			switch (dwOrigin)
			{
			case 0:
				this.stream.Position = dlibMove;
				break;
			case 1:
				this.stream.Position += dlibMove;
				break;
			case 2:
				this.stream.Position = this.stream.Length + dlibMove;
				break;
			}
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, this.stream.Position);
			}
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x001C0DA4 File Offset: 0x001C0DA4
		public void SetSize(long libNewSize)
		{
			this.stream.SetLength(libNewSize);
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x001C0DB4 File Offset: 0x001C0DB4
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			System.Runtime.InteropServices.ComTypes.STATSTG statstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			statstg.cbSize = this.stream.Length;
			statstg.clsid = Guid.Empty;
			statstg.grfLocksSupported = 0;
			statstg.grfMode = 2;
			statstg.grfStateBits = 0;
			if ((grfStatFlag & 1) == 0)
			{
				statstg.pwcsName = this.name;
			}
			statstg.reserved = 0;
			statstg.type = 2;
			pstatstg = statstg;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x001C0E30 File Offset: 0x001C0E30
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			Marshal.ThrowExceptionForHR(-2147287039);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001C0E3C File Offset: 0x001C0E3C
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			this.stream.Write(pv, 0, cb);
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbWritten, cb);
			}
		}

		// Token: 0x04002D58 RID: 11608
		private readonly Stream stream;

		// Token: 0x04002D59 RID: 11609
		private readonly string name;

		// Token: 0x04002D5A RID: 11610
		private const int STG_E_INVALIDFUNCTION = -2147287039;

		// Token: 0x0200103B RID: 4155
		private enum STREAM_SEEK
		{
			// Token: 0x0400451F RID: 17695
			SET,
			// Token: 0x04004520 RID: 17696
			CUR,
			// Token: 0x04004521 RID: 17697
			END
		}

		// Token: 0x0200103C RID: 4156
		private enum STATFLAG
		{
			// Token: 0x04004523 RID: 17699
			DEFAULT,
			// Token: 0x04004524 RID: 17700
			NONAME,
			// Token: 0x04004525 RID: 17701
			NOOPEN
		}

		// Token: 0x0200103D RID: 4157
		private enum STGTY
		{
			// Token: 0x04004527 RID: 17703
			STORAGE = 1,
			// Token: 0x04004528 RID: 17704
			STREAM,
			// Token: 0x04004529 RID: 17705
			LOCKBYTES,
			// Token: 0x0400452A RID: 17706
			PROPERTY
		}
	}
}
