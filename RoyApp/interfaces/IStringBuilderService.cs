using System.Text;

namespace RoyApp.interfaces
{
    public interface IStringBuilderService
    {
        IStringBuilderService Append(string line);
        IStringBuilderService AppendLine();
        StringBuilder GetS();
    }
}