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
            this.components = new System.ComponentModel.Container();
            this.curveTypeTabControl = new System.Windows.Forms.TabControl();
            this.pcTabPage = new System.Windows.Forms.TabPage();
            this.csiTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bezierTabPage = new System.Windows.Forms.TabPage();
            this.bsTabPage = new System.Windows.Forms.TabPage();
            this.nurbsTabPage = new System.Windows.Forms.TabPage();
            this.pcsiTabPage = new System.Windows.Forms.TabPage();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.drawButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.curveTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.curveTypeTabControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.curveTypeTabControl.SelectedIndexChanged += new System.EventHandler(curveTypeTabControl_SelectedIndexChanged);
            // 
            // pcTabPage
            // 
            this.pcTabPage.Location = new System.Drawing.Point(4, 22);
            this.pcTabPage.Name = "pcTabPage";
            this.pcTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pcTabPage.Size = new System.Drawing.Size(549, 123);
            this.pcTabPage.TabIndex = 0;
            this.pcTabPage.Text = "多项式插值曲线";
            this.pcTabPage.UseVisualStyleBackColor = true;
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
            // bezierTabPage
            // 
            this.bezierTabPage.Location = new System.Drawing.Point(4, 22);
            this.bezierTabPage.Name = "bezierTabPage";
            this.bezierTabPage.Size = new System.Drawing.Size(549, 123);
            this.bezierTabPage.TabIndex = 2;
            this.bezierTabPage.Text = "Bézier曲线";
            this.bezierTabPage.UseVisualStyleBackColor = true;
            // 
            // bsTabPage
            // 
            this.bsTabPage.Location = new System.Drawing.Point(4, 22);
            this.bsTabPage.Name = "bsTabPage";
            this.bsTabPage.Size = new System.Drawing.Size(549, 123);
            this.bsTabPage.TabIndex = 3;
            this.bsTabPage.Text = "B样条曲线";
            this.bsTabPage.UseVisualStyleBackColor = true;
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
            // pcsiTabPage
            // 
            this.pcsiTabPage.Location = new System.Drawing.Point(4, 22);
            this.pcsiTabPage.Name = "pcsiTabPage";
            this.pcsiTabPage.Size = new System.Drawing.Size(549, 123);
            this.pcsiTabPage.TabIndex = 5;
            this.pcsiTabPage.Text = "参数样条曲线";
            this.pcsiTabPage.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 244);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(781, 401);
            this.zedGraphControl1.TabIndex = 2;
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(591, 146);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 1;
            this.drawButton.Text = "绘制曲线";
            this.drawButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(688, 146);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "清空画布";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(660, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(103, 21);
            this.textBox1.TabIndex = 3;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "曲线类型：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 657);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "产品建模技术实践类大作业 - 曲线画布 - SY1507220 刘欣";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.curveTypeTabControl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox curveTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button drawButton;
    }
}

