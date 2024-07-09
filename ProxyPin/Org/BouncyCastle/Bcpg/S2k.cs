using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B8 RID: 696
	public class S2k : BcpgObject
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x00071164 File Offset: 0x00071164
		internal S2k(Stream inStr)
		{
			this.type = inStr.ReadByte();
			this.algorithm = (HashAlgorithmTag)inStr.ReadByte();
			if (this.type != 101)
			{
				if (this.type != 0)
				{
					this.iv = new byte[8];
					if (Streams.ReadFully(inStr, this.iv, 0, this.iv.Length) < this.iv.Length)
					{
						throw new EndOfStreamException();
					}
					if (this.type == 3)
					{
						this.itCount = inStr.ReadByte();
						return;
					}
				}
			}
			else
			{
				inStr.ReadByte();
				inStr.ReadByte();
				inStr.ReadByte();
				this.protectionMode = inStr.ReadByte();
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00071228 File Offset: 0x00071228
		public S2k(HashAlgorithmTag algorithm)
		{
			this.type = 0;
			this.algorithm = algorithm;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0007124C File Offset: 0x0007124C
		public S2k(HashAlgorithmTag algorithm, byte[] iv)
		{
			this.type = 1;
			this.algorithm = algorithm;
			this.iv = iv;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00071278 File Offset: 0x00071278
		public S2k(HashAlgorithmTag algorithm, byte[] iv, int itCount)
		{
			this.type = 3;
			this.algorithm = algorithm;
			this.iv = iv;
			this.itCount = itCount;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000712AC File Offset: 0x000712AC
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x000712B4 File Offset: 0x000712B4
		public virtual HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000712BC File Offset: 0x000712BC
		public virtual byte[] GetIV()
		{
			return Arrays.Clone(this.iv);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000712CC File Offset: 0x000712CC
		[Obsolete("Use 'IterationCount' property instead")]
		public long GetIterationCount()
		{
			return this.IterationCount;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000712D4 File Offset: 0x000712D4
		public virtual long IterationCount
		{
			get
			{
				return (long)((long)(16 + (this.itCount & 15)) << (this.itCount >> 4) + 6);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000712F4 File Offset: 0x000712F4
		public virtual int ProtectionMode
		{
			get
			{
				return this.protectionMode;
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000712FC File Offset: 0x000712FC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteByte((byte)this.type);
			bcpgOut.WriteByte((byte)this.algorithm);
			if (this.type != 101)
			{
				if (this.type != 0)
				{
					bcpgOut.Write(this.iv);
				}
				if (this.type == 3)
				{
					bcpgOut.WriteByte((byte)this.itCount);
					return;
				}
			}
			else
			{
				bcpgOut.WriteByte(71);
				bcpgOut.WriteByte(78);
				bcpgOut.WriteByte(85);
				bcpgOut.WriteByte((byte)this.protectionMode);
			}
		}

		// Token: 0x04000E88 RID: 3720
		private const int ExpBias = 6;

		// Token: 0x04000E89 RID: 3721
		public const int Simple = 0;

		// Token: 0x04000E8A RID: 3722
		public const int Salted = 1;

		// Token: 0x04000E8B RID: 3723
		public const int SaltedAndIterated = 3;

		// Token: 0x04000E8C RID: 3724
		public const int GnuDummyS2K = 101;

		// Token: 0x04000E8D RID: 3725
		public const int GnuProtectionModeNoPrivateKey = 1;

		// Token: 0x04000E8E RID: 3726
		public const int GnuProtectionModeDivertToCard = 2;

		// Token: 0x04000E8F RID: 3727
		internal int type;

		// Token: 0x04000E90 RID: 3728
		internal HashAlgorithmTag algorithm;

		// Token: 0x04000E91 RID: 3729
		internal byte[] iv;

		// Token: 0x04000E92 RID: 3730
		internal int itCount = -1;

		// Token: 0x04000E93 RID: 3731
		internal int protectionMode = -1;
	}
}
