namespace AlBarakaPOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }
        public void isActive(Control Btn)
        {
            if (Btn.Name == saleBtn.Name)
            {
                saleBtn.IsActive = true;
                DailyByn.IsActive = false;
                ReportsBtn.IsActive = false;
                ClintesBtn.IsActive = false;
                SettingsBtn.IsActive = false;
                ChangpasswordBtn.IsActive = false;
            }
            else if (Btn.Name == DailyByn.Name)
            {
                saleBtn.IsActive = false;
                DailyByn.IsActive = true;
                ReportsBtn.IsActive = false;
                ClintesBtn.IsActive = false;
                SettingsBtn.IsActive = false;
                ChangpasswordBtn.IsActive = false;
            }
            else if (Btn.Name == ReportsBtn.Name)
            {
                saleBtn.IsActive = false;
                DailyByn.IsActive = false;
                ReportsBtn.IsActive = true;
                ClintesBtn.IsActive = false;
                SettingsBtn.IsActive = false;
                ChangpasswordBtn.IsActive = false;
            }
            else if (Btn.Name == ClintesBtn.Name) 
            {
                saleBtn.IsActive = false;
                DailyByn.IsActive = false;
                ReportsBtn.IsActive = false;
                ClintesBtn.IsActive = true;
                SettingsBtn.IsActive = false;
                ChangpasswordBtn.IsActive = false;
            }
            else if (Btn.Name == SettingsBtn.Name) 
            {
                saleBtn.IsActive = false;
                DailyByn.IsActive = false;
                ReportsBtn.IsActive = false;
                ClintesBtn.IsActive = false;
                SettingsBtn.IsActive = true;
                ChangpasswordBtn.IsActive = false;
            }
            else if (Btn.Name == ChangpasswordBtn.Name)
            {
                saleBtn.IsActive = false;
                DailyByn.IsActive = false;
                ReportsBtn.IsActive = false;
                ClintesBtn.IsActive = false;
                SettingsBtn.IsActive = false;
                ChangpasswordBtn.IsActive = true;
            }
        }
        private void saleBtn_Click(object sender, EventArgs e)
        {
            isActive(saleBtn);
            panel3.Show();
            panel2.Show();
            panel3.Update();
            panel2.Update();

        }

        private void DailyByn_Click(object sender, EventArgs e)
        {
            isActive(DailyByn);
            // show form
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            isActive(ReportsBtn);
            panel3.Hide();
            panel2.Hide();
        }

        private void ClintesBtn_Click(object sender, EventArgs e)
        {
            isActive(ClintesBtn);

        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            isActive(SettingsBtn);

        }

        private void ChangpasswordBtn_Click(object sender, EventArgs e)
        {
            isActive(ChangpasswordBtn);

        }

        private void StartShiftBtn_Click(object sender, EventArgs e)
        {

        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
