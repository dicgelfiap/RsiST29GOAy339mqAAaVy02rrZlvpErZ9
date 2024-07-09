using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004CA RID: 1226
	public abstract class AlertLevel
	{
		// Token: 0x060025EE RID: 9710 RVA: 0x000CF7E8 File Offset: 0x000CF7E8
		public static string GetName(byte alertDescription)
		{
			switch (alertDescription)
			{
			case 1:
				return "warning";
			case 2:
				return "fatal";
			default:
				return "UNKNOWN";
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000CF824 File Offset: 0x000CF824
		public static string GetText(byte alertDescription)
		{
			return string.Concat(new object[]
			{
				AlertLevel.GetName(alertDescription),
				"(",
				alertDescription,
				")"
			});
		}

		// Token: 0x040017C6 RID: 6086
		public const byte warning = 1;

		// Token: 0x040017C7 RID: 6087
		public const byte fatal = 2;
	}
}
