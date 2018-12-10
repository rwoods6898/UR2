namespace Shapes
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
            this.cameraBox = new System.Windows.Forms.PictureBox();
            this.captureBox = new System.Windows.Forms.PictureBox();
            this.capture = new System.Windows.Forms.Button();
            this.shape = new System.Windows.Forms.Button();
            this.shapesBox = new System.Windows.Forms.PictureBox();
            this.shapeNum = new System.Windows.Forms.Label();
            this.yCord = new System.Windows.Forms.Label();
            this.xCord = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shapesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraBox
            // 
            this.cameraBox.Location = new System.Drawing.Point(12, 12);
            this.cameraBox.Name = "cameraBox";
            this.cameraBox.Size = new System.Drawing.Size(300, 250);
            this.cameraBox.TabIndex = 0;
            this.cameraBox.TabStop = false;
            // 
            // captureBox
            // 
            this.captureBox.Location = new System.Drawing.Point(334, 12);
            this.captureBox.Name = "captureBox";
            this.captureBox.Size = new System.Drawing.Size(300, 250);
            this.captureBox.TabIndex = 1;
            this.captureBox.TabStop = false;
            // 
            // capture
            // 
            this.capture.Location = new System.Drawing.Point(30, 331);
            this.capture.Name = "capture";
            this.capture.Size = new System.Drawing.Size(75, 23);
            this.capture.TabIndex = 2;
            this.capture.Text = "Capture";
            this.capture.UseVisualStyleBackColor = true;
            this.capture.Click += new System.EventHandler(this.capture_Click);
            // 
            // shape
            // 
            this.shape.Location = new System.Drawing.Point(123, 331);
            this.shape.Name = "shape";
            this.shape.Size = new System.Drawing.Size(110, 23);
            this.shape.TabIndex = 3;
            this.shape.Text = "Find Shapes";
            this.shape.UseVisualStyleBackColor = true;
            this.shape.Click += new System.EventHandler(this.shape_Click);
            // 
            // shapesBox
            // 
            this.shapesBox.Location = new System.Drawing.Point(649, 12);
            this.shapesBox.Name = "shapesBox";
            this.shapesBox.Size = new System.Drawing.Size(300, 250);
            this.shapesBox.TabIndex = 4;
            this.shapesBox.TabStop = false;
            // 
            // shapeNum
            // 
            this.shapeNum.AutoSize = true;
            this.shapeNum.Location = new System.Drawing.Point(250, 334);
            this.shapeNum.Name = "shapeNum";
            this.shapeNum.Size = new System.Drawing.Size(84, 17);
            this.shapeNum.TabIndex = 5;
            this.shapeNum.Text = "Coordinates";
            
            // 
            // yCord
            // 
            this.yCord.AutoSize = true;
            this.yCord.Location = new System.Drawing.Point(317, 369);
            this.yCord.Name = "yCord";
            this.yCord.Size = new System.Drawing.Size(17, 17);
            this.yCord.TabIndex = 6;
            this.yCord.Text = "Y";
            // 
            // xCord
            // 
            this.xCord.AutoSize = true;
            this.xCord.Location = new System.Drawing.Point(250, 369);
            this.xCord.Name = "xCord";
            this.xCord.Size = new System.Drawing.Size(17, 17);
            this.xCord.TabIndex = 7;
            this.xCord.Text = "X";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(135, 369);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(48, 17);
            this.status.TabIndex = 8;
            this.status.Text = "Status";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 450);
            this.Controls.Add(this.status);
            this.Controls.Add(this.xCord);
            this.Controls.Add(this.yCord);
            this.Controls.Add(this.shapeNum);
            this.Controls.Add(this.shapesBox);
            this.Controls.Add(this.shape);
            this.Controls.Add(this.capture);
            this.Controls.Add(this.captureBox);
            this.Controls.Add(this.cameraBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shapesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraBox;
        private System.Windows.Forms.PictureBox captureBox;
        private System.Windows.Forms.Button capture;
        private System.Windows.Forms.Button shape;
        private System.Windows.Forms.PictureBox shapesBox;
        private System.Windows.Forms.Label shapeNum;
        private System.Windows.Forms.Label yCord;
        private System.Windows.Forms.Label xCord;
        private System.Windows.Forms.Label status;
    }
}

