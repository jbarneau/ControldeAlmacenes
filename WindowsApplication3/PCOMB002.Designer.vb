<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCOMB002
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCOMB002))
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lbmodelo = New System.Windows.Forms.Label()
        Me.txtdominio = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtaño = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtnticket = New System.Windows.Forms.TextBox()
        Me.btnbuscar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblcomb = New System.Windows.Forms.Label()
        Me.lbllitros = New System.Windows.Forms.Label()
        Me.lblcombust = New System.Windows.Forms.Label()
        Me.lblfecha = New System.Windows.Forms.Label()
        Me.lbllit = New System.Windows.Forms.Label()
        Me.lblticket = New System.Windows.Forms.Label()
        Me.txtnro = New System.Windows.Forms.TextBox()
        Me.txtlitros = New System.Windows.Forms.TextBox()
        Me.btnactualizar = New System.Windows.Forms.Button()
        Me.calendario = New System.Windows.Forms.DateTimePicker()
        Me.ErrorTicket = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorLitros = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorCombustible = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cbcombustible = New System.Windows.Forms.ComboBox()
        Me.lbCombustible = New System.Windows.Forms.Label()
        Me.lbLitros = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GROUP1 = New System.Windows.Forms.GroupBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorLitros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorCombustible, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GROUP1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(109, 41)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(640, 18)
        Me.Label16.TabIndex = 231
        Me.Label16.Text = "_______________________________________________________________________________"
        '
        'lbmodelo
        '
        Me.lbmodelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbmodelo.Location = New System.Drawing.Point(609, 142)
        Me.lbmodelo.Name = "lbmodelo"
        Me.lbmodelo.Size = New System.Drawing.Size(234, 20)
        Me.lbmodelo.TabIndex = 226
        '
        'txtdominio
        '
        Me.txtdominio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtdominio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdominio.Location = New System.Drawing.Point(113, 139)
        Me.txtdominio.Name = "txtdominio"
        Me.txtdominio.Size = New System.Drawing.Size(100, 26)
        Me.txtdominio.TabIndex = 212
        Me.txtdominio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 222
        Me.Label2.Text = "Dominio"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(494, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(204, 18)
        Me.Label5.TabIndex = 221
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(110, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 220
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(265, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(281, 32)
        Me.Label1.TabIndex = 218
        Me.Label1.Text = "Actualizar Ticket" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtaño
        '
        Me.txtaño.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtaño.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaño.Location = New System.Drawing.Point(359, 139)
        Me.txtaño.Name = "txtaño"
        Me.txtaño.Size = New System.Drawing.Size(67, 26)
        Me.txtaño.TabIndex = 232
        Me.txtaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(310, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 20)
        Me.Label4.TabIndex = 233
        Me.Label4.Text = "Tiket"
        '
        'txtnticket
        '
        Me.txtnticket.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtnticket.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnticket.Location = New System.Drawing.Point(432, 139)
        Me.txtnticket.Name = "txtnticket"
        Me.txtnticket.Size = New System.Drawing.Size(141, 26)
        Me.txtnticket.TabIndex = 234
        Me.txtnticket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnbuscar
        '
        Me.btnbuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbuscar.Location = New System.Drawing.Point(223, 134)
        Me.btnbuscar.Name = "btnbuscar"
        Me.btnbuscar.Size = New System.Drawing.Size(77, 37)
        Me.btnbuscar.TabIndex = 235
        Me.btnbuscar.Text = "Buscar"
        Me.btnbuscar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(92, 89)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 219
        Me.PictureBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(422, 200)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 20)
        Me.Label6.TabIndex = 239
        Me.Label6.Text = "Litros"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(37, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 20)
        Me.Label7.TabIndex = 237
        Me.Label7.Text = "Combustible"
        '
        'lblcomb
        '
        Me.lblcomb.AutoSize = True
        Me.lblcomb.Location = New System.Drawing.Point(178, 228)
        Me.lblcomb.Name = "lblcomb"
        Me.lblcomb.Size = New System.Drawing.Size(0, 13)
        Me.lblcomb.TabIndex = 240
        '
        'lbllitros
        '
        Me.lbllitros.AutoSize = True
        Me.lbllitros.Location = New System.Drawing.Point(416, 228)
        Me.lbllitros.Name = "lbllitros"
        Me.lbllitros.Size = New System.Drawing.Size(0, 13)
        Me.lbllitros.TabIndex = 241
        '
        'lblcombust
        '
        Me.lblcombust.AutoSize = True
        Me.lblcombust.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcombust.Location = New System.Drawing.Point(6, 81)
        Me.lblcombust.Name = "lblcombust"
        Me.lblcombust.Size = New System.Drawing.Size(97, 20)
        Me.lblcombust.TabIndex = 243
        Me.lblcombust.Text = "Combustible"
        '
        'lblfecha
        '
        Me.lblfecha.AutoSize = True
        Me.lblfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha.Location = New System.Drawing.Point(39, 40)
        Me.lblfecha.Name = "lblfecha"
        Me.lblfecha.Size = New System.Drawing.Size(54, 20)
        Me.lblfecha.TabIndex = 242
        Me.lblfecha.Text = "Fecha"
        '
        'lbllit
        '
        Me.lbllit.AutoSize = True
        Me.lbllit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllit.Location = New System.Drawing.Point(410, 77)
        Me.lbllit.Name = "lbllit"
        Me.lbllit.Size = New System.Drawing.Size(48, 20)
        Me.lbllit.TabIndex = 245
        Me.lbllit.Text = "Litros"
        '
        'lblticket
        '
        Me.lblticket.AutoSize = True
        Me.lblticket.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblticket.Location = New System.Drawing.Point(238, 37)
        Me.lblticket.Name = "lblticket"
        Me.lblticket.Size = New System.Drawing.Size(43, 20)
        Me.lblticket.TabIndex = 244
        Me.lblticket.Text = "Tiket"
        '
        'txtnro
        '
        Me.txtnro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtnro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnro.Location = New System.Drawing.Point(287, 34)
        Me.txtnro.Name = "txtnro"
        Me.txtnro.Size = New System.Drawing.Size(206, 26)
        Me.txtnro.TabIndex = 246
        '
        'txtlitros
        '
        Me.txtlitros.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtlitros.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlitros.Location = New System.Drawing.Point(464, 74)
        Me.txtlitros.Name = "txtlitros"
        Me.txtlitros.Size = New System.Drawing.Size(79, 26)
        Me.txtlitros.TabIndex = 247
        '
        'btnactualizar
        '
        Me.btnactualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnactualizar.Location = New System.Drawing.Point(572, 40)
        Me.btnactualizar.Name = "btnactualizar"
        Me.btnactualizar.Size = New System.Drawing.Size(99, 40)
        Me.btnactualizar.TabIndex = 250
        Me.btnactualizar.Text = "Actualizar"
        Me.btnactualizar.UseVisualStyleBackColor = True
        '
        'calendario
        '
        Me.calendario.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.calendario.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.calendario.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.calendario.Location = New System.Drawing.Point(104, 36)
        Me.calendario.Name = "calendario"
        Me.calendario.Size = New System.Drawing.Size(128, 26)
        Me.calendario.TabIndex = 251
        '
        'ErrorTicket
        '
        Me.ErrorTicket.ContainerControl = Me
        '
        'ErrorLitros
        '
        Me.ErrorLitros.ContainerControl = Me
        '
        'ErrorCombustible
        '
        Me.ErrorCombustible.ContainerControl = Me
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(579, 134)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 37)
        Me.Button1.TabIndex = 252
        Me.Button1.Text = "Buscar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cbcombustible
        '
        Me.cbcombustible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbcombustible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbcombustible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcombustible.FormattingEnabled = True
        Me.cbcombustible.Location = New System.Drawing.Point(109, 75)
        Me.cbcombustible.Name = "cbcombustible"
        Me.cbcombustible.Size = New System.Drawing.Size(295, 28)
        Me.cbcombustible.TabIndex = 253
        '
        'lbCombustible
        '
        Me.lbCombustible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCombustible.Location = New System.Drawing.Point(140, 200)
        Me.lbCombustible.Name = "lbCombustible"
        Me.lbCombustible.Size = New System.Drawing.Size(276, 28)
        Me.lbCombustible.TabIndex = 254
        '
        'lbLitros
        '
        Me.lbLitros.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitros.Location = New System.Drawing.Point(481, 200)
        Me.lbLitros.Name = "lbLitros"
        Me.lbLitros.Size = New System.Drawing.Size(188, 20)
        Me.lbLitros.TabIndex = 255
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(662, 134)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(77, 37)
        Me.Button2.TabIndex = 256
        Me.Button2.Text = "Borrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GROUP1
        '
        Me.GROUP1.Controls.Add(Me.lblfecha)
        Me.GROUP1.Controls.Add(Me.lblcombust)
        Me.GROUP1.Controls.Add(Me.lblticket)
        Me.GROUP1.Controls.Add(Me.lbllit)
        Me.GROUP1.Controls.Add(Me.cbcombustible)
        Me.GROUP1.Controls.Add(Me.btnactualizar)
        Me.GROUP1.Controls.Add(Me.txtnro)
        Me.GROUP1.Controls.Add(Me.txtlitros)
        Me.GROUP1.Controls.Add(Me.calendario)
        Me.GROUP1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GROUP1.Location = New System.Drawing.Point(41, 244)
        Me.GROUP1.Name = "GROUP1"
        Me.GROUP1.Size = New System.Drawing.Size(698, 116)
        Me.GROUP1.TabIndex = 257
        Me.GROUP1.TabStop = False
        Me.GROUP1.Text = "CARGA REALIZADA"
        Me.GROUP1.Visible = False
        '
        'PCOMB002
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 391)
        Me.Controls.Add(Me.GROUP1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lbLitros)
        Me.Controls.Add(Me.lbCombustible)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lbllitros)
        Me.Controls.Add(Me.lblcomb)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnbuscar)
        Me.Controls.Add(Me.txtnticket)
        Me.Controls.Add(Me.txtaño)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lbmodelo)
        Me.Controls.Add(Me.txtdominio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PCOMB002"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PCOMB002 - CARGA DE TICKET"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorTicket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorLitros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorCombustible, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GROUP1.ResumeLayout(False)
        Me.GROUP1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lbmodelo As System.Windows.Forms.Label
    Friend WithEvents txtdominio As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtaño As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtnticket As System.Windows.Forms.TextBox
    Friend WithEvents btnbuscar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblcomb As System.Windows.Forms.Label
    Friend WithEvents lbllitros As System.Windows.Forms.Label
    Friend WithEvents lblcombust As System.Windows.Forms.Label
    Friend WithEvents lblfecha As System.Windows.Forms.Label
    Friend WithEvents lbllit As System.Windows.Forms.Label
    Friend WithEvents lblticket As System.Windows.Forms.Label
    Friend WithEvents txtnro As System.Windows.Forms.TextBox
    Friend WithEvents txtlitros As System.Windows.Forms.TextBox
    Friend WithEvents btnactualizar As System.Windows.Forms.Button
    Friend WithEvents calendario As System.Windows.Forms.DateTimePicker
    Friend WithEvents ErrorTicket As System.Windows.Forms.ErrorProvider
    Friend WithEvents ErrorLitros As System.Windows.Forms.ErrorProvider
    Friend WithEvents ErrorCombustible As System.Windows.Forms.ErrorProvider
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbcombustible As System.Windows.Forms.ComboBox
    Friend WithEvents lbLitros As System.Windows.Forms.Label
    Friend WithEvents lbCombustible As System.Windows.Forms.Label
    Friend WithEvents GROUP1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
