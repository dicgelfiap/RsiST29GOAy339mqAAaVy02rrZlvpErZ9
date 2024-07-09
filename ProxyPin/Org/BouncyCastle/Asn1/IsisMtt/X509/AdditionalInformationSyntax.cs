using System;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000176 RID: 374
	public class AdditionalInformationSyntax : Asn1Encodable
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x00050B1C File Offset: 0x00050B1C
		public static AdditionalInformationSyntax GetInstance(object obj)
		{
			if (obj is AdditionalInformationSyntax)
			{
				return (AdditionalInformationSyntax)obj;
			}
			if (obj is IAsn1String)
			{
				return new AdditionalInformationSyntax(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00050B70 File Offset: 0x00050B70
		private AdditionalInformationSyntax(DirectoryString information)
		{
			this.information = information;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00050B80 File Offset: 0x00050B80
		public AdditionalInformationSyntax(string information)
		{
			this.information = new DirectoryString(information);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00050B94 File Offset: 0x00050B94
		public virtual DirectoryString Information
		{
			get
			{
				return this.information;
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00050B9C File Offset: 0x00050B9C
		public override Asn1Object ToAsn1Object()
		{
			return this.information.ToAsn1Object();
		}

		// Token: 0x040008B9 RID: 2233
		private readonly DirectoryString information;
	}
}
