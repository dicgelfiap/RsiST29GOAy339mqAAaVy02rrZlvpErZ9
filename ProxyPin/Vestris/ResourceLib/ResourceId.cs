using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D32 RID: 3378
	[ComVisible(true)]
	public class ResourceId
	{
		// Token: 0x06008933 RID: 35123 RVA: 0x00294664 File Offset: 0x00294664
		public ResourceId(IntPtr value)
		{
			this.Id = value;
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x00294680 File Offset: 0x00294680
		public ResourceId(uint value)
		{
			this.Id = new IntPtr((long)((ulong)value));
		}

		// Token: 0x06008935 RID: 35125 RVA: 0x002946A0 File Offset: 0x002946A0
		public ResourceId(Kernel32.ResourceTypes value)
		{
			this.Id = (IntPtr)((int)value);
		}

		// Token: 0x06008936 RID: 35126 RVA: 0x002946C0 File Offset: 0x002946C0
		public ResourceId(string value)
		{
			this.Name = value;
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06008937 RID: 35127 RVA: 0x002946DC File Offset: 0x002946DC
		// (set) Token: 0x06008938 RID: 35128 RVA: 0x002946E4 File Offset: 0x002946E4
		public IntPtr Id
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = (ResourceId.IsIntResource(value) ? value : Marshal.StringToHGlobalUni(Marshal.PtrToStringUni(value)));
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06008939 RID: 35129 RVA: 0x00294708 File Offset: 0x00294708
		public string TypeName
		{
			get
			{
				if (!this.IsIntResource())
				{
					return this.Name;
				}
				return this.ResourceType.ToString();
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x0600893A RID: 35130 RVA: 0x00294740 File Offset: 0x00294740
		// (set) Token: 0x0600893B RID: 35131 RVA: 0x00294770 File Offset: 0x00294770
		public Kernel32.ResourceTypes ResourceType
		{
			get
			{
				if (this.IsIntResource())
				{
					return (Kernel32.ResourceTypes)((int)this._name);
				}
				throw new InvalidCastException(string.Format("Resource {0} is not of built-in type.", this.Name));
			}
			set
			{
				this._name = (IntPtr)((int)value);
			}
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x00294780 File Offset: 0x00294780
		public bool IsIntResource()
		{
			return ResourceId.IsIntResource(this._name);
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x00294790 File Offset: 0x00294790
		internal static bool IsIntResource(IntPtr value)
		{
			return value.ToInt64() <= 65535L;
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x0600893E RID: 35134 RVA: 0x002947A4 File Offset: 0x002947A4
		// (set) Token: 0x0600893F RID: 35135 RVA: 0x002947C8 File Offset: 0x002947C8
		public string Name
		{
			get
			{
				if (!this.IsIntResource())
				{
					return Marshal.PtrToStringUni(this._name);
				}
				return this._name.ToString();
			}
			set
			{
				this._name = Marshal.StringToHGlobalUni(value);
			}
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x002947D8 File Offset: 0x002947D8
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06008941 RID: 35137 RVA: 0x002947E0 File Offset: 0x002947E0
		public override int GetHashCode()
		{
			if (!this.IsIntResource())
			{
				return this.Name.GetHashCode();
			}
			return this.Id.ToInt32();
		}

		// Token: 0x06008942 RID: 35138 RVA: 0x00294818 File Offset: 0x00294818
		public override bool Equals(object obj)
		{
			return (obj is ResourceId && obj == this) || (obj is ResourceId && (obj as ResourceId).GetHashCode() == this.GetHashCode());
		}

		// Token: 0x04003ECD RID: 16077
		private IntPtr _name = IntPtr.Zero;
	}
}
