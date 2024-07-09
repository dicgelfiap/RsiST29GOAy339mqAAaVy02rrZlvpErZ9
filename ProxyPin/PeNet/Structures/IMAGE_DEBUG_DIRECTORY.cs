using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA5 RID: 2981
	[ComVisible(true)]
	public class IMAGE_DEBUG_DIRECTORY : AbstractStructure
	{
		// Token: 0x060077D7 RID: 30679 RVA: 0x0023B9B0 File Offset: 0x0023B9B0
		public IMAGE_DEBUG_DIRECTORY(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x060077D8 RID: 30680 RVA: 0x0023B9BC File Offset: 0x0023B9BC
		// (set) Token: 0x060077D9 RID: 30681 RVA: 0x0023B9D0 File Offset: 0x0023B9D0
		public uint Characteristics
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset, value);
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x060077DA RID: 30682 RVA: 0x0023B9E4 File Offset: 0x0023B9E4
		// (set) Token: 0x060077DB RID: 30683 RVA: 0x0023B9FC File Offset: 0x0023B9FC
		public uint TimeDateStamp
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 4U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 4U, value);
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x060077DC RID: 30684 RVA: 0x0023BA14 File Offset: 0x0023BA14
		// (set) Token: 0x060077DD RID: 30685 RVA: 0x0023BA2C File Offset: 0x0023BA2C
		public ushort MajorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 8U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 8U), value);
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x060077DE RID: 30686 RVA: 0x0023BA44 File Offset: 0x0023BA44
		// (set) Token: 0x060077DF RID: 30687 RVA: 0x0023BA5C File Offset: 0x0023BA5C
		public ushort MinorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 10U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 10U), value);
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x060077E0 RID: 30688 RVA: 0x0023BA74 File Offset: 0x0023BA74
		// (set) Token: 0x060077E1 RID: 30689 RVA: 0x0023BA8C File Offset: 0x0023BA8C
		public uint Type
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 12U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 12U, value);
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x060077E2 RID: 30690 RVA: 0x0023BAA4 File Offset: 0x0023BAA4
		// (set) Token: 0x060077E3 RID: 30691 RVA: 0x0023BABC File Offset: 0x0023BABC
		public uint SizeOfData
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 16U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 16U, value);
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x0023BAD4 File Offset: 0x0023BAD4
		// (set) Token: 0x060077E5 RID: 30693 RVA: 0x0023BAEC File Offset: 0x0023BAEC
		public uint AddressOfRawData
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 20U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 20U, value);
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x0023BB04 File Offset: 0x0023BB04
		// (set) Token: 0x060077E7 RID: 30695 RVA: 0x0023BB1C File Offset: 0x0023BB1C
		public uint PointerToRawData
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 24U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 24U, value);
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x060077E8 RID: 30696 RVA: 0x0023BB34 File Offset: 0x0023BB34
		// (set) Token: 0x060077E9 RID: 30697 RVA: 0x0023BB70 File Offset: 0x0023BB70
		public Guid PdbSignature
		{
			get
			{
				byte[] array = new byte[16];
				Array.Copy(this.Buff, (long)((ulong)(this.PointerToRawData + 4U)), array, 0L, 16L);
				return new Guid(array);
			}
			set
			{
				Array.Copy(value.ToByteArray(), 0L, this.Buff, (long)((ulong)(this.PointerToRawData + 4U)), 16L);
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x060077EA RID: 30698 RVA: 0x0023BBA4 File Offset: 0x0023BBA4
		// (set) Token: 0x060077EB RID: 30699 RVA: 0x0023BBBC File Offset: 0x0023BBBC
		public uint PdbAge
		{
			get
			{
				return this.Buff.BytesToUInt32(this.PointerToRawData + 20U);
			}
			set
			{
				this.Buff.SetUInt32(this.PointerToRawData + 20U, value);
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x060077EC RID: 30700 RVA: 0x0023BBD4 File Offset: 0x0023BBD4
		public string PdbPath
		{
			get
			{
				byte[] bytes = this.Buff.Skip((int)(this.PointerToRawData + 24U)).TakeWhile((byte x) => x > 0).ToArray<byte>();
				return Encoding.UTF8.GetString(bytes);
			}
		}
	}
}
