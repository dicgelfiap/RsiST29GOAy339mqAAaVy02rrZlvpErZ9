using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000425 RID: 1061
	public class GenericKey
	{
		// Token: 0x060021A5 RID: 8613 RVA: 0x000C2DBC File Offset: 0x000C2DBC
		public GenericKey(object representation)
		{
			this.algorithmIdentifier = null;
			this.representation = representation;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000C2DD4 File Offset: 0x000C2DD4
		public GenericKey(AlgorithmIdentifier algorithmIdentifier, byte[] representation)
		{
			this.algorithmIdentifier = algorithmIdentifier;
			this.representation = representation;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000C2DEC File Offset: 0x000C2DEC
		public GenericKey(AlgorithmIdentifier algorithmIdentifier, object representation)
		{
			this.algorithmIdentifier = algorithmIdentifier;
			this.representation = representation;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x000C2E04 File Offset: 0x000C2E04
		public AlgorithmIdentifier AlgorithmIdentifier
		{
			get
			{
				return this.algorithmIdentifier;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000C2E0C File Offset: 0x000C2E0C
		public object Representation
		{
			get
			{
				return this.representation;
			}
		}

		// Token: 0x040015CE RID: 5582
		private readonly AlgorithmIdentifier algorithmIdentifier;

		// Token: 0x040015CF RID: 5583
		private readonly object representation;
	}
}
