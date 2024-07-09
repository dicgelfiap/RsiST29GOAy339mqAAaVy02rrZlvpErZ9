using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200028D RID: 653
	public class RevocationKey : SignatureSubpacket
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0006DD18 File Offset: 0x0006DD18
		public RevocationKey(bool isCritical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.RevocationKey, isCritical, isLongLength, data)
		{
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0006DD28 File Offset: 0x0006DD28
		public RevocationKey(bool isCritical, RevocationKeyTag signatureClass, PublicKeyAlgorithmTag keyAlgorithm, byte[] fingerprint) : base(SignatureSubpacketTag.RevocationKey, isCritical, false, RevocationKey.CreateData(signatureClass, keyAlgorithm, fingerprint))
		{
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0006DD40 File Offset: 0x0006DD40
		private static byte[] CreateData(RevocationKeyTag signatureClass, PublicKeyAlgorithmTag keyAlgorithm, byte[] fingerprint)
		{
			byte[] array = new byte[2 + fingerprint.Length];
			array[0] = (byte)signatureClass;
			array[1] = (byte)keyAlgorithm;
			Array.Copy(fingerprint, 0, array, 2, fingerprint.Length);
			return array;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0006DD74 File Offset: 0x0006DD74
		public virtual RevocationKeyTag SignatureClass
		{
			get
			{
				return (RevocationKeyTag)base.GetData()[0];
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0006DD80 File Offset: 0x0006DD80
		public virtual PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return (PublicKeyAlgorithmTag)base.GetData()[1];
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0006DD8C File Offset: 0x0006DD8C
		public virtual byte[] GetFingerprint()
		{
			byte[] data = base.GetData();
			byte[] array = new byte[data.Length - 2];
			Array.Copy(data, 2, array, 0, array.Length);
			return array;
		}
	}
}
