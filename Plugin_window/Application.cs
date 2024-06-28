using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;

using System.IO;

namespace Plugin_window
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemlyPath = Assembly.GetExecutingAssembly().Location;

            if(panel.AddItem(new PushButtonData("Plugin_window", "QR Code Generator", thisAssemlyPath, "Plugin_window.Command")) is PushButton button)
            {
                button.ToolTip = "My Plugin window";

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemlyPath), "Resources", "PP.ico"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;
            }

            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication a)
        {
            string tab = "Pfenninger und Partner AG";
            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tab);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "Pfenninger und Partner AG");
            }
            catch (Exception ex)
            { 
                Debug.WriteLine(ex.Message); 
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "Pfenninger und Partner AG"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;
        }
    }
}
