﻿/// Copyright 2016 Troy Lewis. Some Rights Reserved
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
using System.IO;
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
                        //多项式插值曲线
                        masterCurveManager.DrawPolynomialCurve(curveName, comboBox1.SelectedIndex + 1);
                        break;
                    case 1:
                        //三次样条插值曲线
                        masterCurveManager.DrawCSICurve(curveName, comboBox2.SelectedIndex + 1, textBox9.Text, textBox10.Text);
                        break;
                    case 2:
                        //参数样条曲线
                        masterCurveManager.DrawPCSICurve(curveName, comboBox3.SelectedIndex + 1, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text);
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
                        masterCurveManager.DrawNURBSCurve(curveName, textBox7.Text, textBox6.Text, textBox8.Text);
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
                UpdateBSplineCurveSetting();
                UpdateNurbsCurveSetting();
            }
        }

        private void clearPointsButton_Click(object sender, EventArgs e)
        {
            masterCurveManager.ClearBasePoint();
            listView1.Items.Clear();
            UpdateBSplineCurveSetting();
            UpdateNurbsCurveSetting();
        }

        private void AddBasePoint(Util.Variable.DataPoint point)
        {
            if (masterCurveManager.CaptureSwitch)
            {
                listView1.Items.Add(new ListViewItem(new string[] {masterCurveManager.BaseNumber.ToString(), point.X.ApproximateString, point.Y.ApproximateString}, -1));
                UpdateBSplineCurveSetting();
                UpdateNurbsCurveSetting();
            }
        }

        private void DisplayBasePoint(Util.Variable.DataPoint point)
        {
            textBox3.Text = point.ToString() + "   " + masterCurveManager.TransformFromPaneToScreen(point).ToString();
        }

        private void listView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (masterCurveManager.CaptureSwitch)
            {
                string filename = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (!File.Exists(filename))
                {
                    return;
                }
                using (StreamReader sr = new StreamReader(filename))
                {
                    string text;
                    while (!sr.EndOfStream)
                    {
                        text = sr.ReadLine().Trim();
                        if (text.Contains("#"))
                        {
                            text = text.Substring(0, text.IndexOf('#'));
                        }
                        if (text != "")
                        {
                            if (!masterCurveManager.TryAddBasePointFromText(text))
                            {
                                textBox2.AppendText(Environment.NewLine + "Unrecognized text: " + text);
                                return;
                            }
                        }
                    }
                }
                masterCurveManager.UpdatePaneView();
            }
            else
            {
                MessageBox.Show("导入型值点/控制点前，请先点击\"开始抓取\"按钮");
            }
        }
        #endregion
        #region CurveSettings

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    label25.Text = "一阶导数值";
                    label26.Text = "一阶导数值";
                    textBox9.Enabled = true;
                    textBox10.Enabled = true;
                    break;
                case 1:
                    label25.Text = "二阶导数值";
                    label26.Text = "二阶导数值";
                    textBox9.Enabled = true;
                    textBox10.Enabled = true;
                    break;
                case 2:
                    label25.Text = "";
                    label26.Text = "";
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    //指定首尾一阶导数
                    label27.Text = "一阶导数值";
                    label28.Text = "一阶导数值";
                    label29.Text = "";
                    label30.Text = "";
                    textBox11.Enabled = true;
                    textBox12.Enabled = true;
                    textBox13.Enabled = false;
                    textBox14.Enabled = false;
                    break;
                case 1:
                    //首尾端点曲率均为0
                    label27.Text = "";
                    label28.Text = "";
                    label29.Text = "";
                    label30.Text = "";
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    textBox13.Enabled = false;
                    textBox14.Enabled = false;
                    break;
                case 2:
                    //指定首尾端点的曲率中心
                    label27.Text = "曲率中心_x";
                    label28.Text = "曲率中心_x";
                    label29.Text = "曲率中心_y";
                    label30.Text = "曲率中心_y";
                    textBox11.Enabled = true;
                    textBox12.Enabled = true;
                    textBox13.Enabled = true;
                    textBox14.Enabled = true;
                    break;
            }
        }

        public void UpdateBSplineCurveSetting()
        {
            int degree = 0;
            if (!Int32.TryParse(textBox4.Text, out degree))
            {
                MessageBox.Show("The degree is not a integer string: " + textBox4.Text, "B样条曲线次数无法识别");
                return;
            }
            if (degree < 1)
            {
                MessageBox.Show("The degree is invalid: " + textBox4.Text, "B样条曲线次数无法识别");
                return;
            }
            char[] chArray = textBox5.Text.ToCharArray();
            int currentCommaCount = 0;
            int totalCommaCount = degree + masterCurveManager.BasePointsCount;
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

        public void UpdateNurbsCurveSetting()
        {
            int degree = 0;
            if (!Int32.TryParse(textBox7.Text, out degree))
            {
                MessageBox.Show("The degree is not a integer string: " + textBox7.Text, "NURBS曲线次数无法识别");
                return;
            }
            if (degree < 1)
            {
                MessageBox.Show("The degree is invalid: " + textBox7.Text, "NURBS曲线次数无法识别");
                return;
            }
            char[] chArray = textBox6.Text.ToCharArray();
            int currentCommaCount = 0;
            int totalCommaCount = degree + masterCurveManager.BasePointsCount;
            if (checkBox2.Checked)
            {
                int val = 1;
                textBox6.Text = "0";
                for (int i = 0; i < totalCommaCount; i++)
                {
                    textBox6.Text += "," + val++.ToString();
                }
            }
            else
            {
                textBox6.Text = "";
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] == ',')
                    {
                        if (++currentCommaCount > totalCommaCount)
                        {
                            break;
                        }
                    }
                    textBox6.Text += chArray[i];
                }
                while (currentCommaCount++ < totalCommaCount)
                {
                    textBox6.Text += ',';
                }
            }

            chArray = textBox8.Text.ToCharArray();
            currentCommaCount = 0;
            totalCommaCount = masterCurveManager.BasePointsCount - 1;
            if (checkBox3.Checked)
            {
                textBox8.Text = "1";
                for (int i = 0; i < totalCommaCount; i++)
                {
                    textBox8.Text += ",1";
                }
            }
            else
            {
                textBox8.Text = "";
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] == ',')
                    {
                        if (++currentCommaCount > totalCommaCount)
                        {
                            break;
                        }
                    }
                    textBox8.Text += chArray[i];
                }
                while (currentCommaCount++ < totalCommaCount)
                {
                    textBox8.Text += ',';
                }
            }
        }

        private void DegreeChangedForBSplineCurve(object sender, EventArgs e)
        {
            UpdateBSplineCurveSetting();
        }

        private void DegreeChangedForNurbsCurve(object sender, EventArgs e)
        {
            UpdateNurbsCurveSetting();
        }

        private void LockNodesTextBoxForBSplineCurve(object sender, EventArgs e)
        {
            UpdateBSplineCurveSetting();
            if (checkBox1.Checked)
            {
                textBox5.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
            }
        }

        private void LockNodesTextBoxForNurbsCurve(object sender, EventArgs e)
        {
            UpdateNurbsCurveSetting();
            if (checkBox2.Checked)
            {
                textBox6.Enabled = false;
            }
            else
            {
                textBox6.Enabled = true;
            }
        }

        private void LockWeightsTextBoxForNurbsCurve(object sender, EventArgs e)
        {
            UpdateNurbsCurveSetting();
            if (checkBox3.Checked)
            {
                textBox8.Enabled = false;
            }
            else
            {
                textBox8.Enabled = true;
            }
        }

        private void SetMultiplicityForBSplineCurve(object sender, EventArgs e)
        {
            int multCount = masterCurveManager.GetMultiplycityOfNodes(textBox5.Text);
            label9.Text = multCount.ToString();
        }

        private void SetMultiplycityForNurbsCurve(object sender, EventArgs e)
        {
            int multCount = masterCurveManager.GetMultiplycityOfNodes(textBox6.Text);
            label21.Text = multCount.ToString();
        }

        private void SetMaxDegreeForBSplineOrNurbsCurve(object sender, EventArgs e)
        {
            label12.Text = (masterCurveManager.BasePointsCount - 1).ToString();
            label18.Text = (masterCurveManager.BasePointsCount - 1).ToString();
        }
        #endregion
        #region PaneSetting
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                masterCurveManager.SameStepForXY = true;
            }
            else
            {
                masterCurveManager.SameStepForXY = false;
            }
        }
        #endregion
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            curveTypeComboBox.SelectedIndex = 0;
            masterCurveManager = new CurveManager(masterZedGraphControl);
            CurveManager.AddBasePointEvent += new ZedGraphTool.ZedGraphWrapper.DataPointEventHandler(AddBasePoint);
            CurveManager.DisplayBasePointEvent += new ZedGraphTool.ZedGraphWrapper.DataPointEventHandler(DisplayBasePoint);
            CurveManager.BasePointChangedEvent += new EventHandler(SetMaxDegreeForBSplineOrNurbsCurve);

            listView1.ListViewItemSorter = new ListViewColumnSorter();
            listView1.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);

            comboBox1.SelectedIndex = 0;
            textBox1.Text = masterCurveManager.NextAvailableName;

            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
    }
}
