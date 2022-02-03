using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A2B
{
    public class MainForm : Form
    {
        const string LINE = "\r\n";

        public MainForm()
        {
            InitializeComponent();
            mapTextBox.LostFocus += TargetTextChanged;
            targetTextBox.TextChanged += TargetTextChanged;
            revCheckBox.CheckedChanged += TargetTextChanged;
            copyButton.Click += CopyButton_Clikc;
        }

        private void CopyButton_Clikc(object sender, EventArgs e)
        {
            Clipboard.SetText(convertTextBox.Text);
        }

        private void TargetTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mapTextBox.Text)
                || string.IsNullOrWhiteSpace(targetTextBox.Text))
            {
                return;
            }
            convertTextBox.Text = "";

            var map = GetMap();
            var sb = new StringBuilder();
            var errSb = new StringBuilder();
            foreach (var text in targetTextBox.Text.Split(new string[] { LINE }, StringSplitOptions.None))
            {
                sb.AppendLine(A2B(text, map.Item1, map.Item2));
            }

            convertTextBox.Text = sb.ToString();
        }

        private string A2B(string text, string[] a, string[] b)
        {
            if (revCheckBox.Checked)
            {
                return Core.AtoBRev(text, a, b);
            }
            else
            {
                return Core.AtoB(text, a, b);
            }
        }

        private Tuple<string[], string[]> GetMap()
        {
            var text = mapTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var mapA = new List<string>();
            var mapB = new List<string>();
            foreach (var line in text.Split(new string[] { LINE }, StringSplitOptions.None))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var s = line.Split(new char[] { '=' });
                    mapA.Add(s[0]);
                    mapB.Add(s.Length > 1 ? s[1] : "");
                }
            }

            return Tuple.Create(mapA.ToArray(), mapB.ToArray());
        }

        private TextBox convertTextBox;
        private TextBox targetTextBox;
        private TextBox mapTextBox;
        private CheckBox revCheckBox;
        private Button copyButton;

        private void Add()
        {
            convertTextBox = new TextBox();
            targetTextBox = new TextBox();
            mapTextBox = new TextBox();
            revCheckBox = new CheckBox();
            copyButton = new Button();

            Controls.Add(copyButton);
            Controls.Add(revCheckBox);
            Controls.Add(mapTextBox);
            Controls.Add(targetTextBox);
            Controls.Add(convertTextBox);
        }
        private void InitializeComponent()
        {
            SuspendLayout();
            Add();

            convertTextBox.AcceptsReturn = true;
            convertTextBox.Location = new System.Drawing.Point(660, 0);
            convertTextBox.Multiline = true;
            convertTextBox.Name = "convertTextBox";
            convertTextBox.ReadOnly = true;
            convertTextBox.Size = new System.Drawing.Size(240, 400);
            convertTextBox.TabIndex = 0;
            convertTextBox.ScrollBars = ScrollBars.Both;

            targetTextBox.AcceptsReturn = true;
            targetTextBox.Location = new System.Drawing.Point(310, 0);
            targetTextBox.Multiline = true;
            targetTextBox.Name = "targetTextBox";
            targetTextBox.Size = new System.Drawing.Size(340, 450);
            targetTextBox.TabIndex = 0;
            targetTextBox.ScrollBars = ScrollBars.Both;

            mapTextBox.AcceptsReturn = true;
            mapTextBox.Location = new System.Drawing.Point(0, 0);
            mapTextBox.Multiline = true;
            mapTextBox.Name = "mapTextBox";
            mapTextBox.Size = new System.Drawing.Size(300, 450);
            mapTextBox.TabIndex = 0;
            mapTextBox.ScrollBars = ScrollBars.Both;
            mapTextBox.MaxLength = int.MaxValue;

            revCheckBox.AutoSize = true;
            revCheckBox.Location = new System.Drawing.Point(660, 410);
            revCheckBox.Name = "revCheckBox";
            revCheckBox.Size = new System.Drawing.Size(55, 24);
            revCheckBox.TabIndex = 1;
            revCheckBox.Text = "Rev";
            revCheckBox.UseVisualStyleBackColor = true;
            revCheckBox.Checked = true;

            copyButton.Location = new System.Drawing.Point(820, 410);
            copyButton.Name = "copyButton";
            copyButton.Size = new System.Drawing.Size(60, 30);
            copyButton.TabIndex = 1;
            copyButton.Text = "Copy";

            Font = new Font("Yu Gothic", 11);
            MaximizeBox = false;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 450);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "A2B";
            Text = "A2B";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
