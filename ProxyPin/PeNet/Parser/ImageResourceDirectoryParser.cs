using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BFB RID: 3067
	internal class ImageResourceDirectoryParser : SafeParser<IMAGE_RESOURCE_DIRECTORY>
	{
		// Token: 0x06007AB0 RID: 31408 RVA: 0x002418A8 File Offset: 0x002418A8
		internal ImageResourceDirectoryParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x002418B4 File Offset: 0x002418B4
		protected override IMAGE_RESOURCE_DIRECTORY ParseTarget()
		{
			if (this._offset == 0U)
			{
				return null;
			}
			IMAGE_RESOURCE_DIRECTORY image_RESOURCE_DIRECTORY = new IMAGE_RESOURCE_DIRECTORY(this._buff, this._offset, this._offset);
			foreach (IMAGE_RESOURCE_DIRECTORY_ENTRY image_RESOURCE_DIRECTORY_ENTRY in image_RESOURCE_DIRECTORY.DirectoryEntries)
			{
				image_RESOURCE_DIRECTORY_ENTRY.ResourceDirectory = new IMAGE_RESOURCE_DIRECTORY(this._buff, this._offset + image_RESOURCE_DIRECTORY_ENTRY.OffsetToDirectory, this._offset);
				foreach (IMAGE_RESOURCE_DIRECTORY_ENTRY image_RESOURCE_DIRECTORY_ENTRY2 in image_RESOURCE_DIRECTORY_ENTRY.ResourceDirectory.DirectoryEntries)
				{
					image_RESOURCE_DIRECTORY_ENTRY2.ResourceDirectory = new IMAGE_RESOURCE_DIRECTORY(this._buff, this._offset + image_RESOURCE_DIRECTORY_ENTRY2.OffsetToDirectory, this._offset);
					foreach (IMAGE_RESOURCE_DIRECTORY_ENTRY image_RESOURCE_DIRECTORY_ENTRY3 in image_RESOURCE_DIRECTORY_ENTRY2.ResourceDirectory.DirectoryEntries)
					{
						image_RESOURCE_DIRECTORY_ENTRY3.ResourceDataEntry = new IMAGE_RESOURCE_DATA_ENTRY(this._buff, this._offset + image_RESOURCE_DIRECTORY_ENTRY3.OffsetToData);
					}
				}
			}
			return image_RESOURCE_DIRECTORY;
		}
	}
}
