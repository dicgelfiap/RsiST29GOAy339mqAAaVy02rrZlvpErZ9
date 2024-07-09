using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using ProtoBuf.Meta;

namespace ProtoBuf.ServiceModel
{
	// Token: 0x02000C45 RID: 3141
	[ComVisible(true)]
	public sealed class XmlProtoSerializer : XmlObjectSerializer
	{
		// Token: 0x06007CD3 RID: 31955 RVA: 0x0024B394 File Offset: 0x0024B394
		internal XmlProtoSerializer(TypeModel model, int key, Type type, bool isList)
		{
			if (key < 0)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.model = model;
			this.key = key;
			this.isList = isList;
			if (type == null)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.type = type;
			this.isEnum = Helpers.IsEnum(type);
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x0024B40C File Offset: 0x0024B40C
		public static XmlProtoSerializer TryCreate(TypeModel model, Type type)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			bool flag;
			int num = XmlProtoSerializer.GetKey(model, ref type, out flag);
			if (num >= 0)
			{
				return new XmlProtoSerializer(model, num, type, flag);
			}
			return null;
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x0024B464 File Offset: 0x0024B464
		public XmlProtoSerializer(TypeModel model, Type type)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.key = XmlProtoSerializer.GetKey(model, ref type, out this.isList);
			this.model = model;
			this.type = type;
			this.isEnum = Helpers.IsEnum(type);
			if (this.key < 0)
			{
				throw new ArgumentOutOfRangeException("type", "Type not recognised by the model: " + type.FullName);
			}
		}

		// Token: 0x06007CD6 RID: 31958 RVA: 0x0024B4F8 File Offset: 0x0024B4F8
		private static int GetKey(TypeModel model, ref Type type, out bool isList)
		{
			if (model != null && type != null)
			{
				int num = model.GetKey(ref type);
				if (num >= 0)
				{
					isList = false;
					return num;
				}
				Type listItemType = TypeModel.GetListItemType(model, type);
				if (listItemType != null)
				{
					num = model.GetKey(ref listItemType);
					if (num >= 0)
					{
						isList = true;
						return num;
					}
				}
			}
			isList = false;
			return -1;
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x0024B560 File Offset: 0x0024B560
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x0024B57C File Offset: 0x0024B57C
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartElement("proto");
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x0024B59C File Offset: 0x0024B59C
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (graph == null)
			{
				writer.WriteAttributeString("nil", "true");
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.isList)
				{
					this.model.Serialize(memoryStream, graph, null);
				}
				else
				{
					using (ProtoWriter protoWriter = ProtoWriter.Create(memoryStream, this.model, null))
					{
						this.model.Serialize(this.key, graph, protoWriter);
					}
				}
				byte[] buffer = memoryStream.GetBuffer();
				writer.WriteBase64(buffer, 0, (int)memoryStream.Length);
			}
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x0024B66C File Offset: 0x0024B66C
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			reader.MoveToContent();
			return reader.NodeType == XmlNodeType.Element && reader.Name == "proto";
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x0024B6A4 File Offset: 0x0024B6A4
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			reader.MoveToContent();
			bool isEmptyElement = reader.IsEmptyElement;
			bool flag = reader.GetAttribute("nil") == "true";
			reader.ReadStartElement("proto");
			if (flag)
			{
				if (!isEmptyElement)
				{
					reader.ReadEndElement();
				}
				return null;
			}
			if (isEmptyElement)
			{
				if (this.isList || this.isEnum)
				{
					return this.model.Deserialize(Stream.Null, null, this.type, null);
				}
				ProtoReader protoReader = null;
				try
				{
					protoReader = ProtoReader.Create(Stream.Null, this.model, null, -1L);
					return this.model.Deserialize(this.key, null, protoReader);
				}
				finally
				{
					ProtoReader.Recycle(protoReader);
				}
			}
			object result;
			using (MemoryStream memoryStream = new MemoryStream(reader.ReadContentAsBase64()))
			{
				if (this.isList || this.isEnum)
				{
					result = this.model.Deserialize(memoryStream, null, this.type, null);
				}
				else
				{
					ProtoReader protoReader2 = null;
					try
					{
						protoReader2 = ProtoReader.Create(memoryStream, this.model, null, -1L);
						result = this.model.Deserialize(this.key, null, protoReader2);
					}
					finally
					{
						ProtoReader.Recycle(protoReader2);
					}
				}
			}
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x04003C2D RID: 15405
		private readonly TypeModel model;

		// Token: 0x04003C2E RID: 15406
		private readonly int key;

		// Token: 0x04003C2F RID: 15407
		private readonly bool isList;

		// Token: 0x04003C30 RID: 15408
		private readonly bool isEnum;

		// Token: 0x04003C31 RID: 15409
		private readonly Type type;

		// Token: 0x04003C32 RID: 15410
		private const string PROTO_ELEMENT = "proto";
	}
}
