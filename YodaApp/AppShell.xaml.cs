using YodaApp.Views;

namespace YodaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("yodapageview", typeof(YodaPageView));
           // Routing.RegisterRoute("loadsheddinganswer", typeof(LoadsheddingAnswerPage));
        }
    }
}
