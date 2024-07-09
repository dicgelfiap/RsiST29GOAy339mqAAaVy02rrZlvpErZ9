using System;
using System.ComponentModel;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CCE RID: 3278
	[System.Memory.IsReadOnly]
	[ComVisible(true)]
	public struct SequencePosition : IEquatable<SequencePosition>
	{
		// Token: 0x06008410 RID: 33808 RVA: 0x00268F04 File Offset: 0x00268F04
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x00268F14 File Offset: 0x00268F14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetObject()
		{
			return this._object;
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x00268F1C File Offset: 0x00268F1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetInteger()
		{
			return this._integer;
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x00268F24 File Offset: 0x00268F24
		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		// Token: 0x06008414 RID: 33812 RVA: 0x00268F4C File Offset: 0x00268F4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is SequencePosition)
			{
				SequencePosition other = (SequencePosition)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06008415 RID: 33813 RVA: 0x00268F7C File Offset: 0x00268F7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			object @object = this._object;
			return HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		// Token: 0x04003D5C RID: 15708
		private readonly object _object;

		// Token: 0x04003D5D RID: 15709
		private readonly int _integer;
	}
}
