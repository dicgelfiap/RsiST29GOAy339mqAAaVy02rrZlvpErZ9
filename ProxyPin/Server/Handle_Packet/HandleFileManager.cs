using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Connection;
using Server.Helper;
using Server.MessagePack;

namespace Server.Handle_Packet
{
	// Token: 0x02000043 RID: 67
	public class HandleFileManager
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00015A9C File Offset: 0x00015A9C
		public void FileManager(Clients client, MsgPack unpack_msgpack)
		{
			HandleFileManager.<FileManager>d__0 <FileManager>d__;
			<FileManager>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<FileManager>d__.<>4__this = this;
			<FileManager>d__.client = client;
			<FileManager>d__.unpack_msgpack = unpack_msgpack;
			<FileManager>d__.<>1__state = -1;
			<FileManager>d__.<>t__builder.Start<HandleFileManager.<FileManager>d__0>(ref <FileManager>d__);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00015AE8 File Offset: 0x00015AE8
		public void SocketDownload(Clients client, MsgPack unpack_msgpack)
		{
			HandleFileManager.<SocketDownload>d__1 <SocketDownload>d__;
			<SocketDownload>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SocketDownload>d__.<>4__this = this;
			<SocketDownload>d__.client = client;
			<SocketDownload>d__.unpack_msgpack = unpack_msgpack;
			<SocketDownload>d__.<>1__state = -1;
			<SocketDownload>d__.<>t__builder.Start<HandleFileManager.<SocketDownload>d__1>(ref <SocketDownload>d__);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00015B34 File Offset: 0x00015B34
		private Task SaveFileAsync(MsgPack unpack_msgpack, string name)
		{
			HandleFileManager.<SaveFileAsync>d__2 <SaveFileAsync>d__;
			<SaveFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveFileAsync>d__.unpack_msgpack = unpack_msgpack;
			<SaveFileAsync>d__.name = name;
			<SaveFileAsync>d__.<>1__state = -1;
			<SaveFileAsync>d__.<>t__builder.Start<HandleFileManager.<SaveFileAsync>d__2>(ref <SaveFileAsync>d__);
			return <SaveFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00015B84 File Offset: 0x00015B84
		private List<ListViewItem> GetFolders(MsgPack unpack_msgpack, ListViewGroup listViewGroup)
		{
			string[] array = unpack_msgpack.ForcePathObject("Folder").AsString.Split(new string[]
			{
				"-=>"
			}, StringSplitOptions.None);
			List<ListViewItem> list = new List<ListViewItem>();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length > 0)
				{
					list.Add(new ListViewItem
					{
						Text = array[i],
						ToolTipText = array[i + 1],
						Group = listViewGroup,
						ImageIndex = 0
					});
					num++;
				}
				i++;
			}
			return list;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00015C2C File Offset: 0x00015C2C
		private List<ListViewItem> GetFiles(MsgPack unpack_msgpack, ListViewGroup listViewGroup, ImageList imageList1)
		{
			string[] _files = unpack_msgpack.ForcePathObject("File").AsString.Split(new string[]
			{
				"-=>"
			}, StringSplitOptions.None);
			List<ListViewItem> list = new List<ListViewItem>();
			int i;
			int j;
			for (i = 0; i < _files.Length; i = j + 1)
			{
				if (_files[i].Length > 0)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = Path.GetFileName(_files[i]);
					listViewItem.ToolTipText = _files[i + 1];
					Image im = Image.FromStream(new MemoryStream(Convert.FromBase64String(_files[i + 2])));
					Program.form1.Invoke(new MethodInvoker(delegate()
					{
						imageList1.Images.Add(_files[i + 1], im);
					}));
					listViewItem.ImageKey = _files[i + 1];
					listViewItem.Group = listViewGroup;
					listViewItem.SubItems.Add(Methods.BytesToString(Convert.ToInt64(_files[i + 3])));
					list.Add(listViewItem);
				}
				i += 3;
				j = i;
			}
			return list;
		}
	}
}
