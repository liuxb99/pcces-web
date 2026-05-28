using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain;

public class PccesHelp
{
	public static void HelpPDF(string FileName)
	{
		string F_ConnStr = Application.StartupPath + "\\Help\\";
		PDFForm PDFForm1 = new PDFForm();
		PDFForm1._FileName = F_ConnStr + FileName + ".pdf";
		PDFForm1.Show();
	}
}
