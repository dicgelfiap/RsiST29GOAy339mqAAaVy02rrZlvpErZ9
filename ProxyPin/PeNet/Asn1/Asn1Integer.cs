using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using PeNet.Asn1.Utils;

namespace PeNet.Asn1
{
	// Token: 0x02000B77 RID: 2935
	[ComVisible(true)]
	public class Asn1Integer : Asn1Node
	{
		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06007626 RID: 30246 RVA: 0x00236AB8 File Offset: 0x00236AB8
		// (set) Token: 0x06007627 RID: 30247 RVA: 0x00236AC0 File Offset: 0x00236AC0
		public byte[] Value { get; set; }

		// Token: 0x06007628 RID: 30248 RVA: 0x00236ACC File Offset: 0x00236ACC
		public Asn1Integer(long value)
		{
			bool flag = true;
			List<byte> list = new List<byte>();
			for (int i = 7; i >= 0; i--)
			{
				long num = value >> i * 8;
				if (!flag || num != 0L)
				{
					flag = false;
					list.Add((byte)(num & 255L));
				}
			}
			this.Value = ((list.Count > 0) ? list.ToArray() : new byte[1]);
		}

		// Token: 0x06007629 RID: 30249 RVA: 0x00236B44 File Offset: 0x00236B44
		public Asn1Integer(byte[] data)
		{
			this.Value = data.ToArray<byte>();
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x00236B58 File Offset: 0x00236B58
		public ulong ToUInt64()
		{
			ulong num = 0UL;
			for (int i = 0; i < this.Value.Length; i++)
			{
				num <<= 8;
				num |= (ulong)this.Value[i];
			}
			return num;
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x00236B94 File Offset: 0x00236B94
		public static Asn1Integer ReadFrom(Stream stream)
		{
			List<byte> list = new List<byte>();
			while (stream.Position < stream.Length)
			{
				list.Add((byte)stream.ReadByte());
			}
			return new Asn1Integer(list.ToArray());
		}

		// Token: 0x0600762C RID: 30252 RVA: 0x00236BD8 File Offset: 0x00236BD8
		public static Asn1Integer FromHexString(string val)
		{
			return new Asn1Integer(StringUtils.GetBytesFromHexString(val));
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x00236BE8 File Offset: 0x00236BE8
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Integer;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x0600762E RID: 30254 RVA: 0x00236BEC File Offset: 0x00236BEC
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x0600762F RID: 30255 RVA: 0x00236BF0 File Offset: 0x00236BF0
		protected override XElement ToXElementCore()
		{
			return new XElement("Integer", string.Join("", from v in this.Value
			select v.ToString("x2")));
		}

		// Token: 0x06007630 RID: 30256 RVA: 0x00236C48 File Offset: 0x00236C48
		protected override byte[] GetBytesCore()
		{
			return this.Value.ToArray<byte>();
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x00236C58 File Offset: 0x00236C58
		public new static Asn1Integer Parse(XElement xNode)
		{
			List<byte> list = new List<byte>();
			string text = xNode.Value.Trim();
			for (int i = 0; i < text.Length; i += 2)
			{
				byte item = byte.Parse(text.Substring(i, 2), NumberStyles.HexNumber);
				list.Add(item);
			}
			return new Asn1Integer(list.ToArray());
		}

		// Token: 0x04003966 RID: 14694
		public const string NODE_NAME = "Integer";
	}
}
