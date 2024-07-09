using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A3E RID: 2622
	public struct Place : IEquatable<Place>
	{
		// Token: 0x060066BF RID: 26303 RVA: 0x001F35DC File Offset: 0x001F35DC
		public Place(int iChar, int iLine)
		{
			this.iChar = iChar;
			this.iLine = iLine;
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x001F35F0 File Offset: 0x001F35F0
		public void Offset(int dx, int dy)
		{
			this.iChar += dx;
			this.iLine += dy;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x001F3610 File Offset: 0x001F3610
		public bool Equals(Place other)
		{
			return this.iChar == other.iChar && this.iLine == other.iLine;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x001F3650 File Offset: 0x001F3650
		public override bool Equals(object obj)
		{
			return obj is Place && this.Equals((Place)obj);
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001F3688 File Offset: 0x001F3688
		public override int GetHashCode()
		{
			return this.iChar.GetHashCode() ^ this.iLine.GetHashCode();
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x001F36B8 File Offset: 0x001F36B8
		public static bool operator !=(Place p1, Place p2)
		{
			return !p1.Equals(p2);
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x001F36DC File Offset: 0x001F36DC
		public static bool operator ==(Place p1, Place p2)
		{
			return p1.Equals(p2);
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x001F3700 File Offset: 0x001F3700
		public static bool operator <(Place p1, Place p2)
		{
			bool flag = p1.iLine < p2.iLine;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = p1.iLine > p2.iLine;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = p1.iChar < p2.iChar;
					result = flag3;
				}
			}
			return result;
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x001F3770 File Offset: 0x001F3770
		public static bool operator <=(Place p1, Place p2)
		{
			bool flag = p1.Equals(p2);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = p1.iLine < p2.iLine;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = p1.iLine > p2.iLine;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = p1.iChar < p2.iChar;
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x001F37F8 File Offset: 0x001F37F8
		public static bool operator >(Place p1, Place p2)
		{
			bool flag = p1.iLine > p2.iLine;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = p1.iLine < p2.iLine;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = p1.iChar > p2.iChar;
					result = flag3;
				}
			}
			return result;
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x001F3868 File Offset: 0x001F3868
		public static bool operator >=(Place p1, Place p2)
		{
			bool flag = p1.Equals(p2);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = p1.iLine > p2.iLine;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = p1.iLine < p2.iLine;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = p1.iChar > p2.iChar;
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x001F38F0 File Offset: 0x001F38F0
		public static Place operator +(Place p1, Place p2)
		{
			return new Place(p1.iChar + p2.iChar, p1.iLine + p2.iLine);
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x001F3928 File Offset: 0x001F3928
		public static Place Empty
		{
			get
			{
				return default(Place);
			}
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x001F394C File Offset: 0x001F394C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.iChar,
				",",
				this.iLine,
				")"
			});
		}

		// Token: 0x040034A3 RID: 13475
		public int iChar;

		// Token: 0x040034A4 RID: 13476
		public int iLine;
	}
}
