using System;
using Org.BouncyCastle.Bcpg.Sig;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000668 RID: 1640
	public class PgpSignatureSubpacketVector
	{
		// Token: 0x06003972 RID: 14706 RVA: 0x0013481C File Offset: 0x0013481C
		internal PgpSignatureSubpacketVector(SignatureSubpacket[] packets)
		{
			this.packets = packets;
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x0013482C File Offset: 0x0013482C
		public SignatureSubpacket GetSubpacket(SignatureSubpacketTag type)
		{
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (this.packets[num].SubpacketType == type)
				{
					return this.packets[num];
				}
			}
			return null;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00134878 File Offset: 0x00134878
		public bool HasSubpacket(SignatureSubpacketTag type)
		{
			return this.GetSubpacket(type) != null;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x00134888 File Offset: 0x00134888
		public SignatureSubpacket[] GetSubpackets(SignatureSubpacketTag type)
		{
			int num = 0;
			for (int i = 0; i < this.packets.Length; i++)
			{
				if (this.packets[i].SubpacketType == type)
				{
					num++;
				}
			}
			SignatureSubpacket[] array = new SignatureSubpacket[num];
			int num2 = 0;
			for (int j = 0; j < this.packets.Length; j++)
			{
				if (this.packets[j].SubpacketType == type)
				{
					array[num2++] = this.packets[j];
				}
			}
			return array;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x0013491C File Offset: 0x0013491C
		public NotationData[] GetNotationDataOccurrences()
		{
			SignatureSubpacket[] subpackets = this.GetSubpackets(SignatureSubpacketTag.NotationData);
			NotationData[] array = new NotationData[subpackets.Length];
			for (int i = 0; i < subpackets.Length; i++)
			{
				array[i] = (NotationData)subpackets[i];
			}
			return array;
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00134964 File Offset: 0x00134964
		[Obsolete("Use 'GetNotationDataOccurrences' instead")]
		public NotationData[] GetNotationDataOccurences()
		{
			return this.GetNotationDataOccurrences();
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x0013496C File Offset: 0x0013496C
		public long GetIssuerKeyId()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.IssuerKeyId);
			if (subpacket != null)
			{
				return ((IssuerKeyId)subpacket).KeyId;
			}
			return 0L;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x0013499C File Offset: 0x0013499C
		public bool HasSignatureCreationTime()
		{
			return this.GetSubpacket(SignatureSubpacketTag.CreationTime) != null;
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x001349AC File Offset: 0x001349AC
		public DateTime GetSignatureCreationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.CreationTime);
			if (subpacket == null)
			{
				throw new PgpException("SignatureCreationTime not available");
			}
			return ((SignatureCreationTime)subpacket).GetTime();
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x001349E4 File Offset: 0x001349E4
		public long GetSignatureExpirationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.ExpireTime);
			if (subpacket != null)
			{
				return ((SignatureExpirationTime)subpacket).Time;
			}
			return 0L;
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00134A14 File Offset: 0x00134A14
		public long GetKeyExpirationTime()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.KeyExpireTime);
			if (subpacket != null)
			{
				return ((KeyExpirationTime)subpacket).Time;
			}
			return 0L;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00134A44 File Offset: 0x00134A44
		public int[] GetPreferredHashAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredHashAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00134A74 File Offset: 0x00134A74
		public int[] GetPreferredSymmetricAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredSymmetricAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x00134AA4 File Offset: 0x00134AA4
		public int[] GetPreferredCompressionAlgorithms()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.PreferredCompressionAlgorithms);
			if (subpacket != null)
			{
				return ((PreferredAlgorithms)subpacket).GetPreferences();
			}
			return null;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00134AD4 File Offset: 0x00134AD4
		public int GetKeyFlags()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.KeyFlags);
			if (subpacket != null)
			{
				return ((KeyFlags)subpacket).Flags;
			}
			return 0;
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00134B04 File Offset: 0x00134B04
		public string GetSignerUserId()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.SignerUserId);
			if (subpacket != null)
			{
				return ((SignerUserId)subpacket).GetId();
			}
			return null;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00134B34 File Offset: 0x00134B34
		public bool IsPrimaryUserId()
		{
			PrimaryUserId primaryUserId = (PrimaryUserId)this.GetSubpacket(SignatureSubpacketTag.PrimaryUserId);
			return primaryUserId != null && primaryUserId.IsPrimaryUserId();
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00134B64 File Offset: 0x00134B64
		public SignatureSubpacketTag[] GetCriticalTags()
		{
			int num = 0;
			for (int num2 = 0; num2 != this.packets.Length; num2++)
			{
				if (this.packets[num2].IsCritical())
				{
					num++;
				}
			}
			SignatureSubpacketTag[] array = new SignatureSubpacketTag[num];
			num = 0;
			for (int num3 = 0; num3 != this.packets.Length; num3++)
			{
				if (this.packets[num3].IsCritical())
				{
					array[num++] = this.packets[num3].SubpacketType;
				}
			}
			return array;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x00134BF8 File Offset: 0x00134BF8
		public Features GetFeatures()
		{
			SignatureSubpacket subpacket = this.GetSubpacket(SignatureSubpacketTag.Features);
			if (subpacket == null)
			{
				return null;
			}
			return new Features(subpacket.IsCritical(), subpacket.IsLongLength(), subpacket.GetData());
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x00134C34 File Offset: 0x00134C34
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.packets.Length;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x00134C40 File Offset: 0x00134C40
		public int Count
		{
			get
			{
				return this.packets.Length;
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00134C4C File Offset: 0x00134C4C
		internal SignatureSubpacket[] ToSubpacketArray()
		{
			return this.packets;
		}

		// Token: 0x04001E0B RID: 7691
		private readonly SignatureSubpacket[] packets;
	}
}
