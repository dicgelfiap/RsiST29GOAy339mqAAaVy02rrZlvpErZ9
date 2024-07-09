using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B3 RID: 435
	public class Pkcs12PbeParams : Asn1Encodable
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x00056340 File Offset: 0x00056340
		public Pkcs12PbeParams(byte[] salt, int iterations)
		{
			this.iv = new DerOctetString(salt);
			this.iterations = new DerInteger(iterations);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00056360 File Offset: 0x00056360
		private Pkcs12PbeParams(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.iterations = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000563B8 File Offset: 0x000563B8
		public static Pkcs12PbeParams GetInstance(object obj)
		{
			if (obj is Pkcs12PbeParams)
			{
				return (Pkcs12PbeParams)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pkcs12PbeParams((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0005640C File Offset: 0x0005640C
		public BigInteger Iterations
		{
			get
			{
				return this.iterations.Value;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0005641C File Offset: 0x0005641C
		public byte[] GetIV()
		{
			return this.iv.GetOctets();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0005642C File Offset: 0x0005642C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.iterations
			});
		}

		// Token: 0x040009F1 RID: 2545
		private readonly DerInteger iterations;

		// Token: 0x040009F2 RID: 2546
		private readonly Asn1OctetString iv;
	}
}
