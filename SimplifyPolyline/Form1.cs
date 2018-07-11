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
        IEnumerable<MapItem> items;
        DouglasPeuckerSimplyfier douglasPeuckerSimplyfier = new DouglasPeuckerSimplyfier();
        

        public Form1() {
            InitializeComponent();
            this.mapControl1.Layers.Add(itemsLayer);
            itemsLayer.Data = mapItemStorage;
            this.trackBarControl1.Properties.Maximum = 100;
            //this.trackBarControl1.Properties.SmallChangeUseMode = new DevExpress.XtraEditors.Repository.SmallChangeUseMode();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

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
            this.items = e.Items;
            //this.mapItemStorage.Items.AddRange(e.Items.ToArray());
            MapItemsSimplificator simplificator = new MapItemsSimplificator();
           
            this.mapItemStorage.Items.AddRange(simplificator.Simplify(e.Items, (100 - this.trackBarControl1.Value)).ToArray());
        }

        IEnumerable<MapItem> SimplifyItems(IEnumerable<MapItem> items, double procent) {
            List<MapItem> simpleMapItems = new List<MapItem>();
            foreach (MapItem item in items) 
                simpleMapItems.Add(SimplifyMapPath(item as MapPath, procent));
            return simpleMapItems;
        }

        MapItem SimplifyMapPath(MapPath mapPath, double procent) {
            MapPath simpleMapPath = new MapPath();
            foreach (MapPathSegment segment in mapPath.Segments) {
                MapPathSegment simpleMapPathSegment = new MapPathSegment();
                simpleMapPathSegment.Points.AddRange(this.douglasPeuckerSimplyfier.Simplify(segment.Points.ToArray(), (100 - procent)));
                simpleMapPath.Segments.Add(simpleMapPathSegment);
            }
            return simpleMapPath;
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e) {
            this.mapItemStorage.Items.Clear();
            MapItemsSimplificator simplificator = new MapItemsSimplificator();
            this.mapItemStorage.Items.AddRange(simplificator.Simplify(this.items, (100 - this.trackBarControl1.Value)).ToArray());
            this.textBox1.Text = (100 - this.trackBarControl1.Value).ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            double procent = Math.Round(double.Parse(this.textBox1.Text));
            this.trackBarControl1.Value = 100 - (int)procent;
            this.mapItemStorage.Items.Clear();
            MapItemsSimplificator simplificator = new MapItemsSimplificator();
            this.mapItemStorage.Items.AddRange(simplificator.Simplify(this.items, procent).ToArray());

        }
    }
}