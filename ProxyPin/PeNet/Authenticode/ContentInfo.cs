using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PeNet.Asn1;

namespace PeNet.Authenticode
{
	// Token: 0x02000C0C RID: 3084
	[ComVisible(true)]
	public class ContentInfo
	{
		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06007AE5 RID: 31461 RVA: 0x00244648 File Offset: 0x00244648
		public Asn1Node Content { get; }

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x00244650 File Offset: 0x00244650
		public string ContentType { get; }

		// Token: 0x06007AE7 RID: 31463 RVA: 0x00244658 File Offset: 0x00244658
		public ContentInfo(byte[] data) : this(Asn1Node.ReadNode(data))
		{
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x00244668 File Offset: 0x00244668
		public ContentInfo(Asn1Node asn1)
		{
			IList<Asn1Node> nodes = asn1.Nodes;
			if (asn1.NodeType != Asn1UniversalNodeType.Sequence || (nodes.Count < 1 && nodes.Count > 2))
			{
				throw new ArgumentException("Invalid ASN1");
			}
			if (!(nodes[0] is Asn1ObjectIdentifier))
			{
				throw new ArgumentException("Invalid contentType");
			}
			this.ContentType = ((Asn1ObjectIdentifier)nodes[0]).FriendlyName;
			if (nodes.Count <= 1)
			{
				return;
			}
			if (nodes[1].TagClass != Asn1TagClass.ContextDefined || nodes[1].TagForm != Asn1TagForm.Constructed)
			{
				throw new ArgumentException("Invalid content");
			}
			this.Content = nodes[1];
		}
	}
}
