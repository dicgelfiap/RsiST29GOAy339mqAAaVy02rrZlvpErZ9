using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B1E RID: 2846
	[NullableContext(1)]
	[Nullable(0)]
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060072AF RID: 29359 RVA: 0x00228530 File Offset: 0x00228530
		public JPropertyDescriptor(string name) : base(name, null)
		{
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x0022853C File Offset: 0x0022853C
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x00228544 File Offset: 0x00228544
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x00228548 File Offset: 0x00228548
		[return: Nullable(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x00228564 File Offset: 0x00228564
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x00228568 File Offset: 0x00228568
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken value2 = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = value2;
			}
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x002285A8 File Offset: 0x002285A8
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060072B6 RID: 29366 RVA: 0x002285AC File Offset: 0x002285AC
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060072B7 RID: 29367 RVA: 0x002285B8 File Offset: 0x002285B8
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060072B8 RID: 29368 RVA: 0x002285BC File Offset: 0x002285BC
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060072B9 RID: 29369 RVA: 0x002285C8 File Offset: 0x002285C8
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
