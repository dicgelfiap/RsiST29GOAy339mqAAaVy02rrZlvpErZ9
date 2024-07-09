using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AC RID: 428
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x00055A64 File Offset: 0x00055A64
		public static IssuerAndSerialNumber GetInstance(object obj)
		{
			if (obj is IssuerAndSerialNumber)
			{
				return (IssuerAndSerialNumber)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerAndSerialNumber((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00055AB8 File Offset: 0x00055AB8
		private IssuerAndSerialNumber(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.name = X509Name.GetInstance(seq[0]);
			this.certSerialNumber = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00055B10 File Offset: 0x00055B10
		public IssuerAndSerialNumber(X509Name name, BigInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = new DerInteger(certSerialNumber);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00055B2C File Offset: 0x00055B2C
		public IssuerAndSerialNumber(X509Name name, DerInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = certSerialNumber;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00055B44 File Offset: 0x00055B44
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00055B4C File Offset: 0x00055B4C
		public DerInteger CertificateSerialNumber
		{
			get
			{
				return this.certSerialNumber;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00055B54 File Offset: 0x00055B54
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.certSerialNumber
			});
		}

		// Token: 0x040009E1 RID: 2529
		private readonly X509Name name;

		// Token: 0x040009E2 RID: 2530
		private readonly DerInteger certSerialNumber;
	}
}
