using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QRCoder;
using System.Linq;
using System.Data.Linq;
using static QRCoder.Base64QRCode;

namespace Plugin_window
{
    public partial class QRGenerator : System.Windows.Forms.Form
    {
        Document Doc;
        Bitmap qrCodeImage;
        private string linkToEncode;
        private string tempImagePath = Path.Combine(Path.GetTempPath(), "temp_QRCodeImage.png");

        public QRGenerator(Document doc)
        {
            InitializeComponent();
            Doc = doc;
            PopulateSheetList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void insert_text_label_Click(object sender, EventArgs e)
        {

        }

        private void insert_link_textbox_TextChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            SaveImageToFile();
        }


        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            linkToEncode = insert_link_textbox.Text;

            GenerateImage(linkToEncode);

            TaskDialog.Show("Info!", "QR code generated!");
        }

        private void place_to_plans_Click(object sender, EventArgs e)
        {
            try
            {
                Place_to_Sheets();
                TaskDialog.Show("QR Code placed", $"You placed the QR code on the selected sheets");
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }

        }

        private void SaveImageToFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the image to the selected file
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }

        private void GenerateImage(string link)
        {
            // Create QR code data
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.L);

            QRCode qrCode = new QRCode(qrCodeData);
            qrCodeImage = qrCode.GetGraphic(6);

            pictureBox1.Image = qrCodeImage;

            Bitmap qeCodeImage_resized = ResizeBitmap(qrCodeImage, 150, 150);

            qeCodeImage_resized.Save(tempImagePath);

        }


        private void PopulateSheetList()
        {
            checkedListBox1.Items.Clear();

            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            ICollection<Element> sheets = collector.OfClass(typeof(ViewSheet)).ToElements();

            foreach (Element sheet in sheets)
                {
                    checkedListBox1.Items.Add(sheet.Name);
                }
        }

        private void Place_to_Sheets()
        {
            using (Transaction trans = new Transaction(Doc, "Creating a Text note"))
            {
                trans.Start();

                ElementId rasterImageTypeId = LoadImage(tempImagePath);

                if (rasterImageTypeId == ElementId.InvalidElementId)
                {
                    TaskDialog.Show("Error", "Failed to load QR code image into Revit.");
                    trans.RollBack();
                    return;
                }

                FilteredElementCollector collector = new FilteredElementCollector(Doc);
             
                TextNoteType textNoteType = collector.OfClass(typeof(TextNoteType)).FirstOrDefault() as TextNoteType;

                if (textNoteType == null)
                {
                    TaskDialog.Show("Error", "No valid TextNoteType found in the document.");
                    trans.RollBack();
                    return;
                }

                TextNoteOptions opts = new TextNoteOptions(textNoteType.Id)
                {
                    HorizontalAlignment = HorizontalTextAlignment.Left
                };
                
                XYZ origin_text = new XYZ(2.385, 0.1785, 0);
                XYZ origin_link = new XYZ(2.385, 0.11, 0);
                XYZ origin_qrCode = new XYZ(2.62, 0.12, 0); 
                BoxPlacement box = BoxPlacement.Center;
               
                ImagePlacementOptions placementOptions = new ImagePlacementOptions(origin_qrCode, box);

                string text_link = linkToEncode;
                string multiline_text = "Gerne können Sie die\r\n" +
                    "Gelegenheit nutzen, alle Pläne\r\n" +
                    "und 3D-Ansichten online zu\r\n" +
                    "erkunden. Bitte verwenden Sie\r\n" +
                    "hierfür den QR-Code oder Link.";

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    FilteredElementCollector sheetCollector = new FilteredElementCollector(Doc);
                  
                    ICollection<Element> sheets = sheetCollector.OfClass(typeof(ViewSheet)).ToElements();


                    bool sheetFound = false;
                    foreach (Element sheet in sheets)
                    {
                        if (sheet.Name == item.ToString())
                        {
                            ViewSheet viewSheet = sheet as ViewSheet;
                            FilteredElementCollector titleBlockCollector = new FilteredElementCollector(Doc, sheet.Id).OfCategory(BuiltInCategory.OST_TitleBlocks).OfClass(typeof(FamilyInstance));
                            foreach (FamilyInstance titleblock in titleBlockCollector)
                            {
                                Parameter title_width = titleblock.get_Parameter(BuiltInParameter.SHEET_WIDTH);
                                string str_title_width = title_width.AsValueString();
                                // TaskDialog.Show("Title Block width", $"Width: {str_title_width}");
                            }

                            if (viewSheet != null)
                            {
                                TextNote textNote = TextNote.Create(Doc, sheet.Id, origin_text, multiline_text, opts);

                                TextNote LinkNote = TextNote.Create(Doc, sheet.Id, origin_link, text_link, opts);

                                Autodesk.Revit.DB.ImageType imageType = Doc.GetElement(rasterImageTypeId) as Autodesk.Revit.DB.ImageType;
                                if (imageType != null)
                                {
                                    ImageInstance imageInstance = ImageInstance.Create(Doc, viewSheet, rasterImageTypeId, placementOptions);
                                    // TaskDialog.Show("Success", $"QR code image placed on sheet: {sheet.Name}");
                                }

                                sheetFound = true;
                                // TaskDialog.Show("Success", $"Text note created on sheet: {sheet.Name}");
                                break;
                            }

                        }
                    }
                    if (!sheetFound)
                    {
                        TaskDialog.Show("Error", $"Sheet '{item}' not found!");
                    }
                }
                trans.Commit();
                TaskDialog.Show("Info", "Text notes placed succesfully!");
          
            }
        }


        private ElementId LoadImage(string imagePath)
        {
            try
            {
                ImageTypeOptions options = new ImageTypeOptions(imagePath, false, ImageTypeSource.Import);
                Autodesk.Revit.DB.ImageType imageType = Autodesk.Revit.DB.ImageType.Create(Doc, options);
                return imageType.Id;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", $"Failed to load image: {ex.Message}");
                return ElementId.InvalidElementId;
            }
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (checkedListBox1.SelectedIndex != -1)
            //{
            //    string selectedSheetName = checkedListBox1.SelectedItem.ToString();
            //    TaskDialog.Show("Selected Sheet", $"You selected the sheet: {selectedSheetName}");
            //}
        }
    }
}
