using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B6 RID: 438
	public class RC2CbcParameter : Asn1Encodable
	{
		// Token: 0x06000E3E RID: 3646 RVA: 0x00056F80 File Offset: 0x00056F80
		public static RC2CbcParameter GetInstance(object obj)
		{
			if (obj is Asn1Sequence)
			{
				return new RC2CbcParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00056FB4 File Offset: 0x00056FB4
		public RC2CbcParameter(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00056FC8 File Offset: 0x00056FC8
		public RC2CbcParameter(int parameterVersion, byte[] iv)
		{
			this.version = new DerInteger(parameterVersion);
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00056FE8 File Offset: 0x00056FE8
		private RC2CbcParameter(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
				return;
			}
			this.version = (DerInteger)seq[0];
			this.iv = (Asn1OctetString)seq[1];
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00057044 File Offset: 0x00057044
		public BigInteger RC2ParameterVersion
		{
			get
			{
				if (this.version != null)
				{
					return this.version.Value;
				}
				return null;
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00057060 File Offset: 0x00057060
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00057074 File Offset: 0x00057074
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.version
			});
			asn1EncodableVector.Add(this.iv);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000A8A RID: 2698
		internal DerInteger version;

		// Token: 0x04000A8B RID: 2699
		internal Asn1OctetString iv;
	}
}
