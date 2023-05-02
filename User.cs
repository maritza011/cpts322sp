namespace TeamVaxxers
{
    internal class User
    {
        public string userName = "";
        public string password = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

   
}
