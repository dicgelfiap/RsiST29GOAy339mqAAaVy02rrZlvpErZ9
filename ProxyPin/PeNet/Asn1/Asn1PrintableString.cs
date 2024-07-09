using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B7E RID: 2942
	[ComVisible(true)]
	public class Asn1PrintableString : Asn1Node
	{
		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x0600768A RID: 30346 RVA: 0x00237D64 File Offset: 0x00237D64
		// (set) Token: 0x0600768B RID: 30347 RVA: 0x00237D6C File Offset: 0x00237D6C
		public string Value { get; set; }

		// Token: 0x0600768C RID: 30348 RVA: 0x00237D78 File Offset: 0x00237D78
		public static Asn1PrintableString ReadFrom(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return new Asn1PrintableString
			{
				Value = Encoding.ASCII.GetString(array)
			};
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x0600768D RID: 30349 RVA: 0x00237DB8 File Offset: 0x00237DB8
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.PrintableString;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x0600768E RID: 30350 RVA: 0x00237DBC File Offset: 0x00237DBC
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x00237DC0 File Offset: 0x00237DC0
		protected override XElement ToXElementCore()
		{
			return new XElement("PrintableString", this.Value);
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x00237DD8 File Offset: 0x00237DD8
		protected override byte[] GetBytesCore()
		{
			return Encoding.ASCII.GetBytes(this.Value);
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x00237DEC File Offset: 0x00237DEC
		public new static Asn1PrintableString Parse(XElement xNode)
		{
			return new Asn1PrintableString
			{
				Value = xNode.Value.Trim()
			};
		}

		// Token: 0x04003985 RID: 14725
		public const string NODE_NAME = "PrintableString";
	}
}
