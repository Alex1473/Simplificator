﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraMap;
using System.Drawing;

namespace SimplifyPolyline
{
    public partial class Form1 : Form {
        VectorItemsLayer itemsLayer = new VectorItemsLayer();
        MapItemStorage mapItemStorage = new MapItemStorage();
        PolylineSimplificator polylineSimplificator = new PolylineSimplificator(new SimplificationWeightsCalculator(new DouglasPeuckerWeightsCalculator()), new SimplificationFilterPointsByWeight());
        
        public Form1() {
            InitializeComponent();
            this.mapControl1.Layers.Add(itemsLayer);
            itemsLayer.Data = mapItemStorage;
            this.trackBarControl1.Properties.Maximum = 100;
            this.textEdit1.EditValueChanged += OnEditValueChanged;
           
        }

        private void button1_Click(object sender, EventArgs e) {
            ShapefileDataAdapter dataAdapter = new ShapefileDataAdapter();
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK) 
                 dataAdapter.FileUri = new Uri(dialog.FileName);
            dataAdapter.ItemsLoaded += OnItemsLoaded;
            dataAdapter.Load();        
        }

        void OnItemsLoaded(object sender, ItemsLoadedEventArgs e) {
            this.mapItemStorage.Items.Clear();
            this.polylineSimplificator.Prepare(e.Items);
            this.mapItemStorage.Items.AddRange(this.polylineSimplificator.Simplify(100 -this.trackBarControl1.Value).ToArray());
        }

        void OnEditValueChanged(object sender, EventArgs e) {
            double procent;
            if (!double.TryParse(this.textEdit1.Text, out procent))
                return;
            procent = Math.Round(double.Parse(this.textEdit1.Text));

            this.trackBarControl1.Value = 100 - (int)procent;
            this.mapItemStorage.Items.Clear();
            this.mapItemStorage.Items.AddRange(this.polylineSimplificator.Simplify(procent).ToArray());
        }
        void trackBarControl1_EditValueChanged(object sender, EventArgs e) {
            this.mapItemStorage.Items.Clear();
            this.mapItemStorage.Items.AddRange(this.polylineSimplificator.Simplify(100 - this.trackBarControl1.Value).ToArray());
            this.textEdit1.Text = (100 - this.trackBarControl1.Value).ToString();
        }

        
    }
}