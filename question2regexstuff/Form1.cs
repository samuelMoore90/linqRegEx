using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;



namespace question2regexstuff
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string confernceIdregEx =
               ConfigurationManager.AppSettings["conferenceId"];
            string firstnamereg = ConfigurationManager.AppSettings["names"];
            string lastnameregex = ConfigurationManager.AppSettings["names"];
            string conferenceId = btnConferenceId.Text;
            string fn = btnFirstName.Text;
            string ln = btnSurname.Text;

            if(Regex.IsMatch(conferenceId, confernceIdregEx))
            {
               // MessageBox.Show("Valid Entry");
               if(Regex.IsMatch(ln, lastnameregex) && Regex.IsMatch(fn, firstnamereg))
                {
                    // write to file
                    StreamWriter sw = new StreamWriter(@"C:\Users\samue\Desktop\advprogramming\question2regexstuff\question2regexstuff\people.txt");
                    
                    sw.WriteLine("first Name:" + fn);
                    sw.WriteLine("Lastname: " + ln);
                    sw.WriteLine("ConferenceID: " + conferenceId);
                    sw.WriteLine("Organisation: " + btnOrganistation.Text);
                    sw.Close();

                    using(Stream f = File.Open(@"C:\Users\samue\Desktop\advprogramming\question2regexstuff\conference.bin", FileMode.OpenOrCreate))
                    {
                        
                        using(BinaryWriter bw = new BinaryWriter(f))
                        {
                            try
                            {
                                bw.Write(fn);
                                bw.Write(ln);
                                bw.Write(conferenceId);
                                bw.Write(btnOrganistation.Text);
                                MessageBox.Show("Valid Entry, Contact written to binary file");
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                           
                        }
                    }

                }
               else if(Regex.IsMatch(fn, firstnamereg))
                {
                    MessageBox.Show("lastname wrong");
                    btnSurname.Focus();

                }
                else if(Regex.IsMatch(ln, lastnameregex))
                {
                    MessageBox.Show("firstname wrong");
                    btnFirstName.Focus();

                }
               else
                {
                    MessageBox.Show("Both Names are wrong");
                    btnSurname.Focus();
                }
            }
            else
            {
                MessageBox.Show("Invalid Conference Id");
                btnConferenceId.Focus(); ;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            using (Stream f = File.Open(@"C:\Users\samue\Desktop\advprogramming\question2regexstuff\conference.bin", FileMode.Open))
            {
                using(BinaryReader br = new BinaryReader(f))
                {
                   string fn =  br.ReadString();
                   string ln = br.ReadString();
                    string o = br.ReadString();
                    var names = fn + " " + ln + " " + o + Environment.NewLine;
                    
                    richTextBox1.AppendText(names);
                }
            }
        }
    }
}
