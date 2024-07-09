using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace dnlib.IO
{
	// Token: 0x0200076F RID: 1903
	internal sealed class MemoryMappedDataReaderFactory : DataReaderFactory
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x00165DC4 File Offset: 0x00165DC4
		public override string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x00165DCC File Offset: 0x00165DCC
		public override uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060042B1 RID: 17073 RVA: 0x00165DD4 File Offset: 0x00165DD4
		// (remove) Token: 0x060042B2 RID: 17074 RVA: 0x00165E10 File Offset: 0x00165E10
		public override event EventHandler DataReaderInvalidated;

		// Token: 0x060042B3 RID: 17075 RVA: 0x00165E4C File Offset: 0x00165E4C
		private MemoryMappedDataReaderFactory(string filename)
		{
			this.osType = MemoryMappedDataReaderFactory.OSType.Unknown;
			this.filename = filename;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x00165E64 File Offset: 0x00165E64
		~MemoryMappedDataReaderFactory()
		{
			this.Dispose(false);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x00165E94 File Offset: 0x00165E94
		public override DataReader CreateReader(uint offset, uint length)
		{
			return base.CreateReader(this.stream, offset, length);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x00165EA4 File Offset: 0x00165EA4
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x00165EB4 File Offset: 0x00165EB4
		internal void SetLength(uint length)
		{
			this.length = length;
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x00165EC0 File Offset: 0x00165EC0
		internal static MemoryMappedDataReaderFactory CreateWindows(string filename, bool mapAsImage)
		{
			if (!MemoryMappedDataReaderFactory.canTryWindows)
			{
				return null;
			}
			MemoryMappedDataReaderFactory memoryMappedDataReaderFactory = new MemoryMappedDataReaderFactory(MemoryMappedDataReaderFactory.GetFullPath(filename));
			try
			{
				MemoryMappedDataReaderFactory.Windows.Mmap(memoryMappedDataReaderFactory, mapAsImage);
				return memoryMappedDataReaderFactory;
			}
			catch (EntryPointNotFoundException)
			{
			}
			catch (DllNotFoundException)
			{
			}
			MemoryMappedDataReaderFactory.canTryWindows = false;
			return null;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x00165F28 File Offset: 0x00165F28
		internal static MemoryMappedDataReaderFactory CreateUnix(string filename, bool mapAsImage)
		{
			if (!MemoryMappedDataReaderFactory.canTryUnix)
			{
				return null;
			}
			MemoryMappedDataReaderFactory memoryMappedDataReaderFactory = new MemoryMappedDataReaderFactory(MemoryMappedDataReaderFactory.GetFullPath(filename));
			try
			{
				MemoryMappedDataReaderFactory.Unix.Mmap(memoryMappedDataReaderFactory, mapAsImage);
				if (mapAsImage)
				{
					memoryMappedDataReaderFactory.Dispose();
					throw new ArgumentException("mapAsImage == true is not supported on this OS");
				}
				return memoryMappedDataReaderFactory;
			}
			catch (MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException)
			{
			}
			catch (EntryPointNotFoundException)
			{
			}
			catch (DllNotFoundException)
			{
			}
			MemoryMappedDataReaderFactory.canTryUnix = false;
			return null;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x00165FBC File Offset: 0x00165FBC
		private static string GetFullPath(string filename)
		{
			string result;
			try
			{
				result = Path.GetFullPath(filename);
			}
			catch
			{
				result = filename;
			}
			return result;
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x00165FF0 File Offset: 0x00165FF0
		private void Dispose(bool disposing)
		{
			this.FreeMemoryMappedIoData();
			if (disposing)
			{
				this.length = 0U;
				this.stream = EmptyDataStream.Instance;
				this.data = IntPtr.Zero;
				this.filename = null;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x00166024 File Offset: 0x00166024
		internal bool IsMemoryMappedIO
		{
			get
			{
				return this.dataAry == null;
			}
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x00166030 File Offset: 0x00166030
		internal unsafe void UnsafeDisableMemoryMappedIO()
		{
			if (this.dataAry != null)
			{
				return;
			}
			byte[] array = new byte[this.length];
			Marshal.Copy(this.data, array, 0, array.Length);
			this.FreeMemoryMappedIoData();
			this.length = (uint)array.Length;
			this.dataAry = array;
			this.gcHandle = GCHandle.Alloc(this.dataAry, GCHandleType.Pinned);
			this.data = this.gcHandle.AddrOfPinnedObject();
			this.stream = DataStreamFactory.Create((byte*)((void*)this.data));
			EventHandler dataReaderInvalidated = this.DataReaderInvalidated;
			if (dataReaderInvalidated == null)
			{
				return;
			}
			dataReaderInvalidated(this, EventArgs.Empty);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x001660D4 File Offset: 0x001660D4
		private void FreeMemoryMappedIoData()
		{
			if (this.dataAry == null)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this.data, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					this.length = 0U;
					MemoryMappedDataReaderFactory.OSType ostype = this.osType;
					if (ostype != MemoryMappedDataReaderFactory.OSType.Windows)
					{
						if (ostype != MemoryMappedDataReaderFactory.OSType.Unix)
						{
							throw new InvalidOperationException("Shouldn't be here");
						}
						MemoryMappedDataReaderFactory.Unix.Dispose(intPtr, this.origDataLength);
					}
					else
					{
						MemoryMappedDataReaderFactory.Windows.Dispose(intPtr);
					}
				}
			}
			if (this.gcHandle.IsAllocated)
			{
				try
				{
					this.gcHandle.Free();
				}
				catch (InvalidOperationException)
				{
				}
			}
			this.dataAry = null;
		}

		// Token: 0x04002391 RID: 9105
		private DataStream stream;

		// Token: 0x04002392 RID: 9106
		private uint length;

		// Token: 0x04002393 RID: 9107
		private string filename;

		// Token: 0x04002394 RID: 9108
		private GCHandle gcHandle;

		// Token: 0x04002395 RID: 9109
		private byte[] dataAry;

		// Token: 0x04002396 RID: 9110
		private IntPtr data;

		// Token: 0x04002397 RID: 9111
		private MemoryMappedDataReaderFactory.OSType osType;

		// Token: 0x04002398 RID: 9112
		private long origDataLength;

		// Token: 0x04002399 RID: 9113
		private static volatile bool canTryWindows = true;

		// Token: 0x0400239A RID: 9114
		private static volatile bool canTryUnix = true;

		// Token: 0x02000FC6 RID: 4038
		private enum OSType : byte
		{
			// Token: 0x04004300 RID: 17152
			Unknown,
			// Token: 0x04004301 RID: 17153
			Windows,
			// Token: 0x04004302 RID: 17154
			Unix
		}

		// Token: 0x02000FC7 RID: 4039
		[Serializable]
		private sealed class MemoryMappedIONotSupportedException : IOException
		{
			// Token: 0x06008DA5 RID: 36261 RVA: 0x002A7114 File Offset: 0x002A7114
			public MemoryMappedIONotSupportedException(string s) : base(s)
			{
			}

			// Token: 0x06008DA6 RID: 36262 RVA: 0x002A7120 File Offset: 0x002A7120
			public MemoryMappedIONotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}

		// Token: 0x02000FC8 RID: 4040
		private static class Windows
		{
			// Token: 0x06008DA7 RID: 36263
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

			// Token: 0x06008DA8 RID: 36264
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern SafeFileHandle CreateFileMapping(SafeFileHandle hFile, IntPtr lpAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

			// Token: 0x06008DA9 RID: 36265
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern IntPtr MapViewOfFile(SafeFileHandle hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumberOfBytesToMap);

			// Token: 0x06008DAA RID: 36266
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

			// Token: 0x06008DAB RID: 36267
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern uint GetFileSize(SafeFileHandle hFile, out uint lpFileSizeHigh);

			// Token: 0x06008DAC RID: 36268 RVA: 0x002A712C File Offset: 0x002A712C
			public unsafe static void Mmap(MemoryMappedDataReaderFactory creator, bool mapAsImage)
			{
				using (SafeFileHandle safeFileHandle = MemoryMappedDataReaderFactory.Windows.CreateFile(creator.filename, 2147483648U, 1U, IntPtr.Zero, 3U, 128U, IntPtr.Zero))
				{
					if (safeFileHandle.IsInvalid)
					{
						throw new IOException(string.Format("Could not open file {0} for reading. Error: {1:X8}", creator.filename, Marshal.GetLastWin32Error()));
					}
					uint num;
					uint fileSize = MemoryMappedDataReaderFactory.Windows.GetFileSize(safeFileHandle, out num);
					int lastWin32Error;
					if (fileSize == 4294967295U && (lastWin32Error = Marshal.GetLastWin32Error()) != 0)
					{
						throw new IOException(string.Format("Could not get file size. File: {0}, error: {1:X8}", creator.filename, lastWin32Error));
					}
					long num2 = (long)((ulong)num << 32 | (ulong)fileSize);
					using (SafeFileHandle safeFileHandle2 = MemoryMappedDataReaderFactory.Windows.CreateFileMapping(safeFileHandle, IntPtr.Zero, 2U | (mapAsImage ? 16777216U : 0U), 0U, 0U, null))
					{
						if (safeFileHandle2.IsInvalid)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not create a file mapping object. File: {0}, error: {1:X8}", creator.filename, Marshal.GetLastWin32Error()));
						}
						creator.data = MemoryMappedDataReaderFactory.Windows.MapViewOfFile(safeFileHandle2, 4U, 0U, 0U, UIntPtr.Zero);
						if (creator.data == IntPtr.Zero)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not map file {0}. Error: {1:X8}", creator.filename, Marshal.GetLastWin32Error()));
						}
						creator.length = (uint)num2;
						creator.osType = MemoryMappedDataReaderFactory.OSType.Windows;
						creator.stream = DataStreamFactory.Create((byte*)((void*)creator.data));
					}
				}
			}

			// Token: 0x06008DAD RID: 36269 RVA: 0x002A72DC File Offset: 0x002A72DC
			public static void Dispose(IntPtr addr)
			{
				if (addr != IntPtr.Zero)
				{
					MemoryMappedDataReaderFactory.Windows.UnmapViewOfFile(addr);
				}
			}

			// Token: 0x04004303 RID: 17155
			private const uint GENERIC_READ = 2147483648U;

			// Token: 0x04004304 RID: 17156
			private const uint FILE_SHARE_READ = 1U;

			// Token: 0x04004305 RID: 17157
			private const uint OPEN_EXISTING = 3U;

			// Token: 0x04004306 RID: 17158
			private const uint FILE_ATTRIBUTE_NORMAL = 128U;

			// Token: 0x04004307 RID: 17159
			private const uint PAGE_READONLY = 2U;

			// Token: 0x04004308 RID: 17160
			private const uint SEC_IMAGE = 16777216U;

			// Token: 0x04004309 RID: 17161
			private const uint SECTION_MAP_READ = 4U;

			// Token: 0x0400430A RID: 17162
			private const uint FILE_MAP_READ = 4U;

			// Token: 0x0400430B RID: 17163
			private const uint INVALID_FILE_SIZE = 4294967295U;

			// Token: 0x0400430C RID: 17164
			private const int NO_ERROR = 0;
		}

		// Token: 0x02000FC9 RID: 4041
		private static class Unix
		{
			// Token: 0x06008DAE RID: 36270
			[DllImport("libc")]
			private static extern int open(string pathname, int flags);

			// Token: 0x06008DAF RID: 36271
			[DllImport("libc")]
			private static extern int close(int fd);

			// Token: 0x06008DB0 RID: 36272
			[DllImport("libc", EntryPoint = "lseek", SetLastError = true)]
			private static extern int lseek32(int fd, int offset, int whence);

			// Token: 0x06008DB1 RID: 36273
			[DllImport("libc", EntryPoint = "lseek", SetLastError = true)]
			private static extern long lseek64(int fd, long offset, int whence);

			// Token: 0x06008DB2 RID: 36274
			[DllImport("libc", EntryPoint = "mmap", SetLastError = true)]
			private static extern IntPtr mmap32(IntPtr addr, IntPtr length, int prot, int flags, int fd, int offset);

			// Token: 0x06008DB3 RID: 36275
			[DllImport("libc", EntryPoint = "mmap", SetLastError = true)]
			private static extern IntPtr mmap64(IntPtr addr, IntPtr length, int prot, int flags, int fd, long offset);

			// Token: 0x06008DB4 RID: 36276
			[DllImport("libc")]
			private static extern int munmap(IntPtr addr, IntPtr length);

			// Token: 0x06008DB5 RID: 36277 RVA: 0x002A72F8 File Offset: 0x002A72F8
			public unsafe static void Mmap(MemoryMappedDataReaderFactory creator, bool mapAsImage)
			{
				int num = MemoryMappedDataReaderFactory.Unix.open(creator.filename, 0);
				try
				{
					if (num < 0)
					{
						throw new IOException(string.Format("Could not open file {0} for reading. Error: {1}", creator.filename, num));
					}
					long num2;
					IntPtr intPtr;
					if (IntPtr.Size == 4)
					{
						num2 = (long)MemoryMappedDataReaderFactory.Unix.lseek32(num, 0, 2);
						if (num2 == -1L)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not get length of {0} (lseek failed): {1}", creator.filename, Marshal.GetLastWin32Error()));
						}
						intPtr = MemoryMappedDataReaderFactory.Unix.mmap32(IntPtr.Zero, (IntPtr)num2, 1, 2, num, 0);
						if (intPtr == new IntPtr(-1) || intPtr == IntPtr.Zero)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not map file {0}. Error: {1}", creator.filename, Marshal.GetLastWin32Error()));
						}
					}
					else
					{
						num2 = MemoryMappedDataReaderFactory.Unix.lseek64(num, 0L, 2);
						if (num2 == -1L)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not get length of {0} (lseek failed): {1}", creator.filename, Marshal.GetLastWin32Error()));
						}
						intPtr = MemoryMappedDataReaderFactory.Unix.mmap64(IntPtr.Zero, (IntPtr)num2, 1, 2, num, 0L);
						if (intPtr == new IntPtr(-1) || intPtr == IntPtr.Zero)
						{
							throw new MemoryMappedDataReaderFactory.MemoryMappedIONotSupportedException(string.Format("Could not map file {0}. Error: {1}", creator.filename, Marshal.GetLastWin32Error()));
						}
					}
					creator.data = intPtr;
					creator.length = (uint)num2;
					creator.origDataLength = num2;
					creator.osType = MemoryMappedDataReaderFactory.OSType.Unix;
					creator.stream = DataStreamFactory.Create((byte*)((void*)creator.data));
				}
				finally
				{
					if (num >= 0)
					{
						MemoryMappedDataReaderFactory.Unix.close(num);
					}
				}
			}

			// Token: 0x06008DB6 RID: 36278 RVA: 0x002A74B4 File Offset: 0x002A74B4
			public static void Dispose(IntPtr addr, long size)
			{
				if (addr != IntPtr.Zero)
				{
					MemoryMappedDataReaderFactory.Unix.munmap(addr, new IntPtr(size));
				}
			}

			// Token: 0x0400430D RID: 17165
			private const int O_RDONLY = 0;

			// Token: 0x0400430E RID: 17166
			private const int SEEK_END = 2;

			// Token: 0x0400430F RID: 17167
			private const int PROT_READ = 1;

			// Token: 0x04004310 RID: 17168
			private const int MAP_PRIVATE = 2;
		}
	}
}
