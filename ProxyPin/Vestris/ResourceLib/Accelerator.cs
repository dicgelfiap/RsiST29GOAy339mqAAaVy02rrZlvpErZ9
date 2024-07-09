using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D06 RID: 3334
	[ComVisible(true)]
	public class Accelerator
	{
		// Token: 0x0600878D RID: 34701 RVA: 0x0028FA90 File Offset: 0x0028FA90
		internal IntPtr Read(IntPtr lpRes)
		{
			this._accel = (User32.ACCEL)Marshal.PtrToStructure(lpRes, typeof(User32.ACCEL));
			return new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._accel));
		}

		// Token: 0x0600878E RID: 34702 RVA: 0x0028FACC File Offset: 0x0028FACC
		internal void Write(BinaryWriter w)
		{
			w.Write(this._accel.fVirt);
			w.Write(this._accel.key);
			w.Write(this._accel.cmd);
			ResourceUtil.PadToWORD(w);
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x0600878F RID: 34703 RVA: 0x0028FB08 File Offset: 0x0028FB08
		public string Key
		{
			get
			{
				string name = Enum.GetName(typeof(User32.VirtualKeys), this._accel.key);
				if (!string.IsNullOrEmpty(name))
				{
					return name;
				}
				char key = (char)this._accel.key;
				return key.ToString();
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06008790 RID: 34704 RVA: 0x0028FB5C File Offset: 0x0028FB5C
		// (set) Token: 0x06008791 RID: 34705 RVA: 0x0028FB6C File Offset: 0x0028FB6C
		public uint Command
		{
			get
			{
				return this._accel.cmd;
			}
			set
			{
				this._accel.cmd = value;
			}
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x0028FB7C File Offset: 0x0028FB7C
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.Key, this.Command, ResourceUtil.FlagsToString<User32.AcceleratorVirtualKey>((uint)this._accel.fVirt).Replace(" |", ","));
		}

		// Token: 0x04003E7C RID: 15996
		private User32.ACCEL _accel;
	}
}
