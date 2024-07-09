using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000659 RID: 1625
	public class PgpOnePassSignatureList : PgpObject
	{
		// Token: 0x0600385E RID: 14430 RVA: 0x0012ED80 File Offset: 0x0012ED80
		public PgpOnePassSignatureList(PgpOnePassSignature[] sigs)
		{
			this.sigs = (PgpOnePassSignature[])sigs.Clone();
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x0012ED9C File Offset: 0x0012ED9C
		public PgpOnePassSignatureList(PgpOnePassSignature sig)
		{
			this.sigs = new PgpOnePassSignature[]
			{
				sig
			};
		}

		// Token: 0x170009CA RID: 2506
		public PgpOnePassSignature this[int index]
		{
			get
			{
				return this.sigs[index];
			}
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x0012EDDC File Offset: 0x0012EDDC
		[Obsolete("Use 'object[index]' syntax instead")]
		public PgpOnePassSignature Get(int index)
		{
			return this[index];
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x0012EDE8 File Offset: 0x0012EDE8
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x0012EDF4 File Offset: 0x0012EDF4
		public int Count
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x0012EE00 File Offset: 0x0012EE00
		public bool IsEmpty
		{
			get
			{
				return this.sigs.Length == 0;
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private readonly PgpOnePassSignature[] sigs;
	}
}
