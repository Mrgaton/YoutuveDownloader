
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
            this.VideoStreamsComboBox = new System.Windows.Forms.ComboBox();
            this.ReEncodeAudioCheckBox = new System.Windows.Forms.CheckBox();
            this.VideoBoxLabel = new System.Windows.Forms.Label();
            this.AudioBoxLabel = new System.Windows.Forms.Label();
            this.AudioStreamsComboBox = new System.Windows.Forms.ComboBox();
            this.AudioCodecsComboBox = new System.Windows.Forms.ComboBox();
            this.VideoCodecsComboBox = new System.Windows.Forms.ComboBox();
            this.ReEncodeVideoCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.VideoFotoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // YoutubeLinkTextBox
            // 
            this.YoutubeLinkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutubeLinkTextBox.Location = new System.Drawing.Point(19, 46);
            this.YoutubeLinkTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.YoutubeLinkTextBox.Name = "YoutubeLinkTextBox";
            this.YoutubeLinkTextBox.Size = new System.Drawing.Size(245, 41);
            this.YoutubeLinkTextBox.TabIndex = 0;
            this.YoutubeLinkTextBox.TextChanged += new System.EventHandler(this.YoutubeLinkTextBox_TextChanged);
            this.YoutubeLinkTextBox.DoubleClick += new System.EventHandler(this.YoutubeLinkTextBox_DoubleClick);
            // 
            // YoutubeLinkLabel
            // 
            this.YoutubeLinkLabel.AutoSize = true;
            this.YoutubeLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutubeLinkLabel.Location = new System.Drawing.Point(15, 11);
            this.YoutubeLinkLabel.Name = "YoutubeLinkLabel";
            this.YoutubeLinkLabel.Size = new System.Drawing.Size(181, 32);
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
            this.FormatComboBox.Location = new System.Drawing.Point(271, 47);
            this.FormatComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FormatComboBox.Name = "FormatComboBox";
            this.FormatComboBox.Size = new System.Drawing.Size(95, 39);
            this.FormatComboBox.Sorted = true;
            this.FormatComboBox.TabIndex = 2;
            this.FormatComboBox.SelectedIndexChanged += new System.EventHandler(this.FormatComboBox_SelectedIndexChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Enabled = false;
            this.DownloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadButton.Location = new System.Drawing.Point(753, 92);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(335, 59);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Descargar";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // VideoFotoPictureBox
            // 
            this.VideoFotoPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.VideoFotoPictureBox.Location = new System.Drawing.Point(13, 156);
            this.VideoFotoPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideoFotoPictureBox.Name = "VideoFotoPictureBox";
            this.VideoFotoPictureBox.Size = new System.Drawing.Size(1075, 564);
            this.VideoFotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VideoFotoPictureBox.TabIndex = 4;
            this.VideoFotoPictureBox.TabStop = false;
            this.VideoFotoPictureBox.Click += new System.EventHandler(this.VideoFotoPictureBox_Click);
            // 
            // VideoNameLabel
            // 
            this.VideoNameLabel.AutoSize = true;
            this.VideoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoNameLabel.Location = new System.Drawing.Point(15, 123);
            this.VideoNameLabel.Name = "VideoNameLabel";
            this.VideoNameLabel.Size = new System.Drawing.Size(225, 31);
            this.VideoNameLabel.TabIndex = 5;
            this.VideoNameLabel.Text = "Nombre del video";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(13, 725);
            this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadProgressBar.MarqueeAnimationSpeed = 30;
            this.DownloadProgressBar.Maximum = 1000;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(1075, 63);
            this.DownloadProgressBar.Step = 30;
            this.DownloadProgressBar.TabIndex = 6;
            // 
            // VideoStreamsComboBox
            // 
            this.VideoStreamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoStreamsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoStreamsComboBox.FormattingEnabled = true;
            this.VideoStreamsComboBox.Location = new System.Drawing.Point(372, 46);
            this.VideoStreamsComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideoStreamsComboBox.Name = "VideoStreamsComboBox";
            this.VideoStreamsComboBox.Size = new System.Drawing.Size(375, 39);
            this.VideoStreamsComboBox.TabIndex = 7;
            this.VideoStreamsComboBox.SelectedIndexChanged += new System.EventHandler(this.VideoStreamsComboBox_SelectedIndexChanged);
            // 
            // ReEncodeAudioCheckBox
            // 
            this.ReEncodeAudioCheckBox.AutoSize = true;
            this.ReEncodeAudioCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReEncodeAudioCheckBox.Location = new System.Drawing.Point(832, 12);
            this.ReEncodeAudioCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReEncodeAudioCheckBox.Name = "ReEncodeAudioCheckBox";
            this.ReEncodeAudioCheckBox.Size = new System.Drawing.Size(149, 33);
            this.ReEncodeAudioCheckBox.TabIndex = 8;
            this.ReEncodeAudioCheckBox.Text = "ReEncode";
            this.ReEncodeAudioCheckBox.UseVisualStyleBackColor = true;
            this.ReEncodeAudioCheckBox.CheckedChanged += new System.EventHandler(this.ReEncodeAudioCheckBox_CheckedChanged);
            // 
            // VideoBoxLabel
            // 
            this.VideoBoxLabel.AutoSize = true;
            this.VideoBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoBoxLabel.Location = new System.Drawing.Point(365, 11);
            this.VideoBoxLabel.Name = "VideoBoxLabel";
            this.VideoBoxLabel.Size = new System.Drawing.Size(88, 32);
            this.VideoBoxLabel.TabIndex = 9;
            this.VideoBoxLabel.Text = "Video";
            // 
            // AudioBoxLabel
            // 
            this.AudioBoxLabel.AutoSize = true;
            this.AudioBoxLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioBoxLabel.Location = new System.Drawing.Point(747, 11);
            this.AudioBoxLabel.Name = "AudioBoxLabel";
            this.AudioBoxLabel.Size = new System.Drawing.Size(88, 32);
            this.AudioBoxLabel.TabIndex = 11;
            this.AudioBoxLabel.Text = "Audio";
            // 
            // AudioStreamsComboBox
            // 
            this.AudioStreamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioStreamsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioStreamsComboBox.FormattingEnabled = true;
            this.AudioStreamsComboBox.Location = new System.Drawing.Point(753, 46);
            this.AudioStreamsComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AudioStreamsComboBox.Name = "AudioStreamsComboBox";
            this.AudioStreamsComboBox.Size = new System.Drawing.Size(333, 39);
            this.AudioStreamsComboBox.TabIndex = 10;
            this.AudioStreamsComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioStreamsComboBox_SelectedIndexChanged);
            // 
            // AudioCodecsComboBox
            // 
            this.AudioCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AudioCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioCodecsComboBox.FormattingEnabled = true;
            this.AudioCodecsComboBox.Location = new System.Drawing.Point(980, 11);
            this.AudioCodecsComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AudioCodecsComboBox.Name = "AudioCodecsComboBox";
            this.AudioCodecsComboBox.Size = new System.Drawing.Size(106, 32);
            this.AudioCodecsComboBox.TabIndex = 12;
            // 
            // VideoCodecsComboBox
            // 
            this.VideoCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.VideoCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoCodecsComboBox.FormattingEnabled = true;
            this.VideoCodecsComboBox.Location = new System.Drawing.Point(638, 11);
            this.VideoCodecsComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VideoCodecsComboBox.Name = "VideoCodecsComboBox";
            this.VideoCodecsComboBox.Size = new System.Drawing.Size(108, 32);
            this.VideoCodecsComboBox.TabIndex = 14;
            // 
            // ReEncodeVideoCheckBox
            // 
            this.ReEncodeVideoCheckBox.AutoSize = true;
            this.ReEncodeVideoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReEncodeVideoCheckBox.Location = new System.Drawing.Point(463, 12);
            this.ReEncodeVideoCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReEncodeVideoCheckBox.Name = "ReEncodeVideoCheckBox";
            this.ReEncodeVideoCheckBox.Size = new System.Drawing.Size(149, 33);
            this.ReEncodeVideoCheckBox.TabIndex = 13;
            this.ReEncodeVideoCheckBox.Text = "ReEncode";
            this.ReEncodeVideoCheckBox.UseVisualStyleBackColor = true;
            // 
            // YoutubeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 800);
            this.Controls.Add(this.VideoStreamsComboBox);
            this.Controls.Add(this.VideoCodecsComboBox);
            this.Controls.Add(this.ReEncodeVideoCheckBox);
            this.Controls.Add(this.AudioCodecsComboBox);
            this.Controls.Add(this.AudioStreamsComboBox);
            this.Controls.Add(this.ReEncodeAudioCheckBox);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.AudioBoxLabel);
            this.Controls.Add(this.VideoBoxLabel);
            this.Controls.Add(this.DownloadProgressBar);
            this.Controls.Add(this.VideoNameLabel);
            this.Controls.Add(this.VideoFotoPictureBox);
            this.Controls.Add(this.FormatComboBox);
            this.Controls.Add(this.YoutubeLinkLabel);
            this.Controls.Add(this.YoutubeLinkTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "YoutubeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Youtube Downloader By Mrgaton";
            this.Shown += new System.EventHandler(this.YoutubeForm_Shown);
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
        private System.Windows.Forms.ComboBox VideoStreamsComboBox;
        private System.Windows.Forms.CheckBox ReEncodeAudioCheckBox;
        private System.Windows.Forms.Label VideoBoxLabel;
        private System.Windows.Forms.Label AudioBoxLabel;
        private System.Windows.Forms.ComboBox AudioStreamsComboBox;
        private System.Windows.Forms.ComboBox AudioCodecsComboBox;
        private System.Windows.Forms.ComboBox VideoCodecsComboBox;
        private System.Windows.Forms.CheckBox ReEncodeVideoCheckBox;
    }
}

