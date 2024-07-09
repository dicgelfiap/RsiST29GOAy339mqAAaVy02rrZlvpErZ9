using System;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x02000841 RID: 2113
	[ComVisible(true)]
	public class ResourceCollection : LazyList<Resource, object>
	{
		// Token: 0x06004EF0 RID: 20208 RVA: 0x00187448 File Offset: 0x00187448
		public ResourceCollection()
		{
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00187450 File Offset: 0x00187450
		public ResourceCollection(IListListener<Resource> listener) : base(listener)
		{
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0018745C File Offset: 0x0018745C
		public ResourceCollection(int length, object context, Func<object, int, Resource> readOriginalValue) : base(length, context, readOriginalValue)
		{
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x00187468 File Offset: 0x00187468
		public int IndexOf(UTF8String name)
		{
			int num = -1;
			foreach (Resource resource in this)
			{
				num++;
				if (resource != null && resource.Name == name)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x001874DC File Offset: 0x001874DC
		public int IndexOfEmbeddedResource(UTF8String name)
		{
			int num = -1;
			foreach (Resource resource in this)
			{
				num++;
				if (resource != null && resource.ResourceType == ResourceType.Embedded && resource.Name == name)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0018755C File Offset: 0x0018755C
		public int IndexOfAssemblyLinkedResource(UTF8String name)
		{
			int num = -1;
			foreach (Resource resource in this)
			{
				num++;
				if (resource != null && resource.ResourceType == ResourceType.AssemblyLinked && resource.Name == name)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x001875DC File Offset: 0x001875DC
		public int IndexOfLinkedResource(UTF8String name)
		{
			int num = -1;
			foreach (Resource resource in this)
			{
				num++;
				if (resource != null && resource.ResourceType == ResourceType.Linked && resource.Name == name)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0018765C File Offset: 0x0018765C
		public Resource Find(UTF8String name)
		{
			foreach (Resource resource in this)
			{
				if (resource != null && resource.Name == name)
				{
					return resource;
				}
			}
			return null;
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x001876CC File Offset: 0x001876CC
		public EmbeddedResource FindEmbeddedResource(UTF8String name)
		{
			foreach (Resource resource in this)
			{
				if (resource != null && resource.ResourceType == ResourceType.Embedded && resource.Name == name)
				{
					return (EmbeddedResource)resource;
				}
			}
			return null;
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0018774C File Offset: 0x0018774C
		public AssemblyLinkedResource FindAssemblyLinkedResource(UTF8String name)
		{
			foreach (Resource resource in this)
			{
				if (resource != null && resource.ResourceType == ResourceType.AssemblyLinked && resource.Name == name)
				{
					return (AssemblyLinkedResource)resource;
				}
			}
			return null;
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x001877CC File Offset: 0x001877CC
		public LinkedResource FindLinkedResource(UTF8String name)
		{
			foreach (Resource resource in this)
			{
				if (resource != null && resource.ResourceType == ResourceType.Linked && resource.Name == name)
				{
					return (LinkedResource)resource;
				}
			}
			return null;
		}
	}
}
