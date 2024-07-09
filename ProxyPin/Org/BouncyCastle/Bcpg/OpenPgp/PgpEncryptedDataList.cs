using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064D RID: 1613
	public class PgpEncryptedDataList : PgpObject
	{
		// Token: 0x06003816 RID: 14358 RVA: 0x0012DD1C File Offset: 0x0012DD1C
		public PgpEncryptedDataList(BcpgInputStream bcpgInput)
		{
			while (bcpgInput.NextPacketTag() == PacketTag.PublicKeyEncryptedSession || bcpgInput.NextPacketTag() == PacketTag.SymmetricKeyEncryptedSessionKey)
			{
				this.list.Add(bcpgInput.ReadPacket());
			}
			Packet packet = bcpgInput.ReadPacket();
			if (!(packet is InputStreamPacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			this.data = (InputStreamPacket)packet;
			for (int num = 0; num != this.list.Count; num++)
			{
				if (this.list[num] is SymmetricKeyEncSessionPacket)
				{
					this.list[num] = new PgpPbeEncryptedData((SymmetricKeyEncSessionPacket)this.list[num], this.data);
				}
				else
				{
					this.list[num] = new PgpPublicKeyEncryptedData((PublicKeyEncSessionPacket)this.list[num], this.data);
				}
			}
		}

		// Token: 0x170009BC RID: 2492
		public PgpEncryptedData this[int index]
		{
			get
			{
				return (PgpEncryptedData)this.list[index];
			}
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0012DE30 File Offset: 0x0012DE30
		[Obsolete("Use 'object[index]' syntax instead")]
		public object Get(int index)
		{
			return this[index];
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x0012DE3C File Offset: 0x0012DE3C
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x0012DE4C File Offset: 0x0012DE4C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x0012DE5C File Offset: 0x0012DE5C
		public bool IsEmpty
		{
			get
			{
				return this.list.Count == 0;
			}
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0012DE6C File Offset: 0x0012DE6C
		public IEnumerable GetEncryptedDataObjects()
		{
			return new EnumerableProxy(this.list);
		}

		// Token: 0x04001DAB RID: 7595
		private readonly IList list = Platform.CreateArrayList();

		// Token: 0x04001DAC RID: 7596
		private readonly InputStreamPacket data;
	}
}
