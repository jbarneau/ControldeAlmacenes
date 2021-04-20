<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PALMA100
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PALMA100))
        Me.cbProvincia = New System.Windows.Forms.ComboBox()
        Me.cbPartido = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.BTnuevo = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TBDIRECCION = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TBCOD = New System.Windows.Forms.TextBox()
        Me.BTBAJA = New System.Windows.Forms.Button()
        Me.BTMODIFICAR = New System.Windows.Forms.Button()
        Me.BTALTA = New System.Windows.Forms.Button()
        Me.TBNOMBRE = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cblocalidad = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TBALTA = New System.Windows.Forms.TextBox()
        Me.TBBAJA = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btBorrar = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbProvincia
        '
        Me.cbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProvincia.Enabled = False
        Me.cbProvincia.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProvincia.FormattingEnabled = True
        Me.cbProvincia.Location = New System.Drawing.Point(148, 238)
        Me.cbProvincia.Name = "cbProvincia"
        Me.cbProvincia.Size = New System.Drawing.Size(249, 28)
        Me.cbProvincia.TabIndex = 4
        '
        'cbPartido
        '
        Me.cbPartido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPartido.Enabled = False
        Me.cbPartido.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPartido.FormattingEnabled = True
        Me.cbPartido.Location = New System.Drawing.Point(497, 237)
        Me.cbPartido.Name = "cbPartido"
        Me.cbPartido.Size = New System.Drawing.Size(249, 28)
        Me.cbPartido.TabIndex = 5
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(148, 118)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(345, 28)
        Me.ComboBox1.TabIndex = 0
        '
        'BTnuevo
        '
        Me.BTnuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTnuevo.Location = New System.Drawing.Point(511, 158)
        Me.BTnuevo.Name = "BTnuevo"
        Me.BTnuevo.Size = New System.Drawing.Size(161, 31)
        Me.BTnuevo.TabIndex = 249
        Me.BTnuevo.Text = "Nuevo "
        Me.BTnuevo.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(70, 242)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 20)
        Me.Label9.TabIndex = 248
        Me.Label9.Text = "Provincia"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(434, 242)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 20)
        Me.Label8.TabIndex = 246
        Me.Label8.Text = "Partido"
        '
        'TBDIRECCION
        '
        Me.TBDIRECCION.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TBDIRECCION.Enabled = False
        Me.TBDIRECCION.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBDIRECCION.Location = New System.Drawing.Point(148, 199)
        Me.TBDIRECCION.Name = "TBDIRECCION"
        Me.TBDIRECCION.Size = New System.Drawing.Size(334, 26)
        Me.TBDIRECCION.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(67, 203)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 20)
        Me.Label6.TabIndex = 244
        Me.Label6.Text = "Dirección"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(68, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 243
        Me.Label2.Text = "Depósito"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(104, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(680, 18)
        Me.Label4.TabIndex = 242
        Me.Label4.Text = "_________________________________________________________________________________" &
    "___"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(662, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(125, 18)
        Me.Label5.TabIndex = 241
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(104, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 240
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(92, 89)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 239
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(231, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(320, 29)
        Me.Label1.TabIndex = 238
        Me.Label1.Text = "Administración de Depósitos"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(45, 165)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 20)
        Me.Label10.TabIndex = 263
        Me.Label10.Text = "Cod. Interno"
        '
        'TBCOD
        '
        Me.TBCOD.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBCOD.Location = New System.Drawing.Point(148, 163)
        Me.TBCOD.Name = "TBCOD"
        Me.TBCOD.ReadOnly = True
        Me.TBCOD.Size = New System.Drawing.Size(51, 26)
        Me.TBCOD.TabIndex = 2
        Me.TBCOD.TabStop = False
        '
        'BTBAJA
        '
        Me.BTBAJA.Enabled = False
        Me.BTBAJA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTBAJA.Location = New System.Drawing.Point(271, 383)
        Me.BTBAJA.Name = "BTBAJA"
        Me.BTBAJA.Size = New System.Drawing.Size(167, 35)
        Me.BTBAJA.TabIndex = 9
        Me.BTBAJA.Text = "Baja"
        Me.BTBAJA.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.BTBAJA.UseVisualStyleBackColor = True
        '
        'BTMODIFICAR
        '
        Me.BTMODIFICAR.Enabled = False
        Me.BTMODIFICAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTMODIFICAR.Location = New System.Drawing.Point(444, 383)
        Me.BTMODIFICAR.Name = "BTMODIFICAR"
        Me.BTMODIFICAR.Size = New System.Drawing.Size(167, 35)
        Me.BTMODIFICAR.TabIndex = 8
        Me.BTMODIFICAR.Text = "Modificar"
        Me.BTMODIFICAR.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.BTMODIFICAR.UseVisualStyleBackColor = True
        '
        'BTALTA
        '
        Me.BTALTA.Enabled = False
        Me.BTALTA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTALTA.Location = New System.Drawing.Point(617, 383)
        Me.BTALTA.Name = "BTALTA"
        Me.BTALTA.Size = New System.Drawing.Size(167, 35)
        Me.BTALTA.TabIndex = 7
        Me.BTALTA.Text = "Dar de Alta"
        Me.BTALTA.UseVisualStyleBackColor = True
        '
        'TBNOMBRE
        '
        Me.TBNOMBRE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TBNOMBRE.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBNOMBRE.Location = New System.Drawing.Point(147, 120)
        Me.TBNOMBRE.Name = "TBNOMBRE"
        Me.TBNOMBRE.Size = New System.Drawing.Size(345, 26)
        Me.TBNOMBRE.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(65, 281)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 20)
        Me.Label11.TabIndex = 270
        Me.Label11.Text = "Localidad"
        '
        'cblocalidad
        '
        Me.cblocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cblocalidad.Enabled = False
        Me.cblocalidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cblocalidad.FormattingEnabled = True
        Me.cblocalidad.Location = New System.Drawing.Point(148, 278)
        Me.cblocalidad.Name = "cblocalidad"
        Me.cblocalidad.Size = New System.Drawing.Size(249, 28)
        Me.cblocalidad.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(64, 326)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 20)
        Me.Label7.TabIndex = 271
        Me.Label7.Text = "Fecha alta"
        '
        'TBALTA
        '
        Me.TBALTA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBALTA.Location = New System.Drawing.Point(149, 321)
        Me.TBALTA.Name = "TBALTA"
        Me.TBALTA.ReadOnly = True
        Me.TBALTA.Size = New System.Drawing.Size(163, 26)
        Me.TBALTA.TabIndex = 272
        Me.TBALTA.TabStop = False
        '
        'TBBAJA
        '
        Me.TBBAJA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBBAJA.Location = New System.Drawing.Point(448, 324)
        Me.TBBAJA.Name = "TBBAJA"
        Me.TBBAJA.ReadOnly = True
        Me.TBBAJA.Size = New System.Drawing.Size(163, 26)
        Me.TBBAJA.TabIndex = 274
        Me.TBBAJA.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(350, 327)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 20)
        Me.Label12.TabIndex = 273
        Me.Label12.Text = "Fecha baja"
        '
        'btBorrar
        '
        Me.btBorrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btBorrar.Location = New System.Drawing.Point(511, 116)
        Me.btBorrar.Name = "btBorrar"
        Me.btBorrar.Size = New System.Drawing.Size(161, 31)
        Me.btBorrar.TabIndex = 275
        Me.btBorrar.Text = "Borrar"
        Me.btBorrar.UseVisualStyleBackColor = True
        '
        'PALMA100
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 437)
        Me.Controls.Add(Me.btBorrar)
        Me.Controls.Add(Me.TBBAJA)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TBALTA)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cblocalidad)
        Me.Controls.Add(Me.TBNOMBRE)
        Me.Controls.Add(Me.BTBAJA)
        Me.Controls.Add(Me.BTMODIFICAR)
        Me.Controls.Add(Me.BTALTA)
        Me.Controls.Add(Me.TBCOD)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cbProvincia)
        Me.Controls.Add(Me.cbPartido)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.BTnuevo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TBDIRECCION)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PALMA100"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ABMC ALMACENES - PALMA100"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbProvincia As System.Windows.Forms.ComboBox
    Friend WithEvents cbPartido As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents BTnuevo As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TBDIRECCION As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TBCOD As System.Windows.Forms.TextBox
    Friend WithEvents BTBAJA As System.Windows.Forms.Button
    Friend WithEvents BTMODIFICAR As System.Windows.Forms.Button
    Friend WithEvents BTALTA As System.Windows.Forms.Button
    Friend WithEvents TBNOMBRE As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cblocalidad As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TBALTA As System.Windows.Forms.TextBox
    Friend WithEvents TBBAJA As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btBorrar As System.Windows.Forms.Button
End Class
