using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D07 RID: 3335
	[ComVisible(true)]
	public class AcceleratorResource : Resource
	{
		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06008794 RID: 34708 RVA: 0x0028FBD0 File Offset: 0x0028FBD0
		// (set) Token: 0x06008795 RID: 34709 RVA: 0x0028FBD8 File Offset: 0x0028FBD8
		public List<Accelerator> Accelerators
		{
			get
			{
				return this._accelerators;
			}
			set
			{
				this._accelerators = value;
			}
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x0028FBE4 File Offset: 0x0028FBE4
		public AcceleratorResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_ACCELERATOR), null, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x0028FC10 File Offset: 0x0028FC10
		public AcceleratorResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x06008798 RID: 34712 RVA: 0x0028FC2C File Offset: 0x0028FC2C
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			long num = (long)(this._size / Marshal.SizeOf(typeof(User32.ACCEL)));
			for (long num2 = 0L; num2 < num; num2 += 1L)
			{
				Accelerator accelerator = new Accelerator();
				lpRes = accelerator.Read(lpRes);
				this._accelerators.Add(accelerator);
			}
			return lpRes;
		}

		// Token: 0x06008799 RID: 34713 RVA: 0x0028FC84 File Offset: 0x0028FC84
		internal override void Write(BinaryWriter w)
		{
			foreach (Accelerator accelerator in this._accelerators)
			{
				accelerator.Write(w);
			}
		}

		// Token: 0x0600879A RID: 34714 RVA: 0x0028FCDC File Offset: 0x0028FCDC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0} ACCELERATORS", base.Name));
			stringBuilder.AppendLine("BEGIN");
			foreach (Accelerator arg in this._accelerators)
			{
				stringBuilder.AppendLine(string.Format(" {0}", arg));
			}
			stringBuilder.AppendLine("END");
			return stringBuilder.ToString();
		}

		// Token: 0x04003E7D RID: 15997
		private List<Accelerator> _accelerators = new List<Accelerator>();
	}
}
