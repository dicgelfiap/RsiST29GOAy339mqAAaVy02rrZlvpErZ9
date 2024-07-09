using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BE RID: 2494
	[ComVisible(true)]
	public readonly struct RawAssemblyRefRow
	{
		// Token: 0x06005F94 RID: 24468 RVA: 0x001C9454 File Offset: 0x001C9454
		public RawAssemblyRefRow(ushort MajorVersion, ushort MinorVersion, ushort BuildNumber, ushort RevisionNumber, uint Flags, uint PublicKeyOrToken, uint Name, uint Locale, uint HashValue)
		{
			this.MajorVersion = MajorVersion;
			this.MinorVersion = MinorVersion;
			this.BuildNumber = BuildNumber;
			this.RevisionNumber = RevisionNumber;
			this.Flags = Flags;
			this.PublicKeyOrToken = PublicKeyOrToken;
			this.Name = Name;
			this.Locale = Locale;
			this.HashValue = HashValue;
		}

		// Token: 0x17001401 RID: 5121
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.MajorVersion;
					break;
				case 1:
					result = (uint)this.MinorVersion;
					break;
				case 2:
					result = (uint)this.BuildNumber;
					break;
				case 3:
					result = (uint)this.RevisionNumber;
					break;
				case 4:
					result = this.Flags;
					break;
				case 5:
					result = this.PublicKeyOrToken;
					break;
				case 6:
					result = this.Name;
					break;
				case 7:
					result = this.Locale;
					break;
				case 8:
					result = this.HashValue;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EB5 RID: 11957
		public readonly ushort MajorVersion;

		// Token: 0x04002EB6 RID: 11958
		public readonly ushort MinorVersion;

		// Token: 0x04002EB7 RID: 11959
		public readonly ushort BuildNumber;

		// Token: 0x04002EB8 RID: 11960
		public readonly ushort RevisionNumber;

		// Token: 0x04002EB9 RID: 11961
		public readonly uint Flags;

		// Token: 0x04002EBA RID: 11962
		public readonly uint PublicKeyOrToken;

		// Token: 0x04002EBB RID: 11963
		public readonly uint Name;

		// Token: 0x04002EBC RID: 11964
		public readonly uint Locale;

		// Token: 0x04002EBD RID: 11965
		public readonly uint HashValue;
	}
}
