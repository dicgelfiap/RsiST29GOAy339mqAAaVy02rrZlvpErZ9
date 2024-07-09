using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000ED RID: 237
	public class PkiStatusEncodable : Asn1Encodable
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00043438 File Offset: 0x00043438
		private PkiStatusEncodable(PkiStatus status) : this(new DerInteger((int)status))
		{
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00043448 File Offset: 0x00043448
		private PkiStatusEncodable(DerInteger status)
		{
			this.status = status;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00043458 File Offset: 0x00043458
		public static PkiStatusEncodable GetInstance(object obj)
		{
			if (obj is PkiStatusEncodable)
			{
				return (PkiStatusEncodable)obj;
			}
			if (obj is DerInteger)
			{
				return new PkiStatusEncodable((DerInteger)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000434AC File Offset: 0x000434AC
		public virtual BigInteger Value
		{
			get
			{
				return this.status.Value;
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000434BC File Offset: 0x000434BC
		public override Asn1Object ToAsn1Object()
		{
			return this.status;
		}

		// Token: 0x04000680 RID: 1664
		public static readonly PkiStatusEncodable granted = new PkiStatusEncodable(PkiStatus.Granted);

		// Token: 0x04000681 RID: 1665
		public static readonly PkiStatusEncodable grantedWithMods = new PkiStatusEncodable(PkiStatus.GrantedWithMods);

		// Token: 0x04000682 RID: 1666
		public static readonly PkiStatusEncodable rejection = new PkiStatusEncodable(PkiStatus.Rejection);

		// Token: 0x04000683 RID: 1667
		public static readonly PkiStatusEncodable waiting = new PkiStatusEncodable(PkiStatus.Waiting);

		// Token: 0x04000684 RID: 1668
		public static readonly PkiStatusEncodable revocationWarning = new PkiStatusEncodable(PkiStatus.RevocationWarning);

		// Token: 0x04000685 RID: 1669
		public static readonly PkiStatusEncodable revocationNotification = new PkiStatusEncodable(PkiStatus.RevocationNotification);

		// Token: 0x04000686 RID: 1670
		public static readonly PkiStatusEncodable keyUpdateWaiting = new PkiStatusEncodable(PkiStatus.KeyUpdateWarning);

		// Token: 0x04000687 RID: 1671
		private readonly DerInteger status;
	}
}
