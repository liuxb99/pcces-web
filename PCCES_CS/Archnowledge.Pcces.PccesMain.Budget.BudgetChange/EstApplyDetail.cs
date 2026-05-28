using System;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class EstApplyDetail
{
	public string Num;

	private DateTime InputDate;

	private string ApplyUser;

	private string ChangeTitle;

	public EstApplyDetail(string Num, DateTime InputDate, string ApplyUser, string ChangeTitle)
	{
		this.Num = Num;
		this.InputDate = InputDate;
		this.ApplyUser = ApplyUser;
		this.ChangeTitle = ChangeTitle;
	}

	public override string ToString()
	{
		return Num.PadLeft(3, ' ') + "  " + InputDate.ToString("yyyy/MM/dd") + "  [" + ApplyUser + "] " + ChangeTitle;
	}
}
