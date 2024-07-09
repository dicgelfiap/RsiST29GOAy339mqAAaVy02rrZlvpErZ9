using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B85 RID: 2949
	[ComVisible(true)]
	public class Asn1UtcTime : Asn1Node
	{
		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x060076A3 RID: 30371 RVA: 0x00237EC4 File Offset: 0x00237EC4
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.UtcTime;
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x060076A4 RID: 30372 RVA: 0x00237EC8 File Offset: 0x00237EC8
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x060076A5 RID: 30373 RVA: 0x00237ECC File Offset: 0x00237ECC
		// (set) Token: 0x060076A6 RID: 30374 RVA: 0x00237ED4 File Offset: 0x00237ED4
		public DateTimeOffset Value { get; set; }

		// Token: 0x060076A7 RID: 30375 RVA: 0x00237EE0 File Offset: 0x00237EE0
		protected override XElement ToXElementCore()
		{
			return new XElement("UTCTime", this.Value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x00237F14 File Offset: 0x00237F14
		protected override byte[] GetBytesCore()
		{
			string format = (this.Value.Offset != TimeSpan.Zero) ? "yyMMddHHmmsszzz" : "yyMMddHHmmssZ";
			string s = this.Value.ToString(format, CultureInfo.InvariantCulture);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x00237F74 File Offset: 0x00237F74
		public static Asn1UtcTime ReadFrom(Stream stream)
		{
			string input = new StreamReader(stream).ReadToEnd();
			return new Asn1UtcTime
			{
				Value = DateTimeOffset.ParseExact(input, new string[]
				{
					"yyMMddHHmmssZ",
					"yyMMddHHmmsszzz"
				}, CultureInfo.InvariantCulture, DateTimeStyles.None)
			};
		}
	}
}
