﻿
namespace SQLFormat
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtSource = new System.Windows.Forms.RichTextBox();
            this.rtxtTarget = new System.Windows.Forms.RichTextBox();
            this.btnFormat = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBtn = new System.Windows.Forms.TableLayoutPanel();
            this.cboxDefine = new System.Windows.Forms.CheckBox();
            this.cboxUpper = new System.Windows.Forms.CheckBox();
            this.cboxSQL = new System.Windows.Forms.CheckBox();
            this.cboxParam = new System.Windows.Forms.CheckBox();
            this.cboxNull2 = new System.Windows.Forms.CheckBox();
            this.cboxNull = new System.Windows.Forms.CheckBox();
            this.cboxReplace = new System.Windows.Forms.CheckBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.tlpSqlResult = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtBoxGauss = new System.Windows.Forms.RichTextBox();
            this.rtxtBoxOracle = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlpMain.SuspendLayout();
            this.tlpBtn.SuspendLayout();
            this.tlpSqlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtSource
            // 
            this.rtxtSource.Location = new System.Drawing.Point(3, 3);
            this.rtxtSource.Name = "rtxtSource";
            this.rtxtSource.Size = new System.Drawing.Size(182, 199);
            this.rtxtSource.TabIndex = 0;
            this.rtxtSource.Text = "";
            // 
            // rtxtTarget
            // 
            this.rtxtTarget.Location = new System.Drawing.Point(324, 3);
            this.rtxtTarget.Name = "rtxtTarget";
            this.rtxtTarget.Size = new System.Drawing.Size(182, 199);
            this.rtxtTarget.TabIndex = 1;
            this.rtxtTarget.Text = "";
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(3, 303);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(68, 29);
            this.btnFormat.TabIndex = 2;
            this.btnFormat.Text = "Format";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.tlpBtn, 1, 0);
            this.tlpMain.Controls.Add(this.rtxtSource, 0, 0);
            this.tlpMain.Controls.Add(this.rtxtTarget, 2, 0);
            this.tlpMain.Location = new System.Drawing.Point(20, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(522, 433);
            this.tlpMain.TabIndex = 4;
            // 
            // tlpBtn
            // 
            this.tlpBtn.ColumnCount = 1;
            this.tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtn.Controls.Add(this.cboxDefine, 0, 6);
            this.tlpBtn.Controls.Add(this.cboxUpper, 0, 0);
            this.tlpBtn.Controls.Add(this.btnFormat, 0, 10);
            this.tlpBtn.Controls.Add(this.cboxSQL, 0, 8);
            this.tlpBtn.Controls.Add(this.cboxParam, 0, 5);
            this.tlpBtn.Controls.Add(this.cboxNull2, 0, 4);
            this.tlpBtn.Controls.Add(this.cboxNull, 0, 3);
            this.tlpBtn.Controls.Add(this.cboxReplace, 0, 1);
            this.tlpBtn.Controls.Add(this.txtReplace, 0, 2);
            this.tlpBtn.Controls.Add(this.btnSetting, 0, 9);
            this.tlpBtn.Location = new System.Drawing.Point(204, 3);
            this.tlpBtn.Name = "tlpBtn";
            this.tlpBtn.RowCount = 11;
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtn.Size = new System.Drawing.Size(114, 396);
            this.tlpBtn.TabIndex = 5;
            // 
            // cboxDefine
            // 
            this.cboxDefine.AutoSize = true;
            this.cboxDefine.Location = new System.Drawing.Point(3, 183);
            this.cboxDefine.Name = "cboxDefine";
            this.cboxDefine.Size = new System.Drawing.Size(108, 16);
            this.cboxDefine.TabIndex = 11;
            this.cboxDefine.Text = "${Param} -> \'\'";
            this.cboxDefine.UseVisualStyleBackColor = true;
            // 
            // cboxUpper
            // 
            this.cboxUpper.AutoSize = true;
            this.cboxUpper.Location = new System.Drawing.Point(3, 3);
            this.cboxUpper.Name = "cboxUpper";
            this.cboxUpper.Size = new System.Drawing.Size(66, 16);
            this.cboxUpper.TabIndex = 6;
            this.cboxUpper.Text = "ToUpper";
            this.cboxUpper.UseVisualStyleBackColor = true;
            // 
            // cboxSQL
            // 
            this.cboxSQL.AutoSize = true;
            this.cboxSQL.Location = new System.Drawing.Point(3, 243);
            this.cboxSQL.Name = "cboxSQL";
            this.cboxSQL.Size = new System.Drawing.Size(42, 16);
            this.cboxSQL.TabIndex = 9;
            this.cboxSQL.Text = "SQL";
            this.cboxSQL.UseVisualStyleBackColor = true;
            // 
            // cboxParam
            // 
            this.cboxParam.AutoSize = true;
            this.cboxParam.Location = new System.Drawing.Point(3, 153);
            this.cboxParam.Name = "cboxParam";
            this.cboxParam.Size = new System.Drawing.Size(60, 16);
            this.cboxParam.TabIndex = 8;
            this.cboxParam.Text = ": -> @";
            this.cboxParam.UseVisualStyleBackColor = true;
            // 
            // cboxNull2
            // 
            this.cboxNull2.AutoSize = true;
            this.cboxNull2.Location = new System.Drawing.Point(3, 123);
            this.cboxNull2.Name = "cboxNull2";
            this.cboxNull2.Size = new System.Drawing.Size(90, 16);
            this.cboxNull2.TabIndex = 11;
            this.cboxNull2.Text = "@Name -> \'\'";
            this.cboxNull2.UseVisualStyleBackColor = true;
            // 
            // cboxNull
            // 
            this.cboxNull.AutoSize = true;
            this.cboxNull.Location = new System.Drawing.Point(3, 93);
            this.cboxNull.Name = "cboxNull";
            this.cboxNull.Size = new System.Drawing.Size(90, 16);
            this.cboxNull.TabIndex = 10;
            this.cboxNull.Text = ":Name -> \'\'";
            this.cboxNull.UseVisualStyleBackColor = true;
            // 
            // cboxReplace
            // 
            this.cboxReplace.AutoSize = true;
            this.cboxReplace.Location = new System.Drawing.Point(3, 33);
            this.cboxReplace.Name = "cboxReplace";
            this.cboxReplace.Size = new System.Drawing.Size(108, 16);
            this.cboxReplace.TabIndex = 7;
            this.cboxReplace.Text = "Replace(,分隔)";
            this.cboxReplace.UseVisualStyleBackColor = true;
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(3, 63);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(100, 21);
            this.txtReplace.TabIndex = 12;
            this.txtReplace.Text = "\" +,\"";
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(3, 273);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(42, 23);
            this.btnSetting.TabIndex = 13;
            this.btnSetting.Text = "配置";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // tlpSqlResult
            // 
            this.tlpSqlResult.ColumnCount = 4;
            this.tlpSqlResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpSqlResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSqlResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpSqlResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSqlResult.Controls.Add(this.label2, 2, 0);
            this.tlpSqlResult.Controls.Add(this.rtxtBoxGauss, 3, 0);
            this.tlpSqlResult.Controls.Add(this.rtxtBoxOracle, 1, 0);
            this.tlpSqlResult.Controls.Add(this.label1, 0, 0);
            this.tlpSqlResult.Location = new System.Drawing.Point(20, 13);
            this.tlpSqlResult.Name = "tlpSqlResult";
            this.tlpSqlResult.RowCount = 1;
            this.tlpSqlResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSqlResult.Size = new System.Drawing.Size(388, 64);
            this.tlpSqlResult.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gauss";
            // 
            // rtxtBoxGauss
            // 
            this.rtxtBoxGauss.Location = new System.Drawing.Point(249, 3);
            this.rtxtBoxGauss.Name = "rtxtBoxGauss";
            this.rtxtBoxGauss.Size = new System.Drawing.Size(100, 58);
            this.rtxtBoxGauss.TabIndex = 1;
            this.rtxtBoxGauss.Text = "";
            // 
            // rtxtBoxOracle
            // 
            this.rtxtBoxOracle.Location = new System.Drawing.Point(55, 3);
            this.rtxtBoxOracle.Name = "rtxtBoxOracle";
            this.rtxtBoxOracle.Size = new System.Drawing.Size(100, 58);
            this.rtxtBoxOracle.TabIndex = 0;
            this.rtxtBoxOracle.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oracle";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlpMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlpSqlResult);
            this.splitContainer1.Size = new System.Drawing.Size(572, 525);
            this.splitContainer1.SplitterDistance = 439;
            this.splitContainer1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 549);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Format";
            this.tlpMain.ResumeLayout(false);
            this.tlpBtn.ResumeLayout(false);
            this.tlpBtn.PerformLayout();
            this.tlpSqlResult.ResumeLayout(false);
            this.tlpSqlResult.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtSource;
        private System.Windows.Forms.RichTextBox rtxtTarget;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.CheckBox cboxParam;
        private System.Windows.Forms.CheckBox cboxReplace;
        private System.Windows.Forms.CheckBox cboxUpper;
        private System.Windows.Forms.CheckBox cboxSQL;
        private System.Windows.Forms.CheckBox cboxNull;
        private System.Windows.Forms.CheckBox cboxNull2;
        private System.Windows.Forms.TableLayoutPanel tlpBtn;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.CheckBox cboxDefine;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.TableLayoutPanel tlpSqlResult;
        private System.Windows.Forms.RichTextBox rtxtBoxGauss;
        private System.Windows.Forms.RichTextBox rtxtBoxOracle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

