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
        #region Curve
        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please type the curve's name before draw!");
                    return;
                }
                if (masterCurveManager.ContainCurveName(textBox1.Text))
                {
                    MessageBox.Show("This curve's name has been used before! Please change another one.");
                    return;
                }
                string curveName = textBox1.Text;
                switch (curveTypeComboBox.SelectedIndex)
                {
                    case 0:
                        //Polynomial Curve 多项式插值曲线
                        masterCurveManager.DrawPolynomialCurve(curveName, comboBox1.SelectedIndex + 1);
                        break;
                    case 1:
                        //三次样条插值曲线
                        break;
                    case 2:
                        //参数样条曲线
                        break;
                    case 3:
                        //Bezier曲线
                        masterCurveManager.DrawBezierCurve(curveName);
                        break;
                    case 4:
                        //B样条曲线
                        masterCurveManager.DrawBSplineCurve(curveName, textBox4.Text, textBox5.Text);
                        break;
                    case 5:
                        //NURBS曲线
                        break;
                }
                textBox1.Text = masterCurveManager.NextAvailableName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            masterCurveManager.RemoveAllLines();
            textBox1.Text = masterCurveManager.NextAvailableName;
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
        #region BasePoints
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
                UpdateBSplineCurveSetting(masterCurveManager.BasePointsCount, textBox4.Text);
            }
        }

        private void clearPointsButton_Click(object sender, EventArgs e)
        {
            masterCurveManager.ClearBasePoint();
            listView1.Items.Clear();
            UpdateBSplineCurveSetting(masterCurveManager.BasePointsCount, textBox4.Text);
        }

        private void AddBasePoint(Util.Variable.DataPoint point)
        {
            if (masterCurveManager.CaptureSwitch)
            {
                listView1.Items.Add(new ListViewItem(new string[] {masterCurveManager.BaseNumber.ToString(), point.X.ApproximateString, point.Y.ApproximateString}, -1));
                UpdateBSplineCurveSetting(masterCurveManager.BasePointsCount, textBox4.Text);
            }
        }

        private void DisplayBasePoint(Util.Variable.DataPoint point)
        {
            textBox3.Text = point.String;
        }
        #endregion
        #region CurveSettings
        public void UpdateBSplineCurveSetting(int count, string degree)
        {
            int degreeInt = 0;
            if (!Int32.TryParse(degree, out degreeInt))
            {
                MessageBox.Show("The degree is not a integer string: " + degree, "曲线次数无法识别");
                return;
            }
            if (degreeInt < 1)
            {
                MessageBox.Show("The degree is invalid: " + degree, "曲线次数无法识别");
                return;
            }
            char[] chArray = textBox5.Text.ToCharArray();
            int currentCommaCount = 0;
            int totalCommaCount = degreeInt + count;
            if (checkBox1.Checked)
            {
                int val = 1;
                textBox5.Text = "0";
                for (int i = 0; i < totalCommaCount; i++)
                {
                    textBox5.Text += "," + val++.ToString();
                }
            }
            else
            {
                textBox5.Text = "";
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] == ',')
                    {
                        if (++currentCommaCount > totalCommaCount)
                        {
                            break;
                        }
                    }
                    textBox5.Text += chArray[i];
                }
                while (currentCommaCount++ < totalCommaCount)
                {
                    textBox5.Text += ',';
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            UpdateBSplineCurveSetting(masterCurveManager.BasePointsCount, textBox4.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBSplineCurveSetting(masterCurveManager.BasePointsCount, textBox4.Text);
        }
        #endregion
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            curveTypeComboBox.SelectedIndex = 0;
            masterCurveManager = new CurveManager(masterZedGraphControl);
            CurveManager.AddBasePointEvent += new ZedGraphTool.ZedGraphWrapper.DataPointEventHandler(AddBasePoint);
            CurveManager.DisplayBasePointEvent += new ZedGraphTool.ZedGraphWrapper.DataPointEventHandler(DisplayBasePoint);

            listView1.ListViewItemSorter = new ListViewColumnSorter();
            listView1.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);

            comboBox1.SelectedIndex = 0;
            textBox1.Text = masterCurveManager.NextAvailableName;

            checkBox1.Checked = true;
        }
    }
}
