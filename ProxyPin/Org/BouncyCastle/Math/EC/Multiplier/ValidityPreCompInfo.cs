using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FD RID: 1533
	internal class ValidityPreCompInfo : PreCompInfo
	{
		// Token: 0x06003335 RID: 13109 RVA: 0x00108ED0 File Offset: 0x00108ED0
		internal bool HasFailed()
		{
			return this.failed;
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x00108ED8 File Offset: 0x00108ED8
		internal void ReportFailed()
		{
			this.failed = true;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x00108EE4 File Offset: 0x00108EE4
		internal bool HasCurveEquationPassed()
		{
			return this.curveEquationPassed;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x00108EEC File Offset: 0x00108EEC
		internal void ReportCurveEquationPassed()
		{
			this.curveEquationPassed = true;
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x00108EF8 File Offset: 0x00108EF8
		internal bool HasOrderPassed()
		{
			return this.orderPassed;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x00108F00 File Offset: 0x00108F00
		internal void ReportOrderPassed()
		{
			this.orderPassed = true;
		}

		// Token: 0x04001C8F RID: 7311
		internal static readonly string PRECOMP_NAME = "bc_validity";

		// Token: 0x04001C90 RID: 7312
		private bool failed = false;

		// Token: 0x04001C91 RID: 7313
		private bool curveEquationPassed = false;

		// Token: 0x04001C92 RID: 7314
		private bool orderPassed = false;
	}
}
