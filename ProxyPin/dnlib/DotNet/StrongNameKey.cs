using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000856 RID: 2134
	[ComVisible(true)]
	public sealed class StrongNameKey
	{
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x0018FEBC File Offset: 0x0018FEBC
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					Interlocked.CompareExchange<byte[]>(ref this.publicKey, this.CreatePublicKey(), null);
				}
				return this.publicKey;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x0018FEE4 File Offset: 0x0018FEE4
		public int SignatureSize
		{
			get
			{
				return this.modulus.Length;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005084 RID: 20612 RVA: 0x0018FEF0 File Offset: 0x0018FEF0
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashAlg;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005085 RID: 20613 RVA: 0x0018FEF8 File Offset: 0x0018FEF8
		public byte[] PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x0018FF00 File Offset: 0x0018FF00
		public byte[] Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x0018FF08 File Offset: 0x0018FF08
		public byte[] Prime1
		{
			get
			{
				return this.prime1;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x0018FF10 File Offset: 0x0018FF10
		public byte[] Prime2
		{
			get
			{
				return this.prime2;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x0018FF18 File Offset: 0x0018FF18
		public byte[] Exponent1
		{
			get
			{
				return this.exponent1;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x0018FF20 File Offset: 0x0018FF20
		public byte[] Exponent2
		{
			get
			{
				return this.exponent2;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x0018FF28 File Offset: 0x0018FF28
		public byte[] Coefficient
		{
			get
			{
				return this.coefficient;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x0018FF30 File Offset: 0x0018FF30
		public byte[] PrivateExponent
		{
			get
			{
				return this.privateExponent;
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x0018FF38 File Offset: 0x0018FF38
		public StrongNameKey(byte[] keyData) : this(new BinaryReader(new MemoryStream(keyData)))
		{
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0018FF4C File Offset: 0x0018FF4C
		public StrongNameKey(string filename) : this(File.ReadAllBytes(filename))
		{
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0018FF5C File Offset: 0x0018FF5C
		public StrongNameKey(Stream stream) : this(new BinaryReader(stream))
		{
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0018FF6C File Offset: 0x0018FF6C
		public StrongNameKey(BinaryReader reader)
		{
			try
			{
				this.publicKey = null;
				if (reader.ReadByte() != 7)
				{
					throw new InvalidKeyException("Not a public/private key pair");
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
				if (reader.ReadUInt32() != 843141970U)
				{
					throw new InvalidKeyException("Invalid RSA2 magic");
				}
				uint num = reader.ReadUInt32();
				this.publicExponent = reader.ReadBytesReverse(4);
				int len = (int)(num / 8U);
				int len2 = (int)(num / 16U);
				this.modulus = reader.ReadBytesReverse(len);
				this.prime1 = reader.ReadBytesReverse(len2);
				this.prime2 = reader.ReadBytesReverse(len2);
				this.exponent1 = reader.ReadBytesReverse(len2);
				this.exponent2 = reader.ReadBytesReverse(len2);
				this.coefficient = reader.ReadBytesReverse(len2);
				this.privateExponent = reader.ReadBytesReverse(len);
			}
			catch (IOException innerException)
			{
				throw new InvalidKeyException("Couldn't read strong name key", innerException);
			}
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0019008C File Offset: 0x0019008C
		private StrongNameKey(AssemblyHashAlgorithm hashAlg, byte[] publicExponent, byte[] modulus, byte[] prime1, byte[] prime2, byte[] exponent1, byte[] exponent2, byte[] coefficient, byte[] privateExponent)
		{
			this.hashAlg = hashAlg;
			this.publicExponent = publicExponent;
			this.modulus = modulus;
			this.prime1 = prime1;
			this.prime2 = prime2;
			this.exponent1 = exponent1;
			this.exponent2 = exponent2;
			this.coefficient = coefficient;
			this.privateExponent = privateExponent;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x001900E8 File Offset: 0x001900E8
		public StrongNameKey WithHashAlgorithm(AssemblyHashAlgorithm hashAlgorithm)
		{
			if (this.hashAlg == hashAlgorithm)
			{
				return this;
			}
			return new StrongNameKey(hashAlgorithm, this.publicExponent, this.modulus, this.prime1, this.prime2, this.exponent1, this.exponent2, this.coefficient, this.privateExponent);
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00190140 File Offset: 0x00190140
		private byte[] CreatePublicKey()
		{
			AssemblyHashAlgorithm assemblyHashAlgorithm = (this.hashAlg == AssemblyHashAlgorithm.None) ? AssemblyHashAlgorithm.SHA1 : this.hashAlg;
			return StrongNamePublicKey.CreatePublicKey(SignatureAlgorithm.CALG_RSA_SIGN, assemblyHashAlgorithm, this.modulus, this.publicExponent);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00190184 File Offset: 0x00190184
		public RSA CreateRSA()
		{
			RSAParameters parameters = new RSAParameters
			{
				Exponent = this.publicExponent,
				Modulus = this.modulus,
				P = this.prime1,
				Q = this.prime2,
				DP = this.exponent1,
				DQ = this.exponent2,
				InverseQ = this.coefficient,
				D = this.privateExponent
			};
			RSA rsa = RSA.Create();
			RSA result;
			try
			{
				rsa.ImportParameters(parameters);
				result = rsa;
			}
			catch
			{
				((IDisposable)rsa).Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00190234 File Offset: 0x00190234
		public byte[] CreateStrongName()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(7);
			binaryWriter.Write(2);
			binaryWriter.Write(0);
			binaryWriter.Write(9216U);
			binaryWriter.Write(843141970U);
			binaryWriter.Write(this.modulus.Length * 8);
			binaryWriter.WriteReverse(this.publicExponent);
			binaryWriter.WriteReverse(this.modulus);
			binaryWriter.WriteReverse(this.prime1);
			binaryWriter.WriteReverse(this.prime2);
			binaryWriter.WriteReverse(this.exponent1);
			binaryWriter.WriteReverse(this.exponent2);
			binaryWriter.WriteReverse(this.coefficient);
			binaryWriter.WriteReverse(this.privateExponent);
			return memoryStream.ToArray();
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x001902F0 File Offset: 0x001902F0
		public static string CreateCounterSignatureAsString(StrongNamePublicKey identityPubKey, StrongNameKey identityKey, StrongNamePublicKey signaturePubKey)
		{
			return Utils.ToHex(StrongNameKey.CreateCounterSignature(identityPubKey, identityKey, signaturePubKey), false);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00190300 File Offset: 0x00190300
		public static byte[] CreateCounterSignature(StrongNamePublicKey identityPubKey, StrongNameKey identityKey, StrongNamePublicKey signaturePubKey)
		{
			byte[] rgbHash = AssemblyHash.Hash(signaturePubKey.CreatePublicKey(), identityPubKey.HashAlgorithm);
			byte[] result;
			using (RSA rsa = identityKey.CreateRSA())
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
				string name = identityPubKey.HashAlgorithm.GetName();
				rsapkcs1SignatureFormatter.SetHashAlgorithm(name);
				byte[] array = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
				Array.Reverse(array);
				result = array;
			}
			return result;
		}

		// Token: 0x0400275E RID: 10078
		private const uint RSA2_SIG = 843141970U;

		// Token: 0x0400275F RID: 10079
		private byte[] publicKey;

		// Token: 0x04002760 RID: 10080
		private readonly AssemblyHashAlgorithm hashAlg;

		// Token: 0x04002761 RID: 10081
		private readonly byte[] publicExponent;

		// Token: 0x04002762 RID: 10082
		private readonly byte[] modulus;

		// Token: 0x04002763 RID: 10083
		private readonly byte[] prime1;

		// Token: 0x04002764 RID: 10084
		private readonly byte[] prime2;

		// Token: 0x04002765 RID: 10085
		private readonly byte[] exponent1;

		// Token: 0x04002766 RID: 10086
		private readonly byte[] exponent2;

		// Token: 0x04002767 RID: 10087
		private readonly byte[] coefficient;

		// Token: 0x04002768 RID: 10088
		private readonly byte[] privateExponent;
	}
}
