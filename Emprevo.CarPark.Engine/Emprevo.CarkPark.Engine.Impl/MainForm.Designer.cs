namespace Emprevo.CarkPark.Engine.Impl
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            entryDateTimePicker = new DateTimePicker();
            exitDateTimePicker = new DateTimePicker();
            entryTime = new Label();
            exitTime = new Label();
            calculateBtn = new Button();
            rateResultBox = new RichTextBox();
            SuspendLayout();
            // 
            // entryDateTimePicker
            // 
            entryDateTimePicker.AccessibleName = "entryTime";
            entryDateTimePicker.Location = new Point(55, 100);
            entryDateTimePicker.Name = "entryDateTimePicker";
            entryDateTimePicker.Size = new Size(300, 31);
            entryDateTimePicker.TabIndex = 0;
            // 
            // exitDateTimePicker
            // 
            exitDateTimePicker.AccessibleName = "exitTime";
            exitDateTimePicker.Location = new Point(55, 229);
            exitDateTimePicker.Name = "exitDateTimePicker";
            exitDateTimePicker.Size = new Size(300, 31);
            exitDateTimePicker.TabIndex = 1;
            // 
            // entryTime
            // 
            entryTime.AutoSize = true;
            entryTime.Location = new Point(55, 72);
            entryTime.Name = "entryTime";
            entryTime.Size = new Size(95, 25);
            entryTime.TabIndex = 2;
            entryTime.Text = "Entry Time";
            // 
            // exitTime
            // 
            exitTime.AutoSize = true;
            exitTime.Location = new Point(55, 201);
            exitTime.Name = "exitTime";
            exitTime.Size = new Size(82, 25);
            exitTime.TabIndex = 3;
            exitTime.Text = "Exit Time";
            // 
            // calculateBtn
            // 
            calculateBtn.Location = new Point(55, 345);
            calculateBtn.Name = "calculateBtn";
            calculateBtn.Size = new Size(112, 34);
            calculateBtn.TabIndex = 4;
            calculateBtn.Text = "Calculate";
            calculateBtn.UseVisualStyleBackColor = true;
            calculateBtn.Click += calculateBtn_Click;
            // 
            // rateResultBox
            // 
            rateResultBox.Location = new Point(388, 100);
            rateResultBox.Name = "rateResultBox";
            rateResultBox.Size = new Size(368, 279);
            rateResultBox.TabIndex = 5;
            rateResultBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rateResultBox);
            Controls.Add(calculateBtn);
            Controls.Add(exitTime);
            Controls.Add(entryTime);
            Controls.Add(exitDateTimePicker);
            Controls.Add(entryDateTimePicker);
            Name = "MainForm";
            Text = "Car Park Engine";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker entryDateTimePicker;
        private DateTimePicker exitDateTimePicker;
        private Label entryTime;
        private Label exitTime;
        private Button calculateBtn;
        private RichTextBox rateResultBox;
    }
}
