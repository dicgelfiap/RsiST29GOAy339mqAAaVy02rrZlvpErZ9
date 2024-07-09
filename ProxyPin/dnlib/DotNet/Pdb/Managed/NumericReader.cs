using System;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000951 RID: 2385
	internal static class NumericReader
	{
		// Token: 0x06005B97 RID: 23447 RVA: 0x001BEC8C File Offset: 0x001BEC8C
		public static bool TryReadNumeric(ref DataReader reader, ulong end, out object value)
		{
			value = null;
			ulong num = (ulong)reader.Position;
			if (num + 2UL > end)
			{
				return false;
			}
			NumericLeaf numericLeaf = (NumericLeaf)reader.ReadUInt16();
			if (numericLeaf < NumericLeaf.LF_NUMERIC)
			{
				value = (short)numericLeaf;
				return true;
			}
			switch (numericLeaf)
			{
			case NumericLeaf.LF_NUMERIC:
				if (num > end)
				{
					return false;
				}
				value = reader.ReadSByte();
				return true;
			case NumericLeaf.LF_SHORT:
				if (num + 2UL > end)
				{
					return false;
				}
				value = reader.ReadInt16();
				return true;
			case NumericLeaf.LF_USHORT:
				if (num + 2UL > end)
				{
					return false;
				}
				value = reader.ReadUInt16();
				return true;
			case NumericLeaf.LF_LONG:
				if (num + 4UL > end)
				{
					return false;
				}
				value = reader.ReadInt32();
				return true;
			case NumericLeaf.LF_ULONG:
				if (num + 4UL > end)
				{
					return false;
				}
				value = reader.ReadUInt32();
				return true;
			case NumericLeaf.LF_REAL32:
				if (num + 4UL > end)
				{
					return false;
				}
				value = reader.ReadSingle();
				return true;
			case NumericLeaf.LF_REAL64:
				if (num + 8UL > end)
				{
					return false;
				}
				value = reader.ReadDouble();
				return true;
			case NumericLeaf.LF_REAL80:
			case NumericLeaf.LF_REAL128:
			case NumericLeaf.LF_REAL48:
			case NumericLeaf.LF_COMPLEX32:
			case NumericLeaf.LF_COMPLEX64:
			case NumericLeaf.LF_COMPLEX80:
			case NumericLeaf.LF_COMPLEX128:
				break;
			case NumericLeaf.LF_QUADWORD:
				if (num + 8UL > end)
				{
					return false;
				}
				value = reader.ReadInt64();
				return true;
			case NumericLeaf.LF_UQUADWORD:
				if (num + 8UL > end)
				{
					return false;
				}
				value = reader.ReadUInt64();
				return true;
			case NumericLeaf.LF_VARSTRING:
			{
				if (num + 2UL > end)
				{
					return false;
				}
				int num2 = (int)reader.ReadUInt16();
				if (num + (ulong)num2 > end)
				{
					return false;
				}
				value = reader.ReadUtf8String(num2);
				return true;
			}
			default:
				if (numericLeaf == NumericLeaf.LF_VARIANT)
				{
					if (num + 16UL > end)
					{
						return false;
					}
					int num3 = reader.ReadInt32();
					int hi = reader.ReadInt32();
					int lo = reader.ReadInt32();
					int mid = reader.ReadInt32();
					byte b = (byte)(num3 >> 16);
					if (b <= 28)
					{
						value = new decimal(lo, mid, hi, num3 < 0, b);
					}
					else
					{
						value = null;
					}
					return true;
				}
				break;
			}
			return false;
		}
	}
}
