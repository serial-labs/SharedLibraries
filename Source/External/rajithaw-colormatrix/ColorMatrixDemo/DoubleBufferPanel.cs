using System.Windows.Forms;

namespace ColorMatrixDemoApp
{
	/// <summary>
	/// Represents a panel which can be used for drawing images real time without flickering
	/// </summary>
	public partial class DoubleBufferPanel : System.Windows.Forms.Panel
	{
		public DoubleBufferPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,	true);
		}
	}
}
