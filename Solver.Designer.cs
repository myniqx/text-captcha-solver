namespace text_captcha_solver
{
	partial class Solver
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.outPut = new System.Windows.Forms.RichTextBox();
			this.train = new System.Windows.Forms.Button();
			this.refresh = new System.Windows.Forms.Button();
			this.prediction = new System.Windows.Forms.TextBox();
			this.captchaBox = new System.Windows.Forms.PictureBox();
			this.clear_samples = new System.Windows.Forms.Button();
			this.generate_samples = new System.Windows.Forms.Button();
			this.sample_repeat = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.c1 = new System.Windows.Forms.PictureBox();
			this.c2 = new System.Windows.Forms.PictureBox();
			this.c3 = new System.Windows.Forms.PictureBox();
			this.c4 = new System.Windows.Forms.PictureBox();
			this.sample_letters = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.epoch = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.batchSize = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.validationSize = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.status = new System.Windows.Forms.Label();
			this.reset_letters = new System.Windows.Forms.Button();
			this.model_list = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.captchaBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sample_repeat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.epoch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.batchSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.validationSize)).BeginInit();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(269, 442);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(810, 20);
			this.progressBar.TabIndex = 1;
			// 
			// outPut
			// 
			this.outPut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outPut.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.outPut.Location = new System.Drawing.Point(269, 12);
			this.outPut.Name = "outPut";
			this.outPut.Size = new System.Drawing.Size(810, 398);
			this.outPut.TabIndex = 2;
			this.outPut.Text = "";
			// 
			// train
			// 
			this.train.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.train.Location = new System.Drawing.Point(1085, 424);
			this.train.Name = "train";
			this.train.Size = new System.Drawing.Size(103, 38);
			this.train.TabIndex = 3;
			this.train.Text = "Train";
			this.train.UseVisualStyleBackColor = true;
			this.train.Click += new System.EventHandler(this.train_Click);
			// 
			// refresh
			// 
			this.refresh.Location = new System.Drawing.Point(12, 12);
			this.refresh.Name = "refresh";
			this.refresh.Size = new System.Drawing.Size(75, 25);
			this.refresh.TabIndex = 4;
			this.refresh.Text = "Refresh";
			this.refresh.UseVisualStyleBackColor = true;
			this.refresh.Click += new System.EventHandler(this.refresh_Click);
			// 
			// prediction
			// 
			this.prediction.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.prediction.Location = new System.Drawing.Point(94, 14);
			this.prediction.Name = "prediction";
			this.prediction.Size = new System.Drawing.Size(169, 23);
			this.prediction.TabIndex = 5;
			this.prediction.Text = "XXXX";
			this.prediction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// captchaBox
			// 
			this.captchaBox.Location = new System.Drawing.Point(12, 74);
			this.captchaBox.Name = "captchaBox";
			this.captchaBox.Size = new System.Drawing.Size(251, 81);
			this.captchaBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.captchaBox.TabIndex = 6;
			this.captchaBox.TabStop = false;
			// 
			// clear_samples
			// 
			this.clear_samples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.clear_samples.Location = new System.Drawing.Point(1085, 14);
			this.clear_samples.Name = "clear_samples";
			this.clear_samples.Size = new System.Drawing.Size(103, 23);
			this.clear_samples.TabIndex = 7;
			this.clear_samples.Text = "Clear Samples";
			this.clear_samples.UseVisualStyleBackColor = true;
			this.clear_samples.Click += new System.EventHandler(this.clear_samples_Click);
			// 
			// generate_samples
			// 
			this.generate_samples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.generate_samples.Location = new System.Drawing.Point(1085, 57);
			this.generate_samples.Name = "generate_samples";
			this.generate_samples.Size = new System.Drawing.Size(103, 23);
			this.generate_samples.TabIndex = 7;
			this.generate_samples.Text = "Generate Samples";
			this.generate_samples.UseVisualStyleBackColor = true;
			this.generate_samples.Click += new System.EventHandler(this.generate_samples_Click);
			// 
			// sample_repeat
			// 
			this.sample_repeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sample_repeat.Location = new System.Drawing.Point(1133, 86);
			this.sample_repeat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.sample_repeat.Name = "sample_repeat";
			this.sample_repeat.Size = new System.Drawing.Size(55, 20);
			this.sample_repeat.TabIndex = 8;
			this.sample_repeat.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(1085, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Repeat";
			// 
			// c1
			// 
			this.c1.Location = new System.Drawing.Point(12, 161);
			this.c1.Name = "c1";
			this.c1.Size = new System.Drawing.Size(58, 78);
			this.c1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.c1.TabIndex = 6;
			this.c1.TabStop = false;
			// 
			// c2
			// 
			this.c2.Location = new System.Drawing.Point(76, 161);
			this.c2.Name = "c2";
			this.c2.Size = new System.Drawing.Size(58, 78);
			this.c2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.c2.TabIndex = 6;
			this.c2.TabStop = false;
			// 
			// c3
			// 
			this.c3.Location = new System.Drawing.Point(140, 161);
			this.c3.Name = "c3";
			this.c3.Size = new System.Drawing.Size(58, 78);
			this.c3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.c3.TabIndex = 6;
			this.c3.TabStop = false;
			// 
			// c4
			// 
			this.c4.Location = new System.Drawing.Point(204, 161);
			this.c4.Name = "c4";
			this.c4.Size = new System.Drawing.Size(58, 78);
			this.c4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.c4.TabIndex = 6;
			this.c4.TabStop = false;
			// 
			// sample_letters
			// 
			this.sample_letters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sample_letters.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.sample_letters.Location = new System.Drawing.Point(1088, 144);
			this.sample_letters.Multiline = true;
			this.sample_letters.Name = "sample_letters";
			this.sample_letters.Size = new System.Drawing.Size(100, 46);
			this.sample_letters.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(1085, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Letters :";
			// 
			// chart
			// 
			this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
			chartArea1.AxisX.IsLabelAutoFit = false;
			chartArea1.AxisX.LabelStyle.Enabled = false;
			chartArea1.Name = "ChartArea";
			chartArea1.ShadowOffset = 1;
			this.chart.ChartAreas.Add(chartArea1);
			this.chart.Cursor = System.Windows.Forms.Cursors.Default;
			legend1.Alignment = System.Drawing.StringAlignment.Center;
			legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
			legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
			legend1.Name = "Legend";
			this.chart.Legends.Add(legend1);
			this.chart.Location = new System.Drawing.Point(12, 245);
			this.chart.Margin = new System.Windows.Forms.Padding(2);
			this.chart.Name = "chart";
			this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
			series1.BorderWidth = 2;
			series1.ChartArea = "ChartArea";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series1.Color = System.Drawing.Color.Maroon;
			series1.Legend = "Legend";
			series1.Name = "Acc";
			series1.ShadowOffset = 1;
			series2.ChartArea = "ChartArea";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series2.Color = System.Drawing.Color.Blue;
			series2.Legend = "Legend";
			series2.Name = "Loss";
			series2.ShadowOffset = 1;
			this.chart.Series.Add(series1);
			this.chart.Series.Add(series2);
			this.chart.Size = new System.Drawing.Size(251, 217);
			this.chart.TabIndex = 11;
			this.chart.Text = "chart1";
			// 
			// epoch
			// 
			this.epoch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.epoch.Location = new System.Drawing.Point(1133, 346);
			this.epoch.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			this.epoch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.epoch.Name = "epoch";
			this.epoch.Size = new System.Drawing.Size(55, 20);
			this.epoch.TabIndex = 8;
			this.epoch.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(1085, 348);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Epoch";
			// 
			// batchSize
			// 
			this.batchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.batchSize.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.batchSize.Location = new System.Drawing.Point(1133, 372);
			this.batchSize.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
			this.batchSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.batchSize.Name = "batchSize";
			this.batchSize.Size = new System.Drawing.Size(55, 20);
			this.batchSize.TabIndex = 8;
			this.batchSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(1085, 374);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Batch";
			// 
			// validationSize
			// 
			this.validationSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.validationSize.DecimalPlaces = 2;
			this.validationSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.validationSize.Location = new System.Drawing.Point(1133, 398);
			this.validationSize.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.validationSize.Name = "validationSize";
			this.validationSize.Size = new System.Drawing.Size(55, 20);
			this.validationSize.TabIndex = 8;
			this.validationSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(1085, 400);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(22, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Val";
			// 
			// status
			// 
			this.status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.status.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.status.Location = new System.Drawing.Point(269, 417);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(810, 22);
			this.status.TabIndex = 12;
			this.status.Text = "capthca solver ready..";
			// 
			// reset_letters
			// 
			this.reset_letters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.reset_letters.Location = new System.Drawing.Point(1146, 114);
			this.reset_letters.Name = "reset_letters";
			this.reset_letters.Size = new System.Drawing.Size(42, 24);
			this.reset_letters.TabIndex = 13;
			this.reset_letters.Text = "reset";
			this.reset_letters.UseVisualStyleBackColor = true;
			this.reset_letters.Click += new System.EventHandler(this.reset_letters_Click);
			// 
			// model_list
			// 
			this.model_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.model_list.FormattingEnabled = true;
			this.model_list.Location = new System.Drawing.Point(13, 44);
			this.model_list.Name = "model_list";
			this.model_list.Size = new System.Drawing.Size(249, 21);
			this.model_list.TabIndex = 14;
			this.model_list.SelectedIndexChanged += new System.EventHandler(this.model_list_SelectedIndexChanged);
			// 
			// Solver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 474);
			this.Controls.Add(this.model_list);
			this.Controls.Add(this.reset_letters);
			this.Controls.Add(this.status);
			this.Controls.Add(this.chart);
			this.Controls.Add(this.sample_letters);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.validationSize);
			this.Controls.Add(this.batchSize);
			this.Controls.Add(this.epoch);
			this.Controls.Add(this.sample_repeat);
			this.Controls.Add(this.generate_samples);
			this.Controls.Add(this.clear_samples);
			this.Controls.Add(this.c4);
			this.Controls.Add(this.c3);
			this.Controls.Add(this.c2);
			this.Controls.Add(this.c1);
			this.Controls.Add(this.captchaBox);
			this.Controls.Add(this.prediction);
			this.Controls.Add(this.refresh);
			this.Controls.Add(this.train);
			this.Controls.Add(this.outPut);
			this.Controls.Add(this.progressBar);
			this.Name = "Solver";
			this.Text = "captcha trainer/solver";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Solver_FormClosing);
			this.Load += new System.EventHandler(this.Solver_Load);
			((System.ComponentModel.ISupportInitialize)(this.captchaBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sample_repeat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.epoch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.batchSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.validationSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RichTextBox outPut;
		private System.Windows.Forms.Button train;
		private System.Windows.Forms.Button refresh;
		private System.Windows.Forms.TextBox prediction;
		private System.Windows.Forms.PictureBox captchaBox;
		private System.Windows.Forms.Button clear_samples;
		private System.Windows.Forms.Button generate_samples;
		private System.Windows.Forms.NumericUpDown sample_repeat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox c1;
		private System.Windows.Forms.PictureBox c2;
		private System.Windows.Forms.PictureBox c3;
		private System.Windows.Forms.PictureBox c4;
		private System.Windows.Forms.TextBox sample_letters;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.ProgressBar progressBar;
		public System.Windows.Forms.DataVisualization.Charting.Chart chart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown batchSize;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown validationSize;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.Label status;
		private System.Windows.Forms.Button reset_letters;
		public System.Windows.Forms.NumericUpDown epoch;
		private System.Windows.Forms.ComboBox model_list;
	}
}

