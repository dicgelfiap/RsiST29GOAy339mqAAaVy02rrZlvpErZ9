using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E8 RID: 488
	public class AuthorityInformationAccess : Asn1Encodable
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0005D070 File Offset: 0x0005D070
		private static AccessDescription[] Copy(AccessDescription[] descriptions)
		{
			return (AccessDescription[])descriptions.Clone();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0005D080 File Offset: 0x0005D080
		public static AuthorityInformationAccess GetInstance(object obj)
		{
			if (obj is AuthorityInformationAccess)
			{
				return (AuthorityInformationAccess)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new AuthorityInformationAccess(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0005D0A8 File Offset: 0x0005D0A8
		public static AuthorityInformationAccess FromExtensions(X509Extensions extensions)
		{
			return AuthorityInformationAccess.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.AuthorityInfoAccess));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0005D0BC File Offset: 0x0005D0BC
		private AuthorityInformationAccess(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("sequence may not be empty");
			}
			this.descriptions = new AccessDescription[seq.Count];
			for (int i = 0; i < seq.Count; i++)
			{
				this.descriptions[i] = AccessDescription.GetInstance(seq[i]);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0005D128 File Offset: 0x0005D128
		public AuthorityInformationAccess(AccessDescription description)
		{
			this.descriptions = new AccessDescription[]
			{
				description
			};
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0005D158 File Offset: 0x0005D158
		public AuthorityInformationAccess(AccessDescription[] descriptions)
		{
			this.descriptions = AuthorityInformationAccess.Copy(descriptions);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0005D16C File Offset: 0x0005D16C
		public AuthorityInformationAccess(DerObjectIdentifier oid, GeneralName location) : this(new AccessDescription(oid, location))
		{
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0005D17C File Offset: 0x0005D17C
		public AccessDescription[] GetAccessDescriptions()
		{
			return AuthorityInformationAccess.Copy(this.descriptions);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0005D18C File Offset: 0x0005D18C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.descriptions);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005D19C File Offset: 0x0005D19C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("AuthorityInformationAccess:");
			stringBuilder.Append(newLine);
			foreach (AccessDescription value in this.descriptions)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BAD RID: 2989
		private readonly AccessDescription[] descriptions;
	}
}
