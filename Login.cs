using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;



namespace TeamVaxxers
{
 //   public override System.Drawing.Color BackColor { get; set; }

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Button createAccountButton = new Button();
            createAccountButton.BackColor = Color.BlueViolet;
            createAccountButton.Text = "Create";
        //    createAccountButton.Left=
          //  this.Controls.Add(createAccountButton);

        }
        private void createLogin(object sender, EventArgs e)
        {
            User user = new User();
            if (user.UserName == usernameBox.Text && user.Password == passwordBox.Text)
            {
                this.Hide();
                ParkingLot engine = new ParkingLot();
                engine.ShowDialog();
               // MessageBox.Show("WELCOME TO SMART PARKING!"); 
                this.Close();


            }
            else
            {
 
             
                label.Text = "Wrong Username or Password";
                //    usernameBox.Text = passwordBox.Text = "";
                // string textToEnter = "Wrong Username or Password\n Please contact maritza.ochoa-roman@wsu.edu for login details";
                // (String.Format("{0," +  / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
                // Console.Read();
                CreateMyForm();
            }

        }
        private void loginBtn_Click_1(object sender, EventArgs e)
        {
            User user = new User();
            //public override System.Drawing.Color BackColor { get; set; }
   

            if (user.UserName == usernameBox.Text && user.Password == passwordBox.Text)
            {
                this.Hide();

                ParkingLot engine = new ParkingLot();
                engine.ShowDialog();
                this.Close();

            }
            else
            {
                label.Text = "*Wrong Username or Password*";

                //if the username and passsword are wrong it will show them a box telling them how to fix it 
                CreateMyForm(); 
            }




        }


        /*Trying to make a help button for username and password helpt*/
        //If username and passwords are wrong the user cannot change them
        //they have to contact me if they want to change their passwords. 
        public void CreateMyForm()
        {

            // Create a new instance of the form.
            Form form1 = new Form();
            // Create a label control to display the text.
            Label label1 = new Label();
            // Set the text of the label.
            label1.Text = "Please re-enter password. For assistance please contact developers.\n " +
                                "Email: maritza.ochoa-roman@wsu.edu\n" + "Cell: 509-790-9800";
                    
            // Set the position of the label on the form.
           // label1.Location = Point(50, 50);

            // Add the label to the form.
            form1.Controls.Add(label1);


       
            //// Display the form as a modal dialog box.
           form1.ShowDialog();
        }


        private void passwordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
