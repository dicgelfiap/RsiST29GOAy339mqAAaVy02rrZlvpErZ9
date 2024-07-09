using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A3 RID: 675
	public abstract class ECPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x0006FF8C File Offset: 0x0006FF8C
		protected ECPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.oid = DerObjectIdentifier.GetInstance(Asn1Object.FromByteArray(ECPublicBcpgKey.ReadBytesOfEncodedLength(bcpgIn)));
			this.point = new MPInteger(bcpgIn).Value;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0006FFBC File Offset: 0x0006FFBC
		protected ECPublicBcpgKey(DerObjectIdentifier oid, ECPoint point)
		{
			this.point = new BigInteger(1, point.GetEncoded(false));
			this.oid = oid;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006FFE0 File Offset: 0x0006FFE0
		protected ECPublicBcpgKey(DerObjectIdentifier oid, BigInteger encodedPoint)
		{
			this.point = encodedPoint;
			this.oid = oid;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0006FFF8 File Offset: 0x0006FFF8
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00070000 File Offset: 0x00070000
		public override byte[] GetEncoded()
		{
			byte[] result;
			try
			{
				result = base.GetEncoded();
			}
			catch (IOException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00070034 File Offset: 0x00070034
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			byte[] encoded = this.oid.GetEncoded();
			bcpgOut.Write(encoded, 1, encoded.Length - 1);
			MPInteger bcpgObject = new MPInteger(this.point);
			bcpgOut.WriteObject(bcpgObject);
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00070074 File Offset: 0x00070074
		public virtual BigInteger EncodedPoint
		{
			get
			{
				return this.point;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0007007C File Offset: 0x0007007C
		public virtual DerObjectIdentifier CurveOid
		{
			get
			{
				return this.oid;
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00070084 File Offset: 0x00070084
		protected static byte[] ReadBytesOfEncodedLength(BcpgInputStream bcpgIn)
		{
			int num = bcpgIn.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			if (num == 0 || num == 255)
			{
				throw new IOException("future extensions not yet implemented");
			}
			byte[] array = new byte[num + 2];
			bcpgIn.ReadFully(array, 2, array.Length - 2);
			array[0] = 6;
			array[1] = (byte)num;
			return array;
		}

		// Token: 0x04000E24 RID: 3620
		internal DerObjectIdentifier oid;

		// Token: 0x04000E25 RID: 3621
		internal BigInteger point;
	}
}
