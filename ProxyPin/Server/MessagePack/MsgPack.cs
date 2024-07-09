using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Server.Algorithm;

namespace Server.MessagePack
{
	// Token: 0x02000019 RID: 25
	public class MsgPack : IEnumerable
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000D054 File Offset: 0x0000D054
		private void SetName(string value)
		{
			this.name = value;
			this.lowerName = this.name.ToLower();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000D070 File Offset: 0x0000D070
		private void Clear()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].Clear();
			}
			this.children.Clear();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000D0B8 File Offset: 0x0000D0B8
		private MsgPack InnerAdd()
		{
			MsgPack msgPack = new MsgPack();
			msgPack.parent = this;
			this.children.Add(msgPack);
			return msgPack;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000D0E4 File Offset: 0x0000D0E4
		private int IndexOf(string name)
		{
			int num = -1;
			int result = -1;
			string text = name.ToLower();
			foreach (MsgPack msgPack in this.children)
			{
				num++;
				if (text.Equals(msgPack.lowerName))
				{
					result = num;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000D160 File Offset: 0x0000D160
		public MsgPack FindObject(string name)
		{
			int num = this.IndexOf(name);
			if (num == -1)
			{
				return null;
			}
			return this.children[num];
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000D190 File Offset: 0x0000D190
		private MsgPack InnerAddMapChild()
		{
			if (this.valueType != MsgPackType.Map)
			{
				this.Clear();
				this.valueType = MsgPackType.Map;
			}
			return this.InnerAdd();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000D1B4 File Offset: 0x0000D1B4
		private MsgPack InnerAddArrayChild()
		{
			if (this.valueType != MsgPackType.Array)
			{
				this.Clear();
				this.valueType = MsgPackType.Array;
			}
			return this.InnerAdd();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000D1D8 File Offset: 0x0000D1D8
		public MsgPack AddArrayChild()
		{
			return this.InnerAddArrayChild();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D1E0 File Offset: 0x0000D1E0
		private void WriteMap(Stream ms)
		{
			int count = this.children.Count;
			if (count <= 15)
			{
				byte value = 128 + (byte)count;
				ms.WriteByte(value);
			}
			else if (count <= 65535)
			{
				byte value = 222;
				ms.WriteByte(value);
				byte[] array = BytesTools.SwapBytes(BitConverter.GetBytes((short)count));
				ms.Write(array, 0, array.Length);
			}
			else
			{
				byte value = 223;
				ms.WriteByte(value);
				byte[] array = BytesTools.SwapBytes(BitConverter.GetBytes(count));
				ms.Write(array, 0, array.Length);
			}
			for (int i = 0; i < count; i++)
			{
				WriteTools.WriteString(ms, this.children[i].name);
				this.children[i].Encode2Stream(ms);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D2AC File Offset: 0x0000D2AC
		private void WirteArray(Stream ms)
		{
			int count = this.children.Count;
			if (count <= 15)
			{
				byte value = 144 + (byte)count;
				ms.WriteByte(value);
			}
			else if (count <= 65535)
			{
				byte value = 220;
				ms.WriteByte(value);
				byte[] array = BytesTools.SwapBytes(BitConverter.GetBytes((short)count));
				ms.Write(array, 0, array.Length);
			}
			else
			{
				byte value = 221;
				ms.WriteByte(value);
				byte[] array = BytesTools.SwapBytes(BitConverter.GetBytes(count));
				ms.Write(array, 0, array.Length);
			}
			for (int i = 0; i < count; i++)
			{
				this.children[i].Encode2Stream(ms);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000D364 File Offset: 0x0000D364
		public void SetAsInteger(long value)
		{
			this.innerValue = value;
			this.valueType = MsgPackType.Integer;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000D37C File Offset: 0x0000D37C
		public void SetAsUInt64(ulong value)
		{
			this.innerValue = value;
			this.valueType = MsgPackType.UInt64;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000D394 File Offset: 0x0000D394
		public ulong GetAsUInt64()
		{
			switch (this.valueType)
			{
			case MsgPackType.String:
				return ulong.Parse(this.innerValue.ToString().Trim());
			case MsgPackType.Integer:
				return Convert.ToUInt64((long)this.innerValue);
			case MsgPackType.UInt64:
				return (ulong)this.innerValue;
			case MsgPackType.Float:
				return Convert.ToUInt64((double)this.innerValue);
			case MsgPackType.Single:
				return Convert.ToUInt64((float)this.innerValue);
			case MsgPackType.DateTime:
				return Convert.ToUInt64((DateTime)this.innerValue);
			}
			return 0UL;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000D440 File Offset: 0x0000D440
		public long GetAsInteger()
		{
			switch (this.valueType)
			{
			case MsgPackType.String:
				return long.Parse(this.innerValue.ToString().Trim());
			case MsgPackType.Integer:
				return (long)this.innerValue;
			case MsgPackType.UInt64:
				return Convert.ToInt64((long)this.innerValue);
			case MsgPackType.Float:
				return Convert.ToInt64((double)this.innerValue);
			case MsgPackType.Single:
				return Convert.ToInt64((float)this.innerValue);
			case MsgPackType.DateTime:
				return Convert.ToInt64((DateTime)this.innerValue);
			}
			return 0L;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000D4EC File Offset: 0x0000D4EC
		public double GetAsFloat()
		{
			switch (this.valueType)
			{
			case MsgPackType.String:
				return double.Parse((string)this.innerValue);
			case MsgPackType.Integer:
				return Convert.ToDouble((long)this.innerValue);
			case MsgPackType.Float:
				return (double)this.innerValue;
			case MsgPackType.Single:
				return (double)((float)this.innerValue);
			case MsgPackType.DateTime:
				return (double)Convert.ToInt64((DateTime)this.innerValue);
			}
			return 0.0;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000D584 File Offset: 0x0000D584
		public void SetAsBytes(byte[] value)
		{
			this.innerValue = value;
			this.valueType = MsgPackType.Binary;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000D598 File Offset: 0x0000D598
		public byte[] GetAsBytes()
		{
			switch (this.valueType)
			{
			case MsgPackType.String:
				return BytesTools.GetUtf8Bytes(this.innerValue.ToString());
			case MsgPackType.Integer:
				return BitConverter.GetBytes((long)this.innerValue);
			case MsgPackType.Float:
				return BitConverter.GetBytes((double)this.innerValue);
			case MsgPackType.Single:
				return BitConverter.GetBytes((float)this.innerValue);
			case MsgPackType.DateTime:
				return BitConverter.GetBytes(((DateTime)this.innerValue).ToBinary());
			case MsgPackType.Binary:
				return (byte[])this.innerValue;
			}
			return new byte[0];
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000D64C File Offset: 0x0000D64C
		public void Add(string key, string value)
		{
			MsgPack msgPack = this.InnerAddArrayChild();
			msgPack.name = key;
			msgPack.SetAsString(value);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000D664 File Offset: 0x0000D664
		public void Add(string key, int value)
		{
			MsgPack msgPack = this.InnerAddArrayChild();
			msgPack.name = key;
			msgPack.SetAsInteger((long)value);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000D67C File Offset: 0x0000D67C
		public Task<bool> LoadFileAsBytes(string fileName)
		{
			MsgPack.<LoadFileAsBytes>d__26 <LoadFileAsBytes>d__;
			<LoadFileAsBytes>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LoadFileAsBytes>d__.<>4__this = this;
			<LoadFileAsBytes>d__.fileName = fileName;
			<LoadFileAsBytes>d__.<>1__state = -1;
			<LoadFileAsBytes>d__.<>t__builder.Start<MsgPack.<LoadFileAsBytes>d__26>(ref <LoadFileAsBytes>d__);
			return <LoadFileAsBytes>d__.<>t__builder.Task;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000D6CC File Offset: 0x0000D6CC
		public Task<bool> SaveBytesToFile(string fileName)
		{
			MsgPack.<SaveBytesToFile>d__27 <SaveBytesToFile>d__;
			<SaveBytesToFile>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SaveBytesToFile>d__.<>4__this = this;
			<SaveBytesToFile>d__.fileName = fileName;
			<SaveBytesToFile>d__.<>1__state = -1;
			<SaveBytesToFile>d__.<>t__builder.Start<MsgPack.<SaveBytesToFile>d__27>(ref <SaveBytesToFile>d__);
			return <SaveBytesToFile>d__.<>t__builder.Task;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000D71C File Offset: 0x0000D71C
		public MsgPack ForcePathObject(string path)
		{
			MsgPack msgPack = this;
			string[] array = path.Trim().Split(new char[]
			{
				'.',
				'/',
				'\\'
			});
			if (array.Length == 0)
			{
				return null;
			}
			string text;
			if (array.Length > 1)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					text = array[i];
					MsgPack msgPack2 = msgPack.FindObject(text);
					if (msgPack2 == null)
					{
						msgPack = msgPack.InnerAddMapChild();
						msgPack.SetName(text);
					}
					else
					{
						msgPack = msgPack2;
					}
				}
			}
			text = array[array.Length - 1];
			int num = msgPack.IndexOf(text);
			if (num > -1)
			{
				return msgPack.children[num];
			}
			msgPack = msgPack.InnerAddMapChild();
			msgPack.SetName(text);
			return msgPack;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000D7DC File Offset: 0x0000D7DC
		public void SetAsNull()
		{
			this.Clear();
			this.innerValue = null;
			this.valueType = MsgPackType.Null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000D7F4 File Offset: 0x0000D7F4
		public void SetAsString(string value)
		{
			this.innerValue = value;
			this.valueType = MsgPackType.String;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000D804 File Offset: 0x0000D804
		public string GetAsString()
		{
			if (this.innerValue == null)
			{
				return "";
			}
			return this.innerValue.ToString();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000D824 File Offset: 0x0000D824
		public void SetAsBoolean(bool bVal)
		{
			this.valueType = MsgPackType.Boolean;
			this.innerValue = bVal;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000D83C File Offset: 0x0000D83C
		public void SetAsSingle(float fVal)
		{
			this.valueType = MsgPackType.Single;
			this.innerValue = fVal;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000D854 File Offset: 0x0000D854
		public void SetAsFloat(double fVal)
		{
			this.valueType = MsgPackType.Float;
			this.innerValue = fVal;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000D86C File Offset: 0x0000D86C
		public void DecodeFromBytes(byte[] bytes)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bytes = Zip.Decompress(bytes);
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Position = 0L;
				this.DecodeFromStream(memoryStream);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000D8C4 File Offset: 0x0000D8C4
		public void DecodeFromFile(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open);
			this.DecodeFromStream(fileStream);
			fileStream.Dispose();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000D8EC File Offset: 0x0000D8EC
		public void DecodeFromStream(Stream ms)
		{
			byte b = (byte)ms.ReadByte();
			if (b <= 127)
			{
				this.SetAsInteger((long)((ulong)b));
				return;
			}
			if (b >= 128 && b <= 143)
			{
				this.Clear();
				this.valueType = MsgPackType.Map;
				int num = (int)(b - 128);
				for (int i = 0; i < num; i++)
				{
					MsgPack msgPack = this.InnerAdd();
					msgPack.SetName(ReadTools.ReadString(ms));
					msgPack.DecodeFromStream(ms);
				}
				return;
			}
			if (b >= 144 && b <= 159)
			{
				this.Clear();
				this.valueType = MsgPackType.Array;
				int num = (int)(b - 144);
				for (int i = 0; i < num; i++)
				{
					this.InnerAdd().DecodeFromStream(ms);
				}
				return;
			}
			if (b >= 160 && b <= 191)
			{
				int num = (int)(b - 160);
				this.SetAsString(ReadTools.ReadString(ms, num));
				return;
			}
			if (b >= 224 && b <= 255)
			{
				this.SetAsInteger((long)((sbyte)b));
				return;
			}
			if (b == 192)
			{
				this.SetAsNull();
				return;
			}
			if (b == 193)
			{
				throw new Exception("(never used) type $c1");
			}
			if (b == 194)
			{
				this.SetAsBoolean(false);
				return;
			}
			if (b == 195)
			{
				this.SetAsBoolean(true);
				return;
			}
			if (b == 196)
			{
				int num = ms.ReadByte();
				byte[] array = new byte[num];
				ms.Read(array, 0, num);
				this.SetAsBytes(array);
				return;
			}
			if (b == 197)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				int num = (int)BitConverter.ToUInt16(array, 0);
				array = new byte[num];
				ms.Read(array, 0, num);
				this.SetAsBytes(array);
				return;
			}
			if (b == 198)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				int num = BitConverter.ToInt32(array, 0);
				array = new byte[num];
				ms.Read(array, 0, num);
				this.SetAsBytes(array);
				return;
			}
			if (b == 199 || b == 200 || b == 201)
			{
				throw new Exception("(ext8,ext16,ex32) type $c7,$c8,$c9");
			}
			if (b == 202)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				this.SetAsSingle(BitConverter.ToSingle(array, 0));
				return;
			}
			if (b == 203)
			{
				byte[] array = new byte[8];
				ms.Read(array, 0, 8);
				array = BytesTools.SwapBytes(array);
				this.SetAsFloat(BitConverter.ToDouble(array, 0));
				return;
			}
			if (b == 204)
			{
				b = (byte)ms.ReadByte();
				this.SetAsInteger((long)((ulong)b));
				return;
			}
			if (b == 205)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				this.SetAsInteger((long)((ulong)BitConverter.ToUInt16(array, 0)));
				return;
			}
			if (b == 206)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				this.SetAsInteger((long)((ulong)BitConverter.ToUInt32(array, 0)));
				return;
			}
			if (b == 207)
			{
				byte[] array = new byte[8];
				ms.Read(array, 0, 8);
				array = BytesTools.SwapBytes(array);
				this.SetAsUInt64(BitConverter.ToUInt64(array, 0));
				return;
			}
			if (b == 220)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				int num = (int)BitConverter.ToInt16(array, 0);
				this.Clear();
				this.valueType = MsgPackType.Array;
				for (int i = 0; i < num; i++)
				{
					this.InnerAdd().DecodeFromStream(ms);
				}
				return;
			}
			if (b == 221)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				int num = (int)BitConverter.ToInt16(array, 0);
				this.Clear();
				this.valueType = MsgPackType.Array;
				for (int i = 0; i < num; i++)
				{
					this.InnerAdd().DecodeFromStream(ms);
				}
				return;
			}
			if (b == 217)
			{
				this.SetAsString(ReadTools.ReadString(b, ms));
				return;
			}
			if (b == 222)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				int num = (int)BitConverter.ToInt16(array, 0);
				this.Clear();
				this.valueType = MsgPackType.Map;
				for (int i = 0; i < num; i++)
				{
					MsgPack msgPack2 = this.InnerAdd();
					msgPack2.SetName(ReadTools.ReadString(ms));
					msgPack2.DecodeFromStream(ms);
				}
				return;
			}
			if (b == 222)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				int num = (int)BitConverter.ToInt16(array, 0);
				this.Clear();
				this.valueType = MsgPackType.Map;
				for (int i = 0; i < num; i++)
				{
					MsgPack msgPack3 = this.InnerAdd();
					msgPack3.SetName(ReadTools.ReadString(ms));
					msgPack3.DecodeFromStream(ms);
				}
				return;
			}
			if (b == 223)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				int num = BitConverter.ToInt32(array, 0);
				this.Clear();
				this.valueType = MsgPackType.Map;
				for (int i = 0; i < num; i++)
				{
					MsgPack msgPack4 = this.InnerAdd();
					msgPack4.SetName(ReadTools.ReadString(ms));
					msgPack4.DecodeFromStream(ms);
				}
				return;
			}
			if (b == 218)
			{
				this.SetAsString(ReadTools.ReadString(b, ms));
				return;
			}
			if (b == 219)
			{
				this.SetAsString(ReadTools.ReadString(b, ms));
				return;
			}
			if (b == 208)
			{
				this.SetAsInteger((long)((sbyte)ms.ReadByte()));
				return;
			}
			if (b == 209)
			{
				byte[] array = new byte[2];
				ms.Read(array, 0, 2);
				array = BytesTools.SwapBytes(array);
				this.SetAsInteger((long)BitConverter.ToInt16(array, 0));
				return;
			}
			if (b == 210)
			{
				byte[] array = new byte[4];
				ms.Read(array, 0, 4);
				array = BytesTools.SwapBytes(array);
				this.SetAsInteger((long)BitConverter.ToInt32(array, 0));
				return;
			}
			if (b == 211)
			{
				byte[] array = new byte[8];
				ms.Read(array, 0, 8);
				array = BytesTools.SwapBytes(array);
				this.SetAsInteger(BitConverter.ToInt64(array, 0));
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000DF04 File Offset: 0x0000DF04
		public byte[] Encode2Bytes()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.Encode2Stream(memoryStream);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Position = 0L;
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				result = Zip.Compress(array);
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000DF70 File Offset: 0x0000DF70
		public void Encode2Stream(Stream ms)
		{
			switch (this.valueType)
			{
			case MsgPackType.Unknown:
			case MsgPackType.Null:
				WriteTools.WriteNull(ms);
				return;
			case MsgPackType.Map:
				this.WriteMap(ms);
				return;
			case MsgPackType.Array:
				this.WirteArray(ms);
				return;
			case MsgPackType.String:
				WriteTools.WriteString(ms, (string)this.innerValue);
				return;
			case MsgPackType.Integer:
				WriteTools.WriteInteger(ms, (long)this.innerValue);
				return;
			case MsgPackType.UInt64:
				WriteTools.WriteUInt64(ms, (ulong)this.innerValue);
				return;
			case MsgPackType.Boolean:
				WriteTools.WriteBoolean(ms, (bool)this.innerValue);
				return;
			case MsgPackType.Float:
				WriteTools.WriteFloat(ms, (double)this.innerValue);
				return;
			case MsgPackType.Single:
				WriteTools.WriteFloat(ms, (double)((float)this.innerValue));
				return;
			case MsgPackType.DateTime:
				WriteTools.WriteInteger(ms, this.GetAsInteger());
				return;
			case MsgPackType.Binary:
				WriteTools.WriteBinary(ms, (byte[])this.innerValue);
				return;
			default:
				WriteTools.WriteNull(ms);
				return;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000E06C File Offset: 0x0000E06C
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000E074 File Offset: 0x0000E074
		public string AsString
		{
			get
			{
				return this.GetAsString();
			}
			set
			{
				this.SetAsString(value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000E080 File Offset: 0x0000E080
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000E088 File Offset: 0x0000E088
		public long AsInteger
		{
			get
			{
				return this.GetAsInteger();
			}
			set
			{
				this.SetAsInteger(value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000E094 File Offset: 0x0000E094
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000E09C File Offset: 0x0000E09C
		public double AsFloat
		{
			get
			{
				return this.GetAsFloat();
			}
			set
			{
				this.SetAsFloat(value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000E0A8 File Offset: 0x0000E0A8
		public MsgPackArray AsArray
		{
			get
			{
				lock (this)
				{
					if (this.refAsArray == null)
					{
						this.refAsArray = new MsgPackArray(this, this.children);
					}
				}
				return this.refAsArray;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000E108 File Offset: 0x0000E108
		public MsgPackType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000E110 File Offset: 0x0000E110
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MsgPackEnum(this.children);
		}

		// Token: 0x040000CD RID: 205
		private string name;

		// Token: 0x040000CE RID: 206
		private string lowerName;

		// Token: 0x040000CF RID: 207
		private object innerValue;

		// Token: 0x040000D0 RID: 208
		private MsgPackType valueType;

		// Token: 0x040000D1 RID: 209
		private MsgPack parent;

		// Token: 0x040000D2 RID: 210
		private List<MsgPack> children = new List<MsgPack>();

		// Token: 0x040000D3 RID: 211
		private MsgPackArray refAsArray;
	}
}
