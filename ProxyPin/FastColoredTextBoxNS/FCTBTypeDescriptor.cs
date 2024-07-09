using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4B RID: 2635
	internal class FCTBTypeDescriptor : CustomTypeDescriptor
	{
		// Token: 0x0600679C RID: 26524 RVA: 0x001F8D54 File Offset: 0x001F8D54
		public FCTBTypeDescriptor(ICustomTypeDescriptor parent, object instance) : base(parent)
		{
			this.parent = parent;
			this.instance = instance;
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x001F8D70 File Offset: 0x001F8D70
		public override string GetComponentName()
		{
			Control control = this.instance as Control;
			return (control == null) ? null : control.Name;
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x001F8DA8 File Offset: 0x001F8DA8
		public override EventDescriptorCollection GetEvents()
		{
			EventDescriptorCollection events = base.GetEvents();
			EventDescriptor[] array = new EventDescriptor[events.Count];
			for (int i = 0; i < events.Count; i++)
			{
				bool flag = events[i].Name == "TextChanged";
				if (flag)
				{
					array[i] = new FooTextChangedDescriptor(events[i]);
				}
				else
				{
					array[i] = events[i];
				}
			}
			return new EventDescriptorCollection(array);
		}

		// Token: 0x040034CB RID: 13515
		private ICustomTypeDescriptor parent;

		// Token: 0x040034CC RID: 13516
		private object instance;
	}
}
