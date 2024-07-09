using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200098D RID: 2445
	[ComVisible(true)]
	public sealed class GuidStream : HeapStream
	{
		// Token: 0x06005E2A RID: 24106 RVA: 0x001C54B8 File Offset: 0x001C54B8
		public GuidStream()
		{
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x001C54C0 File Offset: 0x001C54C0
		public GuidStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x001C54CC File Offset: 0x001C54CC
		public override bool IsValidIndex(uint index)
		{
			return index == 0U || (index <= 268435456U && base.IsValidOffset((index - 1U) * 16U, 16));
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x001C54F4 File Offset: 0x001C54F4
		public Guid? Read(uint index)
		{
			if (index == 0U || !this.IsValidIndex(index))
			{
				return null;
			}
			DataReader dataReader = this.dataReader;
			dataReader.Position = (index - 1U) * 16U;
			return new Guid?(dataReader.ReadGuid());
		}
	}
}
