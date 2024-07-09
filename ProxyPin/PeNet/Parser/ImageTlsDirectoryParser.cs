using System;
using System.Collections.Generic;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet.Parser
{
	// Token: 0x02000BFD RID: 3069
	internal class ImageTlsDirectoryParser : SafeParser<IMAGE_TLS_DIRECTORY>
	{
		// Token: 0x06007AB4 RID: 31412 RVA: 0x00241A4C File Offset: 0x00241A4C
		internal ImageTlsDirectoryParser(byte[] buff, uint offset, bool is64Bit, IMAGE_SECTION_HEADER[] sectionHeaders) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
			this._sectionsHeaders = sectionHeaders;
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x00241A68 File Offset: 0x00241A68
		protected override IMAGE_TLS_DIRECTORY ParseTarget()
		{
			IMAGE_TLS_DIRECTORY image_TLS_DIRECTORY = new IMAGE_TLS_DIRECTORY(this._buff, this._offset, this._is64Bit);
			image_TLS_DIRECTORY.TlsCallbacks = this.ParseTlsCallbacks(image_TLS_DIRECTORY.AddressOfCallBacks);
			return image_TLS_DIRECTORY;
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x00241AA4 File Offset: 0x00241AA4
		private IMAGE_TLS_CALLBACK[] ParseTlsCallbacks(ulong addressOfCallBacks)
		{
			List<IMAGE_TLS_CALLBACK> list = new List<IMAGE_TLS_CALLBACK>();
			uint num = (uint)addressOfCallBacks.VAtoFileMapping(this._sectionsHeaders);
			uint num2 = 0U;
			for (;;)
			{
				if (this._is64Bit)
				{
					IMAGE_TLS_CALLBACK image_TLS_CALLBACK = new IMAGE_TLS_CALLBACK(this._buff, num + num2 * 8U, this._is64Bit);
					if (image_TLS_CALLBACK.Callback == 0UL)
					{
						break;
					}
					list.Add(image_TLS_CALLBACK);
					num2 += 1U;
				}
				else
				{
					IMAGE_TLS_CALLBACK image_TLS_CALLBACK2 = new IMAGE_TLS_CALLBACK(this._buff, num + num2 * 4U, this._is64Bit);
					if (image_TLS_CALLBACK2.Callback == 0UL)
					{
						break;
					}
					list.Add(image_TLS_CALLBACK2);
					num2 += 1U;
				}
			}
			return list.ToArray();
		}

		// Token: 0x04003B16 RID: 15126
		private readonly bool _is64Bit;

		// Token: 0x04003B17 RID: 15127
		private readonly IMAGE_SECTION_HEADER[] _sectionsHeaders;
	}
}
