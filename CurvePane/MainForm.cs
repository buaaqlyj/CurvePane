using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CurvePane
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Control_EventHandler

        #region Github Info
        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/buaaqlyj/CurvePane");
        }
        #endregion
        #region CurveTypeComboBox and CurveTypeTabControl
        private void setCurveTypeComboBox(int index)
        {
            curveTypeComboBox.SelectedIndex = index;
        }
        private void setCurveTypeTabControl(int index)
        {
            curveTypeTabControl.SelectedIndex = index;
        }
        private void curveTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCurveTypeTabControl(curveTypeComboBox.SelectedIndex);
        }
        private void curveTypeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCurveTypeComboBox(curveTypeTabControl.SelectedIndex);
        }
        #endregion
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            curveTypeComboBox.SelectedIndex = 0;
        }

    }
}
