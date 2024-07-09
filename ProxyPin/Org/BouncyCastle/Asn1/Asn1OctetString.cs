using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200023D RID: 573
	public abstract class Asn1OctetString : Asn1Object, Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x00068390 File Offset: 0x00068390
		public static Asn1OctetString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is Asn1OctetString)
			{
				return Asn1OctetString.GetInstance(@object);
			}
			return BerOctetString.FromSequence(Asn1Sequence.GetInstance(@object));
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000683CC File Offset: 0x000683CC
		public static Asn1OctetString GetInstance(object obj)
		{
			if (obj == null || obj is Asn1OctetString)
			{
				return (Asn1OctetString)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return Asn1OctetString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00068428 File Offset: 0x00068428
		internal Asn1OctetString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00068448 File Offset: 0x00068448
		public Stream GetOctetStream()
		{
			return new MemoryStream(this.str, false);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00068458 File Offset: 0x00068458
		public Asn1OctetStringParser Parser
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006845C File Offset: 0x0006845C
		public virtual byte[] GetOctets()
		{
			return this.str;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00068464 File Offset: 0x00068464
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.GetOctets());
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00068474 File Offset: 0x00068474
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerOctetString derOctetString = asn1Object as DerOctetString;
			return derOctetString != null && Arrays.AreEqual(this.GetOctets(), derOctetString.GetOctets());
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000684A8 File Offset: 0x000684A8
		public override string ToString()
		{
			return "#" + Hex.ToHexString(this.str);
		}

		// Token: 0x04000D5F RID: 3423
		internal byte[] str;
	}
}
