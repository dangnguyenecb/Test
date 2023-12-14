using ECBIM.API.Extensions;

namespace ECBIM.APIUI
{
    public static class ConsoleAPI
    {
        public static ResultBox Box { get; set; }
        private static bool IsShown { get; set; } = false;

        private static void ShowNewResultBox()
        {
            Box = new ResultBox();
            Box.Title = "Résultats";
            Box.Show();
            IsShown = true;
        }

        public static void Dispose()
        {
            IsShown = false;
        }

        public static void WriteLine(string text)
        {
            if (!IsShown)
                ShowNewResultBox();

            Box.MainContent += text + "\n";
        }

        public static void Start(string title)
        {
            if (!IsShown)
                ShowNewResultBox();

            Box.CustomTitle = title;
        }

        public static void WriteLine(string title, string text)
        {
            if (!IsShown)
                ShowNewResultBox();

            Box.CustomTitle = title;
            Box.MainContent += text + "\n";
        }

        public static void WriteEmptyLine()
        {
            Box.MainContent += "\n";
        }

        public static void WriteLightSeparator()
        {
            Box.MainContent += new string('-', 60);
            Box.MainContent += "\n";
        }

        public static void WriteMediumSeparator()
        {
            Box.MainContent += new string('=', 40);
            Box.MainContent += "\n";
        }

        public static void WriteMediumTitle(string title)
        {
            Box.MainContent += title.Center(40, '=');
            Box.MainContent += "\n";
        }

        public static void WriteStrongSeparator()
        {
            Box.MainContent += new string('#', 40);
            Box.MainContent += "\n";
        }

        public static void WriteStrongTitle(string title)
        {
            Box.MainContent += title.Center(40, '#');
            Box.MainContent += "\n";
        }
    }
}
