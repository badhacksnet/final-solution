using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNESTest3
{
    public partial class metatileEditor : Form
    {
        public metatileEditor()
        {
            InitializeComponent();
        }

        //bitmap stuff
        static Bitmap loMetatileBrowser = new Bitmap(128, 128);
        Graphics loMetatileBrowserGfx = Graphics.FromImage(loMetatileBrowser);
        static Bitmap loMetatileViewer = new Bitmap(16, 16);
        Graphics loMetatileViewerGfx = Graphics.FromImage(loMetatileViewer);
        static Bitmap tile = new Bitmap(8, 8);
        Graphics tileGfx = Graphics.FromImage(tile);
        static Bitmap tileBrowser = new Bitmap(128, 256);
        Graphics tileBrowserGfx = Graphics.FromImage(tileBrowser);
        static Bitmap hiMetatile = new Bitmap(32, 32);
        Graphics hiMetatileGfx = Graphics.FromImage(hiMetatile);

        //metatile editor variables
        bool negHi;
        bool negLo;
        int loMetatileBankOffsetNeg;
        int loMetatileBankOffsetPos;
        int hiMetatileBankOffset;

        int loMetatileSelection;
        int selectedTile = 0;

        int loMetatileBrowserHover;
        int tileBrowserHover;
        int hiMetatileHover;
        int loMetatileHover;

        MetatileEditorHelp helpForm = new MetatileEditorHelp();



        //integers for tileIDs in loMetatile data
        int tile0 = 0;
        int tile1 = 0;
        int tile2 = 0;
        int tile3 = 0;



        private void metatileEditor_Load(object sender, EventArgs e)
        {
            //This sets the loMetatile bank depending on the high bit of the hiMetatile.
            //For negative loMetatile values (d15 == 1) the bank index is -0x8000 of the actual location so the offset will work

            negHi = Convert.ToBoolean(MainForm.hiMetatileEditSelection & 0x8000);
            if (negHi == true)
            {
                this.Text = "Metatile Editor - NEGATIVE BANK";
            }
            else
            {
                this.Text = "Metatile Editor - POSITIVE BANK";
            }
            
            groupboxHiMetatile.Text = "hiMetatile " + MainForm.formatHexAddress(MainForm.hiMetatileEditSelection, false);

            if (negHi == true)
            {
                loMetatileBankOffsetNeg = 0x58000;
                loMetatileBankOffsetPos = 0x70000;
                hiMetatileBankOffset = 0x60000;
            }
            else
            {
                loMetatileBankOffsetNeg = 0x108000;
                loMetatileBankOffsetPos = 0x118000;
                hiMetatileBankOffset = 0x108000;
            }

            drawTileBrowser();
            drawLoMetatileBrowser();
            drawHiMetatilePanel();
            drawTilePanel();

            //set loMetatileSelection to the first loMetatile in the selected hiMetatile
            loMetatileSelection = (Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection)) << 8)
                | Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + 1));

            drawLoMetatilePanel();

        }

        private void drawTileBrowser()
        {
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    MainForm.drawSingleTile(
                        (Convert.ToInt32(tileBrowserPageSelector.Value) * 512) + x + (y * 16),
                        Convert.ToInt32(tileBrowserPaletteSelector.Value),
                        MainForm.paletteVram.palette,
                        MainForm.vram);
                    tileBrowserGfx.DrawImage(MainForm.singleTile, (x * 8), (y * 8));
                }
            }
            tileBrowserPanel.Refresh();
        }

        private void drawTilePanel()
        {
            groupBoxSelectedTile.Text = "Selected Tile " + MainForm.formatHexAddress(selectedTile, false);

            MainForm.drawSingleTile(selectedTile, Convert.ToInt32(selectedTilePaletteSelector.Value), MainForm.paletteVram.palette, MainForm.vram);
            if (checkSelectedTileFlipUD.Checked == true)
            {
                MainForm.singleTile.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            if (checkSelectedTileFlipLR.Checked == true)
            {
                MainForm.singleTile.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            tileGfx.DrawImage(MainForm.singleTile, 0, 0);
            tileViewer.Refresh();
        }

        private void drawHiMetatilePanel()
        {
            //redraws the hiMetatile panel at top center of the editor, for use on form load or whenever a metatile is edited
            MainForm.drawHiMetatile(MainForm.hiMetatileEditSelection);
            hiMetatileViewer.Refresh(); 

        }

        private void drawLoMetatileBrowser()
            //draws the LoMetatile Browser panel, accounting for the selected page
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    MainForm.drawLoMetatile(negHi, (Convert.ToInt32(loMetatilePageSelector.Value) * 512) + (x * 8) + (y * 64));
                    loMetatileBrowserGfx.DrawImage(MainForm.loMetatile, (x * 16), (y * 16));
                    //metatileBrowserPanel.Refresh();
                }
            }
            loMetatileBrowserPanel.Refresh();
        }

        private void drawLoMetatilePanel()
            //draws the LoMetatile editing panel in the middle of the editor window
        {
            groupBoxLoMetatile.Text = "loMetatile " + MainForm.formatHexAddress(loMetatileSelection, false);
            MainForm.drawLoMetatile(negHi, loMetatileSelection);
            loMetatileViewerGfx.DrawImage(MainForm.loMetatile, 0, 0);
            loMetatileViewerPanel.Refresh();

            updateLoMetatileControls();
        }

        private void updateLoMetatileControls()
        {
            /*
            Bit 0-9   - Character Number (000h-3FFh)
            Bit 10-12 - Palette Number   (0-7)
            Bit 13    - BG Priority      (0=Lower, 1=Higher)
            Bit 14    - X-Flip           (0=Normal, 1=Mirror horizontally)
            Bit 15    - Y-Flip           (0=Normal, 1=Mirror vertically)
            */

            tile0 = getTileDataFromLoMetatile(loMetatileSelection, 0) & 0x3FF;
            tile1 = getTileDataFromLoMetatile(loMetatileSelection, 1) & 0x3FF;
            tile2 = getTileDataFromLoMetatile(loMetatileSelection, 2) & 0x3FF;
            tile3 = getTileDataFromLoMetatile(loMetatileSelection, 3) & 0x3FF;

            labelTile0.Text = MainForm.formatHexAddress(tile0, false);
            labelTile1.Text = MainForm.formatHexAddress(tile1, false);
            labelTile2.Text = MainForm.formatHexAddress(tile2, false);
            labelTile3.Text = MainForm.formatHexAddress(tile3, false);

            tile0PaletteSelector.Value = (getTileDataFromLoMetatile(loMetatileSelection, 0) & 0x1C00) >> 10;
            tile1PaletteSelector.Value = (getTileDataFromLoMetatile(loMetatileSelection, 1) & 0x1C00) >> 10;
            tile2PaletteSelector.Value = (getTileDataFromLoMetatile(loMetatileSelection, 2) & 0x1C00) >> 10;
            tile3PaletteSelector.Value = (getTileDataFromLoMetatile(loMetatileSelection, 3) & 0x1C00) >> 10;

            checkTile0Priority.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 0) & 0x2000);
            checkTile1Priority.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 1) & 0x2000);
            checkTile2Priority.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 2) & 0x2000);
            checkTile3Priority.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 3) & 0x2000);

            checkTile0FlipUD.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 0) & 0x8000);
            checkTile1FlipUD.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 1) & 0x8000);
            checkTile2FlipUD.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 2) & 0x8000);
            checkTile3FlipUD.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 3) & 0x8000);

            checkTile0FlipLR.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 0) & 0x4000);
            checkTile1FlipLR.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 1) & 0x4000);
            checkTile2FlipLR.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 2) & 0x4000);
            checkTile3FlipLR.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, 3) & 0x4000);
        }

        private void loMetatileSettingChanged(int quadrant)
        {
            //debug
            //MessageBox.Show("loMetatileSettingsChanged quadrant = " + quadrant.ToString());

            //Converts the controls from a quadrant of the loMetatile editor to be sent to updateLoMetatileQuadrant for processing
            int tileID = 0;
            int palette = 0;
            bool priority = false;
            bool udFlip = false;
            bool lrFlip = false;

            if (quadrant == 0)
            {
                tileID = tile0;
                palette = Convert.ToInt32(tile0PaletteSelector.Value);
                priority = checkTile0Priority.Checked;
                udFlip = checkTile0FlipUD.Checked;
                lrFlip = checkTile0FlipLR.Checked;
            }
            else if (quadrant == 1)
            {
                tileID = tile1;
                palette = Convert.ToInt32(tile1PaletteSelector.Value);
                priority = checkTile1Priority.Checked;
                udFlip = checkTile1FlipUD.Checked;
                lrFlip = checkTile1FlipLR.Checked;
            }
            else if (quadrant == 2)
            {
                tileID = tile2;
                palette = Convert.ToInt32(tile2PaletteSelector.Value);
                priority = checkTile2Priority.Checked;
                udFlip = checkTile2FlipUD.Checked;
                lrFlip = checkTile2FlipLR.Checked;
            }
            else if (quadrant == 3)
            {
                tileID = tile3;
                palette = Convert.ToInt32(tile3PaletteSelector.Value);
                priority = checkTile3Priority.Checked;
                udFlip = checkTile3FlipUD.Checked;
                lrFlip = checkTile3FlipLR.Checked;
            }

            updateLoMetatileQuadrant(tileID, palette, priority, udFlip, lrFlip, quadrant);
        }

        private void updateLoMetatileQuadrant(int tileID, int palette, bool priority, bool udFlip, bool lrFlip, int quadrant)
        {
            bool negLo = Convert.ToBoolean(loMetatileSelection & 0x8000);
            int offsetLo = loMetatileBankOffsetPos;
            if (negLo == true)
            {
                offsetLo = loMetatileBankOffsetNeg;
            }

            MainForm.romArray[offsetLo + loMetatileSelection + (quadrant * 2)] = assembleBGTile(tileID, palette, priority, udFlip, lrFlip, true);
            MainForm.romArray[offsetLo + loMetatileSelection + (quadrant * 2) + 1] = assembleBGTile(tileID, palette, priority, udFlip, lrFlip, false);

            drawLoMetatilePanel();
            drawLoMetatileBrowser();
            drawHiMetatilePanel();
        }

        private byte assembleBGTile(int tileID, int palette, bool priority, bool udFlip, bool lrFlip, bool loByte)
        {
            //combines a set of tile controls (either from loMetatile controls or tile controls) into a little-endian BG tile
            int priorityInt = 0;
            int udFlipInt = 0;
            int lrFlipInt = 0;

            if (lrFlip == true)
            {
                lrFlipInt = 0x4000;
            }

            if (udFlip == true)
            {
                udFlipInt = 0x8000;
            }

            if (priority == true)
            {
                priorityInt = 0x2000;
            }

            int bgTile = tileID
                | (palette << 10)
                | priorityInt
                | udFlipInt
                | lrFlipInt;

            if (loByte == true)
            {
                bgTile = bgTile & 0xFF;
            }
            else
            {
                bgTile = (bgTile & 0xFF00) >> 8;
            }

            return Convert.ToByte(bgTile);

            //return ((bgTile & 0xFF) << 8) | ((bgTile & 0xFF00) >> 8);
        }

        private int getTileDataFromLoMetatile(int loID, int quadrant)
        {
            bool neg = Convert.ToBoolean(loID & 0x8000);
            int finalOffset = loID + (quadrant * 2);

            if (neg == true)
            {
                finalOffset = finalOffset + loMetatileBankOffsetNeg;
            }
            else
            {
                finalOffset = finalOffset + loMetatileBankOffsetPos;
            }

            return ((Convert.ToInt32(MainForm.romArray.GetValue(finalOffset + 1)) << 8) | Convert.ToInt32(MainForm.romArray.GetValue(finalOffset)));

        }

        private void metatileLoBrowser_MouseMove(object sender, MouseEventArgs e)
        {
            //loMetatileBrowserHover = (Convert.ToInt32(MainForm.romArray.GetValue((loMetatileBankOffset + 1) + Convert.ToInt32(loMetatilePageSelector.Value * 128 + (((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2)))) << 8) | Convert.ToInt32(MainForm.romArray.GetValue(loMetatileBankOffset + Convert.ToInt32(loMetatilePageSelector.Value * 128 + (((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2))));
            //bgMapPanelHover = (Convert.ToInt32(bgMapSelector.Value) * 512) + ((e.Location.Y / 64) * 64) + ((e.Location.X / 64) * 8);
            loMetatileBrowserHover = (Convert.ToInt32(loMetatilePageSelector.Value) * 512) + ((e.Location.Y / 64) * 64) + ((e.Location.X / 64) * 8);
            loMetatileBrowserHoverDisplay.Text = MainForm.formatHexAddress(loMetatileBrowserHover, false);
        }

        private void loMetatileViewerPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(loMetatileViewer, 0, 0, 128, 128);
        }

        private void loMetatileViewerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            loMetatileHover = (e.Location.X / 64) + ((e.Location.Y / 64) * 2);
            labelDebugLoMetatileHover.Text = "loMetatile Hover = " + loMetatileHover.ToString();
        }

        private void loMetatileBrowserPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(loMetatileBrowser, 0, 0, 512, 512);
        }

        private void loMetatileBrowserPanel_MouseClick(object sender, MouseEventArgs e)
        {
            loMetatileSelection = loMetatileBrowserHover;
            drawLoMetatilePanel();
        }

        private void hiMetatileViewer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(MainForm.hiMetatile, 0, 0, 128, 128);
        }

        private void loMetatilePageSelector_ValueChanged(object sender, EventArgs e)
        {
            drawLoMetatileBrowser();
        }

        private void tileBrowserPaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            drawTileBrowser();
        }

        private void tileBrowserPageSelector_ValueChanged(object sender, EventArgs e)
        {
            drawTileBrowser();
        }

        private void tileBrowserPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(tileBrowser, 0, 0, 256, 512);
        }

        private void tileBrowserPanel_MouseMove(object sender, MouseEventArgs e)
        {
            tileBrowserHover = (Convert.ToInt32(tileBrowserPageSelector.Value) * 512) + ((e.Location.Y / 16) * 16) + (e.Location.X / 16);
            tileBrowserHoverDisplay.Text = MainForm.formatHexAddress(tileBrowserHover, false);
        }

        private void tileBrowserPanel_MouseLeave(object sender, EventArgs e)
        {
            tileBrowserHoverDisplay.Text = "";
        }

        private void loMetatileBrowserPanel_MouseLeave(object sender, EventArgs e)
        {
            loMetatileBrowserHoverDisplay.Text = "";
        }

        private void hiMetatileViewer_MouseMove(object sender, MouseEventArgs e)
        {
            hiMetatileHover = ((e.Location.X / 64) + ((e.Location.Y / 64) * 2));
            //OLD hiMetatileHover = (Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + (((e.Location.X / 64) + ((e.Location.Y / 64) * 2)) * 2))) << 8 ) | Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + 1 + (((e.Location.X / 64) + ((e.Location.Y / 64) * 2)) * 2)));
            hiMetatileHoverDisplay.Text = MainForm.formatHexAddress((Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + (hiMetatileHover * 2))) << 8) | Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + 1 + (hiMetatileHover * 2))), false);
        }

        private void hiMetatileViewer_MouseLeave(object sender, EventArgs e)
        {
            hiMetatileHoverDisplay.Text = "";
        }

        private void hiMetatileViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                loMetatileSelection = (Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + (hiMetatileHover * 2))) << 8) | Convert.ToInt32(MainForm.romArray.GetValue(hiMetatileBankOffset + MainForm.hiMetatileEditSelection + 1 + (hiMetatileHover * 2)));
                drawLoMetatilePanel();
            }
            if (e.Button == MouseButtons.Right)
            {
                MainForm.romArray[hiMetatileBankOffset + MainForm.hiMetatileEditSelection + (hiMetatileHover * 2)] = Convert.ToByte((loMetatileSelection & 0xFF00) >> 8);
                MainForm.romArray[hiMetatileBankOffset + MainForm.hiMetatileEditSelection + (hiMetatileHover * 2) + 1] = Convert.ToByte(loMetatileSelection & 0xFF);
                drawHiMetatilePanel();
            }
        }

        private void tileBrowserPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectedTile = tileBrowserHover;
                selectedTilePaletteSelector.Value = tileBrowserPaletteSelector.Value;
                checkSelectedTileFlipLR.Checked = false;
                checkSelectedTileFlipUD.Checked = false;
                checkSelectedTilePriority.Checked = false;
                drawTilePanel();
            }
            if (e.Button == MouseButtons.Right)
            {
                selectedTile = tileBrowserHover;
                drawTilePanel();
            }
        }

        private void selectedTilePaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            drawTilePanel();
        }

        private void checkSelectedTileFlipLR_Click(object sender, EventArgs e)
        {
            drawTilePanel();
        }

        private void checkSelectedTilePriority_Click(object sender, EventArgs e)
        {
            drawTilePanel();
        }

        private void checkSelectedTileFlipUD_Click(object sender, EventArgs e)
        {
            drawTilePanel();
        }

        private void tileViewer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(tile, 0, 0, 64, 64);
        }

        private void loMetatileViewerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectedTile = getTileDataFromLoMetatile(loMetatileSelection, loMetatileHover) & 0x3FF;
                selectedTilePaletteSelector.Value = (getTileDataFromLoMetatile(loMetatileSelection, loMetatileHover) & 0x1C00) >> 10;
                checkSelectedTilePriority.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, loMetatileHover) & 0x2000);
                checkSelectedTileFlipUD.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, loMetatileHover) & 0x8000);
                checkSelectedTileFlipLR.Checked = Convert.ToBoolean(getTileDataFromLoMetatile(loMetatileSelection, loMetatileHover) & 0x4000);
                drawTilePanel();
            }
            if(e.Button == MouseButtons.Right)
            {
                /*bool negLo = Convert.ToBoolean(loMetatileSelection & 0x8000);
                int offsetLo = loMetatileBankOffsetPos;
                if (negLo == true)
                {
                    offsetLo = loMetatileBankOffsetNeg;
                }

                MainForm.romArray[offsetLo + loMetatileSelection + (loMetatileHover * 2)] = assembleBGTile(selectedTile, Convert.ToInt32(selectedTilePaletteSelector.Value), checkSelectedTilePriority.Checked, checkSelectedTileFlipUD.Checked, checkSelectedTileFlipLR.Checked, true);
                MainForm.romArray[offsetLo + loMetatileSelection + (loMetatileHover * 2) + 1] = assembleBGTile(selectedTile, Convert.ToInt32(selectedTilePaletteSelector.Value), checkSelectedTilePriority.Checked, checkSelectedTileFlipUD.Checked, checkSelectedTileFlipLR.Checked, false);
                drawLoMetatilePanel();
                updateLoMetatileControls();
                drawLoMetatileBrowser();*/
                updateLoMetatileQuadrant(selectedTile, Convert.ToInt32(selectedTilePaletteSelector.Value), checkSelectedTilePriority.Checked, checkSelectedTileFlipUD.Checked, checkSelectedTileFlipLR.Checked, loMetatileHover);
            }
        }

        private void tile0PaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            if (tile0PaletteSelector == this.ActiveControl)
            {
                loMetatileSettingChanged(0);
            }
            //loMetatileSettingChanged(0);
        }

        private void checkTile0Priority_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(0);
        }

        private void checkTile0FlipUD_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(0);
        }

        private void checkTile0FlipLR_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(0);
        }

        private void tile1PaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            if (tile1PaletteSelector == this.ActiveControl)
            {
                loMetatileSettingChanged(1);
            }
            //loMetatileSettingChanged(1);
        }

        private void checkTile1Priority_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(1);
        }

        private void checkTile1FlipUD_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(1);
        }

        private void checkTile1FlipLR_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(1);
        }

        private void tile2PaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            if (tile2PaletteSelector == this.ActiveControl)
            {
                loMetatileSettingChanged(2);
            }
            //loMetatileSettingChanged(2);
        }

        private void checkTile2Priority_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(2);
        }

        private void checkTile2FlipUD_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(2);
        }

        private void checkTile2FlipLR_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(2);
        }

        private void tile3PaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            if (tile3PaletteSelector == this.ActiveControl)
            {
                loMetatileSettingChanged(3);
            }
            //loMetatileSettingChanged(3);
        }

        private void checkTile3Priority_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(3);
        }

        private void checkTile3FlipUD_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(3);
        }

        private void checkTile3FlipLR_Click(object sender, EventArgs e)
        {
            //loMetatileSettingChanged(3);
        }

        private void metatileEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void buttonMTHelp_Click(object sender, EventArgs e)
        {
            helpForm.ShowDialog();
        }

        private void checkTile0Priority_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile0Priority == this.ActiveControl)
            {
                loMetatileSettingChanged(0);
            }
        }

        private void checkTile0FlipUD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile0FlipUD == this.ActiveControl)
            {
                loMetatileSettingChanged(0);
            }
        }

        private void checkTile0FlipLR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile0FlipLR == this.ActiveControl)
            {
                loMetatileSettingChanged(0);
            }
        }

        private void checkTile1Priority_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile1Priority == this.ActiveControl)
            {
                loMetatileSettingChanged(1);
            }
        }

        private void checkTile1FlipUD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile1FlipUD == this.ActiveControl)
            {
                loMetatileSettingChanged(1);
            }
        }

        private void checkTile1FlipLR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile1FlipLR == this.ActiveControl)
            {
                loMetatileSettingChanged(1);
            }
        }

        private void checkTile2Priority_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile2Priority == this.ActiveControl)
            {
                loMetatileSettingChanged(2);
            }
        }

        private void checkTile2FlipUD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile2FlipUD == this.ActiveControl)
            {
                loMetatileSettingChanged(2);
            }
        }

        private void checkTile2FlipLR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile2FlipLR == this.ActiveControl)
            {
                loMetatileSettingChanged(2);
            }
        }

        private void checkTile3Priority_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile3Priority == this.ActiveControl)
            {
                loMetatileSettingChanged(3);
            }
        }

        private void checkTile3FlipUD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile3FlipUD == this.ActiveControl)
            {
                loMetatileSettingChanged(3);
            }
        }

        private void checkTile3FlipLR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTile3FlipLR == this.ActiveControl)
            {
                loMetatileSettingChanged(3);
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            helpForm.ShowDialog();
        }
    }
}
