using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace qr_code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var qrtext = textBox1.Text; //считываем текст из TextBox'a
            var encoder = new QRCodeEncoder(); //создаём новую "генерацию кода"
            var qrcode = encoder.Encode(qrtext, Encoding.UTF8); // кодируем слово, полученное из TextBox'a (qrtext) в переменную qrcode. класса Bitmap(класс, который используется для работы с изображениями)
            pictureBox1.Image = qrcode; // pictureBox выводит qrcode как изображение.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog {Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp"};
                // save будет запрашивать у пользователя место, в которое он захочет сохранить файл. 
            //создаём фильтр, который определяет, в каких форматах мы сможем сохранить нашу информацию. В данном случае выбираем форматы изображений. Записывается так: "название_формата_в обозревателе|*.расширение_формата")
            if (save.ShowDialog() == DialogResult.OK) //если пользователь нажимает в обозревателе кнопку "Сохранить".
            {
                pictureBox1.Image.Save(save.FileName); //изображение из pictureBox'a сохраняется под именем, которое введёт пользователь
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var load = new OpenFileDialog(); //  load будет запрашивать у пользователя место, из которого он хочет загрузить файл.
            if (load.ShowDialog() == DialogResult.OK) // //если пользователь нажимает в обозревателе кнопку "Открыть".
            {
                pictureBox1.ImageLocation = load.FileName; // в pictureBox'e открывается файл, который был по пути, запрошенном пользователем.
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var decoder = new QRCodeDecoder(); // создаём "раскодирование изображения"
            var utfMessage = decoder.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap), Encoding.UTF8);
            if (utfMessage == null) throw new ArgumentNullException(nameof(utfMessage));
            MessageBox.Show(utfMessage); //в MessageBox'e программа запишет раскодированное сообщение с изображения, которое предоврительно будет переведено из pictureBox'a в класс Bitmap, чтобы мы смогли с этим изображением работать. 
        }
    }
}
