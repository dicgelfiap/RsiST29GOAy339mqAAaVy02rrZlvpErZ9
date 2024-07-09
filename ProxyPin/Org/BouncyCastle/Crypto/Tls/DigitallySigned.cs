using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F2 RID: 1266
	public class DigitallySigned
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x000D20CC File Offset: 0x000D20CC
		public DigitallySigned(SignatureAndHashAlgorithm algorithm, byte[] signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			this.mAlgorithm = algorithm;
			this.mSignature = signature;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x000D20F4 File Offset: 0x000D20F4
		public virtual SignatureAndHashAlgorithm Algorithm
		{
			get
			{
				return this.mAlgorithm;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000D20FC File Offset: 0x000D20FC
		public virtual byte[] Signature
		{
			get
			{
				return this.mSignature;
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000D2104 File Offset: 0x000D2104
		public virtual void Encode(Stream output)
		{
			if (this.mAlgorithm != null)
			{
				this.mAlgorithm.Encode(output);
			}
			TlsUtilities.WriteOpaque16(this.mSignature, output);
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000D212C File Offset: 0x000D212C
		public static DigitallySigned Parse(TlsContext context, Stream input)
		{
			SignatureAndHashAlgorithm algorithm = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				algorithm = SignatureAndHashAlgorithm.Parse(input);
			}
			byte[] signature = TlsUtilities.ReadOpaque16(input);
			return new DigitallySigned(algorithm, signature);
		}

		// Token: 0x04001926 RID: 6438
		protected readonly SignatureAndHashAlgorithm mAlgorithm;

		// Token: 0x04001927 RID: 6439
		protected readonly byte[] mSignature;
	}
}
