namespace SimplifyPolyline
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.layoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.button2 = new System.Windows.Forms.Button();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.button2);
            this.layoutControl1.Controls.Add(this.textBox1);
            this.layoutControl1.Controls.Add(this.mapControl1);
            this.layoutControl1.Controls.Add(this.button1);
            this.layoutControl1.Controls.Add(this.trackBarControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(933, 245, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(878, 493);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // mapControl1
            // 
            this.mapControl1.Location = new System.Drawing.Point(21, 173);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(835, 308);
            this.mapControl1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(854, 60);
            this.button1.TabIndex = 5;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = null;
            this.trackBarControl1.Location = new System.Drawing.Point(21, 12);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Size = new System.Drawing.Size(845, 45);
            this.trackBarControl1.StyleController = this.layoutControl1;
            this.trackBarControl1.TabIndex = 4;
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.trackBarControl1_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(878, 493);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.trackBarControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(858, 49);
            this.layoutControlItem1.Text = "  ";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(6, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(848, 113);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 360);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.button1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(858, 64);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.mapControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 161);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(848, 312);
            this.layoutControlItem3.Text = "  ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(6, 13);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(835, 20);
            this.textBox1.TabIndex = 7;
            // 
            // layoutControlItem
            // 
            this.layoutControlItem.Control = this.textBox1;
            this.layoutControlItem.Location = new System.Drawing.Point(0, 113);
            this.layoutControlItem.Name = "layoutControlItem";
            this.layoutControlItem.Size = new System.Drawing.Size(848, 24);
            this.layoutControlItem.Text = " ";
            this.layoutControlItem.TextSize = new System.Drawing.Size(6, 13);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(844, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.button2;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 137);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(848, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 493);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraMap.MapControl mapControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}

