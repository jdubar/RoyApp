using RoyApp.interfaces;
using System.Text;

namespace RoyApp.Services
{
    public class StringService
    {
        public class StringBuilderService : IStringBuilderService
        {
            private readonly StringBuilder _actualStringBuilder;

            public StringBuilder GetS()
            { return _actualStringBuilder; }
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
}
