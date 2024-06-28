namespace Plugin_window
{
    partial class QRGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRGenerator));
            this.insert_text_label = new System.Windows.Forms.Label();
            this.insert_link_textbox = new System.Windows.Forms.TextBox();
            this.generate_code_button = new System.Windows.Forms.Button();
            this.save_as_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.place_to_plans = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // insert_text_label
            // 
            this.insert_text_label.AutoSize = true;
            this.insert_text_label.Location = new System.Drawing.Point(23, 17);
            this.insert_text_label.Name = "insert_text_label";
            this.insert_text_label.Size = new System.Drawing.Size(120, 13);
            this.insert_text_label.TabIndex = 2;
            this.insert_text_label.Text = "Insert the your link here:";
            this.insert_text_label.Click += new System.EventHandler(this.insert_text_label_Click);
            // 
            // insert_link_textbox
            // 
            this.insert_link_textbox.Location = new System.Drawing.Point(41, 36);
            this.insert_link_textbox.Name = "insert_link_textbox";
            this.insert_link_textbox.Size = new System.Drawing.Size(383, 20);
            this.insert_link_textbox.TabIndex = 3;
            this.insert_link_textbox.TextChanged += new System.EventHandler(this.insert_link_textbox_TextChanged);
            // 
            // generate_code_button
            // 
            this.generate_code_button.AccessibleName = "";
            this.generate_code_button.Location = new System.Drawing.Point(56, 70);
            this.generate_code_button.Name = "generate_code_button";
            this.generate_code_button.Size = new System.Drawing.Size(87, 33);
            this.generate_code_button.TabIndex = 4;
            this.generate_code_button.Text = "Generate";
            this.generate_code_button.UseVisualStyleBackColor = true;
            this.generate_code_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // save_as_button
            // 
            this.save_as_button.Location = new System.Drawing.Point(186, 71);
            this.save_as_button.Name = "save_as_button";
            this.save_as_button.Size = new System.Drawing.Size(87, 32);
            this.save_as_button.TabIndex = 6;
            this.save_as_button.Text = "Save As";
            this.save_as_button.UseVisualStyleBackColor = true;
            this.save_as_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(238, 126);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // place_to_plans
            // 
            this.place_to_plans.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.place_to_plans.Location = new System.Drawing.Point(310, 70);
            this.place_to_plans.Name = "place_to_plans";
            this.place_to_plans.Size = new System.Drawing.Size(87, 33);
            this.place_to_plans.TabIndex = 9;
            this.place_to_plans.Text = "Place It";
            this.place_to_plans.UseVisualStyleBackColor = true;
            this.place_to_plans.Click += new System.EventHandler(this.place_to_plans_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(41, 126);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(184, 184);
            this.checkedListBox1.TabIndex = 10;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // QRGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 331);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.place_to_plans);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.save_as_button);
            this.Controls.Add(this.generate_code_button);
            this.Controls.Add(this.insert_link_textbox);
            this.Controls.Add(this.insert_text_label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QRGenerator";
            this.Text = "QRGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label insert_text_label;
        private System.Windows.Forms.TextBox insert_link_textbox;
        private System.Windows.Forms.Button generate_code_button;
        private System.Windows.Forms.Button save_as_button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button place_to_plans;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}