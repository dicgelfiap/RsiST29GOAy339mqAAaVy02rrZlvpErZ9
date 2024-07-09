using System;
using System.ComponentModel;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4C RID: 2636
	internal class FooTextChangedDescriptor : EventDescriptor
	{
		// Token: 0x0600679F RID: 26527 RVA: 0x001F8E3C File Offset: 0x001F8E3C
		public FooTextChangedDescriptor(MemberDescriptor desc) : base(desc)
		{
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x001F8E48 File Offset: 0x001F8E48
		public override void AddEventHandler(object component, Delegate value)
		{
			(component as FastColoredTextBox).BindingTextChanged += (value as EventHandler);
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x001F8E60 File Offset: 0x001F8E60
		public override Type ComponentType
		{
			get
			{
				return typeof(FastColoredTextBox);
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x001F8E84 File Offset: 0x001F8E84
		public override Type EventType
		{
			get
			{
				return typeof(EventHandler);
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x001F8EA8 File Offset: 0x001F8EA8
		public override bool IsMulticast
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x001F8EC4 File Offset: 0x001F8EC4
		public override void RemoveEventHandler(object component, Delegate value)
		{
			(component as FastColoredTextBox).BindingTextChanged -= (value as EventHandler);
		}
	}
}
