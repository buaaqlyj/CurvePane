/// Copyright 2016 Troy Lewis. Some Rights Reserved
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
///     http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

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
        private void infoLabel_Click(object sender, EventArgs e)
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
