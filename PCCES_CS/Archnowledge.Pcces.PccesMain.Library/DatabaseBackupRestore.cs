using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.DatabaseUpgrade;
using Archnowledge.Pcces.STDClass;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class DatabaseBackupRestore
{
	public static ExecResult BackupDatabase(PccesBaseHelper baseHelper, string BackupPath, string DatabaseName, out string BackupFile, ChgStrEventHandler eventHandler, ref int Progress)
	{
		Progress++;
		eventHandler?.Invoke("[" + DatabaseName + "] 資料庫備份，請稍後。", ref Progress);
		ExecResult ER = baseHelper.Backup(BackupPath, DatabaseName, out BackupFile);
		if (ER.ReturnCode == 0)
		{
			Progress++;
			eventHandler?.Invoke("[" + DatabaseName + "] 資料庫備份成功。", ref Progress);
		}
		return ER;
	}

	public static ExecResult RestoreDatabase(PccesBaseHelper baseHelper, string BackupPath, string BackupFile, string DatabaseName, out string NewDatabasename, ChgStrEventHandler eventHandler, ref int Progress)
	{
		Progress++;
		eventHandler?.Invoke("正在還原成新的資料庫。", ref Progress);
		NewDatabasename = DatabaseName;
		for (int i = 0; i < 100; i++)
		{
			NewDatabasename = DatabaseName + "_" + i;
			if (!baseHelper.ExistsDatabase(NewDatabasename))
			{
				break;
			}
		}
		Progress++;
		eventHandler?.Invoke("正在還原成新的資料庫 [" + NewDatabasename + "]。", ref Progress);
		ExecResult ER = baseHelper.Restore(BackupPath, BackupFile, NewDatabasename);
		if (ER.ReturnCode == 0)
		{
			Progress++;
			eventHandler?.Invoke("資料庫 [" + NewDatabasename + "]還原成功。", ref Progress);
		}
		return ER;
	}
}
