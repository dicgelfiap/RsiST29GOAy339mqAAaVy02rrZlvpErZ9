using System;
using System.Collections;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200067D RID: 1661
	public class AsymmetricKeyEntry : Pkcs12Entry
	{
		// Token: 0x060039FF RID: 14847 RVA: 0x00137794 File Offset: 0x00137794
		public AsymmetricKeyEntry(AsymmetricKeyParameter key) : base(Platform.CreateHashtable())
		{
			this.key = key;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x001377A8 File Offset: 0x001377A8
		[Obsolete]
		public AsymmetricKeyEntry(AsymmetricKeyParameter key, Hashtable attributes) : base(attributes)
		{
			this.key = key;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x001377B8 File Offset: 0x001377B8
		public AsymmetricKeyEntry(AsymmetricKeyParameter key, IDictionary attributes) : base(attributes)
		{
			this.key = key;
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x001377C8 File Offset: 0x001377C8
		public AsymmetricKeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x001377D0 File Offset: 0x001377D0
		public override bool Equals(object obj)
		{
			AsymmetricKeyEntry asymmetricKeyEntry = obj as AsymmetricKeyEntry;
			return asymmetricKeyEntry != null && this.key.Equals(asymmetricKeyEntry.key);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x00137804 File Offset: 0x00137804
		public override int GetHashCode()
		{
			return ~this.key.GetHashCode();
		}

		// Token: 0x04001E30 RID: 7728
		private readonly AsymmetricKeyParameter key;
	}
}
