using Ex04.Menus.Delegates;
using Ex04.Menus.Test.Actions;

namespace Ex04.Menus.Test
{
    internal class CreateMenuDelegate
    {
        private MainMenu m_MainMenu;

        public CreateMenuDelegate()
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
            countCapitalsMethodOption.OptionChosen += new CountCapital().Action;
            showVersionMethodOption.OptionChosen += new ShowVersion().Action;
            InnerMenu showDateOrTimeMenu = new InnerMenu("Show Date/Time");
            MethodOption showTimeMethodOption = new MethodOption("Show Time");
            MethodOption showDateMethodOption = new MethodOption("Show Date");
            showTimeMethodOption.OptionChosen += new ShowTimeOrDate(TimeTypes.eTypeOfTime.Time).Action;
            showDateMethodOption.OptionChosen += new ShowTimeOrDate(TimeTypes.eTypeOfTime.Date).Action;
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
