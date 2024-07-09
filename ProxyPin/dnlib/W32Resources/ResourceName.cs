using System;
using System.Runtime.InteropServices;

namespace dnlib.W32Resources
{
	// Token: 0x02000735 RID: 1845
	[ComVisible(true)]
	public readonly struct ResourceName : IComparable<ResourceName>, IEquatable<ResourceName>
	{
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x00161A00 File Offset: 0x00161A00
		public bool HasId
		{
			get
			{
				return this.name == null;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x00161A0C File Offset: 0x00161A0C
		public bool HasName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x00161A1C File Offset: 0x00161A1C
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x00161A24 File Offset: 0x00161A24
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00161A2C File Offset: 0x00161A2C
		public ResourceName(int id)
		{
			this.id = id;
			this.name = null;
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x00161A3C File Offset: 0x00161A3C
		public ResourceName(string name)
		{
			this.id = 0;
			this.name = name;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x00161A4C File Offset: 0x00161A4C
		public static implicit operator ResourceName(int id)
		{
			return new ResourceName(id);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x00161A54 File Offset: 0x00161A54
		public static implicit operator ResourceName(string name)
		{
			return new ResourceName(name);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00161A5C File Offset: 0x00161A5C
		public static bool operator <(ResourceName left, ResourceName right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x00161A6C File Offset: 0x00161A6C
		public static bool operator <=(ResourceName left, ResourceName right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x00161A7C File Offset: 0x00161A7C
		public static bool operator >(ResourceName left, ResourceName right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x00161A8C File Offset: 0x00161A8C
		public static bool operator >=(ResourceName left, ResourceName right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x00161A9C File Offset: 0x00161A9C
		public static bool operator ==(ResourceName left, ResourceName right)
		{
			return left.Equals(right);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x00161AA8 File Offset: 0x00161AA8
		public static bool operator !=(ResourceName left, ResourceName right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x00161AB8 File Offset: 0x00161AB8
		public int CompareTo(ResourceName other)
		{
			if (this.HasId != other.HasId)
			{
				if (!this.HasName)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this.HasId)
				{
					return this.id.CompareTo(other.id);
				}
				return this.name.ToUpperInvariant().CompareTo(other.name.ToUpperInvariant());
			}
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x00161B28 File Offset: 0x00161B28
		public bool Equals(ResourceName other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x00161B34 File Offset: 0x00161B34
		public override bool Equals(object obj)
		{
			return obj is ResourceName && this.Equals((ResourceName)obj);
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x00161B50 File Offset: 0x00161B50
		public override int GetHashCode()
		{
			if (this.HasId)
			{
				return this.id;
			}
			return this.name.GetHashCode();
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x00161B70 File Offset: 0x00161B70
		public override string ToString()
		{
			if (!this.HasId)
			{
				return this.name;
			}
			return this.id.ToString();
		}

		// Token: 0x0400227A RID: 8826
		private readonly int id;

		// Token: 0x0400227B RID: 8827
		private readonly string name;
	}
}
