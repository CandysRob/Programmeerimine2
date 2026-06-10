namespace KooliProjekt.WindowsForms
{
    partial class Form1
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            labelNimi = new Label();
            labelStartTime = new Label();
            labelEndTime = new Label();
            idField = new TextBox();
            nimiField = new TextBox();
            startTimeField = new TextBox();
            endTimeField = new TextBox();
            saveCommand = new Button();
            addCommand = new Button();
            deleteCommand = new Button();
            textboxKirjeldus = new TextBox();
            labelKirjeldus = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(525, 300);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(543, 18);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 1;
            label1.Text = "ID:";
            // 
            // labelNimi
            // 
            labelNimi.AutoSize = true;
            labelNimi.Location = new Point(543, 47);
            labelNimi.Name = "labelNimi";
            labelNimi.Size = new Size(36, 15);
            labelNimi.TabIndex = 2;
            labelNimi.Text = "Nimi:";
            // 
            // labelStartTime
            // 
            labelStartTime.AutoSize = true;
            labelStartTime.Location = new Point(543, 76);
            labelStartTime.Name = "labelStartTime";
            labelStartTime.Size = new Size(61, 15);
            labelStartTime.TabIndex = 11;
            labelStartTime.Text = "Start time:";
            // 
            // labelEndTime
            // 
            labelEndTime.AutoSize = true;
            labelEndTime.Location = new Point(543, 105);
            labelEndTime.Name = "labelEndTime";
            labelEndTime.Size = new Size(57, 15);
            labelEndTime.TabIndex = 12;
            labelEndTime.Text = "End time:";
            // 
            // idField
            // 
            idField.Location = new Point(620, 15);
            idField.Name = "idField";
            idField.ReadOnly = true;
            idField.Size = new Size(100, 23);
            idField.TabIndex = 3;
            idField.Text = "-1";
            // 
            // nimiField
            // 
            nimiField.Location = new Point(620, 44);
            nimiField.Name = "nimiField";
            nimiField.Size = new Size(100, 23);
            nimiField.TabIndex = 4;
            // 
            // startTimeField
            // 
            startTimeField.Location = new Point(620, 73);
            startTimeField.Name = "startTimeField";
            startTimeField.Size = new Size(100, 23);
            startTimeField.TabIndex = 5;
            // 
            // endTimeField
            // 
            endTimeField.Location = new Point(620, 102);
            endTimeField.Name = "endTimeField";
            endTimeField.Size = new Size(100, 23);
            endTimeField.TabIndex = 6;
            // 
            // saveCommand
            // 
            saveCommand.Location = new Point(542, 185);
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(75, 26);
            saveCommand.TabIndex = 8;
            saveCommand.Text = "Salvesta";
            saveCommand.UseVisualStyleBackColor = true;
            // 
            // addCommand
            // 
            addCommand.Location = new Point(632, 185);
            addCommand.Name = "addCommand";
            addCommand.Size = new Size(75, 26);
            addCommand.TabIndex = 9;
            addCommand.Text = "Lisa uus";
            addCommand.UseVisualStyleBackColor = true;
            // 
            // deleteCommand
            // 
            deleteCommand.Location = new Point(727, 185);
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(75, 26);
            deleteCommand.TabIndex = 10;
            deleteCommand.Text = "Kustuta";
            deleteCommand.UseVisualStyleBackColor = true;
            // 
            // textboxKirjeldus
            // 
            textboxKirjeldus.Location = new Point(620, 131);
            textboxKirjeldus.Name = "textboxKirjeldus";
            textboxKirjeldus.Size = new Size(100, 23);
            textboxKirjeldus.TabIndex = 14;
            // 
            // labelKirjeldus
            // 
            labelKirjeldus.AutoSize = true;
            labelKirjeldus.Location = new Point(543, 134);
            labelKirjeldus.Name = "labelKirjeldus";
            labelKirjeldus.Size = new Size(55, 15);
            labelKirjeldus.TabIndex = 13;
            labelKirjeldus.Text = "Kirjeldus:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(895, 338);
            Controls.Add(textboxKirjeldus);
            Controls.Add(labelKirjeldus);
            Controls.Add(deleteCommand);
            Controls.Add(addCommand);
            Controls.Add(saveCommand);
            Controls.Add(idField);
            Controls.Add(nimiField);
            Controls.Add(startTimeField);
            Controls.Add(endTimeField);
            Controls.Add(labelEndTime);
            Controls.Add(labelStartTime);
            Controls.Add(labelNimi);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label labelNimi;
        private Label labelStartTime;
        private Label labelEndTime;
        private TextBox idField;
        private TextBox nimiField;
        private TextBox startTimeField;
        private TextBox endTimeField;
        private Button saveCommand;
        private Button addCommand;
        private Button deleteCommand;
        private TextBox textboxKirjeldus;
        private Label labelKirjeldus;
    }
}
