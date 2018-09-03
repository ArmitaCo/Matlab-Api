namespace Matlab.ArticleImageSuggestionTool
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxArticleTitle = new System.Windows.Forms.TextBox();
            this.textBoxQuestionTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCategory = new System.Windows.Forms.Label();
            this.labelPackage = new System.Windows.Forms.Label();
            this.buttonAddImage = new System.Windows.Forms.Button();
            this.buttonRemoveImage = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(675, 536);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save And Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxArticleTitle
            // 
            this.textBoxArticleTitle.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArticleTitle.Location = new System.Drawing.Point(542, 12);
            this.textBoxArticleTitle.Multiline = true;
            this.textBoxArticleTitle.Name = "textBoxArticleTitle";
            this.textBoxArticleTitle.ReadOnly = true;
            this.textBoxArticleTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxArticleTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxArticleTitle.Size = new System.Drawing.Size(533, 119);
            this.textBoxArticleTitle.TabIndex = 1;
            // 
            // textBoxQuestionTitle
            // 
            this.textBoxQuestionTitle.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxQuestionTitle.Location = new System.Drawing.Point(12, 12);
            this.textBoxQuestionTitle.Multiline = true;
            this.textBoxQuestionTitle.Name = "textBoxQuestionTitle";
            this.textBoxQuestionTitle.ReadOnly = true;
            this.textBoxQuestionTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxQuestionTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxQuestionTitle.Size = new System.Drawing.Size(524, 119);
            this.textBoxQuestionTitle.TabIndex = 1;
            this.textBoxQuestionTitle.TextChanged += new System.EventHandler(this.textBoxQuestionTitle_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Category:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Package: ";
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(71, 150);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(0, 13);
            this.labelCategory.TabIndex = 2;
            // 
            // labelPackage
            // 
            this.labelPackage.AutoSize = true;
            this.labelPackage.Location = new System.Drawing.Point(83, 176);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(0, 13);
            this.labelPackage.TabIndex = 2;
            // 
            // buttonAddImage
            // 
            this.buttonAddImage.Location = new System.Drawing.Point(805, 536);
            this.buttonAddImage.Name = "buttonAddImage";
            this.buttonAddImage.Size = new System.Drawing.Size(75, 23);
            this.buttonAddImage.TabIndex = 3;
            this.buttonAddImage.Text = "Add Image";
            this.buttonAddImage.UseVisualStyleBackColor = true;
            this.buttonAddImage.Click += new System.EventHandler(this.buttonAddImage_Click);
            // 
            // buttonRemoveImage
            // 
            this.buttonRemoveImage.Location = new System.Drawing.Point(886, 536);
            this.buttonRemoveImage.Name = "buttonRemoveImage";
            this.buttonRemoveImage.Size = new System.Drawing.Size(97, 23);
            this.buttonRemoveImage.TabIndex = 4;
            this.buttonRemoveImage.Text = "Remove Image";
            this.buttonRemoveImage.UseVisualStyleBackColor = true;
            this.buttonRemoveImage.Click += new System.EventHandler(this.buttonRemoveImage_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(542, 150);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(533, 368);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(24, 211);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 307);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 609);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonRemoveImage);
            this.Controls.Add(this.buttonAddImage);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.labelPackage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxQuestionTitle);
            this.Controls.Add(this.textBoxArticleTitle);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Article Image Suggestion Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxArticleTitle;
        private System.Windows.Forms.TextBox textBoxQuestionTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.Button buttonAddImage;
        private System.Windows.Forms.Button buttonRemoveImage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

