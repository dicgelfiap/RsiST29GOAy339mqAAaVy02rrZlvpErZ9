using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B78 RID: 2936
	[ComVisible(true)]
	public abstract class Asn1Node
	{
		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06007632 RID: 30258 RVA: 0x00236CB4 File Offset: 0x00236CB4
		// (set) Token: 0x06007633 RID: 30259 RVA: 0x00236CBC File Offset: 0x00236CBC
		public Asn1TagClass TagClass { get; set; }

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06007634 RID: 30260
		public abstract Asn1UniversalNodeType NodeType { get; }

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06007635 RID: 30261
		public abstract Asn1TagForm TagForm { get; }

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06007636 RID: 30262 RVA: 0x00236CC8 File Offset: 0x00236CC8
		public IList<Asn1Node> Nodes { get; } = new List<Asn1Node>();

		// Token: 0x06007637 RID: 30263 RVA: 0x00236CD0 File Offset: 0x00236CD0
		public Asn1Node FindSingleNode(Asn1TagClass tagClass, int tagId)
		{
			return this.Nodes.FirstOrDefault((Asn1Node n) => n.Is(tagClass, tagId));
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x00236D0C File Offset: 0x00236D0C
		private static int ReadTagLength(Stream stream)
		{
			int num = stream.ReadByte();
			if (num > 128)
			{
				int num2 = num - 128;
				num = 0;
				for (int i = 0; i < num2; i++)
				{
					num = num * 256 + stream.ReadByte();
				}
			}
			return num;
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x00236D68 File Offset: 0x00236D68
		public static Asn1Node ReadNode(byte[] data)
		{
			return Asn1Node.ReadNode(new MemoryStream(data));
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x00236D78 File Offset: 0x00236D78
		public static Asn1Node ReadNode(Stream stream)
		{
			int num = stream.ReadByte();
			Asn1UniversalNodeType type = (Asn1UniversalNodeType)(num & 31);
			Asn1TagClass asn1TagClass = (Asn1TagClass)(num >> 6);
			Asn1TagForm tagForm = (Asn1TagForm)(num >> 5 & 1);
			int num2 = Asn1Node.ReadTagLength(stream);
			if ((long)num2 > stream.Length)
			{
				throw new Asn1ParsingException(string.Format("Try to read more bytes from stream than exists {0} > {1}", num2, stream.Length));
			}
			byte[] buffer = new byte[num2];
			stream.Read(buffer, 0, num2);
			stream = new MemoryStream(buffer);
			if (asn1TagClass == Asn1TagClass.Universal)
			{
				Asn1Node asn1Node = Asn1Node.ReadUniversalNode(type, tagForm, stream);
				if (asn1Node == null)
				{
					asn1Node = Asn1CustomNode.ReadFrom(type, tagForm, stream);
				}
				asn1Node.TagClass = asn1TagClass;
				return asn1Node;
			}
			Asn1CustomNode asn1CustomNode = Asn1CustomNode.ReadFrom(type, tagForm, stream);
			asn1CustomNode.TagClass = asn1TagClass;
			return asn1CustomNode;
		}

		// Token: 0x0600763B RID: 30267 RVA: 0x00236E2C File Offset: 0x00236E2C
		private static Asn1Node ReadUniversalNode(Asn1UniversalNodeType type, Asn1TagForm tagForm, Stream stream)
		{
			switch (type)
			{
			case Asn1UniversalNodeType.Boolean:
				return Asn1Boolean.ReadFrom(stream);
			case Asn1UniversalNodeType.Integer:
				return Asn1Integer.ReadFrom(stream);
			case Asn1UniversalNodeType.BitString:
				return Asn1BitString.ReadFrom(stream);
			case Asn1UniversalNodeType.OctetString:
				return Asn1OctetString.ReadFrom(stream);
			case Asn1UniversalNodeType.Null:
				return Asn1Null.ReadFrom(stream);
			case Asn1UniversalNodeType.ObjectId:
				return Asn1ObjectIdentifier.ReadFrom(stream);
			case Asn1UniversalNodeType.Utf8String:
				return Asn1Utf8String.ReadFrom(stream);
			case Asn1UniversalNodeType.Sequence:
				return Asn1Sequence.ReadFrom(stream);
			case Asn1UniversalNodeType.Set:
				return Asn1Set.ReadFrom(stream);
			case Asn1UniversalNodeType.NumericString:
				return Asn1NumericString.ReadFrom(stream);
			case Asn1UniversalNodeType.PrintableString:
				return Asn1PrintableString.ReadFrom(stream);
			case Asn1UniversalNodeType.Ia5String:
				return Asn1Ia5String.ReadFrom(stream);
			case Asn1UniversalNodeType.UtcTime:
				return Asn1UtcTime.ReadFrom(stream);
			}
			return null;
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x00236F04 File Offset: 0x00236F04
		protected void ReadChildren(Stream stream)
		{
			while (stream.Position != stream.Length)
			{
				this.Nodes.Add(Asn1Node.ReadNode(stream));
			}
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x00236F2C File Offset: 0x00236F2C
		public virtual XElement ToXElement()
		{
			XElement xelement = this.ToXElementCore();
			if (this.TagClass != Asn1TagClass.Universal)
			{
				xelement.Add(new XAttribute("class", this.TagClass.ToString()));
			}
			return xelement;
		}

		// Token: 0x0600763E RID: 30270
		protected abstract XElement ToXElementCore();

		// Token: 0x0600763F RID: 30271 RVA: 0x00236F7C File Offset: 0x00236F7C
		protected static string ToHexString(byte[] data)
		{
			return data.Aggregate("", (string str, byte bt) => str + "0123456789abcdef"[bt >> 4].ToString() + "0123456789abcdef"[(int)(bt & 15)].ToString());
		}

		// Token: 0x06007640 RID: 30272 RVA: 0x00236FAC File Offset: 0x00236FAC
		protected static byte[] ReadDataFromHexString(string src)
		{
			byte[] array = new byte[src.Length / 2];
			src = src.ToLower().Trim();
			for (int i = 0; i < array.Length; i++)
			{
				char value = src[i * 2];
				char value2 = src[i * 2 + 1];
				int num = "0123456789abcdef".IndexOf(value) * 16 + "0123456789abcdef".IndexOf(value2);
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x00237024 File Offset: 0x00237024
		public static Asn1Node Parse(XElement xNode)
		{
			string localName = xNode.Name.LocalName;
			if (localName != null)
			{
				uint num = PeNet.Asn1.<PrivateImplementationDetails>.ComputeStringHash(localName);
				if (num <= 2502277006U)
				{
					if (num <= 1115340151U)
					{
						if (num != 682729123U)
						{
							if (num == 1115340151U)
							{
								if (localName == "ObjectId")
								{
									return Asn1ObjectIdentifier.Parse(xNode);
								}
							}
						}
						else if (localName == "Set")
						{
							return Asn1Set.Parse(xNode);
						}
					}
					else if (num != 1548285209U)
					{
						if (num != 1691271560U)
						{
							if (num == 2502277006U)
							{
								if (localName == "Custom")
								{
									return Asn1CustomNode.Parse(xNode);
								}
							}
						}
						else if (localName == "Sequence")
						{
							return Asn1Sequence.Parse(xNode);
						}
					}
					else if (localName == "BitString")
					{
						return Asn1BitString.Parse(xNode);
					}
				}
				else if (num <= 3450428817U)
				{
					if (num != 3030765394U)
					{
						if (num == 3450428817U)
						{
							if (localName == "NumericString")
							{
								return Asn1NumericString.Parse(xNode);
							}
						}
					}
					else if (localName == "UTF8")
					{
						return Asn1Utf8String.Parse(xNode);
					}
				}
				else if (num != 3651752933U)
				{
					if (num != 4106979017U)
					{
						if (num == 4147353540U)
						{
							if (localName == "Null")
							{
								return Asn1Null.Parse(xNode);
							}
						}
					}
					else if (localName == "PrintableString")
					{
						return Asn1PrintableString.Parse(xNode);
					}
				}
				else if (localName == "Integer")
				{
					return Asn1Integer.Parse(xNode);
				}
			}
			throw new Exception("Invalid Node " + xNode.Name.LocalName);
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x00237224 File Offset: 0x00237224
		public byte[] GetBytes()
		{
			byte[] bytesCore = this.GetBytesCore();
			Asn1UniversalNodeType nodeType = this.NodeType;
			int tagClass = (int)this.TagClass;
			Asn1TagForm tagForm = this.TagForm;
			MemoryStream memoryStream = new MemoryStream();
			int num = tagClass << 6 | (int)((int)tagForm << 5) | (int)nodeType;
			memoryStream.WriteByte((byte)num);
			if (bytesCore.Length < 128)
			{
				memoryStream.WriteByte((byte)bytesCore.Length);
			}
			else if (bytesCore.Length < 256)
			{
				memoryStream.WriteByte(129);
				memoryStream.WriteByte((byte)bytesCore.Length);
			}
			else
			{
				if (bytesCore.Length >= 65536)
				{
					throw new NotImplementedException();
				}
				memoryStream.WriteByte(130);
				memoryStream.WriteByte((byte)(bytesCore.Length >> 8));
				memoryStream.WriteByte((byte)(bytesCore.Length & 255));
			}
			memoryStream.Write(bytesCore, 0, bytesCore.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x06007643 RID: 30275
		protected abstract byte[] GetBytesCore();

		// Token: 0x06007644 RID: 30276 RVA: 0x002372FC File Offset: 0x002372FC
		public bool Is(Asn1TagClass @class, int tagId)
		{
			return this.TagClass == @class && this.NodeType == (Asn1UniversalNodeType)tagId;
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x00237318 File Offset: 0x00237318
		public bool Is(Asn1UniversalNodeType nodeType)
		{
			return this.TagClass == Asn1TagClass.Universal && this.NodeType == nodeType;
		}
	}
}
