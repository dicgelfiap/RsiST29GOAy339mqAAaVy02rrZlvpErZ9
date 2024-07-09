using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A4 RID: 676
	public class ECDHPublicBcpgKey : ECPublicBcpgKey
	{
		// Token: 0x06001509 RID: 5385 RVA: 0x000700E8 File Offset: 0x000700E8
		public ECDHPublicBcpgKey(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			int num = bcpgIn.ReadByte();
			byte[] array = new byte[num];
			if (array.Length != 3)
			{
				throw new InvalidOperationException("kdf parameters size of 3 expected.");
			}
			bcpgIn.ReadFully(array);
			this.reserved = array[0];
			this.hashFunctionId = (HashAlgorithmTag)array[1];
			this.symAlgorithmId = (SymmetricKeyAlgorithmTag)array[2];
			this.VerifyHashAlgorithm();
			this.VerifySymmetricKeyAlgorithm();
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00070150 File Offset: 0x00070150
		public ECDHPublicBcpgKey(DerObjectIdentifier oid, ECPoint point, HashAlgorithmTag hashAlgorithm, SymmetricKeyAlgorithmTag symmetricKeyAlgorithm) : base(oid, point)
		{
			this.reserved = 1;
			this.hashFunctionId = hashAlgorithm;
			this.symAlgorithmId = symmetricKeyAlgorithm;
			this.VerifyHashAlgorithm();
			this.VerifySymmetricKeyAlgorithm();
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0007017C File Offset: 0x0007017C
		public virtual byte Reserved
		{
			get
			{
				return this.reserved;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00070184 File Offset: 0x00070184
		public virtual HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.hashFunctionId;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0007018C File Offset: 0x0007018C
		public virtual SymmetricKeyAlgorithmTag SymmetricKeyAlgorithm
		{
			get
			{
				return this.symAlgorithmId;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00070194 File Offset: 0x00070194
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			base.Encode(bcpgOut);
			bcpgOut.WriteByte(3);
			bcpgOut.WriteByte(this.reserved);
			bcpgOut.WriteByte((byte)this.hashFunctionId);
			bcpgOut.WriteByte((byte)this.symAlgorithmId);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000701DC File Offset: 0x000701DC
		private void VerifyHashAlgorithm()
		{
			switch (this.hashFunctionId)
			{
			case HashAlgorithmTag.Sha256:
			case HashAlgorithmTag.Sha384:
			case HashAlgorithmTag.Sha512:
				return;
			default:
				throw new InvalidOperationException("Hash algorithm must be SHA-256 or stronger.");
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00070214 File Offset: 0x00070214
		private void VerifySymmetricKeyAlgorithm()
		{
			switch (this.symAlgorithmId)
			{
			case SymmetricKeyAlgorithmTag.Aes128:
			case SymmetricKeyAlgorithmTag.Aes192:
			case SymmetricKeyAlgorithmTag.Aes256:
				return;
			default:
				throw new InvalidOperationException("Symmetric key algorithm must be AES-128 or stronger.");
			}
		}

		// Token: 0x04000E26 RID: 3622
		private byte reserved;

		// Token: 0x04000E27 RID: 3623
		private HashAlgorithmTag hashFunctionId;

		// Token: 0x04000E28 RID: 3624
		private SymmetricKeyAlgorithmTag symAlgorithmId;
	}
}
