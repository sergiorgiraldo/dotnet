using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace DragDropFiles
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblFileSize;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label status;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblFileSize = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.AllowDrop = true;
			this.listBox1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new System.Drawing.Point(8, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(376, 49);
			this.listBox1.TabIndex = 0;
			this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
			this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(392, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "File size:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFileSize
			// 
			this.lblFileSize.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFileSize.Location = new System.Drawing.Point(480, 8);
			this.lblFileSize.Name = "lblFileSize";
			this.lblFileSize.Size = new System.Drawing.Size(88, 16);
			this.lblFileSize.TabIndex = 3;
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 72);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(376, 24);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "";
			// 
			// status
			// 
			this.status.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.status.ForeColor = System.Drawing.Color.Red;
			this.status.Location = new System.Drawing.Point(395, 77);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(165, 16);
			this.status.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 16);
			this.ClientSize = new System.Drawing.Size(600, 109);
			this.Controls.Add(this.status);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.lblFileSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Form1";
			this.Text = "Quick Compare";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		private String GetHash(string sFile) 
		{
			MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();
			FileStream stmcheck = File.OpenRead(sFile);
			byte[] hash = csp.ComputeHash(stmcheck);
			stmcheck.Close();
			return BitConverter.ToString(hash).Replace("-", "").ToLower();			
		}

		private void listBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// make sure they're actually dropping files (not text or anything else)
			if( e.Data.GetDataPresent(DataFormats.FileDrop, false) == true )
				// allow them to continue
				// (without this, the cursor stays a "NO" symbol
				e.Effect = DragDropEffects.All;
		}

		private void listBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// transfer the filenames to a string array
			// (yes, everything to the left of the "=" can be put in the 
			// foreach loop in place of "files", but this is easier to understand.)
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			if (listBox1.Items.Count == 2)
			{
				listBox1.Items.Clear();
			}		

			// loop through the string array, adding each filename to the ListBox
			foreach( string file in files )
			{
				listBox1.Items.Add(file);
			}

			if (listBox1.Items.Count == 2)
			{
				if (GetHash(listBox1.Items[0].ToString()) == GetHash(listBox1.Items[1].ToString()))
				{
					status.Text = "Iguais !";
					status.ForeColor = Color.Green;
				}
				else
				{
					status.Text = "Diferentes !";
					status.ForeColor = Color.Red;
				}
			}		

		}

		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			double len=0;
			byte type=0;
			string typeName="";

			// grab file info
			System.IO.FileInfo file = new System.IO.FileInfo(listBox1.Items[listBox1.SelectedIndex].ToString());

			// directories crash the program :|
			if( (file.Attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory) 
			{
				lblFileSize.Text = "This is a directory.";
				return;
			}

			// we just want the length
			len = (double)file.Length;

			// beat it down a bit until it's a number smaller than 1024
			while( len > 1024 )
			{
				len /= 1024;
				type++;
			}
			
			// change the descriptive name based on how many times we divided
			switch(type)
			{
				case 0:
					typeName=" bytes";
					break;
				case 1:
					typeName=" kb";
					break;
				case 2:
					typeName=" MB";
					break;
				case 3:
					typeName=" GB";
					break;
				default:	// what is this? Just make it bytes...
					len = (double)file.Length;
					typeName=" bytes";
					break;
			}

			// cut off 00 from the end of it
			string size = len.ToString("F");
			if( size.EndsWith(".00") )
				size = size.Remove(size.Length-3,3);

			// set the label
			lblFileSize.Text = size + typeName;

			textBox1.Text = GetHash(file.FullName);
		}


	}
}
