using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF7 RID: 3063
	internal class ImageExportDirectoriesParser : SafeParser<IMAGE_EXPORT_DIRECTORY>
	{
		// Token: 0x06007AA8 RID: 31400 RVA: 0x0024179C File Offset: 0x0024179C
		public ImageExportDirectoriesParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x002417A8 File Offset: 0x002417A8
		protected override IMAGE_EXPORT_DIRECTORY ParseTarget()
		{
			return new IMAGE_EXPORT_DIRECTORY(this._buff, this._offset);
		}
	}
}
