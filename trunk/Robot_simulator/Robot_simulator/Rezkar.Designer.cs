namespace Robot_simulator
{
    partial class Rezkar
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
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.datotekaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shraniJBIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odpriJBIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izhodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastavitveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_crta = new System.Windows.Forms.Button();
            this.button_lok = new System.Windows.Forms.Button();
            this.button_krog = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 27);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(562, 595);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datotekaToolStripMenuItem,
            this.nastavitveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // datotekaToolStripMenuItem
            // 
            this.datotekaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shraniJBIToolStripMenuItem,
            this.odpriJBIToolStripMenuItem,
            this.izhodToolStripMenuItem});
            this.datotekaToolStripMenuItem.Name = "datotekaToolStripMenuItem";
            this.datotekaToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.datotekaToolStripMenuItem.Text = "Datoteka";
            // 
            // shraniJBIToolStripMenuItem
            // 
            this.shraniJBIToolStripMenuItem.Name = "shraniJBIToolStripMenuItem";
            this.shraniJBIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shraniJBIToolStripMenuItem.Text = "Shrani JBI...";
            // 
            // odpriJBIToolStripMenuItem
            // 
            this.odpriJBIToolStripMenuItem.Name = "odpriJBIToolStripMenuItem";
            this.odpriJBIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.odpriJBIToolStripMenuItem.Text = "Odpri JBI...";
            // 
            // izhodToolStripMenuItem
            // 
            this.izhodToolStripMenuItem.Name = "izhodToolStripMenuItem";
            this.izhodToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.izhodToolStripMenuItem.Text = "Izhod";
            // 
            // nastavitveToolStripMenuItem
            // 
            this.nastavitveToolStripMenuItem.Name = "nastavitveToolStripMenuItem";
            this.nastavitveToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.nastavitveToolStripMenuItem.Text = "Nastavitve";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button_krog);
            this.groupBox1.Controls.Add(this.button_lok);
            this.groupBox1.Controls.Add(this.button_crta);
            this.groupBox1.Location = new System.Drawing.Point(594, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 595);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orodjarna";
            // 
            // button_crta
            // 
            this.button_crta.Location = new System.Drawing.Point(44, 19);
            this.button_crta.Name = "button_crta";
            this.button_crta.Size = new System.Drawing.Size(96, 90);
            this.button_crta.TabIndex = 0;
            this.button_crta.Text = "Črta";
            this.button_crta.UseVisualStyleBackColor = true;
            // 
            // button_lok
            // 
            this.button_lok.Location = new System.Drawing.Point(44, 115);
            this.button_lok.Name = "button_lok";
            this.button_lok.Size = new System.Drawing.Size(96, 90);
            this.button_lok.TabIndex = 1;
            this.button_lok.Text = "Lok";
            this.button_lok.UseVisualStyleBackColor = true;
            // 
            // button_krog
            // 
            this.button_krog.Location = new System.Drawing.Point(44, 211);
            this.button_krog.Name = "button_krog";
            this.button_krog.Size = new System.Drawing.Size(96, 90);
            this.button_krog.TabIndex = 2;
            this.button_krog.Text = "Krog";
            this.button_krog.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(44, 307);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 90);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(44, 403);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 90);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(44, 499);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 90);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // Rezkar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 644);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Rezkar";
            this.Text = "Rezkar";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem datotekaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shraniJBIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odpriJBIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izhodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nastavitveToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button_krog;
        private System.Windows.Forms.Button button_lok;
        private System.Windows.Forms.Button button_crta;
        private System.Windows.Forms.Button button6;
    }
}