using System;
using System.Collections;
using Org.BouncyCastle.Bcpg.Attr;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066A RID: 1642
	public class PgpUserAttributeSubpacketVectorGenerator
	{
		// Token: 0x0600398E RID: 14734 RVA: 0x00134D9C File Offset: 0x00134D9C
		public virtual void SetImageAttribute(ImageAttrib.Format imageType, byte[] imageData)
		{
			if (imageData == null)
			{
				throw new ArgumentException("attempt to set null image", "imageData");
			}
			this.list.Add(new ImageAttrib(imageType, imageData));
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x00134DC8 File Offset: 0x00134DC8
		public virtual PgpUserAttributeSubpacketVector Generate()
		{
			UserAttributeSubpacket[] array = new UserAttributeSubpacket[this.list.Count];
			for (int i = 0; i < this.list.Count; i++)
			{
				array[i] = (UserAttributeSubpacket)this.list[i];
			}
			return new PgpUserAttributeSubpacketVector(array);
		}

		// Token: 0x04001E0D RID: 7693
		private IList list = Platform.CreateArrayList();
	}
}
