using System;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet.Parser
{
	// Token: 0x02000BF0 RID: 3056
	internal class ExportedFunctionsParser : SafeParser<ExportFunction[]>
	{
		// Token: 0x06007A99 RID: 31385 RVA: 0x00241414 File Offset: 0x00241414
		internal ExportedFunctionsParser(byte[] buff, IMAGE_EXPORT_DIRECTORY exportDirectory, IMAGE_SECTION_HEADER[] sectionHeaders, IMAGE_DATA_DIRECTORY exportDataDir) : base(buff, 0U)
		{
			this._exportDirectory = exportDirectory;
			this._sectionHeaders = sectionHeaders;
			this._exportDataDir = exportDataDir;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x00241434 File Offset: 0x00241434
		protected override ExportFunction[] ParseTarget()
		{
			if (this._exportDirectory == null || this._exportDirectory.AddressOfFunctions == 0U)
			{
				return null;
			}
			ExportFunction[] array = new ExportFunction[this._exportDirectory.NumberOfFunctions];
			uint num = this._exportDirectory.AddressOfFunctions.RVAtoFileMapping(this._sectionHeaders);
			uint num2 = this._exportDirectory.AddressOfNameOrdinals.RVAtoFileMapping(this._sectionHeaders);
			uint num3 = this._exportDirectory.AddressOfNames.RVAtoFileMapping(this._sectionHeaders);
			uint num4 = 0U;
			while ((ulong)num4 < (ulong)((long)array.Length))
			{
				uint num5 = num4 + this._exportDirectory.Base;
				uint address = this._buff.BytesToUInt32(num + 4U * num4);
				array[(int)num4] = new ExportFunction(null, address, (ushort)num5);
				num4 += 1U;
			}
			for (uint num6 = 0U; num6 < this._exportDirectory.NumberOfNames; num6 += 1U)
			{
				uint num7 = this._buff.BytesToUInt32(num3 + 4U * num6).RVAtoFileMapping(this._sectionHeaders);
				string cstring = this._buff.GetCString((ulong)num7);
				uint ordinal = (uint)this._buff.GetOrdinal(num2 + 2U * num6);
				if (this.IsForwardedExport(array[(int)ordinal].Address))
				{
					uint num8 = array[(int)ordinal].Address.RVAtoFileMapping(this._sectionHeaders);
					string cstring2 = this._buff.GetCString((ulong)num8);
					array[(int)ordinal] = new ExportFunction(cstring, array[(int)ordinal].Address, array[(int)ordinal].Ordinal, cstring2);
				}
				else
				{
					array[(int)ordinal] = new ExportFunction(cstring, array[(int)ordinal].Address, array[(int)ordinal].Ordinal);
				}
			}
			return array;
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x00241604 File Offset: 0x00241604
		private bool IsForwardedExport(uint address)
		{
			return this._exportDataDir.VirtualAddress <= address && address < this._exportDataDir.VirtualAddress + this._exportDataDir.Size;
		}

		// Token: 0x04003B0D RID: 15117
		private readonly IMAGE_EXPORT_DIRECTORY _exportDirectory;

		// Token: 0x04003B0E RID: 15118
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;

		// Token: 0x04003B0F RID: 15119
		private readonly IMAGE_DATA_DIRECTORY _exportDataDir;
	}
}
