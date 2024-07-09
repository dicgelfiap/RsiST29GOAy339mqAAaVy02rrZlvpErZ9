using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A10 RID: 2576
	internal class HotkeysEditor : UITypeEditor
	{
		// Token: 0x06006338 RID: 25400 RVA: 0x001DAC60 File Offset: 0x001DAC60
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x001DAC7C File Offset: 0x001DAC7C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			bool flag = provider != null && (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService)) != null;
			if (flag)
			{
				HotkeysEditorForm hotkeysEditorForm = new HotkeysEditorForm(HotkeysMapping.Parse(value as string));
				bool flag2 = hotkeysEditorForm.ShowDialog() == DialogResult.OK;
				if (flag2)
				{
					value = hotkeysEditorForm.GetHotkeys().ToString();
				}
			}
			return value;
		}
	}
}
