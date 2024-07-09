using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C70 RID: 3184
	internal abstract class AttributeMap
	{
		// Token: 0x06007E8A RID: 32394 RVA: 0x00253CE8 File Offset: 0x00253CE8
		public override string ToString()
		{
			Type attributeType = this.AttributeType;
			return ((attributeType != null) ? attributeType.FullName : null) ?? "";
		}

		// Token: 0x06007E8B RID: 32395
		public abstract bool TryGet(string key, bool publicOnly, out object value);

		// Token: 0x06007E8C RID: 32396 RVA: 0x00253D10 File Offset: 0x00253D10
		public bool TryGet(string key, out object value)
		{
			return this.TryGet(key, true, out value);
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06007E8D RID: 32397
		public abstract Type AttributeType { get; }

		// Token: 0x06007E8E RID: 32398 RVA: 0x00253D1C File Offset: 0x00253D1C
		public static AttributeMap[] Create(TypeModel model, Type type, bool inherit)
		{
			object[] customAttributes = type.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x00253D64 File Offset: 0x00253D64
		public static AttributeMap[] Create(TypeModel model, MemberInfo member, bool inherit)
		{
			object[] customAttributes = member.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x00253DAC File Offset: 0x00253DAC
		public static AttributeMap[] Create(TypeModel model, Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(false);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06007E91 RID: 32401
		public abstract object Target { get; }

		// Token: 0x02001174 RID: 4468
		private sealed class ReflectionAttributeMap : AttributeMap
		{
			// Token: 0x06009357 RID: 37719 RVA: 0x002C2CD0 File Offset: 0x002C2CD0
			public ReflectionAttributeMap(Attribute attribute)
			{
				this.attribute = attribute;
			}

			// Token: 0x17001E81 RID: 7809
			// (get) Token: 0x06009358 RID: 37720 RVA: 0x002C2CE0 File Offset: 0x002C2CE0
			public override object Target
			{
				get
				{
					return this.attribute;
				}
			}

			// Token: 0x17001E82 RID: 7810
			// (get) Token: 0x06009359 RID: 37721 RVA: 0x002C2CE8 File Offset: 0x002C2CE8
			public override Type AttributeType
			{
				get
				{
					return this.attribute.GetType();
				}
			}

			// Token: 0x0600935A RID: 37722 RVA: 0x002C2CF8 File Offset: 0x002C2CF8
			public override bool TryGet(string key, bool publicOnly, out object value)
			{
				MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(this.attribute.GetType(), publicOnly);
				MemberInfo[] array = instanceFieldsAndProperties;
				int i = 0;
				while (i < array.Length)
				{
					MemberInfo memberInfo = array[i];
					if (string.Equals(memberInfo.Name, key, StringComparison.OrdinalIgnoreCase))
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null)
						{
							value = propertyInfo.GetValue(this.attribute, null);
							return true;
						}
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							value = fieldInfo.GetValue(this.attribute);
							return true;
						}
						throw new NotSupportedException(memberInfo.GetType().Name);
					}
					else
					{
						i++;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x04004B4A RID: 19274
			private readonly Attribute attribute;
		}
	}
}
