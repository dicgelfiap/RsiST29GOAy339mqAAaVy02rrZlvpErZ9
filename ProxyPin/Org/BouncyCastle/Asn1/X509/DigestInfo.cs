using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F3 RID: 499
	public class DigestInfo : Asn1Encodable
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0005E5E4 File Offset: 0x0005E5E4
		public static DigestInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005E5F4 File Offset: 0x0005E5F4
		public static DigestInfo GetInstance(object obj)
		{
			if (obj is DigestInfo)
			{
				return (DigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005E648 File Offset: 0x0005E648
		public DigestInfo(AlgorithmIdentifier algID, byte[] digest)
		{
			this.digest = digest;
			this.algID = algID;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005E660 File Offset: 0x0005E660
		private DigestInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.digest = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0005E6BC File Offset: 0x0005E6BC
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005E6C4 File Offset: 0x0005E6C4
		public byte[] GetDigest()
		{
			return this.digest;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005E6CC File Offset: 0x0005E6CC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				new DerOctetString(this.digest)
			});
		}

		// Token: 0x04000BC9 RID: 3017
		private readonly byte[] digest;

		// Token: 0x04000BCA RID: 3018
		private readonly AlgorithmIdentifier algID;
	}
}
