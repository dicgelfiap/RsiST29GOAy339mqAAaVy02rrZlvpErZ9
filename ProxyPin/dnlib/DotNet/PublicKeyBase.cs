using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000833 RID: 2099
	[ComVisible(true)]
	public abstract class PublicKeyBase
	{
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x001866DC File Offset: 0x001866DC
		public bool IsNullOrEmpty
		{
			get
			{
				return this.data == null || this.data.Length == 0;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x001866F8 File Offset: 0x001866F8
		public bool IsNull
		{
			get
			{
				return this.Data == null;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x00186704 File Offset: 0x00186704
		public virtual byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06004E94 RID: 20116
		public abstract PublicKeyToken Token { get; }

		// Token: 0x06004E95 RID: 20117 RVA: 0x0018670C File Offset: 0x0018670C
		protected PublicKeyBase(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0018671C File Offset: 0x0018671C
		protected PublicKeyBase(string hexString)
		{
			this.data = PublicKeyBase.Parse(hexString);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00186730 File Offset: 0x00186730
		private static byte[] Parse(string hexString)
		{
			if (hexString == null || hexString == "null")
			{
				return null;
			}
			return Utils.ParseBytes(hexString);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00186750 File Offset: 0x00186750
		public static bool IsNullOrEmpty2(PublicKeyBase a)
		{
			return a == null || a.IsNullOrEmpty;
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x00186760 File Offset: 0x00186760
		public static PublicKeyToken ToPublicKeyToken(PublicKeyBase pkb)
		{
			PublicKeyToken publicKeyToken = pkb as PublicKeyToken;
			if (publicKeyToken != null)
			{
				return publicKeyToken;
			}
			PublicKey publicKey = pkb as PublicKey;
			if (publicKey != null)
			{
				return publicKey.Token;
			}
			return null;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x00186798 File Offset: 0x00186798
		public static int TokenCompareTo(PublicKeyBase a, PublicKeyBase b)
		{
			if (a == b)
			{
				return 0;
			}
			return PublicKeyBase.TokenCompareTo(PublicKeyBase.ToPublicKeyToken(a), PublicKeyBase.ToPublicKeyToken(b));
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x001867B4 File Offset: 0x001867B4
		public static bool TokenEquals(PublicKeyBase a, PublicKeyBase b)
		{
			return PublicKeyBase.TokenCompareTo(a, b) == 0;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x001867C0 File Offset: 0x001867C0
		public static int TokenCompareTo(PublicKeyToken a, PublicKeyToken b)
		{
			if (a == b)
			{
				return 0;
			}
			return PublicKeyBase.TokenCompareTo((a != null) ? a.Data : null, (b != null) ? b.Data : null);
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x001867F4 File Offset: 0x001867F4
		private static int TokenCompareTo(byte[] a, byte[] b)
		{
			return Utils.CompareTo(a ?? PublicKeyBase.EmptyByteArray, b ?? PublicKeyBase.EmptyByteArray);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00186818 File Offset: 0x00186818
		public static bool TokenEquals(PublicKeyToken a, PublicKeyToken b)
		{
			return PublicKeyBase.TokenCompareTo(a, b) == 0;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00186824 File Offset: 0x00186824
		public static int GetHashCodeToken(PublicKeyBase a)
		{
			return PublicKeyBase.GetHashCode(PublicKeyBase.ToPublicKeyToken(a));
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00186834 File Offset: 0x00186834
		public static int GetHashCode(PublicKeyToken a)
		{
			if (a == null)
			{
				return 0;
			}
			return Utils.GetHashCode(a.Data);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0018684C File Offset: 0x0018684C
		public static PublicKey CreatePublicKey(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return new PublicKey(data);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0018685C File Offset: 0x0018685C
		public static PublicKeyToken CreatePublicKeyToken(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return new PublicKeyToken(data);
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0018686C File Offset: 0x0018686C
		public static byte[] GetRawData(PublicKeyBase pkb)
		{
			if (pkb == null)
			{
				return null;
			}
			return pkb.Data;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0018687C File Offset: 0x0018687C
		public override string ToString()
		{
			byte[] array = this.Data;
			if (array == null || array.Length == 0)
			{
				return "null";
			}
			return Utils.ToHex(array, false);
		}

		// Token: 0x040026C2 RID: 9922
		protected readonly byte[] data;

		// Token: 0x040026C3 RID: 9923
		private static readonly byte[] EmptyByteArray = Array2.Empty<byte>();
	}
}
