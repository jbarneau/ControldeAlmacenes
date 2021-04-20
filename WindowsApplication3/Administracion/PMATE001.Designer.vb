<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PMATE001
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PMATE001))
        Me.btVerificar = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAlt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbTipo = New System.Windows.Forms.ComboBox()
        Me.txtAlta = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBaja = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtSap = New System.Windows.Forms.TextBox()
        Me.rbSeri = New System.Windows.Forms.RadioButton()
        Me.rbConsumible = New System.Windows.Forms.RadioButton()
        Me.btBaja = New System.Windows.Forms.Button()
        Me.btModi = New System.Windows.Forms.Button()
        Me.btAlta = New System.Windows.Forms.Button()
        Me.cbDecimal = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.cbUnidad = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btVerificar
        '
        Me.btVerificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btVerificar.Location = New System.Drawing.Point(429, 129)
        Me.btVerificar.Name = "btVerificar"
        Me.btVerificar.Size = New System.Drawing.Size(130, 28)
        Me.btVerificar.TabIndex = 1
        Me.btVerificar.Text = "Verificar"
        Me.btVerificar.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(72, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 20)
        Me.Label8.TabIndex = 246
        Me.Label8.Text = "Tipo"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(131, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 20)
        Me.Label6.TabIndex = 244
        Me.Label6.Text = "Código SAP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 213)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 243
        Me.Label2.Text = "Descripción"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(102, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(688, 18)
        Me.Label4.TabIndex = 242
        Me.Label4.Text = "_________________________________________________________________________________" &
    "____"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(549, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(238, 18)
        Me.Label5.TabIndex = 241
        Me.Label5.Text = "Fecha:01/01/2013"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(104, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 18)
        Me.Label3.TabIndex = 240
        Me.Label3.Text = "USUARIO: Ricardo Simone"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 8)
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
        Me.Label1.Location = New System.Drawing.Point(228, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(323, 29)
        Me.Label1.TabIndex = 238
        Me.Label1.Text = "Administración de Materiales"
        '
        'txtAlt
        '
        Me.txtAlt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlt.Enabled = False
        Me.txtAlt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlt.Location = New System.Drawing.Point(232, 172)
        Me.txtAlt.Name = "txtAlt"
        Me.txtAlt.Size = New System.Drawing.Size(181, 26)
        Me.txtAlt.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(90, 172)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 20)
        Me.Label13.TabIndex = 263
        Me.Label13.Text = "Código alternativo"
        '
        'cbTipo
        '
        Me.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTipo.Enabled = False
        Me.cbTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTipo.FormattingEnabled = True
        Me.cbTipo.Location = New System.Drawing.Point(117, 251)
        Me.cbTipo.Name = "cbTipo"
        Me.cbTipo.Size = New System.Drawing.Size(227, 28)
        Me.cbTipo.TabIndex = 4
        '
        'txtAlta
        '
        Me.txtAlta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlta.Location = New System.Drawing.Point(116, 296)
        Me.txtAlta.Name = "txtAlta"
        Me.txtAlta.ReadOnly = True
        Me.txtAlta.Size = New System.Drawing.Size(121, 26)
        Me.txtAlta.TabIndex = 269
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(26, 299)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 20)
        Me.Label9.TabIndex = 268
        Me.Label9.Text = "Fecha alta"
        '
        'txtBaja
        '
        Me.txtBaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaja.Location = New System.Drawing.Point(349, 296)
        Me.txtBaja.Name = "txtBaja"
        Me.txtBaja.ReadOnly = True
        Me.txtBaja.Size = New System.Drawing.Size(121, 26)
        Me.txtBaja.TabIndex = 272
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(255, 299)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 20)
        Me.Label10.TabIndex = 271
        Me.Label10.Text = "Fecha baja"
        '
        'txtDesc
        '
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.Enabled = False
        Me.txtDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(117, 210)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(639, 26)
        Me.txtDesc.TabIndex = 3
        '
        'txtSap
        '
        Me.txtSap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSap.Location = New System.Drawing.Point(233, 131)
        Me.txtSap.MaxLength = 6
        Me.txtSap.Name = "txtSap"
        Me.txtSap.Size = New System.Drawing.Size(181, 26)
        Me.txtSap.TabIndex = 0
        '
        'rbSeri
        '
        Me.rbSeri.AutoSize = True
        Me.rbSeri.Enabled = False
        Me.rbSeri.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSeri.Location = New System.Drawing.Point(514, 296)
        Me.rbSeri.Name = "rbSeri"
        Me.rbSeri.Size = New System.Drawing.Size(105, 24)
        Me.rbSeri.TabIndex = 5
        Me.rbSeri.TabStop = True
        Me.rbSeri.Text = "Serializado"
        Me.rbSeri.UseVisualStyleBackColor = True
        '
        'rbConsumible
        '
        Me.rbConsumible.AutoSize = True
        Me.rbConsumible.Enabled = False
        Me.rbConsumible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbConsumible.Location = New System.Drawing.Point(646, 296)
        Me.rbConsumible.Name = "rbConsumible"
        Me.rbConsumible.Size = New System.Drawing.Size(110, 24)
        Me.rbConsumible.TabIndex = 6
        Me.rbConsumible.TabStop = True
        Me.rbConsumible.Text = "Consumible"
        Me.rbConsumible.UseVisualStyleBackColor = True
        '
        'btBaja
        '
        Me.btBaja.Enabled = False
        Me.btBaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btBaja.Location = New System.Drawing.Point(269, 351)
        Me.btBaja.Name = "btBaja"
        Me.btBaja.Size = New System.Drawing.Size(167, 37)
        Me.btBaja.TabIndex = 10
        Me.btBaja.Text = "Baja"
        Me.btBaja.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btBaja.UseVisualStyleBackColor = True
        '
        'btModi
        '
        Me.btModi.Enabled = False
        Me.btModi.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btModi.Location = New System.Drawing.Point(442, 351)
        Me.btModi.Name = "btModi"
        Me.btModi.Size = New System.Drawing.Size(167, 37)
        Me.btModi.TabIndex = 9
        Me.btModi.Text = "Modificar"
        Me.btModi.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btModi.UseVisualStyleBackColor = True
        '
        'btAlta
        '
        Me.btAlta.Enabled = False
        Me.btAlta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btAlta.Location = New System.Drawing.Point(615, 351)
        Me.btAlta.Name = "btAlta"
        Me.btAlta.Size = New System.Drawing.Size(167, 37)
        Me.btAlta.TabIndex = 8
        Me.btAlta.Text = "Dar de Alta"
        Me.btAlta.UseVisualStyleBackColor = True
        '
        'cbDecimal
        '
        Me.cbDecimal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDecimal.Enabled = False
        Me.cbDecimal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDecimal.FormattingEnabled = True
        Me.cbDecimal.Location = New System.Drawing.Point(676, 251)
        Me.cbDecimal.Name = "cbDecimal"
        Me.cbDecimal.Size = New System.Drawing.Size(80, 28)
        Me.cbDecimal.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(587, 254)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 20)
        Me.Label7.TabIndex = 288
        Me.Label7.Text = "Decimales"
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(565, 129)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(130, 28)
        Me.Button5.TabIndex = 289
        Me.Button5.Text = "Borrar"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'cbUnidad
        '
        Me.cbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUnidad.Enabled = False
        Me.cbUnidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUnidad.FormattingEnabled = True
        Me.cbUnidad.Location = New System.Drawing.Point(462, 251)
        Me.cbUnidad.Name = "cbUnidad"
        Me.cbUnidad.Size = New System.Drawing.Size(110, 28)
        Me.cbUnidad.TabIndex = 290
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(361, 254)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 20)
        Me.Label11.TabIndex = 291
        Me.Label11.Text = "Unidad Med"
        '
        'PMATE001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 411)
        Me.Controls.Add(Me.cbUnidad)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.cbDecimal)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btBaja)
        Me.Controls.Add(Me.btModi)
        Me.Controls.Add(Me.btAlta)
        Me.Controls.Add(Me.rbConsumible)
        Me.Controls.Add(Me.rbSeri)
        Me.Controls.Add(Me.txtSap)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtBaja)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtAlta)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cbTipo)
        Me.Controls.Add(Me.txtAlt)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btVerificar)
        Me.Controls.Add(Me.Label8)
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
        Me.Name = "PMATE001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MATERIALES - PMATE001"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btVerificar As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAlt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents txtAlta As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBaja As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtSap As System.Windows.Forms.TextBox
    Friend WithEvents rbSeri As System.Windows.Forms.RadioButton
    Friend WithEvents rbConsumible As System.Windows.Forms.RadioButton
    Friend WithEvents btBaja As System.Windows.Forms.Button
    Friend WithEvents btModi As System.Windows.Forms.Button
    Friend WithEvents btAlta As System.Windows.Forms.Button
    Friend WithEvents cbDecimal As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents cbUnidad As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
