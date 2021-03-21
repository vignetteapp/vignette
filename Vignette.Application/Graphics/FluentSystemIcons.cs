// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;

namespace Vignette.Application.Graphics
{
    public static class FluentSystemIcons
    {
        public static string Family => "FluentSystemIcons";

        public static IconGroup Filled => new IconGroup("Filled");

        public class IconGroup
        {
            private string weight;

            public IconGroup(string weight)
            {
                this.weight = weight;
            }

            public IconUsage Get(int icon) => new IconUsage((char)icon, Family, weight);

            public IconUsage AccessTime24 => Get(0xf101);

            public IconUsage Accessibility16 => Get(0xf102);

            public IconUsage Accessibility20 => Get(0xf103);

            public IconUsage Accessibility24 => Get(0xf104);

            public IconUsage Accessibility28 => Get(0xf105);

            public IconUsage Activity24 => Get(0xf106);

            public IconUsage Add12 => Get(0xf107);

            public IconUsage Add16 => Get(0xf108);

            public IconUsage Add20 => Get(0xf109);

            public IconUsage Add24 => Get(0xf10a);

            public IconUsage Add28 => Get(0xf10b);

            public IconUsage AddCircle20 => Get(0xf10c);

            public IconUsage AddCircle24 => Get(0xf10d);

            public IconUsage AddCircle28 => Get(0xf10e);

            public IconUsage Airplane20 => Get(0xf10f);

            public IconUsage Airplane24 => Get(0xf110);

            public IconUsage AirplaneTakeOff16 => Get(0xf111);

            public IconUsage AirplaneTakeOff20 => Get(0xf112);

            public IconUsage AirplaneTakeOff24 => Get(0xf113);

            public IconUsage Alert20 => Get(0xf114);

            public IconUsage Alert24 => Get(0xf115);

            public IconUsage Alert28 => Get(0xf116);

            public IconUsage AlertOff16 => Get(0xf117);

            public IconUsage AlertOff20 => Get(0xf118);

            public IconUsage AlertOff24 => Get(0xf119);

            public IconUsage AlertOff28 => Get(0xf11a);

            public IconUsage AlertOn24 => Get(0xf11b);

            public IconUsage AlertSnooze20 => Get(0xf11c);

            public IconUsage AlertSnooze24 => Get(0xf11d);

            public IconUsage AlertUrgent20 => Get(0xf11e);

            public IconUsage AlertUrgent24 => Get(0xf11f);

            public IconUsage AnimalDog20 => Get(0xf120);

            public IconUsage AnimalDog24 => Get(0xf121);

            public IconUsage AppFolder20 => Get(0xf122);

            public IconUsage AppFolder24 => Get(0xf123);

            public IconUsage AppGeneric24 => Get(0xf124);

            public IconUsage AppRecent24 => Get(0xf125);

            public IconUsage AppSpan16 => Get(0xf126);

            public IconUsage AppSpan20 => Get(0xf127);

            public IconUsage AppSpan24 => Get(0xf128);

            public IconUsage AppSpan28 => Get(0xf129);

            public IconUsage AppStore24 => Get(0xf12a);

            public IconUsage AppTitle24 => Get(0xf12b);

            public IconUsage AppUnspan16 => Get(0xf12c);

            public IconUsage AppUnspan20 => Get(0xf12d);

            public IconUsage AppUnspan24 => Get(0xf12e);

            public IconUsage AppUnspan28 => Get(0xf12f);

            public IconUsage ApprovalsApp24 => Get(0xf130);

            public IconUsage ApprovalsApp28 => Get(0xf131);

            public IconUsage Apps16 => Get(0xf132);

            public IconUsage Apps20 => Get(0xf133);

            public IconUsage Apps24 => Get(0xf134);

            public IconUsage Apps28 => Get(0xf135);

            public IconUsage AppsAddIn20 => Get(0xf136);

            public IconUsage AppsAddIn24 => Get(0xf137);

            public IconUsage AppsList24 => Get(0xf138);

            public IconUsage Archive20 => Get(0xf139);

            public IconUsage Archive24 => Get(0xf13a);

            public IconUsage Archive28 => Get(0xf13b);

            public IconUsage Archive48 => Get(0xf13c);

            public IconUsage ArrowClockwise20 => Get(0xf13d);

            public IconUsage ArrowClockwise24 => Get(0xf13e);

            public IconUsage ArrowCounterclockwise20 => Get(0xf13f);

            public IconUsage ArrowCounterclockwise24 => Get(0xf140);

            public IconUsage ArrowCurveDownLeft20 => Get(0xf141);

            public IconUsage ArrowCurveDownRight20 => Get(0xf142);

            public IconUsage ArrowCurveRight20 => Get(0xf143);

            public IconUsage ArrowCurveRight24 => Get(0xf144);

            public IconUsage ArrowCurveUpLeft20 => Get(0xf145);

            public IconUsage ArrowCurveUpRight20 => Get(0xf146);

            public IconUsage ArrowDown16 => Get(0xf147);

            public IconUsage ArrowDown20 => Get(0xf148);

            public IconUsage ArrowDown24 => Get(0xf149);

            public IconUsage ArrowDown28 => Get(0xf14a);

            public IconUsage ArrowDownLeft24 => Get(0xf14b);

            public IconUsage ArrowDownRightCircle16 => Get(0xf14c);

            public IconUsage ArrowDownRightCircle24 => Get(0xf14d);

            public IconUsage ArrowDownSquare24 => Get(0xf14e);

            public IconUsage ArrowDownload16 => Get(0xf14f);

            public IconUsage ArrowDownload20 => Get(0xf150);

            public IconUsage ArrowDownload24 => Get(0xf151);

            public IconUsage ArrowDownload48 => Get(0xf152);

            public IconUsage ArrowEnter24 => Get(0xf153);

            public IconUsage ArrowExpand24 => Get(0xf154);

            public IconUsage ArrowExport20 => Get(0xf155);

            public IconUsage ArrowForward16 => Get(0xf156);

            public IconUsage ArrowForward20 => Get(0xf157);

            public IconUsage ArrowForward24 => Get(0xf158);

            public IconUsage ArrowImport20 => Get(0xf159);

            public IconUsage ArrowImport24 => Get(0xf15a);

            public IconUsage ArrowLeft20 => Get(0xf15b);

            public IconUsage ArrowLeft24 => Get(0xf15c);

            public IconUsage ArrowLeft28 => Get(0xf15d);

            public IconUsage ArrowMaximize16 => Get(0xf15e);

            public IconUsage ArrowMaximize20 => Get(0xf15f);

            public IconUsage ArrowMaximize24 => Get(0xf160);

            public IconUsage ArrowMaximize28 => Get(0xf161);

            public IconUsage ArrowMaximizeVertical20 => Get(0xf162);

            public IconUsage ArrowMaximizeVertical24 => Get(0xf163);

            public IconUsage ArrowMinimize16 => Get(0xf164);

            public IconUsage ArrowMinimize20 => Get(0xf165);

            public IconUsage ArrowMinimize24 => Get(0xf166);

            public IconUsage ArrowMinimize28 => Get(0xf167);

            public IconUsage ArrowMinimizeVertical24 => Get(0xf168);

            public IconUsage ArrowMove24 => Get(0xf169);

            public IconUsage ArrowNext20 => Get(0xf16a);

            public IconUsage ArrowNext24 => Get(0xf16b);

            public IconUsage ArrowPrevious20 => Get(0xf16c);

            public IconUsage ArrowPrevious24 => Get(0xf16d);

            public IconUsage ArrowRedo20 => Get(0xf16e);

            public IconUsage ArrowRedo24 => Get(0xf16f);

            public IconUsage ArrowRepeatAll16 => Get(0xf170);

            public IconUsage ArrowRepeatAll20 => Get(0xf171);

            public IconUsage ArrowRepeatAll24 => Get(0xf172);

            public IconUsage ArrowRepeatAllOff16 => Get(0xf173);

            public IconUsage ArrowRepeatAllOff20 => Get(0xf174);

            public IconUsage ArrowRepeatAllOff24 => Get(0xf175);

            public IconUsage ArrowReply16 => Get(0xf176);

            public IconUsage ArrowReply20 => Get(0xf177);

            public IconUsage ArrowReply24 => Get(0xf178);

            public IconUsage ArrowReply48 => Get(0xf179);

            public IconUsage ArrowReplyAll16 => Get(0xf17a);

            public IconUsage ArrowReplyAll20 => Get(0xf17b);

            public IconUsage ArrowReplyAll24 => Get(0xf17c);

            public IconUsage ArrowReplyAll48 => Get(0xf17d);

            public IconUsage ArrowReplyDown16 => Get(0xf17e);

            public IconUsage ArrowReplyDown20 => Get(0xf17f);

            public IconUsage ArrowReplyDown24 => Get(0xf180);

            public IconUsage ArrowRight20 => Get(0xf181);

            public IconUsage ArrowRight24 => Get(0xf182);

            public IconUsage ArrowRight28 => Get(0xf183);

            public IconUsage ArrowRightCircle24 => Get(0xf184);

            public IconUsage ArrowRotateClockwise20 => Get(0xf185);

            public IconUsage ArrowRotateClockwise24 => Get(0xf186);

            public IconUsage ArrowRotateCounterclockwise20 => Get(0xf187);

            public IconUsage ArrowRotateCounterclockwise24 => Get(0xf188);

            public IconUsage ArrowRotateIcon24 => Get(0xf189);

            public IconUsage ArrowSort20 => Get(0xf18a);

            public IconUsage ArrowSort24 => Get(0xf18b);

            public IconUsage ArrowSort28 => Get(0xf18c);

            public IconUsage ArrowSwap20 => Get(0xf18d);

            public IconUsage ArrowSwap24 => Get(0xf18e);

            public IconUsage ArrowSync12 => Get(0xf18f);

            public IconUsage ArrowSync20 => Get(0xf190);

            public IconUsage ArrowSync24 => Get(0xf191);

            public IconUsage ArrowSyncCircle16 => Get(0xf192);

            public IconUsage ArrowSyncCircle20 => Get(0xf193);

            public IconUsage ArrowSyncCircle24 => Get(0xf194);

            public IconUsage ArrowSyncOff12 => Get(0xf195);

            public IconUsage ArrowTrending16 => Get(0xf196);

            public IconUsage ArrowTrending20 => Get(0xf197);

            public IconUsage ArrowTrending24 => Get(0xf198);

            public IconUsage ArrowUndo20 => Get(0xf199);

            public IconUsage ArrowUndo24 => Get(0xf19a);

            public IconUsage ArrowUp20 => Get(0xf19b);

            public IconUsage ArrowUp24 => Get(0xf19c);

            public IconUsage ArrowUp28 => Get(0xf19d);

            public IconUsage ArrowUpCircle16 => Get(0xf19e);

            public IconUsage ArrowUpCircle20 => Get(0xf19f);

            public IconUsage ArrowUpCircle24 => Get(0xf1a0);

            public IconUsage ArrowUpLeft24 => Get(0xf1a1);

            public IconUsage ArrowUpLeftCircle24 => Get(0xf1a2);

            public IconUsage ArrowUpRight24 => Get(0xf1a3);

            public IconUsage ArrowUpload20 => Get(0xf1a4);

            public IconUsage ArrowUpload24 => Get(0xf1a5);

            public IconUsage ArrowsBidirectional24 => Get(0xf1a6);

            public IconUsage Assignments24 => Get(0xf1a7);

            public IconUsage Attach16 => Get(0xf1a8);

            public IconUsage Attach20 => Get(0xf1a9);

            public IconUsage Attach24 => Get(0xf1aa);

            public IconUsage AttachForward20 => Get(0xf1ab);

            public IconUsage AttachForward24 => Get(0xf1ac);

            public IconUsage AttachWithText24 => Get(0xf1ad);

            public IconUsage Autocorrect24 => Get(0xf1ae);

            public IconUsage Autosum20 => Get(0xf1af);

            public IconUsage Autosum24 => Get(0xf1b0);

            public IconUsage Backspace20 => Get(0xf1b1);

            public IconUsage Backspace24 => Get(0xf1b2);

            public IconUsage Backward20 => Get(0xf1b3);

            public IconUsage Backward24 => Get(0xf1b4);

            public IconUsage Badge24 => Get(0xf1b5);

            public IconUsage Balloon20 => Get(0xf1b6);

            public IconUsage Balloon24 => Get(0xf1b7);

            public IconUsage Bank16 => Get(0xf1b8);

            public IconUsage Bank20 => Get(0xf1b9);

            public IconUsage Bank24 => Get(0xf1ba);

            public IconUsage Battery020 => Get(0xf1bb);

            public IconUsage Battery024 => Get(0xf1bc);

            public IconUsage Battery120 => Get(0xf1bd);

            public IconUsage Battery124 => Get(0xf1be);

            public IconUsage Battery220 => Get(0xf1bf);

            public IconUsage Battery224 => Get(0xf1c0);

            public IconUsage Battery320 => Get(0xf1c1);

            public IconUsage Battery324 => Get(0xf1c2);

            public IconUsage Battery420 => Get(0xf1c3);

            public IconUsage Battery424 => Get(0xf1c4);

            public IconUsage Battery520 => Get(0xf1c5);

            public IconUsage Battery524 => Get(0xf1c6);

            public IconUsage Battery620 => Get(0xf1c7);

            public IconUsage Battery624 => Get(0xf1c8);

            public IconUsage Battery720 => Get(0xf1c9);

            public IconUsage Battery724 => Get(0xf1ca);

            public IconUsage Battery820 => Get(0xf1cb);

            public IconUsage Battery824 => Get(0xf1cc);

            public IconUsage Battery920 => Get(0xf1cd);

            public IconUsage Battery924 => Get(0xf1ce);

            public IconUsage BatteryCharge20 => Get(0xf1cf);

            public IconUsage BatteryCharge24 => Get(0xf1d0);

            public IconUsage BatteryFull20 => Get(0xf1d1);

            public IconUsage BatteryFull24 => Get(0xf1d2);

            public IconUsage BatterySaver20 => Get(0xf1d3);

            public IconUsage BatterySaver24 => Get(0xf1d4);

            public IconUsage BatteryWarning24 => Get(0xf1d5);

            public IconUsage Beaker16 => Get(0xf1d6);

            public IconUsage Beaker20 => Get(0xf1d7);

            public IconUsage Beaker24 => Get(0xf1d8);

            public IconUsage Bed20 => Get(0xf1d9);

            public IconUsage Bed24 => Get(0xf1da);

            public IconUsage Block16 => Get(0xf1db);

            public IconUsage Block20 => Get(0xf1dc);

            public IconUsage Block24 => Get(0xf1dd);

            public IconUsage Bluetooth20 => Get(0xf1de);

            public IconUsage Bluetooth24 => Get(0xf1df);

            public IconUsage BluetoothConnected24 => Get(0xf1e0);

            public IconUsage BluetoothDisabled24 => Get(0xf1e1);

            public IconUsage BluetoothSearching24 => Get(0xf1e2);

            public IconUsage Board24 => Get(0xf1e3);

            public IconUsage BookFormulaCompatibility24 => Get(0xf1e4);

            public IconUsage BookFormulaDatabase24 => Get(0xf1e5);

            public IconUsage BookFormulaDate24 => Get(0xf1e6);

            public IconUsage BookFormulaEngineering24 => Get(0xf1e7);

            public IconUsage BookFormulaFinancial24 => Get(0xf1e8);

            public IconUsage BookFormulaInformation24 => Get(0xf1e9);

            public IconUsage BookFormulaLogical24 => Get(0xf1ea);

            public IconUsage BookFormulaLookup24 => Get(0xf1eb);

            public IconUsage BookFormulaMath24 => Get(0xf1ec);

            public IconUsage BookFormulaRecent24 => Get(0xf1ed);

            public IconUsage BookFormulaStatistics24 => Get(0xf1ee);

            public IconUsage BookFormulaText24 => Get(0xf1ef);

            public IconUsage BookGlobe24 => Get(0xf1f0);

            public IconUsage BookNumber16 => Get(0xf1f1);

            public IconUsage BookNumber20 => Get(0xf1f2);

            public IconUsage BookNumber24 => Get(0xf1f3);

            public IconUsage Bookmark16 => Get(0xf1f4);

            public IconUsage Bookmark20 => Get(0xf1f5);

            public IconUsage Bookmark24 => Get(0xf1f6);

            public IconUsage Bookmark28 => Get(0xf1f7);

            public IconUsage BookmarkOff24 => Get(0xf1f8);

            public IconUsage Bot24 => Get(0xf1f9);

            public IconUsage BotAdd24 => Get(0xf1fa);

            public IconUsage Branch24 => Get(0xf1fb);

            public IconUsage Briefcase20 => Get(0xf1fc);

            public IconUsage Briefcase24 => Get(0xf1fd);

            public IconUsage BringToFront20 => Get(0xf1fe);

            public IconUsage BringToFront24 => Get(0xf1ff);

            public IconUsage BroadActivityFeed24 => Get(0xf200);

            public IconUsage Broom20 => Get(0xf201);

            public IconUsage Broom24 => Get(0xf202);

            public IconUsage BugReport24 => Get(0xf203);

            public IconUsage Building124 => Get(0xf204);

            public IconUsage Building24 => Get(0xf205);

            public IconUsage Building216 => Get(0xf206);

            public IconUsage Building220 => Get(0xf207);

            public IconUsage Building224 => Get(0xf208);

            public IconUsage BuildingRetail24 => Get(0xf209);

            public IconUsage Calculator20 => Get(0xf20a);

            public IconUsage Calendar20 => Get(0xf20b);

            public IconUsage Calendar24 => Get(0xf20c);

            public IconUsage Calendar28 => Get(0xf20d);

            public IconUsage Calendar3Day20 => Get(0xf20e);

            public IconUsage Calendar3Day24 => Get(0xf20f);

            public IconUsage Calendar3Day28 => Get(0xf210);

            public IconUsage CalendarAdd20 => Get(0xf211);

            public IconUsage CalendarAdd24 => Get(0xf212);

            public IconUsage CalendarAgenda20 => Get(0xf213);

            public IconUsage CalendarAgenda24 => Get(0xf214);

            public IconUsage CalendarAgenda28 => Get(0xf215);

            public IconUsage CalendarArrowRight20 => Get(0xf216);

            public IconUsage CalendarAssistant20 => Get(0xf217);

            public IconUsage CalendarAssistant24 => Get(0xf218);

            public IconUsage CalendarCancel20 => Get(0xf219);

            public IconUsage CalendarCancel24 => Get(0xf21a);

            public IconUsage CalendarCheckmark16 => Get(0xf21b);

            public IconUsage CalendarCheckmark20 => Get(0xf21c);

            public IconUsage CalendarClock20 => Get(0xf21d);

            public IconUsage CalendarClock24 => Get(0xf21e);

            public IconUsage CalendarDate20 => Get(0xf21f);

            public IconUsage CalendarDate24 => Get(0xf220);

            public IconUsage CalendarDate28 => Get(0xf221);

            public IconUsage CalendarDay20 => Get(0xf222);

            public IconUsage CalendarDay24 => Get(0xf223);

            public IconUsage CalendarDay28 => Get(0xf224);

            public IconUsage CalendarEmpty16 => Get(0xf225);

            public IconUsage CalendarEmpty20 => Get(0xf226);

            public IconUsage CalendarEmpty24 => Get(0xf227);

            public IconUsage CalendarEmpty28 => Get(0xf228);

            public IconUsage CalendarLater24 => Get(0xf229);

            public IconUsage CalendarMonth20 => Get(0xf22a);

            public IconUsage CalendarMonth24 => Get(0xf22b);

            public IconUsage CalendarMonth28 => Get(0xf22c);

            public IconUsage CalendarMultiple20 => Get(0xf22d);

            public IconUsage CalendarMultiple24 => Get(0xf22e);

            public IconUsage CalendarOverdue24 => Get(0xf22f);

            public IconUsage CalendarPerson20 => Get(0xf230);

            public IconUsage CalendarReply16 => Get(0xf231);

            public IconUsage CalendarReply20 => Get(0xf232);

            public IconUsage CalendarReply24 => Get(0xf233);

            public IconUsage CalendarReply28 => Get(0xf234);

            public IconUsage CalendarSettings20 => Get(0xf235);

            public IconUsage CalendarStar20 => Get(0xf236);

            public IconUsage CalendarStar24 => Get(0xf237);

            public IconUsage CalendarSync16 => Get(0xf238);

            public IconUsage CalendarSync20 => Get(0xf239);

            public IconUsage CalendarSync24 => Get(0xf23a);

            public IconUsage CalendarToday16 => Get(0xf23b);

            public IconUsage CalendarToday20 => Get(0xf23c);

            public IconUsage CalendarToday24 => Get(0xf23d);

            public IconUsage CalendarToday28 => Get(0xf23e);

            public IconUsage CalendarWeekNumbers24 => Get(0xf23f);

            public IconUsage CalendarWeekStart20 => Get(0xf240);

            public IconUsage CalendarWeekStart24 => Get(0xf241);

            public IconUsage CalendarWeekStart28 => Get(0xf242);

            public IconUsage CalendarWorkWeek16 => Get(0xf243);

            public IconUsage CalendarWorkWeek20 => Get(0xf244);

            public IconUsage CalendarWorkWeek24 => Get(0xf245);

            public IconUsage CallAdd24 => Get(0xf246);

            public IconUsage CallEnd20 => Get(0xf247);

            public IconUsage CallEnd24 => Get(0xf248);

            public IconUsage CallEnd28 => Get(0xf249);

            public IconUsage CallForward24 => Get(0xf24a);

            public IconUsage CallInbound16 => Get(0xf24b);

            public IconUsage CallInbound24 => Get(0xf24c);

            public IconUsage CallMissed16 => Get(0xf24d);

            public IconUsage CallMissed24 => Get(0xf24e);

            public IconUsage CallOutbound16 => Get(0xf24f);

            public IconUsage CallOutbound24 => Get(0xf250);

            public IconUsage CallPark24 => Get(0xf251);

            public IconUsage CalligraphyPen20 => Get(0xf252);

            public IconUsage CalligraphyPen24 => Get(0xf253);

            public IconUsage Camera20 => Get(0xf254);

            public IconUsage Camera24 => Get(0xf255);

            public IconUsage Camera28 => Get(0xf256);

            public IconUsage CameraAdd20 => Get(0xf257);

            public IconUsage CameraAdd24 => Get(0xf258);

            public IconUsage CameraAdd48 => Get(0xf259);

            public IconUsage CameraSwitch24 => Get(0xf25a);

            public IconUsage Caret12 => Get(0xf25b);

            public IconUsage Caret16 => Get(0xf25c);

            public IconUsage Caret20 => Get(0xf25d);

            public IconUsage Caret24 => Get(0xf25e);

            public IconUsage CaretDown12 => Get(0xf25f);

            public IconUsage CaretDown16 => Get(0xf260);

            public IconUsage CaretDown20 => Get(0xf261);

            public IconUsage CaretDown24 => Get(0xf262);

            public IconUsage CaretLeft12 => Get(0xf263);

            public IconUsage CaretLeft16 => Get(0xf264);

            public IconUsage CaretLeft20 => Get(0xf265);

            public IconUsage CaretLeft24 => Get(0xf266);

            public IconUsage CaretRight12 => Get(0xf267);

            public IconUsage CaretRight16 => Get(0xf268);

            public IconUsage CaretRight20 => Get(0xf269);

            public IconUsage CaretRight24 => Get(0xf26a);

            public IconUsage Cart24 => Get(0xf26b);

            public IconUsage Cast20 => Get(0xf26c);

            public IconUsage Cast24 => Get(0xf26d);

            public IconUsage Cast28 => Get(0xf26e);

            public IconUsage Cellular3G24 => Get(0xf26f);

            public IconUsage Cellular4G24 => Get(0xf270);

            public IconUsage CellularData120 => Get(0xf271);

            public IconUsage CellularData124 => Get(0xf272);

            public IconUsage CellularData220 => Get(0xf273);

            public IconUsage CellularData224 => Get(0xf274);

            public IconUsage CellularData320 => Get(0xf275);

            public IconUsage CellularData324 => Get(0xf276);

            public IconUsage CellularData420 => Get(0xf277);

            public IconUsage CellularData424 => Get(0xf278);

            public IconUsage CellularData520 => Get(0xf279);

            public IconUsage CellularData524 => Get(0xf27a);

            public IconUsage CellularDataOff24 => Get(0xf27b);

            public IconUsage CellularOff24 => Get(0xf27c);

            public IconUsage CellularUnavailable24 => Get(0xf27d);

            public IconUsage Certificate20 => Get(0xf27e);

            public IconUsage Certificate24 => Get(0xf27f);

            public IconUsage Channel16 => Get(0xf280);

            public IconUsage Channel20 => Get(0xf281);

            public IconUsage Channel24 => Get(0xf282);

            public IconUsage ChannelFollow24 => Get(0xf283);

            public IconUsage ChannelNotifications24 => Get(0xf284);

            public IconUsage ChannelUnfollow24 => Get(0xf285);

            public IconUsage Chat20 => Get(0xf286);

            public IconUsage Chat24 => Get(0xf287);

            public IconUsage Chat28 => Get(0xf288);

            public IconUsage ChatBubblesQuestion24 => Get(0xf289);

            public IconUsage ChatHelp24 => Get(0xf28a);

            public IconUsage ChatOff24 => Get(0xf28b);

            public IconUsage ChatWarning24 => Get(0xf28c);

            public IconUsage CheckboxChecked20 => Get(0xf28d);

            public IconUsage CheckboxChecked24 => Get(0xf28e);

            public IconUsage CheckboxUnchecked12 => Get(0xf28f);

            public IconUsage CheckboxUnchecked16 => Get(0xf290);

            public IconUsage CheckboxUnchecked20 => Get(0xf291);

            public IconUsage CheckboxUnchecked24 => Get(0xf292);

            public IconUsage Checkmark12 => Get(0xf293);

            public IconUsage Checkmark20 => Get(0xf294);

            public IconUsage Checkmark24 => Get(0xf295);

            public IconUsage Checkmark28 => Get(0xf296);

            public IconUsage CheckmarkCircle16 => Get(0xf297);

            public IconUsage CheckmarkCircle20 => Get(0xf298);

            public IconUsage CheckmarkCircle24 => Get(0xf299);

            public IconUsage CheckmarkCircle48 => Get(0xf29a);

            public IconUsage CheckmarkLock16 => Get(0xf29b);

            public IconUsage CheckmarkLock20 => Get(0xf29c);

            public IconUsage CheckmarkLock24 => Get(0xf29d);

            public IconUsage CheckmarkSquare24 => Get(0xf29e);

            public IconUsage CheckmarkUnderlineCircle16 => Get(0xf29f);

            public IconUsage CheckmarkUnderlineCircle20 => Get(0xf2a0);

            public IconUsage ChevronDown12 => Get(0xf2a1);

            public IconUsage ChevronDown16 => Get(0xf2a2);

            public IconUsage ChevronDown20 => Get(0xf2a3);

            public IconUsage ChevronDown24 => Get(0xf2a4);

            public IconUsage ChevronDown28 => Get(0xf2a5);

            public IconUsage ChevronDown48 => Get(0xf2a6);

            public IconUsage ChevronDownCircle24 => Get(0xf2a7);

            public IconUsage ChevronLeft12 => Get(0xf2a8);

            public IconUsage ChevronLeft16 => Get(0xf2a9);

            public IconUsage ChevronLeft20 => Get(0xf2aa);

            public IconUsage ChevronLeft24 => Get(0xf2ab);

            public IconUsage ChevronLeft28 => Get(0xf2ac);

            public IconUsage ChevronLeft48 => Get(0xf2ad);

            public IconUsage ChevronRight12 => Get(0xf2ae);

            public IconUsage ChevronRight16 => Get(0xf2af);

            public IconUsage ChevronRight20 => Get(0xf2b0);

            public IconUsage ChevronRight24 => Get(0xf2b1);

            public IconUsage ChevronRight28 => Get(0xf2b2);

            public IconUsage ChevronRight48 => Get(0xf2b3);

            public IconUsage ChevronUp12 => Get(0xf2b4);

            public IconUsage ChevronUp16 => Get(0xf2b5);

            public IconUsage ChevronUp20 => Get(0xf2b6);

            public IconUsage ChevronUp24 => Get(0xf2b7);

            public IconUsage ChevronUp28 => Get(0xf2b8);

            public IconUsage ChevronUp48 => Get(0xf2b9);

            public IconUsage Circle16 => Get(0xf2ba);

            public IconUsage Circle20 => Get(0xf2bb);

            public IconUsage Circle24 => Get(0xf2bc);

            public IconUsage CircleHalfFill20 => Get(0xf2bd);

            public IconUsage CircleHalfFill24 => Get(0xf2be);

            public IconUsage CircleLine24 => Get(0xf2bf);

            public IconUsage CircleSmall24 => Get(0xf2c0);

            public IconUsage City16 => Get(0xf2c1);

            public IconUsage City20 => Get(0xf2c2);

            public IconUsage City24 => Get(0xf2c3);

            public IconUsage Class24 => Get(0xf2c4);

            public IconUsage Classification16 => Get(0xf2c5);

            public IconUsage Classification20 => Get(0xf2c6);

            public IconUsage Classification24 => Get(0xf2c7);

            public IconUsage ClearFormatting24 => Get(0xf2c8);

            public IconUsage Clipboard20 => Get(0xf2c9);

            public IconUsage Clipboard24 => Get(0xf2ca);

            public IconUsage ClipboardCode16 => Get(0xf2cb);

            public IconUsage ClipboardCode20 => Get(0xf2cc);

            public IconUsage ClipboardCode24 => Get(0xf2cd);

            public IconUsage ClipboardLetter16 => Get(0xf2ce);

            public IconUsage ClipboardLetter20 => Get(0xf2cf);

            public IconUsage ClipboardLetter24 => Get(0xf2d0);

            public IconUsage ClipboardLink16 => Get(0xf2d1);

            public IconUsage ClipboardLink20 => Get(0xf2d2);

            public IconUsage ClipboardLink24 => Get(0xf2d3);

            public IconUsage ClipboardMore24 => Get(0xf2d4);

            public IconUsage ClipboardPaste20 => Get(0xf2d5);

            public IconUsage ClipboardPaste24 => Get(0xf2d6);

            public IconUsage ClipboardSearch20 => Get(0xf2d7);

            public IconUsage ClipboardSearch24 => Get(0xf2d8);

            public IconUsage ClipboardText20 => Get(0xf2d9);

            public IconUsage ClipboardText24 => Get(0xf2da);

            public IconUsage Clock12 => Get(0xf2db);

            public IconUsage Clock16 => Get(0xf2dc);

            public IconUsage Clock20 => Get(0xf2dd);

            public IconUsage Clock24 => Get(0xf2de);

            public IconUsage Clock28 => Get(0xf2df);

            public IconUsage Clock48 => Get(0xf2e0);

            public IconUsage ClockAlarm20 => Get(0xf2e1);

            public IconUsage ClockAlarm24 => Get(0xf2e2);

            public IconUsage ClosedCaption24 => Get(0xf2e3);

            public IconUsage Cloud20 => Get(0xf2e4);

            public IconUsage Cloud24 => Get(0xf2e5);

            public IconUsage Cloud48 => Get(0xf2e6);

            public IconUsage CloudBackup24 => Get(0xf2e7);

            public IconUsage CloudBackup48 => Get(0xf2e8);

            public IconUsage CloudDownload24 => Get(0xf2e9);

            public IconUsage CloudOff24 => Get(0xf2ea);

            public IconUsage CloudOff48 => Get(0xf2eb);

            public IconUsage CloudOffline24 => Get(0xf2ec);

            public IconUsage CloudSyncComplete24 => Get(0xf2ed);

            public IconUsage CloudSyncComplete48 => Get(0xf2ee);

            public IconUsage Code20 => Get(0xf2ef);

            public IconUsage Code24 => Get(0xf2f0);

            public IconUsage Collections20 => Get(0xf2f1);

            public IconUsage Collections24 => Get(0xf2f2);

            public IconUsage CollectionsAdd20 => Get(0xf2f3);

            public IconUsage CollectionsAdd24 => Get(0xf2f4);

            public IconUsage Color20 => Get(0xf2f5);

            public IconUsage Color24 => Get(0xf2f6);

            public IconUsage ColorBackground20 => Get(0xf2f7);

            public IconUsage ColorBackground24 => Get(0xf2f8);

            public IconUsage ColorFill20 => Get(0xf2f9);

            public IconUsage ColorFill24 => Get(0xf2fa);

            public IconUsage ColorLine20 => Get(0xf2fb);

            public IconUsage ColorLine24 => Get(0xf2fc);

            public IconUsage ColumnTriple24 => Get(0xf2fd);

            public IconUsage Comment16 => Get(0xf2fe);

            public IconUsage Comment20 => Get(0xf2ff);

            public IconUsage Comment24 => Get(0xf300);

            public IconUsage CommentAdd24 => Get(0xf301);

            public IconUsage CommentDelete24 => Get(0xf302);

            public IconUsage CommentMention16 => Get(0xf303);

            public IconUsage CommentMention20 => Get(0xf304);

            public IconUsage CommentMention24 => Get(0xf305);

            public IconUsage CommentMultiple16 => Get(0xf306);

            public IconUsage CommentMultiple20 => Get(0xf307);

            public IconUsage CommentMultiple24 => Get(0xf308);

            public IconUsage CommentNext24 => Get(0xf309);

            public IconUsage CommentPrevious24 => Get(0xf30a);

            public IconUsage CommentResolve24 => Get(0xf30b);

            public IconUsage Communication16 => Get(0xf30c);

            public IconUsage Communication20 => Get(0xf30d);

            public IconUsage Communication24 => Get(0xf30e);

            public IconUsage CompassNorthwest16 => Get(0xf30f);

            public IconUsage CompassNorthwest20 => Get(0xf310);

            public IconUsage CompassNorthwest24 => Get(0xf311);

            public IconUsage CompassNorthwest28 => Get(0xf312);

            public IconUsage Compose16 => Get(0xf313);

            public IconUsage Compose20 => Get(0xf314);

            public IconUsage Compose24 => Get(0xf315);

            public IconUsage Compose28 => Get(0xf316);

            public IconUsage ConferenceRoom16 => Get(0xf317);

            public IconUsage ConferenceRoom20 => Get(0xf318);

            public IconUsage ConferenceRoom24 => Get(0xf319);

            public IconUsage ConferenceRoom28 => Get(0xf31a);

            public IconUsage ConferenceRoom48 => Get(0xf31b);

            public IconUsage Connector16 => Get(0xf31c);

            public IconUsage Connector20 => Get(0xf31d);

            public IconUsage Connector24 => Get(0xf31e);

            public IconUsage ContactCard20 => Get(0xf31f);

            public IconUsage ContactCard24 => Get(0xf320);

            public IconUsage ContactCardGroup24 => Get(0xf321);

            public IconUsage Contacts24 => Get(0xf322);

            public IconUsage ContentSettings16 => Get(0xf323);

            public IconUsage ContentSettings20 => Get(0xf324);

            public IconUsage ContentSettings24 => Get(0xf325);

            public IconUsage ConvertToTable24 => Get(0xf326);

            public IconUsage ConvertToText24 => Get(0xf327);

            public IconUsage Cookies20 => Get(0xf328);

            public IconUsage Cookies24 => Get(0xf329);

            public IconUsage Copy16 => Get(0xf32a);

            public IconUsage Copy20 => Get(0xf32b);

            public IconUsage Copy24 => Get(0xf32c);

            public IconUsage CopyImage24 => Get(0xf32d);

            public IconUsage CopyLink24 => Get(0xf32e);

            public IconUsage CopyMove16 => Get(0xf32f);

            public IconUsage CopyMove24 => Get(0xf330);

            public IconUsage Crop24 => Get(0xf331);

            public IconUsage CropInterim24 => Get(0xf332);

            public IconUsage CropInterimOff24 => Get(0xf333);

            public IconUsage Cube16 => Get(0xf334);

            public IconUsage Cube20 => Get(0xf335);

            public IconUsage Cube24 => Get(0xf336);

            public IconUsage Currency16 => Get(0xf337);

            public IconUsage Currency20 => Get(0xf338);

            public IconUsage Currency24 => Get(0xf339);

            public IconUsage Cut20 => Get(0xf33a);

            public IconUsage Cut24 => Get(0xf33b);

            public IconUsage DarkTheme24 => Get(0xf33c);

            public IconUsage DataArea24 => Get(0xf33d);

            public IconUsage DataBarHorizontal24 => Get(0xf33e);

            public IconUsage DataBarVertical20 => Get(0xf33f);

            public IconUsage DataBarVertical24 => Get(0xf340);

            public IconUsage DataFunnel24 => Get(0xf341);

            public IconUsage DataHistogram24 => Get(0xf342);

            public IconUsage DataLine24 => Get(0xf343);

            public IconUsage DataPie20 => Get(0xf344);

            public IconUsage DataPie24 => Get(0xf345);

            public IconUsage DataScatter24 => Get(0xf346);

            public IconUsage DataSunburst24 => Get(0xf347);

            public IconUsage DataTreemap24 => Get(0xf348);

            public IconUsage DataUsage24 => Get(0xf349);

            public IconUsage DataWaterfall24 => Get(0xf34a);

            public IconUsage DataWhisker24 => Get(0xf34b);

            public IconUsage Delete20 => Get(0xf34c);

            public IconUsage Delete24 => Get(0xf34d);

            public IconUsage Delete28 => Get(0xf34e);

            public IconUsage Delete48 => Get(0xf34f);

            public IconUsage DeleteForever24 => Get(0xf350);

            public IconUsage DeleteForever28 => Get(0xf351);

            public IconUsage DeleteOff20 => Get(0xf352);

            public IconUsage DeleteOff24 => Get(0xf353);

            public IconUsage Dentist24 => Get(0xf354);

            public IconUsage DesignIdeas16 => Get(0xf355);

            public IconUsage DesignIdeas20 => Get(0xf356);

            public IconUsage DesignIdeas24 => Get(0xf357);

            public IconUsage Desktop16 => Get(0xf358);

            public IconUsage Desktop20 => Get(0xf359);

            public IconUsage Desktop24 => Get(0xf35a);

            public IconUsage Desktop28 => Get(0xf35b);

            public IconUsage DeveloperBoard24 => Get(0xf35c);

            public IconUsage DeviceEq24 => Get(0xf35d);

            public IconUsage Dialpad20 => Get(0xf35e);

            public IconUsage Dialpad24 => Get(0xf35f);

            public IconUsage DialpadOff24 => Get(0xf360);

            public IconUsage Dictionary20 => Get(0xf361);

            public IconUsage Dictionary24 => Get(0xf362);

            public IconUsage DictionaryAdd20 => Get(0xf363);

            public IconUsage DictionaryAdd24 => Get(0xf364);

            public IconUsage Directions20 => Get(0xf365);

            public IconUsage Directions24 => Get(0xf366);

            public IconUsage Dismiss12 => Get(0xf367);

            public IconUsage Dismiss16 => Get(0xf368);

            public IconUsage Dismiss20 => Get(0xf369);

            public IconUsage Dismiss24 => Get(0xf36a);

            public IconUsage Dismiss28 => Get(0xf36b);

            public IconUsage DismissCircle16 => Get(0xf36c);

            public IconUsage DismissCircle20 => Get(0xf36d);

            public IconUsage DismissCircle24 => Get(0xf36e);

            public IconUsage DismissCircle48 => Get(0xf36f);

            public IconUsage DividerShort24 => Get(0xf370);

            public IconUsage DividerTall24 => Get(0xf371);

            public IconUsage Dock24 => Get(0xf372);

            public IconUsage DockLeft16 => Get(0xf373);

            public IconUsage DockLeft20 => Get(0xf374);

            public IconUsage DockLeft24 => Get(0xf375);

            public IconUsage DockRow24 => Get(0xf376);

            public IconUsage Doctor24 => Get(0xf377);

            public IconUsage Document20 => Get(0xf378);

            public IconUsage Document24 => Get(0xf379);

            public IconUsage Document28 => Get(0xf37a);

            public IconUsage DocumentAutosave24 => Get(0xf37b);

            public IconUsage DocumentBriefcase20 => Get(0xf37c);

            public IconUsage DocumentBriefcase24 => Get(0xf37d);

            public IconUsage DocumentCatchUp24 => Get(0xf37e);

            public IconUsage DocumentCopy16 => Get(0xf37f);

            public IconUsage DocumentCopy20 => Get(0xf380);

            public IconUsage DocumentCopy24 => Get(0xf381);

            public IconUsage DocumentCopy48 => Get(0xf382);

            public IconUsage DocumentDismiss20 => Get(0xf383);

            public IconUsage DocumentDismiss24 => Get(0xf384);

            public IconUsage DocumentEdit16 => Get(0xf385);

            public IconUsage DocumentEdit20 => Get(0xf386);

            public IconUsage DocumentEdit24 => Get(0xf387);

            public IconUsage DocumentEndnote20 => Get(0xf388);

            public IconUsage DocumentEndnote24 => Get(0xf389);

            public IconUsage DocumentError16 => Get(0xf38a);

            public IconUsage DocumentError20 => Get(0xf38b);

            public IconUsage DocumentError24 => Get(0xf38c);

            public IconUsage DocumentFooter24 => Get(0xf38d);

            public IconUsage DocumentFooterRemove24 => Get(0xf38e);

            public IconUsage DocumentHeader24 => Get(0xf38f);

            public IconUsage DocumentHeaderFooter20 => Get(0xf390);

            public IconUsage DocumentHeaderFooter24 => Get(0xf391);

            public IconUsage DocumentHeaderRemove24 => Get(0xf392);

            public IconUsage DocumentLandscape20 => Get(0xf393);

            public IconUsage DocumentLandscape24 => Get(0xf394);

            public IconUsage DocumentMargins20 => Get(0xf395);

            public IconUsage DocumentMargins24 => Get(0xf396);

            public IconUsage DocumentNone20 => Get(0xf397);

            public IconUsage DocumentNone24 => Get(0xf398);

            public IconUsage DocumentOnePage20 => Get(0xf399);

            public IconUsage DocumentOnePage24 => Get(0xf39a);

            public IconUsage DocumentPage24 => Get(0xf39b);

            public IconUsage DocumentPageBottomCenter20 => Get(0xf39c);

            public IconUsage DocumentPageBottomCenter24 => Get(0xf39d);

            public IconUsage DocumentPageBottomLeft20 => Get(0xf39e);

            public IconUsage DocumentPageBottomLeft24 => Get(0xf39f);

            public IconUsage DocumentPageBottomRight20 => Get(0xf3a0);

            public IconUsage DocumentPageBottomRight24 => Get(0xf3a1);

            public IconUsage DocumentPageBreak24 => Get(0xf3a2);

            public IconUsage DocumentPageNumber20 => Get(0xf3a3);

            public IconUsage DocumentPageNumber24 => Get(0xf3a4);

            public IconUsage DocumentPageTopCenter20 => Get(0xf3a5);

            public IconUsage DocumentPageTopCenter24 => Get(0xf3a6);

            public IconUsage DocumentPageTopLeft20 => Get(0xf3a7);

            public IconUsage DocumentPageTopLeft24 => Get(0xf3a8);

            public IconUsage DocumentPageTopRight20 => Get(0xf3a9);

            public IconUsage DocumentPageTopRight24 => Get(0xf3aa);

            public IconUsage DocumentPdf16 => Get(0xf3ab);

            public IconUsage DocumentPdf20 => Get(0xf3ac);

            public IconUsage DocumentPdf24 => Get(0xf3ad);

            public IconUsage DocumentSearch20 => Get(0xf3ae);

            public IconUsage DocumentSearch24 => Get(0xf3af);

            public IconUsage DocumentToolbox20 => Get(0xf3b0);

            public IconUsage DocumentToolbox24 => Get(0xf3b1);

            public IconUsage DocumentTopCenter20 => Get(0xf3b2);

            public IconUsage DocumentTopLeft20 => Get(0xf3b3);

            public IconUsage DocumentTopRight20 => Get(0xf3b4);

            public IconUsage DocumentUnknown16 => Get(0xf3b5);

            public IconUsage DocumentUnknown20 => Get(0xf3b6);

            public IconUsage DocumentUnknown24 => Get(0xf3b7);

            public IconUsage DocumentWidth20 => Get(0xf3b8);

            public IconUsage DocumentWidth24 => Get(0xf3b9);

            public IconUsage DoubleSwipeDown24 => Get(0xf3ba);

            public IconUsage DoubleSwipeUp24 => Get(0xf3bb);

            public IconUsage DoubleTapSwipeDown24 => Get(0xf3bc);

            public IconUsage DoubleTapSwipeUp24 => Get(0xf3bd);

            public IconUsage Drafts16 => Get(0xf3be);

            public IconUsage Drafts20 => Get(0xf3bf);

            public IconUsage Drafts24 => Get(0xf3c0);

            public IconUsage Drag24 => Get(0xf3c1);

            public IconUsage Drink24 => Get(0xf3c2);

            public IconUsage DrinkBeer24 => Get(0xf3c3);

            public IconUsage DrinkCoffee20 => Get(0xf3c4);

            public IconUsage DrinkCoffee24 => Get(0xf3c5);

            public IconUsage DrinkMargarita24 => Get(0xf3c6);

            public IconUsage DrinkWine24 => Get(0xf3c7);

            public IconUsage DualScreen24 => Get(0xf3c8);

            public IconUsage DualScreenAdd24 => Get(0xf3c9);

            public IconUsage DualScreenArrowRight24 => Get(0xf3ca);

            public IconUsage DualScreenClock24 => Get(0xf3cb);

            public IconUsage DualScreenDesktop24 => Get(0xf3cc);

            public IconUsage DualScreenError24 => Get(0xf3cd);

            public IconUsage DualScreenGroup24 => Get(0xf3ce);

            public IconUsage DualScreenLock24 => Get(0xf3cf);

            public IconUsage DualScreenMirror24 => Get(0xf3d0);

            public IconUsage DualScreenPagination24 => Get(0xf3d1);

            public IconUsage DualScreenSettings24 => Get(0xf3d2);

            public IconUsage DualScreenStatusBar24 => Get(0xf3d3);

            public IconUsage DualScreenTablet24 => Get(0xf3d4);

            public IconUsage DualScreenUpdate24 => Get(0xf3d5);

            public IconUsage DualScreenVerticalScroll24 => Get(0xf3d6);

            public IconUsage DualScreenVibrate24 => Get(0xf3d7);

            public IconUsage Earth16 => Get(0xf3d8);

            public IconUsage Earth20 => Get(0xf3d9);

            public IconUsage Earth24 => Get(0xf3da);

            public IconUsage Edit16 => Get(0xf3db);

            public IconUsage Edit20 => Get(0xf3dc);

            public IconUsage Edit24 => Get(0xf3dd);

            public IconUsage Emoji16 => Get(0xf3de);

            public IconUsage Emoji20 => Get(0xf3df);

            public IconUsage Emoji24 => Get(0xf3e0);

            public IconUsage EmojiAdd24 => Get(0xf3e1);

            public IconUsage EmojiAngry20 => Get(0xf3e2);

            public IconUsage EmojiAngry24 => Get(0xf3e3);

            public IconUsage EmojiLaugh20 => Get(0xf3e4);

            public IconUsage EmojiLaugh24 => Get(0xf3e5);

            public IconUsage EmojiMeh20 => Get(0xf3e6);

            public IconUsage EmojiMeh24 => Get(0xf3e7);

            public IconUsage EmojiSad20 => Get(0xf3e8);

            public IconUsage EmojiSad24 => Get(0xf3e9);

            public IconUsage EmojiSurprise20 => Get(0xf3ea);

            public IconUsage EmojiSurprise24 => Get(0xf3eb);

            public IconUsage Erase20 => Get(0xf3ec);

            public IconUsage Erase24 => Get(0xf3ed);

            public IconUsage EraserTool24 => Get(0xf3ee);

            public IconUsage ErrorCircle16 => Get(0xf3ef);

            public IconUsage ErrorCircle20 => Get(0xf3f0);

            public IconUsage ErrorCircle24 => Get(0xf3f1);

            public IconUsage Export24 => Get(0xf3f2);

            public IconUsage ExtendedDock24 => Get(0xf3f3);

            public IconUsage Extension20 => Get(0xf3f4);

            public IconUsage Extension24 => Get(0xf3f5);

            public IconUsage EyeHide20 => Get(0xf3f6);

            public IconUsage EyeHide24 => Get(0xf3f7);

            public IconUsage EyeShow12 => Get(0xf3f8);

            public IconUsage EyeShow16 => Get(0xf3f9);

            public IconUsage EyeShow20 => Get(0xf3fa);

            public IconUsage EyeShow24 => Get(0xf3fb);

            public IconUsage FastAcceleration24 => Get(0xf3fc);

            public IconUsage FastForward20 => Get(0xf3fd);

            public IconUsage FastForward24 => Get(0xf3fe);

            public IconUsage FastMode16 => Get(0xf3ff);

            public IconUsage FastMode20 => Get(0xf400);

            public IconUsage FastMode24 => Get(0xf401);

            public IconUsage FastMode28 => Get(0xf402);

            public IconUsage Favorites20 => Get(0xf403);

            public IconUsage Favorites24 => Get(0xf404);

            public IconUsage Filter20 => Get(0xf405);

            public IconUsage Filter24 => Get(0xf406);

            public IconUsage Filter28 => Get(0xf407);

            public IconUsage Fingerprint24 => Get(0xf408);

            public IconUsage Flag16 => Get(0xf409);

            public IconUsage Flag20 => Get(0xf40a);

            public IconUsage Flag24 => Get(0xf40b);

            public IconUsage Flag28 => Get(0xf40c);

            public IconUsage Flag48 => Get(0xf40d);

            public IconUsage FlagOff24 => Get(0xf40e);

            public IconUsage FlagOff28 => Get(0xf40f);

            public IconUsage FlagOff48 => Get(0xf410);

            public IconUsage FlagPride16 => Get(0xf411);

            public IconUsage FlagPride20 => Get(0xf412);

            public IconUsage FlagPride24 => Get(0xf413);

            public IconUsage FlagPride28 => Get(0xf414);

            public IconUsage FlagPride48 => Get(0xf415);

            public IconUsage FlashAuto24 => Get(0xf416);

            public IconUsage FlashOff24 => Get(0xf417);

            public IconUsage FlashOn20 => Get(0xf418);

            public IconUsage FlashOn24 => Get(0xf419);

            public IconUsage Flashlight24 => Get(0xf41a);

            public IconUsage FlashlightOff24 => Get(0xf41b);

            public IconUsage Folder20 => Get(0xf41c);

            public IconUsage Folder24 => Get(0xf41d);

            public IconUsage Folder28 => Get(0xf41e);

            public IconUsage Folder48 => Get(0xf41f);

            public IconUsage FolderAdd20 => Get(0xf420);

            public IconUsage FolderAdd24 => Get(0xf421);

            public IconUsage FolderAdd28 => Get(0xf422);

            public IconUsage FolderAdd48 => Get(0xf423);

            public IconUsage FolderBriefcase20 => Get(0xf424);

            public IconUsage FolderJunk20 => Get(0xf425);

            public IconUsage FolderJunk24 => Get(0xf426);

            public IconUsage FolderJunk28 => Get(0xf427);

            public IconUsage FolderJunk48 => Get(0xf428);

            public IconUsage FolderLink20 => Get(0xf429);

            public IconUsage FolderLink24 => Get(0xf42a);

            public IconUsage FolderLink28 => Get(0xf42b);

            public IconUsage FolderLink48 => Get(0xf42c);

            public IconUsage FolderMove20 => Get(0xf42d);

            public IconUsage FolderMove24 => Get(0xf42e);

            public IconUsage FolderMove28 => Get(0xf42f);

            public IconUsage FolderMove48 => Get(0xf430);

            public IconUsage FolderOpen16 => Get(0xf431);

            public IconUsage FolderOpen20 => Get(0xf432);

            public IconUsage FolderOpen24 => Get(0xf433);

            public IconUsage FolderOpenVertical20 => Get(0xf434);

            public IconUsage FolderPublic16 => Get(0xf435);

            public IconUsage FolderPublic20 => Get(0xf436);

            public IconUsage FolderPublic24 => Get(0xf437);

            public IconUsage FolderZip16 => Get(0xf438);

            public IconUsage FolderZip20 => Get(0xf439);

            public IconUsage FolderZip24 => Get(0xf43a);

            public IconUsage FontDecrease20 => Get(0xf43b);

            public IconUsage FontDecrease24 => Get(0xf43c);

            public IconUsage FontIncrease20 => Get(0xf43d);

            public IconUsage FontIncrease24 => Get(0xf43e);

            public IconUsage FontSpaceTrackingIn16 => Get(0xf43f);

            public IconUsage FontSpaceTrackingIn20 => Get(0xf440);

            public IconUsage FontSpaceTrackingIn24 => Get(0xf441);

            public IconUsage FontSpaceTrackingIn28 => Get(0xf442);

            public IconUsage FontSpaceTrackingOut16 => Get(0xf443);

            public IconUsage FontSpaceTrackingOut20 => Get(0xf444);

            public IconUsage FontSpaceTrackingOut24 => Get(0xf445);

            public IconUsage FontSpaceTrackingOut28 => Get(0xf446);

            public IconUsage Food20 => Get(0xf447);

            public IconUsage Food24 => Get(0xf448);

            public IconUsage FoodCake24 => Get(0xf449);

            public IconUsage FoodEgg24 => Get(0xf44a);

            public IconUsage FoodToast24 => Get(0xf44b);

            public IconUsage FormNew24 => Get(0xf44c);

            public IconUsage FormNew28 => Get(0xf44d);

            public IconUsage FormNew48 => Get(0xf44e);

            public IconUsage Forward20 => Get(0xf44f);

            public IconUsage Forward24 => Get(0xf450);

            public IconUsage Fps24024 => Get(0xf451);

            public IconUsage Fps96024 => Get(0xf452);

            public IconUsage FullScreenZoom24 => Get(0xf453);

            public IconUsage Gallery24 => Get(0xf454);

            public IconUsage Games24 => Get(0xf455);

            public IconUsage Gesture24 => Get(0xf456);

            public IconUsage Gif20 => Get(0xf457);

            public IconUsage Gif24 => Get(0xf458);

            public IconUsage Gift20 => Get(0xf459);

            public IconUsage Gift24 => Get(0xf45a);

            public IconUsage Glance24 => Get(0xf45b);

            public IconUsage Glasses24 => Get(0xf45c);

            public IconUsage GlassesOff24 => Get(0xf45d);

            public IconUsage Globe20 => Get(0xf45e);

            public IconUsage Globe24 => Get(0xf45f);

            public IconUsage GlobeAdd24 => Get(0xf460);

            public IconUsage GlobeClock24 => Get(0xf461);

            public IconUsage GlobeDesktop24 => Get(0xf462);

            public IconUsage GlobeLocation24 => Get(0xf463);

            public IconUsage GlobeSearch24 => Get(0xf464);

            public IconUsage GlobeVideo24 => Get(0xf465);

            public IconUsage Grid20 => Get(0xf466);

            public IconUsage Grid24 => Get(0xf467);

            public IconUsage Grid28 => Get(0xf468);

            public IconUsage Group20 => Get(0xf469);

            public IconUsage Group24 => Get(0xf46a);

            public IconUsage GroupList24 => Get(0xf46b);

            public IconUsage Guest16 => Get(0xf46c);

            public IconUsage Guest20 => Get(0xf46d);

            public IconUsage Guest24 => Get(0xf46e);

            public IconUsage Guest28 => Get(0xf46f);

            public IconUsage GuestAdd24 => Get(0xf470);

            public IconUsage HandRaise24 => Get(0xf471);

            public IconUsage Handshake16 => Get(0xf472);

            public IconUsage Handshake20 => Get(0xf473);

            public IconUsage Handshake24 => Get(0xf474);

            public IconUsage Hdr24 => Get(0xf475);

            public IconUsage Headphones24 => Get(0xf476);

            public IconUsage Headphones28 => Get(0xf477);

            public IconUsage Headset24 => Get(0xf478);

            public IconUsage Headset28 => Get(0xf479);

            public IconUsage HeadsetVr20 => Get(0xf47a);

            public IconUsage HeadsetVr24 => Get(0xf47b);

            public IconUsage Heart16 => Get(0xf47c);

            public IconUsage Heart20 => Get(0xf47d);

            public IconUsage Heart24 => Get(0xf47e);

            public IconUsage Highlight16 => Get(0xf47f);

            public IconUsage Highlight20 => Get(0xf480);

            public IconUsage Highlight24 => Get(0xf481);

            public IconUsage HighlightAccent16 => Get(0xf482);

            public IconUsage HighlightAccent20 => Get(0xf483);

            public IconUsage HighlightAccent24 => Get(0xf484);

            public IconUsage History20 => Get(0xf485);

            public IconUsage History24 => Get(0xf486);

            public IconUsage Home20 => Get(0xf487);

            public IconUsage Home24 => Get(0xf488);

            public IconUsage Home28 => Get(0xf489);

            public IconUsage HomeAdd24 => Get(0xf48a);

            public IconUsage HomeCheckmark24 => Get(0xf48b);

            public IconUsage Icons20 => Get(0xf48c);

            public IconUsage Icons24 => Get(0xf48d);

            public IconUsage Image16 => Get(0xf48e);

            public IconUsage Image20 => Get(0xf48f);

            public IconUsage Image24 => Get(0xf490);

            public IconUsage Image28 => Get(0xf491);

            public IconUsage Image48 => Get(0xf492);

            public IconUsage ImageAdd24 => Get(0xf493);

            public IconUsage ImageAltText20 => Get(0xf494);

            public IconUsage ImageAltText24 => Get(0xf495);

            public IconUsage ImageCopy20 => Get(0xf496);

            public IconUsage ImageCopy24 => Get(0xf497);

            public IconUsage ImageCopy28 => Get(0xf498);

            public IconUsage ImageEdit16 => Get(0xf499);

            public IconUsage ImageEdit20 => Get(0xf49a);

            public IconUsage ImageEdit24 => Get(0xf49b);

            public IconUsage ImageLibrary20 => Get(0xf49c);

            public IconUsage ImageLibrary24 => Get(0xf49d);

            public IconUsage ImageLibrary28 => Get(0xf49e);

            public IconUsage ImageOff24 => Get(0xf49f);

            public IconUsage ImageSearch20 => Get(0xf4a0);

            public IconUsage ImageSearch24 => Get(0xf4a1);

            public IconUsage ImmersiveReader20 => Get(0xf4a2);

            public IconUsage ImmersiveReader24 => Get(0xf4a3);

            public IconUsage Important12 => Get(0xf4a4);

            public IconUsage Important16 => Get(0xf4a5);

            public IconUsage Important20 => Get(0xf4a6);

            public IconUsage Important24 => Get(0xf4a7);

            public IconUsage Incognito24 => Get(0xf4a8);

            public IconUsage Info16 => Get(0xf4a9);

            public IconUsage Info20 => Get(0xf4aa);

            public IconUsage Info24 => Get(0xf4ab);

            public IconUsage Info28 => Get(0xf4ac);

            public IconUsage InkingTool16 => Get(0xf4ad);

            public IconUsage InkingTool20 => Get(0xf4ae);

            public IconUsage InkingTool24 => Get(0xf4af);

            public IconUsage InkingToolAccent16 => Get(0xf4b0);

            public IconUsage InkingToolAccent20 => Get(0xf4b1);

            public IconUsage InkingToolAccent24 => Get(0xf4b2);

            public IconUsage InprivateAccount16 => Get(0xf4b3);

            public IconUsage InprivateAccount20 => Get(0xf4b4);

            public IconUsage InprivateAccount24 => Get(0xf4b5);

            public IconUsage InprivateAccount28 => Get(0xf4b6);

            public IconUsage Insert20 => Get(0xf4b7);

            public IconUsage Inspect20 => Get(0xf4b8);

            public IconUsage Inspect24 => Get(0xf4b9);

            public IconUsage IosArrowLeft24 => Get(0xf4ba);

            public IconUsage IosChevron24 => Get(0xf4bb);

            public IconUsage IosChevronRight20 => Get(0xf4bc);

            public IconUsage Javascript16 => Get(0xf4bd);

            public IconUsage Javascript20 => Get(0xf4be);

            public IconUsage Javascript24 => Get(0xf4bf);

            public IconUsage Key20 => Get(0xf4c0);

            public IconUsage Key24 => Get(0xf4c1);

            public IconUsage Keyboard20 => Get(0xf4c2);

            public IconUsage Keyboard24 => Get(0xf4c3);

            public IconUsage KeyboardDock24 => Get(0xf4c4);

            public IconUsage KeyboardLayoutFloat24 => Get(0xf4c5);

            public IconUsage KeyboardLayoutOneHandedLeft24 => Get(0xf4c6);

            public IconUsage KeyboardLayoutResize24 => Get(0xf4c7);

            public IconUsage KeyboardLayoutSplit24 => Get(0xf4c8);

            public IconUsage KeyboardShift24 => Get(0xf4c9);

            public IconUsage KeyboardShiftUppercase24 => Get(0xf4ca);

            public IconUsage KeyboardTab24 => Get(0xf4cb);

            public IconUsage Laptop16 => Get(0xf4cc);

            public IconUsage Laptop20 => Get(0xf4cd);

            public IconUsage Laptop24 => Get(0xf4ce);

            public IconUsage Laptop28 => Get(0xf4cf);

            public IconUsage Large16 => Get(0xf4d0);

            public IconUsage Large20 => Get(0xf4d1);

            public IconUsage Large24 => Get(0xf4d2);

            public IconUsage Lasso24 => Get(0xf4d3);

            public IconUsage LauncherSettings24 => Get(0xf4d4);

            public IconUsage Layer20 => Get(0xf4d5);

            public IconUsage Layer24 => Get(0xf4d6);

            public IconUsage Leaf16 => Get(0xf4d7);

            public IconUsage Leaf20 => Get(0xf4d8);

            public IconUsage Leaf24 => Get(0xf4d9);

            public IconUsage LeafTwo16 => Get(0xf4da);

            public IconUsage LeafTwo20 => Get(0xf4db);

            public IconUsage LeafTwo24 => Get(0xf4dc);

            public IconUsage Library24 => Get(0xf4dd);

            public IconUsage Library28 => Get(0xf4de);

            public IconUsage Lightbulb16 => Get(0xf4df);

            public IconUsage Lightbulb20 => Get(0xf4e0);

            public IconUsage Lightbulb24 => Get(0xf4e1);

            public IconUsage LightbulbCircle24 => Get(0xf4e2);

            public IconUsage LightbulbFilament16 => Get(0xf4e3);

            public IconUsage LightbulbFilament20 => Get(0xf4e4);

            public IconUsage LightbulbFilament24 => Get(0xf4e5);

            public IconUsage Like16 => Get(0xf4e6);

            public IconUsage Likert16 => Get(0xf4e7);

            public IconUsage Likert20 => Get(0xf4e8);

            public IconUsage Likert24 => Get(0xf4e9);

            public IconUsage LineHorizontal120 => Get(0xf4ea);

            public IconUsage LineHorizontal320 => Get(0xf4eb);

            public IconUsage LineHorizontal520 => Get(0xf4ec);

            public IconUsage Link16 => Get(0xf4ed);

            public IconUsage Link20 => Get(0xf4ee);

            public IconUsage Link24 => Get(0xf4ef);

            public IconUsage Link28 => Get(0xf4f0);

            public IconUsage Link48 => Get(0xf4f1);

            public IconUsage LinkEdit16 => Get(0xf4f2);

            public IconUsage LinkEdit20 => Get(0xf4f3);

            public IconUsage LinkEdit24 => Get(0xf4f4);

            public IconUsage LinkRemove20 => Get(0xf4f5);

            public IconUsage LinkSquare24 => Get(0xf4f6);

            public IconUsage List20 => Get(0xf4f7);

            public IconUsage List24 => Get(0xf4f8);

            public IconUsage List28 => Get(0xf4f9);

            public IconUsage Live20 => Get(0xf4fa);

            public IconUsage Live24 => Get(0xf4fb);

            public IconUsage LocalLanguage16 => Get(0xf4fc);

            public IconUsage LocalLanguage20 => Get(0xf4fd);

            public IconUsage LocalLanguage24 => Get(0xf4fe);

            public IconUsage LocalLanguage28 => Get(0xf4ff);

            public IconUsage Location12 => Get(0xf500);

            public IconUsage Location16 => Get(0xf501);

            public IconUsage Location20 => Get(0xf502);

            public IconUsage Location24 => Get(0xf503);

            public IconUsage Location28 => Get(0xf504);

            public IconUsage LocationLive20 => Get(0xf505);

            public IconUsage LocationLive24 => Get(0xf506);

            public IconUsage LocationNotFound24 => Get(0xf507);

            public IconUsage Lock12 => Get(0xf508);

            public IconUsage Lock16 => Get(0xf509);

            public IconUsage Lock20 => Get(0xf50a);

            public IconUsage Lock24 => Get(0xf50b);

            public IconUsage LockShield20 => Get(0xf50c);

            public IconUsage LockShield24 => Get(0xf50d);

            public IconUsage LockShield48 => Get(0xf50e);

            public IconUsage MagicWand24 => Get(0xf50f);

            public IconUsage Mail20 => Get(0xf510);

            public IconUsage Mail24 => Get(0xf511);

            public IconUsage Mail28 => Get(0xf512);

            public IconUsage Mail48 => Get(0xf513);

            public IconUsage MailAdd24 => Get(0xf514);

            public IconUsage MailAll20 => Get(0xf515);

            public IconUsage MailAll24 => Get(0xf516);

            public IconUsage MailAllAccounts24 => Get(0xf517);

            public IconUsage MailAllRead20 => Get(0xf518);

            public IconUsage MailAllUnread20 => Get(0xf519);

            public IconUsage MailClock20 => Get(0xf51a);

            public IconUsage MailCopy20 => Get(0xf51b);

            public IconUsage MailCopy24 => Get(0xf51c);

            public IconUsage MailInbox16 => Get(0xf51d);

            public IconUsage MailInbox20 => Get(0xf51e);

            public IconUsage MailInbox24 => Get(0xf51f);

            public IconUsage MailInbox28 => Get(0xf520);

            public IconUsage MailInboxAdd16 => Get(0xf521);

            public IconUsage MailInboxAdd20 => Get(0xf522);

            public IconUsage MailInboxAdd24 => Get(0xf523);

            public IconUsage MailInboxAdd28 => Get(0xf524);

            public IconUsage MailInboxDismiss16 => Get(0xf525);

            public IconUsage MailInboxDismiss20 => Get(0xf526);

            public IconUsage MailInboxDismiss24 => Get(0xf527);

            public IconUsage MailInboxDismiss28 => Get(0xf528);

            public IconUsage MailMoveToFocussed24 => Get(0xf529);

            public IconUsage MailOutbox24 => Get(0xf52a);

            public IconUsage MailRead20 => Get(0xf52b);

            public IconUsage MailRead24 => Get(0xf52c);

            public IconUsage MailRead28 => Get(0xf52d);

            public IconUsage MailRead48 => Get(0xf52e);

            public IconUsage MailUnread16 => Get(0xf52f);

            public IconUsage MailUnread20 => Get(0xf530);

            public IconUsage MailUnread24 => Get(0xf531);

            public IconUsage MailUnread28 => Get(0xf532);

            public IconUsage MailUnread48 => Get(0xf533);

            public IconUsage MailUnsubscribe24 => Get(0xf534);

            public IconUsage MalwareDetected16 => Get(0xf535);

            public IconUsage MalwareDetected24 => Get(0xf536);

            public IconUsage Manufacturer24 => Get(0xf537);

            public IconUsage Map24 => Get(0xf538);

            public IconUsage MapDrive16 => Get(0xf539);

            public IconUsage MapDrive20 => Get(0xf53a);

            public IconUsage MapDrive24 => Get(0xf53b);

            public IconUsage MatchAppLayout24 => Get(0xf53c);

            public IconUsage Maximize16 => Get(0xf53d);

            public IconUsage MeetNow20 => Get(0xf53e);

            public IconUsage MeetNow24 => Get(0xf53f);

            public IconUsage Megaphone16 => Get(0xf540);

            public IconUsage Megaphone20 => Get(0xf541);

            public IconUsage Megaphone24 => Get(0xf542);

            public IconUsage Megaphone28 => Get(0xf543);

            public IconUsage MegaphoneOff24 => Get(0xf544);

            public IconUsage Mention16 => Get(0xf545);

            public IconUsage Mention20 => Get(0xf546);

            public IconUsage Mention24 => Get(0xf547);

            public IconUsage Merge24 => Get(0xf548);

            public IconUsage MicOff12 => Get(0xf549);

            public IconUsage MicOff16 => Get(0xf54a);

            public IconUsage MicOff24 => Get(0xf54b);

            public IconUsage MicOff28 => Get(0xf54c);

            public IconUsage MicOn16 => Get(0xf54d);

            public IconUsage MicOn20 => Get(0xf54e);

            public IconUsage MicOn24 => Get(0xf54f);

            public IconUsage MicOn28 => Get(0xf550);

            public IconUsage MicOn48 => Get(0xf551);

            public IconUsage MicSettings24 => Get(0xf552);

            public IconUsage Midi20 => Get(0xf553);

            public IconUsage Midi24 => Get(0xf554);

            public IconUsage MissingMetadata16 => Get(0xf555);

            public IconUsage MissingMetadata24 => Get(0xf556);

            public IconUsage MobileOptimized24 => Get(0xf557);

            public IconUsage Money16 => Get(0xf558);

            public IconUsage Money20 => Get(0xf559);

            public IconUsage Money24 => Get(0xf55a);

            public IconUsage More16 => Get(0xf55b);

            public IconUsage More20 => Get(0xf55c);

            public IconUsage More24 => Get(0xf55d);

            public IconUsage More28 => Get(0xf55e);

            public IconUsage More48 => Get(0xf55f);

            public IconUsage MoreVertical20 => Get(0xf560);

            public IconUsage MoreVertical24 => Get(0xf561);

            public IconUsage MoreVertical28 => Get(0xf562);

            public IconUsage MoreVertical48 => Get(0xf563);

            public IconUsage MoviesAndTv24 => Get(0xf564);

            public IconUsage Multiselect20 => Get(0xf565);

            public IconUsage Multiselect24 => Get(0xf566);

            public IconUsage Music20 => Get(0xf567);

            public IconUsage Music24 => Get(0xf568);

            public IconUsage MyLocation24 => Get(0xf569);

            public IconUsage Navigation20 => Get(0xf56a);

            public IconUsage Navigation24 => Get(0xf56b);

            public IconUsage NetworkCheck24 => Get(0xf56c);

            public IconUsage New16 => Get(0xf56d);

            public IconUsage New24 => Get(0xf56e);

            public IconUsage News20 => Get(0xf56f);

            public IconUsage News24 => Get(0xf570);

            public IconUsage News28 => Get(0xf571);

            public IconUsage Next16 => Get(0xf572);

            public IconUsage Next20 => Get(0xf573);

            public IconUsage Next24 => Get(0xf574);

            public IconUsage Note20 => Get(0xf575);

            public IconUsage Note24 => Get(0xf576);

            public IconUsage NoteAdd16 => Get(0xf577);

            public IconUsage NoteAdd20 => Get(0xf578);

            public IconUsage NoteAdd24 => Get(0xf579);

            public IconUsage Notebook24 => Get(0xf57a);

            public IconUsage NotebookError24 => Get(0xf57b);

            public IconUsage NotebookLightning24 => Get(0xf57c);

            public IconUsage NotebookQuestionMark24 => Get(0xf57d);

            public IconUsage NotebookSection24 => Get(0xf57e);

            public IconUsage NotebookSync24 => Get(0xf57f);

            public IconUsage Notepad20 => Get(0xf580);

            public IconUsage Notepad24 => Get(0xf581);

            public IconUsage Notepad28 => Get(0xf582);

            public IconUsage NumberRow16 => Get(0xf583);

            public IconUsage NumberRow20 => Get(0xf584);

            public IconUsage NumberRow24 => Get(0xf585);

            public IconUsage NumberSymbol16 => Get(0xf586);

            public IconUsage NumberSymbol20 => Get(0xf587);

            public IconUsage NumberSymbol24 => Get(0xf588);

            public IconUsage OfficeApps24 => Get(0xf589);

            public IconUsage OfficeApps28 => Get(0xf58a);

            public IconUsage Open16 => Get(0xf58b);

            public IconUsage Open20 => Get(0xf58c);

            public IconUsage Open24 => Get(0xf58d);

            public IconUsage OpenFolder24 => Get(0xf58e);

            public IconUsage OpenInBrowser24 => Get(0xf58f);

            public IconUsage Options16 => Get(0xf590);

            public IconUsage Options20 => Get(0xf591);

            public IconUsage Options24 => Get(0xf592);

            public IconUsage Organization20 => Get(0xf593);

            public IconUsage Organization24 => Get(0xf594);

            public IconUsage Organization28 => Get(0xf595);

            public IconUsage Owner24 => Get(0xf596);

            public IconUsage Page20 => Get(0xf597);

            public IconUsage PageFit16 => Get(0xf598);

            public IconUsage PageFit20 => Get(0xf599);

            public IconUsage PageFit24 => Get(0xf59a);

            public IconUsage PaintBrush16 => Get(0xf59b);

            public IconUsage PaintBrush20 => Get(0xf59c);

            public IconUsage PaintBrush24 => Get(0xf59d);

            public IconUsage PaintBucket16 => Get(0xf59e);

            public IconUsage PaintBucket20 => Get(0xf59f);

            public IconUsage PaintBucket24 => Get(0xf5a0);

            public IconUsage Pair24 => Get(0xf5a1);

            public IconUsage PaneClose16 => Get(0xf5a2);

            public IconUsage PaneClose20 => Get(0xf5a3);

            public IconUsage PaneClose24 => Get(0xf5a4);

            public IconUsage PaneOpen16 => Get(0xf5a5);

            public IconUsage PaneOpen20 => Get(0xf5a6);

            public IconUsage PaneOpen24 => Get(0xf5a7);

            public IconUsage Password24 => Get(0xf5a8);

            public IconUsage Patient24 => Get(0xf5a9);

            public IconUsage Pause16 => Get(0xf5aa);

            public IconUsage Pause20 => Get(0xf5ab);

            public IconUsage Pause24 => Get(0xf5ac);

            public IconUsage Pause48 => Get(0xf5ad);

            public IconUsage Payment20 => Get(0xf5ae);

            public IconUsage Payment24 => Get(0xf5af);

            public IconUsage PenSettings24 => Get(0xf5b0);

            public IconUsage People16 => Get(0xf5b1);

            public IconUsage People20 => Get(0xf5b2);

            public IconUsage People24 => Get(0xf5b3);

            public IconUsage People28 => Get(0xf5b4);

            public IconUsage PeopleAdd16 => Get(0xf5b5);

            public IconUsage PeopleAdd20 => Get(0xf5b6);

            public IconUsage PeopleAdd24 => Get(0xf5b7);

            public IconUsage PeopleAudience24 => Get(0xf5b8);

            public IconUsage PeopleCommunity16 => Get(0xf5b9);

            public IconUsage PeopleCommunity20 => Get(0xf5ba);

            public IconUsage PeopleCommunity24 => Get(0xf5bb);

            public IconUsage PeopleCommunity28 => Get(0xf5bc);

            public IconUsage PeopleCommunityAdd24 => Get(0xf5bd);

            public IconUsage PeopleProhibited20 => Get(0xf5be);

            public IconUsage PeopleSearch24 => Get(0xf5bf);

            public IconUsage PeopleSettings20 => Get(0xf5c0);

            public IconUsage PeopleTeam16 => Get(0xf5c1);

            public IconUsage PeopleTeam20 => Get(0xf5c2);

            public IconUsage PeopleTeam24 => Get(0xf5c3);

            public IconUsage PeopleTeam28 => Get(0xf5c4);

            public IconUsage Person12 => Get(0xf5c5);

            public IconUsage Person16 => Get(0xf5c6);

            public IconUsage Person20 => Get(0xf5c7);

            public IconUsage Person24 => Get(0xf5c8);

            public IconUsage Person28 => Get(0xf5c9);

            public IconUsage Person48 => Get(0xf5ca);

            public IconUsage PersonAccounts24 => Get(0xf5cb);

            public IconUsage PersonAdd20 => Get(0xf5cc);

            public IconUsage PersonAdd24 => Get(0xf5cd);

            public IconUsage PersonArrowLeft20 => Get(0xf5ce);

            public IconUsage PersonArrowLeft24 => Get(0xf5cf);

            public IconUsage PersonArrowRight16 => Get(0xf5d0);

            public IconUsage PersonArrowRight20 => Get(0xf5d1);

            public IconUsage PersonArrowRight24 => Get(0xf5d2);

            public IconUsage PersonAvailable16 => Get(0xf5d3);

            public IconUsage PersonAvailable24 => Get(0xf5d4);

            public IconUsage PersonBlock24 => Get(0xf5d5);

            public IconUsage PersonBoard16 => Get(0xf5d6);

            public IconUsage PersonBoard20 => Get(0xf5d7);

            public IconUsage PersonBoard24 => Get(0xf5d8);

            public IconUsage PersonCall24 => Get(0xf5d9);

            public IconUsage PersonDelete16 => Get(0xf5da);

            public IconUsage PersonDelete24 => Get(0xf5db);

            public IconUsage PersonFeedback20 => Get(0xf5dc);

            public IconUsage PersonFeedback24 => Get(0xf5dd);

            public IconUsage PersonProhibited20 => Get(0xf5de);

            public IconUsage PersonQuestionMark16 => Get(0xf5df);

            public IconUsage PersonQuestionMark20 => Get(0xf5e0);

            public IconUsage PersonQuestionMark24 => Get(0xf5e1);

            public IconUsage PersonSupport16 => Get(0xf5e2);

            public IconUsage PersonSupport20 => Get(0xf5e3);

            public IconUsage PersonSupport24 => Get(0xf5e4);

            public IconUsage PersonSwap16 => Get(0xf5e5);

            public IconUsage PersonSwap20 => Get(0xf5e6);

            public IconUsage PersonSwap24 => Get(0xf5e7);

            public IconUsage PersonVoice20 => Get(0xf5e8);

            public IconUsage PersonVoice24 => Get(0xf5e9);

            public IconUsage Phone20 => Get(0xf5ea);

            public IconUsage Phone24 => Get(0xf5eb);

            public IconUsage Phone28 => Get(0xf5ec);

            public IconUsage PhoneAddNewApp24 => Get(0xf5ed);

            public IconUsage PhoneDesktop16 => Get(0xf5ee);

            public IconUsage PhoneDesktop20 => Get(0xf5ef);

            public IconUsage PhoneDesktop24 => Get(0xf5f0);

            public IconUsage PhoneDesktop28 => Get(0xf5f1);

            public IconUsage PhoneError24 => Get(0xf5f2);

            public IconUsage PhoneHomeLock24 => Get(0xf5f3);

            public IconUsage PhoneLaptop20 => Get(0xf5f4);

            public IconUsage PhoneLaptop24 => Get(0xf5f5);

            public IconUsage PhoneLinkSetup24 => Get(0xf5f6);

            public IconUsage PhoneMobile20 => Get(0xf5f7);

            public IconUsage PhoneMobile24 => Get(0xf5f8);

            public IconUsage PhonePageHeader24 => Get(0xf5f9);

            public IconUsage PhonePagination24 => Get(0xf5fa);

            public IconUsage PhoneScreenTime24 => Get(0xf5fb);

            public IconUsage PhoneShake24 => Get(0xf5fc);

            public IconUsage PhoneStatusBar24 => Get(0xf5fd);

            public IconUsage PhoneTablet20 => Get(0xf5fe);

            public IconUsage PhoneTablet24 => Get(0xf5ff);

            public IconUsage PhoneToPc20 => Get(0xf600);

            public IconUsage PhoneToPc24 => Get(0xf601);

            public IconUsage PhoneUpdate24 => Get(0xf602);

            public IconUsage PhoneVerticalScroll24 => Get(0xf603);

            public IconUsage PhoneVibrate24 => Get(0xf604);

            public IconUsage PhotoFilter24 => Get(0xf605);

            public IconUsage PictureInPicture16 => Get(0xf606);

            public IconUsage PictureInPicture20 => Get(0xf607);

            public IconUsage PictureInPicture24 => Get(0xf608);

            public IconUsage Pin12 => Get(0xf609);

            public IconUsage Pin16 => Get(0xf60a);

            public IconUsage Pin20 => Get(0xf60b);

            public IconUsage Pin24 => Get(0xf60c);

            public IconUsage PinOff20 => Get(0xf60d);

            public IconUsage PinOff24 => Get(0xf60e);

            public IconUsage Play20 => Get(0xf60f);

            public IconUsage Play24 => Get(0xf610);

            public IconUsage Play48 => Get(0xf611);

            public IconUsage PlayCircle24 => Get(0xf612);

            public IconUsage PlugDisconnected20 => Get(0xf613);

            public IconUsage PlugDisconnected24 => Get(0xf614);

            public IconUsage PlugDisconnected28 => Get(0xf615);

            public IconUsage PointScan24 => Get(0xf616);

            public IconUsage Poll24 => Get(0xf617);

            public IconUsage Power20 => Get(0xf618);

            public IconUsage Power24 => Get(0xf619);

            public IconUsage Power28 => Get(0xf61a);

            public IconUsage Predictions24 => Get(0xf61b);

            public IconUsage Premium16 => Get(0xf61c);

            public IconUsage Premium20 => Get(0xf61d);

            public IconUsage Premium24 => Get(0xf61e);

            public IconUsage Premium28 => Get(0xf61f);

            public IconUsage PresenceAvailable10 => Get(0xf620);

            public IconUsage PresenceAvailable12 => Get(0xf621);

            public IconUsage PresenceAvailable16 => Get(0xf622);

            public IconUsage PresenceAway10 => Get(0xf623);

            public IconUsage PresenceAway12 => Get(0xf624);

            public IconUsage PresenceAway16 => Get(0xf625);

            public IconUsage PresenceBusy10 => Get(0xf626);

            public IconUsage PresenceBusy12 => Get(0xf627);

            public IconUsage PresenceBusy16 => Get(0xf628);

            public IconUsage PresenceDnd10 => Get(0xf629);

            public IconUsage PresenceDnd12 => Get(0xf62a);

            public IconUsage PresenceDnd16 => Get(0xf62b);

            public IconUsage Presenter24 => Get(0xf62c);

            public IconUsage PresenterOff24 => Get(0xf62d);

            public IconUsage PreviewLink16 => Get(0xf62e);

            public IconUsage PreviewLink20 => Get(0xf62f);

            public IconUsage PreviewLink24 => Get(0xf630);

            public IconUsage Previous16 => Get(0xf631);

            public IconUsage Previous20 => Get(0xf632);

            public IconUsage Previous24 => Get(0xf633);

            public IconUsage Print20 => Get(0xf634);

            public IconUsage Print24 => Get(0xf635);

            public IconUsage Print48 => Get(0xf636);

            public IconUsage Prohibited20 => Get(0xf637);

            public IconUsage Prohibited24 => Get(0xf638);

            public IconUsage Prohibited28 => Get(0xf639);

            public IconUsage Prohibited48 => Get(0xf63a);

            public IconUsage ProofreadLanguage24 => Get(0xf63b);

            public IconUsage ProtocolHandler16 => Get(0xf63c);

            public IconUsage ProtocolHandler20 => Get(0xf63d);

            public IconUsage ProtocolHandler24 => Get(0xf63e);

            public IconUsage QrCode24 => Get(0xf63f);

            public IconUsage QrCode28 => Get(0xf640);

            public IconUsage Question16 => Get(0xf641);

            public IconUsage Question20 => Get(0xf642);

            public IconUsage Question24 => Get(0xf643);

            public IconUsage Question28 => Get(0xf644);

            public IconUsage Question48 => Get(0xf645);

            public IconUsage QuestionCircle16 => Get(0xf646);

            public IconUsage QuestionCircle20 => Get(0xf647);

            public IconUsage QuestionCircle24 => Get(0xf648);

            public IconUsage QuestionCircle28 => Get(0xf649);

            public IconUsage QuestionCircle48 => Get(0xf64a);

            public IconUsage QuizNew24 => Get(0xf64b);

            public IconUsage QuizNew28 => Get(0xf64c);

            public IconUsage QuizNew48 => Get(0xf64d);

            public IconUsage RadioButton20 => Get(0xf64e);

            public IconUsage RadioButton24 => Get(0xf64f);

            public IconUsage RatingMature16 => Get(0xf650);

            public IconUsage RatingMature20 => Get(0xf651);

            public IconUsage RatingMature24 => Get(0xf652);

            public IconUsage ReOrder16 => Get(0xf653);

            public IconUsage ReOrder24 => Get(0xf654);

            public IconUsage ReOrderDots20 => Get(0xf655);

            public IconUsage ReOrderDots24 => Get(0xf656);

            public IconUsage ReadAloud20 => Get(0xf657);

            public IconUsage ReadAloud24 => Get(0xf658);

            public IconUsage ReadOnly16 => Get(0xf659);

            public IconUsage ReadOnly24 => Get(0xf65a);

            public IconUsage ReadingList16 => Get(0xf65b);

            public IconUsage ReadingList20 => Get(0xf65c);

            public IconUsage ReadingList24 => Get(0xf65d);

            public IconUsage ReadingList28 => Get(0xf65e);

            public IconUsage ReadingListAdd16 => Get(0xf65f);

            public IconUsage ReadingListAdd20 => Get(0xf660);

            public IconUsage ReadingListAdd24 => Get(0xf661);

            public IconUsage ReadingListAdd28 => Get(0xf662);

            public IconUsage ReadingMode20 => Get(0xf663);

            public IconUsage ReadingMode24 => Get(0xf664);

            public IconUsage ReadingModeMobile20 => Get(0xf665);

            public IconUsage ReadingModeMobile24 => Get(0xf666);

            public IconUsage Reciept20 => Get(0xf667);

            public IconUsage Reciept24 => Get(0xf668);

            public IconUsage Recommended24 => Get(0xf669);

            public IconUsage Record16 => Get(0xf66a);

            public IconUsage Record20 => Get(0xf66b);

            public IconUsage Record24 => Get(0xf66c);

            public IconUsage Remove12 => Get(0xf66d);

            public IconUsage Remove16 => Get(0xf66e);

            public IconUsage Remove20 => Get(0xf66f);

            public IconUsage Remove24 => Get(0xf670);

            public IconUsage RemoveRecent24 => Get(0xf671);

            public IconUsage Rename16 => Get(0xf672);

            public IconUsage Rename20 => Get(0xf673);

            public IconUsage Rename24 => Get(0xf674);

            public IconUsage Rename28 => Get(0xf675);

            public IconUsage Resize20 => Get(0xf676);

            public IconUsage ResizeImage24 => Get(0xf677);

            public IconUsage ResizeTable24 => Get(0xf678);

            public IconUsage ResizeVideo24 => Get(0xf679);

            public IconUsage Restore16 => Get(0xf67a);

            public IconUsage Reward16 => Get(0xf67b);

            public IconUsage Reward20 => Get(0xf67c);

            public IconUsage Reward24 => Get(0xf67d);

            public IconUsage Rewind20 => Get(0xf67e);

            public IconUsage Rewind24 => Get(0xf67f);

            public IconUsage Rocket16 => Get(0xf680);

            public IconUsage Rocket20 => Get(0xf681);

            public IconUsage Rocket24 => Get(0xf682);

            public IconUsage Router24 => Get(0xf683);

            public IconUsage RowTriple24 => Get(0xf684);

            public IconUsage Ruler16 => Get(0xf685);

            public IconUsage Ruler20 => Get(0xf686);

            public IconUsage Ruler24 => Get(0xf687);

            public IconUsage Run24 => Get(0xf688);

            public IconUsage Save20 => Get(0xf689);

            public IconUsage Save24 => Get(0xf68a);

            public IconUsage SaveAs20 => Get(0xf68b);

            public IconUsage SaveAs24 => Get(0xf68c);

            public IconUsage SaveCopy24 => Get(0xf68d);

            public IconUsage Savings16 => Get(0xf68e);

            public IconUsage Savings20 => Get(0xf68f);

            public IconUsage Savings24 => Get(0xf690);

            public IconUsage ScaleFill24 => Get(0xf691);

            public IconUsage ScaleFit16 => Get(0xf692);

            public IconUsage ScaleFit20 => Get(0xf693);

            public IconUsage ScaleFit24 => Get(0xf694);

            public IconUsage Scan24 => Get(0xf695);

            public IconUsage Scratchpad24 => Get(0xf696);

            public IconUsage Screenshot20 => Get(0xf697);

            public IconUsage Screenshot24 => Get(0xf698);

            public IconUsage Search20 => Get(0xf699);

            public IconUsage Search24 => Get(0xf69a);

            public IconUsage Search28 => Get(0xf69b);

            public IconUsage SearchInfo24 => Get(0xf69c);

            public IconUsage SearchSquare24 => Get(0xf69d);

            public IconUsage SelectAll24 => Get(0xf69e);

            public IconUsage SelectAllOff24 => Get(0xf69f);

            public IconUsage SelectObject20 => Get(0xf6a0);

            public IconUsage SelectObject24 => Get(0xf6a1);

            public IconUsage Send20 => Get(0xf6a2);

            public IconUsage Send24 => Get(0xf6a3);

            public IconUsage Send28 => Get(0xf6a4);

            public IconUsage SendClock20 => Get(0xf6a5);

            public IconUsage SendCopy24 => Get(0xf6a6);

            public IconUsage SendLogging24 => Get(0xf6a7);

            public IconUsage SendToBack20 => Get(0xf6a8);

            public IconUsage SendToBack24 => Get(0xf6a9);

            public IconUsage SerialPort16 => Get(0xf6aa);

            public IconUsage SerialPort20 => Get(0xf6ab);

            public IconUsage SerialPort24 => Get(0xf6ac);

            public IconUsage ServiceBell24 => Get(0xf6ad);

            public IconUsage SetTopStack16 => Get(0xf6ae);

            public IconUsage SetTopStack20 => Get(0xf6af);

            public IconUsage SetTopStack24 => Get(0xf6b0);

            public IconUsage Settings16 => Get(0xf6b1);

            public IconUsage Settings20 => Get(0xf6b2);

            public IconUsage Settings24 => Get(0xf6b3);

            public IconUsage Settings28 => Get(0xf6b4);

            public IconUsage Shapes16 => Get(0xf6b5);

            public IconUsage Shapes20 => Get(0xf6b6);

            public IconUsage Shapes24 => Get(0xf6b7);

            public IconUsage Share20 => Get(0xf6b8);

            public IconUsage Share24 => Get(0xf6b9);

            public IconUsage ShareAndroid20 => Get(0xf6ba);

            public IconUsage ShareAndroid24 => Get(0xf6bb);

            public IconUsage ShareCloseTray24 => Get(0xf6bc);

            public IconUsage ShareDesktop24 => Get(0xf6bd);

            public IconUsage ShareIos20 => Get(0xf6be);

            public IconUsage ShareIos24 => Get(0xf6bf);

            public IconUsage ShareIos28 => Get(0xf6c0);

            public IconUsage ShareIos48 => Get(0xf6c1);

            public IconUsage ShareScreen20 => Get(0xf6c2);

            public IconUsage ShareScreen24 => Get(0xf6c3);

            public IconUsage ShareScreen28 => Get(0xf6c4);

            public IconUsage ShareStop24 => Get(0xf6c5);

            public IconUsage ShareStop28 => Get(0xf6c6);

            public IconUsage Shield20 => Get(0xf6c7);

            public IconUsage Shield24 => Get(0xf6c8);

            public IconUsage ShieldDismiss20 => Get(0xf6c9);

            public IconUsage ShieldDismiss24 => Get(0xf6ca);

            public IconUsage ShieldError20 => Get(0xf6cb);

            public IconUsage ShieldError24 => Get(0xf6cc);

            public IconUsage ShieldKeyhole16 => Get(0xf6cd);

            public IconUsage ShieldKeyhole20 => Get(0xf6ce);

            public IconUsage ShieldKeyhole24 => Get(0xf6cf);

            public IconUsage ShieldProhibited20 => Get(0xf6d0);

            public IconUsage ShieldProhibited24 => Get(0xf6d1);

            public IconUsage Shifts24 => Get(0xf6d2);

            public IconUsage Shifts24H20 => Get(0xf6d3);

            public IconUsage Shifts24H24 => Get(0xf6d4);

            public IconUsage Shifts28 => Get(0xf6d5);

            public IconUsage Shifts30Minutes24 => Get(0xf6d6);

            public IconUsage ShiftsActivity20 => Get(0xf6d7);

            public IconUsage ShiftsActivity24 => Get(0xf6d8);

            public IconUsage ShiftsAdd24 => Get(0xf6d9);

            public IconUsage ShiftsApprove24 => Get(0xf6da);

            public IconUsage ShiftsAvailability24 => Get(0xf6db);

            public IconUsage ShiftsDeny24 => Get(0xf6dc);

            public IconUsage ShiftsOpen20 => Get(0xf6dd);

            public IconUsage ShiftsOpen24 => Get(0xf6de);

            public IconUsage ShiftsPending24 => Get(0xf6df);

            public IconUsage ShiftsTeam24 => Get(0xf6e0);

            public IconUsage Ship20 => Get(0xf6e1);

            public IconUsage Ship24 => Get(0xf6e2);

            public IconUsage SignOut24 => Get(0xf6e3);

            public IconUsage Signature16 => Get(0xf6e4);

            public IconUsage Signature20 => Get(0xf6e5);

            public IconUsage Signature24 => Get(0xf6e6);

            public IconUsage Signature28 => Get(0xf6e7);

            public IconUsage Signed16 => Get(0xf6e8);

            public IconUsage Signed20 => Get(0xf6e9);

            public IconUsage Signed24 => Get(0xf6ea);

            public IconUsage Sim16 => Get(0xf6eb);

            public IconUsage Sim20 => Get(0xf6ec);

            public IconUsage Sim24 => Get(0xf6ed);

            public IconUsage Sleep24 => Get(0xf6ee);

            public IconUsage SlideAdd24 => Get(0xf6ef);

            public IconUsage SlideDesign24 => Get(0xf6f0);

            public IconUsage SlideHide24 => Get(0xf6f1);

            public IconUsage SlideLayout20 => Get(0xf6f2);

            public IconUsage SlideLayout24 => Get(0xf6f3);

            public IconUsage SlideMicrophone24 => Get(0xf6f4);

            public IconUsage SlideText24 => Get(0xf6f5);

            public IconUsage SlowMode16 => Get(0xf6f6);

            public IconUsage SlowMode20 => Get(0xf6f7);

            public IconUsage SlowMode24 => Get(0xf6f8);

            public IconUsage SlowMode28 => Get(0xf6f9);

            public IconUsage Small16 => Get(0xf6fa);

            public IconUsage Small20 => Get(0xf6fb);

            public IconUsage Small24 => Get(0xf6fc);

            public IconUsage Snooze16 => Get(0xf6fd);

            public IconUsage Snooze24 => Get(0xf6fe);

            public IconUsage SoundSource24 => Get(0xf6ff);

            public IconUsage SoundSource28 => Get(0xf700);

            public IconUsage Spacebar24 => Get(0xf701);

            public IconUsage Speaker024 => Get(0xf702);

            public IconUsage Speaker16 => Get(0xf703);

            public IconUsage Speaker124 => Get(0xf704);

            public IconUsage Speaker20 => Get(0xf705);

            public IconUsage Speaker24 => Get(0xf706);

            public IconUsage Speaker28 => Get(0xf707);

            public IconUsage SpeakerBluetooth24 => Get(0xf708);

            public IconUsage SpeakerEdit16 => Get(0xf709);

            public IconUsage SpeakerEdit20 => Get(0xf70a);

            public IconUsage SpeakerEdit24 => Get(0xf70b);

            public IconUsage SpeakerNone20 => Get(0xf70c);

            public IconUsage SpeakerNone24 => Get(0xf70d);

            public IconUsage SpeakerNone28 => Get(0xf70e);

            public IconUsage SpeakerOff24 => Get(0xf70f);

            public IconUsage SpeakerOff28 => Get(0xf710);

            public IconUsage SpeakerSettings24 => Get(0xf711);

            public IconUsage SpinnerIos20 => Get(0xf712);

            public IconUsage Sports16 => Get(0xf713);

            public IconUsage Sports20 => Get(0xf714);

            public IconUsage Sports24 => Get(0xf715);

            public IconUsage Star12 => Get(0xf716);

            public IconUsage Star16 => Get(0xf717);

            public IconUsage Star20 => Get(0xf718);

            public IconUsage Star24 => Get(0xf719);

            public IconUsage Star28 => Get(0xf71a);

            public IconUsage StarAdd16 => Get(0xf71b);

            public IconUsage StarAdd20 => Get(0xf71c);

            public IconUsage StarAdd24 => Get(0xf71d);

            public IconUsage StarArrowRight24 => Get(0xf71e);

            public IconUsage StarArrowRightStart24 => Get(0xf71f);

            public IconUsage StarEmphasis24 => Get(0xf720);

            public IconUsage StarHalf12 => Get(0xf721);

            public IconUsage StarHalf16 => Get(0xf722);

            public IconUsage StarHalf20 => Get(0xf723);

            public IconUsage StarHalf24 => Get(0xf724);

            public IconUsage StarHalf28 => Get(0xf725);

            public IconUsage StarOff12 => Get(0xf726);

            public IconUsage StarOff16 => Get(0xf727);

            public IconUsage StarOff20 => Get(0xf728);

            public IconUsage StarOff24 => Get(0xf729);

            public IconUsage StarOff28 => Get(0xf72a);

            public IconUsage StarOneQuarter12 => Get(0xf72b);

            public IconUsage StarOneQuarter16 => Get(0xf72c);

            public IconUsage StarOneQuarter20 => Get(0xf72d);

            public IconUsage StarOneQuarter24 => Get(0xf72e);

            public IconUsage StarOneQuarter28 => Get(0xf72f);

            public IconUsage StarProhibited16 => Get(0xf730);

            public IconUsage StarProhibited20 => Get(0xf731);

            public IconUsage StarProhibited24 => Get(0xf732);

            public IconUsage StarSettings24 => Get(0xf733);

            public IconUsage StarThreeQuarter12 => Get(0xf734);

            public IconUsage StarThreeQuarter16 => Get(0xf735);

            public IconUsage StarThreeQuarter20 => Get(0xf736);

            public IconUsage StarThreeQuarter24 => Get(0xf737);

            public IconUsage StarThreeQuarter28 => Get(0xf738);

            public IconUsage Status16 => Get(0xf739);

            public IconUsage Status20 => Get(0xf73a);

            public IconUsage Status24 => Get(0xf73b);

            public IconUsage Stethoscope20 => Get(0xf73c);

            public IconUsage Stethoscope24 => Get(0xf73d);

            public IconUsage Sticker20 => Get(0xf73e);

            public IconUsage Sticker24 => Get(0xf73f);

            public IconUsage StickerAdd24 => Get(0xf740);

            public IconUsage Stop16 => Get(0xf741);

            public IconUsage Stop20 => Get(0xf742);

            public IconUsage Stop24 => Get(0xf743);

            public IconUsage Storage24 => Get(0xf744);

            public IconUsage Store16 => Get(0xf745);

            public IconUsage Store20 => Get(0xf746);

            public IconUsage Store24 => Get(0xf747);

            public IconUsage StoreMicrosoft16 => Get(0xf748);

            public IconUsage StoreMicrosoft20 => Get(0xf749);

            public IconUsage StoreMicrosoft24 => Get(0xf74a);

            public IconUsage StyleGuide24 => Get(0xf74b);

            public IconUsage SubGrid24 => Get(0xf74c);

            public IconUsage Subway20 => Get(0xf74d);

            public IconUsage Subway24 => Get(0xf74e);

            public IconUsage Suggestion24 => Get(0xf74f);

            public IconUsage SurfaceEarbuds20 => Get(0xf750);

            public IconUsage SurfaceEarbuds24 => Get(0xf751);

            public IconUsage SurfaceHub20 => Get(0xf752);

            public IconUsage SurfaceHub24 => Get(0xf753);

            public IconUsage SwipeDown24 => Get(0xf754);

            public IconUsage SwipeRight24 => Get(0xf755);

            public IconUsage SwipeUp24 => Get(0xf756);

            public IconUsage Symbols24 => Get(0xf757);

            public IconUsage SyncOff16 => Get(0xf758);

            public IconUsage SyncOff20 => Get(0xf759);

            public IconUsage System24 => Get(0xf75a);

            public IconUsage Tab16 => Get(0xf75b);

            public IconUsage Tab20 => Get(0xf75c);

            public IconUsage Tab24 => Get(0xf75d);

            public IconUsage Tab28 => Get(0xf75e);

            public IconUsage TabDesktop20 => Get(0xf75f);

            public IconUsage TabDesktopArrowClockwise16 => Get(0xf760);

            public IconUsage TabDesktopArrowClockwise20 => Get(0xf761);

            public IconUsage TabDesktopArrowClockwise24 => Get(0xf762);

            public IconUsage TabDesktopClock20 => Get(0xf763);

            public IconUsage TabDesktopCopy20 => Get(0xf764);

            public IconUsage TabDesktopImage16 => Get(0xf765);

            public IconUsage TabDesktopImage20 => Get(0xf766);

            public IconUsage TabDesktopImage24 => Get(0xf767);

            public IconUsage TabDesktopMultiple20 => Get(0xf768);

            public IconUsage TabDesktopNewPage20 => Get(0xf769);

            public IconUsage TabInPrivate16 => Get(0xf76a);

            public IconUsage TabInPrivate20 => Get(0xf76b);

            public IconUsage TabInPrivate24 => Get(0xf76c);

            public IconUsage TabInPrivate28 => Get(0xf76d);

            public IconUsage TabInprivateAccount20 => Get(0xf76e);

            public IconUsage TabInprivateAccount24 => Get(0xf76f);

            public IconUsage TabNew20 => Get(0xf770);

            public IconUsage TabNew24 => Get(0xf771);

            public IconUsage TabSweep24 => Get(0xf772);

            public IconUsage TabTrackingPrevention20 => Get(0xf773);

            public IconUsage TabTrackingPrevention24 => Get(0xf774);

            public IconUsage Table20 => Get(0xf775);

            public IconUsage Table24 => Get(0xf776);

            public IconUsage TableAdd24 => Get(0xf777);

            public IconUsage TableCellsMerge20 => Get(0xf778);

            public IconUsage TableCellsMerge24 => Get(0xf779);

            public IconUsage TableCellsSplit20 => Get(0xf77a);

            public IconUsage TableCellsSplit24 => Get(0xf77b);

            public IconUsage TableColumnDelete24 => Get(0xf77c);

            public IconUsage TableColumnInsert24 => Get(0xf77d);

            public IconUsage TableColumnResize24 => Get(0xf77e);

            public IconUsage TableDelete24 => Get(0xf77f);

            public IconUsage TableEdit24 => Get(0xf780);

            public IconUsage TableFreeze24 => Get(0xf781);

            public IconUsage TableFreezeColumn24 => Get(0xf782);

            public IconUsage TableFreezeRow24 => Get(0xf783);

            public IconUsage TableInsertDown24 => Get(0xf784);

            public IconUsage TableInsertLeft24 => Get(0xf785);

            public IconUsage TableInsertRight24 => Get(0xf786);

            public IconUsage TableInsertUp24 => Get(0xf787);

            public IconUsage TableMoveDown24 => Get(0xf788);

            public IconUsage TableMoveLeft24 => Get(0xf789);

            public IconUsage TableMoveRight24 => Get(0xf78a);

            public IconUsage TableMoveUp24 => Get(0xf78b);

            public IconUsage TableRowDelete24 => Get(0xf78c);

            public IconUsage TableRowInsert24 => Get(0xf78d);

            public IconUsage TableRowResize24 => Get(0xf78e);

            public IconUsage TableSettings24 => Get(0xf78f);

            public IconUsage TableSwitch24 => Get(0xf790);

            public IconUsage Tablet20 => Get(0xf791);

            public IconUsage Tablet24 => Get(0xf792);

            public IconUsage Tabs24 => Get(0xf793);

            public IconUsage Tag20 => Get(0xf794);

            public IconUsage Tag24 => Get(0xf795);

            public IconUsage TapDouble24 => Get(0xf796);

            public IconUsage TapSingle24 => Get(0xf797);

            public IconUsage Target16 => Get(0xf798);

            public IconUsage Target20 => Get(0xf799);

            public IconUsage Target24 => Get(0xf79a);

            public IconUsage TargetEdit16 => Get(0xf79b);

            public IconUsage TargetEdit20 => Get(0xf79c);

            public IconUsage TargetEdit24 => Get(0xf79d);

            public IconUsage TaskList20 => Get(0xf79e);

            public IconUsage TaskList24 => Get(0xf79f);

            public IconUsage TaskListAdd20 => Get(0xf7a0);

            public IconUsage TaskListAdd24 => Get(0xf7a1);

            public IconUsage TasksApp24 => Get(0xf7a2);

            public IconUsage TasksApp28 => Get(0xf7a3);

            public IconUsage TeamAdd24 => Get(0xf7a4);

            public IconUsage TeamDelete24 => Get(0xf7a5);

            public IconUsage Teddy24 => Get(0xf7a6);

            public IconUsage Temperature20 => Get(0xf7a7);

            public IconUsage Temperature24 => Get(0xf7a8);

            public IconUsage Tent24 => Get(0xf7a9);

            public IconUsage TestCall24 => Get(0xf7aa);

            public IconUsage Text24 => Get(0xf7ab);

            public IconUsage TextAdd24 => Get(0xf7ac);

            public IconUsage TextAddSpaceAfter20 => Get(0xf7ad);

            public IconUsage TextAddSpaceAfter24 => Get(0xf7ae);

            public IconUsage TextAddSpaceBefore20 => Get(0xf7af);

            public IconUsage TextAddSpaceBefore24 => Get(0xf7b0);

            public IconUsage TextAlignCenter20 => Get(0xf7b1);

            public IconUsage TextAlignCenter24 => Get(0xf7b2);

            public IconUsage TextAlignDistributed20 => Get(0xf7b3);

            public IconUsage TextAlignDistributed24 => Get(0xf7b4);

            public IconUsage TextAlignJustify20 => Get(0xf7b5);

            public IconUsage TextAlignJustify24 => Get(0xf7b6);

            public IconUsage TextAlignLeft20 => Get(0xf7b7);

            public IconUsage TextAlignLeft24 => Get(0xf7b8);

            public IconUsage TextAlignRight20 => Get(0xf7b9);

            public IconUsage TextAlignRight24 => Get(0xf7ba);

            public IconUsage TextAsterisk20 => Get(0xf7bb);

            public IconUsage TextBold20 => Get(0xf7bc);

            public IconUsage TextBold24 => Get(0xf7bd);

            public IconUsage TextBulletList20 => Get(0xf7be);

            public IconUsage TextBulletList24 => Get(0xf7bf);

            public IconUsage TextBulletListAdd24 => Get(0xf7c0);

            public IconUsage TextBulletListSquare24 => Get(0xf7c1);

            public IconUsage TextBulletListSquareWarning16 => Get(0xf7c2);

            public IconUsage TextBulletListSquareWarning20 => Get(0xf7c3);

            public IconUsage TextBulletListSquareWarning24 => Get(0xf7c4);

            public IconUsage TextBulletListTree16 => Get(0xf7c5);

            public IconUsage TextBulletListTree20 => Get(0xf7c6);

            public IconUsage TextBulletListTree24 => Get(0xf7c7);

            public IconUsage TextChangeAccept20 => Get(0xf7c8);

            public IconUsage TextChangeAccept24 => Get(0xf7c9);

            public IconUsage TextChangeCase20 => Get(0xf7ca);

            public IconUsage TextChangeCase24 => Get(0xf7cb);

            public IconUsage TextChangeNext20 => Get(0xf7cc);

            public IconUsage TextChangeNext24 => Get(0xf7cd);

            public IconUsage TextChangePrevious20 => Get(0xf7ce);

            public IconUsage TextChangePrevious24 => Get(0xf7cf);

            public IconUsage TextChangeReject20 => Get(0xf7d0);

            public IconUsage TextChangeReject24 => Get(0xf7d1);

            public IconUsage TextChangeSettings20 => Get(0xf7d2);

            public IconUsage TextChangeSettings24 => Get(0xf7d3);

            public IconUsage TextClearFormatting20 => Get(0xf7d4);

            public IconUsage TextClearFormatting24 => Get(0xf7d5);

            public IconUsage TextCollapse24 => Get(0xf7d6);

            public IconUsage TextColor20 => Get(0xf7d7);

            public IconUsage TextColor24 => Get(0xf7d8);

            public IconUsage TextColumnOne20 => Get(0xf7d9);

            public IconUsage TextColumnOne24 => Get(0xf7da);

            public IconUsage TextColumnThree20 => Get(0xf7db);

            public IconUsage TextColumnThree24 => Get(0xf7dc);

            public IconUsage TextColumnTwo20 => Get(0xf7dd);

            public IconUsage TextColumnTwo24 => Get(0xf7de);

            public IconUsage TextColumnTwoLeft20 => Get(0xf7df);

            public IconUsage TextColumnTwoLeft24 => Get(0xf7e0);

            public IconUsage TextColumnTwoRight20 => Get(0xf7e1);

            public IconUsage TextColumnTwoRight24 => Get(0xf7e2);

            public IconUsage TextDescription20 => Get(0xf7e3);

            public IconUsage TextDescription24 => Get(0xf7e4);

            public IconUsage TextDirection20 => Get(0xf7e5);

            public IconUsage TextDirection24 => Get(0xf7e6);

            public IconUsage TextDirectionHorizontalLtr20 => Get(0xf7e7);

            public IconUsage TextDirectionHorizontalLtr24 => Get(0xf7e8);

            public IconUsage TextDirectionRotate27020 => Get(0xf7e9);

            public IconUsage TextDirectionRotate27024 => Get(0xf7ea);

            public IconUsage TextDirectionRotate270Ac20 => Get(0xf7eb);

            public IconUsage TextDirectionRotate270Ac24 => Get(0xf7ec);

            public IconUsage TextDirectionRotate9020 => Get(0xf7ed);

            public IconUsage TextDirectionRotate9024 => Get(0xf7ee);

            public IconUsage TextDirectionVertical20 => Get(0xf7ef);

            public IconUsage TextDirectionVertical24 => Get(0xf7f0);

            public IconUsage TextEditStyle20 => Get(0xf7f1);

            public IconUsage TextEditStyle24 => Get(0xf7f2);

            public IconUsage TextEffects20 => Get(0xf7f3);

            public IconUsage TextEffects24 => Get(0xf7f4);

            public IconUsage TextExpand24 => Get(0xf7f5);

            public IconUsage TextField16 => Get(0xf7f6);

            public IconUsage TextField20 => Get(0xf7f7);

            public IconUsage TextField24 => Get(0xf7f8);

            public IconUsage TextFirstLine20 => Get(0xf7f9);

            public IconUsage TextFirstLine24 => Get(0xf7fa);

            public IconUsage TextFont16 => Get(0xf7fb);

            public IconUsage TextFont20 => Get(0xf7fc);

            public IconUsage TextFont24 => Get(0xf7fd);

            public IconUsage TextFontSize20 => Get(0xf7fe);

            public IconUsage TextFontSize24 => Get(0xf7ff);

            public IconUsage TextFootnote20 => Get(0xf800);

            public IconUsage TextFootnote24 => Get(0xf801);

            public IconUsage TextGrammarOptions16 => Get(0xf802);

            public IconUsage TextGrammarOptions20 => Get(0xf803);

            public IconUsage TextGrammarOptions24 => Get(0xf804);

            public IconUsage TextHanging20 => Get(0xf805);

            public IconUsage TextHanging24 => Get(0xf806);

            public IconUsage TextHeader120 => Get(0xf807);

            public IconUsage TextHeader220 => Get(0xf808);

            public IconUsage TextHeader320 => Get(0xf809);

            public IconUsage TextIndentDecrease24 => Get(0xf80a);

            public IconUsage TextIndentIncrease24 => Get(0xf80b);

            public IconUsage TextItalic20 => Get(0xf80c);

            public IconUsage TextItalic24 => Get(0xf80d);

            public IconUsage TextLineSpacing20 => Get(0xf80e);

            public IconUsage TextLineSpacing24 => Get(0xf80f);

            public IconUsage TextNumberFormat20 => Get(0xf810);

            public IconUsage TextNumberFormat24 => Get(0xf811);

            public IconUsage TextNumberListLtr20 => Get(0xf812);

            public IconUsage TextNumberListLtr24 => Get(0xf813);

            public IconUsage TextNumberListRtl24 => Get(0xf814);

            public IconUsage TextParagraphSettings20 => Get(0xf815);

            public IconUsage TextParagraphSettings24 => Get(0xf816);

            public IconUsage TextProofingTools20 => Get(0xf817);

            public IconUsage TextProofingTools24 => Get(0xf818);

            public IconUsage TextQuote20 => Get(0xf819);

            public IconUsage TextQuote24 => Get(0xf81a);

            public IconUsage TextSortAscending20 => Get(0xf81b);

            public IconUsage TextSortDescending20 => Get(0xf81c);

            public IconUsage TextStrikethrough20 => Get(0xf81d);

            public IconUsage TextStrikethrough24 => Get(0xf81e);

            public IconUsage TextSubscript20 => Get(0xf81f);

            public IconUsage TextSubscript24 => Get(0xf820);

            public IconUsage TextSuperscript20 => Get(0xf821);

            public IconUsage TextSuperscript24 => Get(0xf822);

            public IconUsage TextUnderline20 => Get(0xf823);

            public IconUsage TextUnderline24 => Get(0xf824);

            public IconUsage TextWordCount20 => Get(0xf825);

            public IconUsage TextWordCount24 => Get(0xf826);

            public IconUsage TextWrap24 => Get(0xf827);

            public IconUsage Textbox20 => Get(0xf828);

            public IconUsage Textbox24 => Get(0xf829);

            public IconUsage TextboxAlign20 => Get(0xf82a);

            public IconUsage TextboxAlign24 => Get(0xf82b);

            public IconUsage TextboxAlignBottom20 => Get(0xf82c);

            public IconUsage TextboxAlignBottom24 => Get(0xf82d);

            public IconUsage TextboxAlignMiddle20 => Get(0xf82e);

            public IconUsage TextboxAlignMiddle24 => Get(0xf82f);

            public IconUsage TextboxAlignTop20 => Get(0xf830);

            public IconUsage TextboxAlignTop24 => Get(0xf831);

            public IconUsage TextboxVertical20 => Get(0xf832);

            public IconUsage TextboxVertical24 => Get(0xf833);

            public IconUsage Thinking20 => Get(0xf834);

            public IconUsage Thinking24 => Get(0xf835);

            public IconUsage ThumbDislike20 => Get(0xf836);

            public IconUsage ThumbDislike24 => Get(0xf837);

            public IconUsage ThumbLike20 => Get(0xf838);

            public IconUsage ThumbLike24 => Get(0xf839);

            public IconUsage Ticket20 => Get(0xf83a);

            public IconUsage Ticket24 => Get(0xf83b);

            public IconUsage TimeAndWeather24 => Get(0xf83c);

            public IconUsage TimePicker24 => Get(0xf83d);

            public IconUsage Timeline24 => Get(0xf83e);

            public IconUsage Timer1024 => Get(0xf83f);

            public IconUsage Timer24 => Get(0xf840);

            public IconUsage Timer224 => Get(0xf841);

            public IconUsage TimerOff24 => Get(0xf842);

            public IconUsage ToggleRight16 => Get(0xf843);

            public IconUsage ToggleRight20 => Get(0xf844);

            public IconUsage ToggleRight24 => Get(0xf845);

            public IconUsage Toolbox16 => Get(0xf846);

            public IconUsage Toolbox20 => Get(0xf847);

            public IconUsage Toolbox24 => Get(0xf848);

            public IconUsage Toolbox28 => Get(0xf849);

            public IconUsage TopSpeed24 => Get(0xf84a);

            public IconUsage Translate20 => Get(0xf84b);

            public IconUsage Translate24 => Get(0xf84c);

            public IconUsage Trophy16 => Get(0xf84d);

            public IconUsage Trophy20 => Get(0xf84e);

            public IconUsage Trophy24 => Get(0xf84f);

            public IconUsage UninstallApp24 => Get(0xf850);

            public IconUsage Unlock20 => Get(0xf851);

            public IconUsage Unlock24 => Get(0xf852);

            public IconUsage Unlock28 => Get(0xf853);

            public IconUsage Upload24 => Get(0xf854);

            public IconUsage UsbPort20 => Get(0xf855);

            public IconUsage UsbPort24 => Get(0xf856);

            public IconUsage UsbStick20 => Get(0xf857);

            public IconUsage UsbStick24 => Get(0xf858);

            public IconUsage Vault16 => Get(0xf859);

            public IconUsage Vault20 => Get(0xf85a);

            public IconUsage Vault24 => Get(0xf85b);

            public IconUsage VehicleBicycle24 => Get(0xf85c);

            public IconUsage VehicleBus24 => Get(0xf85d);

            public IconUsage VehicleCab24 => Get(0xf85e);

            public IconUsage VehicleCar16 => Get(0xf85f);

            public IconUsage VehicleCar20 => Get(0xf860);

            public IconUsage VehicleCar24 => Get(0xf861);

            public IconUsage VehicleTruck24 => Get(0xf862);

            public IconUsage Video16 => Get(0xf863);

            public IconUsage Video20 => Get(0xf864);

            public IconUsage Video24 => Get(0xf865);

            public IconUsage Video28 => Get(0xf866);

            public IconUsage VideoBackgroundEffect24 => Get(0xf867);

            public IconUsage VideoClip24 => Get(0xf868);

            public IconUsage VideoOff20 => Get(0xf869);

            public IconUsage VideoOff24 => Get(0xf86a);

            public IconUsage VideoOff28 => Get(0xf86b);

            public IconUsage VideoPerson24 => Get(0xf86c);

            public IconUsage VideoPersonOff24 => Get(0xf86d);

            public IconUsage VideoPersonStar24 => Get(0xf86e);

            public IconUsage VideoPlayPause24 => Get(0xf86f);

            public IconUsage VideoSecurity20 => Get(0xf870);

            public IconUsage VideoSecurity24 => Get(0xf871);

            public IconUsage VideoSwitch24 => Get(0xf872);

            public IconUsage ViewDesktop20 => Get(0xf873);

            public IconUsage ViewDesktop24 => Get(0xf874);

            public IconUsage ViewDesktopMobile20 => Get(0xf875);

            public IconUsage ViewDesktopMobile24 => Get(0xf876);

            public IconUsage VisualSearch16 => Get(0xf877);

            public IconUsage VisualSearch20 => Get(0xf878);

            public IconUsage VisualSearch24 => Get(0xf879);

            public IconUsage Voicemail16 => Get(0xf87a);

            public IconUsage Voicemail20 => Get(0xf87b);

            public IconUsage Voicemail24 => Get(0xf87c);

            public IconUsage WalkieTalkie24 => Get(0xf87d);

            public IconUsage WalkieTalkie28 => Get(0xf87e);

            public IconUsage Wallpaper24 => Get(0xf87f);

            public IconUsage Warning16 => Get(0xf880);

            public IconUsage Warning20 => Get(0xf881);

            public IconUsage Warning24 => Get(0xf882);

            public IconUsage WeatherBlowingSnow20 => Get(0xf883);

            public IconUsage WeatherBlowingSnow24 => Get(0xf884);

            public IconUsage WeatherBlowingSnow48 => Get(0xf885);

            public IconUsage WeatherCloudy20 => Get(0xf886);

            public IconUsage WeatherCloudy24 => Get(0xf887);

            public IconUsage WeatherCloudy48 => Get(0xf888);

            public IconUsage WeatherDuststorm20 => Get(0xf889);

            public IconUsage WeatherDuststorm24 => Get(0xf88a);

            public IconUsage WeatherDuststorm48 => Get(0xf88b);

            public IconUsage WeatherFog20 => Get(0xf88c);

            public IconUsage WeatherFog24 => Get(0xf88d);

            public IconUsage WeatherFog48 => Get(0xf88e);

            public IconUsage WeatherHailDay20 => Get(0xf88f);

            public IconUsage WeatherHailDay24 => Get(0xf890);

            public IconUsage WeatherHailDay48 => Get(0xf891);

            public IconUsage WeatherHailNight20 => Get(0xf892);

            public IconUsage WeatherHailNight24 => Get(0xf893);

            public IconUsage WeatherHailNight48 => Get(0xf894);

            public IconUsage WeatherMoon20 => Get(0xf895);

            public IconUsage WeatherMoon24 => Get(0xf896);

            public IconUsage WeatherMoon48 => Get(0xf897);

            public IconUsage WeatherPartlyCloudyDay20 => Get(0xf898);

            public IconUsage WeatherPartlyCloudyDay24 => Get(0xf899);

            public IconUsage WeatherPartlyCloudyDay48 => Get(0xf89a);

            public IconUsage WeatherPartlyCloudyNight20 => Get(0xf89b);

            public IconUsage WeatherPartlyCloudyNight24 => Get(0xf89c);

            public IconUsage WeatherPartlyCloudyNight48 => Get(0xf89d);

            public IconUsage WeatherRain20 => Get(0xf89e);

            public IconUsage WeatherRain24 => Get(0xf89f);

            public IconUsage WeatherRain48 => Get(0xf8a0);

            public IconUsage WeatherRainShowersDay20 => Get(0xf8a1);

            public IconUsage WeatherRainShowersDay24 => Get(0xf8a2);

            public IconUsage WeatherRainShowersDay48 => Get(0xf8a3);

            public IconUsage WeatherRainShowersNight20 => Get(0xf8a4);

            public IconUsage WeatherRainShowersNight24 => Get(0xf8a5);

            public IconUsage WeatherRainShowersNight48 => Get(0xf8a6);

            public IconUsage WeatherRainSnow20 => Get(0xf8a7);

            public IconUsage WeatherRainSnow24 => Get(0xf8a8);

            public IconUsage WeatherRainSnow48 => Get(0xf8a9);

            public IconUsage WeatherSnow20 => Get(0xf8aa);

            public IconUsage WeatherSnow24 => Get(0xf8ab);

            public IconUsage WeatherSnow48 => Get(0xf8ac);

            public IconUsage WeatherSnowShowerDay20 => Get(0xf8ad);

            public IconUsage WeatherSnowShowerDay24 => Get(0xf8ae);

            public IconUsage WeatherSnowShowerDay48 => Get(0xf8af);

            public IconUsage WeatherSnowShowerNight20 => Get(0xf8b0);

            public IconUsage WeatherSnowShowerNight24 => Get(0xf8b1);

            public IconUsage WeatherSnowShowerNight48 => Get(0xf8b2);

            public IconUsage WeatherSnowflake20 => Get(0xf8b3);

            public IconUsage WeatherSnowflake24 => Get(0xf8b4);

            public IconUsage WeatherSnowflake48 => Get(0xf8b5);

            public IconUsage WeatherSqualls20 => Get(0xf8b6);

            public IconUsage WeatherSqualls24 => Get(0xf8b7);

            public IconUsage WeatherSqualls48 => Get(0xf8b8);

            public IconUsage WeatherSunny20 => Get(0xf8b9);

            public IconUsage WeatherSunny24 => Get(0xf8ba);

            public IconUsage WeatherSunny48 => Get(0xf8bb);

            public IconUsage WeatherThunderstorm20 => Get(0xf8bc);

            public IconUsage WeatherThunderstorm24 => Get(0xf8bd);

            public IconUsage WeatherThunderstorm48 => Get(0xf8be);

            public IconUsage WebAsset24 => Get(0xf8bf);

            public IconUsage Weekend12 => Get(0xf8c0);

            public IconUsage Weekend24 => Get(0xf8c1);

            public IconUsage Whiteboard20 => Get(0xf8c2);

            public IconUsage Whiteboard24 => Get(0xf8c3);

            public IconUsage Wifi120 => Get(0xf8c4);

            public IconUsage Wifi124 => Get(0xf8c5);

            public IconUsage Wifi220 => Get(0xf8c6);

            public IconUsage Wifi224 => Get(0xf8c7);

            public IconUsage Wifi320 => Get(0xf8c8);

            public IconUsage Wifi324 => Get(0xf8c9);

            public IconUsage Wifi420 => Get(0xf8ca);

            public IconUsage Wifi424 => Get(0xf8cb);

            public IconUsage WifiProtected24 => Get(0xf8cc);

            public IconUsage Window20 => Get(0xf8cd);

            public IconUsage WindowAd20 => Get(0xf8ce);

            public IconUsage WindowDevTools16 => Get(0xf8cf);

            public IconUsage WindowDevTools20 => Get(0xf8d0);

            public IconUsage WindowDevTools24 => Get(0xf8d1);

            public IconUsage WindowInprivate20 => Get(0xf8d2);

            public IconUsage WindowInprivateAccount20 => Get(0xf8d3);

            public IconUsage WindowMultiple20 => Get(0xf8d4);

            public IconUsage WindowNew20 => Get(0xf8d5);

            public IconUsage WindowShield16 => Get(0xf8d6);

            public IconUsage WindowShield20 => Get(0xf8d7);

            public IconUsage WindowShield24 => Get(0xf8d8);

            public IconUsage Wrench24 => Get(0xf8d9);

            public IconUsage XboxConsole20 => Get(0xf8da);

            public IconUsage XboxConsole24 => Get(0xf8db);

            public IconUsage ZoomIn20 => Get(0xf8dc);

            public IconUsage ZoomIn24 => Get(0xf8dd);

            public IconUsage ZoomOut20 => Get(0xf8de);

            public IconUsage ZoomOut24 => Get(0xf8df);

            public IconUsage MailOutbox20 => Get(0xf8e0);

            public IconUsage CalendarCheckmark24 => Get(0xf8e1);

            public IconUsage AddSquare24 => Get(0xf8e2);

            public IconUsage AppsList20 => Get(0xf8e3);

            public IconUsage Archive16 => Get(0xf8e4);

            public IconUsage ArrowAutofitHeight24 => Get(0xf8e5);

            public IconUsage ArrowAutofitWidth24 => Get(0xf8e6);

            public IconUsage ArrowCounterclockwise28 => Get(0xf8e7);

            public IconUsage ArrowDown12 => Get(0xf8e8);

            public IconUsage ArrowDownLeft16 => Get(0xf8e9);

            public IconUsage ArrowExportRtl20 => Get(0xf8ea);

            public IconUsage ArrowFitHeight24 => Get(0xf8eb);

            public IconUsage ArrowFitWidth24 => Get(0xf8ec);

            public IconUsage ArrowHookDownLeft16 => Get(0xf8ed);

            public IconUsage ArrowHookDownLeft20 => Get(0xf8ee);

            public IconUsage ArrowHookDownLeft24 => Get(0xf8ef);

            public IconUsage ArrowHookDownLeft28 => Get(0xf8f0);

            public IconUsage ArrowHookDownRight16 => Get(0xf8f1);

            public IconUsage ArrowHookDownRight20 => Get(0xf8f2);

            public IconUsage ArrowHookDownRight24 => Get(0xf8f3);

            public IconUsage ArrowHookDownRight28 => Get(0xf8f4);

            public IconUsage ArrowHookUpLeft16 => Get(0xf8f5);

            public IconUsage ArrowHookUpLeft20 => Get(0xf8f6);

            public IconUsage ArrowHookUpLeft24 => Get(0xf8f7);

            public IconUsage ArrowHookUpLeft28 => Get(0xf8f8);

            public IconUsage ArrowHookUpRight16 => Get(0xf8f9);

            public IconUsage ArrowHookUpRight20 => Get(0xf8fa);

            public IconUsage ArrowHookUpRight24 => Get(0xf8fb);

            public IconUsage ArrowHookUpRight28 => Get(0xf8fc);

            public IconUsage ArrowMove20 => Get(0xf8fd);

            public IconUsage ArrowRedo32 => Get(0xf8fe);

            public IconUsage ArrowRedo48 => Get(0xf8ff);

            public IconUsage ArrowRotateLeft24 => Get(0xf900);

            public IconUsage ArrowRotateRight20 => Get(0xf901);

            public IconUsage ArrowRotateRight24 => Get(0xf902);

            public IconUsage ArrowUpRight16 => Get(0xf903);

            public IconUsage AttachArrowRight20 => Get(0xf904);

            public IconUsage AttachArrowRight24 => Get(0xf905);

            public IconUsage AttachText24 => Get(0xf906);

            public IconUsage AutofitContent24 => Get(0xf907);

            public IconUsage Backpack12 => Get(0xf908);

            public IconUsage Backpack16 => Get(0xf909);

            public IconUsage Backpack20 => Get(0xf90a);

            public IconUsage Backpack24 => Get(0xf90b);

            public IconUsage Backpack28 => Get(0xf90c);

            public IconUsage Backpack48 => Get(0xf90d);

            public IconUsage Balloon16 => Get(0xf90e);

            public IconUsage Bed16 => Get(0xf90f);

            public IconUsage Bluetooth28 => Get(0xf910);

            public IconUsage Blur16 => Get(0xf911);

            public IconUsage Blur20 => Get(0xf912);

            public IconUsage Blur24 => Get(0xf913);

            public IconUsage Blur28 => Get(0xf914);

            public IconUsage Book20 => Get(0xf915);

            public IconUsage Book24 => Get(0xf916);

            public IconUsage BookAdd20 => Get(0xf917);

            public IconUsage BookAdd24 => Get(0xf918);

            public IconUsage BookClock24 => Get(0xf919);

            public IconUsage BookCoins24 => Get(0xf91a);

            public IconUsage BookCompass24 => Get(0xf91b);

            public IconUsage BookDatabase24 => Get(0xf91c);

            public IconUsage BookExclamationMark24 => Get(0xf91d);

            public IconUsage BookInformation24 => Get(0xf91e);

            public IconUsage BookLetter24 => Get(0xf91f);

            public IconUsage BookOpen20 => Get(0xf920);

            public IconUsage BookOpen24 => Get(0xf921);

            public IconUsage BookOpenGlobe24 => Get(0xf922);

            public IconUsage BookPulse24 => Get(0xf923);

            public IconUsage BookQuestionMark24 => Get(0xf924);

            public IconUsage BookSearch24 => Get(0xf925);

            public IconUsage BookStar24 => Get(0xf926);

            public IconUsage BookTheta24 => Get(0xf927);

            public IconUsage BorderAll24 => Get(0xf928);

            public IconUsage BorderBottom24 => Get(0xf929);

            public IconUsage BorderBottomDouble24 => Get(0xf92a);

            public IconUsage BorderBottomThick24 => Get(0xf92b);

            public IconUsage BorderLeft24 => Get(0xf92c);

            public IconUsage BorderNone24 => Get(0xf92d);

            public IconUsage BorderOutside24 => Get(0xf92e);

            public IconUsage BorderOutsideThick24 => Get(0xf92f);

            public IconUsage BorderRight24 => Get(0xf930);

            public IconUsage BorderTop24 => Get(0xf931);

            public IconUsage BorderTopBottom24 => Get(0xf932);

            public IconUsage BorderTopBottomDouble24 => Get(0xf933);

            public IconUsage BorderTopBottomThick24 => Get(0xf934);

            public IconUsage Briefcase12 => Get(0xf935);

            public IconUsage Briefcase32 => Get(0xf936);

            public IconUsage BriefcaseAdd24 => Get(0xf937);

            public IconUsage BriefcaseAdd32 => Get(0xf938);

            public IconUsage Bug16 => Get(0xf939);

            public IconUsage Bug20 => Get(0xf93a);

            public IconUsage Bug24 => Get(0xf93b);

            public IconUsage BuildingBank16 => Get(0xf93c);

            public IconUsage BuildingBank20 => Get(0xf93d);

            public IconUsage BuildingBank24 => Get(0xf93e);

            public IconUsage BuildingGovernment24 => Get(0xf93f);

            public IconUsage BuildingGovernment32 => Get(0xf940);

            public IconUsage BuildingMultiple24 => Get(0xf941);

            public IconUsage BuildingShop16 => Get(0xf942);

            public IconUsage BuildingShop20 => Get(0xf943);

            public IconUsage BuildingShop24 => Get(0xf944);

            public IconUsage BuildingSkyscraper16 => Get(0xf945);

            public IconUsage BuildingSkyscraper20 => Get(0xf946);

            public IconUsage BuildingSkyscraper24 => Get(0xf947);

            public IconUsage CalendarCancel16 => Get(0xf948);

            public IconUsage CalendarClock16 => Get(0xf949);

            public IconUsage CalendarMention20 => Get(0xf94a);

            public IconUsage CalendarPerson24 => Get(0xf94b);

            public IconUsage CalendarQuestionMark16 => Get(0xf94c);

            public IconUsage CalendarQuestionMark20 => Get(0xf94d);

            public IconUsage CalendarQuestionMark24 => Get(0xf94e);

            public IconUsage CallBlocked16 => Get(0xf94f);

            public IconUsage CallBlocked20 => Get(0xf950);

            public IconUsage CallBlocked24 => Get(0xf951);

            public IconUsage CallBlocked28 => Get(0xf952);

            public IconUsage CallBlocked48 => Get(0xf953);

            public IconUsage CallForward16 => Get(0xf954);

            public IconUsage CallForward20 => Get(0xf955);

            public IconUsage CallForward28 => Get(0xf956);

            public IconUsage CallForward48 => Get(0xf957);

            public IconUsage CallInbound20 => Get(0xf958);

            public IconUsage CallInbound28 => Get(0xf959);

            public IconUsage CallInbound48 => Get(0xf95a);

            public IconUsage CallMissed28 => Get(0xf95b);

            public IconUsage CallMissed48 => Get(0xf95c);

            public IconUsage CallOutbound20 => Get(0xf95d);

            public IconUsage CallOutbound28 => Get(0xf95e);

            public IconUsage CallOutbound48 => Get(0xf95f);

            public IconUsage CallPark16 => Get(0xf960);

            public IconUsage CallPark20 => Get(0xf961);

            public IconUsage CallPark28 => Get(0xf962);

            public IconUsage CallPark48 => Get(0xf963);

            public IconUsage CameraEdit20 => Get(0xf964);

            public IconUsage CaretUp12 => Get(0xf965);

            public IconUsage CaretUp16 => Get(0xf966);

            public IconUsage CaretUp20 => Get(0xf967);

            public IconUsage CaretUp24 => Get(0xf968);

            public IconUsage Cart16 => Get(0xf969);

            public IconUsage Cart20 => Get(0xf96a);

            public IconUsage CenterHorizontally24 => Get(0xf96b);

            public IconUsage CenterVertically24 => Get(0xf96c);

            public IconUsage Channel28 => Get(0xf96d);

            public IconUsage Channel48 => Get(0xf96e);

            public IconUsage ChannelAdd16 => Get(0xf96f);

            public IconUsage ChannelAdd20 => Get(0xf970);

            public IconUsage ChannelAdd24 => Get(0xf971);

            public IconUsage ChannelAdd28 => Get(0xf972);

            public IconUsage ChannelAdd48 => Get(0xf973);

            public IconUsage ChannelAlert16 => Get(0xf974);

            public IconUsage ChannelAlert20 => Get(0xf975);

            public IconUsage ChannelAlert24 => Get(0xf976);

            public IconUsage ChannelAlert28 => Get(0xf977);

            public IconUsage ChannelAlert48 => Get(0xf978);

            public IconUsage ChannelArrowLeft16 => Get(0xf979);

            public IconUsage ChannelArrowLeft20 => Get(0xf97a);

            public IconUsage ChannelArrowLeft24 => Get(0xf97b);

            public IconUsage ChannelArrowLeft28 => Get(0xf97c);

            public IconUsage ChannelArrowLeft48 => Get(0xf97d);

            public IconUsage ChannelDismiss16 => Get(0xf97e);

            public IconUsage ChannelDismiss20 => Get(0xf97f);

            public IconUsage ChannelDismiss24 => Get(0xf980);

            public IconUsage ChannelDismiss28 => Get(0xf981);

            public IconUsage ChannelDismiss48 => Get(0xf982);

            public IconUsage Chat16 => Get(0xf983);

            public IconUsage Checkmark16 => Get(0xf984);

            public IconUsage ChevronRightCircle24 => Get(0xf985);

            public IconUsage ClipboardPaste16 => Get(0xf986);

            public IconUsage ClockDismiss24 => Get(0xf987);

            public IconUsage ClosedCaption16 => Get(0xf988);

            public IconUsage ClosedCaption20 => Get(0xf989);

            public IconUsage ClosedCaption28 => Get(0xf98a);

            public IconUsage ClosedCaption48 => Get(0xf98b);

            public IconUsage CloudDownload32 => Get(0xf98c);

            public IconUsage CloudDownload48 => Get(0xf98d);

            public IconUsage ColorLine16 => Get(0xf98e);

            public IconUsage Comment12 => Get(0xf98f);

            public IconUsage Comment28 => Get(0xf990);

            public IconUsage Comment48 => Get(0xf991);

            public IconUsage CommentAdd12 => Get(0xf992);

            public IconUsage CommentAdd16 => Get(0xf993);

            public IconUsage CommentAdd20 => Get(0xf994);

            public IconUsage CommentAdd28 => Get(0xf995);

            public IconUsage CommentAdd48 => Get(0xf996);

            public IconUsage CommentArrowLeft12 => Get(0xf997);

            public IconUsage CommentArrowLeft16 => Get(0xf998);

            public IconUsage CommentArrowLeft20 => Get(0xf999);

            public IconUsage CommentArrowLeft28 => Get(0xf99a);

            public IconUsage CommentArrowLeft48 => Get(0xf99b);

            public IconUsage CommentArrowRight12 => Get(0xf99c);

            public IconUsage CommentArrowRight16 => Get(0xf99d);

            public IconUsage CommentArrowRight20 => Get(0xf99e);

            public IconUsage CommentArrowRight28 => Get(0xf99f);

            public IconUsage CommentArrowRight48 => Get(0xf9a0);

            public IconUsage CommentCheckmark12 => Get(0xf9a1);

            public IconUsage CommentCheckmark16 => Get(0xf9a2);

            public IconUsage CommentCheckmark20 => Get(0xf9a3);

            public IconUsage CommentCheckmark24 => Get(0xf9a4);

            public IconUsage CommentCheckmark28 => Get(0xf9a5);

            public IconUsage CommentCheckmark48 => Get(0xf9a6);

            public IconUsage CommentEdit20 => Get(0xf9a7);

            public IconUsage CommentEdit24 => Get(0xf9a8);

            public IconUsage CommentOff16 => Get(0xf9a9);

            public IconUsage CommentOff20 => Get(0xf9aa);

            public IconUsage CommentOff24 => Get(0xf9ab);

            public IconUsage CommentOff28 => Get(0xf9ac);

            public IconUsage CommentOff48 => Get(0xf9ad);

            public IconUsage ContactCardGroup16 => Get(0xf9ae);

            public IconUsage ContactCardGroup20 => Get(0xf9af);

            public IconUsage ContactCardGroup28 => Get(0xf9b0);

            public IconUsage ContactCardGroup48 => Get(0xf9b1);

            public IconUsage ConvertRange24 => Get(0xf9b2);

            public IconUsage CopyAdd24 => Get(0xf9b3);

            public IconUsage CopySelect20 => Get(0xf9b4);

            public IconUsage Couch12 => Get(0xf9b5);

            public IconUsage Couch24 => Get(0xf9b6);

            public IconUsage Crop20 => Get(0xf9b7);

            public IconUsage CurrencyDollarRupee16 => Get(0xf9b8);

            public IconUsage CurrencyDollarRupee20 => Get(0xf9b9);

            public IconUsage CurrencyDollarRupee24 => Get(0xf9ba);

            public IconUsage Cursor20 => Get(0xf9bb);

            public IconUsage Cursor24 => Get(0xf9bc);

            public IconUsage CursorHover16 => Get(0xf9bd);

            public IconUsage CursorHover20 => Get(0xf9be);

            public IconUsage CursorHover24 => Get(0xf9bf);

            public IconUsage CursorHover28 => Get(0xf9c0);

            public IconUsage CursorHover32 => Get(0xf9c1);

            public IconUsage CursorHover48 => Get(0xf9c2);

            public IconUsage CursorHoverOff16 => Get(0xf9c3);

            public IconUsage CursorHoverOff20 => Get(0xf9c4);

            public IconUsage CursorHoverOff24 => Get(0xf9c5);

            public IconUsage CursorHoverOff28 => Get(0xf9c6);

            public IconUsage CursorHoverOff48 => Get(0xf9c7);

            public IconUsage DataBarVerticalAdd24 => Get(0xf9c8);

            public IconUsage DataUsage20 => Get(0xf9c9);

            public IconUsage DecimalArrowLeft24 => Get(0xf9ca);

            public IconUsage DecimalArrowRight24 => Get(0xf9cb);

            public IconUsage Delete16 => Get(0xf9cc);

            public IconUsage Dentist12 => Get(0xf9cd);

            public IconUsage Dentist16 => Get(0xf9ce);

            public IconUsage Dentist20 => Get(0xf9cf);

            public IconUsage Dentist28 => Get(0xf9d0);

            public IconUsage Dentist48 => Get(0xf9d1);

            public IconUsage DismissCircle28 => Get(0xf9d2);

            public IconUsage DockLeft28 => Get(0xf9d3);

            public IconUsage DockLeft48 => Get(0xf9d4);

            public IconUsage DockRight16 => Get(0xf9d5);

            public IconUsage DockRight20 => Get(0xf9d6);

            public IconUsage DockRight24 => Get(0xf9d7);

            public IconUsage DockRight28 => Get(0xf9d8);

            public IconUsage DockRight48 => Get(0xf9d9);

            public IconUsage Doctor12 => Get(0xf9da);

            public IconUsage Doctor16 => Get(0xf9db);

            public IconUsage Doctor20 => Get(0xf9dc);

            public IconUsage Doctor28 => Get(0xf9dd);

            public IconUsage Doctor48 => Get(0xf9de);

            public IconUsage Document16 => Get(0xf9df);

            public IconUsage Document48 => Get(0xf9e0);

            public IconUsage DocumentAdd16 => Get(0xf9e1);

            public IconUsage DocumentAdd20 => Get(0xf9e2);

            public IconUsage DocumentAdd24 => Get(0xf9e3);

            public IconUsage DocumentAdd28 => Get(0xf9e4);

            public IconUsage DocumentAdd48 => Get(0xf9e5);

            public IconUsage DocumentArrowLeft16 => Get(0xf9e6);

            public IconUsage DocumentArrowLeft20 => Get(0xf9e7);

            public IconUsage DocumentArrowLeft24 => Get(0xf9e8);

            public IconUsage DocumentArrowLeft28 => Get(0xf9e9);

            public IconUsage DocumentArrowLeft48 => Get(0xf9ea);

            public IconUsage DocumentCatchUp16 => Get(0xf9eb);

            public IconUsage DocumentCatchUp20 => Get(0xf9ec);

            public IconUsage DocumentLandscapeData24 => Get(0xf9ed);

            public IconUsage DocumentLandscapeSplit20 => Get(0xf9ee);

            public IconUsage DocumentLandscapeSplitHint20 => Get(0xf9ef);

            public IconUsage DocumentPageBreak20 => Get(0xf9f0);

            public IconUsage DrinkBeer16 => Get(0xf9f1);

            public IconUsage DrinkBeer20 => Get(0xf9f2);

            public IconUsage DrinkCoffee16 => Get(0xf9f3);

            public IconUsage DrinkMargarita16 => Get(0xf9f4);

            public IconUsage DrinkMargarita20 => Get(0xf9f5);

            public IconUsage DrinkWine16 => Get(0xf9f6);

            public IconUsage DrinkWine20 => Get(0xf9f7);

            public IconUsage DualScreenSpan24 => Get(0xf9f8);

            public IconUsage Edit32 => Get(0xf9f9);

            public IconUsage EditOff16 => Get(0xf9fa);

            public IconUsage EditOff24 => Get(0xf9fb);

            public IconUsage EditSettings24 => Get(0xf9fc);

            public IconUsage EmojiAdd16 => Get(0xf9fd);

            public IconUsage EmojiHand24 => Get(0xf9fe);

            public IconUsage EmojiHand28 => Get(0xf9ff);

            public IconUsage Eraser20 => Get(0xfa00);

            public IconUsage Eraser24 => Get(0xfa01);

            public IconUsage EraserMedium24 => Get(0xfa02);

            public IconUsage EraserSegment24 => Get(0xfa03);

            public IconUsage EraserSmall24 => Get(0xfa04);

            public IconUsage ErrorCircle12 => Get(0xfa05);

            public IconUsage EyeTracking16 => Get(0xfa06);

            public IconUsage EyeTracking20 => Get(0xfa07);

            public IconUsage EyeTracking24 => Get(0xfa08);

            public IconUsage EyeTrackingOff16 => Get(0xfa09);

            public IconUsage EyeTrackingOff20 => Get(0xfa0a);

            public IconUsage EyeTrackingOff24 => Get(0xfa0b);

            public IconUsage FStop16 => Get(0xfa0c);

            public IconUsage FStop20 => Get(0xfa0d);

            public IconUsage FStop24 => Get(0xfa0e);

            public IconUsage FStop28 => Get(0xfa0f);

            public IconUsage Fingerprint48 => Get(0xfa10);

            public IconUsage FixedWidth24 => Get(0xfa11);

            public IconUsage FlipHorizontal24 => Get(0xfa12);

            public IconUsage FlipVertical24 => Get(0xfa13);

            public IconUsage Fluent32 => Get(0xfa14);

            public IconUsage Fluent48 => Get(0xfa15);

            public IconUsage Fluid20 => Get(0xfa16);

            public IconUsage Fluid24 => Get(0xfa17);

            public IconUsage FolderMove16 => Get(0xfa18);

            public IconUsage FoodEgg16 => Get(0xfa19);

            public IconUsage FoodEgg20 => Get(0xfa1a);

            public IconUsage FoodToast16 => Get(0xfa1b);

            public IconUsage FoodToast20 => Get(0xfa1c);

            public IconUsage Gavel24 => Get(0xfa1d);

            public IconUsage Gavel32 => Get(0xfa1e);

            public IconUsage Glasses16 => Get(0xfa1f);

            public IconUsage Glasses20 => Get(0xfa20);

            public IconUsage Glasses28 => Get(0xfa21);

            public IconUsage Glasses48 => Get(0xfa22);

            public IconUsage GlassesOff16 => Get(0xfa23);

            public IconUsage GlassesOff20 => Get(0xfa24);

            public IconUsage GlassesOff28 => Get(0xfa25);

            public IconUsage GlassesOff48 => Get(0xfa26);

            public IconUsage Globe16 => Get(0xfa27);

            public IconUsage HandLeft20 => Get(0xfa28);

            public IconUsage HandRight24 => Get(0xfa29);

            public IconUsage HandRight28 => Get(0xfa2a);

            public IconUsage HatGraduation16 => Get(0xfa2b);

            public IconUsage HatGraduation20 => Get(0xfa2c);

            public IconUsage HatGraduation24 => Get(0xfa2d);

            public IconUsage Hd16 => Get(0xfa2e);

            public IconUsage Hd20 => Get(0xfa2f);

            public IconUsage Hd24 => Get(0xfa30);

            public IconUsage Headset16 => Get(0xfa31);

            public IconUsage Headset20 => Get(0xfa32);

            public IconUsage Headset48 => Get(0xfa33);

            public IconUsage HeartPulse24 => Get(0xfa34);

            public IconUsage HeartPulse32 => Get(0xfa35);

            public IconUsage Home16 => Get(0xfa36);

            public IconUsage Home32 => Get(0xfa37);

            public IconUsage Home48 => Get(0xfa38);

            public IconUsage ImageArrowCounterclockwise24 => Get(0xfa39);

            public IconUsage InfoShield20 => Get(0xfa3a);

            public IconUsage KeyMultiple20 => Get(0xfa3b);

            public IconUsage LineHorizontal5Error20 => Get(0xfa3c);

            public IconUsage LinkSquare12 => Get(0xfa3d);

            public IconUsage LinkSquare16 => Get(0xfa3e);

            public IconUsage Location48 => Get(0xfa3f);

            public IconUsage LocationOff16 => Get(0xfa40);

            public IconUsage LocationOff20 => Get(0xfa41);

            public IconUsage LocationOff24 => Get(0xfa42);

            public IconUsage LocationOff28 => Get(0xfa43);

            public IconUsage LocationOff48 => Get(0xfa44);

            public IconUsage LockMultiple24 => Get(0xfa45);

            public IconUsage Lottery24 => Get(0xfa46);

            public IconUsage MagicWand16 => Get(0xfa47);

            public IconUsage MagicWand20 => Get(0xfa48);

            public IconUsage MagicWand28 => Get(0xfa49);

            public IconUsage MagicWand48 => Get(0xfa4a);

            public IconUsage Mail16 => Get(0xfa4b);

            public IconUsage MailRead16 => Get(0xfa4c);

            public IconUsage MathFormatLinear24 => Get(0xfa4d);

            public IconUsage MathFormatProfessional24 => Get(0xfa4e);

            public IconUsage MathFormula24 => Get(0xfa4f);

            public IconUsage Maximize20 => Get(0xfa50);

            public IconUsage Maximize24 => Get(0xfa51);

            public IconUsage Maximize28 => Get(0xfa52);

            public IconUsage Maximize48 => Get(0xfa53);

            public IconUsage MeetNow16 => Get(0xfa54);

            public IconUsage MicOff20 => Get(0xfa55);

            public IconUsage MicOff48 => Get(0xfa56);

            public IconUsage MicProhibited24 => Get(0xfa57);

            public IconUsage Minimize12 => Get(0xfa58);

            public IconUsage Minimize16 => Get(0xfa59);

            public IconUsage Minimize20 => Get(0xfa5a);

            public IconUsage Minimize24 => Get(0xfa5b);

            public IconUsage Minimize28 => Get(0xfa5c);

            public IconUsage Minimize48 => Get(0xfa5d);

            public IconUsage MoreCircle20 => Get(0xfa5e);

            public IconUsage MoviesAndTv16 => Get(0xfa5f);

            public IconUsage MoviesAndTv20 => Get(0xfa60);

            public IconUsage MusicNote20 => Get(0xfa61);

            public IconUsage MusicNote24 => Get(0xfa62);

            public IconUsage MusicNotes16 => Get(0xfa63);

            public IconUsage MusicNotes24 => Get(0xfa64);

            public IconUsage NavigationUnread24 => Get(0xfa65);

            public IconUsage NumberSymbolDismiss24 => Get(0xfa66);

            public IconUsage Open28 => Get(0xfa67);

            public IconUsage Open48 => Get(0xfa68);

            public IconUsage OpenFolder16 => Get(0xfa69);

            public IconUsage OpenFolder20 => Get(0xfa6a);

            public IconUsage OpenFolder28 => Get(0xfa6b);

            public IconUsage OpenFolder48 => Get(0xfa6c);

            public IconUsage OpenOff16 => Get(0xfa6d);

            public IconUsage OpenOff20 => Get(0xfa6e);

            public IconUsage OpenOff24 => Get(0xfa6f);

            public IconUsage OpenOff28 => Get(0xfa70);

            public IconUsage OpenOff48 => Get(0xfa71);

            public IconUsage PaintBrushArrowDown24 => Get(0xfa72);

            public IconUsage PaintBrushArrowUp24 => Get(0xfa73);

            public IconUsage Pause12 => Get(0xfa74);

            public IconUsage Payment16 => Get(0xfa75);

            public IconUsage Payment28 => Get(0xfa76);

            public IconUsage PeopleProhibited16 => Get(0xfa77);

            public IconUsage PeopleSwap16 => Get(0xfa78);

            public IconUsage PeopleSwap20 => Get(0xfa79);

            public IconUsage PeopleSwap24 => Get(0xfa7a);

            public IconUsage PeopleSwap28 => Get(0xfa7b);

            public IconUsage PeopleTeamAdd20 => Get(0xfa7c);

            public IconUsage PeopleTeamAdd24 => Get(0xfa7d);

            public IconUsage PeopleTeamDismiss24 => Get(0xfa7e);

            public IconUsage PersonAvailable20 => Get(0xfa7f);

            public IconUsage PersonClock16 => Get(0xfa80);

            public IconUsage PersonClock20 => Get(0xfa81);

            public IconUsage PersonClock24 => Get(0xfa82);

            public IconUsage PersonDelete20 => Get(0xfa83);

            public IconUsage PersonMail16 => Get(0xfa84);

            public IconUsage PersonMail20 => Get(0xfa85);

            public IconUsage PersonMail24 => Get(0xfa86);

            public IconUsage PersonMail28 => Get(0xfa87);

            public IconUsage PersonMail48 => Get(0xfa88);

            public IconUsage PersonProhibited24 => Get(0xfa89);

            public IconUsage Phone16 => Get(0xfa8a);

            public IconUsage Poll20 => Get(0xfa8b);

            public IconUsage Pulse24 => Get(0xfa8c);

            public IconUsage QrCode20 => Get(0xfa8d);

            public IconUsage RealEstate24 => Get(0xfa8e);

            public IconUsage Ribbon24 => Get(0xfa8f);

            public IconUsage RibbonStar20 => Get(0xfa90);

            public IconUsage RibbonStar24 => Get(0xfa91);

            public IconUsage Run16 => Get(0xfa92);

            public IconUsage Run20 => Get(0xfa93);

            public IconUsage Scales24 => Get(0xfa94);

            public IconUsage Scales32 => Get(0xfa95);

            public IconUsage SearchShield20 => Get(0xfa96);

            public IconUsage ShareStop16 => Get(0xfa97);

            public IconUsage ShareStop20 => Get(0xfa98);

            public IconUsage ShareStop48 => Get(0xfa99);

            public IconUsage ShieldDismissShield20 => Get(0xfa9a);

            public IconUsage ShiftsDay20 => Get(0xfa9b);

            public IconUsage ShiftsDay24 => Get(0xfa9c);

            public IconUsage SidebarSearchLtr20 => Get(0xfa9d);

            public IconUsage SidebarSearchRtl20 => Get(0xfa9e);

            public IconUsage SignOut20 => Get(0xfa9f);

            public IconUsage SlideMultipleArrowRight24 => Get(0xfaa0);

            public IconUsage SlideSearch24 => Get(0xfaa1);

            public IconUsage SlideSearch28 => Get(0xfaa2);

            public IconUsage SlideSize24 => Get(0xfaa3);

            public IconUsage SlideText16 => Get(0xfaa4);

            public IconUsage SlideText20 => Get(0xfaa5);

            public IconUsage SlideText28 => Get(0xfaa6);

            public IconUsage SlideText48 => Get(0xfaa7);

            public IconUsage Speaker016 => Get(0xfaa8);

            public IconUsage Speaker020 => Get(0xfaa9);

            public IconUsage Speaker028 => Get(0xfaaa);

            public IconUsage Speaker048 => Get(0xfaab);

            public IconUsage Speaker116 => Get(0xfaac);

            public IconUsage Speaker120 => Get(0xfaad);

            public IconUsage Speaker128 => Get(0xfaae);

            public IconUsage Speaker148 => Get(0xfaaf);

            public IconUsage Speaker48 => Get(0xfab0);

            public IconUsage SpeakerBluetooth28 => Get(0xfab1);

            public IconUsage SpeakerNone16 => Get(0xfab2);

            public IconUsage SpeakerNone48 => Get(0xfab3);

            public IconUsage SpeakerOff16 => Get(0xfab4);

            public IconUsage SpeakerOff20 => Get(0xfab5);

            public IconUsage SpeakerOff48 => Get(0xfab6);

            public IconUsage SpeakerUsb24 => Get(0xfab7);

            public IconUsage SpeakerUsb28 => Get(0xfab8);

            public IconUsage Sport16 => Get(0xfab9);

            public IconUsage Sport20 => Get(0xfaba);

            public IconUsage Sport24 => Get(0xfabb);

            public IconUsage SportAmericanFootball24 => Get(0xfabc);

            public IconUsage SportBaseball24 => Get(0xfabd);

            public IconUsage SportBasketball24 => Get(0xfabe);

            public IconUsage SportHockey24 => Get(0xfabf);

            public IconUsage StarEdit24 => Get(0xfac0);

            public IconUsage TabDesktopArrowLeft20 => Get(0xfac1);

            public IconUsage TabProhibited24 => Get(0xfac2);

            public IconUsage Table16 => Get(0xfac3);

            public IconUsage Table28 => Get(0xfac4);

            public IconUsage Table48 => Get(0xfac5);

            public IconUsage TableSimple16 => Get(0xfac6);

            public IconUsage TableSimple20 => Get(0xfac7);

            public IconUsage TableSimple24 => Get(0xfac8);

            public IconUsage TableSimple28 => Get(0xfac9);

            public IconUsage TableSimple48 => Get(0xfaca);

            public IconUsage Tag16 => Get(0xfacb);

            public IconUsage TasksApp20 => Get(0xfacc);

            public IconUsage Tent12 => Get(0xfacd);

            public IconUsage Tent16 => Get(0xface);

            public IconUsage Tent20 => Get(0xfacf);

            public IconUsage Tent28 => Get(0xfad0);

            public IconUsage Tent48 => Get(0xfad1);

            public IconUsage TextBold16 => Get(0xfad2);

            public IconUsage TextColor16 => Get(0xfad3);

            public IconUsage TextColumnOneNarrow20 => Get(0xfad4);

            public IconUsage TextColumnOneNarrow24 => Get(0xfad5);

            public IconUsage TextColumnOneWide20 => Get(0xfad6);

            public IconUsage TextColumnOneWide24 => Get(0xfad7);

            public IconUsage TextContinuous24 => Get(0xfad8);

            public IconUsage TextIndentDecrease20 => Get(0xfad9);

            public IconUsage TextIndentIncrease20 => Get(0xfada);

            public IconUsage TextItalic16 => Get(0xfadb);

            public IconUsage TextStrikethrough16 => Get(0xfadc);

            public IconUsage TextUnderline16 => Get(0xfadd);

            public IconUsage TextWrapBehind20 => Get(0xfade);

            public IconUsage TextWrapBehind24 => Get(0xfadf);

            public IconUsage TextWrapFront20 => Get(0xfae0);

            public IconUsage TextWrapFront24 => Get(0xfae1);

            public IconUsage TextWrapLine20 => Get(0xfae2);

            public IconUsage TextWrapLine24 => Get(0xfae3);

            public IconUsage TextWrapSquare20 => Get(0xfae4);

            public IconUsage TextWrapSquare24 => Get(0xfae5);

            public IconUsage TextWrapThrough20 => Get(0xfae6);

            public IconUsage TextWrapThrough24 => Get(0xfae7);

            public IconUsage TextWrapTight20 => Get(0xfae8);

            public IconUsage TextWrapTight24 => Get(0xfae9);

            public IconUsage TextWrapTopBottom20 => Get(0xfaea);

            public IconUsage TextWrapTopBottom24 => Get(0xfaeb);

            public IconUsage TicketDiagonal16 => Get(0xfaec);

            public IconUsage TicketDiagonal20 => Get(0xfaed);

            public IconUsage TicketDiagonal24 => Get(0xfaee);

            public IconUsage TicketDiagonal28 => Get(0xfaef);

            public IconUsage Timer16 => Get(0xfaf0);

            public IconUsage Timer20 => Get(0xfaf1);

            public IconUsage ToggleLeft16 => Get(0xfaf2);

            public IconUsage ToggleLeft20 => Get(0xfaf3);

            public IconUsage ToggleLeft24 => Get(0xfaf4);

            public IconUsage ToggleLeft28 => Get(0xfaf5);

            public IconUsage ToggleLeft48 => Get(0xfaf6);

            public IconUsage ToggleRight28 => Get(0xfaf7);

            public IconUsage ToggleRight48 => Get(0xfaf8);

            public IconUsage Tv16 => Get(0xfaf9);

            public IconUsage Tv20 => Get(0xfafa);

            public IconUsage Tv24 => Get(0xfafb);

            public IconUsage Tv28 => Get(0xfafc);

            public IconUsage Tv48 => Get(0xfafd);

            public IconUsage VehicleBicycle16 => Get(0xfafe);

            public IconUsage VehicleBicycle20 => Get(0xfaff);

            public IconUsage VehicleBus16 => Get(0xfb00);

            public IconUsage VehicleBus20 => Get(0xfb01);

            public IconUsage VehicleCar28 => Get(0xfb02);

            public IconUsage VehicleCar48 => Get(0xfb03);

            public IconUsage VehicleShip16 => Get(0xfb04);

            public IconUsage VehicleShip20 => Get(0xfb05);

            public IconUsage VehicleShip24 => Get(0xfb06);

            public IconUsage VehicleSubway16 => Get(0xfb07);

            public IconUsage VehicleSubway20 => Get(0xfb08);

            public IconUsage VehicleSubway24 => Get(0xfb09);

            public IconUsage VehicleTruck16 => Get(0xfb0a);

            public IconUsage VehicleTruck20 => Get(0xfb0b);

            public IconUsage VideoClip20 => Get(0xfb0c);

            public IconUsage Vote20 => Get(0xfb0d);

            public IconUsage Vote24 => Get(0xfb0e);

            public IconUsage WeatherDrizzle20 => Get(0xfb0f);

            public IconUsage WeatherDrizzle24 => Get(0xfb10);

            public IconUsage WeatherDrizzle48 => Get(0xfb11);

            public IconUsage WeatherHaze20 => Get(0xfb12);

            public IconUsage WeatherHaze24 => Get(0xfb13);

            public IconUsage WeatherHaze48 => Get(0xfb14);

            public IconUsage WeatherMoon16 => Get(0xfb15);

            public IconUsage WeatherMoon28 => Get(0xfb16);

            public IconUsage WeatherMoonOff16 => Get(0xfb17);

            public IconUsage WeatherMoonOff20 => Get(0xfb18);

            public IconUsage WeatherMoonOff24 => Get(0xfb19);

            public IconUsage WeatherMoonOff28 => Get(0xfb1a);

            public IconUsage WeatherMoonOff48 => Get(0xfb1b);

            public IconUsage WeatherSunnyHigh20 => Get(0xfb1c);

            public IconUsage WeatherSunnyHigh24 => Get(0xfb1d);

            public IconUsage WeatherSunnyHigh48 => Get(0xfb1e);

            public IconUsage WeatherSunnyLow20 => Get(0xfb1f);

            public IconUsage WeatherSunnyLow24 => Get(0xfb20);

            public IconUsage WeatherSunnyLow48 => Get(0xfb21);

            public IconUsage WindowHorizontal20 => Get(0xfb22);

            public IconUsage WindowNew16 => Get(0xfb23);

            public IconUsage WindowNew24 => Get(0xfb24);

            public IconUsage WindowVertical20 => Get(0xfb25);

            public IconUsage Wrench16 => Get(0xfb26);

            public IconUsage Wrench20 => Get(0xfb27);

            public IconUsage VideoBackgroundEffect20 => Get(0xfb28);

            public IconUsage Alert16 => Get(0xfb29);

            public IconUsage ApprovalsApp16 => Get(0xfb2a);

            public IconUsage ApprovalsApp20 => Get(0xfb2b);

            public IconUsage ArrowBounce16 => Get(0xfb2c);

            public IconUsage ArrowBounce24 => Get(0xfb2d);

            public IconUsage ArrowEnter20 => Get(0xfb2e);

            public IconUsage ArrowEnterUp20 => Get(0xfb2f);

            public IconUsage ArrowEnterUp24 => Get(0xfb30);

            public IconUsage BookmarkMultiple20 => Get(0xfb31);

            public IconUsage Briefcase28 => Get(0xfb32);

            public IconUsage Briefcase48 => Get(0xfb33);

            public IconUsage Building20 => Get(0xfb34);

            public IconUsage Chat48 => Get(0xfb35);

            public IconUsage Checklist48 => Get(0xfb36);

            public IconUsage Contacts20 => Get(0xfb37);

            public IconUsage DesktopArrowRight16 => Get(0xfb38);

            public IconUsage DesktopArrowRight20 => Get(0xfb39);

            public IconUsage DesktopArrowRight24 => Get(0xfb3a);

            public IconUsage DesktopSpeaker20 => Get(0xfb3b);

            public IconUsage DesktopSpeaker24 => Get(0xfb3c);

            public IconUsage DesktopSpeakerOff20 => Get(0xfb3d);

            public IconUsage DesktopSpeakerOff24 => Get(0xfb3e);

            public IconUsage EmojiAdd20 => Get(0xfb3f);

            public IconUsage FoodCake20 => Get(0xfb40);

            public IconUsage GridKanban20 => Get(0xfb41);

            public IconUsage HandRight20 => Get(0xfb42);

            public IconUsage HandRightOff20 => Get(0xfb43);

            public IconUsage LearningApp20 => Get(0xfb44);

            public IconUsage LearningApp24 => Get(0xfb45);

            public IconUsage LiveOff20 => Get(0xfb46);

            public IconUsage LiveOff24 => Get(0xfb47);

            public IconUsage MicProhibited20 => Get(0xfb48);

            public IconUsage NotebookSection20 => Get(0xfb49);

            public IconUsage PeopleAudience20 => Get(0xfb4a);

            public IconUsage PeopleCall16 => Get(0xfb4b);

            public IconUsage PeopleCall20 => Get(0xfb4c);

            public IconUsage PersonCall16 => Get(0xfb4d);

            public IconUsage PersonCall20 => Get(0xfb4e);

            public IconUsage PhoneDesktopAdd20 => Get(0xfb4f);

            public IconUsage Presenter20 => Get(0xfb50);

            public IconUsage PresenterOff20 => Get(0xfb51);

            public IconUsage RectangleLandscape20 => Get(0xfb52);

            public IconUsage Ribbon20 => Get(0xfb53);

            public IconUsage SaveSync20 => Get(0xfb54);

            public IconUsage Shifts20 => Get(0xfb55);

            public IconUsage ShiftsCheckmark20 => Get(0xfb56);

            public IconUsage ShiftsCheckmark24 => Get(0xfb57);

            public IconUsage SlideMultiple24 => Get(0xfb58);

            public IconUsage StarLineHorizontal320 => Get(0xfb59);

            public IconUsage StarLineHorizontal324 => Get(0xfb5a);

            public IconUsage TableAdd20 => Get(0xfb5b);

            public IconUsage TableDismiss20 => Get(0xfb5c);

            public IconUsage TableDismiss24 => Get(0xfb5d);

            public IconUsage TapDouble20 => Get(0xfb5e);

            public IconUsage TapSingle20 => Get(0xfb5f);

            public IconUsage TextBulletListAdd20 => Get(0xfb60);

            public IconUsage TextBulletListSquare20 => Get(0xfb61);

            public IconUsage TextGrammarError20 => Get(0xfb62);

            public IconUsage TextNumberListRtl20 => Get(0xfb63);

            public IconUsage Video36020 => Get(0xfb64);

            public IconUsage Video36024 => Get(0xfb65);

            public IconUsage VideoPerson12 => Get(0xfb66);

            public IconUsage VideoPerson16 => Get(0xfb67);

            public IconUsage VideoPerson20 => Get(0xfb68);

            public IconUsage VideoPerson28 => Get(0xfb69);

            public IconUsage VideoPerson48 => Get(0xfb6a);

            public IconUsage VideoPersonCall16 => Get(0xfb6b);

            public IconUsage VideoPersonCall20 => Get(0xfb6c);

            public IconUsage VideoPersonCall24 => Get(0xfb6d);

            public IconUsage VideoPersonStar20 => Get(0xfb6e);

            public IconUsage VideoProhibited20 => Get(0xfb6f);

            public IconUsage VideoSwitch20 => Get(0xfb70);

            public IconUsage WifiWarning20 => Get(0xfb71);

            public IconUsage Album24 => Get(0xfb72);

            public IconUsage AlbumAdd24 => Get(0xfb73);

            public IconUsage AlertUrgent16 => Get(0xfb74);

            public IconUsage ArrowRight16 => Get(0xfb75);

            public IconUsage ArrowUndo16 => Get(0xfb76);

            public IconUsage ArrowUpLeft16 => Get(0xfb77);

            public IconUsage ArrowUpLeft20 => Get(0xfb78);

            public IconUsage BackpackAdd20 => Get(0xfb79);

            public IconUsage BackpackAdd24 => Get(0xfb7a);

            public IconUsage BackpackAdd28 => Get(0xfb7b);

            public IconUsage BackpackAdd48 => Get(0xfb7c);

            public IconUsage Bot20 => Get(0xfb7d);

            public IconUsage CallConnecting20 => Get(0xfb7e);

            public IconUsage CallExclamation20 => Get(0xfb7f);

            public IconUsage CallTransfer20 => Get(0xfb80);

            public IconUsage CameraOff24 => Get(0xfb81);

            public IconUsage ChatBubblesQuestion20 => Get(0xfb82);

            public IconUsage ChatMail20 => Get(0xfb83);

            public IconUsage ChatOff20 => Get(0xfb84);

            public IconUsage Checkmark48 => Get(0xfb85);

            public IconUsage CloudSync20 => Get(0xfb86);

            public IconUsage ContentView20 => Get(0xfb87);

            public IconUsage CubeRotate20 => Get(0xfb88);

            public IconUsage DataLine20 => Get(0xfb89);

            public IconUsage DeviceMeetingRoom20 => Get(0xfb8a);

            public IconUsage DeviceMeetingRoomRemote20 => Get(0xfb8b);

            public IconUsage DrawShape24 => Get(0xfb8c);

            public IconUsage DrawText24 => Get(0xfb8d);

            public IconUsage FolderArrowUp16 => Get(0xfb8e);

            public IconUsage FolderArrowUp20 => Get(0xfb8f);

            public IconUsage FolderArrowUp24 => Get(0xfb90);

            public IconUsage FolderArrowUp28 => Get(0xfb91);

            public IconUsage FolderUpArrow48 => Get(0xfb92);

            public IconUsage Fps3016 => Get(0xfb93);

            public IconUsage Fps3020 => Get(0xfb94);

            public IconUsage Fps3024 => Get(0xfb95);

            public IconUsage Fps3028 => Get(0xfb96);

            public IconUsage Fps3048 => Get(0xfb97);

            public IconUsage Fps6016 => Get(0xfb98);

            public IconUsage Fps6020 => Get(0xfb99);

            public IconUsage Fps6024 => Get(0xfb9a);

            public IconUsage Fps6028 => Get(0xfb9b);

            public IconUsage Fps6048 => Get(0xfb9c);

            public IconUsage FullScreen24 => Get(0xfb9d);

            public IconUsage HomePerson20 => Get(0xfb9e);

            public IconUsage ImageOff20 => Get(0xfb9f);

            public IconUsage Lasso20 => Get(0xfba0);

            public IconUsage LeafThree16 => Get(0xfba1);

            public IconUsage LeafThree20 => Get(0xfba2);

            public IconUsage LeafThree24 => Get(0xfba3);

            public IconUsage MicSync20 => Get(0xfba4);

            public IconUsage NotebookSubsection20 => Get(0xfba5);

            public IconUsage PersonCircle20 => Get(0xfba6);

            public IconUsage Pulse20 => Get(0xfba7);

            public IconUsage PulseSquare24 => Get(0xfba8);

            public IconUsage Ribbon16 => Get(0xfba9);

            public IconUsage RotateLeft24 => Get(0xfbaa);

            public IconUsage RotateRight20 => Get(0xfbab);

            public IconUsage RotateRight24 => Get(0xfbac);

            public IconUsage ShareCloseTray20 => Get(0xfbad);

            public IconUsage SquareMultiple20 => Get(0xfbae);

            public IconUsage StarEmphasis20 => Get(0xfbaf);

            public IconUsage TvArrowRight20 => Get(0xfbb0);

            public IconUsage VideoPersonStarOff20 => Get(0xfbb1);

            public IconUsage VideoRecording20 => Get(0xfbb2);

            public IconUsage VideoSync20 => Get(0xfbb3);

            public IconUsage BreakoutRoom20 => Get(0xfbb4);

            public IconUsage ContentViewGallery20 => Get(0xfbb5);

            public IconUsage DoorTag24 => Get(0xfbb6);

            public IconUsage Luggage24 => Get(0xfbb7);

            public IconUsage PeopleEdit20 => Get(0xfbb8);

            public IconUsage ChannelShare12 => Get(0xfbb9);

            public IconUsage ChannelShare16 => Get(0xfbba);

            public IconUsage ChannelShare20 => Get(0xfbbb);

            public IconUsage ChannelShare24 => Get(0xfbbc);

            public IconUsage ChannelShare28 => Get(0xfbbd);

            public IconUsage ChannelShare48 => Get(0xfbbe);

            public IconUsage PeopleError16 => Get(0xfbbf);

            public IconUsage PeopleError20 => Get(0xfbc0);

            public IconUsage PeopleError24 => Get(0xfbc1);

            public IconUsage PuzzleCube16 => Get(0xfbc2);

            public IconUsage PuzzleCube20 => Get(0xfbc3);

            public IconUsage PuzzleCube24 => Get(0xfbc4);

            public IconUsage PuzzleCube28 => Get(0xfbc5);

            public IconUsage PuzzleCube48 => Get(0xfbc6);

            public IconUsage ArrowCircleDownRight16 => Get(0xfbc7);

            public IconUsage ArrowCircleDownRight24 => Get(0xfbc8);

            public IconUsage ArrowCircleRight24 => Get(0xfbc9);

            public IconUsage ArrowCircleUp16 => Get(0xfbca);

            public IconUsage ArrowCircleUp20 => Get(0xfbcb);

            public IconUsage ArrowCircleUp24 => Get(0xfbcc);

            public IconUsage ArrowCircleUpLeft24 => Get(0xfbcd);

            public IconUsage ArrowEnterLeft20 => Get(0xfbce);

            public IconUsage ArrowEnterLeft24 => Get(0xfbcf);

            public IconUsage ArrowExportLtr20 => Get(0xfbd0);

            public IconUsage ArrowExportLtr24 => Get(0xfbd1);

            public IconUsage ArrowSquareDown24 => Get(0xfbd2);

            public IconUsage ArrowUndo32 => Get(0xfbd3);

            public IconUsage ArrowUndo48 => Get(0xfbd4);

            public IconUsage AttachArrowLeft20 => Get(0xfbd5);

            public IconUsage AttachArrowLeft24 => Get(0xfbd6);

            public IconUsage AutoFitHeight24 => Get(0xfbd7);

            public IconUsage AutoFitWidth24 => Get(0xfbd8);

            public IconUsage Border24 => Get(0xfbd9);

            public IconUsage BriefcaseMedical24 => Get(0xfbda);

            public IconUsage BriefcaseMedical32 => Get(0xfbdb);

            public IconUsage BuildingFactory24 => Get(0xfbdc);

            public IconUsage CalendarArrowDown24 => Get(0xfbdd);

            public IconUsage Call16 => Get(0xfbde);

            public IconUsage Call20 => Get(0xfbdf);

            public IconUsage Call24 => Get(0xfbe0);

            public IconUsage Call28 => Get(0xfbe1);

            public IconUsage CallMissed20 => Get(0xfbe2);

            public IconUsage CallProhibited16 => Get(0xfbe3);

            public IconUsage CallProhibited20 => Get(0xfbe4);

            public IconUsage CallProhibited24 => Get(0xfbe5);

            public IconUsage CallProhibited28 => Get(0xfbe6);

            public IconUsage CallProhibited48 => Get(0xfbe7);

            public IconUsage CaretDownLeft12 => Get(0xfbe8);

            public IconUsage CaretDownLeft16 => Get(0xfbe9);

            public IconUsage CaretDownLeft20 => Get(0xfbea);

            public IconUsage CaretDownLeft24 => Get(0xfbeb);

            public IconUsage CellularDataCellularOff24 => Get(0xfbec);

            public IconUsage CellularDataCellularUnavailable24 => Get(0xfbed);

            public IconUsage CenterHorizontal24 => Get(0xfbee);

            public IconUsage CenterVertical24 => Get(0xfbef);

            public IconUsage ChevronCircleDown24 => Get(0xfbf0);

            public IconUsage ChevronCircleRight24 => Get(0xfbf1);

            public IconUsage ClipboardImage24 => Get(0xfbf2);

            public IconUsage CommentArrowLeft24 => Get(0xfbf3);

            public IconUsage CommentArrowRight24 => Get(0xfbf4);

            public IconUsage CommentDismiss24 => Get(0xfbf5);

            public IconUsage Component2DoubleTapSwipeDown24 => Get(0xfbf6);

            public IconUsage Component2DoubleTapSwipeUp24 => Get(0xfbf7);

            public IconUsage CopyArrowRight16 => Get(0xfbf8);

            public IconUsage CopyArrowRight24 => Get(0xfbf9);

            public IconUsage CurrencyDollarEuro16 => Get(0xfbfa);

            public IconUsage CurrencyDollarEuro20 => Get(0xfbfb);

            public IconUsage CurrencyDollarEuro24 => Get(0xfbfc);

            public IconUsage DeleteDismiss24 => Get(0xfbfd);

            public IconUsage DeleteDismiss28 => Get(0xfbfe);

            public IconUsage DockPanelLeft16 => Get(0xfbff);

            public IconUsage DockPanelLeft20 => Get(0xfc00);

            public IconUsage DockPanelLeft24 => Get(0xfc01);

            public IconUsage DockPanelLeft28 => Get(0xfc02);

            public IconUsage DockPanelLeft48 => Get(0xfc03);

            public IconUsage DockPanelRight16 => Get(0xfc04);

            public IconUsage DockPanelRight20 => Get(0xfc05);

            public IconUsage DockPanelRight24 => Get(0xfc06);

            public IconUsage DockPanelRight28 => Get(0xfc07);

            public IconUsage DockPanelRight48 => Get(0xfc08);

            public IconUsage DocumentProhibited20 => Get(0xfc09);

            public IconUsage DocumentProhibited24 => Get(0xfc0a);

            public IconUsage DocumentSync24 => Get(0xfc0b);

            public IconUsage DrinkToGo24 => Get(0xfc0c);

            public IconUsage DualScreenHeader24 => Get(0xfc0d);

            public IconUsage EyeTrackingOn16 => Get(0xfc0e);

            public IconUsage EyeTrackingOn20 => Get(0xfc0f);

            public IconUsage EyeTrackingOn24 => Get(0xfc10);

            public IconUsage Fluent24 => Get(0xfc11);

            public IconUsage FolderArrowRight16 => Get(0xfc12);

            public IconUsage FolderArrowRight20 => Get(0xfc13);

            public IconUsage FolderArrowRight24 => Get(0xfc14);

            public IconUsage FolderArrowRight28 => Get(0xfc15);

            public IconUsage FolderArrowRight48 => Get(0xfc16);

            public IconUsage FolderArrowUp48 => Get(0xfc17);

            public IconUsage FolderProhibited20 => Get(0xfc18);

            public IconUsage FolderProhibited24 => Get(0xfc19);

            public IconUsage FolderProhibited28 => Get(0xfc1a);

            public IconUsage FolderProhibited48 => Get(0xfc1b);

            public IconUsage FolderSwap16 => Get(0xfc1c);

            public IconUsage FolderSwap20 => Get(0xfc1d);

            public IconUsage FolderSwap24 => Get(0xfc1e);

            public IconUsage FullScreenMaximize24 => Get(0xfc1f);

            public IconUsage FullScreenMinimize24 => Get(0xfc20);

            public IconUsage ImageMultiple20 => Get(0xfc21);

            public IconUsage ImageMultiple24 => Get(0xfc22);

            public IconUsage ImageMultiple28 => Get(0xfc23);

            public IconUsage LeafOne16 => Get(0xfc24);

            public IconUsage LeafOne20 => Get(0xfc25);

            public IconUsage LeafOne24 => Get(0xfc26);

            public IconUsage LinkDismiss20 => Get(0xfc27);

            public IconUsage LocationDismiss24 => Get(0xfc28);

            public IconUsage LockClosed12 => Get(0xfc29);

            public IconUsage LockClosed16 => Get(0xfc2a);

            public IconUsage LockClosed20 => Get(0xfc2b);

            public IconUsage LockClosed24 => Get(0xfc2c);

            public IconUsage LockOpen20 => Get(0xfc2d);

            public IconUsage LockOpen24 => Get(0xfc2e);

            public IconUsage LockOpen28 => Get(0xfc2f);

            public IconUsage MailInboxAll24 => Get(0xfc30);

            public IconUsage MailInboxArrowRight24 => Get(0xfc31);

            public IconUsage MailInboxArrowUp20 => Get(0xfc32);

            public IconUsage MailInboxArrowUp24 => Get(0xfc33);

            public IconUsage MailOff24 => Get(0xfc34);

            public IconUsage MoreHorizontal16 => Get(0xfc35);

            public IconUsage MoreHorizontal20 => Get(0xfc36);

            public IconUsage MoreHorizontal24 => Get(0xfc37);

            public IconUsage MoreHorizontal28 => Get(0xfc38);

            public IconUsage MoreHorizontal48 => Get(0xfc39);

            public IconUsage MusicNote120 => Get(0xfc3a);

            public IconUsage MusicNote124 => Get(0xfc3b);

            public IconUsage MusicNote216 => Get(0xfc3c);

            public IconUsage MusicNote224 => Get(0xfc3d);

            public IconUsage PeopleTeamDelete24 => Get(0xfc3e);

            public IconUsage PhoneAdd24 => Get(0xfc3f);

            public IconUsage PhoneArrowRight20 => Get(0xfc40);

            public IconUsage PhoneArrowRight24 => Get(0xfc41);

            public IconUsage PhoneDismiss24 => Get(0xfc42);

            public IconUsage PhoneLock24 => Get(0xfc43);

            public IconUsage PhoneSpanIn16 => Get(0xfc44);

            public IconUsage PhoneSpanIn20 => Get(0xfc45);

            public IconUsage PhoneSpanIn24 => Get(0xfc46);

            public IconUsage PhoneSpanIn28 => Get(0xfc47);

            public IconUsage PhoneSpanOut16 => Get(0xfc48);

            public IconUsage PhoneSpanOut20 => Get(0xfc49);

            public IconUsage PhoneSpanOut24 => Get(0xfc4a);

            public IconUsage PhoneSpanOut28 => Get(0xfc4b);

            public IconUsage PositionBackward20 => Get(0xfc4c);

            public IconUsage PositionBackward24 => Get(0xfc4d);

            public IconUsage PositionForward20 => Get(0xfc4e);

            public IconUsage PositionForward24 => Get(0xfc4f);

            public IconUsage PositionToBack20 => Get(0xfc50);

            public IconUsage PositionToBack24 => Get(0xfc51);

            public IconUsage PositionToFront20 => Get(0xfc52);

            public IconUsage PositionToFront24 => Get(0xfc53);

            public IconUsage ResizeLarge16 => Get(0xfc54);

            public IconUsage ResizeLarge20 => Get(0xfc55);

            public IconUsage ResizeLarge24 => Get(0xfc56);

            public IconUsage ResizeSmall16 => Get(0xfc57);

            public IconUsage ResizeSmall20 => Get(0xfc58);

            public IconUsage ResizeSmall24 => Get(0xfc59);

            public IconUsage SaveEdit20 => Get(0xfc5a);

            public IconUsage SaveEdit24 => Get(0xfc5b);

            public IconUsage SearchInfo20 => Get(0xfc5c);

            public IconUsage SearchVisual16 => Get(0xfc5d);

            public IconUsage SearchVisual20 => Get(0xfc5e);

            public IconUsage SearchVisual24 => Get(0xfc5f);

            public IconUsage SelectAllOn24 => Get(0xfc60);

            public IconUsage ShareScreenStart20 => Get(0xfc61);

            public IconUsage ShareScreenStart24 => Get(0xfc62);

            public IconUsage ShareScreenStart28 => Get(0xfc63);

            public IconUsage ShareScreenStop16 => Get(0xfc64);

            public IconUsage ShareScreenStop20 => Get(0xfc65);

            public IconUsage ShareScreenStop24 => Get(0xfc66);

            public IconUsage ShareScreenStop28 => Get(0xfc67);

            public IconUsage ShareScreenStop48 => Get(0xfc68);

            public IconUsage ShieldDismiss16 => Get(0xfc69);

            public IconUsage ShiftsProhibited24 => Get(0xfc6a);

            public IconUsage ShiftsQuestionMark24 => Get(0xfc6b);

            public IconUsage Speaker216 => Get(0xfc6c);

            public IconUsage Speaker220 => Get(0xfc6d);

            public IconUsage Speaker224 => Get(0xfc6e);

            public IconUsage Speaker228 => Get(0xfc6f);

            public IconUsage Speaker248 => Get(0xfc70);

            public IconUsage SpeakerMute16 => Get(0xfc71);

            public IconUsage SpeakerMute20 => Get(0xfc72);

            public IconUsage SpeakerMute24 => Get(0xfc73);

            public IconUsage SpeakerMute28 => Get(0xfc74);

            public IconUsage SpeakerMute48 => Get(0xfc75);

            public IconUsage SportGeneral16 => Get(0xfc76);

            public IconUsage SportGeneral20 => Get(0xfc77);

            public IconUsage SportGeneral24 => Get(0xfc78);

            public IconUsage StarArrowRightEnd24 => Get(0xfc79);

            public IconUsage Subtract12 => Get(0xfc7a);

            public IconUsage Subtract16 => Get(0xfc7b);

            public IconUsage Subtract20 => Get(0xfc7c);

            public IconUsage Subtract24 => Get(0xfc7d);

            public IconUsage Subtract28 => Get(0xfc7e);

            public IconUsage Subtract48 => Get(0xfc7f);

            public IconUsage TabAdd20 => Get(0xfc80);

            public IconUsage TabAdd24 => Get(0xfc81);

            public IconUsage TabArrowLeft24 => Get(0xfc82);

            public IconUsage TabShieldDismiss20 => Get(0xfc83);

            public IconUsage TabShieldDismiss24 => Get(0xfc84);

            public IconUsage TableDeleteColumn24 => Get(0xfc85);

            public IconUsage TableDeleteRow24 => Get(0xfc86);

            public IconUsage TableFreezeColumnAndRow24 => Get(0xfc87);

            public IconUsage TableInsertColumn24 => Get(0xfc88);

            public IconUsage TableInsertRow24 => Get(0xfc89);

            public IconUsage TableMoveAbove24 => Get(0xfc8a);

            public IconUsage TableMoveBelow24 => Get(0xfc8b);

            public IconUsage TableResizeColumn24 => Get(0xfc8c);

            public IconUsage TableResizeRow24 => Get(0xfc8d);

            public IconUsage TableStackAbove24 => Get(0xfc8e);

            public IconUsage TableStackBelow24 => Get(0xfc8f);

            public IconUsage TableStackLeft24 => Get(0xfc90);

            public IconUsage TableStackRight24 => Get(0xfc91);

            public IconUsage TagQuestionMark16 => Get(0xfc92);

            public IconUsage TagQuestionMark24 => Get(0xfc93);

            public IconUsage TextGrammarArrowLeft20 => Get(0xfc94);

            public IconUsage TextGrammarArrowLeft24 => Get(0xfc95);

            public IconUsage TextGrammarArrowRight20 => Get(0xfc96);

            public IconUsage TextGrammarArrowRight24 => Get(0xfc97);

            public IconUsage TextGrammarCheckmark20 => Get(0xfc98);

            public IconUsage TextGrammarCheckmark24 => Get(0xfc99);

            public IconUsage TextGrammarDismiss20 => Get(0xfc9a);

            public IconUsage TextGrammarDismiss24 => Get(0xfc9b);

            public IconUsage TextGrammarSettings20 => Get(0xfc9c);

            public IconUsage TextGrammarSettings24 => Get(0xfc9d);

            public IconUsage TextGrammarWand16 => Get(0xfc9e);

            public IconUsage TextGrammarWand20 => Get(0xfc9f);

            public IconUsage TextGrammarWand24 => Get(0xfca0);

            public IconUsage TextParagraph20 => Get(0xfca1);

            public IconUsage TextParagraph24 => Get(0xfca2);

            public IconUsage TextParagraphDirection20 => Get(0xfca3);

            public IconUsage TextParagraphDirection24 => Get(0xfca4);

            public IconUsage TextPositionBehind20 => Get(0xfca5);

            public IconUsage TextPositionBehind24 => Get(0xfca6);

            public IconUsage TextPositionFront20 => Get(0xfca7);

            public IconUsage TextPositionFront24 => Get(0xfca8);

            public IconUsage TextPositionLine20 => Get(0xfca9);

            public IconUsage TextPositionLine24 => Get(0xfcaa);

            public IconUsage TextPositionSquare20 => Get(0xfcab);

            public IconUsage TextPositionSquare24 => Get(0xfcac);

            public IconUsage TextPositionThrough20 => Get(0xfcad);

            public IconUsage TextPositionThrough24 => Get(0xfcae);

            public IconUsage TextPositionTight20 => Get(0xfcaf);

            public IconUsage TextPositionTight24 => Get(0xfcb0);

            public IconUsage TextPositionTopBottom20 => Get(0xfcb1);

            public IconUsage TextPositionTopBottom24 => Get(0xfcb2);

            public IconUsage TextboxAlignCenter20 => Get(0xfcb3);

            public IconUsage TextboxAlignCenter24 => Get(0xfcb4);

            public IconUsage ThumbLike16 => Get(0xfcb5);

            public IconUsage TicketHorizontal20 => Get(0xfcb6);

            public IconUsage TicketHorizontal24 => Get(0xfcb7);

            public IconUsage Wand16 => Get(0xfcb8);

            public IconUsage Wand20 => Get(0xfcb9);

            public IconUsage Wand24 => Get(0xfcba);

            public IconUsage Wand28 => Get(0xfcbb);

            public IconUsage Wand48 => Get(0xfcbc);

            public IconUsage WindowArrowUp24 => Get(0xfcbd);

            public IconUsage WindowHeaderHorizontal20 => Get(0xfcbe);

            public IconUsage WindowHeaderVertical20 => Get(0xfcbf);

            public IconUsage Accessibility32 => Get(0xfcc0);

            public IconUsage AccessibilityCheckmark24 => Get(0xfcc1);

            public IconUsage AddCircle16 => Get(0xfcc2);

            public IconUsage AddCircle32 => Get(0xfcc3);

            public IconUsage AnimalRabbit16 => Get(0xfcc4);

            public IconUsage AnimalRabbit20 => Get(0xfcc5);

            public IconUsage AnimalRabbit24 => Get(0xfcc6);

            public IconUsage AnimalRabbit28 => Get(0xfcc7);

            public IconUsage AnimalTurtle16 => Get(0xfcc8);

            public IconUsage AnimalTurtle20 => Get(0xfcc9);

            public IconUsage AnimalTurtle24 => Get(0xfcca);

            public IconUsage AnimalTurtle28 => Get(0xfccb);

            public IconUsage BookContacts20 => Get(0xfccc);

            public IconUsage BookContacts24 => Get(0xfccd);

            public IconUsage BookContacts28 => Get(0xfcce);

            public IconUsage BookOpenGlobe20 => Get(0xfccf);

            public IconUsage CalligraphyPenCheckmark20 => Get(0xfcd0);

            public IconUsage CalligraphyPenQuestionMark20 => Get(0xfcd1);

            public IconUsage Cellular5G24 => Get(0xfcd2);

            public IconUsage Checkbox124 => Get(0xfcd3);

            public IconUsage Checkbox224 => Get(0xfcd4);

            public IconUsage CheckboxArrowRight24 => Get(0xfcd5);

            public IconUsage CheckboxPerson24 => Get(0xfcd6);

            public IconUsage CheckboxWarning24 => Get(0xfcd7);

            public IconUsage CircleEdit24 => Get(0xfcd8);

            public IconUsage Clock32 => Get(0xfcd9);

            public IconUsage Cloud16 => Get(0xfcda);

            public IconUsage Cloud32 => Get(0xfcdb);

            public IconUsage CommentNote24 => Get(0xfcdc);

            public IconUsage ContentSettings32 => Get(0xfcdd);

            public IconUsage DesktopMac16 => Get(0xfcde);

            public IconUsage DesktopMac32 => Get(0xfcdf);

            public IconUsage DocumentArrowRight24 => Get(0xfce0);

            public IconUsage DocumentCheckmark24 => Get(0xfce1);

            public IconUsage DualScreenDismiss24 => Get(0xfce2);

            public IconUsage DualScreenSpeaker24 => Get(0xfce3);

            public IconUsage FilterDismiss24 => Get(0xfce4);

            public IconUsage FilterSync24 => Get(0xfce5);

            public IconUsage Folder16 => Get(0xfce6);

            public IconUsage Folder32 => Get(0xfce7);

            public IconUsage GlobePerson24 => Get(0xfce8);

            public IconUsage HomePerson24 => Get(0xfce9);

            public IconUsage ImageGlobe24 => Get(0xfcea);

            public IconUsage InkingTool32 => Get(0xfceb);

            public IconUsage Key16 => Get(0xfcec);

            public IconUsage Key32 => Get(0xfced);

            public IconUsage LineStyle24 => Get(0xfcee);

            public IconUsage MathFormula16 => Get(0xfcef);

            public IconUsage MathFormula32 => Get(0xfcf0);

            public IconUsage NotebookAdd24 => Get(0xfcf1);

            public IconUsage NotebookSectionArrowRight24 => Get(0xfcf2);

            public IconUsage NotebookSubsection24 => Get(0xfcf3);

            public IconUsage Orientation20 => Get(0xfcf4);

            public IconUsage People32 => Get(0xfcf5);

            public IconUsage PersonNote24 => Get(0xfcf6);

            public IconUsage PhoneLaptop16 => Get(0xfcf7);

            public IconUsage PhoneLaptop32 => Get(0xfcf8);

            public IconUsage PhoneSpeaker24 => Get(0xfcf9);

            public IconUsage Pi24 => Get(0xfcfa);

            public IconUsage Premium32 => Get(0xfcfb);

            public IconUsage Receipt20 => Get(0xfcfc);

            public IconUsage Receipt24 => Get(0xfcfd);

            public IconUsage Rss24 => Get(0xfcfe);

            public IconUsage ScreenCut20 => Get(0xfcff);

            public IconUsage ScreenPerson20 => Get(0xfd00);

            public IconUsage ShapeExclude16 => Get(0xfd01);

            public IconUsage ShapeExclude20 => Get(0xfd02);

            public IconUsage ShapeExclude24 => Get(0xfd03);

            public IconUsage ShapeIntersect16 => Get(0xfd04);

            public IconUsage ShapeIntersect20 => Get(0xfd05);

            public IconUsage ShapeIntersect24 => Get(0xfd06);

            public IconUsage ShapeSubtract16 => Get(0xfd07);

            public IconUsage ShapeSubtract20 => Get(0xfd08);

            public IconUsage ShapeSubtract24 => Get(0xfd09);

            public IconUsage ShapeUnion16 => Get(0xfd0a);

            public IconUsage ShapeUnion20 => Get(0xfd0b);

            public IconUsage ShapeUnion24 => Get(0xfd0c);

            public IconUsage Shifts16 => Get(0xfd0d);

            public IconUsage SlideSettings24 => Get(0xfd0e);

            public IconUsage SlideTransition24 => Get(0xfd0f);

            public IconUsage StarEmphasis32 => Get(0xfd10);

            public IconUsage Table32 => Get(0xfd11);

            public IconUsage TableCellEdit24 => Get(0xfd12);

            public IconUsage TabletSpeaker24 => Get(0xfd13);

            public IconUsage Target32 => Get(0xfd14);

            public IconUsage Timer324 => Get(0xfd15);

            public IconUsage Voicemail28 => Get(0xfd16);

            public IconUsage WalkieTalkie20 => Get(0xfd17);

            public IconUsage WarningShield20 => Get(0xfd18);

            public IconUsage AddSubtractCircle16 => Get(0xfd19);

            public IconUsage AddSubtractCircle20 => Get(0xfd1a);

            public IconUsage AddSubtractCircle24 => Get(0xfd1b);

            public IconUsage AddSubtractCircle28 => Get(0xfd1c);

            public IconUsage AddSubtractCircle48 => Get(0xfd1d);

            public IconUsage Beach16 => Get(0xfd1e);

            public IconUsage Beach20 => Get(0xfd1f);

            public IconUsage Beach24 => Get(0xfd20);

            public IconUsage Beach28 => Get(0xfd21);

            public IconUsage Building16 => Get(0xfd22);

            public IconUsage CalendarEdit16 => Get(0xfd23);

            public IconUsage CalendarEdit20 => Get(0xfd24);

            public IconUsage CalendarEdit24 => Get(0xfd25);

            public IconUsage CalendarLtr20 => Get(0xfd26);

            public IconUsage CalendarLtr24 => Get(0xfd27);

            public IconUsage CalendarLtr28 => Get(0xfd28);

            public IconUsage CalendarRtl20 => Get(0xfd29);

            public IconUsage CalendarRtl24 => Get(0xfd2a);

            public IconUsage CalendarRtl28 => Get(0xfd2b);

            public IconUsage CircleSmall20 => Get(0xfd2c);

            public IconUsage Clipboard16 => Get(0xfd2d);

            public IconUsage ClipboardArrowRight16 => Get(0xfd2e);

            public IconUsage ClipboardArrowRight20 => Get(0xfd2f);

            public IconUsage ClipboardArrowRight24 => Get(0xfd30);

            public IconUsage ClipboardTextLtr20 => Get(0xfd31);

            public IconUsage ClipboardTextLtr24 => Get(0xfd32);

            public IconUsage ClipboardTextRtl20 => Get(0xfd33);

            public IconUsage ClipboardTextRtl24 => Get(0xfd34);

            public IconUsage ConvertToType20 => Get(0xfd35);

            public IconUsage ConvertToType24 => Get(0xfd36);

            public IconUsage CubeSync24 => Get(0xfd37);

            public IconUsage DocumentQuestionMark16 => Get(0xfd38);

            public IconUsage DocumentQuestionMark20 => Get(0xfd39);

            public IconUsage DocumentQuestionMark24 => Get(0xfd3a);

            public IconUsage DoorArrowLeft20 => Get(0xfd3b);

            public IconUsage Drop12 => Get(0xfd3c);

            public IconUsage Drop16 => Get(0xfd3d);

            public IconUsage Drop20 => Get(0xfd3e);

            public IconUsage Drop24 => Get(0xfd3f);

            public IconUsage Drop28 => Get(0xfd40);

            public IconUsage Drop48 => Get(0xfd41);

            public IconUsage Dumbbell16 => Get(0xfd42);

            public IconUsage Dumbbell20 => Get(0xfd43);

            public IconUsage Dumbbell24 => Get(0xfd44);

            public IconUsage Dumbbell28 => Get(0xfd45);

            public IconUsage EditOff20 => Get(0xfd46);

            public IconUsage Eyedropper20 => Get(0xfd47);

            public IconUsage Eyedropper24 => Get(0xfd48);

            public IconUsage FlagOff16 => Get(0xfd49);

            public IconUsage FlagOff20 => Get(0xfd4a);

            public IconUsage Fps12020 => Get(0xfd4b);

            public IconUsage Fps12024 => Get(0xfd4c);

            public IconUsage Fps24020 => Get(0xfd4d);

            public IconUsage Guitar16 => Get(0xfd4e);

            public IconUsage Guitar20 => Get(0xfd4f);

            public IconUsage Guitar24 => Get(0xfd50);

            public IconUsage Guitar28 => Get(0xfd51);

            public IconUsage KeyCommand16 => Get(0xfd52);

            public IconUsage MicOn32 => Get(0xfd53);

            public IconUsage MoreVertical16 => Get(0xfd54);

            public IconUsage PeopleCheckmark20 => Get(0xfd55);

            public IconUsage PeopleCheckmark24 => Get(0xfd56);

            public IconUsage PlayCircle16 => Get(0xfd57);

            public IconUsage PlayCircle20 => Get(0xfd58);

            public IconUsage PlayCircle28 => Get(0xfd59);

            public IconUsage ReOrderDotsHorizontal16 => Get(0xfd5a);

            public IconUsage ReOrderDotsHorizontal20 => Get(0xfd5b);

            public IconUsage ReOrderDotsHorizontal24 => Get(0xfd5c);

            public IconUsage ReOrderDotsVertical16 => Get(0xfd5d);

            public IconUsage ReOrderDotsVertical20 => Get(0xfd5e);

            public IconUsage ReOrderDotsVertical24 => Get(0xfd5f);

            public IconUsage ScaleFill20 => Get(0xfd60);

            public IconUsage SkipBack1020 => Get(0xfd61);

            public IconUsage SkipForward1020 => Get(0xfd62);

            public IconUsage SkipForward3020 => Get(0xfd63);

            public IconUsage SlideEraser24 => Get(0xfd64);

            public IconUsage SplitHorizontal12 => Get(0xfd65);

            public IconUsage SplitHorizontal16 => Get(0xfd66);

            public IconUsage SplitHorizontal20 => Get(0xfd67);

            public IconUsage SplitHorizontal24 => Get(0xfd68);

            public IconUsage SplitHorizontal28 => Get(0xfd69);

            public IconUsage SplitHorizontal32 => Get(0xfd6a);

            public IconUsage SplitHorizontal48 => Get(0xfd6b);

            public IconUsage SplitVertical12 => Get(0xfd6c);

            public IconUsage SplitVertical16 => Get(0xfd6d);

            public IconUsage SplitVertical20 => Get(0xfd6e);

            public IconUsage SplitVertical24 => Get(0xfd6f);

            public IconUsage SplitVertical28 => Get(0xfd70);

            public IconUsage SplitVertical32 => Get(0xfd71);

            public IconUsage SplitVertical48 => Get(0xfd72);

            public IconUsage SportSoccer20 => Get(0xfd73);

            public IconUsage SportSoccer24 => Get(0xfd74);

            public IconUsage StrikethroughGaNa16 => Get(0xfd75);

            public IconUsage StrikethroughGaNa20 => Get(0xfd76);

            public IconUsage StrikethroughGaNa24 => Get(0xfd77);

            public IconUsage Symbols20 => Get(0xfd78);

            public IconUsage TableDeleteColumn20 => Get(0xfd79);

            public IconUsage TableDeleteRow20 => Get(0xfd7a);

            public IconUsage TableStackAbove20 => Get(0xfd7b);

            public IconUsage TableStackDown20 => Get(0xfd7c);

            public IconUsage TableStackLeft20 => Get(0xfd7d);

            public IconUsage TableStackRight20 => Get(0xfd7e);

            public IconUsage TaskListLtr20 => Get(0xfd7f);

            public IconUsage TaskListLtr24 => Get(0xfd80);

            public IconUsage TaskListRtl20 => Get(0xfd81);

            public IconUsage TaskListRtl24 => Get(0xfd82);

            public IconUsage TetrisApp16 => Get(0xfd83);

            public IconUsage TetrisApp20 => Get(0xfd84);

            public IconUsage TetrisApp24 => Get(0xfd85);

            public IconUsage TetrisApp28 => Get(0xfd86);

            public IconUsage TetrisApp32 => Get(0xfd87);

            public IconUsage TetrisApp48 => Get(0xfd88);

            public IconUsage TextBulletListLtr20 => Get(0xfd89);

            public IconUsage TextBulletListLtr24 => Get(0xfd8a);

            public IconUsage TextBulletListRtl20 => Get(0xfd8b);

            public IconUsage TextBulletListRtl24 => Get(0xfd8c);

            public IconUsage TextStrikethroughS16 => Get(0xfd8d);

            public IconUsage TextStrikethroughS20 => Get(0xfd8e);

            public IconUsage TextStrikethroughS24 => Get(0xfd8f);

            public IconUsage VehicleCab16 => Get(0xfd90);

            public IconUsage VehicleCab20 => Get(0xfd91);

            public IconUsage VehicleCab28 => Get(0xfd92);

            public IconUsage VehicleTruckProfile24 => Get(0xfd93);

            public IconUsage BotAdd20 => Get(0xfd94);

            public IconUsage ChartPerson20 => Get(0xfd95);

            public IconUsage ChartPerson24 => Get(0xfd96);

            public IconUsage ChartPerson28 => Get(0xfd97);

            public IconUsage ChartPerson48 => Get(0xfd98);

            public IconUsage ConvertToTypeOff20 => Get(0xfd99);

            public IconUsage MicProhibited16 => Get(0xfd9a);

            public IconUsage MicProhibited28 => Get(0xfd9b);

            public IconUsage MicProhibited48 => Get(0xfd9c);

            public IconUsage TvUsb16 => Get(0xfd9d);

            public IconUsage TvUsb20 => Get(0xfd9e);

            public IconUsage TvUsb24 => Get(0xfd9f);

            public IconUsage TvUsb28 => Get(0xfda0);

            public IconUsage TvUsb48 => Get(0xfda1);

            public IconUsage Video360Off20 => Get(0xfda2);

            public IconUsage VideoProhibited16 => Get(0xfda3);

            public IconUsage VideoProhibited24 => Get(0xfda4);

            public IconUsage VideoProhibited28 => Get(0xfda5);

            public IconUsage Alert32 => Get(0xfda6);

            public IconUsage ApprovalApp32 => Get(0xfda7);

            public IconUsage ArrowDownLeft20 => Get(0xfda8);

            public IconUsage ArrowStepBack16 => Get(0xfda9);

            public IconUsage ArrowStepIn16 => Get(0xfdaa);

            public IconUsage ArrowStepOut16 => Get(0xfdab);

            public IconUsage ArrowStepOver16 => Get(0xfdac);

            public IconUsage ArrowUpRight20 => Get(0xfdad);

            public IconUsage Backpack32 => Get(0xfdae);

            public IconUsage BookContacts32 => Get(0xfdaf);

            public IconUsage Bookmark32 => Get(0xfdb0);

            public IconUsage BookmarkMultiple24 => Get(0xfdb1);

            public IconUsage BranchCompare16 => Get(0xfdb2);

            public IconUsage BranchCompare20 => Get(0xfdb3);

            public IconUsage BranchCompare24 => Get(0xfdb4);

            public IconUsage BranchFork16 => Get(0xfdb5);

            public IconUsage BranchFork20 => Get(0xfdb6);

            public IconUsage BranchFork24 => Get(0xfdb7);

            public IconUsage CalendarLtr16 => Get(0xfdb8);

            public IconUsage CalendarLtr32 => Get(0xfdb9);

            public IconUsage CalendarRtl32 => Get(0xfdba);

            public IconUsage Call32 => Get(0xfdbb);

            public IconUsage CalligraphyPenError20 => Get(0xfdbc);

            public IconUsage Chat32 => Get(0xfdbd);

            public IconUsage ClipboardDataBar32 => Get(0xfdbe);

            public IconUsage ClockAlarm32 => Get(0xfdbf);

            public IconUsage ContentView32 => Get(0xfdc0);

            public IconUsage Desktop32 => Get(0xfdc1);

            public IconUsage DismissSquareMultiple16 => Get(0xfdc2);

            public IconUsage Document32 => Get(0xfdc3);

            public IconUsage DocumentPdf32 => Get(0xfdc4);

            public IconUsage FoodPizza20 => Get(0xfdc5);

            public IconUsage FoodPizza24 => Get(0xfdc6);

            public IconUsage Globe32 => Get(0xfdc7);

            public IconUsage Headset32 => Get(0xfdc8);

            public IconUsage HeartPulse20 => Get(0xfdc9);

            public IconUsage Multiplier12X20 => Get(0xfdca);

            public IconUsage Multiplier12X24 => Get(0xfdcb);

            public IconUsage Multiplier12X28 => Get(0xfdcc);

            public IconUsage Multiplier12X32 => Get(0xfdcd);

            public IconUsage Multiplier12X48 => Get(0xfdce);

            public IconUsage Multiplier15X20 => Get(0xfdcf);

            public IconUsage Multiplier15X24 => Get(0xfdd0);

            public IconUsage Multiplier15X28 => Get(0xfdd1);

            public IconUsage Multiplier15X32 => Get(0xfdd2);

            public IconUsage Multiplier15X48 => Get(0xfdd3);

            public IconUsage Multiplier18X20 => Get(0xfdd4);

            public IconUsage Multiplier18X24 => Get(0xfdd5);

            public IconUsage Multiplier18X28 => Get(0xfdd6);

            public IconUsage Multiplier18X32 => Get(0xfdd7);

            public IconUsage Multiplier18X48 => Get(0xfdd8);

            public IconUsage Multiplier1X20 => Get(0xfdd9);

            public IconUsage Multiplier1X24 => Get(0xfdda);

            public IconUsage Multiplier1X28 => Get(0xfddb);

            public IconUsage Multiplier1X32 => Get(0xfddc);

            public IconUsage Multiplier1X48 => Get(0xfddd);

            public IconUsage Multiplier2X20 => Get(0xfdde);

            public IconUsage Multiplier2X24 => Get(0xfddf);

            public IconUsage Multiplier2X28 => Get(0xfde0);

            public IconUsage Multiplier2X32 => Get(0xfde1);

            public IconUsage Multiplier2X48 => Get(0xfde2);

            public IconUsage MyLocation16 => Get(0xfde3);

            public IconUsage MyLocation20 => Get(0xfde4);

            public IconUsage Notepad32 => Get(0xfde5);

            public IconUsage Patient32 => Get(0xfde6);

            public IconUsage PeopleTeam32 => Get(0xfde7);

            public IconUsage Pulse32 => Get(0xfde8);

            public IconUsage Remote16 => Get(0xfde9);

            public IconUsage Ribbon32 => Get(0xfdea);

            public IconUsage Shifts32 => Get(0xfdeb);

            public IconUsage SkipBackward1024 => Get(0xfdec);

            public IconUsage SkipBackward1028 => Get(0xfded);

            public IconUsage SkipBackward1032 => Get(0xfdee);

            public IconUsage SkipBackward1048 => Get(0xfdef);

            public IconUsage SkipForward1024 => Get(0xfdf0);

            public IconUsage SkipForward1028 => Get(0xfdf1);

            public IconUsage SkipForward1032 => Get(0xfdf2);

            public IconUsage SkipForward1048 => Get(0xfdf3);

            public IconUsage SkipForward3024 => Get(0xfdf4);

            public IconUsage SkipForward3028 => Get(0xfdf5);

            public IconUsage SkipForward3032 => Get(0xfdf6);

            public IconUsage SkipForward3048 => Get(0xfdf7);

            public IconUsage SubtractSquareMultiple16 => Get(0xfdf8);

            public IconUsage Text20 => Get(0xfdf9);

            public IconUsage TextSortAscending16 => Get(0xfdfa);

            public IconUsage TextSortAscending24 => Get(0xfdfb);

            public IconUsage TextSortDescending16 => Get(0xfdfc);

            public IconUsage TextSortDescending24 => Get(0xfdfd);

            public IconUsage VideoPersonCall32 => Get(0xfdfe);

            public IconUsage WeatherSunny32 => Get(0xfdff);

            public IconUsage AlignBottom16 => Get(0xfe00);

            public IconUsage AlignBottom20 => Get(0xfe01);

            public IconUsage AlignBottom24 => Get(0xfe02);

            public IconUsage AlignBottom28 => Get(0xfe03);

            public IconUsage AlignBottom32 => Get(0xfe04);

            public IconUsage AlignBottom48 => Get(0xfe05);

            public IconUsage AlignCenterHorizontal16 => Get(0xfe06);

            public IconUsage AlignCenterHorizontal20 => Get(0xfe07);

            public IconUsage AlignCenterHorizontal24 => Get(0xfe08);

            public IconUsage AlignCenterHorizontal28 => Get(0xfe09);

            public IconUsage AlignCenterHorizontal32 => Get(0xfe0a);

            public IconUsage AlignCenterHorizontal48 => Get(0xfe0b);

            public IconUsage AlignCenterVertical16 => Get(0xfe0c);

            public IconUsage AlignCenterVertical20 => Get(0xfe0d);

            public IconUsage AlignCenterVertical24 => Get(0xfe0e);

            public IconUsage AlignCenterVertical28 => Get(0xfe0f);

            public IconUsage AlignCenterVertical32 => Get(0xfe10);

            public IconUsage AlignCenterVertical48 => Get(0xfe11);

            public IconUsage AlignLeft16 => Get(0xfe12);

            public IconUsage AlignLeft20 => Get(0xfe13);

            public IconUsage AlignLeft24 => Get(0xfe14);

            public IconUsage AlignLeft28 => Get(0xfe15);

            public IconUsage AlignLeft32 => Get(0xfe16);

            public IconUsage AlignLeft48 => Get(0xfe17);

            public IconUsage AlignRight16 => Get(0xfe18);

            public IconUsage AlignRight20 => Get(0xfe19);

            public IconUsage AlignRight24 => Get(0xfe1a);

            public IconUsage AlignRight28 => Get(0xfe1b);

            public IconUsage AlignRight32 => Get(0xfe1c);

            public IconUsage AlignRight48 => Get(0xfe1d);

            public IconUsage AlignTop16 => Get(0xfe1e);

            public IconUsage AlignTop20 => Get(0xfe1f);

            public IconUsage AlignTop24 => Get(0xfe20);

            public IconUsage AlignTop28 => Get(0xfe21);

            public IconUsage AlignTop32 => Get(0xfe22);

            public IconUsage AlignTop48 => Get(0xfe23);

            public IconUsage Calculator24 => Get(0xfe24);

            public IconUsage Camera16 => Get(0xfe25);

            public IconUsage GroupDismiss24 => Get(0xfe26);

            public IconUsage GroupReturn24 => Get(0xfe27);

            public IconUsage HandLeft16 => Get(0xfe28);

            public IconUsage HandLeft24 => Get(0xfe29);

            public IconUsage HandLeft28 => Get(0xfe2a);

            public IconUsage HandRight16 => Get(0xfe2b);

            public IconUsage Home12 => Get(0xfe2c);

            public IconUsage KeyboardShift16 => Get(0xfe2d);

            public IconUsage KeyboardShift20 => Get(0xfe2e);

            public IconUsage LinkSquare20 => Get(0xfe2f);

            public IconUsage MailInboxCheckmark16 => Get(0xfe30);

            public IconUsage MailInboxCheckmark20 => Get(0xfe31);

            public IconUsage MailInboxCheckmark24 => Get(0xfe32);

            public IconUsage MusicNote220 => Get(0xfe33);

            public IconUsage MyLocation12 => Get(0xfe34);

            public IconUsage NumberSymbolSquare20 => Get(0xfe35);

            public IconUsage NumberSymbolSquare24 => Get(0xfe36);

            public IconUsage Person32 => Get(0xfe37);

            public IconUsage Person532 => Get(0xfe38);

            public IconUsage Search16 => Get(0xfe39);

            public IconUsage Send16 => Get(0xfe3a);

            public IconUsage Symbols16 => Get(0xfe3b);

            public IconUsage Teddy20 => Get(0xfe3c);

            public IconUsage VideoPersonStarOff24 => Get(0xfe3d);
        }
    }
}
