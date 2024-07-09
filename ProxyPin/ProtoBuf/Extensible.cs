using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C22 RID: 3106
	[ComVisible(true)]
	public abstract class Extensible : IExtensible
	{
		// Token: 0x06007B77 RID: 31607 RVA: 0x002466E4 File Offset: 0x002466E4
		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return this.GetExtensionObject(createIfMissing);
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x002466F0 File Offset: 0x002466F0
		protected virtual IExtension GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x00246700 File Offset: 0x00246700
		public static IExtension GetExtensionObject(ref IExtension extensionObject, bool createIfMissing)
		{
			if (createIfMissing && extensionObject == null)
			{
				extensionObject = new BufferExtension();
			}
			return extensionObject;
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x00246718 File Offset: 0x00246718
		public static void AppendValue<TValue>(IExtensible instance, int tag, TValue value)
		{
			Extensible.AppendValue<TValue>(instance, tag, DataFormat.Default, value);
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x00246724 File Offset: 0x00246724
		public static void AppendValue<TValue>(IExtensible instance, int tag, DataFormat format, TValue value)
		{
			ExtensibleUtil.AppendExtendValue(RuntimeTypeModel.Default, instance, tag, format, value);
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x0024673C File Offset: 0x0024673C
		public static TValue GetValue<TValue>(IExtensible instance, int tag)
		{
			return Extensible.GetValue<TValue>(instance, tag, DataFormat.Default);
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x00246748 File Offset: 0x00246748
		public static TValue GetValue<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			TValue result;
			Extensible.TryGetValue<TValue>(instance, tag, format, out result);
			return result;
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x00246768 File Offset: 0x00246768
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, DataFormat.Default, out value);
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x00246774 File Offset: 0x00246774
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, format, false, out value);
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x00246780 File Offset: 0x00246780
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out TValue value)
		{
			value = default(TValue);
			bool result = false;
			foreach (TValue tvalue in ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, true, allowDefinedTag))
			{
				value = tvalue;
				result = true;
			}
			return result;
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x002467E8 File Offset: 0x002467E8
		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, DataFormat.Default, false, false);
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x002467F4 File Offset: 0x002467F4
		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, false, false);
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x00246800 File Offset: 0x00246800
		public static bool TryGetValue(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out object value)
		{
			value = null;
			bool result = false;
			foreach (object obj in ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, true, allowDefinedTag))
			{
				value = obj;
				result = true;
			}
			return result;
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x0024686C File Offset: 0x0024686C
		public static IEnumerable GetValues(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, false, false);
		}

		// Token: 0x06007B85 RID: 31621 RVA: 0x0024687C File Offset: 0x0024687C
		public static void AppendValue(TypeModel model, IExtensible instance, int tag, DataFormat format, object value)
		{
			ExtensibleUtil.AppendExtendValue(model, instance, tag, format, value);
		}

		// Token: 0x04003BA5 RID: 15269
		private IExtension extensionObject;
	}
}
