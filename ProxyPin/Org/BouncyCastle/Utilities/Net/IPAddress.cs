using System;
using System.Globalization;

namespace Org.BouncyCastle.Utilities.Net
{
	// Token: 0x020006ED RID: 1773
	public class IPAddress
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x00151518 File Offset: 0x00151518
		public static bool IsValid(string address)
		{
			return IPAddress.IsValidIPv4(address) || IPAddress.IsValidIPv6(address);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00151530 File Offset: 0x00151530
		public static bool IsValidWithNetMask(string address)
		{
			return IPAddress.IsValidIPv4WithNetmask(address) || IPAddress.IsValidIPv6WithNetmask(address);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00151548 File Offset: 0x00151548
		public static bool IsValidIPv4(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv4(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0015158C File Offset: 0x0015158C
		private static bool unsafeIsValidIPv4(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ".";
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf('.', num2)) > num2)
			{
				if (num == 4)
				{
					return false;
				}
				string s = text.Substring(num2, num3 - num2);
				int num4 = int.Parse(s);
				if (num4 < 0 || num4 > 255)
				{
					return false;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 4;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0015161C File Offset: 0x0015161C
		public static bool IsValidIPv4WithNetmask(string address)
		{
			int num = address.IndexOf('/');
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv4(address.Substring(0, num)) && (IPAddress.IsValidIPv4(text) || IPAddress.IsMaskValue(text, 32));
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00151670 File Offset: 0x00151670
		public static bool IsValidIPv6WithNetmask(string address)
		{
			int num = address.IndexOf('/');
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv6(address.Substring(0, num)) && (IPAddress.IsValidIPv6(text) || IPAddress.IsMaskValue(text, 128));
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x001516CC File Offset: 0x001516CC
		private static bool IsMaskValue(string component, int size)
		{
			int num = int.Parse(component);
			try
			{
				return num >= 0 && num <= size;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00151728 File Offset: 0x00151728
		public static bool IsValidIPv6(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv6(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0015176C File Offset: 0x0015176C
		private static bool unsafeIsValidIPv6(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ":";
			bool flag = false;
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf(':', num2)) >= num2)
			{
				if (num == 8)
				{
					return false;
				}
				if (num2 != num3)
				{
					string text2 = text.Substring(num2, num3 - num2);
					if (num3 == text.Length - 1 && text2.IndexOf('.') > 0)
					{
						if (!IPAddress.IsValidIPv4(text2))
						{
							return false;
						}
						num++;
					}
					else
					{
						string s = text.Substring(num2, num3 - num2);
						int num4 = int.Parse(s, NumberStyles.AllowHexSpecifier);
						if (num4 < 0 || num4 > 65535)
						{
							return false;
						}
					}
				}
				else
				{
					if (num3 != 1 && num3 != text.Length - 1 && flag)
					{
						return false;
					}
					flag = true;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 8 || flag;
		}
	}
}
