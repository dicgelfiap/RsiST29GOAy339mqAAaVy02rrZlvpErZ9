using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000666 RID: 1638
	public class PgpSignatureList : PgpObject
	{
		// Token: 0x06003956 RID: 14678 RVA: 0x001344E0 File Offset: 0x001344E0
		public PgpSignatureList(PgpSignature[] sigs)
		{
			this.sigs = (PgpSignature[])sigs.Clone();
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x001344FC File Offset: 0x001344FC
		public PgpSignatureList(PgpSignature sig)
		{
			this.sigs = new PgpSignature[]
			{
				sig
			};
		}

		// Token: 0x170009F0 RID: 2544
		public PgpSignature this[int index]
		{
			get
			{
				return this.sigs[index];
			}
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x0013453C File Offset: 0x0013453C
		[Obsolete("Use 'object[index]' syntax instead")]
		public PgpSignature Get(int index)
		{
			return this[index];
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x00134548 File Offset: 0x00134548
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x00134554 File Offset: 0x00134554
		public int Count
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x00134560 File Offset: 0x00134560
		public bool IsEmpty
		{
			get
			{
				return this.sigs.Length == 0;
			}
		}

		// Token: 0x04001E09 RID: 7689
		private PgpSignature[] sigs;
	}
}
