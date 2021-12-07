using System;

namespace RoyApp.Interfaces
{
    public interface IMessageBoxService
    {
        void Error(Exception ex);
        void ExportSuccess();
        void ValueMissing();
    }
}
