using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000832 RID: 2098
	[ComVisible(true)]
	public sealed class PublicKey : PublicKeyBase
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x0018662C File Offset: 0x0018662C
		public override PublicKeyToken Token
		{
			get
			{
				if (this.publicKeyToken == null && !base.IsNullOrEmpty)
				{
					Interlocked.CompareExchange<PublicKeyToken>(ref this.publicKeyToken, AssemblyHash.CreatePublicKeyToken(this.data), null);
				}
				return this.publicKeyToken;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00186664 File Offset: 0x00186664
		public override byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0018666C File Offset: 0x0018666C
		public PublicKey() : base(null)
		{
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00186678 File Offset: 0x00186678
		public PublicKey(byte[] data) : base(data)
		{
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00186684 File Offset: 0x00186684
		public PublicKey(string hexString) : base(hexString)
		{
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00186690 File Offset: 0x00186690
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PublicKey publicKey = obj as PublicKey;
			return publicKey != null && Utils.Equals(this.Data, publicKey.Data);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x001866CC File Offset: 0x001866CC
		public override int GetHashCode()
		{
			return Utils.GetHashCode(this.Data);
		}

		// Token: 0x040026C0 RID: 9920
		private const AssemblyHashAlgorithm DEFAULT_ALGORITHM = AssemblyHashAlgorithm.SHA1;

		// Token: 0x040026C1 RID: 9921
		private PublicKeyToken publicKeyToken;
	}
}
