using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FA RID: 506
	public class GeneralNames : Asn1Encodable
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x0005F780 File Offset: 0x0005F780
		private static GeneralName[] Copy(GeneralName[] names)
		{
			return (GeneralName[])names.Clone();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005F790 File Offset: 0x0005F790
		public static GeneralNames GetInstance(object obj)
		{
			if (obj is GeneralNames)
			{
				return (GeneralNames)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new GeneralNames(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005F7B8 File Offset: 0x0005F7B8
		public static GeneralNames GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return GeneralNames.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005F7C8 File Offset: 0x0005F7C8
		public static GeneralNames FromExtensions(X509Extensions extensions, DerObjectIdentifier extOid)
		{
			return GeneralNames.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, extOid));
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005F7D8 File Offset: 0x0005F7D8
		public GeneralNames(GeneralName name)
		{
			this.names = new GeneralName[]
			{
				name
			};
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005F808 File Offset: 0x0005F808
		public GeneralNames(GeneralName[] names)
		{
			this.names = GeneralNames.Copy(names);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005F81C File Offset: 0x0005F81C
		private GeneralNames(Asn1Sequence seq)
		{
			this.names = new GeneralName[seq.Count];
			for (int num = 0; num != seq.Count; num++)
			{
				this.names[num] = GeneralName.GetInstance(seq[num]);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0005F870 File Offset: 0x0005F870
		public GeneralName[] GetNames()
		{
			return GeneralNames.Copy(this.names);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0005F880 File Offset: 0x0005F880
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.names);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005F890 File Offset: 0x0005F890
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("GeneralNames:");
			stringBuilder.Append(newLine);
			foreach (GeneralName value in this.names)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BE9 RID: 3049
		private readonly GeneralName[] names;
	}
}
