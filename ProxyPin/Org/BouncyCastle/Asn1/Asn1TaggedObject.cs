using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000247 RID: 583
	public abstract class Asn1TaggedObject : Asn1Object, Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x00069360 File Offset: 0x00069360
		internal static bool IsConstructed(bool isExplicit, Asn1Object obj)
		{
			if (isExplicit || obj is Asn1Sequence || obj is Asn1Set)
			{
				return true;
			}
			Asn1TaggedObject asn1TaggedObject = obj as Asn1TaggedObject;
			return asn1TaggedObject != null && Asn1TaggedObject.IsConstructed(asn1TaggedObject.IsExplicit(), asn1TaggedObject.GetObject());
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000693B0 File Offset: 0x000693B0
		public static Asn1TaggedObject GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			if (explicitly)
			{
				return Asn1TaggedObject.GetInstance(obj.GetObject());
			}
			throw new ArgumentException("implicitly tagged tagged object");
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000693D0 File Offset: 0x000693D0
		public static Asn1TaggedObject GetInstance(object obj)
		{
			if (obj == null || obj is Asn1TaggedObject)
			{
				return (Asn1TaggedObject)obj;
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00069404 File Offset: 0x00069404
		protected Asn1TaggedObject(int tagNo, Asn1Encodable obj)
		{
			this.explicitly = true;
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00069428 File Offset: 0x00069428
		protected Asn1TaggedObject(bool explicitly, int tagNo, Asn1Encodable obj)
		{
			this.explicitly = (explicitly || obj is IAsn1Choice);
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00069460 File Offset: 0x00069460
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1TaggedObject asn1TaggedObject = asn1Object as Asn1TaggedObject;
			return asn1TaggedObject != null && (this.tagNo == asn1TaggedObject.tagNo && this.explicitly == asn1TaggedObject.explicitly) && object.Equals(this.GetObject(), asn1TaggedObject.GetObject());
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000694B8 File Offset: 0x000694B8
		protected override int Asn1GetHashCode()
		{
			int num = this.tagNo.GetHashCode();
			if (this.obj != null)
			{
				num ^= this.obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000694F0 File Offset: 0x000694F0
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000694F8 File Offset: 0x000694F8
		public bool IsExplicit()
		{
			return this.explicitly;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00069500 File Offset: 0x00069500
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00069504 File Offset: 0x00069504
		public Asn1Object GetObject()
		{
			if (this.obj != null)
			{
				return this.obj.ToAsn1Object();
			}
			return null;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00069520 File Offset: 0x00069520
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (tag == 4)
			{
				return Asn1OctetString.GetInstance(this, isExplicit).Parser;
			}
			switch (tag)
			{
			case 16:
				return Asn1Sequence.GetInstance(this, isExplicit).Parser;
			case 17:
				return Asn1Set.GetInstance(this, isExplicit).Parser;
			default:
				if (isExplicit)
				{
					return this.GetObject();
				}
				throw Platform.CreateNotImplementedException("implicit tagging for tag: " + tag);
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0006959C File Offset: 0x0006959C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.tagNo,
				"]",
				this.obj
			});
		}

		// Token: 0x04000D65 RID: 3429
		internal int tagNo;

		// Token: 0x04000D66 RID: 3430
		internal bool explicitly = true;

		// Token: 0x04000D67 RID: 3431
		internal Asn1Encodable obj;
	}
}
