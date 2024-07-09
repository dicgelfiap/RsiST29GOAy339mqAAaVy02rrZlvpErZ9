using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000267 RID: 615
	public class DerBoolean : Asn1Object
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x0006B010 File Offset: 0x0006B010
		public static DerBoolean GetInstance(object obj)
		{
			if (obj == null || obj is DerBoolean)
			{
				return (DerBoolean)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0006B040 File Offset: 0x0006B040
		public static DerBoolean GetInstance(bool value)
		{
			if (!value)
			{
				return DerBoolean.False;
			}
			return DerBoolean.True;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0006B054 File Offset: 0x0006B054
		public static DerBoolean GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerBoolean)
			{
				return DerBoolean.GetInstance(@object);
			}
			return DerBoolean.FromOctetString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0006B094 File Offset: 0x0006B094
		public DerBoolean(byte[] val)
		{
			if (val.Length != 1)
			{
				throw new ArgumentException("byte value should have 1 byte in it", "val");
			}
			this.value = val[0];
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0006B0C0 File Offset: 0x0006B0C0
		private DerBoolean(bool value)
		{
			this.value = (value ? byte.MaxValue : 0);
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0006B0E0 File Offset: 0x0006B0E0
		public bool IsTrue
		{
			get
			{
				return this.value != 0;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0006B0F0 File Offset: 0x0006B0F0
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(1, new byte[]
			{
				this.value
			});
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0006B11C File Offset: 0x0006B11C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBoolean derBoolean = asn1Object as DerBoolean;
			return derBoolean != null && this.IsTrue == derBoolean.IsTrue;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006B14C File Offset: 0x0006B14C
		protected override int Asn1GetHashCode()
		{
			return this.IsTrue.GetHashCode();
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0006B16C File Offset: 0x0006B16C
		public override string ToString()
		{
			if (!this.IsTrue)
			{
				return "FALSE";
			}
			return "TRUE";
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0006B184 File Offset: 0x0006B184
		internal static DerBoolean FromOctetString(byte[] value)
		{
			if (value.Length != 1)
			{
				throw new ArgumentException("BOOLEAN value should have 1 byte in it", "value");
			}
			byte b = value[0];
			if (b == 0)
			{
				return DerBoolean.False;
			}
			if (b != 255)
			{
				return new DerBoolean(value);
			}
			return DerBoolean.True;
		}

		// Token: 0x04000DA7 RID: 3495
		private readonly byte value;

		// Token: 0x04000DA8 RID: 3496
		public static readonly DerBoolean False = new DerBoolean(false);

		// Token: 0x04000DA9 RID: 3497
		public static readonly DerBoolean True = new DerBoolean(true);
	}
}
