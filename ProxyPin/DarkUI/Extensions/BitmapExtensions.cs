using System;
using System.Drawing;

namespace DarkUI.Extensions
{
	// Token: 0x02000086 RID: 134
	internal static class BitmapExtensions
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0003284C File Offset: 0x0003284C
		internal static Bitmap SetColor(this Bitmap bitmap, Color color)
		{
			Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					if (bitmap.GetPixel(i, j).A > 0)
					{
						bitmap2.SetPixel(i, j, color);
					}
				}
			}
			return bitmap2;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000328B8 File Offset: 0x000328B8
		internal static Bitmap ChangeColor(this Bitmap bitmap, Color oldColor, Color newColor)
		{
			Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					if (bitmap.GetPixel(i, j) == oldColor)
					{
						bitmap2.SetPixel(i, j, newColor);
					}
				}
			}
			return bitmap2;
		}
	}
}
