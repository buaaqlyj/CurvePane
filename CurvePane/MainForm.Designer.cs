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

namespace CurvePane
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.curveTypeTabControl = new System.Windows.Forms.TabControl();
            this.pcTabPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.csiTabPage = new System.Windows.Forms.TabPage();
            this.pcsiTabPage = new System.Windows.Forms.TabPage();
            this.bezierTabPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.bsTabPage = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nurbsTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.curveTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.masterZedGraphControl = new ZedGraph.ZedGraphControl();
            this.infoLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clearPointsButton = new System.Windows.Forms.Button();
            this.fetchControlButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.curveTypeTabControl.SuspendLayout();
            this.pcTabPage.SuspendLayout();
            this.bezierTabPage.SuspendLayout();
            this.bsTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // curveTypeTabControl
            // 
            this.curveTypeTabControl.Controls.Add(this.pcTabPage);
            this.curveTypeTabControl.Controls.Add(this.csiTabPage);
            this.curveTypeTabControl.Controls.Add(this.pcsiTabPage);
            this.curveTypeTabControl.Controls.Add(this.bezierTabPage);
            this.curveTypeTabControl.Controls.Add(this.bsTabPage);
            this.curveTypeTabControl.Controls.Add(this.nurbsTabPage);
            this.curveTypeTabControl.Location = new System.Drawing.Point(6, 20);
            this.curveTypeTabControl.Name = "curveTypeTabControl";
            this.curveTypeTabControl.SelectedIndex = 0;
            this.curveTypeTabControl.Size = new System.Drawing.Size(557, 149);
            this.curveTypeTabControl.TabIndex = 0;
            this.curveTypeTabControl.SelectedIndexChanged += new System.EventHandler(this.curveTypeTabControl_SelectedIndexChanged);
            // 
            // pcTabPage
            // 
            this.pcTabPage.Controls.Add(this.label4);
            this.pcTabPage.Controls.Add(this.comboBox1);
            this.pcTabPage.Location = new System.Drawing.Point(4, 22);
            this.pcTabPage.Name = "pcTabPage";
            this.pcTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pcTabPage.Size = new System.Drawing.Size(549, 123);
            this.pcTabPage.TabIndex = 0;
            this.pcTabPage.Text = "多项式插值曲线";
            this.pcTabPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "多项式插值方法：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "拉格朗日一次插值",
            "拉格朗日二次插值",
            "牛顿插值"});
            this.comboBox1.Location = new System.Drawing.Point(127, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // csiTabPage
            // 
            this.csiTabPage.Location = new System.Drawing.Point(4, 22);
            this.csiTabPage.Name = "csiTabPage";
            this.csiTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.csiTabPage.Size = new System.Drawing.Size(549, 123);
            this.csiTabPage.TabIndex = 1;
            this.csiTabPage.Text = "三次样条插值曲线";
            this.csiTabPage.UseVisualStyleBackColor = true;
            // 
            // pcsiTabPage
            // 
            this.pcsiTabPage.Location = new System.Drawing.Point(4, 22);
            this.pcsiTabPage.Name = "pcsiTabPage";
            this.pcsiTabPage.Size = new System.Drawing.Size(549, 123);
            this.pcsiTabPage.TabIndex = 5;
            this.pcsiTabPage.Text = "参数样条曲线";
            this.pcsiTabPage.UseVisualStyleBackColor = true;
            // 
            // bezierTabPage
            // 
            this.bezierTabPage.Controls.Add(this.label5);
            this.bezierTabPage.Location = new System.Drawing.Point(4, 22);
            this.bezierTabPage.Name = "bezierTabPage";
            this.bezierTabPage.Size = new System.Drawing.Size(549, 123);
            this.bezierTabPage.TabIndex = 2;
            this.bezierTabPage.Text = "Bézier曲线";
            this.bezierTabPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Bezier曲线绘制不需要额外的参数";
            // 
            // bsTabPage
            // 
            this.bsTabPage.Controls.Add(this.label13);
            this.bsTabPage.Controls.Add(this.label12);
            this.bsTabPage.Controls.Add(this.label11);
            this.bsTabPage.Controls.Add(this.label10);
            this.bsTabPage.Controls.Add(this.label9);
            this.bsTabPage.Controls.Add(this.label8);
            this.bsTabPage.Controls.Add(this.checkBox1);
            this.bsTabPage.Controls.Add(this.textBox5);
            this.bsTabPage.Controls.Add(this.textBox4);
            this.bsTabPage.Controls.Add(this.label7);
            this.bsTabPage.Controls.Add(this.label6);
            this.bsTabPage.Location = new System.Drawing.Point(4, 22);
            this.bsTabPage.Name = "bsTabPage";
            this.bsTabPage.Size = new System.Drawing.Size(549, 123);
            this.bsTabPage.TabIndex = 3;
            this.bsTabPage.Text = "B样条曲线";
            this.bsTabPage.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(105, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "均匀节点矢量";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(105, 39);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(425, 21);
            this.textBox5.TabIndex = 3;
            this.textBox5.LostFocus += new System.EventHandler(textBox5_LostFocus);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(105, 13);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(66, 21);
            this.textBox4.TabIndex = 2;
            this.textBox4.Text = "2";
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "节点矢量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "曲线次数：";
            // 
            // nurbsTabPage
            // 
            this.nurbsTabPage.Location = new System.Drawing.Point(4, 22);
            this.nurbsTabPage.Name = "nurbsTabPage";
            this.nurbsTabPage.Size = new System.Drawing.Size(549, 123);
            this.nurbsTabPage.TabIndex = 4;
            this.nurbsTabPage.Text = "NURBS曲线";
            this.nurbsTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.curveTypeComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.clearButton);
            this.groupBox1.Controls.Add(this.drawButton);
            this.groupBox1.Controls.Add(this.curveTypeTabControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 181);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "曲线类型：";
            // 
            // curveTypeComboBox
            // 
            this.curveTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.curveTypeComboBox.FormattingEnabled = true;
            this.curveTypeComboBox.Items.AddRange(new object[] {
            "多项式插值曲线",
            "三次样条插值曲线",
            "参数样条曲线",
            "Bézier曲线",
            "B样条曲线",
            "NURBS曲线"});
            this.curveTypeComboBox.Location = new System.Drawing.Point(660, 20);
            this.curveTypeComboBox.Name = "curveTypeComboBox";
            this.curveTypeComboBox.Size = new System.Drawing.Size(103, 20);
            this.curveTypeComboBox.TabIndex = 5;
            this.curveTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.curveTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(589, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "曲线名称：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(660, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(103, 21);
            this.textBox1.TabIndex = 3;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(688, 73);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "清空画布";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(591, 73);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 1;
            this.drawButton.Text = "绘制曲线";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // masterZedGraphControl
            // 
            this.masterZedGraphControl.Location = new System.Drawing.Point(12, 199);
            this.masterZedGraphControl.Name = "masterZedGraphControl";
            this.masterZedGraphControl.ScrollGrace = 0D;
            this.masterZedGraphControl.ScrollMaxX = 0D;
            this.masterZedGraphControl.ScrollMaxY = 0D;
            this.masterZedGraphControl.ScrollMaxY2 = 0D;
            this.masterZedGraphControl.ScrollMinX = 0D;
            this.masterZedGraphControl.ScrollMinY = 0D;
            this.masterZedGraphControl.ScrollMinY2 = 0D;
            this.masterZedGraphControl.Size = new System.Drawing.Size(781, 434);
            this.masterZedGraphControl.TabIndex = 2;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoLabel.Location = new System.Drawing.Point(322, 641);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(425, 12);
            this.infoLabel.TabIndex = 3;
            this.infoLabel.Text = "Under Apache License 2.0 Github: https://github.com/buaaqlyj/CurvePane";
            this.infoLabel.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deleteButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.clearPointsButton);
            this.groupBox2.Controls.Add(this.fetchControlButton);
            this.groupBox2.Location = new System.Drawing.Point(799, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 308);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "型值点/控制点";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(100, 20);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "删除该点";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "消息：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(53, 49);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(215, 21);
            this.textBox3.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.xColumnHeader,
            this.yColumnHeader});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 76);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(262, 226);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // idColumnHeader
            // 
            this.idColumnHeader.Text = "序号";
            this.idColumnHeader.Width = 53;
            // 
            // xColumnHeader
            // 
            this.xColumnHeader.Text = "X";
            this.xColumnHeader.Width = 93;
            // 
            // yColumnHeader
            // 
            this.yColumnHeader.Text = "Y";
            this.yColumnHeader.Width = 91;
            // 
            // clearPointsButton
            // 
            this.clearPointsButton.Location = new System.Drawing.Point(193, 20);
            this.clearPointsButton.Name = "clearPointsButton";
            this.clearPointsButton.Size = new System.Drawing.Size(75, 23);
            this.clearPointsButton.TabIndex = 1;
            this.clearPointsButton.Text = "清空点集";
            this.clearPointsButton.UseVisualStyleBackColor = true;
            this.clearPointsButton.Click += new System.EventHandler(this.clearPointsButton_Click);
            // 
            // fetchControlButton
            // 
            this.fetchControlButton.Location = new System.Drawing.Point(8, 20);
            this.fetchControlButton.Name = "fetchControlButton";
            this.fetchControlButton.Size = new System.Drawing.Size(75, 23);
            this.fetchControlButton.TabIndex = 0;
            this.fetchControlButton.Text = "开始抓取";
            this.fetchControlButton.UseVisualStyleBackColor = true;
            this.fetchControlButton.Click += new System.EventHandler(this.fetchControlButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Location = new System.Drawing.Point(799, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 308);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "消息";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 20);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(262, 282);
            this.textBox2.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(390, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "节点矢量重复度为：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(509, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(191, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "当前条件允许的最低次数：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(390, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "最高次数：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(346, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(461, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 662);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.masterZedGraphControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "产品建模技术实践类大作业 - 曲线画布";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.curveTypeTabControl.ResumeLayout(false);
            this.pcTabPage.ResumeLayout(false);
            this.pcTabPage.PerformLayout();
            this.bezierTabPage.ResumeLayout(false);
            this.bezierTabPage.PerformLayout();
            this.bsTabPage.ResumeLayout(false);
            this.bsTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl curveTypeTabControl;
        private System.Windows.Forms.TabPage pcTabPage;
        private System.Windows.Forms.TabPage csiTabPage;
        private System.Windows.Forms.TabPage pcsiTabPage;
        private System.Windows.Forms.TabPage bezierTabPage;
        private System.Windows.Forms.TabPage bsTabPage;
        private System.Windows.Forms.TabPage nurbsTabPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private ZedGraph.ZedGraphControl masterZedGraphControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox curveTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader idColumnHeader;
        private System.Windows.Forms.ColumnHeader xColumnHeader;
        private System.Windows.Forms.ColumnHeader yColumnHeader;
        private System.Windows.Forms.Button clearPointsButton;
        private System.Windows.Forms.Button fetchControlButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}

