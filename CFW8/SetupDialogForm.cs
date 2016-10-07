using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ASCOM.CFW8
{
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
        }

        private void CmdOkClick(object sender, EventArgs e)
        {
            using (ASCOM.Utilities.Profile p = new Utilities.Profile()) { 
                p.DeviceType = "FilterWheel";
                p.WriteValue(FilterWheel.driverId, "ComPort", (string)portsSeries.SelectedItem);
                p.WriteValue(FilterWheel.driverId, "Position1", (string)textBox1.Text);
                p.WriteValue(FilterWheel.driverId, "Position2", (string)textBox2.Text);
                p.WriteValue(FilterWheel.driverId, "Position3", (string)textBox3.Text);
                p.WriteValue(FilterWheel.driverId, "Position4", (string)textBox4.Text);
                p.WriteValue(FilterWheel.driverId, "Position5", (string)textBox5.Text); 
            }

            Dispose();
        }

        private void CmdCancelClick(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BrowseToAscom(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            portsSeries.Items.Clear();
            Utilities.Serial serial = new Utilities.Serial();
            foreach (var item in serial.AvailableCOMPorts)
            {
                portsSeries.Items.Add(item);

            }

            if (portsSeries.Items.Count != 0)
            {
                portsSeries.SelectedIndex = 0;
            }
        }
    }
}