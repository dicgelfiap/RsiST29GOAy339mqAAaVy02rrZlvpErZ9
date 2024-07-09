using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA3 RID: 2979
	[ComVisible(true)]
	public class IMAGE_COR20_HEADER : AbstractStructure
	{
		// Token: 0x060077BD RID: 30653 RVA: 0x0023B68C File Offset: 0x0023B68C
		public IMAGE_COR20_HEADER(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x060077BE RID: 30654 RVA: 0x0023B698 File Offset: 0x0023B698
		// (set) Token: 0x060077BF RID: 30655 RVA: 0x0023B6AC File Offset: 0x0023B6AC
		public uint cb
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

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x060077C0 RID: 30656 RVA: 0x0023B6C0 File Offset: 0x0023B6C0
		// (set) Token: 0x060077C1 RID: 30657 RVA: 0x0023B6D8 File Offset: 0x0023B6D8
		public ushort MajorRuntimeVersion
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

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x060077C2 RID: 30658 RVA: 0x0023B6F0 File Offset: 0x0023B6F0
		// (set) Token: 0x060077C3 RID: 30659 RVA: 0x0023B708 File Offset: 0x0023B708
		public ushort MinorRuntimeVersion
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

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x060077C4 RID: 30660 RVA: 0x0023B720 File Offset: 0x0023B720
		public IMAGE_DATA_DIRECTORY MetaData
		{
			get
			{
				if (this._metaData != null)
				{
					return this._metaData;
				}
				this._metaData = this.SetImageDataDirectory(this.Buff, this.Offset + 8U);
				return this._metaData;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x060077C5 RID: 30661 RVA: 0x0023B754 File Offset: 0x0023B754
		// (set) Token: 0x060077C6 RID: 30662 RVA: 0x0023B76C File Offset: 0x0023B76C
		public uint Flags
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

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x060077C7 RID: 30663 RVA: 0x0023B784 File Offset: 0x0023B784
		// (set) Token: 0x060077C8 RID: 30664 RVA: 0x0023B79C File Offset: 0x0023B79C
		public uint EntryPointToken
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

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x060077C9 RID: 30665 RVA: 0x0023B7B4 File Offset: 0x0023B7B4
		// (set) Token: 0x060077CA RID: 30666 RVA: 0x0023B7BC File Offset: 0x0023B7BC
		public uint EntryPointRVA
		{
			get
			{
				return this.EntryPointToken;
			}
			set
			{
				this.EntryPointToken = value;
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x0023B7C8 File Offset: 0x0023B7C8
		public IMAGE_DATA_DIRECTORY Resources
		{
			get
			{
				if (this._resources != null)
				{
					return this._resources;
				}
				this._resources = this.SetImageDataDirectory(this.Buff, this.Offset + 24U);
				return this._resources;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x0023B800 File Offset: 0x0023B800
		public IMAGE_DATA_DIRECTORY StrongNameSignature
		{
			get
			{
				if (this._strongSignatureNames != null)
				{
					return this._strongSignatureNames;
				}
				this._strongSignatureNames = this.SetImageDataDirectory(this.Buff, this.Offset + 32U);
				return this._strongSignatureNames;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x060077CD RID: 30669 RVA: 0x0023B838 File Offset: 0x0023B838
		public IMAGE_DATA_DIRECTORY CodeManagerTable
		{
			get
			{
				if (this._codeManagerTable != null)
				{
					return this._codeManagerTable;
				}
				this._codeManagerTable = this.SetImageDataDirectory(this.Buff, this.Offset + 40U);
				return this._codeManagerTable;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x060077CE RID: 30670 RVA: 0x0023B870 File Offset: 0x0023B870
		public IMAGE_DATA_DIRECTORY VTableFixups
		{
			get
			{
				if (this._vTableFixups != null)
				{
					return this._vTableFixups;
				}
				this._vTableFixups = this.SetImageDataDirectory(this.Buff, this.Offset + 48U);
				return this._vTableFixups;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x060077CF RID: 30671 RVA: 0x0023B8A8 File Offset: 0x0023B8A8
		public IMAGE_DATA_DIRECTORY ExportAddressTableJumps
		{
			get
			{
				if (this._exportAddressTableJumps != null)
				{
					return this._exportAddressTableJumps;
				}
				this._exportAddressTableJumps = this.SetImageDataDirectory(this.Buff, this.Offset + 56U);
				return this._exportAddressTableJumps;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x060077D0 RID: 30672 RVA: 0x0023B8E0 File Offset: 0x0023B8E0
		public IMAGE_DATA_DIRECTORY ManagedNativeHeader
		{
			get
			{
				if (this._managedNativeHeader != null)
				{
					return this._managedNativeHeader;
				}
				this._managedNativeHeader = this.SetImageDataDirectory(this.Buff, this.Offset + 64U);
				return this._managedNativeHeader;
			}
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x0023B918 File Offset: 0x0023B918
		private IMAGE_DATA_DIRECTORY SetImageDataDirectory(byte[] buff, uint offset)
		{
			IMAGE_DATA_DIRECTORY result;
			try
			{
				result = new IMAGE_DATA_DIRECTORY(buff, offset);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04003A45 RID: 14917
		private IMAGE_DATA_DIRECTORY _metaData;

		// Token: 0x04003A46 RID: 14918
		private IMAGE_DATA_DIRECTORY _resources;

		// Token: 0x04003A47 RID: 14919
		private IMAGE_DATA_DIRECTORY _strongSignatureNames;

		// Token: 0x04003A48 RID: 14920
		private IMAGE_DATA_DIRECTORY _codeManagerTable;

		// Token: 0x04003A49 RID: 14921
		private IMAGE_DATA_DIRECTORY _vTableFixups;

		// Token: 0x04003A4A RID: 14922
		private IMAGE_DATA_DIRECTORY _exportAddressTableJumps;

		// Token: 0x04003A4B RID: 14923
		private IMAGE_DATA_DIRECTORY _managedNativeHeader;
	}
}
