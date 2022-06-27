namespace TransformationPicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Данный файл не открывается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap incoming = new Bitmap(pictureBox1.Image);
                Bitmap outgoing = new Bitmap(incoming.Width, incoming.Height);
                for (int h = 0; h < incoming.Height; h++)
                    for (int w = 0; w < incoming.Width; w++)
                    {
                        UInt32 pixel = (UInt32)(incoming.GetPixel(w, h).ToArgb());
                        float Red = (float)((pixel & 0x00FF0000) >> 16);
                        float Green = (float)((pixel & 0x0000FF00) >> 8);
                        float Blue = (float)(pixel & 0x000000FF);
                        Red = Green = Blue = (Red + Green + Blue) / 3.0f;
                        UInt32 newPixel = 0xFF000000 | ((UInt32)Red << 16) | ((UInt32)Green << 8) | ((UInt32)Blue);
                        outgoing.SetPixel(w, h, Color.FromArgb((int)newPixel));
                    }
                pictureBox1.Image = outgoing;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files(*.bmp)|*.bmp|Image Files(*.jpg)|*.jpg|Image Files(*.gif)|*.gif|Image Files(*.png)|*.png|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранение изображения";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.ShowHelp = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(saveFileDialog.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Изображение отсутствует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}