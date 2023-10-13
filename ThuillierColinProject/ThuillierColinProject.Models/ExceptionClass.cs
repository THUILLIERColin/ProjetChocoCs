using ThuillierColinProject.ServiceLogs;

namespace ThuillierColinProject.Models;

public class ExceptionClass : Exception
{
    string _message;

    public ExceptionClass(string msg)
    {
        _message = msg;
        SingletonLog.GetInstance().Log(msg, LogClass.TypeMessage.Error);
    }

    public override string ToString()
    {
        return _message;
    }
}