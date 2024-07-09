using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x02000806 RID: 2054
	[DebuggerDisplay("{Table} {Rid}")]
	[ComVisible(true)]
	public readonly struct MDToken : IEquatable<MDToken>, IComparable<MDToken>
	{
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x0017A7A0 File Offset: 0x0017A7A0
		public Table Table
		{
			get
			{
				return MDToken.ToTable(this.token);
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x0017A7B0 File Offset: 0x0017A7B0
		public uint Rid
		{
			get
			{
				return MDToken.ToRID(this.token);
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x0017A7C0 File Offset: 0x0017A7C0
		public uint Raw
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x0017A7C8 File Offset: 0x0017A7C8
		public bool IsNull
		{
			get
			{
				return this.Rid == 0U;
			}
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0017A7D4 File Offset: 0x0017A7D4
		public MDToken(uint token)
		{
			this.token = token;
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0017A7E0 File Offset: 0x0017A7E0
		public MDToken(int token)
		{
			this = new MDToken((uint)token);
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0017A7EC File Offset: 0x0017A7EC
		public MDToken(Table table, uint rid)
		{
			this = new MDToken((uint)((uint)table << 24) | rid);
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0017A7FC File Offset: 0x0017A7FC
		public MDToken(Table table, int rid)
		{
			this = new MDToken((uint)((int)((int)table << 24) | rid));
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0017A80C File Offset: 0x0017A80C
		public static uint ToRID(uint token)
		{
			return token & 16777215U;
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0017A818 File Offset: 0x0017A818
		public static uint ToRID(int token)
		{
			return MDToken.ToRID((uint)token);
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0017A820 File Offset: 0x0017A820
		public static Table ToTable(uint token)
		{
			return (Table)(token >> 24);
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0017A828 File Offset: 0x0017A828
		public static Table ToTable(int token)
		{
			return MDToken.ToTable((uint)token);
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0017A830 File Offset: 0x0017A830
		public int ToInt32()
		{
			return (int)this.token;
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0017A838 File Offset: 0x0017A838
		public uint ToUInt32()
		{
			return this.token;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0017A840 File Offset: 0x0017A840
		public static bool operator ==(MDToken left, MDToken right)
		{
			return left.CompareTo(right) == 0;
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x0017A850 File Offset: 0x0017A850
		public static bool operator !=(MDToken left, MDToken right)
		{
			return left.CompareTo(right) != 0;
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x0017A860 File Offset: 0x0017A860
		public static bool operator <(MDToken left, MDToken right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x0017A870 File Offset: 0x0017A870
		public static bool operator >(MDToken left, MDToken right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0017A880 File Offset: 0x0017A880
		public static bool operator <=(MDToken left, MDToken right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0017A890 File Offset: 0x0017A890
		public static bool operator >=(MDToken left, MDToken right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0017A8A0 File Offset: 0x0017A8A0
		public int CompareTo(MDToken other)
		{
			return this.token.CompareTo(other.token);
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0017A8C8 File Offset: 0x0017A8C8
		public bool Equals(MDToken other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x0017A8D4 File Offset: 0x0017A8D4
		public override bool Equals(object obj)
		{
			return obj is MDToken && this.Equals((MDToken)obj);
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x0017A8F0 File Offset: 0x0017A8F0
		public override int GetHashCode()
		{
			return (int)this.token;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0017A8F8 File Offset: 0x0017A8F8
		public override string ToString()
		{
			return this.token.ToString("X8");
		}

		// Token: 0x04002553 RID: 9555
		public const uint RID_MASK = 16777215U;

		// Token: 0x04002554 RID: 9556
		public const uint RID_MAX = 16777215U;

		// Token: 0x04002555 RID: 9557
		public const int TABLE_SHIFT = 24;

		// Token: 0x04002556 RID: 9558
		private readonly uint token;
	}
}
