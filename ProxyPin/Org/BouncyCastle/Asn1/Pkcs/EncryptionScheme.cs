using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AB RID: 427
	public class EncryptionScheme : AlgorithmIdentifier
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x00055994 File Offset: 0x00055994
		public EncryptionScheme(DerObjectIdentifier objectID) : base(objectID)
		{
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000559A0 File Offset: 0x000559A0
		public EncryptionScheme(DerObjectIdentifier objectID, Asn1Encodable parameters) : base(objectID, parameters)
		{
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000559AC File Offset: 0x000559AC
		internal EncryptionScheme(Asn1Sequence seq) : this((DerObjectIdentifier)seq[0], seq[1])
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000559C8 File Offset: 0x000559C8
		public new static EncryptionScheme GetInstance(object obj)
		{
			if (obj is EncryptionScheme)
			{
				return (EncryptionScheme)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptionScheme((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00055A1C File Offset: 0x00055A1C
		public Asn1Object Asn1Object
		{
			get
			{
				return this.Parameters.ToAsn1Object();
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00055A2C File Offset: 0x00055A2C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.Algorithm,
				this.Parameters
			});
		}
	}
}
