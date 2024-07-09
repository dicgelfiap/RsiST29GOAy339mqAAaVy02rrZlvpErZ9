using System;
using System.Collections;
using System.Text;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006C6 RID: 1734
	public abstract class CollectionUtilities
	{
		// Token: 0x06003CAD RID: 15533 RVA: 0x0014F398 File Offset: 0x0014F398
		public static void AddRange(IList to, IEnumerable range)
		{
			foreach (object value in range)
			{
				to.Add(value);
			}
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x0014F3F4 File Offset: 0x0014F3F4
		public static bool CheckElementsAreOfType(IEnumerable e, Type t)
		{
			foreach (object o in e)
			{
				if (!t.IsInstanceOfType(o))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x0014F45C File Offset: 0x0014F45C
		public static IDictionary ReadOnly(IDictionary d)
		{
			return new UnmodifiableDictionaryProxy(d);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x0014F464 File Offset: 0x0014F464
		public static IList ReadOnly(IList l)
		{
			return new UnmodifiableListProxy(l);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x0014F46C File Offset: 0x0014F46C
		public static ISet ReadOnly(ISet s)
		{
			return new UnmodifiableSetProxy(s);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x0014F474 File Offset: 0x0014F474
		public static object RequireNext(IEnumerator e)
		{
			if (!e.MoveNext())
			{
				throw new InvalidOperationException();
			}
			return e.Current;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x0014F490 File Offset: 0x0014F490
		public static string ToString(IEnumerable c)
		{
			IEnumerator enumerator = c.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder("[");
			stringBuilder.Append(enumerator.Current.ToString());
			while (enumerator.MoveNext())
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(enumerator.Current.ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
