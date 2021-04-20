<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PALMA036BIS3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PALMA036BIS3))
        Me.txtdesc = New System.Windows.Forms.TextBox()
        Me.txtnserie = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnactualizar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbsap = New System.Windows.Forms.ComboBox()
        Me.ASDA = New System.Windows.Forms.GroupBox()
        Me.ASDA.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtdesc
        '
        Me.txtdesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.Location = New System.Drawing.Point(42, 91)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReadOnly = True
        Me.txtdesc.Size = New System.Drawing.Size(367, 26)
        Me.txtdesc.TabIndex = 0
        '
        'txtnserie
        '
        Me.txtnserie.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnserie.Location = New System.Drawing.Point(22, 145)
        Me.txtnserie.Name = "txtnserie"
        Me.txtnserie.ReadOnly = True
        Me.txtnserie.Size = New System.Drawing.Size(120, 35)
        Me.txtnserie.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 29)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "NSERIE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(130, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(178, 29)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "SAP ORIGINAL"
        '
        'btnactualizar
        '
        Me.btnactualizar.Enabled = False
        Me.btnactualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnactualizar.Location = New System.Drawing.Point(205, 320)
        Me.btnactualizar.Name = "btnactualizar"
        Me.btnactualizar.Size = New System.Drawing.Size(146, 52)
        Me.btnactualizar.TabIndex = 4
        Me.btnactualizar.Text = "ACTUALIZAR"
        Me.btnactualizar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(144, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 29)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "SAP NUEVO"
        '
        'cmbsap
        '
        Me.cmbsap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsap.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsap.FormattingEnabled = True
        Me.cmbsap.Location = New System.Drawing.Point(42, 221)
        Me.cmbsap.Name = "cmbsap"
        Me.cmbsap.Size = New System.Drawing.Size(367, 32)
        Me.cmbsap.TabIndex = 7
        '
        'ASDA
        '
        Me.ASDA.Controls.Add(Me.cmbsap)
        Me.ASDA.Controls.Add(Me.Label3)
        Me.ASDA.Controls.Add(Me.Label2)
        Me.ASDA.Controls.Add(Me.txtdesc)
        Me.ASDA.Location = New System.Drawing.Point(163, 9)
        Me.ASDA.Name = "ASDA"
        Me.ASDA.Size = New System.Drawing.Size(417, 280)
        Me.ASDA.TabIndex = 8
        Me.ASDA.TabStop = False
        '
        'PALMA036BIS3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 384)
        Me.Controls.Add(Me.ASDA)
        Me.Controls.Add(Me.btnactualizar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtnserie)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PALMA036BIS3"
        Me.Text = "PALMA036BIS3"
        Me.ASDA.ResumeLayout(False)
        Me.ASDA.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtdesc As TextBox
    Friend WithEvents txtnserie As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnactualizar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbsap As ComboBox
    Friend WithEvents ASDA As GroupBox
End Class
