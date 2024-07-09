using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B74 RID: 2932
	[ComVisible(true)]
	public abstract class Asn1CompositeNode : Asn1Node
	{
		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x0600760C RID: 30220 RVA: 0x00236680 File Offset: 0x00236680
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Constructed;
			}
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x00236684 File Offset: 0x00236684
		public override XElement ToXElement()
		{
			XElement xelement = base.ToXElement();
			foreach (Asn1Node asn1Node in base.Nodes)
			{
				xelement.Add(asn1Node.ToXElement());
			}
			return xelement;
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x002366E8 File Offset: 0x002366E8
		protected void ParseChildren(XElement xNode)
		{
			foreach (XElement xNode2 in xNode.Elements())
			{
				Asn1Node item = Asn1Node.Parse(xNode2);
				base.Nodes.Add(item);
			}
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x00236748 File Offset: 0x00236748
		protected override byte[] GetBytesCore()
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (Asn1Node asn1Node in base.Nodes)
			{
				byte[] bytes = asn1Node.GetBytes();
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			return memoryStream.ToArray();
		}
	}
}
