<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCOMB001
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCOMB001))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtdominio = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbmarca = New System.Windows.Forms.Label()
        Me.lbmodelo = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbchofer = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbcontrato = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtcarga = New System.Windows.Forms.TextBox()
        Me.cbtipo = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbcombustible = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lbuso = New System.Windows.Forms.Label()
        Me.lbcombustiuble = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cbUso = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(588, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(204, 18)
        Me.Label5.TabIndex = 188
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(110, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 187
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(364, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 29)
        Me.Label1.TabIndex = 185
        Me.Label1.Text = "Generar Vale"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 189
        Me.Label2.Text = "Dominio"
        '
        'txtdominio
        '
        Me.txtdominio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtdominio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdominio.Location = New System.Drawing.Point(113, 148)
        Me.txtdominio.Name = "txtdominio"
        Me.txtdominio.Size = New System.Drawing.Size(100, 26)
        Me.txtdominio.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(243, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 20)
        Me.Label4.TabIndex = 191
        Me.Label4.Text = "Marca"
        '
        'lbmarca
        '
        Me.lbmarca.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbmarca.Location = New System.Drawing.Point(302, 151)
        Me.lbmarca.Name = "lbmarca"
        Me.lbmarca.Size = New System.Drawing.Size(234, 20)
        Me.lbmarca.TabIndex = 192
        '
        'lbmodelo
        '
        Me.lbmodelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbmodelo.Location = New System.Drawing.Point(609, 151)
        Me.lbmodelo.Name = "lbmodelo"
        Me.lbmodelo.Size = New System.Drawing.Size(234, 20)
        Me.lbmodelo.TabIndex = 194
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(542, 151)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 20)
        Me.Label8.TabIndex = 193
        Me.Label8.Text = "Modelo"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(37, 204)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 20)
        Me.Label9.TabIndex = 195
        Me.Label9.Text = "Chofer"
        '
        'cbchofer
        '
        Me.cbchofer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbchofer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbchofer.Enabled = False
        Me.cbchofer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbchofer.FormattingEnabled = True
        Me.cbchofer.Location = New System.Drawing.Point(113, 202)
        Me.cbchofer.Name = "cbchofer"
        Me.cbchofer.Size = New System.Drawing.Size(306, 28)
        Me.cbchofer.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(435, 205)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 20)
        Me.Label10.TabIndex = 197
        Me.Label10.Text = "Contrato"
        '
        'cbcontrato
        '
        Me.cbcontrato.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbcontrato.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbcontrato.Enabled = False
        Me.cbcontrato.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcontrato.FormattingEnabled = True
        Me.cbcontrato.Location = New System.Drawing.Point(512, 201)
        Me.cbcontrato.Name = "cbcontrato"
        Me.cbcontrato.Size = New System.Drawing.Size(306, 28)
        Me.cbcontrato.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(646, 255)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 20)
        Me.Label11.TabIndex = 199
        Me.Label11.Text = "Carga"
        '
        'txtcarga
        '
        Me.txtcarga.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcarga.Enabled = False
        Me.txtcarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcarga.Location = New System.Drawing.Point(718, 252)
        Me.txtcarga.Name = "txtcarga"
        Me.txtcarga.Size = New System.Drawing.Size(100, 26)
        Me.txtcarga.TabIndex = 5
        Me.txtcarga.Text = "0"
        Me.txtcarga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbtipo
        '
        Me.cbtipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbtipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbtipo.Enabled = False
        Me.cbtipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbtipo.FormattingEnabled = True
        Me.cbtipo.Location = New System.Drawing.Point(369, 360)
        Me.cbtipo.Name = "cbtipo"
        Me.cbtipo.Size = New System.Drawing.Size(205, 28)
        Me.cbtipo.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(243, 368)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 20)
        Me.Label12.TabIndex = 201
        Me.Label12.Text = "Tipo Carga"
        '
        'cbcombustible
        '
        Me.cbcombustible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbcombustible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbcombustible.Enabled = False
        Me.cbcombustible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcombustible.FormattingEnabled = True
        Me.cbcombustible.Location = New System.Drawing.Point(140, 247)
        Me.cbcombustible.Name = "cbcombustible"
        Me.cbcombustible.Size = New System.Drawing.Size(210, 28)
        Me.cbcombustible.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(37, 255)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 20)
        Me.Label13.TabIndex = 203
        Me.Label13.Text = "Combustible"
        '
        'lbuso
        '
        Me.lbuso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbuso.Location = New System.Drawing.Point(423, 298)
        Me.lbuso.Name = "lbuso"
        Me.lbuso.Size = New System.Drawing.Size(395, 28)
        Me.lbuso.TabIndex = 205
        '
        'lbcombustiuble
        '
        Me.lbcombustiuble.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbcombustiuble.Location = New System.Drawing.Point(360, 252)
        Me.lbcombustiuble.Name = "lbcombustiuble"
        Me.lbcombustiuble.Size = New System.Drawing.Size(257, 28)
        Me.lbcombustiuble.TabIndex = 206
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(109, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(752, 18)
        Me.Label16.TabIndex = 207
        Me.Label16.Text = "_________________________________________________________________________________" &
    "____________"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(718, 352)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 42)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Cargar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(41, 357)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 42)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Borrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cbUso
        '
        Me.cbUso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbUso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbUso.Enabled = False
        Me.cbUso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUso.FormattingEnabled = True
        Me.cbUso.Location = New System.Drawing.Point(140, 298)
        Me.cbUso.Name = "cbUso"
        Me.cbUso.Size = New System.Drawing.Size(283, 28)
        Me.cbUso.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(37, 301)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 20)
        Me.Label6.TabIndex = 211
        Me.Label6.Text = "Uso"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(92, 89)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 186
        Me.PictureBox1.TabStop = False
        '
        'PCOMB001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 425)
        Me.Controls.Add(Me.cbUso)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lbcombustiuble)
        Me.Controls.Add(Me.lbuso)
        Me.Controls.Add(Me.cbcombustible)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cbtipo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtcarga)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cbcontrato)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cbchofer)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbmodelo)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbmarca)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtdominio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PCOMB001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PCOMB001- GENERAR VALE COMBUSTIBLE"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtdominio As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbmarca As System.Windows.Forms.Label
    Friend WithEvents lbmodelo As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbchofer As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbcontrato As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtcarga As System.Windows.Forms.TextBox
    Friend WithEvents cbtipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbcombustible As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbuso As System.Windows.Forms.Label
    Friend WithEvents lbcombustiuble As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cbUso As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
