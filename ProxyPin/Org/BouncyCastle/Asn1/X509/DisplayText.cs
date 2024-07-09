using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F4 RID: 500
	public class DisplayText : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x0005E70C File Offset: 0x0005E70C
		public DisplayText(int type, string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = type;
			switch (type)
			{
			case 0:
				this.contents = new DerIA5String(text);
				return;
			case 1:
				this.contents = new DerBmpString(text);
				return;
			case 2:
				this.contents = new DerUtf8String(text);
				return;
			case 3:
				this.contents = new DerVisibleString(text);
				return;
			default:
				this.contents = new DerUtf8String(text);
				return;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0005E7A8 File Offset: 0x0005E7A8
		public DisplayText(string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = 2;
			this.contents = new DerUtf8String(text);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0005E7E4 File Offset: 0x0005E7E4
		public DisplayText(IAsn1String contents)
		{
			this.contents = contents;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005E7F4 File Offset: 0x0005E7F4
		public static DisplayText GetInstance(object obj)
		{
			if (obj is IAsn1String)
			{
				return new DisplayText((IAsn1String)obj);
			}
			if (obj is DisplayText)
			{
				return (DisplayText)obj;
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005E848 File Offset: 0x0005E848
		public override Asn1Object ToAsn1Object()
		{
			return (Asn1Object)this.contents;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005E858 File Offset: 0x0005E858
		public string GetString()
		{
			return this.contents.GetString();
		}

		// Token: 0x04000BCB RID: 3019
		public const int ContentTypeIA5String = 0;

		// Token: 0x04000BCC RID: 3020
		public const int ContentTypeBmpString = 1;

		// Token: 0x04000BCD RID: 3021
		public const int ContentTypeUtf8String = 2;

		// Token: 0x04000BCE RID: 3022
		public const int ContentTypeVisibleString = 3;

		// Token: 0x04000BCF RID: 3023
		public const int DisplayTextMaximumSize = 200;

		// Token: 0x04000BD0 RID: 3024
		internal readonly int contentType;

		// Token: 0x04000BD1 RID: 3025
		internal readonly IAsn1String contents;
	}
}
