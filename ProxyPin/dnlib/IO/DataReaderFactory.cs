using System;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x02000765 RID: 1893
	[ComVisible(true)]
	public abstract class DataReaderFactory : IDisposable
	{
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004269 RID: 17001
		public abstract string Filename { get; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600426A RID: 17002
		public abstract uint Length { get; }

		// Token: 0x0600426B RID: 17003 RVA: 0x00165848 File Offset: 0x00165848
		public DataReader CreateReader()
		{
			return this.CreateReader(0U, this.Length);
		}

		// Token: 0x0600426C RID: 17004
		public abstract DataReader CreateReader(uint offset, uint length);

		// Token: 0x0600426D RID: 17005 RVA: 0x00165858 File Offset: 0x00165858
		private static void ThrowArgumentOutOfRangeException(string paramName)
		{
			throw new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x00165860 File Offset: 0x00165860
		private static void Throw_CreateReader_2(int offset, int length)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			throw new ArgumentOutOfRangeException("length");
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x00165880 File Offset: 0x00165880
		public DataReader CreateReader(uint offset, int length)
		{
			if (length < 0)
			{
				DataReaderFactory.ThrowArgumentOutOfRangeException("length");
			}
			return this.CreateReader(offset, (uint)length);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0016589C File Offset: 0x0016589C
		public DataReader CreateReader(int offset, uint length)
		{
			if (offset < 0)
			{
				DataReaderFactory.ThrowArgumentOutOfRangeException("offset");
			}
			return this.CreateReader((uint)offset, length);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x001658B8 File Offset: 0x001658B8
		public DataReader CreateReader(int offset, int length)
		{
			if (offset < 0 || length < 0)
			{
				DataReaderFactory.Throw_CreateReader_2(offset, length);
			}
			return this.CreateReader((uint)offset, (uint)length);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x001658D8 File Offset: 0x001658D8
		protected DataReader CreateReader(DataStream stream, uint offset, uint length)
		{
			uint length2 = this.Length;
			if (offset > length2)
			{
				offset = length2;
			}
			if ((ulong)offset + (ulong)length > (ulong)length2)
			{
				length = length2 - offset;
			}
			return new DataReader(stream, offset, length);
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06004273 RID: 17011 RVA: 0x00165914 File Offset: 0x00165914
		// (remove) Token: 0x06004274 RID: 17012 RVA: 0x00165918 File Offset: 0x00165918
		public virtual event EventHandler DataReaderInvalidated
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06004275 RID: 17013
		public abstract void Dispose();
	}
}
