using System;

namespace FMOD
{
	// Token: 0x02000057 RID: 87
	public struct CREATESOUNDEXINFO
	{
		// Token: 0x04000235 RID: 565
		public int cbsize;

		// Token: 0x04000236 RID: 566
		public uint length;

		// Token: 0x04000237 RID: 567
		public uint fileoffset;

		// Token: 0x04000238 RID: 568
		public int numchannels;

		// Token: 0x04000239 RID: 569
		public int defaultfrequency;

		// Token: 0x0400023A RID: 570
		public SOUND_FORMAT format;

		// Token: 0x0400023B RID: 571
		public uint decodebuffersize;

		// Token: 0x0400023C RID: 572
		public int initialsubsound;

		// Token: 0x0400023D RID: 573
		public int numsubsounds;

		// Token: 0x0400023E RID: 574
		public IntPtr inclusionlist;

		// Token: 0x0400023F RID: 575
		public int inclusionlistnum;

		// Token: 0x04000240 RID: 576
		public SOUND_PCMREADCALLBACK pcmreadcallback;

		// Token: 0x04000241 RID: 577
		public SOUND_PCMSETPOSCALLBACK pcmsetposcallback;

		// Token: 0x04000242 RID: 578
		public SOUND_NONBLOCKCALLBACK nonblockcallback;

		// Token: 0x04000243 RID: 579
		public IntPtr dlsname;

		// Token: 0x04000244 RID: 580
		public IntPtr encryptionkey;

		// Token: 0x04000245 RID: 581
		public int maxpolyphony;

		// Token: 0x04000246 RID: 582
		public IntPtr userdata;

		// Token: 0x04000247 RID: 583
		public SOUND_TYPE suggestedsoundtype;

		// Token: 0x04000248 RID: 584
		public FILE_OPENCALLBACK fileuseropen;

		// Token: 0x04000249 RID: 585
		public FILE_CLOSECALLBACK fileuserclose;

		// Token: 0x0400024A RID: 586
		public FILE_READCALLBACK fileuserread;

		// Token: 0x0400024B RID: 587
		public FILE_SEEKCALLBACK fileuserseek;

		// Token: 0x0400024C RID: 588
		public FILE_ASYNCREADCALLBACK fileuserasyncread;

		// Token: 0x0400024D RID: 589
		public FILE_ASYNCCANCELCALLBACK fileuserasynccancel;

		// Token: 0x0400024E RID: 590
		public IntPtr fileuserdata;

		// Token: 0x0400024F RID: 591
		public int filebuffersize;

		// Token: 0x04000250 RID: 592
		public CHANNELORDER channelorder;

		// Token: 0x04000251 RID: 593
		public CHANNELMASK channelmask;

		// Token: 0x04000252 RID: 594
		public IntPtr initialsoundgroup;

		// Token: 0x04000253 RID: 595
		public uint initialseekposition;

		// Token: 0x04000254 RID: 596
		public TIMEUNIT initialseekpostype;

		// Token: 0x04000255 RID: 597
		public int ignoresetfilesystem;

		// Token: 0x04000256 RID: 598
		public uint audioqueuepolicy;

		// Token: 0x04000257 RID: 599
		public uint minmidigranularity;

		// Token: 0x04000258 RID: 600
		public int nonblockthreadid;

		// Token: 0x04000259 RID: 601
		public IntPtr fsbguid;
	}
}
