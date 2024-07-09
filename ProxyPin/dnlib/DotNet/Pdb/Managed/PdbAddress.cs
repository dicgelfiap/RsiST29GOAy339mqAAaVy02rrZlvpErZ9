using System;
using System.Diagnostics;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000952 RID: 2386
	[DebuggerDisplay("{Section}:{Offset}")]
	internal readonly struct PdbAddress : IEquatable<PdbAddress>, IComparable<PdbAddress>
	{
		// Token: 0x06005B98 RID: 23448 RVA: 0x001BEEAC File Offset: 0x001BEEAC
		public PdbAddress(ushort section, int offset)
		{
			this.Section = section;
			this.Offset = (uint)offset;
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x001BEEBC File Offset: 0x001BEEBC
		public PdbAddress(ushort section, uint offset)
		{
			this.Section = section;
			this.Offset = offset;
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001BEECC File Offset: 0x001BEECC
		public static bool operator <=(PdbAddress a, PdbAddress b)
		{
			return a.CompareTo(b) <= 0;
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x001BEEDC File Offset: 0x001BEEDC
		public static bool operator <(PdbAddress a, PdbAddress b)
		{
			return a.CompareTo(b) < 0;
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x001BEEEC File Offset: 0x001BEEEC
		public static bool operator >=(PdbAddress a, PdbAddress b)
		{
			return a.CompareTo(b) >= 0;
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x001BEEFC File Offset: 0x001BEEFC
		public static bool operator >(PdbAddress a, PdbAddress b)
		{
			return a.CompareTo(b) > 0;
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x001BEF0C File Offset: 0x001BEF0C
		public static bool operator ==(PdbAddress a, PdbAddress b)
		{
			return a.Equals(b);
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x001BEF18 File Offset: 0x001BEF18
		public static bool operator !=(PdbAddress a, PdbAddress b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x001BEF28 File Offset: 0x001BEF28
		public int CompareTo(PdbAddress other)
		{
			if (this.Section != other.Section)
			{
				return this.Section.CompareTo(other.Section);
			}
			return this.Offset.CompareTo(other.Offset);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x001BEF74 File Offset: 0x001BEF74
		public bool Equals(PdbAddress other)
		{
			return this.Section == other.Section && this.Offset == other.Offset;
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x001BEF98 File Offset: 0x001BEF98
		public override bool Equals(object obj)
		{
			return obj is PdbAddress && this.Equals((PdbAddress)obj);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x001BEFB4 File Offset: 0x001BEFB4
		public override int GetHashCode()
		{
			return (int)this.Section << 16 ^ (int)this.Offset;
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x001BEFC8 File Offset: 0x001BEFC8
		public override string ToString()
		{
			return string.Format("{0:X4}:{1:X8}", this.Section, this.Offset);
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x001BEFEC File Offset: 0x001BEFEC
		public static PdbAddress ReadAddress(ref DataReader reader)
		{
			uint offset = reader.ReadUInt32();
			return new PdbAddress(reader.ReadUInt16(), offset);
		}

		// Token: 0x04002C7A RID: 11386
		public readonly ushort Section;

		// Token: 0x04002C7B RID: 11387
		public readonly uint Offset;
	}
}
