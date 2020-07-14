using Ex04.Menus.Interfaces;
using Ex04.Menus.Test.Actions;

namespace Ex04.Menus.Test
{
    internal class CreateMenuInterface
    {
        private MainMenu m_MainMenu;

        public CreateMenuInterface()
        {
            m_MainMenu = new MainMenu("Main Menu");
            Create();
        }

        public void Create()
        {
            InnerMenu inMain = (InnerMenu)m_MainMenu.InnerMenu;
            InnerMenu versionAndCapitalsMenu = new InnerMenu("Version and Capitals");
            MethodOption countCapitalsMethodOption = new MethodOption("Count Capitals");
            MethodOption showVersionMethodOption = new MethodOption("Show Version");
            showVersionMethodOption.Action = new ShowVersion();
            countCapitalsMethodOption.Action = new CountCapital();
            InnerMenu showDateOrTimeMenu = new InnerMenu("Show Date/Time");
            MethodOption showTimeMethodOption = new MethodOption("Show Time");
            MethodOption showDateMethodOption = new MethodOption("Show Date");
            showTimeMethodOption.Action = new ShowTimeOrDate(TimeTypes.eTypeOfTime.Time);
            showDateMethodOption.Action = new ShowTimeOrDate(TimeTypes.eTypeOfTime.Date);
            versionAndCapitalsMenu.AddToMenu(countCapitalsMethodOption);
            versionAndCapitalsMenu.AddToMenu(showVersionMethodOption);
            showDateOrTimeMenu.AddToMenu(showTimeMethodOption);
            showDateOrTimeMenu.AddToMenu(showDateMethodOption);
            inMain.AddToMenu(versionAndCapitalsMenu);
            inMain.AddToMenu(showDateOrTimeMenu);
            m_MainMenu.Show();
        }
    }
}