using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050F RID: 1295
	public class NewSessionTicket
	{
		// Token: 0x06002771 RID: 10097 RVA: 0x000D5AB8 File Offset: 0x000D5AB8
		public NewSessionTicket(long ticketLifetimeHint, byte[] ticket)
		{
			this.mTicketLifetimeHint = ticketLifetimeHint;
			this.mTicket = ticket;
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000D5AD0 File Offset: 0x000D5AD0
		public virtual long TicketLifetimeHint
		{
			get
			{
				return this.mTicketLifetimeHint;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000D5AD8 File Offset: 0x000D5AD8
		public virtual byte[] Ticket
		{
			get
			{
				return this.mTicket;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000D5AE0 File Offset: 0x000D5AE0
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint32(this.mTicketLifetimeHint, output);
			TlsUtilities.WriteOpaque16(this.mTicket, output);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000D5AFC File Offset: 0x000D5AFC
		public static NewSessionTicket Parse(Stream input)
		{
			long ticketLifetimeHint = TlsUtilities.ReadUint32(input);
			byte[] ticket = TlsUtilities.ReadOpaque16(input);
			return new NewSessionTicket(ticketLifetimeHint, ticket);
		}

		// Token: 0x04001A03 RID: 6659
		protected readonly long mTicketLifetimeHint;

		// Token: 0x04001A04 RID: 6660
		protected readonly byte[] mTicket;
	}
}
