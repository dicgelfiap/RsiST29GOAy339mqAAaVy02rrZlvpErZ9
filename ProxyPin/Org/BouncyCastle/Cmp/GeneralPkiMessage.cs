using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CB RID: 715
	public class GeneralPkiMessage
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x00072A0C File Offset: 0x00072A0C
		private static PkiMessage ParseBytes(byte[] encoding)
		{
			return PkiMessage.GetInstance(Asn1Object.FromByteArray(encoding));
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00072A1C File Offset: 0x00072A1C
		public GeneralPkiMessage(PkiMessage pkiMessage)
		{
			this.pkiMessage = pkiMessage;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00072A2C File Offset: 0x00072A2C
		public GeneralPkiMessage(byte[] encoding) : this(GeneralPkiMessage.ParseBytes(encoding))
		{
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00072A3C File Offset: 0x00072A3C
		public PkiHeader Header
		{
			get
			{
				return this.pkiMessage.Header;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00072A4C File Offset: 0x00072A4C
		public PkiBody Body
		{
			get
			{
				return this.pkiMessage.Body;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00072A5C File Offset: 0x00072A5C
		public bool HasProtection
		{
			get
			{
				return this.pkiMessage.Protection != null;
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00072A70 File Offset: 0x00072A70
		public PkiMessage ToAsn1Structure()
		{
			return this.pkiMessage;
		}

		// Token: 0x04000EE5 RID: 3813
		private readonly PkiMessage pkiMessage;
	}
}
