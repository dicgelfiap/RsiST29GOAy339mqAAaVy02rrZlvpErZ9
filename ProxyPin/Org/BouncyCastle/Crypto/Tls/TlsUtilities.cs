using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000553 RID: 1363
	public abstract class TlsUtilities
	{
		// Token: 0x060029DA RID: 10714 RVA: 0x000E05AC File Offset: 0x000E05AC
		public static void CheckUint8(int i)
		{
			if (!TlsUtilities.IsValidUint8(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000E05C4 File Offset: 0x000E05C4
		public static void CheckUint8(long i)
		{
			if (!TlsUtilities.IsValidUint8(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000E05DC File Offset: 0x000E05DC
		public static void CheckUint16(int i)
		{
			if (!TlsUtilities.IsValidUint16(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000E05F4 File Offset: 0x000E05F4
		public static void CheckUint16(long i)
		{
			if (!TlsUtilities.IsValidUint16(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000E060C File Offset: 0x000E060C
		public static void CheckUint24(int i)
		{
			if (!TlsUtilities.IsValidUint24(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000E0624 File Offset: 0x000E0624
		public static void CheckUint24(long i)
		{
			if (!TlsUtilities.IsValidUint24(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000E063C File Offset: 0x000E063C
		public static void CheckUint32(long i)
		{
			if (!TlsUtilities.IsValidUint32(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000E0654 File Offset: 0x000E0654
		public static void CheckUint48(long i)
		{
			if (!TlsUtilities.IsValidUint48(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000E066C File Offset: 0x000E066C
		public static void CheckUint64(long i)
		{
			if (!TlsUtilities.IsValidUint64(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000E0684 File Offset: 0x000E0684
		public static bool IsValidUint8(int i)
		{
			return (i & 255) == i;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000E0690 File Offset: 0x000E0690
		public static bool IsValidUint8(long i)
		{
			return (i & 255L) == i;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000E06A0 File Offset: 0x000E06A0
		public static bool IsValidUint16(int i)
		{
			return (i & 65535) == i;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000E06AC File Offset: 0x000E06AC
		public static bool IsValidUint16(long i)
		{
			return (i & 65535L) == i;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000E06BC File Offset: 0x000E06BC
		public static bool IsValidUint24(int i)
		{
			return (i & 16777215) == i;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000E06C8 File Offset: 0x000E06C8
		public static bool IsValidUint24(long i)
		{
			return (i & 16777215L) == i;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000E06D8 File Offset: 0x000E06D8
		public static bool IsValidUint32(long i)
		{
			return (i & (long)((ulong)-1)) == i;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000E06E4 File Offset: 0x000E06E4
		public static bool IsValidUint48(long i)
		{
			return (i & 281474976710655L) == i;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000E06F4 File Offset: 0x000E06F4
		public static bool IsValidUint64(long i)
		{
			return true;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000E06F8 File Offset: 0x000E06F8
		public static bool IsSsl(TlsContext context)
		{
			return context.ServerVersion.IsSsl;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000E0708 File Offset: 0x000E0708
		public static bool IsTlsV11(ProtocolVersion version)
		{
			return ProtocolVersion.TLSv11.IsEqualOrEarlierVersionOf(version.GetEquivalentTLSVersion());
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000E071C File Offset: 0x000E071C
		public static bool IsTlsV11(TlsContext context)
		{
			return TlsUtilities.IsTlsV11(context.ServerVersion);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000E072C File Offset: 0x000E072C
		public static bool IsTlsV12(ProtocolVersion version)
		{
			return ProtocolVersion.TLSv12.IsEqualOrEarlierVersionOf(version.GetEquivalentTLSVersion());
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000E0740 File Offset: 0x000E0740
		public static bool IsTlsV12(TlsContext context)
		{
			return TlsUtilities.IsTlsV12(context.ServerVersion);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000E0750 File Offset: 0x000E0750
		public static void WriteUint8(byte i, Stream output)
		{
			output.WriteByte(i);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000E075C File Offset: 0x000E075C
		public static void WriteUint8(byte i, byte[] buf, int offset)
		{
			buf[offset] = i;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000E0764 File Offset: 0x000E0764
		public static void WriteUint16(int i, Stream output)
		{
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000E0778 File Offset: 0x000E0778
		public static void WriteUint16(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 8);
			buf[offset + 1] = (byte)i;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000E0788 File Offset: 0x000E0788
		public static void WriteUint24(int i, Stream output)
		{
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000E07A8 File Offset: 0x000E07A8
		public static void WriteUint24(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 16);
			buf[offset + 1] = (byte)(i >> 8);
			buf[offset + 2] = (byte)i;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000E07C4 File Offset: 0x000E07C4
		public static void WriteUint32(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000E0800 File Offset: 0x000E0800
		public static void WriteUint32(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 24);
			buf[offset + 1] = (byte)(i >> 16);
			buf[offset + 2] = (byte)(i >> 8);
			buf[offset + 3] = (byte)i;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000E0824 File Offset: 0x000E0824
		public static void WriteUint48(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 40));
			output.WriteByte((byte)(i >> 32));
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000E0874 File Offset: 0x000E0874
		public static void WriteUint48(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 40);
			buf[offset + 1] = (byte)(i >> 32);
			buf[offset + 2] = (byte)(i >> 24);
			buf[offset + 3] = (byte)(i >> 16);
			buf[offset + 4] = (byte)(i >> 8);
			buf[offset + 5] = (byte)i;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000E08AC File Offset: 0x000E08AC
		public static void WriteUint64(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 56));
			output.WriteByte((byte)(i >> 48));
			output.WriteByte((byte)(i >> 40));
			output.WriteByte((byte)(i >> 32));
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000E0914 File Offset: 0x000E0914
		public static void WriteUint64(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 56);
			buf[offset + 1] = (byte)(i >> 48);
			buf[offset + 2] = (byte)(i >> 40);
			buf[offset + 3] = (byte)(i >> 32);
			buf[offset + 4] = (byte)(i >> 24);
			buf[offset + 5] = (byte)(i >> 16);
			buf[offset + 6] = (byte)(i >> 8);
			buf[offset + 7] = (byte)i;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000E0970 File Offset: 0x000E0970
		public static void WriteOpaque8(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint8((byte)buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000E0988 File Offset: 0x000E0988
		public static void WriteOpaque16(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint16(buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000E09A0 File Offset: 0x000E09A0
		public static void WriteOpaque24(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint24(buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000E09B8 File Offset: 0x000E09B8
		public static void WriteUint8Array(byte[] uints, Stream output)
		{
			output.Write(uints, 0, uints.Length);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000E09C8 File Offset: 0x000E09C8
		public static void WriteUint8Array(byte[] uints, byte[] buf, int offset)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint8(uints[i], buf, offset);
				offset++;
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000E09FC File Offset: 0x000E09FC
		public static void WriteUint8ArrayWithUint8Length(byte[] uints, Stream output)
		{
			TlsUtilities.CheckUint8(uints.Length);
			TlsUtilities.WriteUint8((byte)uints.Length, output);
			TlsUtilities.WriteUint8Array(uints, output);
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000E0A18 File Offset: 0x000E0A18
		public static void WriteUint8ArrayWithUint8Length(byte[] uints, byte[] buf, int offset)
		{
			TlsUtilities.CheckUint8(uints.Length);
			TlsUtilities.WriteUint8((byte)uints.Length, buf, offset);
			TlsUtilities.WriteUint8Array(uints, buf, offset + 1);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000E0A38 File Offset: 0x000E0A38
		public static void WriteUint16Array(int[] uints, Stream output)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint16(uints[i], output);
			}
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000E0A64 File Offset: 0x000E0A64
		public static void WriteUint16Array(int[] uints, byte[] buf, int offset)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint16(uints[i], buf, offset);
				offset += 2;
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000E0A98 File Offset: 0x000E0A98
		public static void WriteUint16ArrayWithUint16Length(int[] uints, Stream output)
		{
			int i = 2 * uints.Length;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, output);
			TlsUtilities.WriteUint16Array(uints, output);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000E0AC4 File Offset: 0x000E0AC4
		public static void WriteUint16ArrayWithUint16Length(int[] uints, byte[] buf, int offset)
		{
			int i = 2 * uints.Length;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, buf, offset);
			TlsUtilities.WriteUint16Array(uints, buf, offset + 2);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000E0AF4 File Offset: 0x000E0AF4
		public static byte DecodeUint8(byte[] buf)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (buf.Length != 1)
			{
				throw new TlsFatalAlert(50);
			}
			return TlsUtilities.ReadUint8(buf, 0);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000E0B20 File Offset: 0x000E0B20
		public static byte[] DecodeUint8ArrayWithUint8Length(byte[] buf)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			int num = (int)TlsUtilities.ReadUint8(buf, 0);
			if (buf.Length != num + 1)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = TlsUtilities.ReadUint8(buf, i + 1);
			}
			return array;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000E0B80 File Offset: 0x000E0B80
		public static byte[] EncodeOpaque8(byte[] buf)
		{
			TlsUtilities.CheckUint8(buf.Length);
			return Arrays.Prepend(buf, (byte)buf.Length);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000E0B94 File Offset: 0x000E0B94
		public static byte[] EncodeUint8(byte val)
		{
			TlsUtilities.CheckUint8((int)val);
			byte[] array = new byte[1];
			TlsUtilities.WriteUint8(val, array, 0);
			return array;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000E0BBC File Offset: 0x000E0BBC
		public static byte[] EncodeUint8ArrayWithUint8Length(byte[] uints)
		{
			byte[] array = new byte[1 + uints.Length];
			TlsUtilities.WriteUint8ArrayWithUint8Length(uints, array, 0);
			return array;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000E0BE4 File Offset: 0x000E0BE4
		public static byte[] EncodeUint16ArrayWithUint16Length(int[] uints)
		{
			int num = 2 * uints.Length;
			byte[] array = new byte[2 + num];
			TlsUtilities.WriteUint16ArrayWithUint16Length(uints, array, 0);
			return array;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000E0C10 File Offset: 0x000E0C10
		public static byte ReadUint8(Stream input)
		{
			int num = input.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return (byte)num;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000E0C38 File Offset: 0x000E0C38
		public static byte ReadUint8(byte[] buf, int offset)
		{
			return buf[offset];
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000E0C40 File Offset: 0x000E0C40
		public static int ReadUint16(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 8 | num2;
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000E0C74 File Offset: 0x000E0C74
		public static int ReadUint16(byte[] buf, int offset)
		{
			uint num = (uint)((uint)buf[offset] << 8);
			return (int)(num | (uint)buf[++offset]);
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000E0C98 File Offset: 0x000E0C98
		public static int ReadUint24(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			int num3 = input.ReadByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 16 | num2 << 8 | num3;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000E0CD8 File Offset: 0x000E0CD8
		public static int ReadUint24(byte[] buf, int offset)
		{
			uint num = (uint)((uint)buf[offset] << 16);
			num |= (uint)((uint)buf[++offset] << 8);
			return (int)(num | (uint)buf[++offset]);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000E0D0C File Offset: 0x000E0D0C
		public static long ReadUint32(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			int num3 = input.ReadByte();
			int num4 = input.ReadByte();
			if (num4 < 0)
			{
				throw new EndOfStreamException();
			}
			return (long)((ulong)(num << 24 | num2 << 16 | num3 << 8 | num4));
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000E0D58 File Offset: 0x000E0D58
		public static long ReadUint32(byte[] buf, int offset)
		{
			uint num = (uint)((uint)buf[offset] << 24);
			num |= (uint)((uint)buf[++offset] << 16);
			num |= (uint)((uint)buf[++offset] << 8);
			num |= (uint)buf[++offset];
			return (long)((ulong)num);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000E0D98 File Offset: 0x000E0D98
		public static long ReadUint48(Stream input)
		{
			int num = TlsUtilities.ReadUint24(input);
			int num2 = TlsUtilities.ReadUint24(input);
			return ((long)num & (long)((ulong)-1)) << 24 | ((long)num2 & (long)((ulong)-1));
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000E0DC8 File Offset: 0x000E0DC8
		public static long ReadUint48(byte[] buf, int offset)
		{
			int num = TlsUtilities.ReadUint24(buf, offset);
			int num2 = TlsUtilities.ReadUint24(buf, offset + 3);
			return ((long)num & (long)((ulong)-1)) << 24 | ((long)num2 & (long)((ulong)-1));
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000E0DFC File Offset: 0x000E0DFC
		public static byte[] ReadAllOrNothing(int length, Stream input)
		{
			if (length < 1)
			{
				return TlsUtilities.EmptyBytes;
			}
			byte[] array = new byte[length];
			int num = Streams.ReadFully(input, array);
			if (num == 0)
			{
				return null;
			}
			if (num != length)
			{
				throw new EndOfStreamException();
			}
			return array;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000E0E40 File Offset: 0x000E0E40
		public static byte[] ReadFully(int length, Stream input)
		{
			if (length < 1)
			{
				return TlsUtilities.EmptyBytes;
			}
			byte[] array = new byte[length];
			if (length != Streams.ReadFully(input, array))
			{
				throw new EndOfStreamException();
			}
			return array;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000E0E7C File Offset: 0x000E0E7C
		public static void ReadFully(byte[] buf, Stream input)
		{
			if (Streams.ReadFully(input, buf, 0, buf.Length) < buf.Length)
			{
				throw new EndOfStreamException();
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000E0E98 File Offset: 0x000E0E98
		public static byte[] ReadOpaque8(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			byte[] array = new byte[(int)b];
			TlsUtilities.ReadFully(array, input);
			return array;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000E0EC0 File Offset: 0x000E0EC0
		public static byte[] ReadOpaque16(Stream input)
		{
			int num = TlsUtilities.ReadUint16(input);
			byte[] array = new byte[num];
			TlsUtilities.ReadFully(array, input);
			return array;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000E0EE8 File Offset: 0x000E0EE8
		public static byte[] ReadOpaque24(Stream input)
		{
			int length = TlsUtilities.ReadUint24(input);
			return TlsUtilities.ReadFully(length, input);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000E0F08 File Offset: 0x000E0F08
		public static byte[] ReadUint8Array(int count, Stream input)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = TlsUtilities.ReadUint8(input);
			}
			return array;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000E0F3C File Offset: 0x000E0F3C
		public static int[] ReadUint16Array(int count, Stream input)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = TlsUtilities.ReadUint16(input);
			}
			return array;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000E0F70 File Offset: 0x000E0F70
		public static ProtocolVersion ReadVersion(byte[] buf, int offset)
		{
			return ProtocolVersion.Get((int)buf[offset], (int)buf[offset + 1]);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000E0F80 File Offset: 0x000E0F80
		public static ProtocolVersion ReadVersion(Stream input)
		{
			int major = input.ReadByte();
			int num = input.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return ProtocolVersion.Get(major, num);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000E0FB4 File Offset: 0x000E0FB4
		public static int ReadVersionRaw(byte[] buf, int offset)
		{
			return (int)buf[offset] << 8 | (int)buf[offset + 1];
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000E0FC4 File Offset: 0x000E0FC4
		public static int ReadVersionRaw(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 8 | num2;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000E0FF8 File Offset: 0x000E0FF8
		public static Asn1Object ReadAsn1Object(byte[] encoding)
		{
			MemoryStream memoryStream = new MemoryStream(encoding, false);
			Asn1InputStream asn1InputStream = new Asn1InputStream(memoryStream, encoding.Length);
			Asn1Object asn1Object = asn1InputStream.ReadObject();
			if (asn1Object == null)
			{
				throw new TlsFatalAlert(50);
			}
			if (memoryStream.Position != memoryStream.Length)
			{
				throw new TlsFatalAlert(50);
			}
			return asn1Object;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000E104C File Offset: 0x000E104C
		public static Asn1Object ReadDerObject(byte[] encoding)
		{
			Asn1Object asn1Object = TlsUtilities.ReadAsn1Object(encoding);
			byte[] encoded = asn1Object.GetEncoded("DER");
			if (!Arrays.AreEqual(encoded, encoding))
			{
				throw new TlsFatalAlert(50);
			}
			return asn1Object;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000E1088 File Offset: 0x000E1088
		public static void WriteGmtUnixTime(byte[] buf, int offset)
		{
			int num = (int)(DateTimeUtilities.CurrentUnixMs() / 1000L);
			buf[offset] = (byte)(num >> 24);
			buf[offset + 1] = (byte)(num >> 16);
			buf[offset + 2] = (byte)(num >> 8);
			buf[offset + 3] = (byte)num;
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000E10CC File Offset: 0x000E10CC
		public static void WriteVersion(ProtocolVersion version, Stream output)
		{
			output.WriteByte((byte)version.MajorVersion);
			output.WriteByte((byte)version.MinorVersion);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000E10F8 File Offset: 0x000E10F8
		public static void WriteVersion(ProtocolVersion version, byte[] buf, int offset)
		{
			buf[offset] = (byte)version.MajorVersion;
			buf[offset + 1] = (byte)version.MinorVersion;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000E1110 File Offset: 0x000E1110
		public static IList GetAllSignatureAlgorithms()
		{
			IList list = Platform.CreateArrayList(4);
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			return list;
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000E1160 File Offset: 0x000E1160
		public static IList GetDefaultDssSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 2));
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000E1170 File Offset: 0x000E1170
		public static IList GetDefaultECDsaSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 3));
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000E1180 File Offset: 0x000E1180
		public static IList GetDefaultRsaSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 1));
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000E1190 File Offset: 0x000E1190
		public static byte[] GetExtensionData(IDictionary extensions, int extensionType)
		{
			if (extensions != null)
			{
				return (byte[])extensions[extensionType];
			}
			return null;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000E11AC File Offset: 0x000E11AC
		public static IList GetDefaultSupportedSignatureAlgorithms()
		{
			byte[] array = new byte[]
			{
				2,
				3,
				4,
				5,
				6
			};
			byte[] array2 = new byte[]
			{
				1,
				2,
				3
			};
			IList list = Platform.CreateArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					list.Add(new SignatureAndHashAlgorithm(array[j], array2[i]));
				}
			}
			return list;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000E1224 File Offset: 0x000E1224
		public static SignatureAndHashAlgorithm GetSignatureAndHashAlgorithm(TlsContext context, TlsSignerCredentials signerCredentials)
		{
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				signatureAndHashAlgorithm = signerCredentials.SignatureAndHashAlgorithm;
				if (signatureAndHashAlgorithm == null)
				{
					throw new TlsFatalAlert(80);
				}
			}
			return signatureAndHashAlgorithm;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000E1258 File Offset: 0x000E1258
		public static bool HasExpectedEmptyExtensionData(IDictionary extensions, int extensionType, byte alertDescription)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, extensionType);
			if (extensionData == null)
			{
				return false;
			}
			if (extensionData.Length != 0)
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return true;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000E128C File Offset: 0x000E128C
		public static TlsSession ImportSession(byte[] sessionID, SessionParameters sessionParameters)
		{
			return new TlsSessionImpl(sessionID, sessionParameters);
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000E1298 File Offset: 0x000E1298
		public static bool IsSignatureAlgorithmsExtensionAllowed(ProtocolVersion clientVersion)
		{
			return ProtocolVersion.TLSv12.IsEqualOrEarlierVersionOf(clientVersion.GetEquivalentTLSVersion());
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000E12AC File Offset: 0x000E12AC
		public static void AddSignatureAlgorithmsExtension(IDictionary extensions, IList supportedSignatureAlgorithms)
		{
			extensions[13] = TlsUtilities.CreateSignatureAlgorithmsExtension(supportedSignatureAlgorithms);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000E12C4 File Offset: 0x000E12C4
		public static IList GetSignatureAlgorithmsExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 13);
			if (extensionData != null)
			{
				return TlsUtilities.ReadSignatureAlgorithmsExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000E12EC File Offset: 0x000E12EC
		public static byte[] CreateSignatureAlgorithmsExtension(IList supportedSignatureAlgorithms)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.EncodeSupportedSignatureAlgorithms(supportedSignatureAlgorithms, false, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000E1314 File Offset: 0x000E1314
		public static IList ReadSignatureAlgorithmsExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			IList result = TlsUtilities.ParseSupportedSignatureAlgorithms(false, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000E1350 File Offset: 0x000E1350
		public static void EncodeSupportedSignatureAlgorithms(IList supportedSignatureAlgorithms, bool allowAnonymous, Stream output)
		{
			if (supportedSignatureAlgorithms == null)
			{
				throw new ArgumentNullException("supportedSignatureAlgorithms");
			}
			if (supportedSignatureAlgorithms.Count < 1 || supportedSignatureAlgorithms.Count >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "supportedSignatureAlgorithms");
			}
			int i = 2 * supportedSignatureAlgorithms.Count;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, output);
			foreach (object obj in supportedSignatureAlgorithms)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
				if (!allowAnonymous && signatureAndHashAlgorithm.Signature == 0)
				{
					throw new ArgumentException("SignatureAlgorithm.anonymous MUST NOT appear in the signature_algorithms extension");
				}
				signatureAndHashAlgorithm.Encode(output);
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000E141C File Offset: 0x000E141C
		public static IList ParseSupportedSignatureAlgorithms(bool allowAnonymous, Stream input)
		{
			int num = TlsUtilities.ReadUint16(input);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int num2 = num / 2;
			IList list = Platform.CreateArrayList(num2);
			for (int i = 0; i < num2; i++)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = SignatureAndHashAlgorithm.Parse(input);
				if (!allowAnonymous && signatureAndHashAlgorithm.Signature == 0)
				{
					throw new TlsFatalAlert(47);
				}
				list.Add(signatureAndHashAlgorithm);
			}
			return list;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000E1494 File Offset: 0x000E1494
		public static void VerifySupportedSignatureAlgorithm(IList supportedSignatureAlgorithms, SignatureAndHashAlgorithm signatureAlgorithm)
		{
			if (supportedSignatureAlgorithms == null)
			{
				throw new ArgumentNullException("supportedSignatureAlgorithms");
			}
			if (supportedSignatureAlgorithms.Count < 1 || supportedSignatureAlgorithms.Count >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "supportedSignatureAlgorithms");
			}
			if (signatureAlgorithm == null)
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			if (signatureAlgorithm.Signature != 0)
			{
				foreach (object obj in supportedSignatureAlgorithms)
				{
					SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
					if (signatureAndHashAlgorithm.Hash == signatureAlgorithm.Hash && signatureAndHashAlgorithm.Signature == signatureAlgorithm.Signature)
					{
						return;
					}
				}
			}
			throw new TlsFatalAlert(47);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000E1574 File Offset: 0x000E1574
		public static byte[] PRF(TlsContext context, byte[] secret, string asciiLabel, byte[] seed, int size)
		{
			ProtocolVersion serverVersion = context.ServerVersion;
			if (serverVersion.IsSsl)
			{
				throw new InvalidOperationException("No PRF available for SSLv3 session");
			}
			byte[] array = Strings.ToByteArray(asciiLabel);
			byte[] array2 = TlsUtilities.Concat(array, seed);
			int prfAlgorithm = context.SecurityParameters.PrfAlgorithm;
			if (prfAlgorithm == 0)
			{
				return TlsUtilities.PRF_legacy(secret, array, array2, size);
			}
			IDigest digest = TlsUtilities.CreatePrfHash(prfAlgorithm);
			byte[] array3 = new byte[size];
			TlsUtilities.HMacHash(digest, secret, array2, array3);
			return array3;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000E15EC File Offset: 0x000E15EC
		public static byte[] PRF_legacy(byte[] secret, string asciiLabel, byte[] seed, int size)
		{
			byte[] array = Strings.ToByteArray(asciiLabel);
			byte[] labelSeed = TlsUtilities.Concat(array, seed);
			return TlsUtilities.PRF_legacy(secret, array, labelSeed, size);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000E1618 File Offset: 0x000E1618
		internal static byte[] PRF_legacy(byte[] secret, byte[] label, byte[] labelSeed, int size)
		{
			int num = (secret.Length + 1) / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(secret, 0, array, 0, num);
			Array.Copy(secret, secret.Length - num, array2, 0, num);
			byte[] array3 = new byte[size];
			byte[] array4 = new byte[size];
			TlsUtilities.HMacHash(TlsUtilities.CreateHash(1), array, labelSeed, array3);
			TlsUtilities.HMacHash(TlsUtilities.CreateHash(2), array2, labelSeed, array4);
			for (int i = 0; i < size; i++)
			{
				byte[] array5;
				IntPtr intPtr;
				(array5 = array3)[(int)(intPtr = (IntPtr)i)] = (array5[(int)intPtr] ^ array4[i]);
			}
			return array3;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000E16B0 File Offset: 0x000E16B0
		internal static byte[] Concat(byte[] a, byte[] b)
		{
			byte[] array = new byte[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000E16EC File Offset: 0x000E16EC
		internal static void HMacHash(IDigest digest, byte[] secret, byte[] seed, byte[] output)
		{
			HMac hmac = new HMac(digest);
			hmac.Init(new KeyParameter(secret));
			byte[] array = seed;
			int digestSize = digest.GetDigestSize();
			int num = (output.Length + digestSize - 1) / digestSize;
			byte[] array2 = new byte[hmac.GetMacSize()];
			byte[] array3 = new byte[hmac.GetMacSize()];
			for (int i = 0; i < num; i++)
			{
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.DoFinal(array2, 0);
				array = array2;
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.BlockUpdate(seed, 0, seed.Length);
				hmac.DoFinal(array3, 0);
				Array.Copy(array3, 0, output, digestSize * i, Math.Min(digestSize, output.Length - digestSize * i));
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000E17A4 File Offset: 0x000E17A4
		internal static void ValidateKeyUsage(X509CertificateStructure c, int keyUsageBits)
		{
			X509Extensions extensions = c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.KeyUsage);
				if (extension != null)
				{
					DerBitString instance = KeyUsage.GetInstance(extension);
					int num = (int)instance.GetBytes()[0];
					if ((num & keyUsageBits) != keyUsageBits)
					{
						throw new TlsFatalAlert(46);
					}
				}
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000E17FC File Offset: 0x000E17FC
		internal static byte[] CalculateKeyBlock(TlsContext context, int size)
		{
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] masterSecret = securityParameters.MasterSecret;
			byte[] array = TlsUtilities.Concat(securityParameters.ServerRandom, securityParameters.ClientRandom);
			if (TlsUtilities.IsSsl(context))
			{
				return TlsUtilities.CalculateKeyBlock_Ssl(masterSecret, array, size);
			}
			return TlsUtilities.PRF(context, masterSecret, "key expansion", array, size);
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000E1850 File Offset: 0x000E1850
		internal static byte[] CalculateKeyBlock_Ssl(byte[] master_secret, byte[] random, int size)
		{
			IDigest digest = TlsUtilities.CreateHash(1);
			IDigest digest2 = TlsUtilities.CreateHash(2);
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[digest2.GetDigestSize()];
			byte[] array2 = new byte[size + digestSize];
			int num = 0;
			int i = 0;
			while (i < size)
			{
				byte[] array3 = TlsUtilities.SSL3_CONST[num];
				digest2.BlockUpdate(array3, 0, array3.Length);
				digest2.BlockUpdate(master_secret, 0, master_secret.Length);
				digest2.BlockUpdate(random, 0, random.Length);
				digest2.DoFinal(array, 0);
				digest.BlockUpdate(master_secret, 0, master_secret.Length);
				digest.BlockUpdate(array, 0, array.Length);
				digest.DoFinal(array2, i);
				i += digestSize;
				num++;
			}
			return Arrays.CopyOfRange(array2, 0, size);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000E190C File Offset: 0x000E190C
		internal static byte[] CalculateMasterSecret(TlsContext context, byte[] pre_master_secret)
		{
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] array = securityParameters.IsExtendedMasterSecret ? securityParameters.SessionHash : TlsUtilities.Concat(securityParameters.ClientRandom, securityParameters.ServerRandom);
			if (TlsUtilities.IsSsl(context))
			{
				return TlsUtilities.CalculateMasterSecret_Ssl(pre_master_secret, array);
			}
			string asciiLabel = securityParameters.IsExtendedMasterSecret ? ExporterLabel.extended_master_secret : "master secret";
			return TlsUtilities.PRF(context, pre_master_secret, asciiLabel, array, 48);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000E1988 File Offset: 0x000E1988
		internal static byte[] CalculateMasterSecret_Ssl(byte[] pre_master_secret, byte[] random)
		{
			IDigest digest = TlsUtilities.CreateHash(1);
			IDigest digest2 = TlsUtilities.CreateHash(2);
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[digest2.GetDigestSize()];
			byte[] array2 = new byte[digestSize * 3];
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				byte[] array3 = TlsUtilities.SSL3_CONST[i];
				digest2.BlockUpdate(array3, 0, array3.Length);
				digest2.BlockUpdate(pre_master_secret, 0, pre_master_secret.Length);
				digest2.BlockUpdate(random, 0, random.Length);
				digest2.DoFinal(array, 0);
				digest.BlockUpdate(pre_master_secret, 0, pre_master_secret.Length);
				digest.BlockUpdate(array, 0, array.Length);
				digest.DoFinal(array2, num);
				num += digestSize;
			}
			return array2;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000E1A40 File Offset: 0x000E1A40
		internal static byte[] CalculateVerifyData(TlsContext context, string asciiLabel, byte[] handshakeHash)
		{
			if (TlsUtilities.IsSsl(context))
			{
				return handshakeHash;
			}
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] masterSecret = securityParameters.MasterSecret;
			int verifyDataLength = securityParameters.VerifyDataLength;
			return TlsUtilities.PRF(context, masterSecret, asciiLabel, handshakeHash, verifyDataLength);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000E1A80 File Offset: 0x000E1A80
		public static IDigest CreateHash(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return new MD5Digest();
			case 2:
				return new Sha1Digest();
			case 3:
				return new Sha224Digest();
			case 4:
				return new Sha256Digest();
			case 5:
				return new Sha384Digest();
			case 6:
				return new Sha512Digest();
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000E1AEC File Offset: 0x000E1AEC
		public static IDigest CreateHash(SignatureAndHashAlgorithm signatureAndHashAlgorithm)
		{
			if (signatureAndHashAlgorithm != null)
			{
				return TlsUtilities.CreateHash(signatureAndHashAlgorithm.Hash);
			}
			return new CombinedHash();
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000E1B08 File Offset: 0x000E1B08
		public static IDigest CloneHash(byte hashAlgorithm, IDigest hash)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return new MD5Digest((MD5Digest)hash);
			case 2:
				return new Sha1Digest((Sha1Digest)hash);
			case 3:
				return new Sha224Digest((Sha224Digest)hash);
			case 4:
				return new Sha256Digest((Sha256Digest)hash);
			case 5:
				return new Sha384Digest((Sha384Digest)hash);
			case 6:
				return new Sha512Digest((Sha512Digest)hash);
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000E1B98 File Offset: 0x000E1B98
		public static IDigest CreatePrfHash(int prfAlgorithm)
		{
			if (prfAlgorithm == 0)
			{
				return new CombinedHash();
			}
			return TlsUtilities.CreateHash(TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm));
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000E1BC4 File Offset: 0x000E1BC4
		public static IDigest ClonePrfHash(int prfAlgorithm, IDigest hash)
		{
			if (prfAlgorithm == 0)
			{
				return new CombinedHash((CombinedHash)hash);
			}
			return TlsUtilities.CloneHash(TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm), hash);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000E1BF8 File Offset: 0x000E1BF8
		public static byte GetHashAlgorithmForPrfAlgorithm(int prfAlgorithm)
		{
			switch (prfAlgorithm)
			{
			case 0:
				throw new ArgumentException("legacy PRF not a valid algorithm", "prfAlgorithm");
			case 1:
				return 4;
			case 2:
				return 5;
			default:
				throw new ArgumentException("unknown PrfAlgorithm", "prfAlgorithm");
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000E1C48 File Offset: 0x000E1C48
		public static DerObjectIdentifier GetOidForHashAlgorithm(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return PkcsObjectIdentifiers.MD5;
			case 2:
				return X509ObjectIdentifiers.IdSha1;
			case 3:
				return NistObjectIdentifiers.IdSha224;
			case 4:
				return NistObjectIdentifiers.IdSha256;
			case 5:
				return NistObjectIdentifiers.IdSha384;
			case 6:
				return NistObjectIdentifiers.IdSha512;
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000E1CB4 File Offset: 0x000E1CB4
		internal static short GetClientCertificateType(Certificate clientCertificate, Certificate serverCertificate)
		{
			if (clientCertificate.IsEmpty)
			{
				return -1;
			}
			X509CertificateStructure certificateAt = clientCertificate.GetCertificateAt(0);
			SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
			short result;
			try
			{
				AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
				if (asymmetricKeyParameter.IsPrivate)
				{
					throw new TlsFatalAlert(80);
				}
				if (asymmetricKeyParameter is RsaKeyParameters)
				{
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 1;
				}
				else if (asymmetricKeyParameter is DsaPublicKeyParameters)
				{
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 2;
				}
				else
				{
					if (!(asymmetricKeyParameter is ECPublicKeyParameters))
					{
						throw new TlsFatalAlert(43);
					}
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 64;
				}
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(43, alertCause);
			}
			return result;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000E1D74 File Offset: 0x000E1D74
		internal static void TrackHashAlgorithms(TlsHandshakeHash handshakeHash, IList supportedSignatureAlgorithms)
		{
			if (supportedSignatureAlgorithms != null)
			{
				foreach (object obj in supportedSignatureAlgorithms)
				{
					SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
					byte hash = signatureAndHashAlgorithm.Hash;
					if (HashAlgorithm.IsRecognized(hash))
					{
						handshakeHash.TrackHashAlgorithm(hash);
					}
				}
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000E1DEC File Offset: 0x000E1DEC
		public static bool HasSigningCapability(byte clientCertificateType)
		{
			switch (clientCertificateType)
			{
			case 1:
			case 2:
				break;
			default:
				if (clientCertificateType != 64)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000E1E1C File Offset: 0x000E1E1C
		public static TlsSigner CreateTlsSigner(byte clientCertificateType)
		{
			switch (clientCertificateType)
			{
			case 1:
				return new TlsRsaSigner();
			case 2:
				return new TlsDssSigner();
			default:
				if (clientCertificateType != 64)
				{
					throw new ArgumentException("not a type with signing capability", "clientCertificateType");
				}
				return new TlsECDsaSigner();
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000E1E70 File Offset: 0x000E1E70
		private static byte[][] GenSsl3Const()
		{
			int num = 10;
			byte[][] array = new byte[num][];
			for (int i = 0; i < num; i++)
			{
				byte[] array2 = new byte[i + 1];
				Arrays.Fill(array2, (byte)(65 + i));
				array[i] = array2;
			}
			return array;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000E1EB8 File Offset: 0x000E1EB8
		private static IList VectorOfOne(object obj)
		{
			IList list = Platform.CreateArrayList(1);
			list.Add(obj);
			return list;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000E1EDC File Offset: 0x000E1EDC
		public static int GetCipherType(int ciphersuite)
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(ciphersuite);
			switch (encryptionAlgorithm)
			{
			case 0:
			case 1:
			case 2:
				return 0;
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 12:
			case 13:
			case 14:
				return 1;
			case 10:
			case 11:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
				break;
			default:
				switch (encryptionAlgorithm)
				{
				case 103:
				case 104:
					break;
				default:
					throw new TlsFatalAlert(80);
				}
				break;
			}
			return 2;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000E1F78 File Offset: 0x000E1F78
		public static int GetEncryptionAlgorithm(int ciphersuite)
		{
			switch (ciphersuite)
			{
			case 1:
				return 0;
			case 2:
			case 44:
			case 45:
			case 46:
				return 0;
			case 3:
			case 6:
			case 7:
			case 8:
			case 9:
			case 11:
			case 12:
			case 14:
			case 15:
			case 17:
			case 18:
			case 20:
			case 21:
			case 23:
			case 25:
			case 26:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 43:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 92:
			case 93:
			case 94:
			case 95:
			case 96:
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
			case 115:
			case 116:
			case 117:
			case 118:
			case 119:
			case 120:
			case 121:
			case 122:
			case 123:
			case 124:
			case 125:
			case 126:
			case 127:
			case 128:
			case 129:
			case 130:
			case 131:
				goto IL_64A;
			case 4:
			case 24:
				return 2;
			case 5:
			case 138:
			case 142:
			case 146:
				return 2;
			case 10:
			case 13:
			case 16:
			case 19:
			case 22:
			case 27:
			case 139:
			case 143:
			case 147:
				break;
			case 47:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 60:
			case 62:
			case 63:
			case 64:
			case 103:
			case 108:
			case 140:
			case 144:
			case 148:
			case 174:
			case 178:
			case 182:
				return 8;
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 61:
			case 104:
			case 105:
			case 106:
			case 107:
			case 109:
			case 141:
			case 145:
			case 149:
			case 175:
			case 179:
			case 183:
				return 9;
			case 59:
			case 176:
			case 180:
			case 184:
				return 0;
			case 65:
			case 66:
			case 67:
			case 68:
			case 69:
			case 70:
			case 186:
			case 187:
			case 188:
			case 189:
			case 190:
			case 191:
				return 12;
			case 132:
			case 133:
			case 134:
			case 135:
			case 136:
			case 137:
			case 192:
			case 193:
			case 194:
			case 195:
			case 196:
			case 197:
				return 13;
			case 150:
			case 151:
			case 152:
			case 153:
			case 154:
			case 155:
				return 14;
			case 156:
			case 158:
			case 160:
			case 162:
			case 164:
			case 166:
			case 168:
			case 170:
			case 172:
				return 10;
			case 157:
			case 159:
			case 161:
			case 163:
			case 165:
			case 167:
			case 169:
			case 171:
			case 173:
				return 11;
			case 177:
			case 181:
			case 185:
				return 0;
			default:
				switch (ciphersuite)
				{
				case 49153:
				case 49158:
				case 49163:
				case 49168:
				case 49173:
				case 49209:
					return 0;
				case 49154:
				case 49159:
				case 49164:
				case 49169:
				case 49174:
				case 49203:
					return 2;
				case 49155:
				case 49160:
				case 49165:
				case 49170:
				case 49175:
				case 49178:
				case 49179:
				case 49180:
				case 49204:
					break;
				case 49156:
				case 49161:
				case 49166:
				case 49171:
				case 49176:
				case 49181:
				case 49182:
				case 49183:
				case 49187:
				case 49189:
				case 49191:
				case 49193:
				case 49205:
				case 49207:
					return 8;
				case 49157:
				case 49162:
				case 49167:
				case 49172:
				case 49177:
				case 49184:
				case 49185:
				case 49186:
				case 49188:
				case 49190:
				case 49192:
				case 49194:
				case 49206:
				case 49208:
					return 9;
				case 49195:
				case 49197:
				case 49199:
				case 49201:
					return 10;
				case 49196:
				case 49198:
				case 49200:
				case 49202:
					return 11;
				case 49210:
					return 0;
				case 49211:
					return 0;
				case 49212:
				case 49213:
				case 49214:
				case 49215:
				case 49216:
				case 49217:
				case 49218:
				case 49219:
				case 49220:
				case 49221:
				case 49222:
				case 49223:
				case 49224:
				case 49225:
				case 49226:
				case 49227:
				case 49228:
				case 49229:
				case 49230:
				case 49231:
				case 49232:
				case 49233:
				case 49234:
				case 49235:
				case 49236:
				case 49237:
				case 49238:
				case 49239:
				case 49240:
				case 49241:
				case 49242:
				case 49243:
				case 49244:
				case 49245:
				case 49246:
				case 49247:
				case 49248:
				case 49249:
				case 49250:
				case 49251:
				case 49252:
				case 49253:
				case 49254:
				case 49255:
				case 49256:
				case 49257:
				case 49258:
				case 49259:
				case 49260:
				case 49261:
				case 49262:
				case 49263:
				case 49264:
				case 49265:
					goto IL_64A;
				case 49266:
				case 49268:
				case 49270:
				case 49272:
				case 49300:
				case 49302:
				case 49304:
				case 49306:
					return 12;
				case 49267:
				case 49269:
				case 49271:
				case 49273:
				case 49301:
				case 49303:
				case 49305:
				case 49307:
					return 13;
				case 49274:
				case 49276:
				case 49278:
				case 49280:
				case 49282:
				case 49284:
				case 49286:
				case 49288:
				case 49290:
				case 49292:
				case 49294:
				case 49296:
				case 49298:
					return 19;
				case 49275:
				case 49277:
				case 49279:
				case 49281:
				case 49283:
				case 49285:
				case 49287:
				case 49289:
				case 49291:
				case 49293:
				case 49295:
				case 49297:
				case 49299:
					return 20;
				case 49308:
				case 49310:
				case 49316:
				case 49318:
				case 49324:
					return 15;
				case 49309:
				case 49311:
				case 49317:
				case 49319:
				case 49325:
					return 17;
				case 49312:
				case 49314:
				case 49320:
				case 49322:
				case 49326:
					return 16;
				case 49313:
				case 49315:
				case 49321:
				case 49323:
				case 49327:
					return 18;
				default:
					switch (ciphersuite)
					{
					case 52392:
					case 52393:
					case 52394:
					case 52395:
					case 52396:
					case 52397:
					case 52398:
						return 21;
					default:
						goto IL_64A;
					}
					break;
				}
				break;
			}
			return 7;
			IL_64A:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000E25DC File Offset: 0x000E25DC
		public static int GetKeyExchangeAlgorithm(int ciphersuite)
		{
			switch (ciphersuite)
			{
			case 1:
			case 2:
			case 4:
			case 5:
			case 10:
			case 47:
			case 53:
			case 59:
			case 60:
			case 61:
			case 65:
			case 132:
			case 150:
			case 156:
			case 157:
			case 186:
			case 192:
				return 1;
			case 3:
			case 6:
			case 7:
			case 8:
			case 9:
			case 11:
			case 12:
			case 14:
			case 15:
			case 17:
			case 18:
			case 20:
			case 21:
			case 23:
			case 25:
			case 26:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 43:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 92:
			case 93:
			case 94:
			case 95:
			case 96:
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
			case 115:
			case 116:
			case 117:
			case 118:
			case 119:
			case 120:
			case 121:
			case 122:
			case 123:
			case 124:
			case 125:
			case 126:
			case 127:
			case 128:
			case 129:
			case 130:
			case 131:
				goto IL_645;
			case 13:
			case 48:
			case 54:
			case 62:
			case 66:
			case 104:
			case 133:
			case 151:
			case 164:
			case 165:
			case 187:
			case 193:
				return 7;
			case 16:
			case 49:
			case 55:
			case 63:
			case 67:
			case 105:
			case 134:
			case 152:
			case 160:
			case 161:
			case 188:
			case 194:
				return 9;
			case 19:
			case 50:
			case 56:
			case 64:
			case 68:
			case 106:
			case 135:
			case 153:
			case 162:
			case 163:
			case 189:
			case 195:
				return 3;
			case 22:
			case 51:
			case 57:
			case 69:
			case 103:
			case 107:
			case 136:
			case 154:
			case 158:
			case 159:
			case 190:
			case 196:
				return 5;
			case 24:
			case 27:
			case 52:
			case 58:
			case 70:
			case 108:
			case 109:
			case 137:
			case 155:
			case 166:
			case 167:
			case 191:
			case 197:
				break;
			case 44:
			case 138:
			case 139:
			case 140:
			case 141:
			case 168:
			case 169:
			case 174:
			case 175:
			case 176:
			case 177:
				return 13;
			case 45:
			case 142:
			case 143:
			case 144:
			case 145:
			case 170:
			case 171:
			case 178:
			case 179:
			case 180:
			case 181:
				return 14;
			case 46:
			case 146:
			case 147:
			case 148:
			case 149:
			case 172:
			case 173:
			case 182:
			case 183:
			case 184:
			case 185:
				return 15;
			default:
				switch (ciphersuite)
				{
				case 49153:
				case 49154:
				case 49155:
				case 49156:
				case 49157:
				case 49189:
				case 49190:
				case 49197:
				case 49198:
				case 49268:
				case 49269:
				case 49288:
				case 49289:
					return 16;
				case 49158:
				case 49159:
				case 49160:
				case 49161:
				case 49162:
				case 49187:
				case 49188:
				case 49195:
				case 49196:
				case 49266:
				case 49267:
				case 49286:
				case 49287:
				case 49324:
				case 49325:
				case 49326:
				case 49327:
					break;
				case 49163:
				case 49164:
				case 49165:
				case 49166:
				case 49167:
				case 49193:
				case 49194:
				case 49201:
				case 49202:
				case 49272:
				case 49273:
				case 49292:
				case 49293:
					return 18;
				case 49168:
				case 49169:
				case 49170:
				case 49171:
				case 49172:
				case 49191:
				case 49192:
				case 49199:
				case 49200:
				case 49270:
				case 49271:
				case 49290:
				case 49291:
					return 19;
				case 49173:
				case 49174:
				case 49175:
				case 49176:
				case 49177:
					return 20;
				case 49178:
				case 49181:
				case 49184:
					return 21;
				case 49179:
				case 49182:
				case 49185:
					return 23;
				case 49180:
				case 49183:
				case 49186:
					return 22;
				case 49203:
				case 49204:
				case 49205:
				case 49206:
				case 49207:
				case 49208:
				case 49209:
				case 49210:
				case 49211:
				case 49306:
				case 49307:
					return 24;
				case 49212:
				case 49213:
				case 49214:
				case 49215:
				case 49216:
				case 49217:
				case 49218:
				case 49219:
				case 49220:
				case 49221:
				case 49222:
				case 49223:
				case 49224:
				case 49225:
				case 49226:
				case 49227:
				case 49228:
				case 49229:
				case 49230:
				case 49231:
				case 49232:
				case 49233:
				case 49234:
				case 49235:
				case 49236:
				case 49237:
				case 49238:
				case 49239:
				case 49240:
				case 49241:
				case 49242:
				case 49243:
				case 49244:
				case 49245:
				case 49246:
				case 49247:
				case 49248:
				case 49249:
				case 49250:
				case 49251:
				case 49252:
				case 49253:
				case 49254:
				case 49255:
				case 49256:
				case 49257:
				case 49258:
				case 49259:
				case 49260:
				case 49261:
				case 49262:
				case 49263:
				case 49264:
				case 49265:
					goto IL_645;
				case 49274:
				case 49275:
				case 49308:
				case 49309:
				case 49312:
				case 49313:
					return 1;
				case 49276:
				case 49277:
				case 49310:
				case 49311:
				case 49314:
				case 49315:
					return 5;
				case 49278:
				case 49279:
					return 9;
				case 49280:
				case 49281:
					return 3;
				case 49282:
				case 49283:
					return 7;
				case 49284:
				case 49285:
					return 11;
				case 49294:
				case 49295:
				case 49300:
				case 49301:
				case 49316:
				case 49317:
				case 49320:
				case 49321:
					return 13;
				case 49296:
				case 49297:
				case 49302:
				case 49303:
				case 49318:
				case 49319:
				case 49322:
				case 49323:
					return 14;
				case 49298:
				case 49299:
				case 49304:
				case 49305:
					return 15;
				default:
					switch (ciphersuite)
					{
					case 52392:
						return 19;
					case 52393:
						break;
					case 52394:
						return 5;
					case 52395:
						return 13;
					case 52396:
						return 24;
					case 52397:
						return 14;
					case 52398:
						return 1;
					default:
						goto IL_645;
					}
					break;
				}
				return 17;
			}
			return 11;
			IL_645:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000E2C3C File Offset: 0x000E2C3C
		public static int GetMacAlgorithm(int ciphersuite)
		{
			switch (ciphersuite)
			{
			case 1:
			case 4:
			case 24:
				return 1;
			case 2:
			case 5:
			case 10:
			case 13:
			case 16:
			case 19:
			case 22:
			case 27:
			case 44:
			case 45:
			case 46:
			case 47:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 65:
			case 66:
			case 67:
			case 68:
			case 69:
			case 70:
			case 132:
			case 133:
			case 134:
			case 135:
			case 136:
			case 137:
			case 138:
			case 139:
			case 140:
			case 141:
			case 142:
			case 143:
			case 144:
			case 145:
			case 146:
			case 147:
			case 148:
			case 149:
			case 150:
			case 151:
			case 152:
			case 153:
			case 154:
			case 155:
				return 2;
			case 3:
			case 6:
			case 7:
			case 8:
			case 9:
			case 11:
			case 12:
			case 14:
			case 15:
			case 17:
			case 18:
			case 20:
			case 21:
			case 23:
			case 25:
			case 26:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 43:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 92:
			case 93:
			case 94:
			case 95:
			case 96:
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
			case 115:
			case 116:
			case 117:
			case 118:
			case 119:
			case 120:
			case 121:
			case 122:
			case 123:
			case 124:
			case 125:
			case 126:
			case 127:
			case 128:
			case 129:
			case 130:
			case 131:
				goto IL_61D;
			case 59:
			case 60:
			case 61:
			case 62:
			case 63:
			case 64:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
			case 108:
			case 109:
			case 174:
			case 176:
			case 178:
			case 180:
			case 182:
			case 184:
			case 186:
			case 187:
			case 188:
			case 189:
			case 190:
			case 191:
			case 192:
			case 193:
			case 194:
			case 195:
			case 196:
			case 197:
				return 3;
			case 156:
			case 157:
			case 158:
			case 159:
			case 160:
			case 161:
			case 162:
			case 163:
			case 164:
			case 165:
			case 166:
			case 167:
			case 168:
			case 169:
			case 170:
			case 171:
			case 172:
			case 173:
				break;
			case 175:
			case 177:
			case 179:
			case 181:
			case 183:
			case 185:
				return 4;
			default:
				switch (ciphersuite)
				{
				case 49153:
				case 49154:
				case 49155:
				case 49156:
				case 49157:
				case 49158:
				case 49159:
				case 49160:
				case 49161:
				case 49162:
				case 49163:
				case 49164:
				case 49165:
				case 49166:
				case 49167:
				case 49168:
				case 49169:
				case 49170:
				case 49171:
				case 49172:
				case 49173:
				case 49174:
				case 49175:
				case 49176:
				case 49177:
				case 49178:
				case 49179:
				case 49180:
				case 49181:
				case 49182:
				case 49183:
				case 49184:
				case 49185:
				case 49186:
				case 49203:
				case 49204:
				case 49205:
				case 49206:
				case 49209:
					return 2;
				case 49187:
				case 49189:
				case 49191:
				case 49193:
				case 49207:
				case 49210:
				case 49266:
				case 49268:
				case 49270:
				case 49272:
				case 49300:
				case 49302:
				case 49304:
				case 49306:
					return 3;
				case 49188:
				case 49190:
				case 49192:
				case 49194:
				case 49208:
				case 49211:
				case 49267:
				case 49269:
				case 49271:
				case 49273:
				case 49301:
				case 49303:
				case 49305:
				case 49307:
					return 4;
				case 49195:
				case 49196:
				case 49197:
				case 49198:
				case 49199:
				case 49200:
				case 49201:
				case 49202:
				case 49274:
				case 49275:
				case 49276:
				case 49277:
				case 49278:
				case 49279:
				case 49280:
				case 49281:
				case 49282:
				case 49283:
				case 49284:
				case 49285:
				case 49286:
				case 49287:
				case 49288:
				case 49289:
				case 49290:
				case 49291:
				case 49292:
				case 49293:
				case 49294:
				case 49295:
				case 49296:
				case 49297:
				case 49298:
				case 49299:
				case 49308:
				case 49309:
				case 49310:
				case 49311:
				case 49312:
				case 49313:
				case 49314:
				case 49315:
				case 49316:
				case 49317:
				case 49318:
				case 49319:
				case 49320:
				case 49321:
				case 49322:
				case 49323:
				case 49324:
				case 49325:
				case 49326:
				case 49327:
					break;
				case 49212:
				case 49213:
				case 49214:
				case 49215:
				case 49216:
				case 49217:
				case 49218:
				case 49219:
				case 49220:
				case 49221:
				case 49222:
				case 49223:
				case 49224:
				case 49225:
				case 49226:
				case 49227:
				case 49228:
				case 49229:
				case 49230:
				case 49231:
				case 49232:
				case 49233:
				case 49234:
				case 49235:
				case 49236:
				case 49237:
				case 49238:
				case 49239:
				case 49240:
				case 49241:
				case 49242:
				case 49243:
				case 49244:
				case 49245:
				case 49246:
				case 49247:
				case 49248:
				case 49249:
				case 49250:
				case 49251:
				case 49252:
				case 49253:
				case 49254:
				case 49255:
				case 49256:
				case 49257:
				case 49258:
				case 49259:
				case 49260:
				case 49261:
				case 49262:
				case 49263:
				case 49264:
				case 49265:
					goto IL_61D;
				default:
					switch (ciphersuite)
					{
					case 52392:
					case 52393:
					case 52394:
					case 52395:
					case 52396:
					case 52397:
					case 52398:
						break;
					default:
						goto IL_61D;
					}
					break;
				}
				break;
			}
			return 0;
			IL_61D:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000E3274 File Offset: 0x000E3274
		public static ProtocolVersion GetMinimumVersion(int ciphersuite)
		{
			if (ciphersuite <= 197)
			{
				switch (ciphersuite)
				{
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
					break;
				default:
					switch (ciphersuite)
					{
					case 103:
					case 104:
					case 105:
					case 106:
					case 107:
					case 108:
					case 109:
						break;
					default:
						switch (ciphersuite)
						{
						case 156:
						case 157:
						case 158:
						case 159:
						case 160:
						case 161:
						case 162:
						case 163:
						case 164:
						case 165:
						case 166:
						case 167:
						case 168:
						case 169:
						case 170:
						case 171:
						case 172:
						case 173:
						case 186:
						case 187:
						case 188:
						case 189:
						case 190:
						case 191:
						case 192:
						case 193:
						case 194:
						case 195:
						case 196:
						case 197:
							break;
						case 174:
						case 175:
						case 176:
						case 177:
						case 178:
						case 179:
						case 180:
						case 181:
						case 182:
						case 183:
						case 184:
						case 185:
							goto IL_28F;
						default:
							goto IL_28F;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (ciphersuite)
				{
				case 49187:
				case 49188:
				case 49189:
				case 49190:
				case 49191:
				case 49192:
				case 49193:
				case 49194:
				case 49195:
				case 49196:
				case 49197:
				case 49198:
				case 49199:
				case 49200:
				case 49201:
				case 49202:
					break;
				default:
					switch (ciphersuite)
					{
					case 49266:
					case 49267:
					case 49268:
					case 49269:
					case 49270:
					case 49271:
					case 49272:
					case 49273:
					case 49274:
					case 49275:
					case 49276:
					case 49277:
					case 49278:
					case 49279:
					case 49280:
					case 49281:
					case 49282:
					case 49283:
					case 49284:
					case 49285:
					case 49286:
					case 49287:
					case 49288:
					case 49289:
					case 49290:
					case 49291:
					case 49292:
					case 49293:
					case 49294:
					case 49295:
					case 49296:
					case 49297:
					case 49298:
					case 49299:
					case 49308:
					case 49309:
					case 49310:
					case 49311:
					case 49312:
					case 49313:
					case 49314:
					case 49315:
					case 49316:
					case 49317:
					case 49318:
					case 49319:
					case 49320:
					case 49321:
					case 49322:
					case 49323:
					case 49324:
					case 49325:
					case 49326:
					case 49327:
						break;
					case 49300:
					case 49301:
					case 49302:
					case 49303:
					case 49304:
					case 49305:
					case 49306:
					case 49307:
						goto IL_28F;
					default:
						switch (ciphersuite)
						{
						case 52392:
						case 52393:
						case 52394:
						case 52395:
						case 52396:
						case 52397:
						case 52398:
							break;
						default:
							goto IL_28F;
						}
						break;
					}
					break;
				}
			}
			return ProtocolVersion.TLSv12;
			IL_28F:
			return ProtocolVersion.SSLv3;
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000E351C File Offset: 0x000E351C
		public static bool IsAeadCipherSuite(int ciphersuite)
		{
			return 2 == TlsUtilities.GetCipherType(ciphersuite);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000E3528 File Offset: 0x000E3528
		public static bool IsBlockCipherSuite(int ciphersuite)
		{
			return 1 == TlsUtilities.GetCipherType(ciphersuite);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000E3534 File Offset: 0x000E3534
		public static bool IsStreamCipherSuite(int ciphersuite)
		{
			return 0 == TlsUtilities.GetCipherType(ciphersuite);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000E3540 File Offset: 0x000E3540
		public static bool IsValidCipherSuiteForSignatureAlgorithms(int cipherSuite, IList sigAlgs)
		{
			int keyExchangeAlgorithm;
			try
			{
				keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(cipherSuite);
			}
			catch (IOException)
			{
				return true;
			}
			switch (keyExchangeAlgorithm)
			{
			case 3:
			case 4:
			case 22:
				return sigAlgs.Contains(2);
			case 5:
			case 6:
			case 19:
			case 23:
				return sigAlgs.Contains(1);
			case 11:
			case 12:
			case 20:
				return sigAlgs.Contains(0);
			case 17:
				return sigAlgs.Contains(3);
			}
			return true;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000E360C File Offset: 0x000E360C
		public static bool IsValidCipherSuiteForVersion(int cipherSuite, ProtocolVersion serverVersion)
		{
			return TlsUtilities.GetMinimumVersion(cipherSuite).IsEqualOrEarlierVersionOf(serverVersion.GetEquivalentTLSVersion());
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000E3620 File Offset: 0x000E3620
		public static IList GetUsableSignatureAlgorithms(IList sigHashAlgs)
		{
			if (sigHashAlgs == null)
			{
				return TlsUtilities.GetAllSignatureAlgorithms();
			}
			IList list = Platform.CreateArrayList(4);
			list.Add(0);
			foreach (object obj in sigHashAlgs)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
				byte signature = signatureAndHashAlgorithm.Signature;
				if (!list.Contains(signature))
				{
					list.Add(signature);
				}
			}
			return list;
		}

		// Token: 0x04001B22 RID: 6946
		public static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04001B23 RID: 6947
		public static readonly short[] EmptyShorts = new short[0];

		// Token: 0x04001B24 RID: 6948
		public static readonly int[] EmptyInts = new int[0];

		// Token: 0x04001B25 RID: 6949
		public static readonly long[] EmptyLongs = new long[0];

		// Token: 0x04001B26 RID: 6950
		internal static readonly byte[] SSL_CLIENT = new byte[]
		{
			67,
			76,
			78,
			84
		};

		// Token: 0x04001B27 RID: 6951
		internal static readonly byte[] SSL_SERVER = new byte[]
		{
			83,
			82,
			86,
			82
		};

		// Token: 0x04001B28 RID: 6952
		internal static readonly byte[][] SSL3_CONST = TlsUtilities.GenSsl3Const();
	}
}
