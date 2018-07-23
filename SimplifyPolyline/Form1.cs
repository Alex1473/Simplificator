using System;
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
using System.Diagnostics;
using SimplifyPolyline.PolygonsSpliter;

namespace SimplifyPolyline
{
    public partial class Form1 : Form {
        const string DouglasPeuckerAlgorithmName = "Douglas-Peucker";
        const string VislalingamAlgorithmName = "Visvalingam";

        VectorItemsLayer itemsLayer = new VectorItemsLayer();
        MapItemStorage mapItemStorage = new MapItemStorage();
        PolylineSimplificator polylineSimplificator = new PolylineSimplificator(new SimplificationBySegmentWeightedCalculator(new VislalingamEffectiveAreaWeightsCalculator()), 
            new SimplificationFilterPointsByWeight());
        Stopwatch stopWatch = new Stopwatch();
        IList<MapItem> items;

        public Form1() {
            InitializeComponent();
            this.mapControl1.Layers.Add(itemsLayer);
            itemsLayer.Data = mapItemStorage;
            this.trackBarControl1.Properties.Maximum = 100;
            this.textEdit1.EditValueChanged += OnEditValueChanged;
            this.comboBox1.Items.AddRange(new string[] { DouglasPeuckerAlgorithmName, VislalingamAlgorithmName });
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            
           
        }

        private void button1_Click(object sender, EventArgs e) {
            ShapefileDataAdapter dataAdapter = new ShapefileDataAdapter();
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK) 
                 dataAdapter.FileUri = new Uri(dialog.FileName);
            dataAdapter.ItemsLoaded += OnItemsLoaded;
            dataAdapter.Load();        
        }

        void OnItemsLoaded(object sender, ItemsLoadedEventArgs e) {
            this.items = e.Items;
            this.mapControl1.ZoomToFit(e.Items);
            this.comboBox1.SelectedIndex = 1;
        }

        void OnEditValueChanged(object sender, EventArgs e) {
            double procent;
            if (!double.TryParse(this.textEdit1.Text, out procent))
                return;
            procent = Math.Round(double.Parse(this.textEdit1.Text));
            this.trackBarControl1.Value = 100 - (int)procent;
            SimplificationProcess(procent);
        }
        void trackBarControl1_EditValueChanged(object sender, EventArgs e) {
            this.textEdit1.Text = (100 - this.trackBarControl1.Value).ToString();
        }

        void SimplificationProcess(double procent) {
            stopWatch.Reset();
            stopWatch.Start();
            this.mapControl1.SuspendRender();
            this.mapItemStorage.Items.Clear();
            this.mapItemStorage.Items.AddRange(this.polylineSimplificator.Simplify(procent).ToArray());
            this.mapControl1.ResumeRender();
            stopWatch.Stop();
            this.label1.Text = stopWatch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void button2_Click(object sender, EventArgs e) {
            this.itemsLayer.ExportToShp("C:\\compressedFile.shp", new ShpExportOptions());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            string algorithmName = this.comboBox1.SelectedItem.ToString();
            stopWatch.Reset();
            stopWatch.Start();
            if (algorithmName == DouglasPeuckerAlgorithmName)
                this.polylineSimplificator = new PolylineSimplificator(new SimplificationBySegmentWeightedCalculator(new DouglasPeuckerWeightsCalculator()), 
                    new SimplificationFilterPointsByWeight());

            if (algorithmName == VislalingamAlgorithmName)
                this.polylineSimplificator = new PolylineSimplificator(new SimplificationBySegmentWeightedCalculator(new VislalingamEffectiveAreaWeightsCalculator()), 
                    new SimplificationFilterPointsByWeight());

            this.polylineSimplificator.Prepare(this.items);
            SimplificationProcess(100 - this.trackBarControl1.Value);
            stopWatch.Stop();
            this.label1.Text = stopWatch.ElapsedMilliseconds.ToString() + " ms";
        }
    }
}