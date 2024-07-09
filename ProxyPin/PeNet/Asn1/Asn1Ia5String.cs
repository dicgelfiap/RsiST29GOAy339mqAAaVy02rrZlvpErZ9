using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B76 RID: 2934
	[ComVisible(true)]
	public class Asn1Ia5String : Asn1Node
	{
		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x0600761C RID: 30236 RVA: 0x00236A00 File Offset: 0x00236A00
		// (set) Token: 0x0600761D RID: 30237 RVA: 0x00236A08 File Offset: 0x00236A08
		public string Value { get; set; }

		// Token: 0x0600761E RID: 30238 RVA: 0x00236A14 File Offset: 0x00236A14
		public Asn1Ia5String()
		{
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x00236A1C File Offset: 0x00236A1C
		public Asn1Ia5String(string value)
		{
			this.Value = value;
		}

		// Token: 0x06007620 RID: 30240 RVA: 0x00236A2C File Offset: 0x00236A2C
		public static Asn1Ia5String ReadFrom(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return new Asn1Ia5String
			{
				Value = Encoding.ASCII.GetString(array)
			};
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06007621 RID: 30241 RVA: 0x00236A6C File Offset: 0x00236A6C
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Ia5String;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06007622 RID: 30242 RVA: 0x00236A70 File Offset: 0x00236A70
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x00236A74 File Offset: 0x00236A74
		protected override XElement ToXElementCore()
		{
			return new XElement("IA5", this.Value);
		}

		// Token: 0x06007624 RID: 30244 RVA: 0x00236A8C File Offset: 0x00236A8C
		protected override byte[] GetBytesCore()
		{
			return Encoding.ASCII.GetBytes(this.Value);
		}

		// Token: 0x06007625 RID: 30245 RVA: 0x00236AA0 File Offset: 0x00236AA0
		public new static Asn1Ia5String Parse(XElement xNode)
		{
			return new Asn1Ia5String
			{
				Value = xNode.Value.Trim()
			};
		}

		// Token: 0x04003964 RID: 14692
		public const string NODE_NAME = "IA5";
	}
}
