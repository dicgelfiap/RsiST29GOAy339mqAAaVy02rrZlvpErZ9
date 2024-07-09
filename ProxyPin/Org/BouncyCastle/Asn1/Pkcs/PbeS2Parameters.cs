using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B0 RID: 432
	public class PbeS2Parameters : Asn1Encodable
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x00055E54 File Offset: 0x00055E54
		public static PbeS2Parameters GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			PbeS2Parameters pbeS2Parameters = obj as PbeS2Parameters;
			if (pbeS2Parameters != null)
			{
				return pbeS2Parameters;
			}
			return new PbeS2Parameters(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00055E88 File Offset: 0x00055E88
		public PbeS2Parameters(KeyDerivationFunc keyDevFunc, EncryptionScheme encScheme)
		{
			this.func = keyDevFunc;
			this.scheme = encScheme;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00055EA0 File Offset: 0x00055EA0
		[Obsolete("Use GetInstance() instead")]
		public PbeS2Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[0].ToAsn1Object();
			if (asn1Sequence[0].Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				this.func = new KeyDerivationFunc(PkcsObjectIdentifiers.IdPbkdf2, Pbkdf2Params.GetInstance(asn1Sequence[1]));
			}
			else
			{
				this.func = new KeyDerivationFunc(asn1Sequence);
			}
			this.scheme = EncryptionScheme.GetInstance(seq[1].ToAsn1Object());
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00055F40 File Offset: 0x00055F40
		public KeyDerivationFunc KeyDerivationFunc
		{
			get
			{
				return this.func;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00055F48 File Offset: 0x00055F48
		public EncryptionScheme EncryptionScheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00055F50 File Offset: 0x00055F50
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.func,
				this.scheme
			});
		}

		// Token: 0x040009E8 RID: 2536
		private readonly KeyDerivationFunc func;

		// Token: 0x040009E9 RID: 2537
		private readonly EncryptionScheme scheme;
	}
}
