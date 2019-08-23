namespace NAudio_Spotify_Local
{
    partial class Play_Items
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Play_Items));
            this.lDuration = new System.Windows.Forms.Label();
            this.lArtist = new System.Windows.Forms.Label();
            this.lNameSong = new System.Windows.Forms.Label();
            this.actionBtn = new Bunifu.Framework.UI.BunifuImageButton();
            this.thumbnail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.actionBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // lDuration
            // 
            this.lDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDuration.AutoSize = true;
            this.lDuration.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDuration.Location = new System.Drawing.Point(577, 34);
            this.lDuration.Name = "lDuration";
            this.lDuration.Size = new System.Drawing.Size(48, 22);
            this.lDuration.TabIndex = 0;
            this.lDuration.Text = "0:00";
            // 
            // lArtist
            // 
            this.lArtist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lArtist.AutoSize = true;
            this.lArtist.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lArtist.Location = new System.Drawing.Point(427, 34);
            this.lArtist.Name = "lArtist";
            this.lArtist.Size = new System.Drawing.Size(68, 22);
            this.lArtist.TabIndex = 1;
            this.lArtist.Text = "Artista";
            // 
            // lNameSong
            // 
            this.lNameSong.AutoSize = true;
            this.lNameSong.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNameSong.Location = new System.Drawing.Point(190, 34);
            this.lNameSong.Name = "lNameSong";
            this.lNameSong.Size = new System.Drawing.Size(105, 23);
            this.lNameSong.TabIndex = 2;
            this.lNameSong.Text = "1.Canción";
            // 
            // actionBtn
            // 
            this.actionBtn.BackColor = System.Drawing.Color.Transparent;
            this.actionBtn.Image = global::NAudio_Spotify_Local.Properties.Resources.play2;
            this.actionBtn.ImageActive = null;
            this.actionBtn.Location = new System.Drawing.Point(112, 23);
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.Size = new System.Drawing.Size(50, 44);
            this.actionBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.actionBtn.TabIndex = 4;
            this.actionBtn.TabStop = false;
            this.actionBtn.Zoom = 10;
            this.actionBtn.Click += new System.EventHandler(this.actionBtn_Click);
            // 
            // thumbnail
            // 
            this.thumbnail.Image = ((System.Drawing.Image)(resources.GetObject("thumbnail.Image")));
            this.thumbnail.Location = new System.Drawing.Point(13, 12);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(64, 66);
            this.thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumbnail.TabIndex = 3;
            this.thumbnail.TabStop = false;
            // 
            // Play_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.thumbnail);
            this.Controls.Add(this.lNameSong);
            this.Controls.Add(this.lArtist);
            this.Controls.Add(this.lDuration);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Play_Items";
            this.Size = new System.Drawing.Size(696, 94);
            ((System.ComponentModel.ISupportInitialize)(this.actionBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDuration;
        private System.Windows.Forms.Label lArtist;
        private System.Windows.Forms.Label lNameSong;
        private System.Windows.Forms.PictureBox thumbnail;
        private Bunifu.Framework.UI.BunifuImageButton actionBtn;
    }
}
