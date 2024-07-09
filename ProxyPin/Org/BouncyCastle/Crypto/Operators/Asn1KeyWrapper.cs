using System;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000411 RID: 1041
	public class Asn1KeyWrapper : IKeyWrapper
	{
		// Token: 0x06002165 RID: 8549 RVA: 0x000C1FC0 File Offset: 0x000C1FC0
		public Asn1KeyWrapper(string algorithm, X509Certificate cert)
		{
			this.algorithm = algorithm;
			this.wrapper = KeyWrapperUtil.WrapperForName(algorithm, cert.GetPublicKey());
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x000C1FE4 File Offset: 0x000C1FE4
		public object AlgorithmDetails
		{
			get
			{
				return this.wrapper.AlgorithmDetails;
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000C1FF4 File Offset: 0x000C1FF4
		public IBlockResult Wrap(byte[] keyData)
		{
			return this.wrapper.Wrap(keyData);
		}

		// Token: 0x040015B4 RID: 5556
		private string algorithm;

		// Token: 0x040015B5 RID: 5557
		private IKeyWrapper wrapper;
	}
}
