using System;
using System.Collections;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000667 RID: 1639
	public class PgpSignatureSubpacketGenerator
	{
		// Token: 0x0600395D RID: 14685 RVA: 0x00134570 File Offset: 0x00134570
		public void SetRevocable(bool isCritical, bool isRevocable)
		{
			this.list.Add(new Revocable(isCritical, isRevocable));
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x00134588 File Offset: 0x00134588
		public void SetExportable(bool isCritical, bool isExportable)
		{
			this.list.Add(new Exportable(isCritical, isExportable));
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x001345A0 File Offset: 0x001345A0
		public void SetFeature(bool isCritical, byte feature)
		{
			this.list.Add(new Features(isCritical, feature));
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x001345B8 File Offset: 0x001345B8
		public void SetTrust(bool isCritical, int depth, int trustAmount)
		{
			this.list.Add(new TrustSignature(isCritical, depth, trustAmount));
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x001345D0 File Offset: 0x001345D0
		public void SetKeyExpirationTime(bool isCritical, long seconds)
		{
			this.list.Add(new KeyExpirationTime(isCritical, seconds));
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001345E8 File Offset: 0x001345E8
		public void SetSignatureExpirationTime(bool isCritical, long seconds)
		{
			this.list.Add(new SignatureExpirationTime(isCritical, seconds));
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x00134600 File Offset: 0x00134600
		public void SetSignatureCreationTime(bool isCritical, DateTime date)
		{
			this.list.Add(new SignatureCreationTime(isCritical, date));
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x00134618 File Offset: 0x00134618
		public void SetPreferredHashAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredHashAlgorithms, isCritical, algorithms));
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x00134630 File Offset: 0x00134630
		public void SetPreferredSymmetricAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredSymmetricAlgorithms, isCritical, algorithms));
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x00134648 File Offset: 0x00134648
		public void SetPreferredCompressionAlgorithms(bool isCritical, int[] algorithms)
		{
			this.list.Add(new PreferredAlgorithms(SignatureSubpacketTag.PreferredCompressionAlgorithms, isCritical, algorithms));
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x00134660 File Offset: 0x00134660
		public void SetKeyFlags(bool isCritical, int flags)
		{
			this.list.Add(new KeyFlags(isCritical, flags));
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00134678 File Offset: 0x00134678
		public void SetSignerUserId(bool isCritical, string userId)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			this.list.Add(new SignerUserId(isCritical, userId));
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x001346A0 File Offset: 0x001346A0
		public void SetSignerUserId(bool isCritical, byte[] rawUserId)
		{
			if (rawUserId == null)
			{
				throw new ArgumentNullException("rawUserId");
			}
			this.list.Add(new SignerUserId(isCritical, false, rawUserId));
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x001346C8 File Offset: 0x001346C8
		public void SetEmbeddedSignature(bool isCritical, PgpSignature pgpSignature)
		{
			byte[] encoded = pgpSignature.GetEncoded();
			byte[] array;
			if (encoded.Length - 1 > 256)
			{
				array = new byte[encoded.Length - 3];
			}
			else
			{
				array = new byte[encoded.Length - 2];
			}
			Array.Copy(encoded, encoded.Length - array.Length, array, 0, array.Length);
			this.list.Add(new EmbeddedSignature(isCritical, false, array));
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00134730 File Offset: 0x00134730
		public void SetPrimaryUserId(bool isCritical, bool isPrimaryUserId)
		{
			this.list.Add(new PrimaryUserId(isCritical, isPrimaryUserId));
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00134748 File Offset: 0x00134748
		public void SetNotationData(bool isCritical, bool isHumanReadable, string notationName, string notationValue)
		{
			this.list.Add(new NotationData(isCritical, isHumanReadable, notationName, notationValue));
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00134760 File Offset: 0x00134760
		public void SetRevocationReason(bool isCritical, RevocationReasonTag reason, string description)
		{
			this.list.Add(new RevocationReason(isCritical, reason, description));
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00134778 File Offset: 0x00134778
		public void SetRevocationKey(bool isCritical, PublicKeyAlgorithmTag keyAlgorithm, byte[] fingerprint)
		{
			this.list.Add(new RevocationKey(isCritical, RevocationKeyTag.ClassDefault, keyAlgorithm, fingerprint));
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00134794 File Offset: 0x00134794
		public void SetIssuerKeyID(bool isCritical, long keyID)
		{
			this.list.Add(new IssuerKeyId(isCritical, keyID));
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x001347AC File Offset: 0x001347AC
		public PgpSignatureSubpacketVector Generate()
		{
			SignatureSubpacket[] array = new SignatureSubpacket[this.list.Count];
			for (int i = 0; i < this.list.Count; i++)
			{
				array[i] = (SignatureSubpacket)this.list[i];
			}
			return new PgpSignatureSubpacketVector(array);
		}

		// Token: 0x04001E0A RID: 7690
		private IList list = Platform.CreateArrayList();
	}
}
