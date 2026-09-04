using AlliED.Extensions;

namespace AlliED.Impl.ViewsImpl;

internal static class HyperBoxImpl
{
    public static void Register(HyperBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(HyperBox window)
    {
        window.Closed += (s, e) => THyperForm_FormClose(window);
    }

    // L004C38F4
    private static void FormCreate(HyperBox HyperForm)
    {
        for (int esi = 0; esi < 4; esi++)
        {
            HyperForm.FromBox.AddItem(AlliedVariables.s_TieFileHeader.Header.Regions[esi].Name);
        }

        HyperForm.ToBox.SetItems(HyperForm.FromBox.Items);

        HyperForm.FromBox.SelectedIndex = 0;
        HyperForm.ToBox.SelectedIndex = 1;
    }

    // L004C39A8
    private static void THyperForm_FormClose(HyperBox HyperForm)
    {
        //AlliedVariables.s_THyperForm_Instance!.Visibility = Visibility.Visible;

        if (HyperForm.DialogResult == true)
        {
            Unit_00513838_Proc_005146A4();

            string ebp10_1 = string.Empty;
            string ebp10_0 = string.Empty;

            if (HyperForm.DoFroms.IsChecked == true)
            {
                ebp10_1 = "To ";
                ebp10_0 = "From ";
            }

            int fromBoxSelectedIndex = HyperForm.FromBox.SelectedIndex;
            int toBoxSelectedIndex = HyperForm.ToBox.SelectedIndex;

            byte ebp08 = 0;

            if (AlliedVariables.s_TieFileHeader.Header.MissionType == 0x07)
            {
                ebp08 = 0x05;
            }
            else
            {
                ebp08 = 0;
            }

            S0xFGObject ebx;

            ebx = new();
            ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            ebx.AutoLink = true;
            ebx.FlightGroupStruct.StartPointRegions[0] = (byte)fromBoxSelectedIndex;
            ebx.m001444[fromBoxSelectedIndex] = 0x01;
            ebx.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
            ebx.FlightGroupStruct.TacticalRole0 = (TacticalRoleEnum)((int)TacticalRoleEnum.ExitToR1 + toBoxSelectedIndex);

            if (HyperForm.DoFroms.IsChecked != true)
            {
                ebx.FlightGroupStruct.TacticalRoleUsed1 = TacticalRoleUsedEnum.NoTC;
                ebx.FlightGroupStruct.TacticalRole1 = (TacticalRoleEnum)((int)TacticalRoleEnum.InFromR1 + fromBoxSelectedIndex);
            }

            ebx.FlightGroupStruct.CraftId = CraftIdEnum._085_1_8_BuoyFaux;
            ebx.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Indestructible;
            ebx.FlightGroupStruct.Iff = ebp08;
            ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(ebp10_1 + AlliedVariables.s_TieFileHeader.Header.Regions[toBoxSelectedIndex].Name);
            ebx.FlightGroupStruct.Orders[fromBoxSelectedIndex * 4].OrderId = TieOrderIdEnum._49_HyperBuoy;

            ebx.m001448 = 1.0;
            ebx.m001450 = 0;
            ebx.m001458 = 0;
            ebx.m001460 = -1.0;

            AlliedVariables.s_FlightGroupObjectsList.Add(ebx);

            if (HyperForm.DoFroms.IsChecked == true)
            {
                ebx = new();
                ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
                ebx.AutoLink = true;
                ebx.FlightGroupStruct.StartPointRegions[0] = (byte)toBoxSelectedIndex;
                ebx.m001444[toBoxSelectedIndex] = 0x01;
                ebx.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                ebx.FlightGroupStruct.TacticalRole0 = (TacticalRoleEnum)((int)TacticalRoleEnum.InFromR1 + fromBoxSelectedIndex);

                ebx.FlightGroupStruct.CraftId = CraftIdEnum._085_1_8_BuoyFaux;
                ebx.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Indestructible;
                ebx.FlightGroupStruct.Iff = ebp08;
                ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(ebp10_0 + AlliedVariables.s_TieFileHeader.Header.Regions[fromBoxSelectedIndex].Name);
                ebx.FlightGroupStruct.Orders[toBoxSelectedIndex * 4].OrderId = TieOrderIdEnum._49_HyperBuoy;

                ebx.m001448 = 1.0;
                ebx.m001450 = 0;
                ebx.m001458 = 0;
                ebx.m001460 = -1.0;

                AlliedVariables.s_FlightGroupObjectsList.Add(ebx);
            }

            ebx = new();
            ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            ebx.AutoLink = true;
            ebx.FlightGroupStruct.StartPointRegions[0] = (byte)toBoxSelectedIndex;
            ebx.m001444[toBoxSelectedIndex] = 0x01;
            ebx.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
            ebx.FlightGroupStruct.TacticalRole0 = (TacticalRoleEnum)((int)TacticalRoleEnum.ExitToR1 + fromBoxSelectedIndex);

            if (HyperForm.DoFroms.IsChecked != true)
            {
                ebx.FlightGroupStruct.TacticalRoleUsed1 = TacticalRoleUsedEnum.NoTC;
                ebx.FlightGroupStruct.TacticalRole1 = (TacticalRoleEnum)((int)TacticalRoleEnum.InFromR1 + toBoxSelectedIndex);
            }

            ebx.FlightGroupStruct.CraftId = CraftIdEnum._085_1_8_BuoyFaux;
            ebx.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Indestructible;
            ebx.FlightGroupStruct.Iff = ebp08;
            ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(ebp10_1 + AlliedVariables.s_TieFileHeader.Header.Regions[fromBoxSelectedIndex].Name);
            ebx.FlightGroupStruct.Orders[toBoxSelectedIndex * 4].OrderId = TieOrderIdEnum._49_HyperBuoy;

            ebx.m001448 = 1.0;
            ebx.m001450 = 0;
            ebx.m001458 = 0;
            ebx.m001460 = -1.0;

            AlliedVariables.s_FlightGroupObjectsList.Add(ebx);

            if (HyperForm.DoFroms.IsChecked == true)
            {
                ebx = new();
                ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
                ebx.AutoLink = true;
                ebx.FlightGroupStruct.StartPointRegions[0] = (byte)fromBoxSelectedIndex;
                ebx.m001444[fromBoxSelectedIndex] = 0x01;
                ebx.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                ebx.FlightGroupStruct.TacticalRole0 = (TacticalRoleEnum)((int)TacticalRoleEnum.InFromR1 + toBoxSelectedIndex);
                ebx.FlightGroupStruct.CraftId = CraftIdEnum._085_1_8_BuoyFaux;
                ebx.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Indestructible;
                ebx.FlightGroupStruct.Iff = ebp08;
                ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(ebp10_0 + AlliedVariables.s_TieFileHeader.Header.Regions[toBoxSelectedIndex].Name);
                ebx.FlightGroupStruct.Orders[fromBoxSelectedIndex * 4].OrderId = TieOrderIdEnum._49_HyperBuoy;

                ebx.m001448 = 1.0;
                ebx.m001450 = 0;
                ebx.m001458 = 0;
                ebx.m001460 = -1.0;

                AlliedVariables.s_FlightGroupObjectsList.Add(ebx);
            }
        }
    }
}
