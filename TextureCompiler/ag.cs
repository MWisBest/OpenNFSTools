using System.Drawing;
using System.Windows.Forms;

public class ag : Form
{
	private PictureBox pictureBoxA; // obf: "a"

	//private Container containerB = null; // obf: "b"

	public ag()
	{
		this.a();
	}

	/*
	protected override void a(bool A_0)
	{
		if (A_0 && this.containerB != null)
		{
			this.containerB.Dispose();
		}
		base.Dispose(A_0);
	}
	*/

	private void a()
	{
		this.pictureBoxA = new PictureBox();
		base.SuspendLayout();
		this.pictureBoxA.Location = new Point( 0, 0 );
		this.pictureBoxA.Name = "picBox";
		this.pictureBoxA.Size = new Size( 120, 104 );
		this.pictureBoxA.TabIndex = 0;
		this.pictureBoxA.TabStop = false;
		this.AutoScaleBaseSize = new Size( 5, 13 );
		this.AutoScroll = true;
		base.ClientSize = new Size( 480, 453 );
		base.Controls.Add( this.pictureBoxA );
		base.Name = "TextureDisplay";
		this.Text = "TextureDisplay";
		base.ResumeLayout( false );
	}

	public void a( Image A_0 )
	{
		this.pictureBoxA.Image = A_0;
		this.pictureBoxA.Width = A_0.Width;
		this.pictureBoxA.Height = A_0.Height;
	}
}
