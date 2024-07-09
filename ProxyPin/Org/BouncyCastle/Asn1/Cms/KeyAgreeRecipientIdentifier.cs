using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000110 RID: 272
	public class KeyAgreeRecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x00046690 File Offset: 0x00046690
		public static KeyAgreeRecipientIdentifier GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return KeyAgreeRecipientIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000466A0 File Offset: 0x000466A0
		public static KeyAgreeRecipientIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientIdentifier)
			{
				return (KeyAgreeRecipientIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientIdentifier(IssuerAndSerialNumber.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject && ((Asn1TaggedObject)obj).TagNo == 0)
			{
				return new KeyAgreeRecipientIdentifier(RecipientKeyIdentifier.GetInstance((Asn1TaggedObject)obj, false));
			}
			throw new ArgumentException("Invalid KeyAgreeRecipientIdentifier: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00046728 File Offset: 0x00046728
		public KeyAgreeRecipientIdentifier(IssuerAndSerialNumber issuerSerial)
		{
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00046738 File Offset: 0x00046738
		public KeyAgreeRecipientIdentifier(RecipientKeyIdentifier rKeyID)
		{
			this.rKeyID = rKeyID;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00046748 File Offset: 0x00046748
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00046750 File Offset: 0x00046750
		public RecipientKeyIdentifier RKeyID
		{
			get
			{
				return this.rKeyID;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00046758 File Offset: 0x00046758
		public override Asn1Object ToAsn1Object()
		{
			if (this.issuerSerial != null)
			{
				return this.issuerSerial.ToAsn1Object();
			}
			return new DerTaggedObject(false, 0, this.rKeyID);
		}

		// Token: 0x040006F8 RID: 1784
		private readonly IssuerAndSerialNumber issuerSerial;

		// Token: 0x040006F9 RID: 1785
		private readonly RecipientKeyIdentifier rKeyID;
	}
}
