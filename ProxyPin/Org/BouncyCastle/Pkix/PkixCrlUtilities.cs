using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200069A RID: 1690
	public class PkixCrlUtilities
	{
		// Token: 0x06003AF1 RID: 15089 RVA: 0x0013E2B0 File Offset: 0x0013E2B0
		public virtual ISet FindCrls(X509CrlStoreSelector crlselect, PkixParameters paramsPkix, DateTime currentDate)
		{
			ISet set = new HashSet();
			try
			{
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetAdditionalStores()));
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetStores()));
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception obtaining complete CRLs.", innerException);
			}
			ISet set2 = new HashSet();
			DateTime dateTime = currentDate;
			if (paramsPkix.Date != null)
			{
				dateTime = paramsPkix.Date.Value;
			}
			foreach (object obj in set)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (x509Crl.NextUpdate.Value.CompareTo(dateTime) > 0)
				{
					X509Certificate certificateChecking = crlselect.CertificateChecking;
					if (certificateChecking != null)
					{
						if (x509Crl.ThisUpdate.CompareTo(certificateChecking.NotAfter) < 0)
						{
							set2.Add(x509Crl);
						}
					}
					else
					{
						set2.Add(x509Crl);
					}
				}
			}
			return set2;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x0013E3E0 File Offset: 0x0013E3E0
		public virtual ISet FindCrls(X509CrlStoreSelector crlselect, PkixParameters paramsPkix)
		{
			ISet set = new HashSet();
			try
			{
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetStores()));
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception obtaining complete CRLs.", innerException);
			}
			return set;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x0013E42C File Offset: 0x0013E42C
		private ICollection FindCrls(X509CrlStoreSelector crlSelect, IList crlStores)
		{
			ISet set = new HashSet();
			Exception ex = null;
			bool flag = false;
			foreach (object obj in crlStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					set.AddAll(ix509Store.GetMatches(crlSelect));
					flag = true;
				}
				catch (X509StoreException innerException)
				{
					ex = new Exception("Exception searching in X.509 CRL store.", innerException);
				}
			}
			if (!flag && ex != null)
			{
				throw ex;
			}
			return set;
		}
	}
}
