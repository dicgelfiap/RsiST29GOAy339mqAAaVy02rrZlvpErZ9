using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X500
{
	// Token: 0x020001D5 RID: 469
	public class DirectoryString : Asn1Encodable, IAsn1Choice, IAsn1String
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0005B748 File Offset: 0x0005B748
		public static DirectoryString GetInstance(object obj)
		{
			if (obj == null || obj is DirectoryString)
			{
				return (DirectoryString)obj;
			}
			if (obj is DerStringBase && (obj is DerT61String || obj is DerPrintableString || obj is DerUniversalString || obj is DerUtf8String || obj is DerBmpString))
			{
				return new DirectoryString((DerStringBase)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0005B7DC File Offset: 0x0005B7DC
		public static DirectoryString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return DirectoryString.GetInstance(obj.GetObject());
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0005B7FC File Offset: 0x0005B7FC
		private DirectoryString(DerStringBase str)
		{
			this.str = str;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0005B80C File Offset: 0x0005B80C
		public DirectoryString(string str)
		{
			this.str = new DerUtf8String(str);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0005B820 File Offset: 0x0005B820
		public string GetString()
		{
			return this.str.GetString();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0005B830 File Offset: 0x0005B830
		public override Asn1Object ToAsn1Object()
		{
			return this.str.ToAsn1Object();
		}

		// Token: 0x04000B6C RID: 2924
		private readonly DerStringBase str;
	}
}
