using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB7 RID: 2999
	[ComVisible(true)]
	public class METADATAHDR : AbstractStructure
	{
		// Token: 0x0600792B RID: 31019 RVA: 0x0023E648 File Offset: 0x0023E648
		public METADATAHDR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x0600792C RID: 31020 RVA: 0x0023E654 File Offset: 0x0023E654
		// (set) Token: 0x0600792D RID: 31021 RVA: 0x0023E668 File Offset: 0x0023E668
		public uint Signature
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

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x0600792E RID: 31022 RVA: 0x0023E67C File Offset: 0x0023E67C
		// (set) Token: 0x0600792F RID: 31023 RVA: 0x0023E694 File Offset: 0x0023E694
		public ushort MajorVersion
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

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06007930 RID: 31024 RVA: 0x0023E6AC File Offset: 0x0023E6AC
		// (set) Token: 0x06007931 RID: 31025 RVA: 0x0023E6C4 File Offset: 0x0023E6C4
		public ushort MinorVersion
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

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06007932 RID: 31026 RVA: 0x0023E6DC File Offset: 0x0023E6DC
		// (set) Token: 0x06007933 RID: 31027 RVA: 0x0023E6F4 File Offset: 0x0023E6F4
		public uint Reserved
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 8U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 8U, value);
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06007934 RID: 31028 RVA: 0x0023E70C File Offset: 0x0023E70C
		// (set) Token: 0x06007935 RID: 31029 RVA: 0x0023E724 File Offset: 0x0023E724
		public uint VersionLength
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

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06007936 RID: 31030 RVA: 0x0023E73C File Offset: 0x0023E73C
		public string Version
		{
			get
			{
				if (!this._versionStringParsed)
				{
					this._versionStringParsed = true;
					try
					{
						this._versionString = this.ParseVersionString(this.Offset + 16U, this.VersionLength);
					}
					catch (Exception)
					{
						this._versionString = null;
					}
				}
				return this._versionString;
			}
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06007937 RID: 31031 RVA: 0x0023E7A0 File Offset: 0x0023E7A0
		// (set) Token: 0x06007938 RID: 31032 RVA: 0x0023E7C0 File Offset: 0x0023E7C0
		public ushort Flags
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.VersionLength + this.Offset + 16U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.VersionLength + this.Offset + 16U), value);
			}
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x0023E7E0 File Offset: 0x0023E7E0
		// (set) Token: 0x0600793A RID: 31034 RVA: 0x0023E800 File Offset: 0x0023E800
		public ushort Streams
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.VersionLength + this.Offset + 18U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.VersionLength + this.Offset + 18U), value);
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x0600793B RID: 31035 RVA: 0x0023E820 File Offset: 0x0023E820
		public METADATASTREAMHDR[] MetaDataStreamsHdrs
		{
			get
			{
				if (!this._metaDataStreamsHdrsParsed)
				{
					this._metaDataStreamsHdrsParsed = true;
					try
					{
						this._metaDataStreamsHdrs = this.ParseMetaDataStreamHdrs(this.VersionLength + this.Offset + 20U);
					}
					catch (Exception)
					{
						this._metaDataStreamsHdrs = null;
					}
				}
				return this._metaDataStreamsHdrs;
			}
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x0023E884 File Offset: 0x0023E884
		private METADATASTREAMHDR[] ParseMetaDataStreamHdrs(uint offset)
		{
			List<METADATASTREAMHDR> list = new List<METADATASTREAMHDR>();
			uint num = offset;
			for (int i = 0; i < (int)this.Streams; i++)
			{
				METADATASTREAMHDR metadatastreamhdr = new METADATASTREAMHDR(this.Buff, num);
				list.Add(metadatastreamhdr);
				num += metadatastreamhdr.HeaderLength;
			}
			return list.ToArray();
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x0023E8D4 File Offset: 0x0023E8D4
		private string ParseVersionString(uint offset, uint versionLength)
		{
			byte[] array = new byte[versionLength];
			Array.Copy(this.Buff, (long)((ulong)offset), array, 0L, (long)((ulong)versionLength));
			return Encoding.UTF8.GetString(array).Replace("\0", string.Empty);
		}

		// Token: 0x04003A5A RID: 14938
		private METADATASTREAMHDR[] _metaDataStreamsHdrs;

		// Token: 0x04003A5B RID: 14939
		private bool _metaDataStreamsHdrsParsed;

		// Token: 0x04003A5C RID: 14940
		private string _versionString;

		// Token: 0x04003A5D RID: 14941
		private bool _versionStringParsed;
	}
}
