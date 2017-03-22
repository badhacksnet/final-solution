using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNESTest3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //romArray is our currently loaded ROM. Hardcoded at the moment for testing.
        public static byte[] romArray = new byte[0x2000000];
        public static byte[] vram = new byte[0x10000];
        byte[] vramRaw = new byte[0x8000];
        static byte[] bgPalettes = new byte[0x100];
        //pcx test stuff
        byte[] vramBitplanes = new byte[32768];
        List<byte> encodedBitplanes;

        //Table of binary values for using with AND in tile conversion
        static int[] andTable = new int[8] { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };

        //defining BGMapPresets with level name, levelID, and sublevelID
        BGMapPreset slums0 = new BGMapPreset("00-00 (Slums)", 0x0, 0x0);
        BGMapPreset slums1 = new BGMapPreset("00-01 (Slums)", 0x0, 0x1);
        BGMapPreset slums2 = new BGMapPreset("00-02 (Slums)", 0x0, 0x2);
        BGMapPreset subway0 = new BGMapPreset("01-00 (Subway)", 0x1, 0x0);
        BGMapPreset subway1 = new BGMapPreset("01-01 (Subway)", 0x1, 0x1);
        BGMapPreset subway2 = new BGMapPreset("01-02 (Subway)", 0x1, 0x2);
        BGMapPreset subway3 = new BGMapPreset("01-03 (Subway)", 0x1, 0x3);
        BGMapPreset westside0 = new BGMapPreset("02-00 (Westside)", 0x2, 0x0);
        BGMapPreset westside1 = new BGMapPreset("02-01 (Westside)", 0x2, 0x1);
        BGMapPreset westside2 = new BGMapPreset("02-02 (Westside)", 0x2, 0x2);
        /*Industrial Area (03-00, 03-01) does not use the standard tile load routine and cannot be included.
        Even if it did, it simply uses the same batch offset as 00-00*/
        //BGMapPreset industrial0 = new BGMapPreset("03-00 (Industrial Area)", 0x3, 0x0);
        //BGMapPreset industrial1 = new BGMapPreset("03-01 (Industrial Area)", 0x3, 0x1);
        BGMapPreset bayarea0 = new BGMapPreset("04-00 (Bay Area)", 0x4, 0x0);
        BGMapPreset uptown0 = new BGMapPreset("05-00 (Uptown)", 0x5, 0x0);
        BGMapPreset uptown1 = new BGMapPreset("05-01 (Uptown)", 0x5, 0x1);
        BGMapPreset uptown2 = new BGMapPreset("05-02 (Uptown)", 0x5, 0x2);
        BGMapPreset uptown3 = new BGMapPreset("05-03 (Uptown)", 0x5, 0x3);
        BGMapPreset intro1 = new BGMapPreset("08-04 (Intro 1)", 0x8, 0x4);
        BGMapPreset intro2 = new BGMapPreset("08-00 (Intro 2)", 0x8, 0x0);
        BGMapPreset charsel = new BGMapPreset("08-01 (Character Select)", 0x8, 0x1);
        BGMapPreset title = new BGMapPreset("0A-00 (Title Screen)", 0xA, 0x0);

        //Graphics and bitmap objects
        public static Bitmap singleTile = new Bitmap(8, 8);
        public static Graphics singleTileGfx = Graphics.FromImage(singleTile);
        public static Bitmap loMetatile = new Bitmap(16, 16);
        public static Graphics loMetatileGfx = Graphics.FromImage(loMetatile);
        public static Bitmap hiMetatile = new Bitmap(32, 32);
        public static Graphics hiMetatileGfx = Graphics.FromImage(hiMetatile);
        public static Bitmap bgMap = new Bitmap(256, 256);
        public static Graphics bgMapGfx = Graphics.FromImage(bgMap);
        public static Bitmap vramFullView = new Bitmap(128, 512);
        public static Graphics vramFullViewGfx = Graphics.FromImage(vramFullView);
        public static Bitmap hiMetatileBrowser = new Bitmap(256, 256);
        public static Graphics hiMetatileBrowserGfx = Graphics.FromImage(hiMetatileBrowser);
        public static Bitmap hiMetatileSelection1Bitmap = new Bitmap(32, 32);
        public static Graphics hiMetatileSelection1BitmapGfx = Graphics.FromImage(hiMetatileSelection1Bitmap);
        public static Bitmap hiMetatileSelection2Bitmap = new Bitmap(32, 32);
        public static Graphics hiMetatileSelection2BitmapGfx = Graphics.FromImage(hiMetatileSelection2Bitmap);

        //ROM specific offsets
        //using DF'89 settings by default
        //metatile banks
        int offsetHiMetatileNeg = 0x60000;
        int offsetHiMetatilePos = 0x108000;
        int offsetNegLoMetatileNeg = 0x58000;
        int offsetNegLoMetatilePos = 0x70000;
        int offsetPosLoMetatileNeg = 0x108000;
        int offsetPosLoMetatilePos = 0x118000;
        //palette construction offsets

        //editor variables
        int hiMetatile1Selection = 0x8000;
        int hiMetatile2Selection = 0x8000;
        int hiMetatileBrowserHover;
        int bgMapPanelHover;
        public static int hiMetatileEditSelection = 0x8000;

        //other forms
        metatileEditor mte = new metatileEditor();
        MainHelp help = new MainHelp();

        //SnesPalette paletteVramTest = new SnesPalette(romArray, 0x3EE20);
        public static SnesPalette paletteVram = new SnesPalette(bgPalettes, 0);
        //DirectBitmap vramBitmap = new DirectBitmap(128, 512);

        //ROM opening & saving
        string selectedROM;
        bool recentlySaved = true;


        //about form
        AboutForm about = new AboutForm();

        private void Form1_Load(object sender, EventArgs e)
        {
            openROM.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            openROM.Filter = "Death Fuck '89 ROM|*.smc";
            openROM.RestoreDirectory = true;
            saveROM.Filter = "Death Fuck '89 ROM|*.smc";
 

        }

        private void loadedROM(string fileName)
        {
            romArray = File.ReadAllBytes(fileName);
            //toolStripButton1.Enabled = true;
            assetComboBox.Enabled = true;
            populateAssetComboBox();
            bgMapSelector.Enabled = true;
            bgMapSelector.Value = 1;
            //btnSelectBGMap.Enabled = true;
            //btnPageDown.Enabled = true;
            //btnPageUp.Enabled = true;
            metatilePageSelector.Enabled = true;
            metatilePageSelector.Value = 0x40;
            selectAsset();
            selectBGMap(Convert.ToInt32(bgMapSelector.Value));
            updateHiMetatileSelection();

            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            bgMapPanel.Enabled = true;
            metatileBrowserPanel.Enabled = true;
            bgMapSelector.Enabled = true;
            metatilePageSelector.Enabled = true;
            hiMetatileSelection1Panel.Enabled = true;

            
        }

        private void populateAssetComboBox()
        {
            //populate asset combo box
            assetComboBox.Items.Add(slums0);
            assetComboBox.Items.Add(slums1);
            assetComboBox.Items.Add(slums2);
            assetComboBox.Items.Add(subway0);
            assetComboBox.Items.Add(subway1);
            assetComboBox.Items.Add(subway2);
            assetComboBox.Items.Add(subway3);
            assetComboBox.Items.Add(westside0);
            assetComboBox.Items.Add(westside1);
            assetComboBox.Items.Add(westside2);
            //assetComboBox.Items.Add(industrial0);
            //assetComboBox.Items.Add(industrial1);
            assetComboBox.Items.Add(bayarea0);
            assetComboBox.Items.Add(uptown0);
            assetComboBox.Items.Add(uptown1);
            assetComboBox.Items.Add(uptown2);
            assetComboBox.Items.Add(uptown3);
            //assetComboBox.Items.Add(intro1);
            //assetComboBox.Items.Add(intro2);
            //assetComboBox.Items.Add(title);
            //assetComboBox.Items.Add(charsel);

            assetComboBox.SelectedIndex = 0;
        }

        public static string formatHexAddress(int n, bool is8bit)
        {
            //feed me an integer such as a metatile ID, and I will return a string formatted like 0x01A5 or 0xFF
            //set the bool to true to return only an 8bit value like 0xFF
            string s = "";
            n = n & 0xFFFF;

            if (is8bit == true)
            {
                n = n & 0xFF;
                s = n.ToString("X");
            }
            else
            {
                if (n <= 0x0F)
                {
                    s = "000" + n.ToString("X");
                }
                else if (n <= 0xFF)
                {
                    s = "00" + n.ToString("X");
                }
                else if (n <= 0xFFF)
                {
                    s = "0" + n.ToString("X");
                }
                else
                {
                    s = n.ToString("X");
                }
            }

            return "0x" + s;
        }

        private void drawVram()
        {
            vramFullView = new Bitmap(128, 512);
            vramFullViewGfx = Graphics.FromImage(vramFullView);
            for (int y = 0; y < 64; y++)
            {
                for(int x = 0; x < 16; x++)
                {
                    drawSingleTile((y * 16) + x, Convert.ToInt32(tileTestPaletteSelector.Value), paletteVram.palette, vram);
                    vramFullViewGfx.DrawImage(singleTile, (x * 8), (y * 8));
                }
            }
        }

        private void savePCXData()
        {
            convertVramToPCXBitplanes(vram);
            encodedBitplanes = new List<byte>();
            encodePCXBitplanes();
            
            using (FileStream fs = new FileStream("test.bin", FileMode.Create, FileAccess.Write))
            {
                fs.Write(encodedBitplanes.ToArray(), 0, encodedBitplanes.Count);
                fs.Flush();
                fs.Close();
            }
        }

        private void savePCXData2()
        {
            convertVramToPCXBitplanes(vram);
            encodedBitplanes = new List<byte>();
            encodePCXBitplanes2();

            using (FileStream fs = new FileStream("test2.bin", FileMode.Create, FileAccess.Write))
            {
                fs.Write(encodedBitplanes.ToArray(), 0, encodedBitplanes.Count);
                fs.Flush();
                fs.Close();
            }
        }

        private void convertVramToPCXBitplanes(byte[] imageInput)
        {
            //test for saving VRAM tilesheet to a pcx file using currently selected palette from debug controls
            //start by converting the current vram tilesheet to uncompressed bitplanes
            //vram image is 128x512, 1 byte per pixel
            //16 bytes per linear bitplane
            //64 bytes per pixel scanline at 4bpp
            //512 * 64 = 32768
            for (int scanline = 0; scanline < 512; scanline++)
            {
                for (int bitplane = 0; bitplane < 4; bitplane++)
                {
                    for (int gridTile = 0; gridTile < 16; gridTile++)
                    {
                      vramBitplanes[(scanline * 64) + (bitplane * 16) + gridTile] = compilePCXBitplaneByte(scanline,bitplane,gridTile);
                    }
                }
            }
        }

        private byte compilePCXBitplaneByte(int scanline, int bitplane, int gridTile)
        {
            int b = 0;

            for (int pixel = 0; pixel < 8; pixel++)
            {
                b = b | 
                    (((Convert.ToInt32(vram.GetValue((scanline * 128) + (gridTile * 8) + pixel))
                    & (0x01 << bitplane))
                    >> bitplane) 
                    << (pixel ^ 0x7));
            }

            return Convert.ToByte(b);
        }

        private void encodePCXBitplanes2()
        {
            //cheesier version of encodePCXBitplanes that doesn't bother with RLE apart from bytes where (byte) & 0xC0 == 0xC0
            for (int bytecount = 0; bytecount < 0x8000; bytecount++)
            {
                int b = Convert.ToInt32(vramBitplanes.GetValue(bytecount));

                if((b & 0xC0) == 0xC0)
                {
                    encodedBitplanes.Add(0xC1);
                    encodedBitplanes.Add(Convert.ToByte(b));
                }
                else
                {
                    encodedBitplanes.Add(Convert.ToByte(b));
                }
            
            }
        }

        private void encodePCXBitplanes()
        {
            for (int scanline = 0; scanline < 512; scanline++)
            {
                for (int bitplane = 0; bitplane < 4; bitplane++)
                {
                    int bytecount = 0;
                    while (bytecount < 16)
                    {
                        int current = Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + bytecount));

                        //check if the current byte is the same as the next byte and we are not on the last byte of the bitplane
                        if (bytecount < 15 && current == Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + bytecount + 1)))
                        {
                            //current byte is same as next byte. Set the repeat value to 1 (as is currently known) and see how many other bytes in this bitplane == current
                            //increment the bytecount with each byte confirmed == current
                            int repCount = 1;
                            bytecount++;

                            while (bytecount < 15 && current == Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + repCount + 1)))
                            {
                                //keep incrementing the repcount and bytecount for each bitplane byte that == current
                                repCount++;
                                bytecount++;
                            }

                            encodedBitplanes.Add(Convert.ToByte(0xC0 | repCount));
                            encodedBitplanes.Add(Convert.ToByte(current));

                        }
                        else
                        //current byte != next byte
                        {
                            if ((current & 0xC0) == 0xC0)
                            {
                                //2 most significant bits are set
                                //include a RLE control byte with a repcount of 1 to prevent this byte from being misinterpreted as a control byte
                                encodedBitplanes.Add(Convert.ToByte(current));
                                bytecount++;
                            }
                            else
                            {
                                //one or neither of the MSBs are set
                                encodedBitplanes.Add(0xC1);
                                encodedBitplanes.Add(Convert.ToByte(current));
                                bytecount++;
                            }
                        }
                    }
                }
            }
        }


        /* quarantined
        private void encodePCXBitplanes()
        {
            for (int scanline = 0; scanline < 512; scanline++)
            {
                for (int bitplane = 0; bitplane < 4; bitplane++)
                {
                    int bytecount = 0;
                    while (bytecount < 16)
                    {
                        int current = Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + bytecount));

                        //check if the current byte is the same as the next byte and we are not on the last byte of the bitplane
                        if (current == Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + bytecount + 1)) && bytecount < 15)
                        {
                            //current byte is same as next byte. Set the repeat value to 1 (as is currently known) and see how many other bytes in this bitplane == current
                            //increment the bytecount with each byte confirmed == current
                            int repCount = 1;
                            bytecount++;

                            while(current == Convert.ToInt32(vramBitplanes.GetValue((scanline * 64) + (bitplane * 16) + repCount + 1)) && bytecount < 15)
                            {
                                //keep incrementing the repcount and bytecount for each bitplane byte that == current
                                repCount++;
                                bytecount++;
                            }

                            encodedBitplanes.Add(Convert.ToByte(0xC0 | repCount));
                            encodedBitplanes.Add(Convert.ToByte(current));

                        }
                        else
                        //current byte != next byte
                        {
                            if ((current & 0xC0) == 0xC0)
                            {
                                //2 most significant bits are set
                                //include a RLE control byte with a repcount of 1 to prevent this byte from being misinterpreted as a control byte
                                encodedBitplanes.Add(Convert.ToByte(current));
                                bytecount++;
                            }
                            else
                            {
                                //one or neither of the MSBs are set
                                encodedBitplanes.Add(0xC1);
                                encodedBitplanes.Add(Convert.ToByte(current));
                                bytecount++;
                            }
                        }
                    }
                }
            }
        }

            */
        public static void drawSingleTile(int tileID, int palID, string[] palettes, byte[] tileData)
        {

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Brush pixel = new SolidBrush(ColorTranslator.FromHtml(Convert.ToString(palettes.GetValue((palID * 16) + Convert.ToInt32(tileData.GetValue((tileID * 64) + (y * 8) + x))))));
                    singleTileGfx.FillRectangle(pixel, x, y, 1, 1);
                }
            }
        }

        /*private unsafe Image drawSingleTile2(int tileID, int palID, string[] palettes, byte[] tileData)
        {
            //experimental faster version of drawSingleTile
            Bitmap b = new Bitmap(8, 8);
            BitmapData bData = b.LockBits(new Rectangle(0, 0, 8, 8), ImageLockMode.ReadWrite, b.PixelFormat);
            byte bpp = 32;
            byte* scan0 = (byte*)bData.Scan0.ToPointer();

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    byte* data = scan0 + i * bData.Stride + j * bpp / 8;
                }
            }

            b.UnlockBits(bData);

            return b;

        }
        */

        public static void drawLoMetatile(bool negHi, int loID)
        {
            int tableOffset;
            bool negLo = Convert.ToBoolean(loID & 0x8000);

            if (negHi == true)
            {
                if (negLo == true)
                {
                    tableOffset = 0x58000;
                }
                else
                {
                    tableOffset = 0x70000;
                }
            }
            else
            {
                if (negLo == true)
                {
                    tableOffset = 0x108000;
                }
                else
                {
                    tableOffset = 0x118000;
                }
            }

            for(int y = 0; y < 2; y++)
            {
                for(int x = 0; x < 2; x++)
                {
                    int finalOffset = tableOffset + loID + (x * 2) + (y * 4);
                    int palSelect = (Convert.ToInt32(romArray.GetValue(finalOffset + 1)) & 0x1C) >> 2;
                    drawSingleTile(
                        ((Convert.ToInt32(romArray.GetValue(finalOffset + 1)) << 8) | Convert.ToInt32(romArray.GetValue(finalOffset))) & 0x3FF,
                        palSelect,
                        paletteVram.palette,
                        vram
                        );

                    bool flipX = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tableOffset + loID + (x * 2) + (y * 4) + 1)) & 0x80);
                    bool flipY = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tableOffset + loID + (x * 2) + (y * 4) + 1)) & 0x40);
                    if(flipX == true)
                    {
                        singleTile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                    if(flipY == true)
                    {
                        singleTile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    loMetatileGfx.DrawImage(singleTile, (x * 8), (y * 8));

                    //DEBUG MESSAGE BOX
                    /*MessageBox.Show(
                        "loID: " + loID.ToString("X") + Environment.NewLine +
                        "tableOffset: " + tableOffset.ToString("X") + Environment.NewLine +
                        "Final offset: " + finalOffset.ToString("X") + Environment.NewLine +
                        "Palette: " + palSelect.ToString("X")
                        );*/
                }
            }
        }

        public static void drawHiMetatile(int hiID)
        {
            bool negHi = Convert.ToBoolean(hiID & 0x8000);
            int hiOffset;

            if(negHi == true)
            {
                hiOffset = 0x60000;
            }
            else
            {
                hiOffset = 0x108000;
            }

            for(int y = 0; y < 2; y++)
            {
                for(int x = 0; x < 2; x++)
                {
                    int finalOffset = hiOffset + hiID + (x * 2) + (y * 4);
                    drawLoMetatile(negHi,
                        (Convert.ToInt32(romArray.GetValue(finalOffset)) << 8) |
                        (Convert.ToInt32(romArray.GetValue(finalOffset + 1)))
                        );
                    hiMetatileGfx.DrawImage(loMetatile, (x * 16), (y * 16));

                    //DEBUG MESSAGE BOX
                    /*
                    int debugLoID = (Convert.ToInt32(romArray.GetValue(finalOffset + 1)) << 8) |
                        (Convert.ToInt32(romArray.GetValue(finalOffset)));
                    MessageBox.Show(
                        "hiID: " + hiID.ToString("X") + Environment.NewLine +
                        "hiOffset: " + hiOffset.ToString("X") + Environment.NewLine +
                        "Final offset: " + finalOffset.ToString("X") + Environment.NewLine +
                        "loID " + debugLoID.ToString("X")
                        );
                        */
                }
            }
        }

        public static void drawBGMap(int bgID)
        {
            for(int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    int finalOffset = 0x72000 + (bgID * 128) + (x * 2) + (y * 16);
                    drawHiMetatile(
                        (Convert.ToInt32(romArray.GetValue(finalOffset)) << 8) |
                        Convert.ToInt32(romArray.GetValue(finalOffset + 1))
                        );
                    bgMapGfx.DrawImage(hiMetatile, (x * 32), (y * 32));
                }
            }
        }

        private int loRomConversion(int n) /*Converts a LoROM address to a normal address*/
        {
            return n - (0x8000 * (1 + ((n / 0x8000) / 2)));
        }

        private void bgMapListCreate()
        {

        }

        private void tileSolidColor()
        {
            /* DISASSEMBLY OF SOLID COLOR TILE LOAD ROUTINE FROM FINAL FIGHT GUY (J)

          .00:9ABA bgtile_load_getoffset:                  ; CODE XREF: .00:9742p
.00:9ABA                 LDA     #$80 ; 'Ç'
.00:9ABC                 STA     VRAM_ADDRESS_INCREMENT_VALUE
.00:9ABF                 LDA     #0
.00:9AC1                 XBA
.00:9AC2                 LDA     $CB0            ; load levelID*2
.00:9AC5                 ASL
.00:9AC6                 TAX
.00:9AC7                 REP     #$20 ; ' '
.00:9AC9 .A16
.00:9AC9                 LDA     $9BAC,X         ; levelID*2 becomes offest for recursive table 9BAC
.00:9ACD                 STA     $1E
.00:9AD0                 SEP     #$20 ; ' '
.00:9AD2 .A8
.00:9AD2                 LDA     #0
.00:9AD4                 XBA
.00:9AD5                 LDA     $CB1
.00:9AD8                 ASL
.00:9AD9                 TAX
.00:9ADA                 REP     #$20 ; ' '
.00:9ADC .A16
.00:9ADC                 CLC
.00:9ADD                 ADC     $1E             ; sublevelID*2 added to offset
.00:9AE0                 TAX
.00:9AE1                 LDA     $9BAC,X         ; load final offset
.00:9AE5                 TAX
.00:9AE6                 SEP     #$20 ; ' '
.00:9AE8 .A8
.00:9AE8                 LDY     #$2000          ; load Y VRAM write address 0x2000
.00:9AEB                 JSR     bgtile_solid_2x1
.00:9AEE                 LDA     $9BAC,X         ; Tile row 1 data length
.00:9AF2                 BEQ     bgtile_solid_exit
.00:9AF4                 INX
.00:9AF5                 STA     0
.00:9AF8
.00:9AF8 loc_9AF8:                               ; CODE XREF: .00:9AFFj
.00:9AF8                 JSR     bgtile_solid_preload
.00:9AFB                 INX
.00:9AFC                 DEC     0
.00:9AFF                 BNE     loc_9AF8
.00:9B01                 LDY     #$2100          ; First row of color blocks loaded, now do bottom half of 2x2 color block.
.00:9B04                 JSR     bgtile_solid_2x1
.00:9B07                 LDA     $9BAC,X         ; Tile row 2 data length
.00:9B0B                 BEQ     bgtile_solid_exit
.00:9B0D                 INX
.00:9B0E                 STA     0
.00:9B11
.00:9B11 loc_9B11:                               ; CODE XREF: .00:9B18j
.00:9B11                 JSR     bgtile_solid_preload
.00:9B14                 INX
.00:9B15                 DEC     0
.00:9B18                 BNE     loc_9B11
.00:9B1A
.00:9B1A bgtile_solid_exit:                      ; CODE XREF: .00:9AF2j
.00:9B1A                                         ; .00:9B0Bj
.00:9B1A                 RTS
.00:9B1B ; ---------------------------------------------------------------------------
.00:9B1B
.00:9B1B bgtile_solid_2x1:                       ; CODE XREF: .00:9AEBp
.00:9B1B                                         ; .00:9B04p
.00:9B1B                 LDA     #0              ; Forces the next 2 solid color tiles to value stored at 0x00.
.00:9B1D                 JSR     bgtile_solid
.00:9B20                 LDA     #0
.00:9B22                 JSR     bgtile_solid
.00:9B25                 RTS
.00:9B26 ; ---------------------------------------------------------------------------
.00:9B26
.00:9B26 bgtile_solid_preload:                   ; CODE XREF: .00:loc_9AF8p
.00:9B26                                         ; .00:loc_9B11p
.00:9B26                 LDA     $9BAC,X
.00:9B2A                 BPL     bgtile_solid
.00:9B2C                 JSR     bgtile_solid
.00:9B2F                 LDA     $9BAC,X
.00:9B33
.00:9B33 bgtile_solid:                           ; CODE XREF: .00:9B1Dp
.00:9B33                                         ; .00:9B22p ...
.00:9B33                 STY     ADDRESS_FOR_VRAM_READ_WRITE_LOW_BYTE ; Set VRAM write address
.00:9B36                 PHY                     ; Preserve VRAM write address
.00:9B37                 REP     #$20 ; ' '
.00:9B39 .A16
.00:9B39                 AND     #$7F ; ''      ; d7 masked out (signals end?) (deprecated?)
.00:9B3C                 ASL
.00:9B3D                 ASL                     ; multiply solid color offset by 4
.00:9B3E                 TAY                     ; offset = (table entry AND 0x7F) * 4
.00:9B3F                 LDA     #7
.00:9B42                 STA     4               ; Uses $04 as a counter
.00:9B45                 LDA     $9B6C,Y         ; 9B6C is the bitplane table.
.00:9B45                                         ; It contains 16 4 byte entries, one for
.00:9B45                                         ; each solid color.
.00:9B45                                         ; Load bitplanes 0 and 1
.00:9B48
.00:9B48 solidtile_bitplanes_0_1:                ; CODE XREF: .00:9B4Ej
.00:9B48                 STA     DATA_FOR_VRAM_WRITE_LOW_BYTE
.00:9B4B                 DEC     4
.00:9B4E                 BPL     solidtile_bitplanes_0_1
.00:9B50                 INY
.00:9B51                 INY
.00:9B52                 LDA     #7
.00:9B55                 STA     4
.00:9B58                 LDA     $9B6C,Y         ; Load bitplanes 2 and 3
.00:9B5B
.00:9B5B solidtile_bitplanes_2_3:                ; CODE XREF: .00:9B61j
.00:9B5B                 STA     DATA_FOR_VRAM_WRITE_LOW_BYTE
.00:9B5E                 DEC     4
.00:9B61                 BPL     solidtile_bitplanes_2_3
.00:9B63                 PLA                     ; Retrieve VRAM write address
.00:9B64                 CLC
.00:9B65                 ADC     #$10            ; Prep it for the next tile
.00:9B68                 TAY
.00:9B69                 SEP     #$20 ; ' '
.00:9B6B .A8
.00:9B6B                 RTS

          */

            int colorOffset = Convert.ToInt32(romArray.GetValue(0x1BAC + (Convert.ToInt32(romArray.GetValue(0x1BAC + (((BGMapPreset)assetComboBox.SelectedItem).LevelID << 1)))
                + (((BGMapPreset)assetComboBox.SelectedItem).SublevelID << 1))));
            int destOffset = 0;

            for (int solid2by1 = 0; solid2by1 < 2; solid2by1++)
            {
                tileSolidColorDraw(destOffset + (solid2by1 * 0x20), 0x00, 0x00, 0x00, 0x00);
            }

            destOffset = destOffset + 0x40;

            int rowLength = Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset));
            if (rowLength != 0)
            {
                colorOffset++;

                for (int solidRow1 = 0; solidRow1 < rowLength; solidRow1++)
                {
                    tileSolidColorDraw(destOffset,
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4))),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 1)),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 2)),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 3))
                        );
                    colorOffset++;
                    destOffset = destOffset + 0x20;
                }

                destOffset = 0x200;

                for (int solid2by1 = 0; solid2by1 < 2; solid2by1++)
                {
                    tileSolidColorDraw(destOffset + (solid2by1 * 0x20), 0x00, 0x00, 0x00, 0x00);
                }

                destOffset = destOffset + 0x40;

                rowLength = Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset));
                if (rowLength != 0)
                {
                    colorOffset++;

                    for (int solidRow1 = 0; solidRow1 < rowLength; solidRow1++)
                    {
                        tileSolidColorDraw(destOffset,
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4))),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 1)),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 2)),
                            Convert.ToByte(romArray.GetValue(0x1B6C + (Convert.ToInt32(romArray.GetValue(0x1BAC + colorOffset)) * 4) + 3))
                            );
                        colorOffset++;
                        destOffset = destOffset + 0x20;
                    }
                }

            }

            


        }

        private void tileSolidColorDraw(int destOffset, byte b0, byte b1, byte b2, byte b3)
        {
            /* DEBUG MESSAGE BOX
            MessageBox.Show("Dest: " + destOffset.ToString("X") + Environment.NewLine +
                b0.ToString("X") + " " + b1.ToString("X") + Environment.NewLine + 
                b2.ToString("X") + " " + b3.ToString("X")); */

                    for (int byteSets = 0; byteSets < 8; byteSets++)
                    {
                        vramRaw[destOffset + (byteSets * 2)] = b0;
                        vramRaw[0x1 + destOffset + (byteSets * 2)] = b1;
                        vramRaw[0x10 + destOffset + (byteSets * 2)] = b2;
                        vramRaw[0x11 + destOffset + (byteSets * 2)] = b3;
                    }
                
        }

        private void levelLoad() /*Loads tile batches for current level*/
        {




            //MessageBox.Show(levelOffsetSolidColor.ToString("X"));





            /* DISASSEMBLY OF LEVEL TILE LOAD ROUTINE FROM FINAL FIGHT GUY (J)
.00:9745 ; ---------------------------------------------------------------------------
.00:9745                 LDA     #0
.00:9747                 XBA
.00:9748                 LDA     $CB0            ; Load level ID
.00:974B                 ASL                     ; *2
.00:974C                 TAX                     ; becomes offset
.00:974D                 REP     #$20 ; ' '
.00:974F .A16
.00:974F                 LDA     $97D3,X         ; Array of level specific offsets
.00:9753                 STA     $1E             ; Store selection in $1E
.00:9756                 SEP     #$20 ; ' '
.00:9758 .A8
.00:9758                 LDA     #0              ; clear A
.00:975A                 XBA
.00:975B                 LDA     $CB1            ; Load sublevel ID
.00:975E                 ASL                     ; *2
.00:975F                 REP     #$20 ; ' '
.00:9761 .A16
.00:9761                 CLC
.00:9762                 ADC     $1E             ; Add result to value in $1E
.00:9765                 TAX                     ; This becomes offset
.00:9766                 LDA     $97D3,X         ; Get offset for first batch of tiles to copy for this screen
.00:976A                 STA     $1E             ; Store back at $1E
.00:976D                 SEP     #$20 ; ' '
.00:976F .A8
.00:976F
.00:976F loc_976F:                               ; CODE XREF: .00:9781j
.00:976F                 JSR     loadlevel_copytiles
.00:9772                 LDX     $1E             ; Retrieve offset for last tile batch
.00:9775                 LDA     $97D5,X         ; Retrieves the byte for the VRAM destination address for this last tile batch.
.00:9775                                         ; Note how bit 7 is masked out by instruction $797A. This is because bit 7 marks
.00:9775                                         ; if this batch is the last one loaded, which the following BMI determines.
.00:9779                 BMI     loc_9783        ; Exit from tile loading if negative
.00:977B                 INX                     ; Or else increment X 3 times for the next batch and start this shit over again.
.00:977C                 INX
.00:977D                 INX
.00:977E                 STX     $1E
.00:9781                 BRA     loc_976F
.00:9783 ; ---------------------------------------------------------------------------
.00:9783
.00:9783 loc_9783:                               ; CODE XREF: .00:9779j
.00:9783                 PLB
.00:9784                 RTL                     ; exit
.00:9785 ; ---------------------------------------------------------------------------
.00:9785
.00:9785 loadlevel_copytiles:                    ; CODE XREF: .00:loc_976Fp
.00:9785                 LDA     #$80 ; 'Ç'
.00:9787                 STA     VRAM_ADDRESS_INCREMENT_VALUE ; Increment VRAM address after accessing high byte
.00:978A                 REP     #$20 ; ' '
.00:978C .A16
.00:978C                 LDX     $1E             ; retrieve offset
.00:978F                 LDA     $97D3,X         ; Use it to get secondary offset (LoROM address for this batch's parameters)
.00:9793                 STA     $10
.00:9796                 LDA     $97D5,X         ; Retrieve VRAM destination address
.00:979A                 AND     #$7F ; ''      ; Mask out all but lowest 7 bits
.00:979D                 XBA
.00:979E                 CLC
.00:979F                 ADC     #$2000          ; $2000 is start of BG tile area in VRAM
.00:979F                                         ; First $200 bytes reserved for solid color tiles and other nonsense
.00:97A2                 STA     ADDRESS_FOR_VRAM_READ_WRITE_LOW_BYTE
.00:97A5                 SEP     #$20 ; ' '
.00:97A7 .A8
.00:97A7                 LDX     $10             ; Retrieve secondary offset
.00:97AA                 LDA     0,X             ; Tile address, byte 2
.00:97AD                 STA     $14
.00:97B0                 LDA     1,X             ; Tile address, byte 1
.00:97B3                 STA     $15
.00:97B6                 LDA     2,X             ; Tile address, byte 0
.00:97B9                 STA     $16
.00:97BC                 REP     #$20 ; ' '
.00:97BE .A16
.00:97BE                 LDA     3,X             ; Number of bytes to copy
.00:97C1                 TAX
.00:97C2                 LDY     #0              ; Y = incrementing offset for tile copy
.00:97C5
.00:97C5 tile_copy_loop:                         ; CODE XREF: .00:97CEj
.00:97C5                 LDA     [D,$14],Y       ; Self explanatory.
.00:97C7                 STA     DATA_FOR_VRAM_WRITE_LOW_BYTE
.00:97CA                 INY
.00:97CB                 INY
.00:97CC                 DEX
.00:97CD                 DEX
.00:97CE                 BNE     tile_copy_loop
.00:97D0                 SEP     #$20 ; ' '
.00:97D2 .A8
.00:97D2                 RTS
*/


            tileSolidColor();

            int levelOffset = Convert.ToInt32(romArray.GetValue(0x17D3 + (((BGMapPreset)assetComboBox.SelectedItem).LevelID << 1)))
                + (((BGMapPreset)assetComboBox.SelectedItem).SublevelID << 1);
            int batchOffset = (Convert.ToInt32(romArray.GetValue(0x17D4 + levelOffset)) << 8) | Convert.ToInt32(romArray.GetValue(0x17D3 + levelOffset));
            bool batchEnd = false;
            int debugBatchCount = 0;

            while (batchEnd == false)
            {
                int batchSrc = loRomConversion(
                    (Convert.ToInt32(romArray.GetValue(0x17D4 + batchOffset)) << 8) | Convert.ToInt32(romArray.GetValue(0x17D3 + batchOffset))
                    );
                int vramDest = ((Convert.ToInt32(romArray.GetValue(0x17D5 + batchOffset)) & 0x7F) << 8) * 2;
                int tileAddr = (loRomConversion(
                    (Convert.ToInt32(romArray.GetValue(2 + batchSrc)) << 16) |
                    (Convert.ToInt32(romArray.GetValue(1 + batchSrc)) << 8) |
                    Convert.ToInt32(romArray.GetValue(0 + batchSrc))
                    )) & 0x1FFFFF;
                int entryLength = (Convert.ToInt32(romArray.GetValue(4 + batchSrc)) << 8) | Convert.ToInt32(romArray.GetValue(3 + batchSrc));

                Buffer.BlockCopy(romArray, tileAddr, vramRaw, vramDest, entryLength);

                batchEnd = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(0x17D5 + batchOffset)) & 0x80);
                debugBatchCount++;
                batchOffset = batchOffset + 3;
            }
            
            for (int tileCount = 0; tileCount < 1024; tileCount++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int finalByte =
                            (((Convert.ToInt32(vramRaw.GetValue((tileCount * 32) + (y * 2))) << x) & 0x80) >> 7)
                            | (((Convert.ToInt32(vramRaw.GetValue((tileCount * 32) + (y * 2) + 1)) << x) & 0x80) >> 6)
                            | (((Convert.ToInt32(vramRaw.GetValue((tileCount * 32) + (y * 2) + 16)) << x) & 0x80) >> 5)
                            | (((Convert.ToInt32(vramRaw.GetValue((tileCount * 32) + (y * 2) + 17)) << x) & 0x80) >> 4);
                        vram[(tileCount * 64) + (y * 8) + x] = Convert.ToByte(finalByte);
                    }
                }
            }

            /*
            for (int tileCount = 0; tileCount < 1024; tileCount++)
            {
                for (int rowCount = 0; rowCount < 8; rowCount++)
                {
                    for (int pixelCount = 0; pixelCount < 8; pixelCount++)
                    {
                        bool d0 = Convert.ToBoolean(Convert.ToInt32((vramRaw.GetValue((tileCount * 32) + (rowCount * 2)))) & Convert.ToInt32(andTable.GetValue(pixelCount)));
                        bool d1 = Convert.ToBoolean(Convert.ToInt32((vramRaw.GetValue((tileCount * 32) + (rowCount * 2) + 1))) & Convert.ToInt32(andTable.GetValue(pixelCount)));
                        bool d2 = Convert.ToBoolean(Convert.ToInt32((vramRaw.GetValue((tileCount * 32) + (rowCount * 2) + 16))) & Convert.ToInt32(andTable.GetValue(pixelCount)));
                        bool d3 = Convert.ToBoolean(Convert.ToInt32((vramRaw.GetValue((tileCount * 32) + (rowCount * 2) + 17))) & Convert.ToInt32(andTable.GetValue(pixelCount)));

                        int finalByte = 0;
                        //Mask final bits with XOR depending on the above boolean calculations

                        if (d0 == true)
                        {
                            finalByte = finalByte ^ 1;
                        }

                        if (d1 == true)
                        {
                            finalByte = finalByte ^ 2;
                        }

                        if (d2 == true)
                        {
                            finalByte = finalByte ^ 4;
                        }

                        if (d3 == true)
                        {
                            finalByte = finalByte ^ 8;
                        }

                        vram[(tileCount * 64) + (rowCount * 8) + pixelCount] = Convert.ToByte(finalByte);
                    }
                   
                }
            } */


            /***** old broken shit
            statusLabel.Text = "Starting level load.";

            //clear labels for testing... delete these later.
            labelBatchOffset2.Text = "";
            labelTileAddr.Text = "";
            labelVramAddr.Text = "";

            //int levelID = ((BGMapPreset)bgMapComboBox.SelectedItem).LevelID;
            //int sublevelID = ((BGMapPreset)bgMapComboBox.SelectedItem).SublevelID;
            //batchOffset is the value which is created at stored at $E1 from $977E in the above code
            int batchPreOffset = Convert.ToInt32(romArray.GetValue(0x17D3 + (((BGMapPreset)bgMapComboBox.SelectedItem).LevelID << 1))) + (((BGMapPreset)bgMapComboBox.SelectedItem).SublevelID << 1);
            int batchOffset = (Convert.ToInt32(romArray.GetValue(0x17D4 + batchPreOffset)) << 8) | Convert.ToInt32(romArray.GetValue(0x17D3 + batchPreOffset));
            labelBatchOffset1.Text = batchOffset.ToString("X");
            int batchCount = 0;
            bool batchEnd = false;

            statusLabel.Text = "Calculating total batch count.";
            //Checks d7 of VRAM address byte from starting batch until it finds a batch where d7 is set, signaling the last batch for this level entry.
            while (batchEnd == false)
            {
                batchEnd = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(0x17D5 + batchOffset + (batchCount * 3))) & 0x80);
                batchCount++;
            }

            labelBatchCount.Text = batchCount.ToString("X");
            statusLabel.Text = "Batch count complete. Total batches: " + Convert.ToString(batchCount);

            for (int batchCompleted = 0; batchCompleted < batchCount; batchCompleted++)
            {
                int vramDest = ((Convert.ToInt32(romArray.GetValue(0x17D5 + batchOffset + (batchCompleted * 3))) & 0x7F) << 8) * 2;
                //int vramDest = ((((((Convert.ToInt32(romArray.GetValue(0x17D6 + batchOffset + (batchCompleted * 3))) << 8) | (Convert.ToInt32(romArray.GetValue(0x17D5 + batchOffset + (batchCompleted * 3))))) & 0x1F) << 8) + 0x800) * 2);
                //gets secondary offset as in $978F
                int batchOffset2 = loRomConversion(((Convert.ToInt32(romArray.GetValue(0x17D4 + batchOffset + (batchCompleted * 3))) << 8) | (Convert.ToInt32(romArray.GetValue(0x17D3 + batchOffset + (batchCompleted * 3))))));
                int tileAddress = loRomConversion((Convert.ToInt32(romArray.GetValue(batchOffset2 + 2)) << 16) 
                    | (Convert.ToInt32(romArray.GetValue(batchOffset2 + 1)) << 8)
                    | (Convert.ToInt32(romArray.GetValue(batchOffset2 + 0))));
                labelTileAddr.Text = labelTileAddr.Text + " " + tileAddress.ToString("X");
                labelBatchOffset2.Text = labelBatchOffset2.Text + " " + batchOffset2.ToString("X");
                labelVramAddr.Text = labelVramAddr.Text + " " + vramDest.ToString("X");

                int byteCount = ((Convert.ToInt32(romArray.GetValue(batchOffset2 + 4 + (batchCompleted * 5)))) << 8) | Convert.ToInt32(romArray.GetValue(batchOffset2 + 3 + (batchCompleted * 5)));
                labelEntryLength.Text = labelEntryLength.Text + " " + byteCount.ToString("X");

                convertTiles(vramDest, tileAddress, byteCount);

                statusLabel.Text = "Converted " + Convert.ToString(batchCompleted + 1) + "/" + Convert.ToString(batchCount) + " tile batches.";
            }


            /*DEBUG - display bytes to text box
            StringBuilder byteString = new StringBuilder(vram.Length * 2);
            foreach (byte b in vram)
                byteString.AppendFormat("{0:X2}", b);
            byteDisplayBox.Text = byteString.ToString();*/
        }

        private void drawSolidColor(int vramAddr)
        {

        }

        private void processPalettes() /*Loads palette batches for current level*/
        {
            /* DISASSEMBLY OF PALETTE LOAD ROUTINE FROM FINAL FIGHT GUY (J)
.00:9CA2 ; ---------------------------------------------------------------------------
.00:9CA2                 PHB                     ; Preserve data bank register
.00:9CA3                 LDA     #7
.00:9CA5                 PHA
.00:9CA6                 PLB
.00:9CA7 ; ds=7000 B=7
.00:9CA7                 LDA     #0
.00:9CA9                 XBA                     ; Clear accumulator
.00:9CAA                 LDA     $CB0
.00:9CAD                 ASL                     ; Get levelID * 2
.00:9CAE                 TAX
.00:9CAF                 REP     #$20 ; ' '
.00:9CB1 .A16
.00:9CB1                 LDA     $9D2A,X         ; Get palette batch pre-offset
.00:9CB5                 STA     $10
.00:9CB8                 SEP     #$20 ; ' '
.00:9CBA .A8
.00:9CBA                 LDA     #0
.00:9CBC                 XBA
.00:9CBD                 LDA     $CB1
.00:9CC0                 ASL                     ; Get sublevelID * 2
.00:9CC1                 REP     #$20 ; ' '
.00:9CC3 .A16
.00:9CC3                 CLC
.00:9CC4                 ADC     $10             ; Add sublevelID * 2 to palette batch pre-offset
.00:9CC7                 TAX
.00:9CC8                 LDA     $9D2A,X         ; Final palette batch offset (mainOffset)
.00:9CCC                 STA     $10
.00:9CCF                 LDY     #$20 ; ' '      ; Load Y with offset for palette staging area in RAM ($400)
.00:9CCF                                         ; First 0x20 bytes are reserved for status bar colors.
.00:9CD2                 SEP     #$20 ; ' '
.00:9CD4 .A8
.00:9CD4
.00:9CD4 loc_9CD4:                               ; CODE XREF: .00:9CFCj
.00:9CD4                 LDX     $10
.00:9CD7                 LDA     $9D2A,X         ; Get first palette batch entry.
.00:9CDB                 STA     0               ; Store to $00 to preserve bit 7
.00:9CDE                 AND     #$7F ; ''
.00:9CE0                 STA     8               ; Store same byte sans d7 to $08. It is the total number of
.00:9CE0                                         ; palettes to copy, starting with the address in this entry.
.00:9CE3                 REP     #$20 ; ' '
.00:9CE5 .A16
.00:9CE5                 LDA     $9D2B,X         ; Palette address entry (uses data bank register when copying)
.00:9CE9                 STA     $18
.00:9CEC                 SEP     #$20 ; ' '
.00:9CEE .A8
.00:9CEE                 INX                     ; Increments X ahead of loading batch entry
.00:9CEF                 INX
.00:9CF0                 INX
.00:9CF1                 STX     $10
.00:9CF4                 JSR     palette_copy    ; Goto palette copying code
.00:9CF7                 LDA     0
.00:9CFA                 BPL     palette_copy_exit ; Checks d7 of palette batch entry. d7 false = exit palette copying.
.00:9CFC                 BRA     loc_9CD4
.00:9CFE ; ---------------------------------------------------------------------------
.00:9CFE
.00:9CFE palette_copy_exit:                      ; CODE XREF: .00:9CFAj
.00:9CFE                 PLB                     ; Retrieve data bank register
.00:9CFF                 LDA     $D6
.00:9D02                 ORA     #2
.00:9D04                 STA     $D6             ; ???
.00:9D07                 RTL                     ; Exit palette copying
.00:9D08 ; ---------------------------------------------------------------------------
.00:9D08
.00:9D08 palette_copy:                           ; CODE XREF: .00:9CF4p
.00:9D08                 LDX     $18             ; Retrieve palette address offset
.00:9D0B
.00:9D0B loc_9D0B:                               ; CODE XREF: .00:9D27j
.00:9D0B                 REP     #$20 ; ' '
.00:9D0D .A16
.00:9D0D                 LDA     #$F
.00:9D10                 STA     $A              ; Primes $0A with number of words to copy
.00:9D13
.00:9D13 palette_copy_loop:                      ; CODE XREF: .00:9D20j
.00:9D13                 LDA     $E800,X         ; Self explanatory...
.00:9D16                 STA     $400,Y
.00:9D19                 INX
.00:9D1A                 INX
.00:9D1B                 INY
.00:9D1C                 INY
.00:9D1D                 DEC     $A
.00:9D20                 BPL     palette_copy_loop ; Loop ends when $0A == 0
.00:9D22                 SEP     #$20 ; ' '
.00:9D24 .A8
.00:9D24                 DEC     8
.00:9D27                 BNE     loc_9D0B        ; If $08 != 0, start copying the very next palette.
.00:9D29                 RTS                     ; Else return
.00:9D29 ; ---------------------------------------------------------------------------
            */

            int mainOffset = Convert.ToInt32(romArray.GetValue(0x1D2A + Convert.ToInt32(romArray.GetValue(0x1D2A + (((BGMapPreset)assetComboBox.SelectedItem).LevelID << 1)))
                + (((BGMapPreset)assetComboBox.SelectedItem).SublevelID << 1)));
            int batchCounter = 0;
            int paletteCounter = 0;
            bool batchNotEnd = true;
            //for some stupid reason d7 flag indicates if this is NOT the last entry of the palette batch, unlike the tile copy loop

            while(batchNotEnd == true)
            {
                int batchEntryCount = Convert.ToInt32(romArray.GetValue(0x1D2A + mainOffset + (batchCounter * 3))) & 0x7F;
                batchNotEnd = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(0x1D2A + mainOffset + (batchCounter * 3))) & 0x80);
                int paletteAddress = (Convert.ToInt32(romArray.GetValue(0x1D2C + mainOffset + (batchCounter* 3))) << 8) | 
                    Convert.ToInt32(romArray.GetValue(0x1D2B + mainOffset + (batchCounter * 3)));

                for (int entryLoopCount = 0; entryLoopCount < batchEntryCount; entryLoopCount++)
                {
                    int srcAddress = (0x3E800 + paletteAddress + (entryLoopCount * 0x20));
                    int destAddress = (0x20 + (paletteCounter * 0x20));
                    Buffer.BlockCopy(romArray, srcAddress, bgPalettes, destAddress, 0x20);
                    paletteCounter++;
                }

                batchCounter++;

            }

            paletteVram = new SnesPalette(bgPalettes, 0);

            //BROKEN SHIT AHEAD

            //A "palette batch" is a collection of one or more "palette batch entries."
            //A "palette batch entry" contains an 8-bit counter and 16-bit offset for copying palette data to VRAM.
            //The counter in a palette batch entry specifies how many palettes to copy, starting with the palette at the provided offset.
            //Bit 7 of the palette batch entry counter indicates if this is the last entry in the palette batch. Bit 7 is masked out before being used as a counter ($9CDE).
            //If the entry counter >1 then the loop will continue copying the next palettes until the counter is finished.
            //Once the entry counter is exhausted, the game checks bit 7 of the entry counter. If true, then it ceases copying palettes and returns to the main program loop.
            //
            //batchPreOffset is the first offset for the recursive table for level-specific palette offsets. levelID * 2 is used as an offset to obtain a value and then sublevelID * 2 is added to that value
            //batchOffset is the 16bit value obtained from the same table as batchPreOffset. It contains the offset for the first palette batch entry for the current level.
            //paletteTotalCount is the number of palettes that have been copied to bgPalettes. paletteTotalCount * 0x20 is used as an offset for copying palettes to bgPalettes.
            //batchEntryCount is the total number of batch entries that have been processed.
            //batchEnd remains false until a batch entry is completed where d7 of the batch entry counter is TRUE
            //
            //paletteBatchEntryCount is the number of palettes in the current palette batch entry
            //paletteBatchEntryOffset is the offset for the first 32-byte palette in the current palette batch entry

            /*
            int batchPreOffset = Convert.ToInt32(romArray.GetValue(0x1D2A + ((((BGMapPreset)bgMapComboBox.SelectedItem).LevelID) << 1))) | ((((BGMapPreset)bgMapComboBox.SelectedItem).SublevelID) << 1);
            int batchOffset = (Convert.ToInt32(romArray.GetValue(0x1D2B + batchPreOffset)) << 8) + Convert.ToInt32(romArray.GetValue(0x1D2A + batchPreOffset));
            int paletteTotalCount = 0;
            int batchEntryCount = 0;
            bool batchEnd = false;

            while (batchEnd == false)
            {
                int paletteBatchEntryCount = Convert.ToInt32(romArray.GetValue(0x1D2A + batchOffset + (batchEntryCount * 3))) & 0x7F;
                int paletteBatchEntryOffset = (Convert.ToInt32(romArray.GetValue(0x1D2C + batchOffset + (batchEntryCount * 3))) << 8) | Convert.ToInt32(romArray.GetValue(0x1D2B + batchOffset + (batchEntryCount * 3)));

                for (int batchEntryCompleted = 0; batchEntryCompleted < paletteBatchEntryCount; batchEntryCompleted++)
                {
                    int entryAddress = (0x3E800 + paletteBatchEntryOffset + (batchEntryCompleted * 0x20));
                    int destAddress = (0x20 + (paletteTotalCount * 0x20));
                    Buffer.BlockCopy(romArray, entryAddress, bgPalettes, destAddress, 0x20);
                    paletteTotalCount++; */
            //debug message box
            /* MessageBox.Show("Batch entry: " + Convert.ToString(batchEntryCompleted) + Environment.NewLine +
                 "Entry origin offset: 0x" + paletteBatchEntryOffset.ToString("X") + Environment.NewLine + 
                 "Entry Address: 0x" + entryAddress.ToString("X") + Environment.NewLine +
                 "Array Dest: 0x" + destAddress.ToString("X") + Environment.NewLine + Environment.NewLine +
                 "Bytes: " + BitConverter.ToString(romArray, 0x3E800 + paletteBatchEntryOffset + (batchEntryCompleted * 0x20), 0x20) , "Palette Debug"); */
            /*

    }
batchEntryCount++;
batchEnd = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(0x1D2A + batchOffset + (batchEntryCount * 3))) & 0x80);
}

paletteVram = new SnesPalette(bgPalettes, 0);
*/
        }

        private void convertTiles2(int vramDest, int tileAddress, int byteCount)
        {
            //experimental replacement for convertTiles
            for (int completedTiles = 0; completedTiles < (byteCount * 8); completedTiles++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int finalByte =
                            (((Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (y * 2))) << x) & 0x80) >> 7)
                            | (((Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (y * 2) + 1)) << x) & 0x80) >> 6)
                            | (((Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (y * 2) + 16)) << x) & 0x80) >> 5)
                            | (((Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (y * 2) + 17)) << x) & 0x80) >> 4);
                    }
                }
            }
        }

        private void convertTiles(int vramDest, int tileAddress, int byteCount)
        {
            //One SNES bg tile is 32 bytes in 4bpp Planar Composite format
            //To convert to 8bpp then each tile becomes 64 bytes in length
            //
            //Processing 4 bytes at a time 8 times to get the 8bpp values for each row of tile pixels. 

            int rowCounterLow = 0;
            int rowCounterHigh = 0;
            int completedTiles = 0;

            //byteCount = byteCount * 2;

            for (int byteCounter = 0; byteCounter < byteCount; byteCounter++)
            {
                //statusLabel.Text = "Converted " + completedTiles.ToString("X") + " tiles.";
                bool d0 = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (rowCounterHigh * 2))) & Convert.ToInt32(andTable.GetValue(rowCounterLow)));
                bool d1 = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (rowCounterHigh * 2) + 1)) & Convert.ToInt32(andTable.GetValue(rowCounterLow)));
                bool d2 = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (rowCounterHigh * 2) + 16)) & Convert.ToInt32(andTable.GetValue(rowCounterLow)));
                bool d3 = Convert.ToBoolean(Convert.ToInt32(romArray.GetValue(tileAddress + (completedTiles * 32) + (rowCounterHigh * 2) + 17)) & Convert.ToInt32(andTable.GetValue(rowCounterLow)));

                int finalByte = 0;
                //Mask final bits with XOR depending on the above boolean calculations

                if (d0 == true)
                {
                    finalByte = finalByte ^ 1;
                }

                if (d1 == true)
                {
                    finalByte = finalByte ^ 2;
                }

                if (d2 == true)
                {
                    finalByte = finalByte ^ 4;
                }

                if (d3 == true)
                {
                    finalByte = finalByte ^ 8;
                }

                vram[vramDest + byteCounter] = Convert.ToByte(finalByte);

                rowCounterLow++;

                if (rowCounterLow >= 8)
                {
                    rowCounterLow = 0;
                    rowCounterHigh++;
                }

                if (rowCounterHigh >= 8)
                {
                    rowCounterHigh = 0;
                    completedTiles++;
                }
            }
        }

        public void selectBGMap(int bgID)
        {
            drawBGMap(bgID);
            bgMapPanel.Refresh();
        }

        private void selectAsset()
        {
            levelLoad();
            processPalettes();
            selectBGMap();
            drawHiMetatileBrowser();
        }

        private void drawHiMetatileBrowser()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    drawHiMetatile((Convert.ToInt32(metatilePageSelector.Value) * 512) + (x * 8) + (y * 64));
                    hiMetatileBrowserGfx.DrawImage(hiMetatile, (x * 32), (y * 32));
                    //metatileBrowserPanel.Refresh();
                }
            }
            metatileBrowserPanel.Refresh();
        }

        private void displayHiMetatileSelection()
        {
            /* obsolete
            drawHiMetatile(hiMetatileSelection);
            hiMetatileSelection1BitmapGfx.DrawImage(hiMetatile, 0, 0);
            hiMetatileSelection1Panel.Refresh();
            */
        }

        private void selectBGMap()
        {
            string text = bgMapSelector.Text;
            int n;
            if (int.TryParse(text, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n))
            {
                selectBGMap(n);
            }
            else
            {
                MessageBox.Show("Please enter a valid hexadecimal value.", "Invalid Value");
            }
            drawHiMetatileBrowser();
        }

        private void launchMTEditor()
        {
            mte.ShowDialog();

            selectBGMap(Convert.ToInt32(bgMapSelector.Value));
            updateHiMetatileSelection();
            drawHiMetatileBrowser();
        }

        private void updateHiMetatileSelection()
        {
            drawHiMetatile(hiMetatile1Selection);
            hiMetatileSelection1BitmapGfx.DrawImage(hiMetatile, 0, 0);
            hiMetatileSelection1Panel.Refresh();
            labelSelectionHiMT1.Text = composeHexAddressString(hiMetatile1Selection);
        }

        private void selectHiMetatile(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                hiMetatile1Selection = hiMetatileBrowserHover;
                updateHiMetatileSelection();
            }
            //right click on hiMetatile Browser to edit the metatile
            if (e.Button == MouseButtons.Right)
            {
                hiMetatileEditSelection = hiMetatileBrowserHover;
                launchMTEditor();
                
                /* moved this code to launchMTEditor()
                mte.ShowDialog();

                selectBGMap(Convert.ToInt32(bgMapSelector.Value));
                drawHiMetatileBrowser();
                drawHiMetatile(hiMetatile1Selection);
                hiMetatileSelection1BitmapGfx.DrawImage(hiMetatile, 0, 0);
                hiMetatileSelection1Panel.Refresh();
                */
            }

            /* Secondary hiMetatile selection
            if (e.Button == MouseButtons.Right)
            {
                hiMetatile2Selection = hiMetatileBrowserHover;
                drawHiMetatile(hiMetatile2Selection);
                hiMetatileSelection2BitmapGfx.DrawImage(hiMetatile, 0, 0);
                hiMetatileSelection2Panel.Refresh();
                labelSelectionHiMT2.Text = composeHexAddressString(hiMetatile2Selection);
            } */
        }

        private void colorViewSelected() /*Used with the View Color debug groupbox*/
        {
            panelTest.BackColor = ColorTranslator.FromHtml(Convert.ToString(paletteVram.palette.GetValue(Convert.ToInt32(colorSelector.Value))));
        }

        public static string composeHexAddressString(int n) 
            //OBSOLETE!
            //use formatHexAddress instead
        {
            string s = "0x";

            if (n < 0x1000)
            {
                s = s + "0";
                if (n < 0x100)
                {
                    s = s + "0";
                }
            }

            return s + n.ToString("X");
        }

        private void editBGMap(MouseEventArgs e)
        {
            //OBSOLETE

            /*
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                int offset = ((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2;
                int id = new int();

                if (e.Button == MouseButtons.Left)
                {
                    id = hiMetatile1Selection;
                }
                if (e.Button == MouseButtons.Right)
                {
                    id = hiMetatile2Selection;
                }
                //changes the bgmap entry in the ROM
                romArray[0x72000 + (Convert.ToInt32(bgMapSelector.Value) * 128) + offset] = Convert.ToByte((id & 0xFF00) >> 8);
                romArray[0x72001 + (Convert.ToInt32(bgMapSelector.Value) * 128) + offset] = Convert.ToByte(id & 0xFF);

                //draws the change to the bgmap panel
                drawHiMetatile(id);
                bgMapGfx.DrawImage(hiMetatile, (e.Location.X / 64) * 32, (e.Location.Y / 64) * 32);
                bgMapPanel.Refresh();
            }
            */
        }
        
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            levelLoad();
            processPalettes();
            selectBGMap();
            drawHiMetatileBrowser();
        }

        private void colorSelector_ValueChanged(object sender, EventArgs e)
        {
            colorViewSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorViewSelected();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tileTestPanel.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawSingleTile(Convert.ToInt32(tileTestTileSelector.Value), Convert.ToInt32(tileTestPaletteSelector.Value), paletteVram.palette, vram);
            tileTestPanel.Refresh();
        }

        private void tileTestPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(singleTile, 0, 0, 32, 32);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openROM.ShowDialog() == DialogResult.Cancel)
            {
            }

            else
            {
                selectedROM = openROM.FileName;
                loadedROM(selectedROM);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(vramFullView, 0, 0);
        }

        private void buttonDrawVram_Click(object sender, EventArgs e)
        {
            drawVram();
            vramViewPanel.Refresh();
        }

        private void btnDisplayLoMetatile_Click(object sender, EventArgs e)
        {
            int selection = int.Parse(loMetatileSelector.Text, System.Globalization.NumberStyles.HexNumber);
            drawLoMetatile(Convert.ToBoolean(loMetatileNegCheck.CheckState), selection);
            loMetatilePanel.Refresh();
        }

        private void loMetatilePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(loMetatile, 0, 0, 48, 48);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tileTestPaletteSelector_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnDisplayHiMetatile_Click(object sender, EventArgs e)
        {
            int hiID = int.Parse(hiMetatileSelector.Text, System.Globalization.NumberStyles.HexNumber);
            drawHiMetatile(hiID);
            hiMetatilePanel.Refresh();
        }

        private void hiMetatilePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(hiMetatile, 0, 0, 96, 96);
        }

        private void bgMapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(bgMap, 0, 0, 512, 512);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(metatilePageSelector.Text, System.Globalization.NumberStyles.Integer);
            if (n > 0)
            {
                metatilePageSelector.Text = Convert.ToString(n - 1);
            }
            drawHiMetatileBrowser();
        }

        private void metatileBrowserPanel_MouseMove(object sender, MouseEventArgs e)
        {
            hiMetatileBrowserHover = (Convert.ToInt32(metatilePageSelector.Value) * 512) + ((e.Location.Y / 64) * 64) + ((e.Location.X / 64) * 8);
            hiMetatileBrowserHoverDisplay.Text = composeHexAddressString(hiMetatileBrowserHover);
        }

        private void metatileBrowserPanel_MouseLeave(object sender, EventArgs e)
        {
            hiMetatileBrowserHoverDisplay.Text = " ";
        }

        private void metatileBrowserPanel_MouseClick(object sender, MouseEventArgs e)
        {
            selectHiMetatile(e);
        }

        private void metatileSelectionPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(hiMetatileSelection1Bitmap, 0, 0, 64, 64);
        }

        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(metatilePageSelector.Text, System.Globalization.NumberStyles.Integer);
            if (n < 127)
            {
                metatilePageSelector.Text = Convert.ToString(n + 1);
                drawHiMetatileBrowser();
            }
        }

        private void btnSelectBGMap_Click(object sender, EventArgs e)
        {
            selectBGMap();
        }

        private void metatileBrowserPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(hiMetatileBrowser, 0, 0, 512, 512);
        }

        private void bgMapSelectorOld_TextChanged(object sender, EventArgs e)
        {

        }

        private void assetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectAsset();
        }

        private void bgMapSelector_ValueChanged(object sender, EventArgs e)
        {
            selectBGMap(Convert.ToInt32(bgMapSelector.Value));
        }

        private void metatilePageSelector_ValueChanged(object sender, EventArgs e)
        {
            drawHiMetatileBrowser();
        }

        private void bgMapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            bgMapPanelHover = (Convert.ToInt32(romArray.GetValue(0x72000 + Convert.ToInt32(bgMapSelector.Value * 128 + (((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2)))) << 8) | Convert.ToInt32(romArray.GetValue(0x72001 + Convert.ToInt32(bgMapSelector.Value * 128 + (((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2))));
            //bgMapPanelHover = (Convert.ToInt32(bgMapSelector.Value) * 512) + ((e.Location.Y / 64) * 64) + ((e.Location.X / 64) * 8);
            bgMapPanelHoverDisplay.Text = composeHexAddressString(bgMapPanelHover);
        }

        private void hiMetatileSelection2Panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(hiMetatileSelection2Bitmap, 0, 0, 64, 64);
        }

        private void bgMapPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int offset = ((e.Location.X / 64) + ((e.Location.Y / 64) * 8)) * 2;
                int id = hiMetatile1Selection;
                
                //changes the bgmap entry in the ROM
                romArray[0x72000 + (Convert.ToInt32(bgMapSelector.Value) * 128) + offset] = Convert.ToByte((id & 0xFF00) >> 8);
                romArray[0x72001 + (Convert.ToInt32(bgMapSelector.Value) * 128) + offset] = Convert.ToByte(id & 0xFF);

                //draws the change to the bgmap panel
                drawHiMetatile(id);
                bgMapGfx.DrawImage(hiMetatile, (e.Location.X / 64) * 32, (e.Location.Y / 64) * 32);
                bgMapPanel.Refresh();
            }
            if (e.Button == MouseButtons.Left)
            {
                hiMetatile1Selection = bgMapPanelHover;
                drawHiMetatile(hiMetatile1Selection);
                hiMetatileSelection1BitmapGfx.DrawImage(hiMetatile, 0, 0);
                hiMetatileSelection1Panel.Refresh();
                labelSelectionHiMT1.Text = composeHexAddressString(hiMetatile1Selection);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes(selectedROM, romArray);
            recentlySaved = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveROM.ShowDialog() == DialogResult.Cancel)
            {

            }
            else
            {
                File.WriteAllBytes(saveROM.FileName, romArray);
                selectedROM = saveROM.FileName;
                recentlySaved = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about.ShowDialog();
        }

        private void hiMetatileSelection1Panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                hiMetatileEditSelection = hiMetatile1Selection;
                launchMTEditor();
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            help.ShowDialog();
        }

        private void buttonPCXTest_Click(object sender, EventArgs e)
        {
            savePCXData();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            savePCXData2();
        }
    }

    /*    public class SingleTile
        {
            static Bitmap bmp = new Bitmap(8, 8);
            public Graphics tile = Graphics.FromImage(bmp);

            public SingleTile(int tileID, int palID, string[] palettes, byte[] tileData)
            {
                for (int y = 0; y > 8; y++)
                {
                    for (int x = 0; x > 8; x++)
                    {
                        Brush pixel = new SolidBrush(ColorTranslator.FromHtml(Convert.ToString(Convert.ToString(palettes.GetValue((palID * 16) + Convert.ToInt32(tileData.GetValue((tileID * 64) + y + x)))))));
                        tile.FillRectangle(pixel, x, y, 1, 1);
                    }
                }
            }
        }*/

    public class SnesPalette
    {
        public string[] palette = new string[0x100];

        //method that fetches 16 colors from a ROM offset
        public SnesPalette(byte[] rom, int offset)
        {
            int length = rom.Length >> 1;
            for (int colorCounter = 0; colorCounter < length; colorCounter++)
            {
                int colorWord = BitConverter.ToUInt16(rom, (offset + (colorCounter * 2)));
                int red = colorWord & 0x1F;
                red = (red << 3) | (red >> 2);
                int green = colorWord & 0x3E0;
                green = (green >> 2) | (green >> 7);
                int blue = colorWord & 0x7C00;
                blue = (blue >> 7) | (blue >> 12);
                palette[colorCounter] = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
            }
        }

        public string[] Palette
        {
            get
            {
                return palette;
            }
        }
    }

    /* ARGB VARIANT OF SNESPALETTE
    public class SnesPalette
    {
        public int[] palette = new int[0x100];

        //method that fetches 16 colors from a ROM offset
        //byte variant for SNESTest3
        //32 bit color
        public SnesPalette(byte[] rom, int offset)
        {
            int length = rom.Length >> 1;
            for (int colorCounter = 0; colorCounter < length; colorCounter++)
            {
                int colorWord = BitConverter.ToUInt16(rom, (offset + (colorCounter * 2)));
                int red = colorWord & 0x1F;
                red = (red << 3) | (red >> 2);
                int green = colorWord & 0x3E0;
                green = (green >> 2) | (green >> 7);
                int blue = colorWord & 0x7C00;
                blue = (blue >> 7) | (blue >> 12);
                palette[colorCounter] = (0xFF << 24) | (red << 16) | (green << 8) | blue;
            }
        }

        public int[] Palette
        {
            get
            {
                return palette;
            }
        }
    } */

    /* BYTE VARIANT OF SNESPALETTE
    public class SnesPalette
    {
        public byte[] palette = new byte[64];

        //method that fetches 16 colors from a ROM offset
        //byte variant for SNESTest3
        //32 bit color
        public SnesPalette(byte[] rom, int offset)
        {
            for (int colorCounter = 0; colorCounter <= 15; colorCounter++)
            {
                int colorWord = BitConverter.ToUInt16(rom, (offset + (colorCounter * 2)));
                int red = colorWord & 0x1F;
                red = (red << 3) | (red >> 2);
                int green = colorWord & 0x3E0;
                green = (green >> 2) | (green >> 7);
                int blue = colorWord & 0x7C00;
                blue = (blue >> 7) | (blue >> 12);
                palette[(colorCounter * 4)] = 0x00;
                palette[(colorCounter * 4) + 1] = (byte)red;
                palette[(colorCounter * 4) + 2] = (byte)green;
                palette[(colorCounter * 4) + 3] = (byte)blue;
            }
        }
    }*/

    public class BGMapPreset
    {
        public string displayName;
        public int levelID;
        public int sublevelID;

        public BGMapPreset (string name, int lvl, int sblvl)
        {
            displayName = name;
            levelID = lvl;
            sublevelID = sblvl;
        }

        public int SublevelID
        {
            get
            {
                return sublevelID;
            }
        }

        public int LevelID
        {
            get
            {
                return levelID;
            }
        }

        public override string ToString()
        {
            return displayName;
        }
    }

    /*public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }*/
}