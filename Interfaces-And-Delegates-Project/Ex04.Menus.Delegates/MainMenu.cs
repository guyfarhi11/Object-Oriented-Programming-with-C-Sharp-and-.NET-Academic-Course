namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private readonly MenuItem r_InnerMenu;

        public MainMenu(string i_MainMenuName)
        {
            r_InnerMenu = new InnerMenu(i_MainMenuName);
        }

        public MenuItem InnerMenu
        { 
            get
            {
                return r_InnerMenu;
            }
        }

        public void Show()
        {
            r_InnerMenu.OnOptionChosen();
        }
    }
}
