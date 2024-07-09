using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B72 RID: 2930
	[ComVisible(true)]
	public class Asn1BitString : Asn1Node
	{
		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x060075F5 RID: 30197 RVA: 0x0023643C File Offset: 0x0023643C
		// (set) Token: 0x060075F6 RID: 30198 RVA: 0x00236444 File Offset: 0x00236444
		public int ExtraBitsCount { get; set; }

		// Token: 0x060075F7 RID: 30199 RVA: 0x00236450 File Offset: 0x00236450
		public Asn1BitString()
		{
		}

		// Token: 0x060075F8 RID: 30200 RVA: 0x00236458 File Offset: 0x00236458
		public Asn1BitString(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x060075F9 RID: 30201 RVA: 0x00236468 File Offset: 0x00236468
		public static Asn1BitString ReadFrom(Stream stream)
		{
			int extraBitsCount = stream.ReadByte();
			byte[] array = new byte[stream.Length - stream.Position];
			stream.Read(array, 0, array.Length);
			return new Asn1BitString
			{
				Data = array,
				ExtraBitsCount = extraBitsCount
			};
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x060075FA RID: 30202 RVA: 0x002364B4 File Offset: 0x002364B4
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.BitString;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060075FB RID: 30203 RVA: 0x002364B8 File Offset: 0x002364B8
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x002364BC File Offset: 0x002364BC
		protected override XElement ToXElementCore()
		{
			return new XElement("BitString", new object[]
			{
				Asn1Node.ToHexString(this.Data),
				new XAttribute("extra", this.ExtraBitsCount)
			});
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x00236510 File Offset: 0x00236510
		protected override byte[] GetBytesCore()
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte((byte)this.ExtraBitsCount);
			memoryStream.Write(this.Data, 0, this.Data.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x00236550 File Offset: 0x00236550
		public new static Asn1BitString Parse(XElement xNode)
		{
			return new Asn1BitString
			{
				Data = Asn1Node.ReadDataFromHexString(xNode.Value),
				ExtraBitsCount = int.Parse(xNode.Attribute("extra").Value)
			};
		}

		// Token: 0x0400395A RID: 14682
		public const string NODE_NAME = "BitString";

		// Token: 0x0400395B RID: 14683
		public byte[] Data;
	}
}
