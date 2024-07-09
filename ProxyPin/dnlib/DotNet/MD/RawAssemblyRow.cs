using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BB RID: 2491
	[ComVisible(true)]
	public readonly struct RawAssemblyRow
	{
		// Token: 0x06005F8E RID: 24462 RVA: 0x001C92B0 File Offset: 0x001C92B0
		public RawAssemblyRow(uint HashAlgId, ushort MajorVersion, ushort MinorVersion, ushort BuildNumber, ushort RevisionNumber, uint Flags, uint PublicKey, uint Name, uint Locale)
		{
			this.HashAlgId = HashAlgId;
			this.MajorVersion = MajorVersion;
			this.MinorVersion = MinorVersion;
			this.BuildNumber = BuildNumber;
			this.RevisionNumber = RevisionNumber;
			this.Flags = Flags;
			this.PublicKey = PublicKey;
			this.Name = Name;
			this.Locale = Locale;
		}

		// Token: 0x170013FE RID: 5118
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.HashAlgId;
					break;
				case 1:
					result = (uint)this.MajorVersion;
					break;
				case 2:
					result = (uint)this.MinorVersion;
					break;
				case 3:
					result = (uint)this.BuildNumber;
					break;
				case 4:
					result = (uint)this.RevisionNumber;
					break;
				case 5:
					result = this.Flags;
					break;
				case 6:
					result = this.PublicKey;
					break;
				case 7:
					result = this.Name;
					break;
				case 8:
					result = this.Locale;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EA8 RID: 11944
		public readonly uint HashAlgId;

		// Token: 0x04002EA9 RID: 11945
		public readonly ushort MajorVersion;

		// Token: 0x04002EAA RID: 11946
		public readonly ushort MinorVersion;

		// Token: 0x04002EAB RID: 11947
		public readonly ushort BuildNumber;

		// Token: 0x04002EAC RID: 11948
		public readonly ushort RevisionNumber;

		// Token: 0x04002EAD RID: 11949
		public readonly uint Flags;

		// Token: 0x04002EAE RID: 11950
		public readonly uint PublicKey;

		// Token: 0x04002EAF RID: 11951
		public readonly uint Name;

		// Token: 0x04002EB0 RID: 11952
		public readonly uint Locale;
	}
}
