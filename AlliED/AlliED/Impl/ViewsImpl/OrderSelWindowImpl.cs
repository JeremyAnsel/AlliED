using AlliED.Extensions;
using System.Globalization;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class OrderSelWindowImpl
{
    public static void Register(OrderSelWindow window)
    {
        SetBindings(window);
        FormCreate(window);

        window.Closing += (s, e) =>
        {
            if (MainImpl.IsMainOpened)
            {
                e.Cancel = true;
                window.Hide();
                window.Owner.Activate();
            }
        };
    }

    private static void SetBindings(OrderSelWindow window)
    {
        window.Closed += (s, e) => TOrderSel_FormDestroy(window);
        window.Closing += (s, e) => TOrderSel_FormClose(window);

        window.KeyDown += (s, e) => TOrderSel_FormKeyDown(window, s, e.Key, Keyboard.Modifiers);
        window.RegionTabs.SelectionChanged += (s, e) => TOrderSel_RegionTabsChange(window);
        window.OrderSelBox.MouseLeftButtonUp += (s, e) => TOrderSel_OrderSelBoxClick(window);
        window.OrderSelBox.MouseDoubleClick += (s, e) => TOrderSel_OrderSelBoxDblClick(window);
    }

    // L0050C870
    private static void FormCreate(OrderSelWindow window)
    {
        if (AlliedVariables.s_V0x005B6D13 == 0)
        {
            return;
        }

        if (Application_GetPixelsPerInch() == 0x78)
        {
            Controls_TSizeConstraints_SetConstraints(AlliedVariables.s_TOrderSel_Instance!, 0, 0x80);
            Controls_TSizeConstraints_SetConstraints(AlliedVariables.s_TOrderSel_Instance!, 0x02, 0x80);
        }

        AlliedVariables.s_V0x005B6D17 = 0x01;

        TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
        DatapadWindowImpl.TDatapad_SetTitle(AlliedVariables.s_TDatapad_Instance!);
        AlliedVariables.s_TDatapad_Instance!.TeamBox.SetItems(AlliedVariables.s_Strings_Teams);
        DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
        Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_V0x005AFC74.m000018.Top);
        Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_V0x005AFC74.m000018.Left);
        Form1WindowImpl.TForm1_Proc_0052F414(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B7048);

        if (!AlliedVariables.s_V0x005B7048)
        {
            ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowDatapad, false);
            Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Datapad1, false);
        }

        Form1WindowImpl.TForm1_Proc_0052BD6C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TOrderSel_Instance!, AlliedVariables.s_V0x005AFC74.m00000C);
        Form1WindowImpl.TForm1_Proc_0052F464(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFC74.m00000C.m000008 != 0);

        if ((byte)AlliedVariables.s_V0x005AFC74.m00000C.m000008 != 0)
        {
            ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowOrderSel, true);
            Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.OrderRegionSelect1, true);
            Buttons_L00467140_SetVisible(AlliedVariables.s_TOrderSel_Instance!, true);
        }
    }

    // L0050CA68
    public static void TOrderSel__PROC_0050CA68(OrderSelWindow eax0)
    {
        if (AlliedVariables.s_V0x005B6D17 != 0)
        {
            string ebp28_2 = System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[AlliedVariables.s_CurrentRegion - 1].Name, 0x84);
            string ebp04 = string.Format(CultureInfo.InvariantCulture, "Order {0}, Region #{1}: \"{2}\"", AlliedVariables.s_CurrentOrderInRegion, AlliedVariables.s_CurrentRegion, ebp28_2);
            Controls_TControl_SetText(AlliedVariables.s_TOrderSel_Instance!, ebp04);

            eax0.OrderSelBox.Clear();

            for (int ebx = 0; ebx < 0x04; ebx++)
            {
                string ebp28_0 = Form1WindowImpl.TForm1_Proc_00526288(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + ebx]);
                eax0.OrderSelBox.AddItem(ebp28_0);
            }

            eax0.OrderSelBox.SelectedIndex = AlliedVariables.s_CurrentOrderInRegion - 1;
            AlliedVariables.s_TDatapad_Instance!.XWOrderLab.Update();
        }
    }

    // L0050C9E4
    private static void TOrderSel_FormDestroy(OrderSelWindow OrderSel)
    {
        AlliedVariables.s_V0x005B6D17 = 0;
    }

    // L0050CEC0
    private static void TOrderSel_FormClose(OrderSelWindow OrderSel)
    {
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowOrderSel, false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.OrderRegionSelect1, false);

        if ((Form1OverallPagesEnum)Convert.ToInt32(AlliedVariables.s_AlliedForm1Window!.OverallPages.GetActivePage().Tag) == Form1OverallPagesEnum.FlightGroups)
        {
            AlliedVariables.s_V0x005B704A = false;
        }
    }

    // L0050CEB8
    private static void TOrderSel_FormKeyDown(OrderSelWindow OrderSel, object? Sender, Key key, ModifierKeys Shift)
    {
    }

    // L0050CCDC
    private static void TOrderSel_RegionTabsChange(OrderSelWindow OrderSel)
    {
        AlliedVariables.s_V0x00543C9A = 0;

        if (OrderSel.RegionTabs.SelectedIndex + 1 != AlliedVariables.s_CurrentRegion)
        {
            AlliedVariables.s_CurrentOrderInRegion = 0x01;
        }

        AlliedVariables.s_CurrentRegion = OrderSel.RegionTabs.SelectedIndex + 1;

        switch (AlliedVariables.s_CurrentRegion)
        {
            case 0x01:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R1btn, true);
                break;

            case 0x02:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R2btn, true);
                break;

            case 0x03:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R3Btn, true);
                break;

            case 0x04:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R4Btn, true);
                break;
        }

        switch (AlliedVariables.s_CurrentRegion)
        {
            case 0x01:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Region11, true);
                break;

            case 0x02:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Region21, true);
                break;

            case 0x03:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Region31, true);
                break;

            case 0x04:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Region41, true);
                break;
        }

        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_CurrentOrderInRegion);
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
        AlliedVariables.s_V0x00543C9A = 0x01;
        Form1WindowImpl.TForm1_Proc_0052D968(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFCBC!);

        if (AlliedVariables.s_V0x005B7050 < AlliedVariables.s_FlightGroupObjectsList.Count)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
            if (eax1.m00147A != 0)
            {
                AlliedVariables.s_V0x005AFCBC!.RenderOpen();
                Form1WindowImpl.TForm1_Proc_0052DC20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B7050, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!);
                AlliedVariables.s_V0x005AFCBC!.RenderClose();
            }
        }

        Form1WindowImpl.TForm1_FitBattleBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.FitBattleBtn);

        if (AlliedVariables.s_BlackenChkSetting)
        {
            Unit_00513838_Proc_0051DB68();
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L0050CBF4
    private static void TOrderSel_OrderSelBoxClick(OrderSelWindow OrderSel)
    {
        int eax1 = OrderSel.OrderSelBox.SelectedIndex + 1;
        byte ebx1 = AlliedVariables.s_V0x00543B54;
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentOrderInRegion = eax1;
        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, eax1);
        Unit_00513838_Proc_00516414((TieOrderIdEnum)Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.OrderBox));
        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543B54 = ebx1;
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);

        switch (AlliedVariables.s_CurrentOrderInRegion)
        {
            case 0x01:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Order11, true);
                break;

            case 0x02:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Order21, true);
                break;

            case 0x03:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Order31, true);
                break;

            case 0x04:
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Order41, true);
                break;
        }

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050CF0C
    private static void TOrderSel_OrderSelBoxDblClick(OrderSelWindow OrderSel)
    {
        AlliedVariables.s_TClipForm_Instance = MainImpl.CreateClipWindow();
        AlliedVariables.s_TClipForm_Instance.Owner = OrderSel;

        Controls_TControl_SetText(AlliedVariables.s_TClipForm_Instance, "Order Clipboard");
        AlliedVariables.s_ClipboardType = ClipboardTypeEnum.Order;

        AlliedVariables.s_TClipForm_Instance.ShowDialog();

        if (AlliedVariables.s_TClipForm_Instance.DialogResult == true)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                int eax1 = AlliedVariables.s_TClipForm_Instance.ListBox1.SelectedIndex;

                S0xOrdObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CFC, eax1);
                S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax3.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)] = S0xTieFlightGroupOrder.FromByteArray(eax2.m000004.ToByteArray());
            }

            Unit_00513838_Proc_005146A4();
        }

        AlliedVariables.s_TClipForm_Instance = null;

        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_CurrentOrderInRegion);
        TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    //// L0050C9F0
    //private static void TOrderSel_Order1LabClick(OrderSelWindow OrderSel, nint edx0)
    //{
    //    int esi = Marshal.ReadInt32(edx0, 0x0C);
    //    byte ebx = AlliedVariables.s_V0x00543B54;
    //    AlliedVariables.s_V0x00543C9A = 0;
    //    AlliedVariables.s_CurrentOrderInRegion = esi;
    //    Unit_00513838_Proc_00519B48(esi);
    //    DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, esi);
    //    Unit_00513838_Proc_00516414((TieOrderIdEnum)Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.OrderBox));
    //    AlliedVariables.s_V0x00543C9A = 0x01;
    //    AlliedVariables.s_V0x00543B54 = ebx;
    //    Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
    //    MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
    //}
}
