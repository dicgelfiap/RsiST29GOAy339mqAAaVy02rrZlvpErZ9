using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AF RID: 431
	public class PbeParameter : Asn1Encodable
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x00055D28 File Offset: 0x00055D28
		public static PbeParameter GetInstance(object obj)
		{
			if (obj is PbeParameter || obj == null)
			{
				return (PbeParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbeParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00055D84 File Offset: 0x00055D84
		private PbeParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.salt = Asn1OctetString.GetInstance(seq[0]);
			this.iterationCount = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00055DDC File Offset: 0x00055DDC
		public PbeParameter(byte[] salt, int iterationCount)
		{
			this.salt = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00055DFC File Offset: 0x00055DFC
		public byte[] GetSalt()
		{
			return this.salt.GetOctets();
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00055E0C File Offset: 0x00055E0C
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00055E1C File Offset: 0x00055E1C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.salt,
				this.iterationCount
			});
		}

		// Token: 0x040009E6 RID: 2534
		private readonly Asn1OctetString salt;

		// Token: 0x040009E7 RID: 2535
		private readonly DerInteger iterationCount;
	}
}
