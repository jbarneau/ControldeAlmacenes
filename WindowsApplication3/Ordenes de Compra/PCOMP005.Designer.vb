<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCOMP005
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCOMP005))
        Me.CB_contrato = New System.Windows.Forms.ComboBox()
        Me.NDPOR = New System.Windows.Forms.NumericUpDown()
        Me.CBCONFIRMAR = New System.Windows.Forms.Button()
        CType(Me.NDPOR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CB_contrato
        '
        Me.CB_contrato.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CB_contrato.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CB_contrato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_contrato.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_contrato.FormattingEnabled = True
        Me.CB_contrato.Location = New System.Drawing.Point(16, 15)
        Me.CB_contrato.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_contrato.Name = "CB_contrato"
        Me.CB_contrato.Size = New System.Drawing.Size(539, 28)
        Me.CB_contrato.TabIndex = 2
        Me.CB_contrato.TabStop = False
        '
        'NDPOR
        '
        Me.NDPOR.Location = New System.Drawing.Point(563, 19)
        Me.NDPOR.Margin = New System.Windows.Forms.Padding(4)
        Me.NDPOR.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NDPOR.Name = "NDPOR"
        Me.NDPOR.Size = New System.Drawing.Size(75, 22)
        Me.NDPOR.TabIndex = 3
        Me.NDPOR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NDPOR.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CBCONFIRMAR
        '
        Me.CBCONFIRMAR.Location = New System.Drawing.Point(661, 10)
        Me.CBCONFIRMAR.Name = "CBCONFIRMAR"
        Me.CBCONFIRMAR.Size = New System.Drawing.Size(119, 39)
        Me.CBCONFIRMAR.TabIndex = 4
        Me.CBCONFIRMAR.Text = "CONFIRMAR"
        Me.CBCONFIRMAR.UseVisualStyleBackColor = True
        '
        'PCOMP005
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 66)
        Me.Controls.Add(Me.CBCONFIRMAR)
        Me.Controls.Add(Me.NDPOR)
        Me.Controls.Add(Me.CB_contrato)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PCOMP005"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PCOMP005"
        CType(Me.NDPOR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CB_contrato As ComboBox
    Friend WithEvents NDPOR As NumericUpDown
    Friend WithEvents CBCONFIRMAR As Button
End Class
