using System;
using System.Text;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200068F RID: 1679
	public class PkixBuilderParameters : PkixParameters
	{
		// Token: 0x06003A9A RID: 15002 RVA: 0x0013B8E8 File Offset: 0x0013B8E8
		public static PkixBuilderParameters GetInstance(PkixParameters pkixParams)
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(pkixParams.GetTrustAnchors(), new X509CertStoreSelector(pkixParams.GetTargetCertConstraints()));
			pkixBuilderParameters.SetParams(pkixParams);
			return pkixBuilderParameters;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0013B918 File Offset: 0x0013B918
		public PkixBuilderParameters(ISet trustAnchors, IX509Selector targetConstraints) : base(trustAnchors)
		{
			this.SetTargetCertConstraints(targetConstraints);
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x0013B93C File Offset: 0x0013B93C
		// (set) Token: 0x06003A9D RID: 15005 RVA: 0x0013B944 File Offset: 0x0013B944
		public virtual int MaxPathLength
		{
			get
			{
				return this.maxPathLength;
			}
			set
			{
				if (value < -1)
				{
					throw new InvalidParameterException("The maximum path length parameter can not be less than -1.");
				}
				this.maxPathLength = value;
			}
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x0013B960 File Offset: 0x0013B960
		public virtual ISet GetExcludedCerts()
		{
			return new HashSet(this.excludedCerts);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0013B970 File Offset: 0x0013B970
		public virtual void SetExcludedCerts(ISet excludedCerts)
		{
			if (excludedCerts == null)
			{
				this.excludedCerts = new HashSet();
				return;
			}
			this.excludedCerts = new HashSet(excludedCerts);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x0013B990 File Offset: 0x0013B990
		protected override void SetParams(PkixParameters parameters)
		{
			base.SetParams(parameters);
			if (parameters is PkixBuilderParameters)
			{
				PkixBuilderParameters pkixBuilderParameters = (PkixBuilderParameters)parameters;
				this.maxPathLength = pkixBuilderParameters.maxPathLength;
				this.excludedCerts = new HashSet(pkixBuilderParameters.excludedCerts);
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0013B9D8 File Offset: 0x0013B9D8
		public override object Clone()
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(this.GetTrustAnchors(), this.GetTargetCertConstraints());
			pkixBuilderParameters.SetParams(this);
			return pkixBuilderParameters;
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x0013BA04 File Offset: 0x0013BA04
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PkixBuilderParameters [" + newLine);
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("  Maximum Path Length: ");
			stringBuilder.Append(this.MaxPathLength);
			stringBuilder.Append(newLine + "]" + newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04001E63 RID: 7779
		private int maxPathLength = 5;

		// Token: 0x04001E64 RID: 7780
		private ISet excludedCerts = new HashSet();
	}
}
