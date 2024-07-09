using System;

namespace PeNet.Parser
{
	// Token: 0x02000C00 RID: 3072
	internal class MetaDataStreamBlobParser : SafeParser<byte[]>
	{
		// Token: 0x06007ABC RID: 31420 RVA: 0x00241D3C File Offset: 0x00241D3C
		public MetaDataStreamBlobParser(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x00241D50 File Offset: 0x00241D50
		protected override byte[] ParseTarget()
		{
			byte[] array = new byte[this._size];
			Array.Copy(this._buff, (long)((ulong)this._offset), array, 0L, (long)((ulong)this._size));
			return array;
		}

		// Token: 0x04003B1B RID: 15131
		private readonly uint _size;
	}
}
