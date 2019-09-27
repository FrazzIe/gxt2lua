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
using System.Diagnostics;

namespace gxt2lua
{
    public partial class frmMain : Form
    {
        //Declare global vars
        Regex regex;
        string github = "https://github.com/FrazzIe";

        public frmMain()
        {
            InitializeComponent();
            regex = new Regex(@"(0[xX][0-9a-fA-F]+)(\s=\s)(\w+.*)", RegexOptions.Multiline); //Init regex obj
        }

        private void InputText_TextChanged(object sender, EventArgs e) //Event triggered when the input text is changed
        {
            string output = ""; //new output
            int count = 0; //counter
            MatchCollection matches = regex.Matches(inputText.Text); //collection of regex matchs

            if (matches.Count > 0) //Check if theres any matches
            {
                foreach (Match match in matches) //Loop through each match
                {
                    GroupCollection groups = match.Groups; //collection of groups in a regex match

                    if (groups.Count >= 4) //check if there are enough groups in a match to continue
                    {
                        if (count > 0) //Check if this isn't the first match
                        {
                            output += "\r\n"; //Add new line
                        }

                        var hash = groups[1].Value; //Text Entry Hash
                        var value = Regex.Replace(groups[3].Value, @"\t|\n|\r", ""); //Text Entry Value

                        output += "AddTextEntryByHash(" + hash + ", \"" + value + "\")\r\n"; //Add match to output

                        count++; //increment match counter
                    }
                }
            }

            if (string.IsNullOrEmpty(output)) //Check if input is empty
            {
                outputText.Text = "No matches found?"; //Send error msg
            } else
            {
                outputText.Text = output; //Send formatted list of matches
            }
            
        }

        private void CreditLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //Event triggered on linked label click
        {
            Process.Start(github); //Open link to github
        }
    }
}
