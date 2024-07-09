using System;
using System.Runtime.InteropServices;
using PeNet.Asn1;

namespace PeNet.Authenticode
{
	// Token: 0x02000C0D RID: 3085
	[ComVisible(true)]
	public class SignedData
	{
		// Token: 0x06007AE9 RID: 31465 RVA: 0x00244734 File Offset: 0x00244734
		public SignedData(Asn1Node asn1)
		{
			Asn1Node asn1Node = asn1.Nodes[0];
			if (asn1Node.NodeType != Asn1UniversalNodeType.Sequence || asn1Node.Nodes.Count < 4)
			{
				throw new ArgumentException("Invalid SignedData");
			}
			if (asn1Node.Nodes[0].NodeType != Asn1UniversalNodeType.Integer)
			{
				throw new ArgumentException("Invalid version");
			}
			this.ContentInfo = new ContentInfo(asn1Node.Nodes[2]);
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x002447BC File Offset: 0x002447BC
		public ContentInfo ContentInfo { get; }
	}
}
