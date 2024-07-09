using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070F RID: 1807
	public sealed class X509StoreFactory
	{
		// Token: 0x06003F35 RID: 16181 RVA: 0x0015ADC4 File Offset: 0x0015ADC4
		private X509StoreFactory()
		{
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0015ADCC File Offset: 0x0015ADCC
		public static IX509Store Create(string type, IX509StoreParameters parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string[] array = Platform.ToUpperInvariant(type).Split(new char[]
			{
				'/'
			});
			if (array.Length < 2)
			{
				throw new ArgumentException("type");
			}
			if (array[1] != "COLLECTION")
			{
				throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
			}
			X509CollectionStoreParameters x509CollectionStoreParameters = (X509CollectionStoreParameters)parameters;
			ICollection collection = x509CollectionStoreParameters.GetCollection();
			string text;
			if ((text = array[0]) != null)
			{
				text = string.IsInterned(text);
				if (text != "ATTRIBUTECERTIFICATE")
				{
					if (text != "CERTIFICATE")
					{
						if (text != "CERTIFICATEPAIR")
						{
							if (text != "CRL")
							{
								goto IL_132;
							}
							X509StoreFactory.checkCorrectType(collection, typeof(X509Crl));
						}
						else
						{
							X509StoreFactory.checkCorrectType(collection, typeof(X509CertificatePair));
						}
					}
					else
					{
						X509StoreFactory.checkCorrectType(collection, typeof(X509Certificate));
					}
				}
				else
				{
					X509StoreFactory.checkCorrectType(collection, typeof(IX509AttributeCertificate));
				}
				return new X509CollectionStore(collection);
			}
			IL_132:
			throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0015AF2C File Offset: 0x0015AF2C
		private static void checkCorrectType(ICollection coll, Type t)
		{
			foreach (object o in coll)
			{
				if (!t.IsInstanceOfType(o))
				{
					throw new InvalidCastException("Can't cast object to type: " + t.FullName);
				}
			}
		}
	}
}
