using System;

namespace BarView
{
    partial class CustomerOrdersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Make = new System.Windows.Forms.Button();
            this.ToPDF = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CustomerOrderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerOrderViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(32, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(137, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(215, 8);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(137, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "По";
            // 
            // Make
            // 
            this.Make.Location = new System.Drawing.Point(369, 8);
            this.Make.Name = "Make";
            this.Make.Size = new System.Drawing.Size(109, 31);
            this.Make.TabIndex = 4;
            this.Make.Text = "Сформировать";
            this.Make.UseVisualStyleBackColor = true;
            this.Make.Click += new System.EventHandler(this.Make_Click);
            // 
            // ToPDF
            // 
            this.ToPDF.Location = new System.Drawing.Point(555, 8);
            this.ToPDF.Name = "ToPDF";
            this.ToPDF.Size = new System.Drawing.Size(109, 31);
            this.ToPDF.TabIndex = 5;
            this.ToPDF.Text = "В PDF";
            this.ToPDF.UseVisualStyleBackColor = true;
            this.ToPDF.Click += new System.EventHandler(this.ToPDF_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "DataSetOrders";
            reportDataSource1.Value = this.CustomerOrderViewModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BarView.Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 57);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(856, 497);
            this.reportViewer1.TabIndex = 6;
            // 
            // CustomerOrderViewModelBindingSource
            // 
            this.CustomerOrderViewModelBindingSource.DataSource = typeof(BarService.ViewModel.CustomerOrderViewModel);
            // 
            // CustomerOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 554);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.ToPDF);
            this.Controls.Add(this.Make);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "CustomerOrdersForm";
            this.Text = "CustomerOrdersForm";
            this.Load += new System.EventHandler(this.CustomerOrdersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerOrderViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Make;
        private System.Windows.Forms.Button ToPDF;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CustomerOrderViewModelBindingSource;
    }
}