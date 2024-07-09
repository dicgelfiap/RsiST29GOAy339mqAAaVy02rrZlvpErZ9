using System;
using System.Collections;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000691 RID: 1681
	public class PkixCertPathBuilder
	{
		// Token: 0x06003AB0 RID: 15024 RVA: 0x0013C170 File Offset: 0x0013C170
		public virtual PkixCertPathBuilderResult Build(PkixBuilderParameters pkixParams)
		{
			IX509Selector targetCertConstraints = pkixParams.GetTargetCertConstraints();
			if (!(targetCertConstraints is X509CertStoreSelector))
			{
				throw new PkixCertPathBuilderException(string.Concat(new string[]
				{
					"TargetConstraints must be an instance of ",
					typeof(X509CertStoreSelector).FullName,
					" for ",
					Platform.GetTypeName(this),
					" class."
				}));
			}
			ISet set = new HashSet();
			try
			{
				set.AddAll(PkixCertPathValidatorUtilities.FindCertificates((X509CertStoreSelector)targetCertConstraints, pkixParams.GetStores()));
			}
			catch (Exception exception)
			{
				throw new PkixCertPathBuilderException("Error finding target certificate.", exception);
			}
			if (set.IsEmpty)
			{
				throw new PkixCertPathBuilderException("No certificate found matching targetContraints.");
			}
			PkixCertPathBuilderResult pkixCertPathBuilderResult = null;
			IList tbvPath = Platform.CreateArrayList();
			foreach (object obj in set)
			{
				X509Certificate tbvCert = (X509Certificate)obj;
				pkixCertPathBuilderResult = this.Build(tbvCert, pkixParams, tbvPath);
				if (pkixCertPathBuilderResult != null)
				{
					break;
				}
			}
			if (pkixCertPathBuilderResult == null && this.certPathException != null)
			{
				throw new PkixCertPathBuilderException(this.certPathException.Message, this.certPathException.InnerException);
			}
			if (pkixCertPathBuilderResult == null && this.certPathException == null)
			{
				throw new PkixCertPathBuilderException("Unable to find certificate chain.");
			}
			return pkixCertPathBuilderResult;
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x0013C2F8 File Offset: 0x0013C2F8
		protected virtual PkixCertPathBuilderResult Build(X509Certificate tbvCert, PkixBuilderParameters pkixParams, IList tbvPath)
		{
			if (tbvPath.Contains(tbvCert))
			{
				return null;
			}
			if (pkixParams.GetExcludedCerts().Contains(tbvCert))
			{
				return null;
			}
			if (pkixParams.MaxPathLength != -1 && tbvPath.Count - 1 > pkixParams.MaxPathLength)
			{
				return null;
			}
			tbvPath.Add(tbvCert);
			PkixCertPathBuilderResult pkixCertPathBuilderResult = null;
			PkixCertPathValidator pkixCertPathValidator = new PkixCertPathValidator();
			try
			{
				if (PkixCertPathValidatorUtilities.IsIssuerTrustAnchor(tbvCert, pkixParams.GetTrustAnchors()))
				{
					PkixCertPath certPath = null;
					try
					{
						certPath = new PkixCertPath(tbvPath);
					}
					catch (Exception innerException)
					{
						throw new Exception("Certification path could not be constructed from certificate list.", innerException);
					}
					PkixCertPathValidatorResult pkixCertPathValidatorResult = null;
					try
					{
						pkixCertPathValidatorResult = pkixCertPathValidator.Validate(certPath, pkixParams);
					}
					catch (Exception innerException2)
					{
						throw new Exception("Certification path could not be validated.", innerException2);
					}
					return new PkixCertPathBuilderResult(certPath, pkixCertPathValidatorResult.TrustAnchor, pkixCertPathValidatorResult.PolicyTree, pkixCertPathValidatorResult.SubjectPublicKey);
				}
				try
				{
					PkixCertPathValidatorUtilities.AddAdditionalStoresFromAltNames(tbvCert, pkixParams);
				}
				catch (CertificateParsingException innerException3)
				{
					throw new Exception("No additiontal X.509 stores can be added from certificate locations.", innerException3);
				}
				HashSet hashSet = new HashSet();
				try
				{
					hashSet.AddAll(PkixCertPathValidatorUtilities.FindIssuerCerts(tbvCert, pkixParams));
				}
				catch (Exception innerException4)
				{
					throw new Exception("Cannot find issuer certificate for certificate in certification path.", innerException4);
				}
				if (hashSet.IsEmpty)
				{
					throw new Exception("No issuer certificate for certificate in certification path found.");
				}
				foreach (object obj in hashSet)
				{
					X509Certificate tbvCert2 = (X509Certificate)obj;
					pkixCertPathBuilderResult = this.Build(tbvCert2, pkixParams, tbvPath);
					if (pkixCertPathBuilderResult != null)
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				this.certPathException = ex;
			}
			if (pkixCertPathBuilderResult == null)
			{
				tbvPath.Remove(tbvCert);
			}
			return pkixCertPathBuilderResult;
		}

		// Token: 0x04001E67 RID: 7783
		private Exception certPathException;
	}
}
