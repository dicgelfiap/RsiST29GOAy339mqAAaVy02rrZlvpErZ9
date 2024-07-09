using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC6 RID: 3014
	[ComVisible(true)]
	public class WIN_CERTIFICATE : AbstractStructure
	{
		// Token: 0x0600799D RID: 31133 RVA: 0x0023FC20 File Offset: 0x0023FC20
		public WIN_CERTIFICATE(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x0600799E RID: 31134 RVA: 0x0023FC2C File Offset: 0x0023FC2C
		// (set) Token: 0x0600799F RID: 31135 RVA: 0x0023FC40 File Offset: 0x0023FC40
		public uint dwLength
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

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x060079A0 RID: 31136 RVA: 0x0023FC54 File Offset: 0x0023FC54
		// (set) Token: 0x060079A1 RID: 31137 RVA: 0x0023FC6C File Offset: 0x0023FC6C
		public ushort wRevision
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 4U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 4U), value);
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x060079A2 RID: 31138 RVA: 0x0023FC84 File Offset: 0x0023FC84
		// (set) Token: 0x060079A3 RID: 31139 RVA: 0x0023FC9C File Offset: 0x0023FC9C
		public ushort wCertificateType
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 6U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 6U), value);
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x0023FCB4 File Offset: 0x0023FCB4
		// (set) Token: 0x060079A5 RID: 31141 RVA: 0x0023FCF4 File Offset: 0x0023FCF4
		[JsonIgnore]
		public byte[] bCertificate
		{
			get
			{
				byte[] array = new byte[this.dwLength - 8U];
				Array.Copy(this.Buff, (long)((ulong)(this.Offset + 8U)), array, 0L, (long)((ulong)(this.dwLength - 8U)));
				return array;
			}
			set
			{
				Array.Copy(value, 0L, this.Buff, (long)((ulong)(this.Offset + 8U)), (long)value.Length);
			}
		}
	}
}
