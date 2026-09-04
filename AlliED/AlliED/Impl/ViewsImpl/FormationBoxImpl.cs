using AlliED.Extensions;
using System.Globalization;

namespace AlliED.Impl.ViewsImpl;

internal static class FormationBoxImpl
{
    public static void Register(FormationBox window)
    {
        SetBindings(window);
        //FormCreate(window);
    }

    private static void SetBindings(FormationBox window)
    {
        window.Loaded += (s, e) =>
        {
            FormCreate(window);
            TFormForm_FormActivate(window);

            window.PaintBox1.InvalidateVisual();
        };
        window.Closed += (s, e) => TFormForm_FormClose(window);

        window.PrevForm.Click += (s, e) => TFormForm_PrevFormClick(window);
        window.NextForm.Click += (s, e) => TFormForm_NextFormClick(window);
        window.Button1.Click += (s, e) => TFormForm_Button1Click(window);
        window.Button2.Click += (s, e) => TFormForm_Button2Click(window);
        window.CheckBox1.Click += (s, e) => TFormForm_CheckBox1Click(window);
    }

    // L004CAD20
    private static void FormCreate(FormationBox FormForm)
    {
        AlliedVariables.s_V0x0053BDA0 = Graphics_TBitmap_Create((int)FormForm.PaintBox1.ActualWidth, (int)FormForm.PaintBox1.ActualHeight);

        FormForm.PaintBox1.Paint += (s, e) => TFormForm_PaintBox1Paint(FormForm);
    }

    // L004C8D34
    private static void TFormForm_FormActivate(FormationBox FormForm)
    {
        AlliedVariables.s_AlliedCurrentFormation = AlliedVariables.s_TShipExt_Instance!.FormBox.SelectedIndex;
        Graphics_TBrush_SetColor(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0);
        Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!.Color = 0x00EE0086;
        Graphics_TPen_SetColor(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x00C0C0C0);
        Graphics_TBrush_SetStyle(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x01);
        FormForm.CheckBox1.IsChecked = AlliedVariables.s_V0x0053BDA4;
    }

    // L004CAD5C
    private static void TFormForm_FormClose(FormationBox FormForm)
    {
        AlliedVariables.s_V0x0053BDA0 = null;
    }

    // L004C90E8
    private static void TFormForm_DrawForms(FormationBox FormForm, TieFormationEnum edx0)
    {
        AlliedVariables.s_V0x0053BDA0!.RenderOpen();

        AlliedVariables.s_V0x0053BD9C = 0;
        Graphics_TBrush_SetColor(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0);
        Graphics_TBrush_SetStyle(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0);
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, FormForm.GetClientRect());

        switch (edx0)
        {
            case TieFormationEnum._00_VicXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 6.0);
                break;

            case TieFormationEnum._01_FingerFour:
                TFormForm_DrawShip(FormForm, 0, 3.0, 1.0, 3.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 4.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 0.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 5.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 6.0, 6.5);
                break;

            case TieFormationEnum._02_LineAstern:
                TFormForm_DrawShip(FormForm, 0, 3.0, 1.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 6.0, 3.0);
                break;

            case TieFormationEnum._03_LineAhead:
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 6.0);
                break;

            case TieFormationEnum._04_EchelonRight:
                TFormForm_DrawShip(FormForm, 0, 3.0, 1.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 6.0, 6.0);
                break;

            case TieFormationEnum._05_EchelonLeft:
                TFormForm_DrawShip(FormForm, 0, 3.0, 1.0, 6.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 6.0, 1.0);
                break;

            case TieFormationEnum._06_LineAhead:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                break;

            case TieFormationEnum._07_DiamondXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                break;

            case TieFormationEnum._08_LineUp:
                TFormForm_DrawShip(FormForm, 0, 6.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 3.0, 3.0);
                break;

            case TieFormationEnum._09_LineAhead:
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                break;

            case TieFormationEnum._10_InvertedVicXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 6.0);
                break;

            case TieFormationEnum._11_SideVicYZ:
                TFormForm_DrawShip(FormForm, 0, 4.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 6.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 5.0, 3.0);
                break;

            case TieFormationEnum._12_InvSideVicYZ:
                TFormForm_DrawShip(FormForm, 0, 4.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 6.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 2.0, 3.0);
                break;

            case TieFormationEnum._13_LineAhead:
                TFormForm_DrawShip(FormForm, 0, 3.0, 6.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 1.0, 3.0);
                break;

            case TieFormationEnum._14_LineDown:
                TFormForm_DrawShip(FormForm, 0, 1.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 6.0, 3.0, 3.0);
                break;

            case TieFormationEnum._15_LineRight:
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 6.0);
                break;

            case TieFormationEnum._16_LineLeft:
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 6.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.0);
                break;

            case TieFormationEnum._17_EschelonAhead:
                TFormForm_DrawShip(FormForm, 0, 6.0, 6.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 5.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 2.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 1.0, 6.0);
                break;

            case TieFormationEnum._18_EchelonAstern:
                TFormForm_DrawShip(FormForm, 0, 6.0, 1.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 2.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 5.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 6.0, 6.0);
                break;

            case TieFormationEnum._19_EchelonUp:
                TFormForm_DrawShip(FormForm, 0, 6.0, 1.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 1.0, 6.0, 3.0);
                break;

            case TieFormationEnum._20_EchelonDown:
                TFormForm_DrawShip(FormForm, 0, 1.0, 1.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 6.0, 6.0, 3.0);
                break;

            case TieFormationEnum._21_VicUpZX:
                TFormForm_DrawShip(FormForm, 0, 5.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 6.0);
                break;

            case TieFormationEnum._22_VicDownZX:
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 1.0);
                TFormForm_DrawShip(FormForm, 0, 5.0, 3.0, 6.0);
                break;

            case TieFormationEnum._23_SquareXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                break;

            case TieFormationEnum._24_SquareYZ:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 4.0, 3.0);
                break;

            case TieFormationEnum._25_SquareZX:
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 4.0);
                break;

            case TieFormationEnum._26_DiamondYZ:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                break;

            case TieFormationEnum._27_DiamondZX:
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.0, 3.0);
                break;

            case TieFormationEnum._28_StarXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.5, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.5);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.5, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.5, 3.0);
                break;

            case TieFormationEnum._29_StarYZ:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.4, 4.7, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.6, 4.7, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.7, 3.0);
                break;

            case TieFormationEnum._30_StarZX:
                TFormForm_DrawShip(FormForm, 0, 2.3, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 4.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 1.3);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 4.7);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.4, 3.0, 3.0);
                break;

            case TieFormationEnum._31_HexXY:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.3, 2.8);
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.3, 4.2);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.5, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.5, 5.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.7, 2.8);
                TFormForm_DrawShip(FormForm, 0, 3.0, 4.7, 4.2);
                break;

            case TieFormationEnum._32_HexYZ:
                TFormForm_DrawShip(FormForm, 0, 3.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 2.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 2.2, 3.5, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.8, 3.5, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 5.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 5.0, 3.0);
                break;

            case TieFormationEnum._33_HexZX:
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 2.8);
                TFormForm_DrawShip(FormForm, 0, 2.0, 3.0, 4.2);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 2.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 5.0);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 2.8);
                TFormForm_DrawShip(FormForm, 0, 4.0, 3.0, 4.2);
                break;

            case TieFormationEnum._34_SinglePoint:
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                TFormForm_DrawShip(FormForm, 0, 3.0, 3.0, 3.0);
                break;
        }

        TBitmap esi0 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
        Graphics_TBrush_SetStyle(esi0, 0x01);
        esi0.FontFace = "MS Sans Serif";
        esi0.FontSize = 0x08;
        Graphics_TFont_SetColor(esi0, 0x00FFFFFF);
        string ebp20_2 = "Formation " + (AlliedVariables.s_AlliedCurrentFormation + 1).ToString(CultureInfo.InvariantCulture) + ":  " + AlliedVariables.s_Strings_Form.GetText((int)edx0);
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x122, 0xE1, ebp20_2);
        Graphics_TFont_SetColor(esi0, 0x00FFBFBF);
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x111, 0xF6, "(Formation patterns become corrupted ");
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x111, 0x104, "  for ships greater than 6.) ");
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x55, 0x173, "      X - Y");
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x55, 0x7C, "     X - Z");
        Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, 0x155, 0x7C, "   Y - Z");
        Graphics_TPen_SetColor(esi0, 0x00808080);
        Graphics_TCanvas_MoveTo(esi0, 0, 0x8C);
        Graphics_TCanvas_LineTo(esi0, (int)FormForm.PaintBox1.ActualWidth, 0x8C);
        Graphics_TCanvas_MoveTo(esi0, 0xEF, 0);
        Graphics_TCanvas_LineTo(esi0, 0xEF, (int)FormForm.PaintBox1.ActualHeight);

        AlliedVariables.s_V0x0053BDA0!.RenderClose();

        TBitmap esi1 = FormForm.PaintBox1.Bitmap!;
        esi1.Color = 0x00CC0020;
        TRect ebp14 = FormForm.GetClientRect();
        TRect ebp30 = FormForm.GetClientRect();
        Graphics_TCanvas_CopyRect(esi1, ebp30, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, ebp14);
    }

    // L004C8DC4
    private static void TFormForm_DrawShip(FormationBox FormForm, int edx0, double A4, double AC, double A14)
    {
        bool bl1 = false;
        AlliedVariables.s_V0x0053BD9C += 1;

        if (FormForm.CheckBox1.IsChecked == true)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);

            if (eax1.FlightGroupStruct.CraftsCount >= AlliedVariables.s_V0x0053BD9C)
            {
                bl1 = true;
            }
        }
        else
        {
            bl1 = true;
        }

        if (bl1)
        {
            TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
            eax1.FontFace = "MS Serif";
            eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
            eax1.FontSize = 0x07;
            eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
            Graphics_TFont_SetColor(eax1, 0x00C0C0C0);
            eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
            Graphics_TBrush_SetStyle(eax1, 0x01);

            TBitmap ebp04 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!;
            int ebx = (int)Math.Round((A14 * 1.0f) * 31.0f + 30.0f);
            int esi = (int)Math.Round((AC - 1.0f) * 40.0f + 150.0f);
            TRect ebp14 = new(0, 0, 0x08, 0x10);
            TRect ebp24 = new(ebx, esi, ebx + 0x08, esi + 0x10);
            Graphics_TCanvas_CopyRect(ebp04, ebp24, ExtCtrls_TImage_GetCanvas(FormForm.Image1Bitmap)!, ebp14);
            Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, ebx + 0x0A, esi + 0x0A, AlliedVariables.s_V0x0053BD9C.ToString(CultureInfo.InvariantCulture));

            ebx = (int)Math.Round((A14 - 1.0f) * 31.0f + 30.0f);
            esi = (int)Math.Round((A4 - 1.0f) * 17.0f + 10.0f);
            ebp14 = new(0x09, 0, 0x14, 0x10);
            ebp24 = new(ebx, esi, ebx + 0x0B, esi + 0x10);
            Graphics_TCanvas_CopyRect(ebp04, ebp24, ExtCtrls_TImage_GetCanvas(FormForm.Image1Bitmap)!, ebp14);
            Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, ebx + 0x0C, esi + 0x0A, AlliedVariables.s_V0x0053BD9C.ToString(CultureInfo.InvariantCulture));

            ebx = (int)Math.Round((AC - 1.0f) * 40.0f + 250.0f);
            esi = (int)Math.Round((A4 - 1.0f) * 17.0f + 10.0f);
            ebp14 = new(0x15, 0, 0x23, 0x10);
            ebp24 = new(ebx, esi, ebx + 0x0E, esi + 0x10);
            Graphics_TCanvas_CopyRect(ebp04, ebp24, ExtCtrls_TImage_GetCanvas(FormForm.Image1Bitmap)!, ebp14);
            Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, ebx + 0x0C, esi + 0x09, AlliedVariables.s_V0x0053BD9C.ToString(CultureInfo.InvariantCulture));
        }
    }

    // L004C8DB8
    private static void TFormForm_PaintBox1Paint(FormationBox FormForm)
    {
        TFormForm_DrawForms(FormForm, (TieFormationEnum)AlliedVariables.s_AlliedCurrentFormation);
    }

    // L004CAC8C
    private static void TFormForm_PrevFormClick(FormationBox FormForm)
    {
        AlliedVariables.s_AlliedCurrentFormation -= 1;

        if (AlliedVariables.s_AlliedCurrentFormation < 0)
        {
            AlliedVariables.s_AlliedCurrentFormation = 0x22;
        }

        TRect esp00 = FormForm.GetClientRect();
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, esp00);
        TFormForm_DrawForms(FormForm, (TieFormationEnum)AlliedVariables.s_AlliedCurrentFormation);
    }

    // L004CACD8
    private static void TFormForm_NextFormClick(FormationBox FormForm)
    {
        AlliedVariables.s_AlliedCurrentFormation += 1;

        if (AlliedVariables.s_AlliedCurrentFormation > 0x22)
        {
            AlliedVariables.s_AlliedCurrentFormation = 0;
        }

        TRect esp00 = FormForm.GetClientRect();
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x0053BDA0)!, esp00);
        TFormForm_DrawForms(FormForm, (TieFormationEnum)AlliedVariables.s_AlliedCurrentFormation);
    }

    // L004CAD68
    private static void TFormForm_Button1Click(FormationBox FormForm)
    {
        AlliedVariables.s_TShipExt_Instance!.FormBox.SelectedIndex = AlliedVariables.s_AlliedCurrentFormation;
        Unit_00513838_Proc_0051E614(0x0E, AlliedVariables.s_AlliedCurrentFormation);
        //FormForm.Close();
    }

    // L004CADA0
    private static void TFormForm_Button2Click(FormationBox FormForm)
    {
    }

    // L004CADAC
    private static void TFormForm_CheckBox1Click(FormationBox FormForm)
    {
        TFormForm_DrawForms(FormForm, (TieFormationEnum)AlliedVariables.s_AlliedCurrentFormation);
        AlliedVariables.s_V0x0053BDA4 = FormForm.CheckBox1.IsChecked == true;
    }
}
