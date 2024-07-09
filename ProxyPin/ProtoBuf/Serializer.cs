using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C3E RID: 3134
	[ComVisible(true)]
	public static class Serializer
	{
		// Token: 0x06007CA5 RID: 31909 RVA: 0x0024AC4C File Offset: 0x0024AC4C
		public static string GetProto<T>()
		{
			return Serializer.GetProto<T>(ProtoSyntax.Proto2);
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x0024AC54 File Offset: 0x0024AC54
		public static string GetProto<T>(ProtoSyntax syntax)
		{
			return RuntimeTypeModel.Default.GetSchema(RuntimeTypeModel.Default.MapType(typeof(T)), syntax);
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x0024AC78 File Offset: 0x0024AC78
		public static T DeepClone<T>(T instance)
		{
			if (instance != null)
			{
				return (T)((object)RuntimeTypeModel.Default.DeepClone(instance));
			}
			return instance;
		}

		// Token: 0x06007CA8 RID: 31912 RVA: 0x0024AC9C File Offset: 0x0024AC9C
		public static T Merge<T>(Stream source, T instance)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, instance, typeof(T)));
		}

		// Token: 0x06007CA9 RID: 31913 RVA: 0x0024ACC0 File Offset: 0x0024ACC0
		public static T Deserialize<T>(Stream source)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, null, typeof(T)));
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x0024ACE0 File Offset: 0x0024ACE0
		public static object Deserialize(Type type, Stream source)
		{
			return RuntimeTypeModel.Default.Deserialize(source, null, type);
		}

		// Token: 0x06007CAB RID: 31915 RVA: 0x0024ACF0 File Offset: 0x0024ACF0
		public static void Serialize<T>(Stream destination, T instance)
		{
			if (instance != null)
			{
				RuntimeTypeModel.Default.Serialize(destination, instance);
			}
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x0024AD10 File Offset: 0x0024AD10
		public static TTo ChangeType<TFrom, TTo>(TFrom instance)
		{
			TTo result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize<TFrom>(memoryStream, instance);
				memoryStream.Position = 0L;
				result = Serializer.Deserialize<TTo>(memoryStream);
			}
			return result;
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x0024AD5C File Offset: 0x0024AD5C
		public static void Serialize<T>(SerializationInfo info, T instance) where T : class, ISerializable
		{
			Serializer.Serialize<T>(info, new StreamingContext(StreamingContextStates.Persistence), instance);
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x0024AD6C File Offset: 0x0024AD6C
		public static void Serialize<T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (instance.GetType() != typeof(T))
			{
				throw new ArgumentException("Incorrect type", "instance");
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				RuntimeTypeModel.Default.Serialize(memoryStream, instance, context);
				info.AddValue("proto", memoryStream.ToArray());
			}
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x0024AE20 File Offset: 0x0024AE20
		public static void Serialize<T>(XmlWriter writer, T instance) where T : IXmlSerializable
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize<T>(memoryStream, instance);
				writer.WriteBase64(Helpers.GetBuffer(memoryStream), 0, (int)memoryStream.Length);
			}
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x0024AE98 File Offset: 0x0024AE98
		public static void Merge<T>(XmlReader reader, T instance) where T : IXmlSerializable
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			byte[] buffer = new byte[4096];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int depth = reader.Depth;
				while (reader.Read() && reader.Depth > depth)
				{
					if (reader.NodeType == XmlNodeType.Text)
					{
						int count;
						while ((count = reader.ReadContentAsBase64(buffer, 0, 4096)) > 0)
						{
							memoryStream.Write(buffer, 0, count);
						}
						if (reader.Depth <= depth)
						{
							break;
						}
					}
				}
				memoryStream.Position = 0L;
				Serializer.Merge<T>(memoryStream, instance);
			}
		}

		// Token: 0x06007CB1 RID: 31921 RVA: 0x0024AF68 File Offset: 0x0024AF68
		public static void Merge<T>(SerializationInfo info, T instance) where T : class, ISerializable
		{
			Serializer.Merge<T>(info, new StreamingContext(StreamingContextStates.Persistence), instance);
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x0024AF78 File Offset: 0x0024AF78
		public static void Merge<T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (instance.GetType() != typeof(T))
			{
				throw new ArgumentException("Incorrect type", "instance");
			}
			byte[] buffer = (byte[])info.GetValue("proto", typeof(byte[]));
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				T t = (T)((object)RuntimeTypeModel.Default.Deserialize(memoryStream, instance, typeof(T), context));
				if (t != instance)
				{
					throw new ProtoException("Deserialization changed the instance; cannot succeed.");
				}
			}
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x0024B064 File Offset: 0x0024B064
		public static void PrepareSerializer<T>()
		{
			Serializer.NonGeneric.PrepareSerializer(typeof(T));
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x0024B078 File Offset: 0x0024B078
		public static IFormatter CreateFormatter<T>()
		{
			return RuntimeTypeModel.Default.CreateFormatter(typeof(T));
		}

		// Token: 0x06007CB5 RID: 31925 RVA: 0x0024B090 File Offset: 0x0024B090
		public static IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			return RuntimeTypeModel.Default.DeserializeItems<T>(source, style, fieldNumber);
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x0024B0A0 File Offset: 0x0024B0A0
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style)
		{
			return Serializer.DeserializeWithLengthPrefix<T>(source, style, 0);
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x0024B0AC File Offset: 0x0024B0AC
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, null, @default.MapType(typeof(T)), style, fieldNumber));
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x0024B0E4 File Offset: 0x0024B0E4
		public static T MergeWithLengthPrefix<T>(Stream source, T instance, PrefixStyle style)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, instance, @default.MapType(typeof(T)), style, 0));
		}

		// Token: 0x06007CB9 RID: 31929 RVA: 0x0024B120 File Offset: 0x0024B120
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style)
		{
			Serializer.SerializeWithLengthPrefix<T>(destination, instance, style, 0);
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x0024B12C File Offset: 0x0024B12C
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(typeof(T)), style, fieldNumber);
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x0024B164 File Offset: 0x0024B164
		public static bool TryReadLengthPrefix(Stream source, PrefixStyle style, out int length)
		{
			int num;
			int num2;
			length = ProtoReader.ReadLengthPrefix(source, false, style, out num, out num2);
			return num2 > 0;
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x0024B188 File Offset: 0x0024B188
		public static bool TryReadLengthPrefix(byte[] buffer, int index, int count, PrefixStyle style, out int length)
		{
			bool result;
			using (Stream stream = new MemoryStream(buffer, index, count))
			{
				result = Serializer.TryReadLengthPrefix(stream, style, out length);
			}
			return result;
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x0024B1CC File Offset: 0x0024B1CC
		public static void FlushPool()
		{
			BufferPool.Flush();
		}

		// Token: 0x04003C20 RID: 15392
		private const string ProtoBinaryField = "proto";

		// Token: 0x04003C21 RID: 15393
		public const int ListItemTag = 1;

		// Token: 0x02001170 RID: 4464
		public static class NonGeneric
		{
			// Token: 0x06009348 RID: 37704 RVA: 0x002C2B88 File Offset: 0x002C2B88
			public static object DeepClone(object instance)
			{
				if (instance != null)
				{
					return RuntimeTypeModel.Default.DeepClone(instance);
				}
				return null;
			}

			// Token: 0x06009349 RID: 37705 RVA: 0x002C2BA0 File Offset: 0x002C2BA0
			public static void Serialize(Stream dest, object instance)
			{
				if (instance != null)
				{
					RuntimeTypeModel.Default.Serialize(dest, instance);
				}
			}

			// Token: 0x0600934A RID: 37706 RVA: 0x002C2BB4 File Offset: 0x002C2BB4
			public static object Deserialize(Type type, Stream source)
			{
				return RuntimeTypeModel.Default.Deserialize(source, null, type);
			}

			// Token: 0x0600934B RID: 37707 RVA: 0x002C2BC4 File Offset: 0x002C2BC4
			public static object Merge(Stream source, object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return RuntimeTypeModel.Default.Deserialize(source, instance, instance.GetType(), null);
			}

			// Token: 0x0600934C RID: 37708 RVA: 0x002C2BEC File Offset: 0x002C2BEC
			public static void SerializeWithLengthPrefix(Stream destination, object instance, PrefixStyle style, int fieldNumber)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(instance.GetType()), style, fieldNumber);
			}

			// Token: 0x0600934D RID: 37709 RVA: 0x002C2C2C File Offset: 0x002C2C2C
			public static bool TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, Serializer.TypeResolver resolver, out object value)
			{
				value = RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, null, null, style, 0, resolver);
				return value != null;
			}

			// Token: 0x0600934E RID: 37710 RVA: 0x002C2C54 File Offset: 0x002C2C54
			public static bool CanSerialize(Type type)
			{
				return RuntimeTypeModel.Default.IsDefined(type);
			}

			// Token: 0x0600934F RID: 37711 RVA: 0x002C2C64 File Offset: 0x002C2C64
			public static void PrepareSerializer(Type t)
			{
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				@default[@default.MapType(t)].CompileInPlace();
			}
		}

		// Token: 0x02001171 RID: 4465
		public static class GlobalOptions
		{
			// Token: 0x17001E80 RID: 7808
			// (get) Token: 0x06009350 RID: 37712 RVA: 0x002C2C90 File Offset: 0x002C2C90
			// (set) Token: 0x06009351 RID: 37713 RVA: 0x002C2C9C File Offset: 0x002C2C9C
			[Obsolete("Please use RuntimeTypeModel.Default.InferTagFromNameDefault instead (or on a per-model basis)", false)]
			public static bool InferTagFromName
			{
				get
				{
					return RuntimeTypeModel.Default.InferTagFromNameDefault;
				}
				set
				{
					RuntimeTypeModel.Default.InferTagFromNameDefault = value;
				}
			}
		}

		// Token: 0x02001172 RID: 4466
		// (Invoke) Token: 0x06009353 RID: 37715
		public delegate Type TypeResolver(int fieldNumber);
	}
}
