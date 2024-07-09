using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B81 RID: 2945
	[ComVisible(true)]
	public class Asn1Set : Asn1CompositeNode
	{
		// Token: 0x0600769E RID: 30366 RVA: 0x00237E84 File Offset: 0x00237E84
		public static Asn1Set ReadFrom(Stream stream)
		{
			Asn1Set asn1Set = new Asn1Set();
			asn1Set.ReadChildren(stream);
			return asn1Set;
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x0600769F RID: 30367 RVA: 0x00237E94 File Offset: 0x00237E94
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Set;
			}
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x00237E98 File Offset: 0x00237E98
		protected override XElement ToXElementCore()
		{
			return new XElement("Set");
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x00237EAC File Offset: 0x00237EAC
		public new static Asn1Set Parse(XElement xNode)
		{
			Asn1Set asn1Set = new Asn1Set();
			asn1Set.ParseChildren(xNode);
			return asn1Set;
		}

		// Token: 0x0400398A RID: 14730
		public const string NODE_NAME = "Set";
	}
}
