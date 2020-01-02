<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PPROV001
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PPROV001))
        Me.btverificar = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbdireccion = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbcontacto = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbtelefono = New System.Windows.Forms.TextBox()
        Me.tbrazon = New System.Windows.Forms.TextBox()
        Me.cbPartido = New System.Windows.Forms.ComboBox()
        Me.cbProvincia = New System.Windows.Forms.ComboBox()
        Me.tbcuit = New System.Windows.Forms.TextBox()
        Me.btbaja = New System.Windows.Forms.Button()
        Me.btmodificar = New System.Windows.Forms.Button()
        Me.btalta = New System.Windows.Forms.Button()
        Me.cbLocalidad = New System.Windows.Forms.ComboBox()
        Me.tbbaja = New System.Windows.Forms.TextBox()
        Me.tbalta = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btverificar
        '
        Me.btverificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btverificar.Location = New System.Drawing.Point(434, 114)
        Me.btverificar.Name = "btverificar"
        Me.btverificar.Size = New System.Drawing.Size(123, 31)
        Me.btverificar.TabIndex = 2
        Me.btverificar.Text = "Verificar"
        Me.btverificar.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(66, 250)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 20)
        Me.Label9.TabIndex = 222
        Me.Label9.Text = "Provincia"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(79, 295)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 20)
        Me.Label8.TabIndex = 220
        Me.Label8.Text = "Partido"
        '
        'tbdireccion
        '
        Me.tbdireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbdireccion.Enabled = False
        Me.tbdireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbdireccion.Location = New System.Drawing.Point(144, 209)
        Me.tbdireccion.MaxLength = 30
        Me.tbdireccion.Name = "tbdireccion"
        Me.tbdireccion.Size = New System.Drawing.Size(413, 26)
        Me.tbdireccion.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(63, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 20)
        Me.Label6.TabIndex = 216
        Me.Label6.Text = "Dirección"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 172)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 20)
        Me.Label2.TabIndex = 214
        Me.Label2.Text = "Razón Social"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(107, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(688, 18)
        Me.Label4.TabIndex = 213
        Me.Label4.Text = "_________________________________________________________________________________" &
    "____"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(563, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(227, 18)
        Me.Label5.TabIndex = 212
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(107, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 211
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(9, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(92, 89)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 210
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(234, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(350, 29)
        Me.Label1.TabIndex = 209
        Me.Label1.Text = "Administración de Proveedores"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(414, 296)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 20)
        Me.Label7.TabIndex = 229
        Me.Label7.Text = "Localidad"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(92, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 20)
        Me.Label10.TabIndex = 230
        Me.Label10.Text = "CUIT"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(68, 341)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 20)
        Me.Label11.TabIndex = 232
        Me.Label11.Text = "Contacto"
        '
        'tbcontacto
        '
        Me.tbcontacto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbcontacto.Enabled = False
        Me.tbcontacto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcontacto.Location = New System.Drawing.Point(144, 337)
        Me.tbcontacto.MaxLength = 30
        Me.tbcontacto.Name = "tbcontacto"
        Me.tbcontacto.Size = New System.Drawing.Size(249, 26)
        Me.tbcontacto.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(420, 340)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 20)
        Me.Label12.TabIndex = 234
        Me.Label12.Text = "Teléfono"
        '
        'tbtelefono
        '
        Me.tbtelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbtelefono.Enabled = False
        Me.tbtelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbtelefono.Location = New System.Drawing.Point(503, 335)
        Me.tbtelefono.MaxLength = 25
        Me.tbtelefono.Name = "tbtelefono"
        Me.tbtelefono.Size = New System.Drawing.Size(249, 26)
        Me.tbtelefono.TabIndex = 10
        '
        'tbrazon
        '
        Me.tbrazon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbrazon.Enabled = False
        Me.tbrazon.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbrazon.Location = New System.Drawing.Point(144, 169)
        Me.tbrazon.MaxLength = 30
        Me.tbrazon.Name = "tbrazon"
        Me.tbrazon.Size = New System.Drawing.Size(413, 26)
        Me.tbrazon.TabIndex = 4
        '
        'cbPartido
        '
        Me.cbPartido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPartido.Enabled = False
        Me.cbPartido.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPartido.FormattingEnabled = True
        Me.cbPartido.Location = New System.Drawing.Point(144, 293)
        Me.cbPartido.Name = "cbPartido"
        Me.cbPartido.Size = New System.Drawing.Size(249, 28)
        Me.cbPartido.Sorted = True
        Me.cbPartido.TabIndex = 7
        '
        'cbProvincia
        '
        Me.cbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProvincia.Enabled = False
        Me.cbProvincia.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProvincia.FormattingEnabled = True
        Me.cbProvincia.Location = New System.Drawing.Point(144, 248)
        Me.cbProvincia.Name = "cbProvincia"
        Me.cbProvincia.Size = New System.Drawing.Size(249, 28)
        Me.cbProvincia.Sorted = True
        Me.cbProvincia.TabIndex = 6
        '
        'tbcuit
        '
        Me.tbcuit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbcuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcuit.Location = New System.Drawing.Point(144, 116)
        Me.tbcuit.MaxLength = 11
        Me.tbcuit.Name = "tbcuit"
        Me.tbcuit.Size = New System.Drawing.Size(271, 26)
        Me.tbcuit.TabIndex = 1
        '
        'btbaja
        '
        Me.btbaja.Enabled = False
        Me.btbaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btbaja.Location = New System.Drawing.Point(274, 471)
        Me.btbaja.Name = "btbaja"
        Me.btbaja.Size = New System.Drawing.Size(167, 35)
        Me.btbaja.TabIndex = 13
        Me.btbaja.Text = "Baja"
        Me.btbaja.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btbaja.UseVisualStyleBackColor = True
        '
        'btmodificar
        '
        Me.btmodificar.Enabled = False
        Me.btmodificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btmodificar.Location = New System.Drawing.Point(447, 471)
        Me.btmodificar.Name = "btmodificar"
        Me.btmodificar.Size = New System.Drawing.Size(167, 35)
        Me.btmodificar.TabIndex = 12
        Me.btmodificar.Text = "Modificar"
        Me.btmodificar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btmodificar.UseVisualStyleBackColor = True
        '
        'btalta
        '
        Me.btalta.Enabled = False
        Me.btalta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btalta.Location = New System.Drawing.Point(620, 471)
        Me.btalta.Name = "btalta"
        Me.btalta.Size = New System.Drawing.Size(167, 35)
        Me.btalta.TabIndex = 11
        Me.btalta.Text = "Dar de Alta"
        Me.btalta.UseVisualStyleBackColor = True
        '
        'cbLocalidad
        '
        Me.cbLocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLocalidad.Enabled = False
        Me.cbLocalidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLocalidad.FormattingEnabled = True
        Me.cbLocalidad.Location = New System.Drawing.Point(503, 293)
        Me.cbLocalidad.Name = "cbLocalidad"
        Me.cbLocalidad.Size = New System.Drawing.Size(249, 28)
        Me.cbLocalidad.Sorted = True
        Me.cbLocalidad.TabIndex = 8
        '
        'tbbaja
        '
        Me.tbbaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbbaja.Location = New System.Drawing.Point(144, 417)
        Me.tbbaja.Name = "tbbaja"
        Me.tbbaja.ReadOnly = True
        Me.tbbaja.Size = New System.Drawing.Size(170, 26)
        Me.tbbaja.TabIndex = 12
        Me.tbbaja.TabStop = False
        '
        'tbalta
        '
        Me.tbalta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbalta.Location = New System.Drawing.Point(144, 379)
        Me.tbalta.Name = "tbalta"
        Me.tbalta.ReadOnly = True
        Me.tbalta.Size = New System.Drawing.Size(170, 26)
        Me.tbalta.TabIndex = 11
        Me.tbalta.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(50, 423)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 20)
        Me.Label14.TabIndex = 251
        Me.Label14.Text = "Fecha baja"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(54, 382)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 20)
        Me.Label13.TabIndex = 250
        Me.Label13.Text = "Fecha alta"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(142, 144)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(135, 13)
        Me.Label15.TabIndex = 254
        Me.Label15.Text = "(Sólo Números sin guiones)"
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(563, 114)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(123, 31)
        Me.Button5.TabIndex = 3
        Me.Button5.Text = "Borrar"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.Enabled = False
        Me.ComboBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"SI", "NO"})
        Me.ComboBox4.Location = New System.Drawing.Point(503, 374)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(143, 28)
        Me.ComboBox4.TabIndex = 255
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(426, 379)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 20)
        Me.Label16.TabIndex = 256
        Me.Label16.Text = "Petición"
        '
        'PPROV001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 522)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.tbbaja)
        Me.Controls.Add(Me.tbalta)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cbLocalidad)
        Me.Controls.Add(Me.btbaja)
        Me.Controls.Add(Me.btmodificar)
        Me.Controls.Add(Me.btalta)
        Me.Controls.Add(Me.tbcuit)
        Me.Controls.Add(Me.tbrazon)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tbtelefono)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.tbcontacto)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbProvincia)
        Me.Controls.Add(Me.cbPartido)
        Me.Controls.Add(Me.btverificar)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbdireccion)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PPROV001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ABM PROVEEDORES - PPROV001"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btverificar As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbdireccion As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbcontacto As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbtelefono As System.Windows.Forms.TextBox
    Friend WithEvents tbrazon As System.Windows.Forms.TextBox
    Friend WithEvents cbPartido As System.Windows.Forms.ComboBox
    Friend WithEvents cbProvincia As System.Windows.Forms.ComboBox
    Friend WithEvents tbcuit As System.Windows.Forms.TextBox
    Friend WithEvents btbaja As System.Windows.Forms.Button
    Friend WithEvents btmodificar As System.Windows.Forms.Button
    Friend WithEvents btalta As System.Windows.Forms.Button
    Friend WithEvents cbLocalidad As System.Windows.Forms.ComboBox
    Friend WithEvents tbbaja As System.Windows.Forms.TextBox
    Friend WithEvents tbalta As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
