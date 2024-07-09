using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000651 RID: 1617
	public abstract class PgpKeyRing : PgpObject
	{
		// Token: 0x06003825 RID: 14373 RVA: 0x0012DF48 File Offset: 0x0012DF48
		internal PgpKeyRing()
		{
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0012DF50 File Offset: 0x0012DF50
		internal static TrustPacket ReadOptionalTrustPacket(BcpgInputStream bcpgInput)
		{
			if (bcpgInput.NextPacketTag() != PacketTag.Trust)
			{
				return null;
			}
			return (TrustPacket)bcpgInput.ReadPacket();
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x0012DF6C File Offset: 0x0012DF6C
		internal static IList ReadSignaturesAndTrust(BcpgInputStream bcpgInput)
		{
			IList result;
			try
			{
				IList list = Platform.CreateArrayList();
				while (bcpgInput.NextPacketTag() == PacketTag.Signature)
				{
					SignaturePacket sigPacket = (SignaturePacket)bcpgInput.ReadPacket();
					TrustPacket trustPacket = PgpKeyRing.ReadOptionalTrustPacket(bcpgInput);
					list.Add(new PgpSignature(sigPacket, trustPacket));
				}
				result = list;
			}
			catch (PgpException ex)
			{
				throw new IOException("can't create signature object: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x0012DFE4 File Offset: 0x0012DFE4
		internal static void ReadUserIDs(BcpgInputStream bcpgInput, out IList ids, out IList idTrusts, out IList idSigs)
		{
			ids = Platform.CreateArrayList();
			idTrusts = Platform.CreateArrayList();
			idSigs = Platform.CreateArrayList();
			while (bcpgInput.NextPacketTag() == PacketTag.UserId || bcpgInput.NextPacketTag() == PacketTag.UserAttribute)
			{
				Packet packet = bcpgInput.ReadPacket();
				if (packet is UserIdPacket)
				{
					UserIdPacket userIdPacket = (UserIdPacket)packet;
					ids.Add(userIdPacket.GetId());
				}
				else
				{
					UserAttributePacket userAttributePacket = (UserAttributePacket)packet;
					ids.Add(new PgpUserAttributeSubpacketVector(userAttributePacket.GetSubpackets()));
				}
				idTrusts.Add(PgpKeyRing.ReadOptionalTrustPacket(bcpgInput));
				idSigs.Add(PgpKeyRing.ReadSignaturesAndTrust(bcpgInput));
			}
		}
	}
}
