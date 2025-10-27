
namespace Youtuve_Downloader
{
    partial class YoutuveForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoutuveForm));
            this.YoutuveLinkTextBox = new System.Windows.Forms.TextBox();
            this.YoutuveLinkLabel = new System.Windows.Forms.Label();
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
            this.StartTextBox = new System.Windows.Forms.TextBox();
            this.StartLabel = new System.Windows.Forms.Label();
            this.EndTextBox = new System.Windows.Forms.TextBox();
            this.EndLabel = new System.Windows.Forms.Label();
            this.TimeCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.FpsUpDown = new System.Windows.Forms.NumericUpDown();
            this.FpsLabel = new System.Windows.Forms.Label();
            this.MinterpolateCheckBox = new System.Windows.Forms.CheckBox();
            this.SubtitlesCheckbox = new System.Windows.Forms.CheckBox();
            this.MetadataCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.VideoFotoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // YoutuveLinkTextBox
            // 
            this.YoutuveLinkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutuveLinkTextBox.Location = new System.Drawing.Point(19, 46);
            this.YoutuveLinkTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.YoutuveLinkTextBox.Name = "YoutuveLinkTextBox";
            this.YoutuveLinkTextBox.Size = new System.Drawing.Size(235, 41);
            this.YoutuveLinkTextBox.TabIndex = 0;
            this.YoutuveLinkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YoutuveLinkTextBox.TextChanged += new System.EventHandler(this.YoutuveLinkTextBox_TextChanged);
            this.YoutuveLinkTextBox.DoubleClick += new System.EventHandler(this.YoutuveLinkTextBox_DoubleClick);
            // 
            // YoutuveLinkLabel
            // 
            this.YoutuveLinkLabel.AutoSize = true;
            this.YoutuveLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YoutuveLinkLabel.Location = new System.Drawing.Point(15, 11);
            this.YoutuveLinkLabel.Name = "YoutuveLinkLabel";
            this.YoutuveLinkLabel.Size = new System.Drawing.Size(155, 32);
            this.YoutuveLinkLabel.TabIndex = 1;
            this.YoutuveLinkLabel.Text = "Youtuve ID";
            // 
            // FormatComboBox
            // 
            this.FormatComboBox.AllowDrop = true;
            this.FormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FormatComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormatComboBox.FormattingEnabled = true;
            this.FormatComboBox.Items.AddRange(new object[] {
            "AUD",
            "MUX",
            "VID"});
            this.FormatComboBox.Location = new System.Drawing.Point(260, 47);
            this.FormatComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FormatComboBox.Name = "FormatComboBox";
            this.FormatComboBox.Size = new System.Drawing.Size(105, 39);
            this.FormatComboBox.Sorted = true;
            this.FormatComboBox.TabIndex = 2;
            this.FormatComboBox.SelectedIndexChanged += new System.EventHandler(this.FormatComboBox_SelectedIndexChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Enabled = false;
            this.DownloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadButton.Location = new System.Drawing.Point(753, 92);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(339, 66);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // VideoFotoPictureBox
            // 
            this.VideoFotoPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.VideoFotoPictureBox.Location = new System.Drawing.Point(13, 197);
            this.VideoFotoPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideoFotoPictureBox.Name = "VideoFotoPictureBox";
            this.VideoFotoPictureBox.Size = new System.Drawing.Size(1073, 564);
            this.VideoFotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VideoFotoPictureBox.TabIndex = 4;
            this.VideoFotoPictureBox.TabStop = false;
            this.VideoFotoPictureBox.Click += new System.EventHandler(this.VideoFotoPictureBox_Click);
            // 
            // VideoNameLabel
            // 
            this.VideoNameLabel.AutoSize = true;
            this.VideoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoNameLabel.Location = new System.Drawing.Point(7, 161);
            this.VideoNameLabel.Name = "VideoNameLabel";
            this.VideoNameLabel.Size = new System.Drawing.Size(173, 36);
            this.VideoNameLabel.TabIndex = 5;
            this.VideoNameLabel.Text = "Video name";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(13, 766);
            this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadProgressBar.MarqueeAnimationSpeed = 30;
            this.DownloadProgressBar.Maximum = 1000;
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(1073, 38);
            this.DownloadProgressBar.Step = 100;
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
            this.VideoBoxLabel.Location = new System.Drawing.Point(365, 7);
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
            this.AudioStreamsComboBox.Size = new System.Drawing.Size(337, 39);
            this.AudioStreamsComboBox.TabIndex = 10;
            this.AudioStreamsComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioStreamsComboBox_SelectedIndexChanged);
            // 
            // AudioCodecsComboBox
            // 
            this.AudioCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AudioCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioCodecsComboBox.FormattingEnabled = true;
            this.AudioCodecsComboBox.Location = new System.Drawing.Point(985, 11);
            this.AudioCodecsComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AudioCodecsComboBox.Name = "AudioCodecsComboBox";
            this.AudioCodecsComboBox.Size = new System.Drawing.Size(105, 32);
            this.AudioCodecsComboBox.TabIndex = 12;
            this.AudioCodecsComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioCodecsComboBox_SelectedIndexChanged);
            // 
            // VideoCodecsComboBox
            // 
            this.VideoCodecsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoCodecsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.VideoCodecsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoCodecsComboBox.FormattingEnabled = true;
            this.VideoCodecsComboBox.Location = new System.Drawing.Point(631, 11);
            this.VideoCodecsComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VideoCodecsComboBox.Name = "VideoCodecsComboBox";
            this.VideoCodecsComboBox.Size = new System.Drawing.Size(115, 32);
            this.VideoCodecsComboBox.TabIndex = 14;
            this.VideoCodecsComboBox.SelectedIndexChanged += new System.EventHandler(this.VideoCodecsComboBox_SelectedIndexChanged);
            // 
            // ReEncodeVideoCheckBox
            // 
            this.ReEncodeVideoCheckBox.AutoSize = true;
            this.ReEncodeVideoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReEncodeVideoCheckBox.Location = new System.Drawing.Point(451, 9);
            this.ReEncodeVideoCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReEncodeVideoCheckBox.Name = "ReEncodeVideoCheckBox";
            this.ReEncodeVideoCheckBox.Size = new System.Drawing.Size(149, 33);
            this.ReEncodeVideoCheckBox.TabIndex = 13;
            this.ReEncodeVideoCheckBox.Text = "ReEncode";
            this.ReEncodeVideoCheckBox.UseVisualStyleBackColor = true;
            this.ReEncodeVideoCheckBox.CheckedChanged += new System.EventHandler(this.ReEncodeVideoCheckBox_CheckedChanged);
            // 
            // StartTextBox
            // 
            this.StartTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartTextBox.Location = new System.Drawing.Point(445, 90);
            this.StartTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartTextBox.Name = "StartTextBox";
            this.StartTextBox.Size = new System.Drawing.Size(103, 31);
            this.StartTextBox.TabIndex = 15;
            this.StartTextBox.TextChanged += new System.EventHandler(this.StartTextBox_TextChanged);
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLabel.Location = new System.Drawing.Point(365, 90);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(82, 32);
            this.StartLabel.TabIndex = 16;
            this.StartLabel.Text = "Start:";
            // 
            // EndTextBox
            // 
            this.EndTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndTextBox.Location = new System.Drawing.Point(629, 90);
            this.EndTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndTextBox.Name = "EndTextBox";
            this.EndTextBox.Size = new System.Drawing.Size(116, 31);
            this.EndTextBox.TabIndex = 17;
            this.EndTextBox.TextChanged += new System.EventHandler(this.EndTextBox_TextChanged);
            // 
            // EndLabel
            // 
            this.EndLabel.AutoSize = true;
            this.EndLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndLabel.Location = new System.Drawing.Point(556, 91);
            this.EndLabel.Name = "EndLabel";
            this.EndLabel.Size = new System.Drawing.Size(73, 32);
            this.EndLabel.TabIndex = 18;
            this.EndLabel.Text = "End:";
            // 
            // TimeCheckTimer
            // 
            this.TimeCheckTimer.Interval = 750;
            this.TimeCheckTimer.Tick += new System.EventHandler(this.TimeCheckTimer_Tick);
            // 
            // FpsUpDown
            // 
            this.FpsUpDown.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FpsUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FpsUpDown.Location = new System.Drawing.Point(260, 90);
            this.FpsUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FpsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FpsUpDown.Name = "FpsUpDown";
            this.FpsUpDown.Size = new System.Drawing.Size(105, 38);
            this.FpsUpDown.TabIndex = 19;
            this.FpsUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FpsLabel.Location = new System.Drawing.Point(188, 91);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(69, 32);
            this.FpsLabel.TabIndex = 20;
            this.FpsLabel.Text = "Fps:";
            // 
            // MinterpolateCheckBox
            // 
            this.MinterpolateCheckBox.AutoSize = true;
            this.MinterpolateCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinterpolateCheckBox.Location = new System.Drawing.Point(19, 91);
            this.MinterpolateCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinterpolateCheckBox.Name = "MinterpolateCheckBox";
            this.MinterpolateCheckBox.Size = new System.Drawing.Size(169, 33);
            this.MinterpolateCheckBox.TabIndex = 21;
            this.MinterpolateCheckBox.Text = "Minterpolate";
            this.MinterpolateCheckBox.UseVisualStyleBackColor = true;
            this.MinterpolateCheckBox.CheckedChanged += new System.EventHandler(this.MinterpolateCheckBox_CheckedChanged);
            // 
            // SubtitlesCheckbox
            // 
            this.SubtitlesCheckbox.AutoSize = true;
            this.SubtitlesCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.SubtitlesCheckbox.Location = new System.Drawing.Point(167, 123);
            this.SubtitlesCheckbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SubtitlesCheckbox.Name = "SubtitlesCheckbox";
            this.SubtitlesCheckbox.Size = new System.Drawing.Size(128, 33);
            this.SubtitlesCheckbox.TabIndex = 22;
            this.SubtitlesCheckbox.Text = "Subtitles";
            this.SubtitlesCheckbox.UseVisualStyleBackColor = true;
            // 
            // MetadataCheckBox
            // 
            this.MetadataCheckBox.AutoSize = true;
            this.MetadataCheckBox.Checked = true;
            this.MetadataCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MetadataCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.MetadataCheckBox.Location = new System.Drawing.Point(19, 124);
            this.MetadataCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MetadataCheckBox.Name = "MetadataCheckBox";
            this.MetadataCheckBox.Size = new System.Drawing.Size(134, 33);
            this.MetadataCheckBox.TabIndex = 23;
            this.MetadataCheckBox.Text = "Metadata";
            this.MetadataCheckBox.UseVisualStyleBackColor = true;
            this.MetadataCheckBox.CheckedChanged += new System.EventHandler(this.MetadataCheckBox_CheckedChanged);
            // 
            // YoutuveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 810);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.FpsUpDown);
            this.Controls.Add(this.VideoFotoPictureBox);
            this.Controls.Add(this.EndTextBox);
            this.Controls.Add(this.EndLabel);
            this.Controls.Add(this.StartTextBox);
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
            this.Controls.Add(this.FormatComboBox);
            this.Controls.Add(this.YoutuveLinkLabel);
            this.Controls.Add(this.YoutuveLinkTextBox);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.MinterpolateCheckBox);
            this.Controls.Add(this.MetadataCheckBox);
            this.Controls.Add(this.SubtitlesCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "YoutuveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Youtuve Downloader By Mrgaton";
            this.Shown += new System.EventHandler(this.YoutuveForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.VideoFotoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox YoutuveLinkTextBox;
        private System.Windows.Forms.Label YoutuveLinkLabel;
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
        private System.Windows.Forms.TextBox StartTextBox;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.TextBox EndTextBox;
        private System.Windows.Forms.Label EndLabel;
        private System.Windows.Forms.Timer TimeCheckTimer;
        private System.Windows.Forms.NumericUpDown FpsUpDown;
        private System.Windows.Forms.Label FpsLabel;
        private System.Windows.Forms.CheckBox MinterpolateCheckBox;
        private System.Windows.Forms.CheckBox SubtitlesCheckbox;
        private System.Windows.Forms.CheckBox MetadataCheckBox;
    }
}

