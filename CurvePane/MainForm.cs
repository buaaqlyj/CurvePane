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
using System.Windows.Forms;

namespace CurvePane
{
    public partial class MainForm : Form
    {
        private CurveManager masterCurveManager = null;
        
        public MainForm()
        {
            InitializeComponent();
        }

        #region Control_EventHandler
        #region PolynomialCurve

        #endregion
        #region Draw

        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please type the curve's name before draw!");
                    return;
                }
                switch (curveTypeComboBox.SelectedIndex)
                {
                    case 0:
                        //Polynomial Curve 多项式插值曲线
                        masterCurveManager.DrawPolynomialCurve(textBox1.Text, comboBox1.SelectedIndex + 1);
                        break;
                    case 1:
                        //三次样条插值曲线
                        break;
                    case 2:
                        //参数样条曲线
                        break;
                    case 3:
                        //Bezier曲线
                        break;
                    case 4:
                        //B样条曲线
                        break;
                    case 5:
                        //NURBS曲线
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
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
        #region FetchBasePoints

        private void fetchControlButton_Click(object sender, EventArgs e)
        {
            if (fetchControlButton.Text == "开始抓取")
            {
                masterCurveManager.CaptureSwitch = true;
                fetchControlButton.Text = "停止抓取";
                drawButton.Enabled = false;
                clearButton.Enabled = false;
            }
            else if (fetchControlButton.Text == "停止抓取")
            {
                masterCurveManager.CaptureSwitch = false;
                fetchControlButton.Text = "开始抓取";
                drawButton.Enabled = true;
                clearButton.Enabled = true;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                double xVal, yVal;
                int index;
                for (int i = listView1.SelectedItems.Count - 1; i > -1; i--)
                {
                    index = listView1.SelectedItems[i].Index;
                    if (double.TryParse(listView1.Items[index].SubItems[1].Text, out xVal) && double.TryParse(listView1.Items[index].SubItems[2].Text, out yVal))
                    {
                        masterCurveManager.RemoveBasePoint(new Util.Variable.DataPoint(xVal, yVal));
                    }
                    listView1.Items.Remove(listView1.Items[index]);
                }
            }
        }

        private void clearPointsButton_Click(object sender, EventArgs e)
        {
            masterCurveManager.ClearBasePoint();
            listView1.Items.Clear();
        }

        private void AddBasePoint(Util.Variable.DataPoint point)
        {
            if (masterCurveManager.CaptureSwitch)
            {
                listView1.Items.Add(new ListViewItem(new string[] {masterCurveManager.BaseNumber.ToString(), point.X.ApproximateString, point.Y.ApproximateString}, -1));
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            masterCurveManager.RemoveAllLines();
        }

        #endregion
        #region CurveManager

        #endregion
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            curveTypeComboBox.SelectedIndex = 0;
            masterCurveManager = new CurveManager(masterZedGraphControl);
            CurveManager.AddBasePointEvent += new ZedGraphTool.ZedGraphWrapper.DoubleClickEventHandler(AddBasePoint);

            listView1.ListViewItemSorter = new ListViewColumnSorter();
            listView1.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);

            comboBox1.SelectedIndex = 0;
        }

    }
}
