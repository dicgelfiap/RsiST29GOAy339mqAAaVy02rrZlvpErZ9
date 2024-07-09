using System;

namespace PeNet.Parser
{
	// Token: 0x02000C06 RID: 3078
	internal abstract class SafeParser<T> where T : class
	{
		// Token: 0x06007AC8 RID: 31432 RVA: 0x00241ED4 File Offset: 0x00241ED4
		internal SafeParser(byte[] buff, uint offset)
		{
			this._buff = buff;
			this._offset = offset;
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x00241EEC File Offset: 0x00241EEC
		private bool SanityCheckFailed()
		{
			long num = (long)((ulong)this._offset);
			byte[] buff = this._buff;
			int? num2 = (buff != null) ? new int?(buff.Length) : null;
			long? num3 = (num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null;
			return num > num3.GetValueOrDefault() & num3 != null;
		}

		// Token: 0x06007ACA RID: 31434
		protected abstract T ParseTarget();

		// Token: 0x06007ACB RID: 31435 RVA: 0x00241F60 File Offset: 0x00241F60
		public T GetParserTarget()
		{
			if (this._alreadyParsed)
			{
				return this._target;
			}
			this._alreadyParsed = true;
			if (this.SanityCheckFailed())
			{
				return default(T);
			}
			try
			{
				this._target = this.ParseTarget();
			}
			catch (Exception)
			{
				this._target = default(T);
			}
			return this._target;
		}

		// Token: 0x04003B22 RID: 15138
		protected readonly byte[] _buff;

		// Token: 0x04003B23 RID: 15139
		protected readonly uint _offset;

		// Token: 0x04003B24 RID: 15140
		private bool _alreadyParsed;

		// Token: 0x04003B25 RID: 15141
		private T _target;
	}
}
