using System.IO;

namespace RoyApp.Interfaces
{
    public interface IViewDialog
    {
        Stream Open();
        string Save();
    }
}
