using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000889 RID: 2185
	[ComVisible(true)]
	public sealed class VTable : IEnumerable<IMethod>, IEnumerable
	{
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x001979C8 File Offset: 0x001979C8
		// (set) Token: 0x060053AA RID: 21418 RVA: 0x001979D0 File Offset: 0x001979D0
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
			set
			{
				this.rva = value;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x001979DC File Offset: 0x001979DC
		// (set) Token: 0x060053AC RID: 21420 RVA: 0x001979E4 File Offset: 0x001979E4
		public VTableFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x001979F0 File Offset: 0x001979F0
		public bool Is32Bit
		{
			get
			{
				return (this.flags & VTableFlags.Bit32) > (VTableFlags)0;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x00197A00 File Offset: 0x00197A00
		public bool Is64Bit
		{
			get
			{
				return (this.flags & VTableFlags.Bit64) > (VTableFlags)0;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x00197A10 File Offset: 0x00197A10
		public IList<IMethod> Methods
		{
			get
			{
				return this.methods;
			}
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00197A18 File Offset: 0x00197A18
		public VTable()
		{
			this.methods = new List<IMethod>();
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00197A2C File Offset: 0x00197A2C
		public VTable(VTableFlags flags)
		{
			this.flags = flags;
			this.methods = new List<IMethod>();
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00197A48 File Offset: 0x00197A48
		public VTable(RVA rva, VTableFlags flags, int numSlots)
		{
			this.rva = rva;
			this.flags = flags;
			this.methods = new List<IMethod>(numSlots);
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00197A6C File Offset: 0x00197A6C
		public VTable(RVA rva, VTableFlags flags, IEnumerable<IMethod> methods)
		{
			this.rva = rva;
			this.flags = flags;
			this.methods = new List<IMethod>(methods);
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x00197A90 File Offset: 0x00197A90
		public IEnumerator<IMethod> GetEnumerator()
		{
			return this.methods.GetEnumerator();
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00197AA0 File Offset: 0x00197AA0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x00197AA8 File Offset: 0x00197AA8
		public override string ToString()
		{
			if (this.methods.Count == 0)
			{
				return string.Format("{0} {1:X8}", this.methods.Count, (uint)this.rva);
			}
			return string.Format("{0} {1:X8} {2}", this.methods.Count, (uint)this.rva, this.methods[0]);
		}

		// Token: 0x04002829 RID: 10281
		private RVA rva;

		// Token: 0x0400282A RID: 10282
		private VTableFlags flags;

		// Token: 0x0400282B RID: 10283
		private readonly IList<IMethod> methods;
	}
}
