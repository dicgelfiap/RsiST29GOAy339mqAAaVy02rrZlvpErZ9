using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B7A RID: 2938
	[ComVisible(true)]
	public class Asn1NumericString : Asn1Node
	{
		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x0600764E RID: 30286 RVA: 0x00237380 File Offset: 0x00237380
		// (set) Token: 0x0600764F RID: 30287 RVA: 0x00237388 File Offset: 0x00237388
		public string Value { get; set; }

		// Token: 0x06007650 RID: 30288 RVA: 0x00237394 File Offset: 0x00237394
		public static Asn1NumericString ReadFrom(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return new Asn1NumericString
			{
				Value = Encoding.ASCII.GetString(array)
			};
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06007651 RID: 30289 RVA: 0x002373D4 File Offset: 0x002373D4
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.NumericString;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06007652 RID: 30290 RVA: 0x002373D8 File Offset: 0x002373D8
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x06007653 RID: 30291 RVA: 0x002373DC File Offset: 0x002373DC
		protected override XElement ToXElementCore()
		{
			return new XElement("NumericString", this.Value);
		}

		// Token: 0x06007654 RID: 30292 RVA: 0x002373F4 File Offset: 0x002373F4
		protected override byte[] GetBytesCore()
		{
			return Encoding.ASCII.GetBytes(this.Value);
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x00237408 File Offset: 0x00237408
		public new static Asn1NumericString Parse(XElement xNode)
		{
			return new Asn1NumericString
			{
				Value = xNode.Value.Trim()
			};
		}

		// Token: 0x0400396B RID: 14699
		public const string NODE_NAME = "NumericString";
	}
}
