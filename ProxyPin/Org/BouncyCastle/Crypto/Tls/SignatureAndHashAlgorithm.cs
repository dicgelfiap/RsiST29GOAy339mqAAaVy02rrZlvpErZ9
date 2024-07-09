using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051E RID: 1310
	public class SignatureAndHashAlgorithm
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x000D6EA8 File Offset: 0x000D6EA8
		public SignatureAndHashAlgorithm(byte hash, byte signature)
		{
			if (!TlsUtilities.IsValidUint8((int)hash))
			{
				throw new ArgumentException("should be a uint8", "hash");
			}
			if (!TlsUtilities.IsValidUint8((int)signature))
			{
				throw new ArgumentException("should be a uint8", "signature");
			}
			if (signature == 0)
			{
				throw new ArgumentException("MUST NOT be \"anonymous\"", "signature");
			}
			this.mHash = hash;
			this.mSignature = signature;
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000D6F1C File Offset: 0x000D6F1C
		public virtual byte Hash
		{
			get
			{
				return this.mHash;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000D6F24 File Offset: 0x000D6F24
		public virtual byte Signature
		{
			get
			{
				return this.mSignature;
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000D6F2C File Offset: 0x000D6F2C
		public override bool Equals(object obj)
		{
			if (!(obj is SignatureAndHashAlgorithm))
			{
				return false;
			}
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
			return signatureAndHashAlgorithm.Hash == this.Hash && signatureAndHashAlgorithm.Signature == this.Signature;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000D6F74 File Offset: 0x000D6F74
		public override int GetHashCode()
		{
			return (int)this.Hash << 16 | (int)this.Signature;
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000D6F88 File Offset: 0x000D6F88
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.Hash, output);
			TlsUtilities.WriteUint8(this.Signature, output);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000D6FA4 File Offset: 0x000D6FA4
		public static SignatureAndHashAlgorithm Parse(Stream input)
		{
			byte hash = TlsUtilities.ReadUint8(input);
			byte signature = TlsUtilities.ReadUint8(input);
			return new SignatureAndHashAlgorithm(hash, signature);
		}

		// Token: 0x04001A50 RID: 6736
		protected readonly byte mHash;

		// Token: 0x04001A51 RID: 6737
		protected readonly byte mSignature;
	}
}
