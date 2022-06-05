using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium.Safari;
using System.Web;
using System.Globalization;

namespace aztecapka1
{
    public partial class SkanApkaMD : Form
    {
        public SkanApkaMD()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else

                    {
                        func(control.Controls);
                    }
                }

            };
            func(Controls);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idpojazdu = textID.Text;
            var decoder = new PolishVehicleRegistrationCertificateDecoder.Decoder();
            var vehicleRegistrationInfo = decoder.Decode(idpojazdu);

            if (string.IsNullOrEmpty(textBox6.Text))
            {
            textBox6.AppendText(vehicleRegistrationInfo.MarkaPojazdu);
            textBox1.AppendText(vehicleRegistrationInfo.VinNrNadwozia);
            textBox3.AppendText(vehicleRegistrationInfo.NrRejestracyjny);
            textBox4.AppendText(vehicleRegistrationInfo.DataWydaniaAktualnegoDr);
            textBox2.AppendText(vehicleRegistrationInfo.DataPierwszejRejestracji);
            textBox7.AppendText(vehicleRegistrationInfo.NazwiskoWlascicielaPojazdu);
            textBox8.AppendText(vehicleRegistrationInfo.NazwaWlascicielaPojazdu);
            textBox5.AppendText(vehicleRegistrationInfo.ModelPojazdu);
            }
            string message = "Dodano dane";
            DialogResult result;

            result = MessageBox.Show(message);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }

            else
            {
                textBox6.Clear();
                textBox1.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox2.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox5.Clear();

                textBox6.AppendText(vehicleRegistrationInfo.MarkaPojazdu);
                textBox1.AppendText(vehicleRegistrationInfo.VinNrNadwozia);
                textBox3.AppendText(vehicleRegistrationInfo.NrRejestracyjny);
                textBox4.AppendText(vehicleRegistrationInfo.DataWydaniaAktualnegoDr);
                textBox2.AppendText(vehicleRegistrationInfo.DataPierwszejRejestracji);
                textBox7.AppendText(vehicleRegistrationInfo.NazwiskoWlascicielaPojazdu);
                textBox8.AppendText(vehicleRegistrationInfo.NazwaWlascicielaPojazdu);
                textBox5.AppendText(vehicleRegistrationInfo.ModelPojazdu);

            }

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;



            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://historiapojazdu.gov.pl/");

            var NRREJESTR = driver.FindElement(By.Id("_historiapojazduportlet_WAR_historiapojazduportlet_:rej"));
            var VIN = driver.FindElement(By.Id("_historiapojazduportlet_WAR_historiapojazduportlet_:vin"));
            var data = driver.FindElement(By.Id("_historiapojazduportlet_WAR_historiapojazduportlet_:data"));
            var loginButton = driver.FindElement(By.XPath("//input[@value='Sprawdź pojazd »']"));


            NRREJESTR.SendKeys(vehicleRegistrationInfo.NrRejestracyjny);
            VIN.SendKeys(vehicleRegistrationInfo.VinNrNadwozia);
            var date = Convert.ToDateTime(vehicleRegistrationInfo.DataPierwszejRejestracji);
         string dataToTrim= date.ToString("dd.MM.yyyy");
            data.Click();
            data.SendKeys((dataToTrim));
            loginButton.Click();

            var titles = driver.FindElements(By.ClassName("oc"));



            foreach (var title in titles)


                Console.WriteLine(title.Text);

            var oc = driver.FindElement(By.ClassName("oc")).GetAttribute("innerText");
            Console.WriteLine(oc);

            var badanieTech = driver.FindElement(By.ClassName("tech")).GetAttribute("innerText");
            Console.WriteLine(badanieTech);


            var osczasu = driver.FindElement(By.Id("raport-content-template-timeline-button"));

            osczasu.Click();

            var test1 = driver.FindElement(By.ClassName("summary-box")).GetAttribute("innerText");
            Console.WriteLine(test1);


            var test2 = driver.FindElement(By.XPath("//p[contains(text(), 'Polisa OC: ')]/span")).GetAttribute("innerText");
            Console.WriteLine(test2);

            //div[text()='Ingredients:']/following-sibling::div/span[contains(@class,'cancel-icon')]

            //div[@class='ingredients-container-header-close']/span[@class='material-icons cancel-icon ']
            //("//div[@class='item-inner']/span[@class='title']"));
            //Console.ReadKey();
            //*[@id="timeline-summary-box"]/div[2]/p[6]




            Thread.Sleep(3000);
            driver.Quit();


        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
        ErrorProvider errorProvider = new ErrorProvider();
        private void textID_Validating(object sender, CancelEventArgs e)

        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SkanApkaMD_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=NOTEBOOK-MD\MSSQLSERVER;Initial Catalog=test;Integrated Security=True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into test100(nameid, id, imie) values('" + 1 + "','" + 1 + "','" + textBox1.Text + "')", conn);
            int i = cmd.ExecuteNonQuery();

            if (i != 0)
            {
                MessageBox.Show("poszlo");

            }
            else
            {
                MessageBox.Show("lipa");
            }
        }
    }

}

