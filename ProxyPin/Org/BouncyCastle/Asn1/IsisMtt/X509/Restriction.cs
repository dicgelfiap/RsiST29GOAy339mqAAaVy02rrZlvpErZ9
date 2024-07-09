using System;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200017E RID: 382
	public class Restriction : Asn1Encodable
	{
		// Token: 0x06000CD7 RID: 3287 RVA: 0x00051E6C File Offset: 0x00051E6C
		public static Restriction GetInstance(object obj)
		{
			if (obj is Restriction)
			{
				return (Restriction)obj;
			}
			if (obj is IAsn1String)
			{
				return new Restriction(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00051EC0 File Offset: 0x00051EC0
		private Restriction(DirectoryString restriction)
		{
			this.restriction = restriction;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00051ED0 File Offset: 0x00051ED0
		public Restriction(string restriction)
		{
			this.restriction = new DirectoryString(restriction);
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00051EE4 File Offset: 0x00051EE4
		public virtual DirectoryString RestrictionString
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00051EEC File Offset: 0x00051EEC
		public override Asn1Object ToAsn1Object()
		{
			return this.restriction.ToAsn1Object();
		}

		// Token: 0x040008E3 RID: 2275
		private readonly DirectoryString restriction;
	}
}
