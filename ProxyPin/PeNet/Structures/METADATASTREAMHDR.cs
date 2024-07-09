using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB8 RID: 3000
	[ComVisible(true)]
	public class METADATASTREAMHDR : AbstractStructure
	{
		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x0600793E RID: 31038 RVA: 0x0023E918 File Offset: 0x0023E918
		internal uint HeaderLength
		{
			get
			{
				return this.GetHeaderLength();
			}
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x0023E920 File Offset: 0x0023E920
		public METADATASTREAMHDR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06007940 RID: 31040 RVA: 0x0023E92C File Offset: 0x0023E92C
		// (set) Token: 0x06007941 RID: 31041 RVA: 0x0023E940 File Offset: 0x0023E940
		public uint offset
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

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06007942 RID: 31042 RVA: 0x0023E954 File Offset: 0x0023E954
		// (set) Token: 0x06007943 RID: 31043 RVA: 0x0023E96C File Offset: 0x0023E96C
		public uint size
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

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06007944 RID: 31044 RVA: 0x0023E984 File Offset: 0x0023E984
		public string streamName
		{
			get
			{
				return this.ParseStreamName(this.Offset + 8U);
			}
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x0023E994 File Offset: 0x0023E994
		private uint GetHeaderLength()
		{
			int num = 100;
			int headerLength = 0;
			for (int i = 8; i < num; i++)
			{
				if (this.Buff[(int)(checked((IntPtr)(unchecked((ulong)this.Offset + (ulong)((long)i)))))] == 0)
				{
					headerLength = i;
					break;
				}
			}
			return (uint)this.AddHeaderPaddingLength(headerLength);
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x0023E9E0 File Offset: 0x0023E9E0
		private int AddHeaderPaddingLength(int headerLength)
		{
			if (headerLength % 4 == 0)
			{
				return headerLength + 4;
			}
			return headerLength + (4 - headerLength % 4);
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x0023E9F8 File Offset: 0x0023E9F8
		private string ParseStreamName(uint nameOffset)
		{
			return this.Buff.GetCString((ulong)nameOffset);
		}
	}
}
