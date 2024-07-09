using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B7C RID: 2940
	[ComVisible(true)]
	public class Asn1OctetString : Asn1Node
	{
		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600767F RID: 30335 RVA: 0x00237C80 File Offset: 0x00237C80
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.OctetString;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06007680 RID: 30336 RVA: 0x00237C84 File Offset: 0x00237C84
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06007681 RID: 30337 RVA: 0x00237C88 File Offset: 0x00237C88
		// (set) Token: 0x06007682 RID: 30338 RVA: 0x00237C90 File Offset: 0x00237C90
		public byte[] Data { get; set; }

		// Token: 0x06007683 RID: 30339 RVA: 0x00237C9C File Offset: 0x00237C9C
		public Asn1OctetString()
		{
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x00237CA4 File Offset: 0x00237CA4
		public Asn1OctetString(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x00237CB4 File Offset: 0x00237CB4
		public static Asn1OctetString ReadFrom(Stream stream)
		{
			byte[] array = new byte[stream.Length - stream.Position];
			stream.Read(array, 0, array.Length);
			return new Asn1OctetString
			{
				Data = array
			};
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x00237CF4 File Offset: 0x00237CF4
		protected override XElement ToXElementCore()
		{
			return new XElement("OctetString", Asn1Node.ToHexString(this.Data));
		}

		// Token: 0x06007687 RID: 30343 RVA: 0x00237D10 File Offset: 0x00237D10
		protected override byte[] GetBytesCore()
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(this.Data, 0, this.Data.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x00237D40 File Offset: 0x00237D40
		public new static Asn1OctetString Parse(XElement xNode)
		{
			return new Asn1OctetString
			{
				Data = Asn1Node.ReadDataFromHexString(xNode.Value)
			};
		}

		// Token: 0x04003983 RID: 14723
		public const string NODE_NAME = "OctetString";
	}
}
