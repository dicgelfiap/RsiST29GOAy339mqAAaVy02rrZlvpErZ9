using System;
using System.Collections.Generic;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet.Parser
{
	// Token: 0x02000BFE RID: 3070
	internal class ImportedFunctionsParser : SafeParser<ImportFunction[]>
	{
		// Token: 0x06007AB7 RID: 31415 RVA: 0x00241B40 File Offset: 0x00241B40
		internal ImportedFunctionsParser(byte[] buff, IMAGE_IMPORT_DESCRIPTOR[] importDescriptors, IMAGE_SECTION_HEADER[] sectionHeaders, bool is64Bit) : base(buff, 0U)
		{
			this._importDescriptors = importDescriptors;
			this._sectionHeaders = sectionHeaders;
			this._is64Bit = is64Bit;
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x00241B60 File Offset: 0x00241B60
		protected override ImportFunction[] ParseTarget()
		{
			if (this._importDescriptors == null)
			{
				return null;
			}
			List<ImportFunction> list = new List<ImportFunction>();
			uint num = this._is64Bit ? 8U : 4U;
			ulong num2 = this._is64Bit ? 9223372036854775808UL : ((ulong)int.MinValue);
			ulong num3 = this._is64Bit ? 9223372036854775807UL : 2147483647UL;
			foreach (IMAGE_IMPORT_DESCRIPTOR image_IMPORT_DESCRIPTOR in this._importDescriptors)
			{
				uint num4 = image_IMPORT_DESCRIPTOR.Name.RVAtoFileMapping(this._sectionHeaders);
				string cstring = this._buff.GetCString((ulong)num4);
				if (!this.IsModuleNameTooLong(cstring))
				{
					uint num5 = (image_IMPORT_DESCRIPTOR.OriginalFirstThunk != 0U) ? image_IMPORT_DESCRIPTOR.OriginalFirstThunk : image_IMPORT_DESCRIPTOR.FirstThunk;
					if (num5 != 0U)
					{
						uint num6 = num5.RVAtoFileMapping(this._sectionHeaders);
						uint num7 = 0U;
						for (;;)
						{
							IMAGE_THUNK_DATA image_THUNK_DATA = new IMAGE_THUNK_DATA(this._buff, num6 + num7 * num, this._is64Bit);
							if (image_THUNK_DATA.AddressOfData == 0UL)
							{
								break;
							}
							if ((image_THUNK_DATA.Ordinal & num2) == num2)
							{
								list.Add(new ImportFunction(null, cstring, (ushort)(image_THUNK_DATA.Ordinal & num3)));
							}
							else
							{
								IMAGE_IMPORT_BY_NAME image_IMPORT_BY_NAME = new IMAGE_IMPORT_BY_NAME(this._buff, ((uint)image_THUNK_DATA.AddressOfData).RVAtoFileMapping(this._sectionHeaders));
								list.Add(new ImportFunction(image_IMPORT_BY_NAME.Name, cstring, image_IMPORT_BY_NAME.Hint));
							}
							num7 += 1U;
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x00241D0C File Offset: 0x00241D0C
		private bool IsModuleNameTooLong(string dllName)
		{
			return dllName.Length > 256;
		}

		// Token: 0x04003B18 RID: 15128
		private readonly IMAGE_IMPORT_DESCRIPTOR[] _importDescriptors;

		// Token: 0x04003B19 RID: 15129
		private readonly bool _is64Bit;

		// Token: 0x04003B1A RID: 15130
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;
	}
}
