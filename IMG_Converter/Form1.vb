Imports System.IO


Public Class Form1
    'Dim open As New OpenFileDialog
    'Dim save As SaveFileDialog

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim url As String = "https://www.facebook.com/groups/devearab"
        Process.Start(url)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

        WebBrowser1.DocumentText = "<MARQUEE DIRECTION=LEFT> PUT your  Ads Here</MARQUEE><br/><MARQUEE DIRECTION=right> ضع اعلانك هنا</MARQUEE>"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.DefaultExt = "*.BMG|*.EPS|*.GIF|*.HDR|*.EXR|*.ICO|*.JPG|*.JPEG|*.PNG|*.SVG|*.TGA|*.TIFF|*.WBMP |*.WebP"
        OpenFileDialog1.Filter = "BMG Files|*.BMG|EPS Files|*.EPS|GIF Files|*.GIF|HDR Files|*.HDR|EXR Files|*.EXR|ICO Files|*.ICO|JPG Files|*.JPG|JPEG Files|*.JPEG|PNG Files|*.PNG|SVG Files|*.SVG|TGA Files|*.TGA|TIFF Files|*.TIFF|WBMP Files|*.WBMP|WebP Files|*.WebP"
        'OpenFileDialog1.CreateObjRef = True
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = System.Drawing.Bitmap.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub
    Public Function Convert(input_image As String, output_icon As String, size As Integer, Optional keep_aspect_ratio As Boolean = False) As Boolean
        PictureBox1.Image.Dispose()
        Dim input_stream As New System.IO.FileStream(input_image, System.IO.FileMode.Open)
        Dim output_stream As New System.IO.FileStream(output_icon, System.IO.FileMode.OpenOrCreate)

        Dim result As Boolean = Convert(input_stream, output_stream, size, keep_aspect_ratio)

        input_stream.Close()
        output_stream.Close()

        Return result
    End Function
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
    Public Shared Function Convert(input_stream As System.IO.Stream, output_stream As System.IO.Stream, size As Integer, Optional keep_aspect_ratio As Boolean = False) As Boolean
        Dim input_bit As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(input_stream), System.Drawing.Bitmap)
        If input_bit IsNot Nothing Then
            Dim width As Integer, height As Integer
            If keep_aspect_ratio Then
                width = size
                height = input_bit.Height / input_bit.Width * size
            Else
                width = InlineAssignHelper(height, size)
            End If
            Dim new_bit As New System.Drawing.Bitmap(input_bit, New System.Drawing.Size(width, height))
            If new_bit IsNot Nothing Then
                Dim mem_data As New System.IO.MemoryStream()
                new_bit.Save(mem_data, System.Drawing.Imaging.ImageFormat.Png)

                Dim icon_writer As New System.IO.BinaryWriter(output_stream)
                If output_stream IsNot Nothing AndAlso icon_writer IsNot Nothing Then

                    icon_writer.Write(CByte(0))
                    icon_writer.Write(CByte(0))


                    icon_writer.Write(CShort(1))


                    icon_writer.Write(CShort(1))

                    icon_writer.Write(CByte(width))

                    icon_writer.Write(CByte(height))

                    icon_writer.Write(CByte(0))


                    icon_writer.Write(CByte(0))


                    icon_writer.Write(CShort(0))

                    icon_writer.Write(CShort(32))


                    icon_writer.Write(CInt(mem_data.Length))


                    icon_writer.Write(CInt(6 + 16))

 
                    icon_writer.Write(mem_data.ToArray())

                    icon_writer.Flush()

                    Return True
                End If
            End If
            Return False
        End If
        Return False
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If PictureBox1.Image Is Nothing Then
            MsgBox("please Upload samme pic ")
        Else

            Dim DESTINO As String = String.Empty

            'SaveFileDialog1.DefaultExt = "*.BMG|*.EPS|*.GIF|*.HDR|*.EXR|*.ICO|*.JPG|*.JPEG|*.PNG|*.SVG|*.TGA|*.TIFF|*.WBMP |*.WebP"
            SaveFileDialog1.Filter = "BMP Files|*.BMP|Gif Files|*.Gif|Icon Files|*.Icon|Jpeg Files|*.Jpeg|Png Files|*.Png|Tiff Files|*.Tiff" '|JPG Files|*.JPG|JPEG Files|*.JPEG|PNG Files|*.PNG|SVG Files|*.SVG|TGA Files|*.TGA|TIFF Files|*.TIFF|WBMP Files|*.WBMP|WebP Files|*.WebP
            SaveFileDialog1.CreatePrompt = True
            SaveFileDialog1.AddExtension = True
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If SaveFileDialog1.FilterIndex = 1 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Bmp)
                ElseIf SaveFileDialog1.FilterIndex = 2 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Gif)
                ElseIf SaveFileDialog1.FilterIndex = 3 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Icon)
                ElseIf SaveFileDialog1.FilterIndex = 4 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
                ElseIf SaveFileDialog1.FilterIndex = 5 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
                ElseIf SaveFileDialog1.FilterIndex = 6 Then
                    PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Tiff)
                End If

            End If
            Dim TAMAÑO As Integer = Nothing

            If RadioButton1.Checked Then
                TAMAÑO = 16
            ElseIf RadioButton2.Checked Then
                TAMAÑO = 32
            ElseIf RadioButton3.Checked Then
                TAMAÑO = 48
            ElseIf RadioButton4.Checked Then
                TAMAÑO = 64
            ElseIf RadioButton5.Checked Then
                TAMAÑO = 100
            ElseIf RadioButton6.Checked Then
                TAMAÑO = 200
            End If
           
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PictureBox1.Image = Nothing
    End Sub
End Class
