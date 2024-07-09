using System;
using System.Runtime.InteropServices;

namespace PeNet
{
	// Token: 0x02000B8E RID: 2958
	[ComVisible(true)]
	public class ExportFunction
	{
		// Token: 0x060076EC RID: 30444 RVA: 0x00239000 File Offset: 0x00239000
		public ExportFunction(string name, uint address, ushort ordinal)
		{
			this.Name = name;
			this.Address = address;
			this.Ordinal = ordinal;
		}

		// Token: 0x060076ED RID: 30445 RVA: 0x00239020 File Offset: 0x00239020
		public ExportFunction(string name, uint address, ushort ordinal, string forwardName)
		{
			this.Name = name;
			this.Address = address;
			this.Ordinal = ordinal;
			this.ForwardName = forwardName;
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060076EE RID: 30446 RVA: 0x00239048 File Offset: 0x00239048
		// (set) Token: 0x060076EF RID: 30447 RVA: 0x00239050 File Offset: 0x00239050
		public string Name { get; private set; }

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060076F0 RID: 30448 RVA: 0x0023905C File Offset: 0x0023905C
		public uint Address { get; }

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060076F1 RID: 30449 RVA: 0x00239064 File Offset: 0x00239064
		public ushort Ordinal { get; }

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060076F2 RID: 30450 RVA: 0x0023906C File Offset: 0x0023906C
		public string ForwardName { get; }

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x060076F3 RID: 30451 RVA: 0x00239074 File Offset: 0x00239074
		public bool HasName
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x060076F4 RID: 30452 RVA: 0x00239084 File Offset: 0x00239084
		public bool HasOrdinal
		{
			get
			{
				return this.Ordinal > 0;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x060076F5 RID: 30453 RVA: 0x00239090 File Offset: 0x00239090
		public bool HasForward
		{
			get
			{
				return !string.IsNullOrEmpty(this.ForwardName);
			}
		}
	}
}
