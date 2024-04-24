﻿
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
            this.YoutubeLinkTextBox.Location = new System.Drawing.Point(14, 37);
            this.YoutubeLinkTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.YoutubeLinkTextBox.Name = "YoutubeLinkTextBox";
            this.YoutubeLinkTextBox.Size = new System.Drawing.Size(185, 35);
            this.YoutubeLinkTextBox.TabIndex = 0;
            this.YoutubeLinkTextBox.TextChanged += new System.EventHandler(this.YoutubeLinkTextBox_TextChanged);
            this.YoutubeLinkTextBox.DoubleClick += new System.EventHandler(this.YoutubeLinkTextBox_DoubleClick);
            // 
            // YoutubeLinkLabel
            // 
            this.YoutubeLinkLabel.AutoSize = true;
            this.YoutubeLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutubeLinkLabel.Location = new System.Drawing.Point(11, 9);
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
            this.FormatComboBox.Location = new System.Drawing.Point(203, 38);
            this.FormatComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FormatComboBox.Name = "FormatComboBox";
            this.FormatComboBox.Size = new System.Drawing.Size(72, 34);
            this.FormatComboBox.Sorted = true;
            this.FormatComboBox.TabIndex = 2;
            this.FormatComboBox.SelectedIndexChanged += new System.EventHandler(this.FormatComboBox_SelectedIndexChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Enabled = false;
            this.DownloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadButton.Location = new System.Drawing.Point(565, 75);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(251, 48);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Descargar";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // VideoFotoPictureBox
            // 
            this.VideoFotoPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.VideoFotoPictureBox.Location = new System.Drawing.Point(10, 127);
            this.VideoFotoPictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VideoFotoPictureBox.Name = "VideoFotoPictureBox";
            this.VideoFotoPictureBox.Size = new System.Drawing.Size(806, 458);
            this.VideoFotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VideoFotoPictureBox.TabIndex = 4;
            this.VideoFotoPictureBox.TabStop = false;
            this.VideoFotoPictureBox.Click += new System.EventHandler(this.VideoFotoPictureBox_Click);
            // 
            // VideoNameLabel
            // 
            this.VideoNameLabel.AutoSize = true;
            this.VideoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoNameLabel.Location = new System.Drawing.Point(11, 100);
            this.VideoNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VideoNameLabel.Name = "VideoNameLabel";
            this.VideoNameLabel.Size = new System.Drawing.Size(180, 25);
            this.VideoNameLabel.TabIndex = 5;
            this.VideoNameLabel.Text = "Nombre del video";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(10, 589);
            this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DownloadProgressBar.MarqueeAnimationSpeed = 30;
            this.DownloadProgressBar.Maximum = 1000;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(806, 51);
            this.DownloadProgressBar.Step = 30;
            this.DownloadProgressBar.TabIndex = 6;
            // 
            // VideoStreamsComboBox
            // 
            this.VideoStreamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoStreamsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoStreamsComboBox.FormattingEnabled = true;
            this.VideoStreamsComboBox.Location = new System.Drawing.Point(279, 37);
            this.VideoStreamsComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VideoStreamsComboBox.Name = "VideoStreamsComboBox";
            this.VideoStreamsComboBox.Size = new System.Drawing.Size(282, 34);
            this.VideoStreamsComboBox.TabIndex = 7;
            this.VideoStreamsComboBox.SelectedIndexChanged += new System.EventHandler(this.VideoStreamsComboBox_SelectedIndexChanged);
            // 
            // ReEncodeAudioCheckBox
            // 
            this.ReEncodeAudioCheckBox.AutoSize = true;
            this.ReEncodeAudioCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReEncodeAudioCheckBox.Location = new System.Drawing.Point(624, 10);
            this.ReEncodeAudioCheckBox.Name = "ReEncodeAudioCheckBox";
            this.ReEncodeAudioCheckBox.Size = new System.Drawing.Size(120, 28);
            this.ReEncodeAudioCheckBox.TabIndex = 8;
            this.ReEncodeAudioCheckBox.Text = "ReEncode";
            this.ReEncodeAudioCheckBox.UseVisualStyleBackColor = true;
            this.ReEncodeAudioCheckBox.CheckedChanged += new System.EventHandler(this.ReEncodeAudioCheckBox_CheckedChanged);
            // 
            // VideoBoxLabel
            // 
            this.VideoBoxLabel.AutoSize = true;
            this.VideoBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoBoxLabel.Location = new System.Drawing.Point(274, 9);
            this.VideoBoxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VideoBoxLabel.Name = "VideoBoxLabel";
            this.VideoBoxLabel.Size = new System.Drawing.Size(68, 26);
            this.VideoBoxLabel.TabIndex = 9;
            this.VideoBoxLabel.Text = "Video";
            // 
            // AudioBoxLabel
            // 
            this.AudioBoxLabel.AutoSize = true;
            this.AudioBoxLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioBoxLabel.Location = new System.Drawing.Point(560, 9);
            this.AudioBoxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AudioBoxLabel.Name = "AudioBoxLabel";
            this.AudioBoxLabel.Size = new System.Drawing.Size(68, 26);
            this.AudioBoxLabel.TabIndex = 11;
            this.AudioBoxLabel.Text = "Audio";
            // 
            // AudioStreamsComboBox
            // 
            this.AudioStreamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioStreamsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioStreamsComboBox.FormattingEnabled = true;
            this.AudioStreamsComboBox.Location = new System.Drawing.Point(565, 37);
            this.AudioStreamsComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AudioStreamsComboBox.Name = "AudioStreamsComboBox";
            this.AudioStreamsComboBox.Size = new System.Drawing.Size(251, 34);
            this.AudioStreamsComboBox.TabIndex = 10;
            this.AudioStreamsComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioStreamsComboBox_SelectedIndexChanged);
            // 
            // AudioCodecsComboBox
            // 
            this.AudioCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AudioCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioCodecsComboBox.FormattingEnabled = true;
            this.AudioCodecsComboBox.Location = new System.Drawing.Point(735, 9);
            this.AudioCodecsComboBox.Name = "AudioCodecsComboBox";
            this.AudioCodecsComboBox.Size = new System.Drawing.Size(80, 26);
            this.AudioCodecsComboBox.TabIndex = 12;
            // 
            // VideoCodecsComboBox
            // 
            this.VideoCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.VideoCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoCodecsComboBox.FormattingEnabled = true;
            this.VideoCodecsComboBox.Location = new System.Drawing.Point(478, 9);
            this.VideoCodecsComboBox.Name = "VideoCodecsComboBox";
            this.VideoCodecsComboBox.Size = new System.Drawing.Size(82, 26);
            this.VideoCodecsComboBox.TabIndex = 14;
            // 
            // ReEncodeVideoCheckBox
            // 
            this.ReEncodeVideoCheckBox.AutoSize = true;
            this.ReEncodeVideoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReEncodeVideoCheckBox.Location = new System.Drawing.Point(347, 10);
            this.ReEncodeVideoCheckBox.Name = "ReEncodeVideoCheckBox";
            this.ReEncodeVideoCheckBox.Size = new System.Drawing.Size(120, 28);
            this.ReEncodeVideoCheckBox.TabIndex = 13;
            this.ReEncodeVideoCheckBox.Text = "ReEncode";
            this.ReEncodeVideoCheckBox.UseVisualStyleBackColor = true;
            this.ReEncodeVideoCheckBox.CheckedChanged += new System.EventHandler(this.ReEncodeVideoCheckBox_CheckedChanged);
            // 
            // YoutubeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 650);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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

