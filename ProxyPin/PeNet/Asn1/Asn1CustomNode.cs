using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B75 RID: 2933
	[ComVisible(true)]
	public class Asn1CustomNode : Asn1Node
	{
		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06007611 RID: 30225 RVA: 0x002367BC File Offset: 0x002367BC
		// (set) Token: 0x06007612 RID: 30226 RVA: 0x002367C4 File Offset: 0x002367C4
		public byte[] Data { get; set; }

		// Token: 0x06007613 RID: 30227 RVA: 0x002367D0 File Offset: 0x002367D0
		public Asn1CustomNode(int type, Asn1TagForm tagForm)
		{
			this.Type = type;
			this.TagForm = tagForm;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x002367E8 File Offset: 0x002367E8
		public Asn1CustomNode(int type, Asn1TagForm tagForm, Asn1TagClass tagClass)
		{
			this.Type = type;
			this.TagForm = tagForm;
			base.TagClass = tagClass;
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06007615 RID: 30229 RVA: 0x00236808 File Offset: 0x00236808
		public Asn1UniversalNodeType Type { get; }

		// Token: 0x06007616 RID: 30230 RVA: 0x00236810 File Offset: 0x00236810
		public static Asn1CustomNode ReadFrom(Asn1UniversalNodeType type, Asn1TagForm tagForm, Stream stream)
		{
			if (tagForm == Asn1TagForm.Primitive)
			{
				byte[] array = new byte[stream.Length];
				stream.Read(array, 0, array.Length);
				return new Asn1CustomNode((int)type, tagForm)
				{
					Data = array
				};
			}
			Asn1CustomNode asn1CustomNode = new Asn1CustomNode((int)type, tagForm);
			asn1CustomNode.ReadChildren(stream);
			return asn1CustomNode;
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06007617 RID: 30231 RVA: 0x00236860 File Offset: 0x00236860
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06007618 RID: 30232 RVA: 0x00236868 File Offset: 0x00236868
		public override Asn1TagForm TagForm { get; }

		// Token: 0x06007619 RID: 30233 RVA: 0x00236870 File Offset: 0x00236870
		protected override XElement ToXElementCore()
		{
			return new XElement("Custom", new object[]
			{
				new XAttribute("type", this.Type),
				new XAttribute("form", this.TagForm),
				Asn1Node.ToHexString(this.Data)
			});
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x002368E0 File Offset: 0x002368E0
		protected override byte[] GetBytesCore()
		{
			if (this.TagForm == Asn1TagForm.Primitive)
			{
				return this.Data;
			}
			MemoryStream memoryStream = new MemoryStream();
			foreach (Asn1Node asn1Node in base.Nodes)
			{
				byte[] bytes = asn1Node.GetBytes();
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x00236960 File Offset: 0x00236960
		public new static Asn1CustomNode Parse(XElement xNode)
		{
			int type = int.Parse(xNode.Attribute("type").Value);
			Asn1TagForm tagForm = (Asn1TagForm)Enum.Parse(typeof(Asn1TagForm), xNode.Attribute("form").Value);
			Asn1TagClass tagClass = (Asn1TagClass)Enum.Parse(typeof(Asn1TagClass), xNode.Attribute("class").Value);
			return new Asn1CustomNode(type, tagForm)
			{
				TagClass = tagClass,
				Data = Asn1Node.ReadDataFromHexString(xNode.Value)
			};
		}

		// Token: 0x04003960 RID: 14688
		public const string NODE_NAME = "Custom";
	}
}
