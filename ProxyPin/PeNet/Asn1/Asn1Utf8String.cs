using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B86 RID: 2950
	[ComVisible(true)]
	public class Asn1Utf8String : Asn1Node
	{
		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x060076AB RID: 30379 RVA: 0x00237FC8 File Offset: 0x00237FC8
		// (set) Token: 0x060076AC RID: 30380 RVA: 0x00237FD0 File Offset: 0x00237FD0
		public string Value { get; set; }

		// Token: 0x060076AD RID: 30381 RVA: 0x00237FDC File Offset: 0x00237FDC
		public Asn1Utf8String()
		{
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x00237FE4 File Offset: 0x00237FE4
		public Asn1Utf8String(string value)
		{
			this.Value = value;
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x00237FF4 File Offset: 0x00237FF4
		public static Asn1Utf8String ReadFrom(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return new Asn1Utf8String
			{
				Value = Encoding.UTF8.GetString(array)
			};
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x060076B0 RID: 30384 RVA: 0x00238034 File Offset: 0x00238034
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Utf8String;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x060076B1 RID: 30385 RVA: 0x00238038 File Offset: 0x00238038
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x060076B2 RID: 30386 RVA: 0x0023803C File Offset: 0x0023803C
		protected override XElement ToXElementCore()
		{
			return new XElement("UTF8", this.Value);
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x00238054 File Offset: 0x00238054
		protected override byte[] GetBytesCore()
		{
			return Encoding.UTF8.GetBytes(this.Value);
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x00238068 File Offset: 0x00238068
		public new static Asn1Utf8String Parse(XElement xNode)
		{
			return new Asn1Utf8String
			{
				Value = xNode.Value.Trim()
			};
		}

		// Token: 0x040039B1 RID: 14769
		public const string NODE_NAME = "UTF8";
	}
}
