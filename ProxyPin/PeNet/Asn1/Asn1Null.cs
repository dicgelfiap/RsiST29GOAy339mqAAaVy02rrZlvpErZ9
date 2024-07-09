using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B79 RID: 2937
	[ComVisible(true)]
	public class Asn1Null : Asn1Node
	{
		// Token: 0x06007647 RID: 30279 RVA: 0x00237344 File Offset: 0x00237344
		public static Asn1Null ReadFrom(Stream stream)
		{
			return new Asn1Null();
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06007648 RID: 30280 RVA: 0x0023734C File Offset: 0x0023734C
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Null;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06007649 RID: 30281 RVA: 0x00237350 File Offset: 0x00237350
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x00237354 File Offset: 0x00237354
		protected override XElement ToXElementCore()
		{
			return new XElement("Null");
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x00237368 File Offset: 0x00237368
		protected override byte[] GetBytesCore()
		{
			return new byte[0];
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x00237370 File Offset: 0x00237370
		public new static Asn1Null Parse(XElement xNode)
		{
			return new Asn1Null();
		}

		// Token: 0x0400396A RID: 14698
		public const string NODE_NAME = "Null";
	}
}
