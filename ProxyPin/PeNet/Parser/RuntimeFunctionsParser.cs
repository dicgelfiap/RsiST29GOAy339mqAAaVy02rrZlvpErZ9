using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C05 RID: 3077
	internal class RuntimeFunctionsParser : SafeParser<RUNTIME_FUNCTION[]>
	{
		// Token: 0x06007AC6 RID: 31430 RVA: 0x00241E3C File Offset: 0x00241E3C
		public RuntimeFunctionsParser(byte[] buff, uint offset, bool is32Bit, uint directorySize, IMAGE_SECTION_HEADER[] sectionHeaders) : base(buff, offset)
		{
			this._is32Bit = is32Bit;
			this._directorySize = directorySize;
			this._sectionHeaders = sectionHeaders;
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x00241E60 File Offset: 0x00241E60
		protected override RUNTIME_FUNCTION[] ParseTarget()
		{
			if (this._is32Bit || this._offset == 0U)
			{
				return null;
			}
			int num = 12;
			RUNTIME_FUNCTION[] array = new RUNTIME_FUNCTION[(ulong)this._directorySize / (ulong)((long)num)];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new RUNTIME_FUNCTION(this._buff, (uint)((ulong)this._offset + (ulong)((long)(i * num))), this._sectionHeaders);
			}
			return array;
		}

		// Token: 0x04003B1F RID: 15135
		private readonly uint _directorySize;

		// Token: 0x04003B20 RID: 15136
		private readonly bool _is32Bit;

		// Token: 0x04003B21 RID: 15137
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;
	}
}
