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
            this.filterEditorControl1 = new DevExpress.DataAccess.UI.FilterEditorControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlIte = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlIte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.filterEditorControl1);
            this.layoutControl1.Controls.Add(this.textEdit1);
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
            // filterEditorControl1
            // 
            this.filterEditorControl1.ActiveView = DevExpress.XtraFilterEditor.FilterEditorActiveView.Visual;
            this.filterEditorControl1.AppearanceEmptyValueColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceFieldNameColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceGroupOperatorColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceOperatorColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceValueColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.Location = new System.Drawing.Point(12, 461);
            this.filterEditorControl1.Name = "filterEditorControl1";
            this.filterEditorControl1.Size = new System.Drawing.Size(854, 20);
            this.filterEditorControl1.TabIndex = 10;
            this.filterEditorControl1.Text = "filterEditorControl1";
            this.filterEditorControl1.UseMenuForOperandsAndOperators = false;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(450, 12);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(416, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 9;
            // 
            // mapControl1
            // 
            this.mapControl1.Location = new System.Drawing.Point(21, 121);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(835, 336);
            this.mapControl1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(854, 56);
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
            this.trackBarControl1.Size = new System.Drawing.Size(416, 45);
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
            this.layoutControlIte,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(878, 493);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.trackBarControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(429, 49);
            this.layoutControlItem1.Text = "  ";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(6, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(848, 109);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 340);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.button1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(858, 60);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.mapControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(848, 340);
            this.layoutControlItem3.Text = "  ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(6, 13);
            // 
            // layoutControlIte
            // 
            this.layoutControlIte.Control = this.textEdit1;
            this.layoutControlIte.Location = new System.Drawing.Point(429, 0);
            this.layoutControlIte.Name = "layoutControlIte";
            this.layoutControlIte.Size = new System.Drawing.Size(429, 49);
            this.layoutControlIte.Text = " ";
            this.layoutControlIte.TextSize = new System.Drawing.Size(6, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.filterEditorControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 449);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(858, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
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
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlIte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlIte;
        private DevExpress.DataAccess.UI.FilterEditorControl filterEditorControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}

