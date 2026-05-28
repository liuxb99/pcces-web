using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.BudExe;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.Coms.ProjectService;
using Archnowledge.Pcces.PccesMain.SysMaintain;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class ComsExpandBudget
{
	private string ProjectCode;

	public ComsExpandBudget(string ProjectCode)
	{
		this.ProjectCode = ProjectCode;
	}

	public void DoExecuteExpandBudget()
	{
		bool Running = true;
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "重新展開 " + ProjectCode + " 執行預算，請稍後。 ";
		FM_INFO._MaxValue = 100;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		Application.DoEvents();
		int ProgressValue = 0;
		int ErrorTry = 0;
		while (Running && ErrorTry < 10)
		{
			Thread.Sleep(2000);
			ProjectServiceHelper theProjectServiceHelper = new ProjectServiceHelper(ForceEnable: true);
			Archnowledge.Common.ExecResult ER;
			PccesExpandAction theAction = theProjectServiceHelper.ExecuteExpandBudgetQuery(ProjectCode, out ER);
			FM_INFO.SetValue("", ProgressValue++);
			if (ER.ReturnCode == 0)
			{
				if (theAction.Running)
				{
					continue;
				}
				FM_INFO.SetValue("", 100);
				if (theAction.ReturnCode == 0)
				{
					MessageBox.Show("展開 " + ProjectCode + " 執行預算成功");
					BudExeProject budExeProject = new BudExeProject();
					ER = budExeProject.SetCOMSExpandBudget(ProjectCode);
					if (ER.ReturnCode != 0)
					{
						MessageBox.Show("budExeProject.SetCOMSExpandBudget() Error：\n" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("展開 " + ProjectCode + " 執行預算失敗, Error=" + theAction.Message);
				}
				Running = false;
			}
			else
			{
				ErrorTry++;
			}
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
	}
}
