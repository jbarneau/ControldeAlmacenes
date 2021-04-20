<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCOMB003
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCOMB003))
        Me.btncargar = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtdominio = New System.Windows.Forms.TextBox()
        Me.lbldom = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblmarca = New System.Windows.Forms.Label()
        Me.txtmod = New System.Windows.Forms.TextBox()
        Me.lblmod = New System.Windows.Forms.Label()
        Me.btneliminar = New System.Windows.Forms.Button()
        Me.txtaño = New System.Windows.Forms.TextBox()
        Me.lblaño = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btncargar
        '
        Me.btncargar.Enabled = False
        Me.btncargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncargar.Location = New System.Drawing.Point(730, 247)
        Me.btncargar.Name = "btncargar"
        Me.btncargar.Size = New System.Drawing.Size(119, 44)
        Me.btncargar.TabIndex = 243
        Me.btncargar.Text = "Cargar"
        Me.btncargar.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(106, 52)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(752, 18)
        Me.Label16.TabIndex = 242
        Me.Label16.Text = "_________________________________________________________________________________" &
    "____________"
        '
        'txtdominio
        '
        Me.txtdominio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtdominio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdominio.Location = New System.Drawing.Point(106, 164)
        Me.txtdominio.Name = "txtdominio"
        Me.txtdominio.Size = New System.Drawing.Size(152, 26)
        Me.txtdominio.TabIndex = 236
        '
        'lbldom
        '
        Me.lbldom.AutoSize = True
        Me.lbldom.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldom.Location = New System.Drawing.Point(33, 167)
        Me.lbldom.Name = "lbldom"
        Me.lbldom.Size = New System.Drawing.Size(67, 20)
        Me.lbldom.TabIndex = 241
        Me.lbldom.Text = "Dominio"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(632, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(204, 18)
        Me.Label5.TabIndex = 240
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(106, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 239
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 23)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(92, 89)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 238
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(296, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(292, 29)
        Me.Label1.TabIndex = 237
        Me.Label1.Text = "Cargar / Eliminar Vehiculo" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblmarca
        '
        Me.lblmarca.AutoSize = True
        Me.lblmarca.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmarca.Location = New System.Drawing.Point(309, 167)
        Me.lblmarca.Name = "lblmarca"
        Me.lblmarca.Size = New System.Drawing.Size(53, 20)
        Me.lblmarca.TabIndex = 245
        Me.lblmarca.Text = "Marca"
        '
        'txtmod
        '
        Me.txtmod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtmod.Enabled = False
        Me.txtmod.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmod.Location = New System.Drawing.Point(172, 208)
        Me.txtmod.Name = "txtmod"
        Me.txtmod.Size = New System.Drawing.Size(455, 26)
        Me.txtmod.TabIndex = 246
        '
        'lblmod
        '
        Me.lblmod.AutoSize = True
        Me.lblmod.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmod.Location = New System.Drawing.Point(105, 211)
        Me.lblmod.Name = "lblmod"
        Me.lblmod.Size = New System.Drawing.Size(61, 20)
        Me.lblmod.TabIndex = 247
        Me.lblmod.Text = "Modelo"
        '
        'btneliminar
        '
        Me.btneliminar.Enabled = False
        Me.btneliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btneliminar.Location = New System.Drawing.Point(19, 247)
        Me.btneliminar.Name = "btneliminar"
        Me.btneliminar.Size = New System.Drawing.Size(119, 44)
        Me.btneliminar.TabIndex = 248
        Me.btneliminar.Text = "Eliminar"
        Me.btneliminar.UseVisualStyleBackColor = True
        '
        'txtaño
        '
        Me.txtaño.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtaño.Enabled = False
        Me.txtaño.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaño.Location = New System.Drawing.Point(688, 208)
        Me.txtaño.Name = "txtaño"
        Me.txtaño.Size = New System.Drawing.Size(100, 26)
        Me.txtaño.TabIndex = 249
        '
        'lblaño
        '
        Me.lblaño.AutoSize = True
        Me.lblaño.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaño.Location = New System.Drawing.Point(644, 211)
        Me.lblaño.Name = "lblaño"
        Me.lblaño.Size = New System.Drawing.Size(38, 20)
        Me.lblaño.TabIndex = 250
        Me.lblaño.Text = "Año"
        '
        'ComboBox1
        '
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(368, 164)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(465, 28)
        Me.ComboBox1.TabIndex = 251
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Location = New System.Drawing.Point(264, 164)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(41, 28)
        Me.Button1.TabIndex = 252
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PCOMB003
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 318)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.lblaño)
        Me.Controls.Add(Me.txtaño)
        Me.Controls.Add(Me.btneliminar)
        Me.Controls.Add(Me.txtmod)
        Me.Controls.Add(Me.lblmod)
        Me.Controls.Add(Me.lblmarca)
        Me.Controls.Add(Me.btncargar)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtdominio)
        Me.Controls.Add(Me.lbldom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PCOMB003"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CARGA DE VEHICULO - PCOMB003"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btncargar As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtdominio As System.Windows.Forms.TextBox
    Friend WithEvents lbldom As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblmarca As System.Windows.Forms.Label
    Friend WithEvents txtmod As System.Windows.Forms.TextBox
    Friend WithEvents lblmod As System.Windows.Forms.Label
    Friend WithEvents btneliminar As System.Windows.Forms.Button
    Friend WithEvents txtaño As System.Windows.Forms.TextBox
    Friend WithEvents lblaño As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
