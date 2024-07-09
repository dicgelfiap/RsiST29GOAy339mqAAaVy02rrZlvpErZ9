using System;
using System.Runtime.InteropServices;

namespace PeNet
{
	// Token: 0x02000B8F RID: 2959
	[ComVisible(true)]
	public class FileCharacteristics
	{
		// Token: 0x060076F6 RID: 30454 RVA: 0x002390A0 File Offset: 0x002390A0
		public FileCharacteristics(ushort characteristics)
		{
			if ((characteristics & 1) > 0)
			{
				this.RelocStripped = true;
			}
			if ((characteristics & 2) > 0)
			{
				this.ExecutableImage = true;
			}
			if ((characteristics & 4) > 0)
			{
				this.LineNumbersStripped = true;
			}
			if ((characteristics & 8) > 0)
			{
				this.LocalSymbolsStripped = true;
			}
			if ((characteristics & 16) > 0)
			{
				this.AggressiveWsTrim = true;
			}
			if ((characteristics & 32) > 0)
			{
				this.LargeAddressAware = true;
			}
			if ((characteristics & 128) > 0)
			{
				this.BytesReversedLo = true;
			}
			if ((characteristics & 256) > 0)
			{
				this.Machine32Bit = true;
			}
			if ((characteristics & 512) > 0)
			{
				this.DebugStripped = true;
			}
			if ((characteristics & 1024) > 0)
			{
				this.RemovableRunFromSwap = true;
			}
			if ((characteristics & 2048) > 0)
			{
				this.NetRunFromSwap = true;
			}
			if ((characteristics & 4096) > 0)
			{
				this.System = true;
			}
			if ((characteristics & 8192) > 0)
			{
				this.DLL = true;
			}
			if ((characteristics & 16384) > 0)
			{
				this.UpSystemOnly = true;
			}
			if ((characteristics & 32768) > 0)
			{
				this.BytesReversedHi = true;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x060076F7 RID: 30455 RVA: 0x002391D0 File Offset: 0x002391D0
		// (set) Token: 0x060076F8 RID: 30456 RVA: 0x002391D8 File Offset: 0x002391D8
		public bool RelocStripped { get; private set; }

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060076F9 RID: 30457 RVA: 0x002391E4 File Offset: 0x002391E4
		// (set) Token: 0x060076FA RID: 30458 RVA: 0x002391EC File Offset: 0x002391EC
		public bool ExecutableImage { get; private set; }

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060076FB RID: 30459 RVA: 0x002391F8 File Offset: 0x002391F8
		// (set) Token: 0x060076FC RID: 30460 RVA: 0x00239200 File Offset: 0x00239200
		public bool LineNumbersStripped { get; private set; }

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x060076FD RID: 30461 RVA: 0x0023920C File Offset: 0x0023920C
		// (set) Token: 0x060076FE RID: 30462 RVA: 0x00239214 File Offset: 0x00239214
		public bool LocalSymbolsStripped { get; private set; }

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x060076FF RID: 30463 RVA: 0x00239220 File Offset: 0x00239220
		// (set) Token: 0x06007700 RID: 30464 RVA: 0x00239228 File Offset: 0x00239228
		public bool AggressiveWsTrim { get; private set; }

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06007701 RID: 30465 RVA: 0x00239234 File Offset: 0x00239234
		// (set) Token: 0x06007702 RID: 30466 RVA: 0x0023923C File Offset: 0x0023923C
		public bool LargeAddressAware { get; private set; }

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06007703 RID: 30467 RVA: 0x00239248 File Offset: 0x00239248
		// (set) Token: 0x06007704 RID: 30468 RVA: 0x00239250 File Offset: 0x00239250
		public bool BytesReversedLo { get; private set; }

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06007705 RID: 30469 RVA: 0x0023925C File Offset: 0x0023925C
		// (set) Token: 0x06007706 RID: 30470 RVA: 0x00239264 File Offset: 0x00239264
		public bool Machine32Bit { get; private set; }

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06007707 RID: 30471 RVA: 0x00239270 File Offset: 0x00239270
		// (set) Token: 0x06007708 RID: 30472 RVA: 0x00239278 File Offset: 0x00239278
		public bool DebugStripped { get; private set; }

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06007709 RID: 30473 RVA: 0x00239284 File Offset: 0x00239284
		// (set) Token: 0x0600770A RID: 30474 RVA: 0x0023928C File Offset: 0x0023928C
		public bool RemovableRunFromSwap { get; private set; }

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600770B RID: 30475 RVA: 0x00239298 File Offset: 0x00239298
		// (set) Token: 0x0600770C RID: 30476 RVA: 0x002392A0 File Offset: 0x002392A0
		public bool NetRunFromSwap { get; private set; }

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x0600770D RID: 30477 RVA: 0x002392AC File Offset: 0x002392AC
		// (set) Token: 0x0600770E RID: 30478 RVA: 0x002392B4 File Offset: 0x002392B4
		public bool System { get; private set; }

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600770F RID: 30479 RVA: 0x002392C0 File Offset: 0x002392C0
		// (set) Token: 0x06007710 RID: 30480 RVA: 0x002392C8 File Offset: 0x002392C8
		public bool DLL { get; private set; }

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06007711 RID: 30481 RVA: 0x002392D4 File Offset: 0x002392D4
		// (set) Token: 0x06007712 RID: 30482 RVA: 0x002392DC File Offset: 0x002392DC
		public bool UpSystemOnly { get; private set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06007713 RID: 30483 RVA: 0x002392E8 File Offset: 0x002392E8
		// (set) Token: 0x06007714 RID: 30484 RVA: 0x002392F0 File Offset: 0x002392F0
		public bool BytesReversedHi { get; private set; }
	}
}
