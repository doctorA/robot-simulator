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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rezkar));
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.datotekaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shraniJBIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odpriJBIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izhodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastavitveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_brisi = new System.Windows.Forms.Button();
            this.button_premakni = new System.Windows.Forms.Button();
            this.button_kvadrat = new System.Windows.Forms.Button();
            this.button_lok = new System.Windows.Forms.Button();
            this.button_krog = new System.Windows.Forms.Button();
            this.button_crta = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 27);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(562, 660);
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
            this.menuStrip1.Size = new System.Drawing.Size(795, 24);
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
            this.shraniJBIToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.shraniJBIToolStripMenuItem.Text = "Shrani JBI...";
            // 
            // odpriJBIToolStripMenuItem
            // 
            this.odpriJBIToolStripMenuItem.Name = "odpriJBIToolStripMenuItem";
            this.odpriJBIToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.odpriJBIToolStripMenuItem.Text = "Odpri JBI...";
            // 
            // izhodToolStripMenuItem
            // 
            this.izhodToolStripMenuItem.Name = "izhodToolStripMenuItem";
            this.izhodToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
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
            this.groupBox1.Controls.Add(this.button_brisi);
            this.groupBox1.Controls.Add(this.button_premakni);
            this.groupBox1.Controls.Add(this.button_kvadrat);
            this.groupBox1.Controls.Add(this.button_lok);
            this.groupBox1.Controls.Add(this.button_krog);
            this.groupBox1.Controls.Add(this.button_crta);
            this.groupBox1.Location = new System.Drawing.Point(594, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 668);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orodjarna";
            // 
            // button_brisi
            // 
            this.button_brisi.Image = ((System.Drawing.Image)(resources.GetObject("button_brisi.Image")));
            this.button_brisi.Location = new System.Drawing.Point(52, 560);
            this.button_brisi.Name = "button_brisi";
            this.button_brisi.Size = new System.Drawing.Size(100, 100);
            this.button_brisi.TabIndex = 5;
            this.button_brisi.UseVisualStyleBackColor = true;
            // 
            // button_premakni
            // 
            this.button_premakni.Image = ((System.Drawing.Image)(resources.GetObject("button_premakni.Image")));
            this.button_premakni.Location = new System.Drawing.Point(52, 454);
            this.button_premakni.Name = "button_premakni";
            this.button_premakni.Size = new System.Drawing.Size(100, 100);
            this.button_premakni.TabIndex = 4;
            this.button_premakni.UseVisualStyleBackColor = true;
            // 
            // button_kvadrat
            // 
            this.button_kvadrat.Image = ((System.Drawing.Image)(resources.GetObject("button_kvadrat.Image")));
            this.button_kvadrat.Location = new System.Drawing.Point(52, 242);
            this.button_kvadrat.Name = "button_kvadrat";
            this.button_kvadrat.Size = new System.Drawing.Size(100, 100);
            this.button_kvadrat.TabIndex = 3;
            this.button_kvadrat.UseVisualStyleBackColor = true;
            // 
            // button_lok
            // 
            this.button_lok.Image = ((System.Drawing.Image)(resources.GetObject("button_lok.Image")));
            this.button_lok.Location = new System.Drawing.Point(52, 136);
            this.button_lok.Name = "button_lok";
            this.button_lok.Size = new System.Drawing.Size(100, 100);
            this.button_lok.TabIndex = 1;
            this.button_lok.UseVisualStyleBackColor = true;
            // 
            // button_krog
            // 
            this.button_krog.Image = ((System.Drawing.Image)(resources.GetObject("button_krog.Image")));
            this.button_krog.Location = new System.Drawing.Point(52, 348);
            this.button_krog.Name = "button_krog";
            this.button_krog.Size = new System.Drawing.Size(100, 100);
            this.button_krog.TabIndex = 2;
            this.button_krog.UseVisualStyleBackColor = true;
            // 
            // button_crta
            // 
            this.button_crta.Image = ((System.Drawing.Image)(resources.GetObject("button_crta.Image")));
            this.button_crta.Location = new System.Drawing.Point(52, 30);
            this.button_crta.Name = "button_crta";
            this.button_crta.Size = new System.Drawing.Size(100, 100);
            this.button_crta.TabIndex = 0;
            this.button_crta.UseVisualStyleBackColor = true;
            // 
            // Rezkar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 696);
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
        private System.Windows.Forms.Button button_premakni;
        private System.Windows.Forms.Button button_kvadrat;
        private System.Windows.Forms.Button button_krog;
        private System.Windows.Forms.Button button_lok;
        private System.Windows.Forms.Button button_crta;
        private System.Windows.Forms.Button button_brisi;
    }
}