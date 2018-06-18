namespace BarView
{
    partial class CreateOrderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.CustomerCB = new System.Windows.Forms.ComboBox();
            this.CocktailCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CountTB = new System.Windows.Forms.TextBox();
            this.SumTB = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // CustomerCB
            // 
            this.CustomerCB.FormattingEnabled = true;
            this.CustomerCB.Location = new System.Drawing.Point(86, 13);
            this.CustomerCB.Name = "CustomerCB";
            this.CustomerCB.Size = new System.Drawing.Size(219, 21);
            this.CustomerCB.TabIndex = 1;
            // 
            // CocktailCB
            // 
            this.CocktailCB.FormattingEnabled = true;
            this.CocktailCB.Location = new System.Drawing.Point(86, 40);
            this.CocktailCB.Name = "CocktailCB";
            this.CocktailCB.Size = new System.Drawing.Size(219, 21);
            this.CocktailCB.TabIndex = 3;
            this.CocktailCB.SelectedIndexChanged += new System.EventHandler(this.CocktailCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Коктейль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Сумма";
            // 
            // CountTB
            // 
            this.CountTB.Location = new System.Drawing.Point(86, 67);
            this.CountTB.Name = "CountTB";
            this.CountTB.Size = new System.Drawing.Size(219, 20);
            this.CountTB.TabIndex = 7;
            this.CountTB.TextChanged += new System.EventHandler(this.CountTB_TextChanged);
            // 
            // SumTB
            // 
            this.SumTB.Enabled = false;
            this.SumTB.Location = new System.Drawing.Point(86, 94);
            this.SumTB.Name = "SumTB";
            this.SumTB.Size = new System.Drawing.Size(219, 20);
            this.SumTB.TabIndex = 8;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(205, 120);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(86, 120);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(100, 23);
            this.Save.TabIndex = 9;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // CreateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 154);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.SumTB);
            this.Controls.Add(this.CountTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CocktailCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CustomerCB);
            this.Controls.Add(this.label1);
            this.Name = "CreateOrderForm";
            this.Text = "CreateOrderForm";
            this.Load += new System.EventHandler(this.CreateOrderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CustomerCB;
        private System.Windows.Forms.ComboBox CocktailCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CountTB;
        private System.Windows.Forms.TextBox SumTB;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button Save;
    }
}