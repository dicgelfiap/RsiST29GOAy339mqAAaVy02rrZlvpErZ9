using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B80 RID: 2944
	[ComVisible(true)]
	public class Asn1Sequence : Asn1CompositeNode
	{
		// Token: 0x06007698 RID: 30360 RVA: 0x00237E34 File Offset: 0x00237E34
		public static Asn1Sequence ReadFrom(Stream stream)
		{
			Asn1Sequence asn1Sequence = new Asn1Sequence();
			asn1Sequence.ReadChildren(stream);
			return asn1Sequence;
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x00237E44 File Offset: 0x00237E44
		public static Asn1Sequence ReadFrom(byte[] data)
		{
			return Asn1Sequence.ReadFrom(new MemoryStream(data));
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600769A RID: 30362 RVA: 0x00237E54 File Offset: 0x00237E54
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Sequence;
			}
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x00237E58 File Offset: 0x00237E58
		protected override XElement ToXElementCore()
		{
			return new XElement("Sequence");
		}

		// Token: 0x0600769C RID: 30364 RVA: 0x00237E6C File Offset: 0x00237E6C
		public new static Asn1Sequence Parse(XElement xNode)
		{
			Asn1Sequence asn1Sequence = new Asn1Sequence();
			asn1Sequence.ParseChildren(xNode);
			return asn1Sequence;
		}

		// Token: 0x04003989 RID: 14729
		public const string NODE_NAME = "Sequence";
	}
}
