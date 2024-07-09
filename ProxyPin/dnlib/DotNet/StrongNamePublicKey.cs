using System;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000855 RID: 2133
	[ComVisible(true)]
	public sealed class StrongNamePublicKey
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0018FC60 File Offset: 0x0018FC60
		public SignatureAlgorithm SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x0018FC68 File Offset: 0x0018FC68
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x0018FC70 File Offset: 0x0018FC70
		public byte[] Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x0018FC78 File Offset: 0x0018FC78
		public byte[] PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0018FC80 File Offset: 0x0018FC80
		public StrongNamePublicKey()
		{
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0018FC88 File Offset: 0x0018FC88
		public StrongNamePublicKey(byte[] modulus, byte[] publicExponent) : this(modulus, publicExponent, AssemblyHashAlgorithm.SHA1, SignatureAlgorithm.CALG_RSA_SIGN)
		{
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0018FC9C File Offset: 0x0018FC9C
		public StrongNamePublicKey(byte[] modulus, byte[] publicExponent, AssemblyHashAlgorithm hashAlgorithm) : this(modulus, publicExponent, hashAlgorithm, SignatureAlgorithm.CALG_RSA_SIGN)
		{
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x0018FCAC File Offset: 0x0018FCAC
		public StrongNamePublicKey(byte[] modulus, byte[] publicExponent, AssemblyHashAlgorithm hashAlgorithm, SignatureAlgorithm signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.modulus = modulus;
			this.publicExponent = publicExponent;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x0018FCD4 File Offset: 0x0018FCD4
		public StrongNamePublicKey(PublicKey pk) : this(pk.Data)
		{
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0018FCE4 File Offset: 0x0018FCE4
		public StrongNamePublicKey(byte[] pk) : this(new BinaryReader(new MemoryStream(pk)))
		{
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0018FCF8 File Offset: 0x0018FCF8
		public StrongNamePublicKey(string filename) : this(File.ReadAllBytes(filename))
		{
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x0018FD08 File Offset: 0x0018FD08
		public StrongNamePublicKey(Stream stream) : this(new BinaryReader(stream))
		{
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0018FD18 File Offset: 0x0018FD18
		public StrongNamePublicKey(BinaryReader reader)
		{
			try
			{
				this.signatureAlgorithm = (SignatureAlgorithm)reader.ReadUInt32();
				this.hashAlgorithm = (AssemblyHashAlgorithm)reader.ReadUInt32();
				reader.ReadInt32();
				if (reader.ReadByte() != 6)
				{
					throw new InvalidKeyException("Not a public key");
				}
				if (reader.ReadByte() != 2)
				{
					throw new InvalidKeyException("Invalid version");
				}
				reader.ReadUInt16();
				if (reader.ReadUInt32() != 9216U)
				{
					throw new InvalidKeyException("Not RSA sign");
				}
				if (reader.ReadUInt32() != 826364754U)
				{
					throw new InvalidKeyException("Invalid RSA1 magic");
				}
				uint num = reader.ReadUInt32();
				this.publicExponent = reader.ReadBytesReverse(4);
				this.modulus = reader.ReadBytesReverse((int)(num / 8U));
			}
			catch (IOException innerException)
			{
				throw new InvalidKeyException("Invalid public key", innerException);
			}
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0018FDFC File Offset: 0x0018FDFC
		public byte[] CreatePublicKey()
		{
			return StrongNamePublicKey.CreatePublicKey(this.signatureAlgorithm, this.hashAlgorithm, this.modulus, this.publicExponent);
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0018FE1C File Offset: 0x0018FE1C
		internal static byte[] CreatePublicKey(SignatureAlgorithm sigAlg, AssemblyHashAlgorithm hashAlg, byte[] modulus, byte[] publicExponent)
		{
			if (sigAlg != SignatureAlgorithm.CALG_RSA_SIGN)
			{
				throw new ArgumentException("Signature algorithm must be RSA");
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write((uint)sigAlg);
			binaryWriter.Write((uint)hashAlg);
			binaryWriter.Write(20 + modulus.Length);
			binaryWriter.Write(6);
			binaryWriter.Write(2);
			binaryWriter.Write(0);
			binaryWriter.Write((uint)sigAlg);
			binaryWriter.Write(826364754U);
			binaryWriter.Write(modulus.Length * 8);
			binaryWriter.WriteReverse(publicExponent);
			binaryWriter.WriteReverse(modulus);
			return memoryStream.ToArray();
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0018FEAC File Offset: 0x0018FEAC
		public override string ToString()
		{
			return Utils.ToHex(this.CreatePublicKey(), false);
		}

		// Token: 0x04002759 RID: 10073
		private const uint RSA1_SIG = 826364754U;

		// Token: 0x0400275A RID: 10074
		private readonly SignatureAlgorithm signatureAlgorithm;

		// Token: 0x0400275B RID: 10075
		private readonly AssemblyHashAlgorithm hashAlgorithm;

		// Token: 0x0400275C RID: 10076
		private readonly byte[] modulus;

		// Token: 0x0400275D RID: 10077
		private readonly byte[] publicExponent;
	}
}
