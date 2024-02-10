
namespace Youtuve_downloader
{
    partial class YoutubeForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoutubeForm));
            this.YoutubeLinkTextBox = new System.Windows.Forms.TextBox();
            this.YoutubeLinkLabel = new System.Windows.Forms.Label();
            this.FormatComboBox = new System.Windows.Forms.ComboBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.VideoFotoPictureBox = new System.Windows.Forms.PictureBox();
            this.VideoNameLabel = new System.Windows.Forms.Label();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.BitrateComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.VideoFotoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // YoutubeLinkTextBox
            // 
            this.YoutubeLinkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutubeLinkTextBox.Location = new System.Drawing.Point(14, 48);
            this.YoutubeLinkTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.YoutubeLinkTextBox.Name = "YoutubeLinkTextBox";
            this.YoutubeLinkTextBox.Size = new System.Drawing.Size(221, 35);
            this.YoutubeLinkTextBox.TabIndex = 0;
            this.YoutubeLinkTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.YoutubeLinkTextBox_MouseClick);
            this.YoutubeLinkTextBox.TextChanged += new System.EventHandler(this.YoutubeLinkTextBox_TextChanged);
            // 
            // YoutubeLinkLabel
            // 
            this.YoutubeLinkLabel.AutoSize = true;
            this.YoutubeLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutubeLinkLabel.Location = new System.Drawing.Point(5, 9);
            this.YoutubeLinkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.YoutubeLinkLabel.Name = "YoutubeLinkLabel";
            this.YoutubeLinkLabel.Size = new System.Drawing.Size(140, 26);
            this.YoutubeLinkLabel.TabIndex = 1;
            this.YoutubeLinkLabel.Text = "Youtube Link";
            // 
            // FormatComboBox
            // 
            this.FormatComboBox.AllowDrop = true;
            this.FormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FormatComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormatComboBox.FormattingEnabled = true;
            this.FormatComboBox.Items.AddRange(new object[] {
            "com",
            "mp3",
            "mp4",
            "mux"});
            this.FormatComboBox.Location = new System.Drawing.Point(239, 48);
            this.FormatComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.FormatComboBox.Name = "FormatComboBox";
            this.FormatComboBox.Size = new System.Drawing.Size(72, 34);
            this.FormatComboBox.Sorted = true;
            this.FormatComboBox.TabIndex = 2;
            this.FormatComboBox.SelectedIndexChanged += new System.EventHandler(this.FormatComboBox_SelectedIndexChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadButton.Location = new System.Drawing.Point(475, 7);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(122, 37);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Descargar";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // VideoFotoPictureBox
            // 
            this.VideoFotoPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.VideoFotoPictureBox.Location = new System.Drawing.Point(14, 112);
            this.VideoFotoPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.VideoFotoPictureBox.Name = "VideoFotoPictureBox";
            this.VideoFotoPictureBox.Size = new System.Drawing.Size(583, 306);
            this.VideoFotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VideoFotoPictureBox.TabIndex = 4;
            this.VideoFotoPictureBox.TabStop = false;
            this.VideoFotoPictureBox.Click += new System.EventHandler(this.VideoFotoPictureBox_Click);
            // 
            // VideoNameLabel
            // 
            this.VideoNameLabel.AutoSize = true;
            this.VideoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoNameLabel.Location = new System.Drawing.Point(9, 85);
            this.VideoNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VideoNameLabel.Name = "VideoNameLabel";
            this.VideoNameLabel.Size = new System.Drawing.Size(180, 25);
            this.VideoNameLabel.TabIndex = 5;
            this.VideoNameLabel.Text = "Nombre del video";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(14, 422);
            this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadProgressBar.MarqueeAnimationSpeed = 30;
            this.DownloadProgressBar.Maximum = 1000;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(583, 41);
            this.DownloadProgressBar.Step = 30;
            this.DownloadProgressBar.TabIndex = 6;
            // 
            // BitrateComboBox
            // 
            this.BitrateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BitrateComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BitrateComboBox.FormattingEnabled = true;
            this.BitrateComboBox.Location = new System.Drawing.Point(315, 48);
            this.BitrateComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.BitrateComboBox.Name = "BitrateComboBox";
            this.BitrateComboBox.Size = new System.Drawing.Size(282, 34);
            this.BitrateComboBox.TabIndex = 7;
            // 
            // YoutubeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 470);
            this.Controls.Add(this.BitrateComboBox);
            this.Controls.Add(this.DownloadProgressBar);
            this.Controls.Add(this.VideoNameLabel);
            this.Controls.Add(this.VideoFotoPictureBox);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.FormatComboBox);
            this.Controls.Add(this.YoutubeLinkLabel);
            this.Controls.Add(this.YoutubeLinkTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "YoutubeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Youtube Downloader By Mrgaton";
            ((System.ComponentModel.ISupportInitialize)(this.VideoFotoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox YoutubeLinkTextBox;
        private System.Windows.Forms.Label YoutubeLinkLabel;
        private System.Windows.Forms.ComboBox FormatComboBox;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.PictureBox VideoFotoPictureBox;
        private System.Windows.Forms.Label VideoNameLabel;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.ComboBox BitrateComboBox;
    }
}

