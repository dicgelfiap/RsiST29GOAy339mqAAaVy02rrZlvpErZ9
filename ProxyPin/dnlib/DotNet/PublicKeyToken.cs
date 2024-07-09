using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000834 RID: 2100
	[ComVisible(true)]
	public sealed class PublicKeyToken : PublicKeyBase
	{
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x001868BC File Offset: 0x001868BC
		public override PublicKeyToken Token
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x001868C0 File Offset: 0x001868C0
		public PublicKeyToken() : base(null)
		{
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x001868CC File Offset: 0x001868CC
		public PublicKeyToken(byte[] data) : base(data)
		{
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x001868D8 File Offset: 0x001868D8
		public PublicKeyToken(string hexString) : base(hexString)
		{
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x001868E4 File Offset: 0x001868E4
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PublicKeyToken publicKeyToken = obj as PublicKeyToken;
			return publicKeyToken != null && Utils.Equals(this.Data, publicKeyToken.Data);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00186920 File Offset: 0x00186920
		public override int GetHashCode()
		{
			return Utils.GetHashCode(this.Data);
		}
	}
}
