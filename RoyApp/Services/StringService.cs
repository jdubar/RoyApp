using System.Text;

namespace RoyApp.Services
{
    public interface IStringBuilderService
    {
        IStringBuilderService Append(string line);
        IStringBuilderService AppendLine();
        StringBuilder GetS();
    }

    public class StringBuilderService : IStringBuilderService
    {
        private readonly StringBuilder _actualStringBuilder;

        public StringBuilder GetS()
        {
            return _actualStringBuilder;
        }
        public StringBuilderService()
        {
            _actualStringBuilder = new StringBuilder();
        }
        public IStringBuilderService Append(string line)
        {
            _actualStringBuilder.Append(line);
            return this;
        }
        public IStringBuilderService AppendLine()
        {
            _actualStringBuilder.AppendLine();
            return this;
        }
        public override string ToString()
        {
            return _actualStringBuilder.ToString();
        }
    }
}
