using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Image_Compressor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image using ImageSharp
                using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imagePath))
                {
                    // Set the size you want the image to be after compression
                    int targetWidth = 800;
                    int targetHeight = 600;

                    // Resize the image to the target size
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(targetWidth, targetHeight),
                        Mode = ResizeMode.Max
                    }));

                    // Compress the image to a lower quality
                    var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 60 };
                    var memoryStream = new MemoryStream();
                    image.Save(memoryStream, encoder);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Create a new Bitmap from the compressed image in the MemoryStream
                    Bitmap bitmap = new Bitmap(memoryStream);

                    // Set the PictureBox's Image property to the new Bitmap
                    pictureBox1.Image = bitmap;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Make sure there is an image loaded in the PictureBox
            if (pictureBox1.Image != null)
            {
                // Show a SaveFileDialog to let the user choose where to save the image
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the image to the selected file
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }

    }
}