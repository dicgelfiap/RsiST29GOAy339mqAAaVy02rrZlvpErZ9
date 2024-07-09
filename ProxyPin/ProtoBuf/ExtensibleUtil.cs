using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C23 RID: 3107
	internal static class ExtensibleUtil
	{
		// Token: 0x06007B87 RID: 31623 RVA: 0x00246894 File Offset: 0x00246894
		internal static IEnumerable<TValue> GetExtendedValues<TValue>(IExtensible instance, int tag, DataFormat format, bool singleton, bool allowDefinedTag)
		{
			foreach (object obj in ExtensibleUtil.GetExtendedValues(RuntimeTypeModel.Default, typeof(TValue), instance, tag, format, singleton, allowDefinedTag))
			{
				TValue tvalue = (TValue)((object)obj);
				yield return tvalue;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x002468C4 File Offset: 0x002468C4
		internal static IEnumerable GetExtendedValues(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format, bool singleton, bool allowDefinedTag)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (tag <= 0)
			{
				throw new ArgumentOutOfRangeException("tag");
			}
			IExtension extn = instance.GetExtensionObject(false);
			if (extn == null)
			{
				yield break;
			}
			Stream stream = extn.BeginQuery();
			object obj = null;
			ProtoReader reader = null;
			try
			{
				SerializationContext context = new SerializationContext();
				reader = ProtoReader.Create(stream, model, context, -1L);
				while (model.TryDeserializeAuxiliaryType(reader, format, tag, type, ref obj, true, true, false, false, null) && obj != null)
				{
					if (!singleton)
					{
						yield return obj;
						obj = null;
					}
				}
				if (singleton && obj != null)
				{
					yield return obj;
				}
			}
			finally
			{
				ProtoReader.Recycle(reader);
				extn.EndQuery(stream);
			}
			yield break;
			yield break;
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x002468FC File Offset: 0x002468FC
		internal static void AppendExtendValue(TypeModel model, IExtensible instance, int tag, DataFormat format, object value)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			IExtension extensionObject = instance.GetExtensionObject(true);
			if (extensionObject == null)
			{
				throw new InvalidOperationException("No extension object available; appended data would be lost.");
			}
			bool commit = false;
			Stream stream = extensionObject.BeginAppend();
			try
			{
				using (ProtoWriter protoWriter = ProtoWriter.Create(stream, model, null))
				{
					model.TrySerializeAuxiliaryType(protoWriter, null, format, tag, value, false, null);
					protoWriter.Close();
				}
				commit = true;
			}
			finally
			{
				extensionObject.EndAppend(stream, commit);
			}
		}
	}
}
